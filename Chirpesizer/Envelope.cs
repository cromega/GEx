using System;

namespace Chirpesizer {
    public class Envelope {
        public const int MAX_TIME = 44100;

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
                } else if (time < Attack + Decay) {
                    phase = (time - Attack) / (double)Decay;
                    value = (1.0 - phase * (1 - Sustain));
                } else {
                    value = Sustain;
                }
            } else {
                if (Release == 0) {
                    value = 0;
                } else if (time < Release) {
                    phase = 1 - Math.Abs(time) / (double)Release;
                    value = phase * Sustain;
                }
            }
            if (double.IsNaN(value) || double.IsInfinity(value)) { throw new Exception("value is NaN, check divisions"); }
            return value;
        }

        public static Envelope Parse(string data) {
            try {
                var values = data.Split(",".ToCharArray());
                return new Envelope(
                    int.Parse(values[0]),
                    int.Parse(values[1]),
                    double.Parse(values[2]),
                    int.Parse(values[3])
                );
            } catch (Exception e) {
                throw new Exception(String.Format("Can't create envelope from {0}", data));
            }
        }
    }
}

