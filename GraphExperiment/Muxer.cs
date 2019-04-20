using System;
using System.Collections.Generic;

namespace GraphExperiment {
    public class Muxer {
        private List<Sample> Samples;

        public Muxer() {
            Samples = new List<Sample>();
        }

        public void Add(Sample sample) {
            Samples.Add(sample);
        }

        public Sample Mux() {
            var output = new Sample(0);

            foreach (var sample in Samples) {
                output += sample;
            }

            //output.L = output.L / Samples.Count;
            //output.R = output.R / Samples.Count;

            Samples.Clear();
            return output;
        }
    }
}