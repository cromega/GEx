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

        public void Modulate(short[] buffer, int time) {
            short value;
            double phase;
            for (int i=0; i<buffer.Length/2; i++) {
                if (time <= Attack) {
                    value = (short)(buffer[i*2] * ((double)time / Attack));
                    buffer[i*2] = value;
                    buffer[i*2+1] = value;
                }

                else if (time <= Attack + Decay) {
                    phase = (time - Attack) / (double)Decay;
                    value = (short)(buffer[i*2] * (1 - phase) * 1.0 + phase * Sustain);
                    buffer[i*2] = value;
                    buffer[i*2+1] = value;
                }

                else if (time > Attack + Decay) {
                    value = (short)(buffer[i*2] * Sustain);
                    buffer[i*2] = value;
                    buffer[i*2+1] = value;
                }

                else if (time > Decay) {
                    return;
                }
                time++;
            }
        }
    }
}

