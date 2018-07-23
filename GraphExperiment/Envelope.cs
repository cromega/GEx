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

        public Envelope(int id, int attack, int decay, double sustain, int release) : base(id) {
            Attack = attack;
            Decay = decay;
            Sustain = sustain;
            Release = release;
        }

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
            double value = sample.L;
            double phase;
            if (time < Attack) {
                value = time / (double)Attack;
            } else if (time < Attack + Decay) {
                phase = (time - Attack) / (double)Decay;
                value = 1.0 - phase * (1 - Sustain);
            } else {
                value = Sustain;
            }
            return value;
        }

        private double HandleRelease(Sample sample, int time) {
            double phase;
            if (time < Release) {
                phase = 1 - time / (double)Release;
                return phase * Sustain;
            } else return 0;
        }
    }
}

