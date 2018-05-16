using System;

namespace chirpcore {
    public class SquareGenerator : IGenerator {
        private int PhaseIndex;

        public SquareGenerator() {
            PhaseIndex = 0;
        }

        public void Fill(double[] buffer, double frequency) {
            double increment = SineGenerator.LOOKUP_TABLE_LENGTH * frequency / 44100;

            double sample;
            for (int i=0; i<buffer.Length / 2; i++) {
                PhaseIndex = (int)Math.Round(PhaseIndex + increment) % SineGenerator.LOOKUP_TABLE_LENGTH;
                sample = SineGenerator.SineTable[PhaseIndex] < 0 ? -1 : 1;
                buffer[i*2] = sample;
                buffer[i*2+1] = sample;
            }
        }
        public void Fill(double[] buffer, double frequency, int frames) {
            double increment = SineGenerator.LOOKUP_TABLE_LENGTH * frequency / 44100;

            double sample;
            for (int i=0; i<frames; i++) {
                PhaseIndex = (int)Math.Round(PhaseIndex + increment) % SineGenerator.LOOKUP_TABLE_LENGTH;
                //sample = SineGenerator.SineTable[PhaseIndex] < 0 ? -1 : 1;
                sample = SineGenerator.SineTable[PhaseIndex] < 0 ? -20000 : 20000;
                buffer[i*2] = sample;
                buffer[i*2+1] = sample;
            }
        }
    }
}