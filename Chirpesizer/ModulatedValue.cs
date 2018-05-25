using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chirpesizer {
    public class ModulatedValue : IValue {
        private double Value;
        private Oscillator Osc;
        private double Height;
        public double Frequency;

        public ModulatedValue(double initial, double frequency, double height) {
            Value = initial;
            Height = height;
            Frequency = frequency;
            Osc = new Oscillator(OscillatorType.Sine);
        }

        public double Get() {
            return Value + Osc.Next(Frequency) * Height;
        }
    }
}
