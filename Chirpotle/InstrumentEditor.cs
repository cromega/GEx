using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Chirpesizer;
using Chirpesizer.Effects;

namespace Chirpotle {
    public partial class InstrumentEditor : Form {
        public Instrument Instrument;
        public string InstrumentName;
        private Triggerer InstrumentPlayer;

        public InstrumentEditor() {
            InitializeComponent();
            InstrumentPlayer = new Triggerer();
            InstrumentPlayer.Start();
        }

        public InstrumentEditor(string name, string instrumentData) {
            Debug.WriteLine(instrumentData);
            InitializeComponent();
            var instrument = InstrumentParser.Parse(instrumentData);
            NameEdit.Text = name;
            LoadInstrument(instrument);
            InstrumentPlayer = new Triggerer();
            InstrumentPlayer.Start();
        }

        private void LoadInstrument(Instrument instrument) {
            waveSelector1.SetSignalType(instrument.Osc);
            VolumeValue.Value = (decimal)instrument.Volume;
            //set main envelope
            var mainEnvelope = (EnvelopeModulator)instrument.Modulators.Last();
            MainEnvelope.Attack = (int)(mainEnvelope.Envelope.Attack / 44.1);
            MainEnvelope.Decay = (int)(mainEnvelope.Envelope.Decay / 44.1);
            MainEnvelope.Sustain = mainEnvelope.Envelope.Sustain;
            MainEnvelope.Release = (int)(mainEnvelope.Envelope.Release / 44.1);

            instrument.Modulators.Take(instrument.Modulators.Count() - 1).ToList().ForEach(mod => {
                AddModulator(mod);
            });
        }

        private void AddModulator(IModulator mod) {
            switch (mod.GetType().Name) {
                case "EnvelopeModulator": {
                        var modulatorControl = new EnvelopePatchControl((EnvelopeModulator)mod) { Parent = EffectsPanel };
                        break;
                    }
                case "LFOModulator": {
                        var modulatorControl = new LFOControl((LFOModulator)mod) { Parent = EffectsPanel };
                        break;
                    }
            }
        }

        private void SaveButton_Click(object sender, EventArgs e) {
            if (NameEdit.Text == "") {
                MessageBox.Show("Specify a name");
                return;
            }

            InstrumentName = NameEdit.Text;
            Instrument = CreateInstrument();
            DialogResult = DialogResult.OK;
            InstrumentPlayer.Stop();
            Close();
        }



        private Dictionary<Keys, double> KeysToNotes = new Dictionary<Keys, double>() {
            { Keys.Q, 16.35 },
            { Keys.A, 17.32 },
            { Keys.W, 18.35 },
            { Keys.S, 19.45 },
            { Keys.E, 20.60 },
            { Keys.R, 21.83 },
            { Keys.F, 23.12 },
            { Keys.T, 24.50 },
            { Keys.G, 25.96 },
            { Keys.Y, 27.50 },
            { Keys.H, 29.14 },
            { Keys.U, 30.87 },
            { Keys.I, 32.70 },
        };

        private Dictionary<Keys, Trigger> ActiveTriggers = new Dictionary<Keys, Trigger>();
        private HashSet<Keys> Pressedkeys = new HashSet<Keys>();
        private Instrument InstrumentPlaying;


        private void InstrumentEditor_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyData == Keys.Escape) {
                DialogResult = DialogResult.Cancel;
                InstrumentPlayer.Stop();
                Close();
            }

            if (Pressedkeys.Contains(e.KeyData)) { return; }
            Pressedkeys.Add(e.KeyData);
            if (!TestPanel.Focused) { e.Handled = true; return; }
            if (!KeysToNotes.ContainsKey(e.KeyData)) {
                return;
            }

            Instrument instrument;
            if (ActiveTriggers.Any()) {
                instrument = InstrumentPlaying;
            } else {
                instrument = CreateInstrument();
                InstrumentPlaying = instrument;
                InstrumentPlayer.SetInstrument(instrument);
            }
            var trigger = instrument.Activate(KeysToNotes[e.KeyData] * Math.Pow(2, 4), -1);
            ActiveTriggers[e.KeyData] = trigger;
        }

        private void InstrumentEditor_KeyUp(object sender, KeyEventArgs e) {
            Pressedkeys.Remove(e.KeyData);
            if (!TestPanel.Focused) { e.Handled = true; return; }
            if (!ActiveTriggers.ContainsKey(e.KeyData)) {
                return;
            }

            Trigger trigger;
            if (!ActiveTriggers.TryGetValue(e.KeyData, out trigger)) { return; }

            trigger.Release();
            ActiveTriggers.Remove(e.KeyData);
        }

        private Instrument CreateInstrument() {
            var envelope = new Envelope(MTime.FromMs(MainEnvelope.Attack).Frames, MTime.FromMs(MainEnvelope.Decay).Frames, MainEnvelope.Sustain, MTime.FromMs(MainEnvelope.Release).Frames);
            var volumeEnvelope = new EnvelopeModulator(envelope, "v");

            // get all modulators
            var modulatorControls = GetModulators();
            var modulators = new List<IModulator>();
            modulatorControls.ForEach(modctrl => modulators.Add(modctrl.GetModulator()));
            //modulators.Add(volumeEnvelope);

            var effects = new List<string>();
            var effectControls = GetEffects();
            effectControls.ForEach(effect => effects.Add(new EffectSerializer().Serialize(effect.GetEffect())));

            return new Instrument(waveSelector1.SignalType, (double)VolumeValue.Value, modulators, effects.ToArray());
        }

        private void panel2_MouseClick(object sender, MouseEventArgs e) {
            TestPanel.Focus();
        }

        private void lFOToolStripMenuItem_Click(object sender, EventArgs e) {
            var modulator = new LFOControl();
            modulator.Parent = EffectsPanel;
        }

        private List<IModulatorControl> GetModulators() {
            var modulators = new List<IModulatorControl>();
            foreach (Control ctrl in EffectsPanel.Controls) {
                if (ctrl is IModulatorControl) { modulators.Add((IModulatorControl)ctrl); }
            }
            return modulators;
        }

        private List<IEffectControl> GetEffects() {
            var effects = new List<IEffectControl>();
            foreach (Control ctrl in EffectsPanel.Controls) {
                if (ctrl is IEffectControl) { effects.Add((IEffectControl)ctrl); }
            }
            return effects;
        }

        private void envelopeToolStripMenuItem_Click(object sender, EventArgs e) {
            var modulator = new EnvelopePatchControl();
            modulator.Parent = EffectsPanel;
        }

        private void echoToolStripMenuItem_Click(object sender, EventArgs e) {
            var echo = new EchoEffect();
            echo.Parent = EffectsPanel;
        }
    }
}
