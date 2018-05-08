using System;

namespace chirpcore {
    public class Instrument {
        private IGenerator generator;
        private Envelope envelope;
        private bool IsActive;
        private double currentFrequency;
        private TriggerMode currentMode;
        private int time;
        private MTime TTL;

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

        public void Activate(double frequency, int ms) {
            if (IsActive) { throw new Exception("instrument already active"); }
            IsActive = true;
            currentFrequency = frequency;
            TTL = MTime.FromMs(ms);
        }

        public void Deactivate() {
            if (!IsActive) { throw new Exception("instrument is not active"); }
            IsActive = false;
        }

        public void Render(short[] buffer) {
            generator.Fill(buffer, currentFrequency);
            envelope.Modulate(buffer, time, StopIn: TTL);
            time += buffer.Length;
            Update(new MTime(buffer.Length));;
        }

        private void Update(MTime framesPassed) {
            TTL.Frames -= framesPassed.Frames;
            if (TTL.Frames <= 0) {
                Deactivate();
            }
        }
    }
}