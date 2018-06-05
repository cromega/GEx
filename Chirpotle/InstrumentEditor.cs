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

        public InstrumentEditor() {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, EventArgs e) {
            if (NameEdit.Text == "") {
                MessageBox.Show("Specify a name");
                return;
            }

            InstrumentName = NameEdit.Text;
            Instrument = CreateInstrument();
            DialogResult = DialogResult.OK;
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
        private Triggerer InstrumentPlayer = new Triggerer();
        private HashSet<Keys> Pressedkeys = new HashSet<Keys>();


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

            var instru = CreateInstrument();
            var trigger = instru.Activate(KeysToNotes[e.KeyData] * Math.Pow(2, 4), -1);
            ActiveTriggers[e.KeyData] = trigger;
            InstrumentPlayer.Add(trigger);
        }

        private void InstrumentEditor_KeyUp(object sender, KeyEventArgs e) {
            Pressedkeys.Remove(e.KeyData);
            if (!TestPanel.Focused) { e.Handled = true; return; }
            if (!KeysToNotes.ContainsKey(e.KeyData)) {
                return;
            }

            var trigger = ActiveTriggers[e.KeyData];
            if (trigger != null) {
                trigger.Release();
                ActiveTriggers.Remove(e.KeyData);
            } else {
                throw new Exception("wtf");
            }
        }

        private Instrument CreateInstrument() {
            var envelope = new Envelope(MTime.FromMs(MainEnvelope.Attack).Frames, MTime.FromMs(MainEnvelope.Decay).Frames, MainEnvelope.Sustain, MTime.FromMs(MainEnvelope.Release).Frames);
            var volumeEnvelope = new EnvelopeModulator(envelope, "v");

            // get all modulators
            var modulatorControls = GetModulators();
            var modulators = new List<IModulator>();
            modulatorControls.ForEach(modctrl => modulators.Add(modctrl.GetModulator()));
            modulators.Add(volumeEnvelope);
            return new Instrument(waveSelector1.SignalType, (double)VolumeValue.Value, modulators);
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

                Debug.WriteLine("ctrl fond");
                if (ctrl is IModulatorControl) {  modulators.Add((IModulatorControl)ctrl); }
            }
            return modulators;
        }
    }
}
