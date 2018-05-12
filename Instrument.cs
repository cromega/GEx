using System;
using System.Collections.Generic;
using System.Linq;

namespace chirpcore {
    public class Instrument {
        private IGenerator generator;
        private Envelope envelope;
        private TriggerMode currentMode;
        private int time;
        private MTime TTL;
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
            var buffers = new List<short[]>();
            Triggers.ForEach(trigger => {
                var buf = new short[buffer.Length];
                generator.Fill(buf, trigger.Frequency);
                trigger.Update(buffer.Length / 2);
                buffers.Add(buf);
            });
            Triggers.RemoveAll(trigger => !trigger.IsActive());
            if (buffers.Count() == 0) { return; }

            var bufs = buffers.ToArray();
            for (int i=0; i<buffer.Length; i++) {
                int value = bufs[0][i];
                for (int j=1; j<bufs.Length; j++) {
                    if (value + bufs[j][i] > short.MaxValue) {
                        value = short.MaxValue;
                        break;
                    }

                    if (value + bufs[j][i] < short.MinValue) {
                        value = short.MinValue;
                        break;
                    }
                    value += bufs[j][i];
                }
                buffer[i] = (short)value;
            }
        }

        private void Update(MTime framesPassed) {
        }
    }
}