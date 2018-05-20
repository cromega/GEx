using System;
using System.Collections.Generic;
using System.Linq;

namespace chirpcore {
    public class Instrument {
        private IGenerator Generator;
        private Envelope Envelope;
        private double Volume;
        private List<Trigger> Triggers;

        public Instrument(IGenerator g, double v, Envelope e = null) {
            Generator = g;
            Volume = v;
            Envelope = e;        
            Triggers = new List<Trigger>();
        }

        public void Activate(double frequency, int ms) {
            Triggers.Add(new Trigger(frequency, ms));
        }

        public bool IsActive() {
            return Triggers.Any(trigger => trigger.IsActive());
        }

        public List<double[]> RenderAll(int frames) {
            var buffers = new List<double[]>();
            Triggers.ForEach(trigger => {
                var buffer = new double[frames * 2];
                var length = Math.Abs(trigger.TTL) + Envelope.Release;
                length = Math.Min(length, frames);
                Generator.Fill(buffer, trigger.Frequency, length);
                var gain = Volume * short.MaxValue;
                new Scaler().Scale(buffer, gain);
                Envelope.Modulate(buffer, trigger);
                buffers.Add(buffer);
            });
            Triggers.RemoveAll(trigger => trigger.Ended);

            return buffers;
        }
    }
}