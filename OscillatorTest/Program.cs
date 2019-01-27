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
            var mdata = "0Trigger:>1; 1Hydra:0,2>2; 2Envelope:50,250,1.0,200>-";
            var machine = new Parser().ParseMachine(mdata);
            //var trigger = new Trigger();
            //var envelope = new Envelope() {
            //    Attack = 50,
            //    Decay = 250,
            //    Sustain = 1.0,
            //    Release = 200,
            //};
            //var osc = new Hydra() {
            //    SignalType = SignalType.Sawtooth,
            //    Cents = 5,
            //};
            //envelope.Connect(osc);
            //osc.Connect(trigger);
            //trigger.Start(440, "t");

            var last = machine.Nodes[2];
            (machine.Nodes[0] as Trigger).Start(440, "t");

            var tick = 0;
            for (int i = 0; i < 20; i++) {
                var buffer = new short[frames * 2];
                for (int j = 0; j < buffer.Length; j += 2) {
                    var packets = last.Next(tick++);

                    var sample = packets[0].Sample;
                    buffer[j] = (short)sample.L;
                    buffer[j + 1] = (short)sample.R;
                }
                audio.Write(buffer);
                wav.Write(buffer);
            }
            Thread.Sleep(400);
            audio.Close();
            wav.Close();
            Console.ReadKey();
        }
    }
}
