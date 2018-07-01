using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chirpesizer.Effects;

namespace Chirpesizer {
    public class EffectParser {
        public List<IEffect> GetEffects(string[] effectsData) {
            var effects = new List<IEffect>();

            effectsData.ToList().ForEach(effectData => {
                var effectParams = effectData.Substring(1).Split(",".ToCharArray());
                switch (effectData[0]) {
                    case 'r': effects.Add(new Reverb(int.Parse(effectParams[0]), double.Parse(effectParams[1]))); break;
                }
            });

            return effects;
        }
    }
}
