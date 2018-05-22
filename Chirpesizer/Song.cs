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

        public Song(string SongData) {
            var instruments = new List<Instrument>();
            var lines = SongData.Split(Environment.NewLine.ToCharArray());

            // 0 1
            lines[0].Split(" ".ToCharArray()).ToList().ForEach(instrument =>
                instruments.Add(InstrumentParser.Parse(instrument))
            );
            Instruments = instruments.ToArray();

            Tempo = 100;
            // Tempo = int.Parse(trackParams[1]);

            TrackLines = lines.Skip(1).Take(lines.Length - 2).Where(line => line != "").ToArray();
            trackIndex = 0;
        }

        public List<double[]> RenderNext(int frames) {
            Logger.Log("rendering started");
            var buffers = new List<double[]>();
            var trackLine = TrackLines[trackIndex++];
            var nodes = trackLine.Split(" ".ToCharArray());
            // Logger.Log("rendering line: {0}", trackLine);

            // update song state with new trackline
            foreach (var node in nodes) {
                if (node == "-") { continue; }
                // 0,8,440
                // instrument index, trigger length in track lines, frequency
                var nodeParams = node.Split(",".ToCharArray());
                var instrumentIndex = int.Parse(nodeParams[0]);
                var triggerLength = double.Parse(nodeParams[1]);
                var freq = int.Parse(nodeParams[2]);
                Instruments[instrumentIndex].Activate((double)freq, MTime.FromMs((int)(Tempo * triggerLength)).Frames);
            }

            // render instruments
            Instruments.ToList().ForEach(instr => {
                buffers.AddRange(instr.RenderAll(frames));             
            });

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
            return !Instruments.Any(inst => inst.IsActive());
        }
    }
}