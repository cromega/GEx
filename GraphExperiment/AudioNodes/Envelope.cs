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

        protected override void Update(Packet packet) {
            double value = 0;
            switch (packet.Control) {
                case Control.Signal:
                    value = GetVolume(packet.Time);
                    break;
                case Control.End:
                    var time = Fetch<int>("ReleasedFor", 0);
                    value = GetReleaseVolume(time);
                    Save("ReleasedFor", ++time);
                    break;
            }
            packet.Sample *= value;
            packet.Control = Fetch<int>("ReleasedFor", 0) > Release ? Control.End : Control.Signal;
        }

        private double GetVolume(int time) {
            if (time < Attack) {
                return time / (double)Attack;
            } else if (time < Attack + Decay) {
                var phase = (time - Attack) / (double)Decay;
                return 1.0 - phase * (1 - Sustain);
            } else {
                return Sustain;
            }
        }

        private double GetReleaseVolume(int time) {
            var phase = 1.0 - time / (double)Release;
            return phase * Sustain;
        }
    }
}

