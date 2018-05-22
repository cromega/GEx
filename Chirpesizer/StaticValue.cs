using System;

namespace Chirpesizer {
    public class StaticValue : IValue {
        private readonly double Value;
        public StaticValue(double value) {
            Value = value;
        }

        public double Get() {
            return Value;
        }
    }
}
