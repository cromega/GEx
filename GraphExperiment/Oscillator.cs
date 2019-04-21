using System;
using System.IO;

namespace GraphExperiment {
    public enum SignalType : int {
        Sine,
        Square,
        Noise,
        Sawtooth,
        Triangle,
    }

    public class Oscillator {
        private double Increment;
        private Random Rnd;
        private double Phase;

        public Oscillator() {
            Phase = 0;
            Rnd = new Random();
        }

        public double Next(SignalType signalType) {
            Phase += Increment;
            switch (signalType) {
                case SignalType.Noise: return Noise();
                case SignalType.Sine: return Sine();
                case SignalType.Square: return Square();
                case SignalType.Sawtooth: return Sawtooth();
                case SignalType.Triangle: return Triangle();
            }

            return 0d;
        }

        public void SetFrequency(double frequency) {
            Increment = LOOKUP_TABLE_LENGTH * frequency / 44100;
        }

        private double Sine() {
            return SineTable[(long)Math.Round(Phase) % SineTable.Length];
        }

        private double Square() {
            return Sine() < 0 ? -1 : 1;
        }

        private double Noise() {
            return Rnd.NextDouble() * 2 - 1;
        }

        private double Sawtooth() {
            return SawtoothTable[(long)Math.Round(Phase) % SawtoothTable.Length];
        }

        private double Triangle() {
            return TriangleTable[(long)Math.Round(Phase) % TriangleTable.Length];
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
