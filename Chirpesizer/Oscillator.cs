using System;

namespace Chirpesizer {
    public class Oscillator {
        private double PhaseIndex;
        private double Increment;
        Func<double> Generator;
        private Random Rnd;

        public Oscillator(OscillatorType type) {
            switch (type) {
                case OscillatorType.Noise: Generator = Noise; break;
                case OscillatorType.Sine: Generator = Sine; break;
                case OscillatorType.Square: Generator = Square; break;
            }
            Rnd = new Random();
            PhaseIndex = Rnd.Next(0, LOOKUP_TABLE_LENGTH);
        }

        public double Next(double frequency) {
            Increment = LOOKUP_TABLE_LENGTH * frequency / 44100;
            PhaseIndex += Increment;
            return Generator();
        }

        private double Sine() {
            return SineTable[(int)Math.Round(PhaseIndex) % SineTable.Length];
        }

        private double Square() {
            return Sine() < 0 ? -1 : 1;
        }

        private double Noise() {
            return Rnd.NextDouble() * 2 - 1;
        }

        public const int LOOKUP_TABLE_LENGTH = 1000;
        public static double[] SineTable = GenerateSineTable();
        public static double[] GenerateSineTable() {
            var table = new double[LOOKUP_TABLE_LENGTH];
            for (int i=0; i<table.Length; i++) {
                table[i] = Math.Sin((Math.PI * 2 * i / LOOKUP_TABLE_LENGTH));
            }
            return table;
        }
    }
}
