using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chirpesizer {
    public class LFOModulator : IModulator {
        public readonly Oscillator Oscillator;
        private readonly string Target;
        private double Frequency;

        public LFOModulator(OscillatorType osc, double frequency, string target) {
            Oscillator = new Oscillator(osc);
            Target = target;
            Frequency = frequency;
        }

        public void SetFrequency(double frequency) {
            Frequency = frequency;
        }

        public string GetTarget() {
            return Target;
        }

        public double Get(int time, bool isActive) {
            return Oscillator.Next(Frequency);
        }
    }
}
