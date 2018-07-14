using System;
using System.Threading.Tasks;

namespace GraphExperiment {
    public class Envelope : AudioNode {
        public readonly int Attack ;
        public readonly int Decay;
        public readonly double Sustain;
        public readonly int Release;

        public Envelope(int id, int attack, int decay, double sustain, int release, Wire connection) : base(id, connection) {
            Attack = attack;
            Decay = decay;
            Sustain = sustain;
            Release = release;

            Task.Run(() => Run());
        }

        private void Run() {
            var time = 0;
            Sample previousSample;
            for (; ; ) {
                var packet = Read();
                switch (packet.Control) {
                    case Control.Signal:
                        HandleSignal(packet.Sample, time++);
                        previousSample = packet.Sample;
                        break;
                    case Control.End:
                        HandleRelease(packet.Sample);
                        time = 0;
                        break;
                }
            }
        }

        private void HandleSignal(Sample sample, int time) {
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
            Send(new Packet(Control.Signal, sample * value));
        }

        private void HandleRelease(Sample sample) {
            var time = 0;
            double phase;
            for (; ;) {
                if (time < Release) {
                    phase = 1 - Math.Abs(time) / (double)Release;
                    Send(new Packet(Control.Signal, sample * (phase * Sustain)));
                    time++;
                } else break;
            }
            Send(new Packet(Control.End, new Sample(0)));
        }
    }
}

