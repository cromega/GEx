using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chirpesizer {
    public class ModulatedValue {
        private double Value;
        private SineGenerator Osc;

        public ModulatedValue(double initial, double frequency) {
            Value = initial;
            Osc = new SineGenerator(frequency);
        }

        public double Get() {
            return Value + Osc.Next() * 10000;
        }
    }
}
