using System;
using System.Runtime.InteropServices;
using System.IO;
using Chirpesizer;
using NAudio;
using System.Collections.Generic;

namespace playsong {
    class Program {
        static void Main(string[] args) {
            Logger.On();

            var audioOutput = new SoundSystem(4410);
            var song = new Song(File.ReadAllText("song.txt"));

            var ww = new NAudio.Wave.WaveFileWriter("song.wav", new NAudio.Wave.WaveFormat(44100, 2));

            do {
                var buffers = song.RenderNext(4410);
                var output = MixBuffers(buffers);
                audioOutput.Write(output);
                var tofile = new byte[output.Length * 2];
                Buffer.BlockCopy(output, 0, tofile, 0, tofile.Length);
                ww.Write(tofile, 0, tofile.Length);
            } while (!song.Ended());
            ww.Close();

            System.Threading.Thread.Sleep(300);
            audioOutput.Close();
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
