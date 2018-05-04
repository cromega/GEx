using System;

namespace chirpcore {
    public class Envelope {
        private double Attack;
        private double Decay;
        private double Sustain;
        private double Release;

        public Envelope(double attack, double decay, double sustain, double release) {
            Attack = attack;
            Decay = decay;
            Sustain = sustain;
            Release = release;
        }
    }
}

