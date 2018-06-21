using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Chirpesizer;

namespace Chirpotle {
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public partial class MainForm : Form {
        public MainForm() {
            InitializeComponent();
        }
        private SoundSystem Sound;
        private Project Project;

        private void Form1_Load(object sender, EventArgs e) {
            Sound = new SoundSystem(4410);
            Sequencer.Url = new Uri(String.Format("file:///{0}/index.html", System.IO.Directory.GetCurrentDirectory()));
            Sequencer.ObjectForScripting = this;
            Project = new Project();
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

        public void SendSongData(string[] songData) {
            foreach (var line in songData) {
                Debug.WriteLine(line);
            }
        }

        private void PlayButton_Click(object sender, EventArgs e) {
            if (Project.Instruments.Count() == 0) {
                MessageBox.Show("No instruments");
                return;
            }

            var track = (string)Sequencer.Document.InvokeScript("getSongData");
            var lines = track.Split("|".ToCharArray()).ToList();
            for (int i=0; i<Project.Instruments.Count(); i++) {
                var instrumentData = new InstrumentSerializer(Project.Instruments[i]).Serialize();
                lines.Insert(i, String.Format("i{0}", instrumentData));
            }

            var songData = String.Join(Environment.NewLine, lines);
            Debug.WriteLine(songData);
            var audio = new SoundSystem(4410);
            var song = new Song(songData);
            do {
                var buffers = song.RenderNext(4410);
                var output = MixBuffers(buffers);
                audio.Write(output);
            } while (!song.Ended());
            audio.Close();
        }

        public void DebugLog(string message) {
            Debug.WriteLine(message);
        }

        public static short[] MixBuffers(List<double[]> buffers) {
            var mixedBuffer = new double[4410 * 2];
            new Mixer().Mix(mixedBuffer, buffers);
            var outputBuffer = new short[8820];
            new Converter().Convert(mixedBuffer, outputBuffer);
            return outputBuffer;
        }

    }
}