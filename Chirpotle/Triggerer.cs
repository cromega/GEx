using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Chirpesizer;

namespace Chirpotle {
    class Triggerer {
        private static Triggerer instance = null;
        private static readonly object Lock = new object();

        private SoundSystem Audio;
        private List<Trigger> Triggers;
        private bool LoopStop;
        int Time;

        public Triggerer() {
            Audio = new SoundSystem(2205);
            Triggers = new List<Trigger>();
            LoopStop = false;
            var thread = new Thread(new ThreadStart(TriggerLoop));
            thread.Start();
            Time = 0;

        }

        public void TriggerLoop() {
            var mixer = new Mixer();
            var converter = new Converter();

            while (true) {
                if (LoopStop) { break; }
                if (Triggers.Count == 0) {
                    Thread.Sleep(10);
                    continue;
                }

                var buffers = new List<double[]>();
                lock (Lock) {
                    Triggers.ForEach(trig => {
                        buffers.Add(trig.Render(2205, Time));
                    });
                }
                Time += 2205;

                double[] mixed = new double[4410];
                mixer.Mix(mixed, buffers);
                short[] output = new short[4410];
                converter.Convert(mixed, output);
                Audio.Write(output);

                lock (Lock) {
                    Triggers.RemoveAll(trig => trig.Finished);
                }
            }
        }

        public void Stop() {
            LoopStop = true;
        }

        public static Triggerer Instance {
            get {
                lock (Lock) {
                    if (instance == null) { instance = new Triggerer(); }
                    return instance;
                }
            }
        }

        public void Add(Trigger trigger) {
            lock (Lock) {
                Triggers.Add(trigger);
            }
        }
    }
}
