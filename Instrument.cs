using System;

namespace chirpcore {
    public class Instrument {
        private IGenerator generator;
        private Envelope envelope;
        private bool IsActive;
        private double currentFrequency;

        public Instrument(IGenerator g, Envelope e = null) {
            generator = g;
            envelope = e;        
            IsActive = false;
        }

        public void Activate(double frequency) {
            if (IsActive) { throw new Exception("instrument already active"); }
            IsActive = true;
            currentFrequency = frequency;
        }

        public void Deactivate() {
            if (!IsActive) { throw new Exception("instrument is not active"); }
            IsActive = false;
        }

        public void Render(short[] buffer) {
            generator.Fill(buffer, currentFrequency);
        }
    }
}