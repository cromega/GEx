using System;
using System.Collections.Generic;
namespace Chirpesizer {
    public class LFOModulator : IModulator {
        public readonly Oscillator Oscillator;
        private readonly string Target;
        private double _Frequency;
        public double Frequency {
            get { return _Frequency; }
        }
        public readonly double Amplitude;
        private double[] SampleCache;
        private int LastPrepared;

        public LFOModulator(OscillatorType osc, double frequency, double amplitude, string target) {
            Oscillator = new Oscillator(osc);
            Oscillator.SetFrequency(frequency);
            Target = target;
            _Frequency = frequency;
            Amplitude = amplitude;
        }

        public void SetFrequency(double frequency) {
            _Frequency = frequency;
        }

        public string GetTarget() {
            return Target;
        }

        public double Get(double value, int time, bool isActive, int songTime) {
            return value + SampleCache[songTime - LastPrepared];
        }

        public void Prepare(int frames, int songTime) {
            SampleCache = new double[frames];
            for (int i = 0; i < frames; i++) { SampleCache[i] = Oscillator.Next() * Amplitude; }
            LastPrepared = songTime;
        }
    }
}
