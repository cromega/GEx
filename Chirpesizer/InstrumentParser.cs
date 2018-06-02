using System;
using System.Linq;
using Chirpesizer.Effects;
using System.Collections.Generic;

namespace Chirpesizer {
    public class InstrumentParser {
        private string InstrumentData;
        public static Instrument Parse(string data) {
            return new InstrumentParser(data).CreateInstrument();
        }

        private InstrumentParser(string data) {
            InstrumentData = data;
        }

        private Instrument CreateInstrument() {
            var parts = InstrumentData.Split(";".ToCharArray());
            OscillatorType osc = OscillatorType.Sine;
            switch (parts[0]) {
                case "0":
                    osc = OscillatorType.Noise;
                    break;
                case "1":
                    osc = OscillatorType.Sine;
                    break;
                case "2":
                    osc = OscillatorType.Square;
                    break;
            }
            var volume = ValueParser.Parse(parts[1]);
            Envelope envelope = Envelope.Decode(parts[2]);
            var effects = ParseEffects(parts.Skip(3));
            return new Instrument(osc, volume, envelope, effects);

            throw new Exception(String.Format("Can't create instrument from \"{0}\"", InstrumentData));
        }

        private List<IEffect> ParseEffects(IEnumerable<string> effectsData) {
            var effects = new List<IEffect>();
            effectsData.ToList().ForEach(effect => {
                var effectType = effect.Substring(0, 1);
                switch (effectType) {
                    case "1": effects.Add(Vibrato.Parse(effect.Substring(1))); break;
                    case "2": effects.Add(PitchEnvelope.Parse(effect.Substring(1))); break;
                }
            });

            if (effects.Any(eff => eff.GetEffectType() == EffectType.Vibrato) && effects.Any(eff => eff.GetEffectType() == EffectType.PitchEnvelope)) {
                throw new Exception("Can't have vibrato and pitch envelope at the same time");
            }

            return effects;
        }
    }
}