using System;
using System.Runtime.InteropServices;
using System.IO;
using Chirpesizer;
using NAudio;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Collections.Concurrent;
using GraphExperiment;

namespace playsong {
    class Program {
        static void Main(string[] args) {
            /*
            GraphExperiment.Logger.On();
            var wavwriter = new NAudio.Wave.WaveFileWriter("output.wav", new NAudio.Wave.WaveFormat());

            int frames = 4410;
            var audio = new GraphExperiment.SoundSystem(frames);
            var channel = new Wire(frames);
            var output = new Wire(frames);
            var envelope = new GraphExperiment.Envelope(2, 4410, 4410, 0.8, 8820, output);
            var source = new Generator(1, SignalType.Sine, envelope.Input) {
                Frequency = 440
            };
            source.Trigger();
            for (int i=0; i<5; i++) {
                var buffer = new short[frames * 2];
                for (int j=0; j<buffer.Length; j+=2) {
                    var sample = output.Take().Sample * 20000;
                    buffer[j] = (short)sample.L;
                    buffer[j + 1] = (short)sample.R;
                }
                audio.Write(buffer);
                var wavbuffer = new byte[frames * 4];
                Buffer.BlockCopy(buffer, 0, wavbuffer, 0, buffer.Length * 2);
                wavwriter.Write(wavbuffer, 0, wavbuffer.Length);
                //source.Frequency += 50;
            }
            source.Release();
            for (int i=0; i<5; i++) {
                var buffer = new short[frames * 2];
                for (int j=0; j<buffer.Length; j+=2) {
                    var packet = output.Take();
                    if (packet.Control == Control.End) {
                        break;
                    }
                    packet.Sample *= 20000;
                    buffer[j] = (short)packet.Sample.L;
                    buffer[j + 1] = (short)packet.Sample.R;
                }
                audio.Write(buffer);
                var wavbuffer = new byte[frames * 4];
                Buffer.BlockCopy(buffer, 0, wavbuffer, 0, buffer.Length * 2);
                wavwriter.Write(wavbuffer, 0, wavbuffer.Length);
                //source.Frequency += 50;
            }
            Thread.Sleep(300);
            audio.Close();
            wavwriter.Close();
            Console.ReadKey();
            return;
            */
            Chirpesizer.Logger.On();

            var audioOutput = new Chirpesizer.SoundSystem(4410);
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
