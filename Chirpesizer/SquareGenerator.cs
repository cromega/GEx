using System;

namespace Chirpesizer {
    public class SquareGenerator : IGenerator {
        private double PhaseIndex;
        private double Frequency;
        private double Increment;

        public SquareGenerator(double frequency) {
            Frequency = frequency;
            PhaseIndex = 0;
            Increment = SineGenerator.LOOKUP_TABLE_LENGTH * frequency / 44100;
        }

        public void Fill(double[] buffer, int frames) {
            double sample;
            for (int i=0; i<frames; i++) {
                PhaseIndex += Increment;
                sample = SineGenerator.SineTable[(int)Math.Round(PhaseIndex) % SineGenerator.SineTable.Length] < 0 ? -1 : 1;
                buffer[i*2] = sample;
                buffer[i*2+1] = sample;
            }
        }

        public double Next() {
            PhaseIndex += Increment;
            return SineGenerator.SineTable[(int)Math.Round(PhaseIndex) % SineGenerator.SineTable.Length] < 0 ? -1 : 1;
        }
    }
}