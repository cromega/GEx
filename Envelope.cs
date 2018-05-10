using System;

namespace chirpcore {
    public class Envelope {
        private int Attack;
        private int Decay;
        private double Sustain;
        private int Release;

        public Envelope(int attack, int decay, double sustain, int release) {
            Attack = attack;
            Decay = decay;
            Sustain = sustain;
            Release = release;
        }

        public void Modulate(short[] buffer, Trigger trigger) {
            short value;
            double phase;
            for (int i=0; i<buffer.Length/2; i++) {
                if (trigger.ActiveFor() < Decay) {
                    value = (short)(buffer[i*2] * ((double)trigger.ActiveFor() / Attack));
                    buffer[i*2] = value;
                    buffer[i*2+1] = value;
                }

                else if (trigger.ActiveFor() <= Attack + Decay) {
                    phase = (trigger.ActiveFor() - Attack) / (double)Decay;
                    value = (short)(buffer[i*2] * (1 - phase) * 1.0 + phase * Sustain);
                    buffer[i*2] = value;
                    buffer[i*2+1] = value;
                }

                else if (trigger.ActiveFor() > Attack + Decay) {
                    value = (short)(buffer[i*2] * Sustain);
                    buffer[i*2] = value;
                    buffer[i*2+1] = value;
                }

                else if (trigger.ActiveFor() > Decay) {
                    phase = (trigger.ActiveFor() - Attack) / (double)Decay;
                    value = (short)(buffer[i*2] * (1 - phase) * 1.0 + phase * Sustain);
                    
                }
                trigger.Update(1);
            }
        }
    }
}

