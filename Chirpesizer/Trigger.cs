using System;

namespace Chirpesizer {
    public class Trigger {
        public readonly Oscillator Osc;
        public readonly IValue Frequency;

        private int _TTL;
        public int TTL { get { return _TTL; } }
        private int _Age;
        public int Age { get { return _Age; } }

        private bool _IsActive;
        public bool IsActive {
            get { return _IsActive; }
        }

        public Trigger(Oscillator osc, IValue frequency, int length) {
            Osc = osc;
            Frequency = frequency;
            _TTL = length;
            _Age = 0;
            _IsActive = true;
        }

        public void Tick() {
            if (_TTL > 0) { _TTL -= 1; }
            if (IsActive && _TTL == 0) {
                _IsActive = false;
                _Age = -1;
            }
            _Age += 1;
        }

        public void End() {
            _IsActive = false;
            _Age = 0;
        }
    }
}