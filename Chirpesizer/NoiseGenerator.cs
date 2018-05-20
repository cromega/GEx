using System;

namespace Chirpesizer {
    public class NoiseGenerator : IGenerator {
        private Random random;

        public NoiseGenerator() {
            random = new Random();
        }

        public void Fill(double[] buffer, double frequency = 0.0d) {
            double sample;
            for (int i=0; i<buffer.Length / 2; i++) {
                sample = (short)random.Next(0, short.MaxValue);
                sample = random.NextDouble();
                buffer[i*2] = sample;
                buffer[i*2+1] = sample;
            }
        }

        public void Fill(double[] buffer, double frequency, int frames) {
            double sample;
            for (int i=0; i<frames; i++) {
                sample = random.NextDouble() * 2 - 1;
                buffer[i*2] = sample;
                buffer[i*2+1] = sample;
            }
        }
    }
}