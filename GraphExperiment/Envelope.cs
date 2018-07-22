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

            Task.Run(() => Run());
        }

        private void Run() {
            for (; ; ) {
                var packet = Read();
                switch (packet.Control) {
                    case Control.Signal:
                        packet.Sample *= HandleSignal(packet.Sample, packet.Time);
                        break;
                    case Control.End:
                        if (!IsSaved("ReleasedFor")) {
                            Save("ReleasedFor", 0);
                        }
                        var time = Fetch<int>("ReleasedFor");
                        packet.Sample *= HandleRelease(packet.Sample, time);
                        Save("ReleasedFor", ++time);
                        break;
                }
                packet.Control = IsSaved("ReleasedFor") ? Control.End : Control.Signal;
                Send(packet);
            }
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
                phase = 1 - Math.Abs(time) / (double)Release;
                return phase * Sustain;
            } else return 0;
        }
    }
}

