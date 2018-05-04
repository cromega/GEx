using System;

namespace chirpcore {
    public static class Osc {
        public static double[] SineTable = GenerateSineTable();
        const int LOOKUP_TABLE_LENGTH = 1000;

        public static void Noise(short[] buffer) {
            var rnd = new Random();

            short sample;
            for (int i=0; i<buffer.Length / 2; i++) {
                sample = (short)rnd.Next(0, short.MaxValue);
                buffer[i*2] = sample;
                buffer[i*2+1] = sample;
            }
        }

        public static void Sine(short[] buffer, double freq) {
            double increment = LOOKUP_TABLE_LENGTH * freq / 44100;
            int phaseIndex = 0;
            for (int i=0; i<buffer.Length / 2; i++) {
                phaseIndex = (int)Math.Round(phaseIndex + increment) % LOOKUP_TABLE_LENGTH;
                var sample = (short)Math.Round(SineTable[phaseIndex] * 32767);
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