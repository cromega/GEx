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

        public void Render(short[] buffer) {
            var mixedBuffer = new double[buffer.Length];
            var buffers = new List<double[]>();
            Triggers.ForEach(trigger => {
                var buf = new double[buffer.Length];
                generator.Fill(buf, trigger.Frequency);
                trigger.Update(buffer.Length / 2);
                buffers.Add(buf);
            });
            Triggers.RemoveAll(trigger => !trigger.IsActive());
            if (buffers.Count() == 0) { return; }

            var bufs = buffers.ToArray();
            Logger.Log("number of buffers: {0}", bufs.Length);
            for (int i=0; i<buffer.Length; i++) {
                double value = bufs[0][i];
                for (int j=1; j<bufs.Length; j++) {
                    value += bufs[j][i];
                }
                mixedBuffer[i] = value / bufs.Length;
            }

            new Normalizer().Normalize(mixedBuffer);
            for (int i=0; i<mixedBuffer.Length; i++) {
                buffer[i] = (short)mixedBuffer[i];
            }
        }
    }
}