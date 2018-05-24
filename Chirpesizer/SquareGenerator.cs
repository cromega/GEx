using System;

namespace Chirpesizer {
    public class SquareGenerator : IGenerator {
        private double PhaseIndex;
        private double Increment;
        private IValue Frequency;

        public SquareGenerator(IValue frequency) {
            Frequency = frequency;
            PhaseIndex = new Random().Next(0, SineGenerator.LOOKUP_TABLE_LENGTH);
        }

        public void Fill(double[] buffer, int frames) {
            double sample;
            for (int i=0; i<frames; i++) {
                Increment = SineGenerator.LOOKUP_TABLE_LENGTH * Frequency.Get() / 44100;
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