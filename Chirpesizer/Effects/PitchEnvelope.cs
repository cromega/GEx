using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chirpesizer;

namespace Chirpesizer.Effects {
    public class PitchEnvelope : IEffect {
        public readonly Envelope Envelope;

        public PitchEnvelope(int a, int d, double s, int r) {
            Envelope = new Envelope(a, d, s, r);
        }

        public EffectType GetEffectType() {
            return EffectType.PitchEnvelope;
        }

        public static PitchEnvelope Parse(string data) {
            var parts = data.Split(",".ToCharArray());
            var a = MTime.FromMs(int.Parse(parts[0])).Frames;
            var d = MTime.FromMs(int.Parse(parts[1])).Frames;
            var s = double.Parse(parts[2]);
            var r = MTime.FromMs(int.Parse(parts[3])).Frames;
            return new PitchEnvelope(a, d, s, r);
        }
    }
}
