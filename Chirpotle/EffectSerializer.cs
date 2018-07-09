using Chirpesizer.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chirpotle {
    class EffectSerializer {
        public string Serialize(IEffect effect) {
            var output = new StringBuilder("");
            switch (effect.GetType().Name) {
                case "Reverb":
                    var reverb = (Reverb)effect;
                    output.AppendFormat("r{0},{1}", Math.Round(reverb.Delay * 44.1), reverb.Decay);
                    break;
            }

            return output.ToString();
        }
    }
}
