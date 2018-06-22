using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using Chirpesizer;

namespace Chirpotle {
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public partial class MainForm : Form {
        public MainForm() {
            InitializeComponent();
        }
        private Project Project;
        private BackgroundWorker SongPlayer;

        private void Form1_Load(object sender, EventArgs e) {
            Sequencer.Url = new Uri(String.Format("file:///{0}/index.html", System.IO.Directory.GetCurrentDirectory()));
            Sequencer.ObjectForScripting = this;
            Project = new Project();
            InstrumentSelector.DataSource = new BindingSource(Project.Instruments, null);
            InstrumentSelector.DisplayMember = "Name";
            InstrumentSelector.ValueMember = "InstrumentData";
            Project.Instruments.ListChanged += UpdateTrackerWithInstrumentListChange;

            SongPlayer = new BackgroundWorker();
            SongPlayer.DoWork += PlaySong;
        }

        private void PlaySong(object sender, DoWorkEventArgs e) {
            var worker = (BackgroundWorker)sender;
            SoundSystem audio = null;
            try {
                var songData = (string)e.Argument;
                Debug.WriteLine(songData);
                audio = new SoundSystem(4410);
                var song = new Song(songData);
                do {
                    if (worker.CancellationPending) {
                        break;
                    }
                    var buffers = song.RenderNext(4410);
                    var output = MixBuffers(buffers);
                    audio.Write(output);
                } while (!song.Ended());
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "WTF", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } finally {
                if (audio != null) { audio.Close(); }
            }
        }

        private void UpdateTrackerWithInstrumentListChange(object sender, ListChangedEventArgs e) {
            switch (e.ListChangedType) {
                case ListChangedType.ItemAdded:
                    AddInstrumentToTracker(Project.Instruments[e.NewIndex]);
                    break;
                case ListChangedType.ItemDeleted:
                    RemoveInstrumentFromTracker(Project.Instruments[e.NewIndex]);
                    break;
            }
        }

        private void RemoveInstrumentFromTracker(InstrumentItem instrumentItem) {
            Sequencer.Document.InvokeScript("removeInstrument", new object[] { instrumentItem.Name });
        }

        private void AddInstrumentToTracker(InstrumentItem instrumentItem) {
            Sequencer.Document.InvokeScript("addInstrument", new Object[] { instrumentItem.Name });
        }

        private void AddInstrumentButton_Click(object sender, EventArgs e) {
            using (var instrcreator = new InstrumentEditor()) {
                instrcreator.ShowDialog();
                if (instrcreator.DialogResult != DialogResult.OK) { return; }

                Project.Instruments.Add(new InstrumentItem(instrcreator.InstrumentName, instrcreator.Instrument));
            }
        }

        private void EditInstrumentButton_Click(object sender, EventArgs e) {
            if (InstrumentSelector.SelectedIndex == -1) {
                MessageBox.Show("Select an instrument");
                return;
            }

            var instrument = (InstrumentItem)InstrumentSelector.SelectedItem;
            using (var instrcreator = new InstrumentEditor(instrument.Name, instrument.InstrumentData)) {
                instrcreator.ShowDialog();
                if (instrcreator.DialogResult != DialogResult.OK) { return; }

                Project.Instruments.RemoveAt(InstrumentSelector.SelectedIndex);
                Project.Instruments.Add(new InstrumentItem(instrcreator.InstrumentName, instrcreator.Instrument));
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
                var instrument = Project.Instruments[i];
                lines.Insert(i, String.Format("i{0}", instrument.InstrumentData));
            }

            var songData = String.Join(Environment.NewLine, lines);
            SongPlayer.RunWorkerAsync(songData);
        }

        #region functions exported to tracker
        public void DebugLog(string message) {
            Debug.WriteLine(message);
        }
        #endregion

        public static short[] MixBuffers(List<double[]> buffers) {
            var mixedBuffer = new double[4410 * 2];
            new Mixer().Mix(mixedBuffer, buffers);
            var outputBuffer = new short[8820];
            new Converter().Convert(mixedBuffer, outputBuffer);
            return outputBuffer;
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e) {
            switch (e.KeyCode) {
                case Keys.Escape:
                    if (SongPlayer.IsBusy) {
                        SongPlayer.CancelAsync();
                    }
                    break;
            }
        }
    }
}