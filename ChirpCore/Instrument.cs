using System;
using System.Collections.Generic;
using System.Linq;

namespace chirpcore {
    public class Instrument {
        private IGenerator generator;
        private Envelope envelope;
        private List<Trigger> Triggers;

        public Instrument(IGenerator g, Envelope e = null) {
            generator = g;
            envelope = e;        
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
                var length = Math.Abs(trigger.TTL);
                if (envelope != null) { length += envelope.Release; }
                length = Math.Min(length, frames);
                generator.Fill(buffer, trigger.Frequency, length);
                if (envelope != null) { envelope.Modulate(buffer, trigger); }
                buffers.Add(buffer);
            });
            Triggers.RemoveAll(trigger => trigger.Ended);

            return buffers;
        }
    }
}