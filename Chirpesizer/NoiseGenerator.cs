using System;

namespace Chirpesizer {
    public class NoiseGenerator : IGenerator {
        private Random random;

        public NoiseGenerator() {
            random = new Random();
        }

        public void Fill(double[] buffer, int frames) {
            double sample;
            for (int i=0; i<frames; i++) {
                sample = random.NextDouble() * 2 - 1;
                buffer[i*2] = sample;
                buffer[i*2+1] = sample;
            }
        }

        public double Next() {
            return random.NextDouble() * 2 - 1;
        }
    }
}