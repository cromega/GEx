using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chirpotle {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }
        private chirpcore.SoundSystem Sound;

        private void Form1_Load(object sender, EventArgs e) {
            Sound = new chirpcore.SoundSystem(4410);
        }

        private void button1_Click(object sender, EventArgs e) {
            chirpcore.IGenerator osc;
            if (NoiseGeneratorButton.Checked) {
                osc = new chirpcore.NoiseGenerator();
            } else if (SineGeneratorButton.Checked) {
                osc = new chirpcore.SineGenerator();
            } else {
                osc = new chirpcore.SquareGenerator();
            }

            var i = new chirpcore.Instrument(osc, 0.5, new chirpcore.Envelope((int)((double)AttackValue.Value * 44.1), (int)((double)DecayValue.Value * 44.1), (double)SustainValue.Value, (int)((double)ReleaseValue.Value * 44.1)));
            i.Activate(440, (int)(100 * 44.1));

            while (true) {
                var buffers = i.RenderAll(4410);
                Sound.AddBuffers(buffers);
                if (!i.IsActive()) { break; }
            }
        }
    }
}
