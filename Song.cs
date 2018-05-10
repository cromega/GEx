using System;
using System.Linq;
using System.Collections.Generic;

namespace chirpcore {
    public class Song {
        private Instrument[] Instruments;
        private string SongData;
        private SoundSystem Sound;
        private int Channels;
        private string[] TrackLines;
        public readonly int Tempo;

        public Song(string SongData) {
            var instruments = new List<Instrument>();
            var lines = SongData.Split("\n".ToCharArray());

            // 0 1
            lines[0].Split(" ".ToCharArray()).ToList().ForEach(instrument =>
                instruments.Add(CreateInstrument(instrument))
            );
            Instruments = instruments.ToArray();

            //4:100
            // channels, length of a pattern line in ms
            var trackParams = lines[1].Split(",".ToCharArray());
            Channels = int.Parse(trackParams[0]);
            Tempo = int.Parse(trackParams[1]);

            TrackLines = lines.Skip(2).Take(lines.Length - 2).ToArray();
            Sound = new SoundSystem(Tempo);
        }

        public void Play() {
            for (int i=0; i<TrackLines.Length; i++) {
                var trackLine = TrackLines[i];
                var nodes = trackLine.Split(" ".ToCharArray());
                for (int j=0; j<nodes.Length; j++) {
                    if (nodes[j] == "-") { continue; }
                    // 0,8,440
                    // instrument index, trigger length in track lines, frequency
                    var nodeParams = nodes[j].Split(",".ToCharArray());
                    var instrumentIndex = int.Parse(nodeParams[0]);
                    var triggerLength = int.Parse(nodeParams[1]);
                    var freq = int.Parse(nodeParams[2]);
                    Instruments[instrumentIndex].Activate((double)freq, Tempo * triggerLength);
                }

                Instruments.Where(inst => inst.IsActive()).ToList().ForEach(instr => {
                    Sound.Render(instr);
                });
                System.Threading.Thread.Sleep(Tempo);
            }
        }

        private Instrument CreateInstrument(string instrument) {
            // oscillator type
            switch (instrument) {
                case "0":
                    return new Instrument(new NoiseGenerator());
                case "1":
                    return new Instrument(new SineGenerator());
            }

            throw new Exception(String.Format("Can't create instrument from \"{0}\"", instrument));
        }
    }
}