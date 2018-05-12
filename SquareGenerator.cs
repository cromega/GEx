using System;

namespace chirpcore {
    public class SquareGenerator : IGenerator {
        private int PhaseIndex;

        public SquareGenerator() {
            PhaseIndex = 0;
        }

        public void Fill(short[] buffer, double frequency) {
            double increment = SineGenerator.LOOKUP_TABLE_LENGTH * frequency / 44100;

            short sample;
            for (int i=0; i<buffer.Length / 2; i++) {
                PhaseIndex = (int)Math.Round(PhaseIndex + increment) % SineGenerator.LOOKUP_TABLE_LENGTH;
                sample = SineGenerator.SineTable[PhaseIndex] < 0 ? (short)short.MinValue : (short)short.MaxValue;
                buffer[i*2] = sample;
                buffer[i*2+1] = sample;
            }
        }
    }
}