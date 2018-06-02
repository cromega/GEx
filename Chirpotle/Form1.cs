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
        private SoundSystem Sound;

        private void Form1_Load(object sender, EventArgs e) {
            Sound = new SoundSystem(4410);
        }

        private void button1_Click(object sender, EventArgs e) {
            //var songData = string.Format("{0}\r\n0,4,440");
            //var sound = new SoundSystem(4410);
            //var song = new Song(songData);
            //do {
            //    var buffers = song.RenderNext(4410);
            //    sound.AddBuffers(buffers);
            //} while (!song.Ended());
        }

        private void AddInstrumentButton_Click(object sender, EventArgs e) {
            string instrumentData;
            using (var instrcreator = new InstrumentEditor()) {
                instrcreator.ShowDialog();
                instrumentData = instrcreator.InstrumentData;
            }
            InstrumentSelector.Items.Add(instrumentData);
        }

        private void EditInstrumentButton_Click(object sender, EventArgs e) {
            if (InstrumentSelector.SelectedIndex == -1) {
                MessageBox.Show("Select an instrument");
                return;
            }

            MessageBox.Show("Nothing to see here.");
        }
    }
}
