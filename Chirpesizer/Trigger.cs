using System;

namespace Chirpesizer {
    public class Trigger {
        public IGenerator Osc;
        public int TTL;
        public int Age;
        public bool Ended;

        public Trigger(IGenerator osc, int length) {
            Osc = osc;
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