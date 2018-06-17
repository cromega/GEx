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

        public EnvelopePatchControl(EnvelopeModulator envelope) {
            InitializeComponent();
            envelopeControl1.Attack = (int)(envelope.Envelope.Attack / 44.1);
            envelopeControl1.Decay = (int)(envelope.Envelope.Decay / 44.1);
            envelopeControl1.Sustain = envelope.Envelope.Sustain;
            envelopeControl1.Release = (int)(envelope.Envelope.Release / 44.1);
            patchableValueSelector1.SetPatchTarget(envelope.GetTarget());
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
