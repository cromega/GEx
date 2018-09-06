using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphExperiment {
    [AudioNode(Direction = AudioNodeDirection.InputOutput)]
    public class Hydra : AudioNode {
        [AudioNodeParameter]
        public SignalType SignalType;

        [AudioNodeParameter]
        public int Cents;

        private const int OscillatorsCount = 5;

        public Hydra(short id) : base(id) { }

        protected override Packet Update(Packet packet) {
            var oscillators = Get<Oscillator[]>("Oscillators");
            if (oscillators == null) {
                oscillators = new Oscillator[OscillatorsCount];
                for (int i=0; i<oscillators.Length; i++) {
                    oscillators[i] = new Oscillator();
                }
            }

            double mixed = 0d;
            for (int i = 0; i < oscillators.Length; i++) {
                var freq = packet.Sample.L * Math.Pow(2, GetSeparation() * (Math.Ceiling(oscillators.Length / 2d) - oscillators.Length + i));

                oscillators[i].SetFrequency(freq);
                mixed += oscillators[i].Next(SignalType) * 0.2 / oscillators.Length * short.MaxValue;
            }

            packet.Sample = new Sample(mixed);
            Save("Oscillators", oscillators);
            return packet;
        }

        private double GetSeparation() {
            return Cents / 1200d;
        }
    }
}
