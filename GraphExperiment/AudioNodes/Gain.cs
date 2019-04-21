using System;

namespace GraphExperiment{
    class Gain : AudioNode {
        public double Level;
        protected override Packet Update(Packet packet) {
            packet.Sample *= Level * short.MaxValue;
            return packet;
        }

        public static Gain Parse(string data) {
            return new Gain() {
                Level = double.Parse(data)
            };
        }
    }
}
