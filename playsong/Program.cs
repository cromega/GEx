using System;
using System.IO;
using System.Collections.Generic;
using GraphExperiment;
using System.Text;
using System.Threading;
using Utils;

namespace playsong {
    class Program {
        static void Main(string[] args) {
            GraphExperiment.Logger.On();
            var frames = 4410;
            var audio = new GraphExperiment.SoundSystem(frames);
            var song = File.ReadAllLines("song.txt");
            var track = new Track(song);
            var muxer = new Muxer();
            var wav = new Utils.WavWriter("output.wav");

            for (; ;) {
                var buffer = new short[frames * 2];
                for (int i = 0; i < frames; i++) {
                    var packets = track.Next();
                    foreach (var packet in packets) {
                        muxer.Add(packet.Sample);
                    }
                    var sample = muxer.Mux();
                    buffer[i * 2] = (short)sample.L;
                    buffer[i * 2 + 1] = (short)sample.R;
                    if (track.HasEnded()) { break; }
                }

                audio.Write(buffer);
                wav.Write(buffer);
                if (track.HasEnded()) { break; }
            }
            wav.Close();
            Thread.Sleep(200);
        }
    }
}
