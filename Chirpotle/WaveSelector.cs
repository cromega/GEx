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
    public partial class WaveSelector : UserControl {
        public OscillatorType SignalType {
            get { return GetSignalType(); }
        }

        public WaveSelector() {
            InitializeComponent();
        }

        private OscillatorType GetSignalType() {
            switch (comboBox1.SelectedText) {
                case "Noise": return OscillatorType.Noise;
                case "Sine": return OscillatorType.Sine;
                case "Square": return OscillatorType.Square;
            }

            throw new Exception("wtf");
        }

        private void WaveSelector_Load(object sender, EventArgs e) {
            comboBox1.SelectedIndex = 1;
        }
    }
}
