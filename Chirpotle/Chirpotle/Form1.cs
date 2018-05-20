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

namespace Chirpotle {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }
        private Chirpesizer.SoundSystem Sound;

        private void Form1_Load(object sender, EventArgs e) {
            Sound = new Chirpesizer.SoundSystem(4410);
        }

        private void button1_Click(object sender, EventArgs e) {
            Oscillator osc;
            if (NoiseGeneratorButton.Checked) {
                osc = Oscillator.Noise;
            } else if (SineGeneratorButton.Checked) {
                osc = Oscillator.Sine;
            } else {
                osc = Oscillator.Square;
            }

            var i = new Chirpesizer.Instrument(osc, (double)VolumeValue.Value, new Chirpesizer.Envelope((int)((double)AttackValue.Value * 44.1), (int)((double)DecayValue.Value * 44.1), (double)SustainValue.Value, (int)((double)ReleaseValue.Value * 44.1)));
            i.Activate(440, (int)(1000 * 44.1));

            while (true) {
                var buffers = i.RenderAll(4410);
                Sound.AddBuffers(buffers);
                if (!i.IsActive()) { break; }
            }
        }
    }
}
