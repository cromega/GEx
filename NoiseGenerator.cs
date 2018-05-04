using System;

namespace chirpcore {
    public class NoiseGenerator : IGenerator {
        private Random random;

        public NoiseGenerator() {
            random = new Random();
        }

        public void Fill(short[] buffer, double frequency = 0.0d) {
            short sample;
            for (int i=0; i<buffer.Length / 2; i++) {
                sample = (short)random.Next(0, short.MaxValue);
                buffer[i*2] = sample;
                buffer[i*2+1] = sample;
            }
        }
    }
}