using System;

namespace chirpcore {
    public class Trigger {
        public double Frequency;
        public int TTL;
        public int Age;

        public Trigger(double freq, int length) {
            Frequency = freq;
            TTL = length;
            Age = 0;
        }

        public void Update(int frames) {
            TTL -= frames;
            Age += frames;
        }

        public bool IsActive() {
            return TTL > 0;
        }
    }
}