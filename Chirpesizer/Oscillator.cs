using System;
using System.IO;

namespace Chirpesizer {
    public class Oscillator {
        private double Increment;
        Func<int, double> Generator;
        private Random Rnd;
        public readonly OscillatorType OscillatorType;
        private double Phase;
        private double _Frequency;
        private int lastTime;

        public Oscillator(OscillatorType type) {
            OscillatorType = type;
            switch (type) {
                case OscillatorType.Noise: Generator = Noise; break;
                case OscillatorType.Sine: Generator = Sine; break;
                case OscillatorType.Square: Generator = Square; break;
                case OscillatorType.Sawtooth: Generator = Sawtooth; break;
                case OscillatorType.Triangle: Generator = Triangle; break;
            }
            Phase = 0;
            Rnd = new Random();
        }

        public double Next() {
            //if (time != lastTime) {
            //    Phase += Increment;
            //    lastTime = time;
            //}
            Phase += Increment;
            return Generator(1);
        }

        public void SetFrequency(double frequency) {
            Increment = LOOKUP_TABLE_LENGTH * frequency / 44100;
            _Frequency = frequency;
        }

        private double Sine(int time) {
            return SineTable[(int)Math.Round(Phase) % SineTable.Length];
        }

        private double Square(int time) {
            return Sine(time) < 0 ? -1 : 1;
        }

        private double Noise(int time) {
            return Rnd.NextDouble() * 2 - 1;
        }

        private double Sawtooth(int time) {
            return SawtoothTable[(int)Math.Round(Phase) % SawtoothTable.Length];
        }

        private double Triangle(int time) {
            return TriangleTable[(int)Math.Round(Phase) % TriangleTable.Length];
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

        private static double[] SawtoothTable = GenerateTriangleTable(0.98);
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
