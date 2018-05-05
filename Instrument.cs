using System;

namespace chirpcore {
    public class Instrument {
        private IGenerator generator;
        private Envelope envelope;
        private bool IsActive;
        private double currentFrequency;
        private TriggerMode currentMode;
        private int time;

        public Instrument(IGenerator g, Envelope e = null) {
            generator = g;
            envelope = e;        
            IsActive = false;
        }

        public void Activate(double frequency, TriggerMode mode) {
            if (IsActive) { throw new Exception("instrument already active"); }
            IsActive = true;
            currentFrequency = frequency;
            currentMode = mode;
            time = 0;
        }

        public void Deactivate() {
            if (!IsActive) { throw new Exception("instrument is not active"); }
            IsActive = false;
        }

        public void Render(short[] buffer) {
            generator.Fill(buffer, currentFrequency);
            envelope.Modulate(buffer, time);
            time += buffer.Length;
        }
    }
}