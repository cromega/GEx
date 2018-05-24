using System;

namespace Chirpesizer {
    public class Converter {
        public void Convert(double[] source, short[] output) {
            for (int i=0; i<source.Length; i++) {
                output[i] = (short)source[i];
            }
        }
    }
}