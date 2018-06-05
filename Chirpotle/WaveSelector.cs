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
            switch (comboBox1.SelectedItem) {
                case "Noise": return OscillatorType.Noise;
                case "Sine": return OscillatorType.Sine;
                case "Square": return OscillatorType.Square;
                case "Sawtooth": return OscillatorType.Sawtooth;
                case "Triangle": return OscillatorType.Triangle;
                default: return OscillatorType.Sine;
            }

            throw new Exception("wtf");
        }

        private void WaveSelector_Load(object sender, EventArgs e) {
            foreach (var signalType in new string[] { "Noise", "Sine", "Square", "Sawtooth", "Triangle" }) {
                comboBox1.Items.Add(signalType);
            }
            comboBox1.SelectedIndex = 1;
        }
    }
}
