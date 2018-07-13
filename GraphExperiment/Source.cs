using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphExperiment {
    public class Source {
        private BlockingCollection<double> Output;
        private bool Released;
        public double Frequency;

        public Source(BlockingCollection<double> output) {
            Output = output;
            Released = false;
        }

        public void Trigger() {
            var t = 0;
            var task = Task.Run(() => {
                for (; !Released; t++) {
                    Output.Add(Math.Sin(Frequency * Math.PI * 2 * t / 44100));
                }
            });

        }
    }
}
