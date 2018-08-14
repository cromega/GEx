using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphExperiment {
    public enum SignalType : int {
        Sine = 0,
        Square = 1,
        Noise = 2,
    }

    [AudioNode]
    public class Generator : AudioNode {

        public double Frequency;
        [AudioNodeParameter]
        public SignalType SignalType;
        private Random Rnd;

        public Generator(short id) : base(id) {
            Rnd = new Random();
        }

        protected override Packet Update(Packet packet) {
            Sample sample = null;
            switch (SignalType) {
                case SignalType.Sine: sample = new Sample(Sine(packet.Sample.L, packet.Tick)); break;
                case SignalType.Square: sample = new Sample(Square(packet.Sample.L, packet.Tick)); break;
                case SignalType.Noise: sample = new Sample(Noise()); break;
            }
            packet.Sample = sample;
            return packet;
        }

        private double Sine(double frequency, int t) {
            return Math.Sin(frequency * Math.PI * 2 * t / 44100);
        }

        private double Square(double frequency, int t) {
            return Sine(frequency, t) > 0 ? 1 : -1;
        }

        private double Noise() {
            return Rnd.NextDouble() * 2 - 1;
        }
    }
}
