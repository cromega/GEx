using System;

namespace Chirpesizer {
    public class Oscillator {
        private double PhaseIndex;
        private double Increment;
        Func<double> Generator;
        private Random Rnd;
        public readonly OscillatorType OscillatorType;

        public Oscillator(OscillatorType type) {
            OscillatorType = type;
            switch (type) {
                case OscillatorType.Noise: Generator = Noise; break;
                case OscillatorType.Sine: Generator = Sine; break;
                case OscillatorType.Square: Generator = Square; break;
                case OscillatorType.Sawtooth: Generator = Sawtooth; break;
                case OscillatorType.Triangle: Generator = Triangle; break;
            }
            Rnd = new Random();
            PhaseIndex = 0;
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

        private double Sawtooth() {
            return SawtoothTable[(int)Math.Round(PhaseIndex) % SawtoothTable.Length];
        }

        private double Triangle() {
            return TriangleTable[(int)Math.Round(PhaseIndex) % TriangleTable.Length];
        }

        public const int LOOKUP_TABLE_LENGTH = 1000;
        private static double[] SineTable = GenerateSineTable();
        private static double[] GenerateSineTable() {
            var table = new double[LOOKUP_TABLE_LENGTH];
            for (int i=0; i<table.Length; i++) {
                table[i] = Math.Sin((Math.PI * 2 * i / LOOKUP_TABLE_LENGTH));
            }
            return table;
        }

        private static double[] SawtoothTable = GenerateTriangleTable(0.95);
        private static double[] TriangleTable = GenerateTriangleTable(0.5);
        private static double[] GenerateTriangleTable(double pivot) {
            var table = new double[LOOKUP_TABLE_LENGTH];
            var peak = LOOKUP_TABLE_LENGTH * pivot;
            for (int i=0; i<peak; i++) {
                table[i] = i / peak * 2 - 1;
            }
            var remaining = table.Length - peak;
            for (int i=0; i<remaining; i++) {
                table[(int)peak + i] = 1 - i / remaining * 2;
            }

            return table;
        }
    }
}
