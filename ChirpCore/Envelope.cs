using System;

namespace chirpcore {
    public class Envelope {
        public readonly int Attack ;
        public readonly int Decay;
        public readonly double Sustain;
        public readonly int Release;

        public Envelope(int attack, int decay, double sustain, int release) {
            Attack = attack;
            Decay = decay;
            Sustain = sustain;
            Release = release;
        }

        public void Modulate(double[] buffer, Trigger trigger) {
            double value;
            double phase;
            for (int i=0; i<buffer.Length/2; i++) {
                if (trigger.Age< Decay) {
                    value = buffer[i*2] * (trigger.Age/ (double)Attack);
                    buffer[i*2] = value;
                    buffer[i*2+1] = value;
                }

                else if (trigger.Age<= Attack+ Decay) {
                    phase = (trigger.Age- Attack) / (double)Decay;
                    value = buffer[i*2] * (1 - phase) * 1.0 + phase * Sustain;
                    buffer[i*2] = value;
                    buffer[i*2+1] = value;
                }

                else if (trigger.Age> Attack+ Decay) {
                    value = buffer[i*2] * Sustain;
                    buffer[i*2] = value;
                    buffer[i*2+1] = value;
                }

                else if (trigger.Age> Decay) {
                    // phase = (trigger.Age.Frames - Attack.Frames) / (double)Decay.Frames;
                    // value = buffer[i*2] * (1 - phase) * 1.0 + phase * Sustain;
                    
                }
                trigger.Update(1);
            }
        }

        public static Envelope Parse(string envelopeData) {
            int attack, decay, release;
            double sustain;
            try {
                var bits = envelopeData.Split(",".ToCharArray());
                attack = MTime.FromMs(int.Parse(bits[0])).Frames;
                decay = MTime.FromMs(int.Parse(bits[1])).Frames;
                sustain = double.Parse(bits[2]);
                release = MTime.FromMs(int.Parse(bits[3])).Frames;
            } catch {
                throw new Exception(String.Format("don't know how to create envelope from {0}", envelopeData));
            }
            return new Envelope(attack, decay, sustain, release);
        }
    }
}

