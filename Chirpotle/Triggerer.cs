using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Chirpesizer;

namespace Chirpotle {
    class Triggerer {
        private object Lock = new object();

        private SoundSystem Audio;
        private bool LoopStop;
        private int Time;
        private Instrument Instrument;
        private Thread thread;

        public Triggerer() {
            Audio = new SoundSystem(2205);
            LoopStop = false;
            Time = 0;
        }

        public void Start() {
            thread = new Thread(new ThreadStart(TriggerLoop));
            thread.Start();
        }

        public void SetInstrument(Instrument instrument) {
            lock (Lock) {
                Instrument = instrument;
            }
        }

        public void TriggerLoop() {
            var mixer = new Mixer();
            var converter = new Converter();

            while (true) {
                if (LoopStop) { break; }

                var buffers = new List<double[]>();
                lock (Lock) {
                    try {
                        if (Instrument != null) { buffers.AddRange(Instrument.RenderTriggers(2205, Time)); }
                    } catch {
                        Debug.WriteLine("wtf happened");
                        continue;
                    }
                }
                if (buffers.Count == 0) {
                    Thread.Sleep(10);
                    continue;
                }
                Time += 2205;

                double[] mixed = new double[4410];
                mixer.Mix(mixed, buffers);
                short[] output = new short[4410];
                converter.Convert(mixed, output);
                Audio.Write(output);
            }
        }

        public void Stop() {
            LoopStop = true;
        }
    }
}
