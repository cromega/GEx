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

        public IModulator GetModulator() {
            return new LFOModulator(waveSelector1.SignalType, (double)FrequencyValue.Value, (double)AmplitudeValue.Value, patchableValueSelector1.PatchTargetId);
        }
    }
}
