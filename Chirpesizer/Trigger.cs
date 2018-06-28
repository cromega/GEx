using System;

namespace Chirpesizer {
    public class Trigger {
        public readonly Oscillator Osc;
        public readonly PatchableValue Frequency;
        public readonly PatchableValue Volume;

        private int _TTL;
        public int TTL { get { return _TTL; } }
        private int _Age;
        public int Age { get { return _Age; } }

        private bool _IsActive;
        public bool IsActive {
            get { return _IsActive; }
        }

        private bool _Finished;
        public bool Finished {
            get { return _Finished; }
        }

        public Trigger(Oscillator osc, PatchableValue frequency, int length, PatchableValue volume) {
            Osc = osc;
            Frequency = frequency;
            _TTL = length;
            _Age = 0;
            _IsActive = true;
            Volume = volume;
            _Finished = false;
        }

        public void Tick() {
            if (_TTL > 0) { _TTL -= 1; }
            if (IsActive && _TTL == 0) {
                _IsActive = false;
                _Age = -1;
            }
            _Age += 1;
        }

        public void Release() {
            _IsActive = false;
            _Age = 0;
        }

        public double[] Render(int frames) {
            var buffer = new double[frames * 2];
            double sample;
            for (int i = 0; i < frames; i++) {
                Osc.SetFrequency(Frequency.Get(Age, IsActive));
                sample = Osc.Next(Age);
                sample *= Volume.Get(Age, IsActive);
                sample *= short.MaxValue;
                buffer[i * 2] = sample;
                buffer[i * 2 + 1] = sample;
                if (!_IsActive && Age >= Envelope.MAX_TIME) { _Finished = true; }
                Tick();
            }
            return buffer;
        }
    }
}