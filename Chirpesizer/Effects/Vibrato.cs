using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chirpesizer.Effects {
    public class Vibrato : IEffect {
        public readonly OscillatorType Osc;
        public readonly double Frequency;
        public readonly double Height;

        public Vibrato(OscillatorType osc, double frequency, double height) {
            Osc = osc;
            Frequency = frequency;
            Height = height;
        }

        public EffectType GetEffectType() {
            return EffectType.Vibrato;
        }

        public static Vibrato Parse(string data) {
            var parts = data.Split(",".ToCharArray());
            return new Vibrato((OscillatorType)int.Parse(parts[0]), double.Parse(parts[1]), double.Parse(parts[2]));
        }
    }
}
