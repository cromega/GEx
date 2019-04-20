using System;
using GraphExperiment;
using System.Threading;

namespace OscillatorTest {
    class Program {
        static void Main(string[] args) {
            Logger.On();
            //var wav = new Utils.WavWriter("output.wav");

            int frames = 4410;
            var audio = new SoundSystem(frames);
            //var mdata = ">a; aEnvelope:1,200,0,0>0; 0Hydra:1,2>1; 1Envelope:50,250,1.0,200>-";
            var mdata = ">0; 0Hydra:1,5>1; 1Envelope:50,250,1.0,200>-";
            var machine = new Parser().ParseMachine(mdata);
            var trigger = new Trigger(machine, 220);

            var output = trigger.Next(0);

            var tick = 0;
            var end = false;

            for (; ;) {
                if (tick == frames * 30) { trigger.Release(); }

                var buffer = new short[frames * 2];
                for (int j = 0; j < buffer.Length; j += 2) {
                    var packet = trigger.Next(tick++);
                    if (packet.Signal == Signal.End) { Logger.Log("signal ended"); end = true; break; }

                    var sample = packet.Sample;
                    buffer[j] = (short)sample.L;
                    buffer[j + 1] = (short)sample.R;
                }
                audio.Write(buffer);
                if (end) { break; }
                //wav.Write(buffer);
            }
            Thread.Sleep(200);
            audio.Close();
            //wav.Close();
            Console.ReadKey();
        }
    }
}
