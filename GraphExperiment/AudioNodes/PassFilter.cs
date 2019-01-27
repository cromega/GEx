using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphExperiment {
    public enum FilterType {
        LoPass,
        HiPass,
    }

    [AudioNode(Direction = AudioNodeDirection.InputOutput)]
    public class PassFilter : AudioNode {
        [AudioNodeParameter]
        public FilterType Filter;

        [AudioNodeParameter]
        public int Cutoff;

        private const double DT = 1d / 44100;

        public PassFilter() : base() {
            Cutoff = 440;
        }

        protected override Packet Update(Packet packet) {
            var RC = 1.0 / Cutoff * 2 * Math.PI;
            var alpha = DT / (RC + DT);

            double previousFiltered = Get<double>("PreviousFiltered", 0d);
            double previousRaw = Get<double>("PreviousRaw", 0d);

            double sample = 0;

            switch (Filter) {
                case FilterType.LoPass:
                    sample = alpha * packet.Sample.L + (1 - alpha) * previousFiltered;
                    break;
                case FilterType.HiPass:
                    sample = alpha * (previousFiltered) + alpha * (packet.Sample.L - previousRaw);
                    break;

            }
            Save("PreviousFiltered", sample);
            Save("PreviousRaw", packet.Sample.L);

            packet.Sample = new Sample(sample);
            return packet;
        }
    }
}
