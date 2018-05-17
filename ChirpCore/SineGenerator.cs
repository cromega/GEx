using System;

namespace chirpcore {
    public class SineGenerator : IGenerator {
        public static double[] SineTable = GenerateSineTable();
        public const int LOOKUP_TABLE_LENGTH = 1000;
        private double phaseIndex = 0;

        public SineGenerator() {
            phaseIndex = 0;
        }

        public void Fill(double[] buffer, double frequency, int frames) {
            double increment = LOOKUP_TABLE_LENGTH * frequency / 44100;

            double sample;
            for (int i=0; i<frames; i++) {
                phaseIndex += increment;
                sample = SineTable[(int)Math.Round(phaseIndex) % SineTable.Length] * 20000;
                buffer[i*2] = sample;
                buffer[i*2+1] = sample;
            }
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