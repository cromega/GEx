using System;
using System.Collections.Generic;


namespace Chirpesizer {
    public class Instrument {
        private OscillatorType Osc;
        private Envelope Envelope;
        private IValue Volume;
        private List<Trigger> Triggers;

        public Instrument(OscillatorType osc, IValue volume, Envelope envelope) {
            Osc = osc;
            Volume = volume;
            Envelope = envelope;        
            Triggers = new List<Trigger>();
        }

        public void Activate(IValue frequency, int length) {
            var osc = new Oscillator(Osc);
            var trig = new Trigger(osc, frequency, length);
            Triggers.Add(trig);
        }

        public bool IsActive() {
            return (Triggers.Count > 0);
        }

        public List<double[]> RenderTriggers(int frames) {
            var buffers = new List<double[]>();
            Triggers.ForEach(trigger => {
                var buffer = new double[frames * 2];
                var length = trigger.TTL + Envelope.Release;
                length = Math.Min(length, frames);
                double sample;
                for (int i = 0; i < length; i++) {
                    sample = trigger.Osc.Next(trigger.Frequency.Get(trigger.Age, trigger.IsActive));
                    sample *= Volume.Get(trigger.Age, trigger.IsActive);
                    sample *= short.MaxValue;
                    sample *= Envelope.Next(trigger.Age, trigger.IsActive);
                    buffer[i * 2] = sample;
                    buffer[i * 2 + 1] = sample;
                    trigger.Tick();
                }
                buffers.Add(buffer);
            });
            Triggers.RemoveAll(trigger => !trigger.IsActive && trigger.Age >= Envelope.Release);

            return buffers;
        }
    }
}