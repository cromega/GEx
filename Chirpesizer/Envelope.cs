using System;

namespace Chirpesizer {
    public class Envelope : IEncodable {
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

        public double Next(int time, bool isActive) {
            double value = 0;
            double phase;
            if (isActive) {
                if (time < Attack) {
                    value = time / (double)Attack;
                } else if (time <= Attack + Decay) {
                    phase = (time - Attack) / (double)Decay;
                    value = (1.0 - phase * (1 - Sustain));
                } else {
                    value = Sustain;
                }
            } else {
                if (time < Release) {
                    phase = 1 - time / (double)Release;
                    value = phase * Sustain;
                }
            }
            return value;
        }

        public string Encode() {
            return String.Format("{0},{1},{2},{3}", Attack, Decay, Sustain, Release);
        }

        public static Envelope Decode(string data) {
            try {
                var values = data.Split(",".ToCharArray());
                return new Envelope(
                    MTime.FromMs(int.Parse(values[0])).Frames,
                    MTime.FromMs(int.Parse(values[1])).Frames,
                    double.Parse(values[2]),
                    MTime.FromMs(int.Parse(values[3])).Frames
                );
            } catch (Exception e) {
                throw new Exception(String.Format("Can't create envelope from {0}", data));
            }
        }
    }
}

