using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chirpesizer {
    public class EnvelopeModulator : IModulator {
        public readonly Envelope Envelope;
        private string Target;

        public EnvelopeModulator(Envelope Envelope, string target) {
            this.Envelope = Envelope;
            Target = target;
        }

        public double Get(int time, bool isActive) {
            return Envelope.Next(time, isActive);
        }

        public string GetTarget() {
            return Target;
        }
    }
}
