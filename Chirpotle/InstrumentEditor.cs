using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

            var envelope = new Envelope(
                MTime.FromMs(envelopeControl1.Attack).Frames,
                MTime.FromMs(envelopeControl1.Decay).Frames,
                envelopeControl1.Sustain,
                MTime.FromMs(envelopeControl1.Release).Frames
                );

            InstrumentName = NameEdit.Text;
            Instrument = new Instrument(waveSelector1.SignalType, new StaticValue(Convert.ToDouble(VolumeValue.Value)), envelope, new List<IEffect>());
        }
    }
}
