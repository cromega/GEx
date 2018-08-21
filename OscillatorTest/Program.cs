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
                Attack = 250,
                Decay = 250,
                Sustain = 0.5,
                Release = 200,
            };
            var osc = new Generator(3) {
                SignalType = SignalType.Sine,
            };
            //trigger.Connect(osc);
            trigger.Connect(envelope);
            envelope.Connect(osc);
            trigger.Start(440);

            for (int i = 0; i < 10; i++) {
                var buffer = new short[frames * 2];
                for (int j = 0; j < buffer.Length; j += 2) {
                    var packets = osc.Next();

                    var sample = packets[0].Sample;
                    buffer[j] = (short)sample.L;
                    buffer[j + 1] = (short)sample.R;
                    //trigger.Triggers[0].Frequency -= 0.005;
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
