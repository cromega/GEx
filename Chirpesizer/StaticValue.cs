using System;

namespace Chirpesizer {
    public class StaticValue : IValue {
        private readonly double Value;
        public StaticValue(double value) {
            Value = value;
        }

        public double Get(int _time, bool _isActive) {
            return Value;
        }
    }
}
