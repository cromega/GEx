using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;

namespace Chirpesizer {
    public class Song {
        private Instrument[] Instruments;
        private string[] TrackLines;
        private int trackIndex;
        public readonly int Tempo;
        public List<Trigger> Triggers;

        public Song(string SongData) {
            var instruments = new List<Instrument>();
            var lines = SongData.Split(Environment.NewLine.ToCharArray()).Where(line => !line.StartsWith("#") && line != "").ToArray();
            // skip comments

            int linesToSkip = 0;
            while (true) {
                var line = lines[linesToSkip];
                if (!line.StartsWith("i")) { break; }
                instruments.Add(InstrumentParser.Parse(line.TrimStart("i".ToCharArray())));
                linesToSkip++;
            }
            Instruments = instruments.ToArray();
            if (Instruments.Count() == 0) { throw new Exception("Song has no instruments"); }

            Tempo = 100;
            // Tempo = int.Parse(trackParams[1]);

            TrackLines = lines.Skip(linesToSkip).Where(line => line != "").ToArray();
            trackIndex = 0;
            Triggers = new List<Trigger>();
        }

        public List<double[]> RenderNext(int frames) {
            Logger.Log("rendering started");
            var buffers = new List<double[]>();
            // only increment tracklines if there are still lines to read
            // otherwise keep playing the triggers until they all finish
            // FIXME: this shit
            if (!EndOfTrack()) {
                var trackLine = TrackLines[trackIndex++];

                // update song state with new trackline
                var nodes = Node.ParseAll(trackLine);
                foreach (var trigger in nodes) {
                    var lengthInSamples = MTime.FromMs((int)(Tempo * trigger.Length)).Frames;
                    Triggers.Add(Instruments[trigger.InstrumentIndex].Activate(trigger.Frequency, lengthInSamples));
                }
            }

            // render instruments
            Triggers.ForEach(trigger => {
                buffers.Add(trigger.Render(4410));
            });
            Triggers.RemoveAll(trigger => trigger.Finished);

            Logger.Log("rendering finished");
            return buffers;
        }

        public bool Ended() {
            return EndOfTrack() && NoInstrumentsPlaying();

        }

        private bool EndOfTrack() {
            return trackIndex == TrackLines.Length;
        }

        private bool NoInstrumentsPlaying() {
            return Triggers.Count() == 0;
        }
    }
}