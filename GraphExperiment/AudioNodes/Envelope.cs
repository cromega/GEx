using System;
using System.Threading.Tasks;

namespace GraphExperiment {
    [AudioNode]
    public class Envelope : AudioNode {
        [AudioNodeParameter]
        public int Attack ;
        [AudioNodeParameter]
        public int Decay;
        [AudioNodeParameter]
        public double Sustain;
        [AudioNodeParameter]
        public int Release;

        //public Envelope(int id, int attack, int decay, double sustain, int release) : base(id) {
        //    Attack = attack;
        //    Decay = decay;
        //    Sustain = sustain;
        //    Release = release;
        //}

        public Envelope(short id) : base(id) { }

        protected override Packet Update(Packet packet) {
            switch (packet.Control) {
                case Control.Signal:
                    packet.Sample *= HandleSignal(packet.Sample, packet.Time);
                    break;
                case Control.End:
                    var time = Fetch<int>("ReleasedFor", 0);
                    packet.Sample *= HandleRelease(packet.Sample, time);
                    Save("ReleasedFor", ++time);
                    break;
            }
            packet.Control = Fetch<int>("ReleasedFor", 0) > Release ? Control.End : Control.Signal;
            return packet;
        }

        private double HandleSignal(Sample sample, int time) {
            if (time < Attack) {
                return time / (double)Attack;
            } else if (time < Attack + Decay) {
                var phase = (time - Attack) / (double)Decay;
                return 1.0 - phase * (1 - Sustain);
            } else {
                return Sustain;
            }
        }

        private double HandleRelease(Sample sample, int time) {
            var phase = 1.0 - time / (double)Release;
            return phase * Sustain;
        }
    }
}
