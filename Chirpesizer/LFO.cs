using System;
using System.Collections.Generic;
namespace Chirpesizer {
    public class LFO {
        public readonly Oscillator Oscillator;
        private readonly string Target;
        public double Frequency {
            get { return Oscillator.Frequency; }
        }
        public readonly double Amplitude;
        private int _Time;

        public LFO(OscillatorType osc, double frequency, double amplitude, string target) {
            Oscillator = new Oscillator(osc);
            Oscillator.SetFrequency(frequency);
            Target = target;
            Amplitude = amplitude;
            _Time = 0;
        }

        public void Tick() {
            _Time++;
        }

        public void SetFrequency(double frequency) {
            Oscillator.SetFrequency(frequency);
        }

        public string GetTarget() {
            return Target;
        }

        public double Get() {
            return Oscillator.Next(_Time) * Amplitude;
        }
    }
}
