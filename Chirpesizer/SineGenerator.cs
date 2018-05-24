using System;

namespace Chirpesizer {
    public class SineGenerator : IGenerator {
        public static double[] SineTable = GenerateSineTable();
        public const int LOOKUP_TABLE_LENGTH = 1000;
        private double PhaseIndex = 0;
        private double Increment;
        private IValue Frequency;

        public SineGenerator(IValue frequency) {
            PhaseIndex = new Random().Next(0, LOOKUP_TABLE_LENGTH);
            Frequency = frequency;
            Increment = LOOKUP_TABLE_LENGTH * Frequency.Get() / 44100;
        }

        public void Fill(double[] buffer, int frames) {
            double sample;
            for (int i=0; i<frames; i++) {
                PhaseIndex += Increment;
                sample = SineTable[(int)Math.Round(PhaseIndex) % SineTable.Length];
                buffer[i*2] = sample;
                buffer[i*2+1] = sample;
                Increment = LOOKUP_TABLE_LENGTH * Frequency.Get() / 44100;
            }
        }

        public double Next() {
            PhaseIndex += Increment;
            return SineTable[(int)Math.Round(PhaseIndex) % SineTable.Length];
        }

        public static double[] GenerateSineTable() {
            var table = new double[LOOKUP_TABLE_LENGTH];
            for (int i=0; i<table.Length; i++) {
                table[i] = Math.Sin((Math.PI * 2 * i / LOOKUP_TABLE_LENGTH));
            }
            return table;
        }
    }
}