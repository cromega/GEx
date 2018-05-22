using System;
using System.Collections.Generic;

namespace Chirpesizer {
    public class Instrument {
        private OscillatorType Osc;
        private Envelope Envelope;
        private double Volume;
        private List<Trigger> Triggers;

        public Instrument(OscillatorType osc, double v, Envelope e) {
            Osc = osc;
            Volume = v;
            Envelope = e;        
            Triggers = new List<Trigger>();
        }

        public void Activate(double frequency, int length) {
            var osc = Oscillator.Create(Osc, new ModulatedValue(frequency, 10, 100));
            var trig = new Trigger(osc, new ModulatedValue(0.5 * short.MaxValue, 5, 10000), length);
            Triggers.Add(trig);
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
    }
}