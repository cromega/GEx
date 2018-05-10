using System;
using System.Collections.Generic;

namespace chirpcore {
    public class Instrument {
        private IGenerator generator;
        private Envelope envelope;
        private TriggerMode currentMode;
        private int time;
        private MTime TTL;
        private Trigger trigger;

        public Instrument(IGenerator g, Envelope e = null) {
            generator = g;
            envelope = e;        
        }

        public void Activate(double frequency, int ms) {
            trigger = new Trigger(frequency, ms);
        }

        public bool IsActive() {
            if (trigger == null) { return false; }
            return trigger.IsActive();
        }

        public void Render(short[] buffer) {
            generator.Fill(buffer, trigger.Frequency);
            // envelope.Modulate(buffer, trigger);
            trigger.Update(buffer.Length / 2);
        }

        private void Update(MTime framesPassed) {
        }
    }
}