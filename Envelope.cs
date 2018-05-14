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

        public void Modulate(double[] buffer, Trigger trigger) {
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

        public static Envelope Parse(string envelopeData) {
            int attack, decay, release;
            double sustain;
            try {
                var bits = envelopeData.Split(",".ToCharArray());
                attack = int.Parse(bits[0]);
                decay = int.Parse(bits[1]);
                sustain = double.Parse(bits[2]);
                release = int.Parse(bits[3]);
            } catch {
                throw new Exception(String.Format("don't know how to create envelope from {0}", envelopeData));
            }
            return new Envelope(attack, decay, sustain, release);
        }
    }
}

