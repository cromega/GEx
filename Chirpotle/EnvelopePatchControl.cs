using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Chirpesizer;

namespace Chirpotle {
    public partial class EnvelopePatchControl : UserControl, IModulatorControl {
        public EnvelopePatchControl() {
            InitializeComponent();
        }

        public IModulator GetModulator() {
            var envelope = new Envelope(
                MTime.FromMs(envelopeControl1.Attack).Frames,
                MTime.FromMs(envelopeControl1.Decay).Frames,
                envelopeControl1.Sustain,
                MTime.FromMs(envelopeControl1.Release).Frames);
            return new EnvelopeModulator(envelope, patchableValueSelector1.PatchTargetId);
        }

        private void button1_Click(object sender, EventArgs e) {
            Parent.Controls.Remove(this);
            Dispose(true);
        }
    }
}
