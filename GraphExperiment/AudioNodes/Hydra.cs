using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphExperiment {
    public class Hydra : AudioNode {
        public SignalType SignalType;
        public int Cents;

        private const int OscillatorsCount = 5;

        protected override Packet Update(Packet packet) {
            var oscillators = Get<Oscillator[]>("Oscillators");
            if (oscillators == null) {
                oscillators = new Oscillator[OscillatorsCount];
                for (int i=0; i<oscillators.Length; i++) {
                    oscillators[i] = new Oscillator();
                }
            }

            var sample = new Sample(0);
            for (int i = 0; i < oscillators.Length; i++) {
                var freq = packet.Sample.L * Math.Pow(2, GetSeparation() * (Math.Ceiling(oscillators.Length / 2d) - oscillators.Length + i));

                oscillators[i].SetFrequency(freq);
                sample += oscillators[i].Next(SignalType) / OscillatorsCount;
            }

            packet.Sample = sample;
            Save("Oscillators", oscillators);
            return packet;
        }

        private double GetSeparation() {
            return Cents / 1200d;
        }

        public static Hydra Parse(string data) {
            var parts = data.Split(',');
            return new Hydra {
                SignalType = (SignalType)int.Parse(parts[0]),
                Cents = int.Parse(parts[1])
            };
        }
    }
}
