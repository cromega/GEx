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
    public partial class MainForm : Form {
        public MainForm() {
            InitializeComponent();
        }
        private SoundSystem Sound;
        private List<Instrument> Instruments;

        private void Form1_Load(object sender, EventArgs e) {
            Sound = new SoundSystem(4410);
            Instruments = new List<Instrument>();
            
        }

        private void button1_Click(object sender, EventArgs e) {
        }

        private void AddInstrumentButton_Click(object sender, EventArgs e) {
            using (var instrcreator = new InstrumentEditor()) {
                instrcreator.ShowDialog();
                if (instrcreator.DialogResult != DialogResult.OK) { return; }

                Instruments.Add(instrcreator.Instrument);
                InstrumentSelector.Items.Add(instrcreator.InstrumentName);   
            }
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
