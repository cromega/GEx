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
    public partial class LFOControl : UserControl, IModulatorControl {
        public LFOControl() {
            InitializeComponent();
        }

        public LFOControl(LFOModulator lfo) {
            InitializeComponent();
            waveSelector1.SetSignalType(lfo.Oscillator.OscillatorType);
            FrequencyValue.Value = (decimal)lfo.Frequency;
            AmplitudeValue.Value = (decimal)lfo.Amplitude;
        }

        public IModulator GetModulator() {
            return new LFOModulator(waveSelector1.SignalType, (double)FrequencyValue.Value, (double)AmplitudeValue.Value, patchableValueSelector1.PatchTargetId);
        }

        private void button1_Click(object sender, EventArgs e) {
            Parent.Controls.Remove(this);
            Dispose(true);
        }
    }
}
