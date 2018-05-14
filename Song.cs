using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;

namespace chirpcore {
    public class Song {
        private Instrument[] Instruments;
        private int Channels;
        private string[] TrackLines;
        private int trackIndex;
        public readonly int Tempo;

        public Song(string SongData) {
            var instruments = new List<Instrument>();
            var lines = SongData.Split("\n".ToCharArray());

            // 0 1
            lines[0].Split(" ".ToCharArray()).ToList().ForEach(instrument =>
                instruments.Add(InstrumentParser.Parse(instrument))
            );
            Instruments = instruments.ToArray();

            //4:100
            // channels, length of a pattern line in ms
            var trackParams = lines[1].Split(",".ToCharArray());
            Channels = int.Parse(trackParams[0]);

            Tempo = 100;
            // Tempo = int.Parse(trackParams[1]);

            TrackLines = lines.Skip(2).Take(lines.Length - 2).ToArray();
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
                var triggerLength = int.Parse(nodeParams[1]);
                var freq = int.Parse(nodeParams[2]);
                Instruments[instrumentIndex].Activate((double)freq, Tempo * triggerLength);
            }

            // render instruments
            Instruments.Where(instr => instr.IsActive()).ToList().ForEach(instr => {
                buffers.AddRange(instr.RenderAll(frames));             
            });

            Logger.Log("rendering finished");
            return buffers;
        }

        public bool Ended() {
            return !Instruments.Any(inst => inst.IsActive());
        }
    }
}