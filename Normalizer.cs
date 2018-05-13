using System;

namespace chirpcore {
    public class Normalizer {
        public void Normalize(double[] buffer) {
            var minmax = buffer[0];

            for (int i=0; i<buffer.Length; i++) {
                if (Math.Abs(buffer[i]) > minmax) { minmax = buffer[i]; }
            }

            var ratio = short.MaxValue / minmax;
            Logger.Log("normalized buffer to {0}", ratio);
            for (int i=0; i<buffer.Length; i++) {
                buffer[i] *= ratio;
            }
        }
    }
}