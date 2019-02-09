using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphExperiment;

namespace GraphExperiment {
    public class Vibrator : AudioNode {
        public SignalType Signal;
        public double Frequency;
        public double Amount;

        private Oscillator Osc;

        public Vibrator() : base() {
            Osc = new Oscillator();
        }

        protected override Packet Update(Packet packet) {
            Osc.SetFrequency(Frequency);
            packet.Sample *= Osc.Next(Signal);
            return packet;
        }
    }
}
