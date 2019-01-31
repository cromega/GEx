using System;
using System.Collections.Generic;

namespace GraphExperiment {
    public class Muxer {
        private List<Sample> Samples;

        public Muxer() {
            Samples = new List<Sample>();
        }

        public void Add(Sample packet) {
            Samples.Add(packet);
        }

        public Sample Mux() {
            var sample = new Sample(0);

            for (int i=0; i<Samples.Count; i++) {
                sample += Samples[i];
            }
            Samples.Clear();

            sample.L = sample.L / Samples.Count;
            sample.R = sample.R / Samples.Count;

            return sample;
        }
    }
}