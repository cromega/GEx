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
            double value = 0;
            switch (packet.Control) {
                case Control.Signal:
                    value = GetValue(packet.TimeMS);
                    break;
                case Control.End:
                    var tick = Fetch<int>("ReleasedFor", 0);
                    var timeMS = (int)(tick / 44.1);
                    value = GetReleasedValue(timeMS);
                    packet.Control = timeMS < Release ? Control.Signal : Control.End;
                    Save("ReleasedFor", ++tick);
                    break;
            }
            packet.Sample *= value;
            return packet;
        }

        private double GetValue(int time) {
            if (time < Attack) {
                return time / (double)Attack;
            } else if (time < Attack + Decay) {
                var phase = (time - Attack) / (double)Decay;
                return 1.0 - phase * (1 - Sustain);
            } else {
                return Sustain;
            }
        }

        private double GetReleasedValue(int time) {
            var phase = 1.0 - time / (double)Release;
            return phase * Sustain;
        }
    }
}

