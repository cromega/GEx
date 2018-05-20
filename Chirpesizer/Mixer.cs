using System;
using System.Collections.Generic;

namespace chirpcore {
    public class Mixer {
        public void Mix(double[] output, List<double[]> buffers) {
            if (buffers.Count == 0) { return; }

            for (int i=0; i<output.Length; i++) {
                double value = buffers[0][i];
                for (int j=1; j<buffers.Count; j++) {
                    value += buffers[j][i];
                }

                output[i] = Math.Min(value, short.MaxValue);
            }
        }
    }
}