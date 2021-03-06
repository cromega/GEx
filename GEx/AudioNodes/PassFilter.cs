﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEx {
    public enum FilterType {
        LoPass,
        HiPass,
    }

    public class PassFilter : AudioNode {
        public FilterType Filter;
        public int Cutoff;

        private const double DT = 1d / 44100;

        public PassFilter() : base() {
            Cutoff = 440;
        }

        protected override Packet Update(Packet packet) {
            var RC = 1.0 / Cutoff * 2 * Math.PI;
            var alpha = DT / (RC + DT);

            double previousFiltered = Memory.Get<double>("PreviousFiltered", 0d);
            double previousRaw = Memory.Get<double>("PreviousRaw", 0d);

            double sample = 0;

            switch (Filter) {
                case FilterType.LoPass:
                    sample = alpha * packet.Sample.L + (1 - alpha) * previousFiltered;
                    break;
                case FilterType.HiPass:
                    sample = alpha * (previousFiltered) + alpha * (packet.Sample.L - previousRaw);
                    break;

            }
            Memory.Set("PreviousFiltered", sample);
            Memory.Set("PreviousRaw", packet.Sample.L);

            packet.Sample = new Sample(sample);
            return packet;
        }
    }
}
