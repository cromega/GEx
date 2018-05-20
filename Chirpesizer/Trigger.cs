using System;

namespace Chirpesizer {
    public class Trigger {
        public double Frequency;
        public int TTL;
        public int Age;
        public bool Ended;

        public Trigger(double freq, int length) {
            Frequency = freq;
            TTL = length;
            Age = 0;
            Ended = false;
        }

        public void Update(int frames) {
            TTL -= frames;
            Age += frames;
        }

        public bool IsActive() {
            return TTL > 0;
        }

        public void End()
        {
            Ended = true;
        }
    }
}