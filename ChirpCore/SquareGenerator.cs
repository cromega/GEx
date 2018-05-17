using System;

namespace chirpcore {
    public class SquareGenerator : IGenerator {
        private double PhaseIndex;

        public SquareGenerator() {
            PhaseIndex = 0;
        }

        public void Fill(double[] buffer, double frequency, int frames) {
            double increment = SineGenerator.LOOKUP_TABLE_LENGTH * frequency / 44100;

            double sample;
            for (int i=0; i<frames; i++) {
                PhaseIndex += increment;
                //sample = SineGenerator.SineTable[PhaseIndex] < 0 ? -1 : 1;
                sample = SineGenerator.SineTable[(int)Math.Round(PhaseIndex) % SineGenerator.SineTable.Length] < 0 ? -20000 : 20000;
                buffer[i*2] = sample;
                buffer[i*2+1] = sample;
            }
        }
    }
}