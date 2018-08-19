using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphExperiment {
    public enum SignalType : int {
        Sine,
        Square,
        Noise,
        Sawtooth,
        Triangle,
    }

    [AudioNode]
    public class Generator : AudioNode {
        public double Frequency;
        [AudioNodeParameter]
        public SignalType SignalType;
        private Oscillator Osc;

        public Generator(short id) : base(id) {
            Osc = new Oscillator();
        }

        protected override Packet Update(Packet packet) {
            Osc.SetFrequency(packet.Sample.L);
            packet.Sample = new Sample(Osc.Next(SignalType) * 0.2 * short.MaxValue);
            return packet;
        }
    }
}
