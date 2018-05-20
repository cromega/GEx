using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chirpesizer {
    public class ModulatedValue {
        private double Value;
        private SineGenerator Osc;
        private int Height;

        public ModulatedValue(double initial, double frequency, int height) {
            Value = initial;
            Height = height;
            Osc = new SineGenerator(frequency);
        }

        public double Get() {
            return Value + Osc.Next() * Height;
        }
    }
}
