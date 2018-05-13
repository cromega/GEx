using System;
using System.Collections.Generic;

namespace chirpcore {
    public class Mixer {
        public Mixer() {

        }

        public void Mix(double[] output, List<double[]> buffers) {
            for (int i=0; i<output.Length; i++) {
                output[i] = buffers[0][i];
            }
            return;
            
            for (int i=0; i<output.Length; i++) {
                double value = buffers[0][i];
                for (int j=1; j<buffers.Count; j++) {
                    value += buffers[j][i];
                }

                output[i] = value / buffers.Count;
            }
        }
    }
}