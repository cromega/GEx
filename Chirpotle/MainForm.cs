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
        private Project Project;

        private void Form1_Load(object sender, EventArgs e) {
            Sound = new SoundSystem(4410);
            webBrowser1.Url = new Uri(String.Format("file:///{0}/index.html", System.IO.Directory.GetCurrentDirectory()));
            //webBrowser1.Url = new Uri(String.Format("https://useragentapi.com/"));
            Project = new Project();
        }

        private void button1_Click(object sender, EventArgs e) {
        }

        private void AddInstrumentButton_Click(object sender, EventArgs e) {
            using (var instrcreator = new InstrumentEditor()) {
                instrcreator.ShowDialog();
                if (instrcreator.DialogResult != DialogResult.OK) { return; }

                Project.Instruments.Add(instrcreator.Instrument);
                InstrumentSelector.Items.Add(instrcreator.InstrumentName);   
            }
        }

        private void EditInstrumentButton_Click(object sender, EventArgs e) {
            if (InstrumentSelector.SelectedIndex == -1) {
                MessageBox.Show("Select an instrument");
                return;
            }

            var instrumentData = new InstrumentSerializer(Project.Instruments[InstrumentSelector.SelectedIndex]).Serialize();
            using (var instrcreator = new InstrumentEditor((string)InstrumentSelector.SelectedItem, instrumentData)) {
                instrcreator.ShowDialog();
                if (instrcreator.DialogResult != DialogResult.OK) { return; }

                Project.Instruments[InstrumentSelector.SelectedIndex] = instrcreator.Instrument;
            }
        }
    }
}
