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
                var buf = new double[frames * 2];
                var length = trigger.TTL.Frames;
                if (envelope != null) { length += envelope.Release.Frames; }
                length = Math.Min(length, buf.Length / 2);
                generator.Fill(buf, trigger.Frequency, length);
                envelope.Modulate(buf, trigger);
                trigger.Update(frames);
                buffers.Add(buf);
            });
            Triggers.RemoveAll(trigger => !trigger.IsActive());

            return buffers;
        }
    }
}