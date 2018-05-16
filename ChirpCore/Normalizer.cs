using System;

namespace chirpcore {
    public class Normalizer {
        public void Normalize(double[] source, short[] output) {
            for (int i=0; i<source.Length; i++) {
                output[i] = (short)source[i];
            }
            return;

            var minmax = source[0];

            for (int i=0; i<source.Length; i++) {
                if (Math.Abs(source[i]) > minmax) { minmax = source[i]; }
            }

            var ratio = short.MaxValue / minmax;
            // Logger.Log("normalized buffer to {0}", ratio);
            for (int i=0; i<source.Length; i++) {
                output[i] = (short)(source[i] * ratio);
            }
        }
    }
}