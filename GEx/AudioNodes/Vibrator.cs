using System;

namespace GEx {
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
