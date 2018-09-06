using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphExperiment{
    [AudioNode(Direction = AudioNodeDirection.InputOutput)]
    public class PassFilter : AudioNode {
        public int Cutoff;

        private double DT;
        private double RC;
        private double alpha;

        public PassFilter(short id) : base(id) {
            Cutoff = 500;

            DT = 1d / 44100;
            RC = 1.0 / Cutoff * 2 * Math.PI;
            alpha = DT / (RC + DT);
        }

        protected override Packet Update(Packet packet) {
            var lastSample = Get<double>("LastSample", packet.Sample.L * alpha);
            //var sample = lastSample + (alpha * (packet.Sample.L - lastSample));
            //var sample = alpha * lastSample  + alpha * ()
            Save("LastSample", sample);
            packet.Sample = new Sample(sample);
            return packet;
        }
    }
}
