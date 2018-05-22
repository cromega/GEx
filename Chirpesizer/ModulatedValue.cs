using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chirpesizer {
    public class ModulatedValue : IValue {
        private double Value;
        private SineGenerator Osc;
        private double Height;

        public ModulatedValue(double initial, double frequency, double height) {
            Value = initial;
            Height = height;
            Osc = new SineGenerator(new StaticValue(frequency));
        }

        public double Get() {
            return Value + Osc.Next() * Height;
        }
    }
}
