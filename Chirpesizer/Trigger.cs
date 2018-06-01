using System;

namespace Chirpesizer {
    public class Trigger {
        public Oscillator Osc;
        public IValue Frequency;
        public int TTL;
        public int Age;

        private bool _IsActive;
        public bool IsActive {
            get { return _IsActive; }
        }

        public Trigger(Oscillator osc, IValue frequency, int length) {
            Osc = osc;
            Frequency = frequency;
            TTL = length;
            Age = 0;
            _IsActive = true;
        }

        public void Tick() {
            if (TTL > 0) { TTL -= 1; }
            if (IsActive && TTL == 0) {
                _IsActive = false;
                Age = -1;
            }
            Age += 1;
        }

        public void End() {
            _IsActive = false;
            Age = 0;
        }
    }
}