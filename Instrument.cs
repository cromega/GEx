using System;
using System.Collections.Generic;

namespace chirpcore {
    public class Instrument {
        private IGenerator generator;
        private Envelope envelope;
        private bool IsActive;
        private TriggerMode currentMode;
        private int time;
        private MTime TTL;
        private Trigger trigger;

        public Instrument(IGenerator g, Envelope e = null) {
            generator = g;
            envelope = e;        
            IsActive = false;
        }

        public void Activate(double frequency, int ms) {
            trigger = new Trigger(frequency, ms);
        }

        public void Deactivate() {
            if (!IsActive) { throw new Exception("instrument is not active"); }
            IsActive = false;
        }

        public void Render(short[] buffer) {
            generator.Fill(buffer, trigger.Frequency);
            // envelope.Modulate(buffer, trigger);
            trigger.Update(buffer.Length / 2);
        }

        private void Update(MTime framesPassed) {
            TTL.Frames -= framesPassed.Frames;
            if (TTL.Frames <= 0) {
                Deactivate();
            }
        }
    }
}