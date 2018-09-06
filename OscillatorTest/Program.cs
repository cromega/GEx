using System;
using GraphExperiment;
using System.Threading;
using Utils;

namespace OscillatorTest {
    class Program {
        static void Main(string[] args) {
            Logger.On();
            var wav = new Utils.WavWriter("output.wav");

            int frames = 4410;
            var audio = new SoundSystem(frames);
            var trigger = new Trigger(1);
            var envelope = new Envelope(2) {
                Attack = 50,
                Decay = 250,
                Sustain = 1.0,
                Release = 200,
            };
            var osc = new Hydra(3) {
                SignalType = SignalType.Sawtooth,
                Cents = 5,
            };
            envelope.Connect(osc);
            osc.Connect(trigger);
            trigger.Start(110);

            var last = envelope;

            for (int i = 0; i < 20; i++) {
                var buffer = new short[frames * 2];
                for (int j = 0; j < buffer.Length; j += 2) {
                    var packets = last.Next();

                    var sample = packets[0].Sample;
                    buffer[j] = (short)sample.L;
                    buffer[j + 1] = (short)sample.R;
                }
                audio.Write(buffer);
                wav.Write(buffer);
            }
            Thread.Sleep(envelope.Release + 200);
            audio.Close();
            wav.Close();
            Console.ReadKey();
        }
    }
}
