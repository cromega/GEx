using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphExperiment {
    public class Instrument {
        private Generator Generator;
        private SoundSystem Output;
        private Wire Connection;
        public volatile bool IsActive;

        public Instrument(SoundSystem output) {
            Output = output;
            Connection = new Wire(2205);
            IsActive = false;
        }

        public void First(Generator node) {
            Generator = node;
        }

        public void Last(AudioNode node) {
            node.Connection = Connection;
        }

        public void Run() {
            var buffer = new short[4410];
            Task.Run(() => {
                for (; ; ) {
                    for (int i = 0; i < 4410; i += 2) {
                        var sample = Connection.Take().Sample * 5000;
                        buffer[i] = (short)sample.L;
                        buffer[i + 1] = (short)sample.R;
                    }
                    Output.Write(buffer);
                }
            });
        }

        public string Trigger(double frequency) {
            return Generator.Start(frequency);
        }

        public void Release(string triggerId) {
            Generator.Remove(triggerId);
        }
    }
}
