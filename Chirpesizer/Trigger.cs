using System;

namespace Chirpesizer {
    public class Trigger {
        public Oscillator Osc;
        public IValue Frequency;
        public int TTL;
        public int Age;
        public bool Ended;

        public Trigger(Oscillator osc, IValue frequency, int length) {
            Osc = osc;
            Frequency = frequency;
            TTL = length;
            Age = 0;
            Ended = false;
        }

        public void Tick() {
            TTL -= 1;
            Age += 1;
        }

        public bool IsActive() {
            return TTL > 0;
        }
    }
}