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
            comboBox1.DataSource = Enum.GetValues(typeof(OscillatorType));
            comboBox1.SelectedItem = OscillatorType.Sine;
        }

        private OscillatorType GetSignalType() {
            return (OscillatorType)comboBox1.SelectedItem;
        }

        public void SetSignalType(OscillatorType waveType) {
            comboBox1.SelectedItem = waveType;
        }
    }
}
