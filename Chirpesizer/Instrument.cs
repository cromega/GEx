using System;
using System.Collections.Generic;

namespace Chirpesizer {
    public class Instrument {
        private Oscillator Osc;
        private Envelope Envelope;
        private double Volume;
        private List<Trigger> Triggers;

        public Instrument(Oscillator osc, double v, Envelope e) {
            Osc = osc;
            Volume = v;
            Envelope = e;        
            Triggers = new List<Trigger>();
        }

        public void Activate(double frequency, int length) {
            Triggers.Add(new Trigger(NewOscillator(frequency), new ModulatedValue(Volume * short.MaxValue, 10, 10000), length));
        }

        public bool IsActive() {
            return (Triggers.Count > 0);
        }

        public List<double[]> RenderAll(int frames) {
            var buffers = new List<double[]>();
            Triggers.ForEach(trigger => {
                var buffer = new double[frames * 2];
                var length = Math.Abs(trigger.TTL) + Envelope.Release;
                length = Math.Min(length, frames);
                trigger.Osc.Fill(buffer, length);
                new Scaler().Scale(buffer, trigger.Volume);
                Envelope.Modulate(buffer, trigger);
                buffers.Add(buffer);
            });
            Triggers.RemoveAll(trigger => trigger.Ended);

            return buffers;
        }

        private IGenerator NewOscillator(double frequency) {
            switch (Osc) {
                case Oscillator.Noise: return new NoiseGenerator();
                case Oscillator.Sine: return new SineGenerator(frequency);
                case Oscillator.Square: return new SquareGenerator(frequency);
            }

            throw new Exception("don't know how to create oscillator");
        }
    }
}