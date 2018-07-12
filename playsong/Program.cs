using System;
using System.Runtime.InteropServices;
using System.IO;
using Chirpesizer;
using NAudio;
using System.Collections.Generic;
using System.Threading;

namespace playsong {
    class Program {
        public static short[] GetRandomBuffer(int frames) {
            var buffer = new short[frames * 2];
            var rnd = new Random();
            double sample;
            for (int i=0; i<buffer.Length; i+=2) {
                sample = rnd.NextDouble() * 2 - 1;
                sample *= 10000;
                buffer[i] = (short)sample;
                buffer[i+1] = (short)sample;
            }
            return buffer;
        }

        static void Main(string[] args) {
            GraphExperiment.Logger.On();
            int frames = 4410;
            var audio = new GraphExperiment.SoundSystem(frames);
            for (int i=0; i<10; i++) {
                var buffer = GetRandomBuffer(frames);
                audio.Write(buffer);
            }

            Thread.Sleep(2000);
            audio.Close();
            Console.ReadKey();
            return;

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
