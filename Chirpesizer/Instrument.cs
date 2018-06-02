using System;
using System.Collections.Generic;
using Chirpesizer.Effects;


namespace Chirpesizer {
    public class Instrument {
        public readonly OscillatorType Osc;
        public readonly Envelope Envelope;
        public readonly IValue Volume;
        private List<Trigger> Triggers;
        public readonly List<IEffect> Effects;

        public Instrument(OscillatorType osc, IValue volume, Envelope envelope, List<IEffect> effects) {
            Osc = osc;
            Volume = volume;
            Envelope = envelope;        
            Triggers = new List<Trigger>();
            Effects = effects;
        }

        public Trigger Activate(double frequency, int length) {
            var freq = GetFreqencyWithEffects(frequency);
            var osc = new Oscillator(Osc);
            var trig = new Trigger(osc, freq, length);
            Triggers.Add(trig);
            return trig;
        }

        private IValue GetFreqencyWithEffects(double frequency) {
            IValue freq;
            var vibrato = Effects.Find(eff => eff.GetEffectType() == EffectType.Vibrato);
            var PitchEnvelope = Effects.Find(eff => eff.GetEffectType() == EffectType.PitchEnvelope);
            if (vibrato != null) {
                var effect = (Vibrato)vibrato;
                freq = new ModulatedValue(frequency, effect.Osc, effect.Frequency, effect.Height);
            } else if (PitchEnvelope != null) {
                var effect = (PitchEnvelope)PitchEnvelope;
                freq = new ModulatedValue(frequency, effect.Envelope);
            } else {
                freq = new StaticValue(frequency);
            }

            return freq;
        }

        public bool IsActive() {
            return (Triggers.Count > 0);
        }

        public List<double[]> RenderTriggers(int frames) {
            var buffers = new List<double[]>();
            Triggers.ForEach(trigger => {
                var buffer = new double[frames * 2];
                double sample;
                for (int i = 0; i < frames; i++) {
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