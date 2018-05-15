using System;

namespace chirpcore {
    public class Trigger {
        public double Frequency;
        public MTime TTL;
        public MTime Age;

        public Trigger(double freq, int length) {
            Frequency = freq;
            TTL = MTime.FromMs(length);
            Age = MTime.FromMs(0);
        }

        public void Update(int frames) {
            TTL.Frames -= frames;
            Age.Frames += frames;
        }

        public bool IsActive() {
            return TTL.Frames > 0;
        }

        public int ActiveFor() {
            return TTL.Milliseconds;
        }
    }
}