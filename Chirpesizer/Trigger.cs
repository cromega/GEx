using System;

namespace Chirpesizer {
    public class Trigger {
        public IGenerator Osc;
        public int TTL;
        public int Age;
        public bool Ended;
        public ModulatedValue Volume;

        public Trigger(IGenerator osc, ModulatedValue volume, int length) {
            Osc = osc;
            TTL = length;
            Age = 0;
            Volume = volume;
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