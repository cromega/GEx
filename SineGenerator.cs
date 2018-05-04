using System;

namespace chirpcore {
    public class SineGenerator : IGenerator {
        public static double[] SineTable = GenerateSineTable();
        const int LOOKUP_TABLE_LENGTH = 1000;

        private int phaseIndex = 0;

        public SineGenerator() {
            phaseIndex = 0;
        }

        public void Fill(short[] buffer, double frequency) {
            double increment = LOOKUP_TABLE_LENGTH * frequency / 44100;

            for (int i=0; i<buffer.Length / 2; i++) {
                phaseIndex = (int)Math.Round(phaseIndex + increment) % LOOKUP_TABLE_LENGTH;
                var sample = (short)Math.Round(SineTable[phaseIndex] * short.MaxValue);
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