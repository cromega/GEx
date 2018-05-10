using System;

namespace chirpcore {
    public class Trigger {
        public double Frequency;
        private MTime TTL;

        public Trigger(double freq, int length) {
            Frequency = freq;
            TTL = MTime.FromMs(length);
        }

        public void Update(int frames) {
            TTL.Frames -= frames;
        }

        public bool IsActive() {
            return TTL.Frames <= 0;
        }

        public int ActiveFor() {
            return TTL.Milliseconds;
        }
    }
}