using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphExperiment {
    public enum Signal {
        Active,
        End,
    }

    public class Packet {
        public string TriggerID;
        public Sample Sample;
        public Signal Signal;
        public long Tick;

        public double TimeMS {
            get { return Tick / 44.1; }
        }

        public Packet(string triggerId, Signal control, Sample sample, long tick) {
            TriggerID = triggerId;
            Signal = control;
            Sample = sample;
            Tick = tick;
        }

        public static Packet Empty() {
            return new Packet("", Signal.Active, new Sample(), 0);
        }

        public override string ToString() {
            return String.Format("L: {0}, R: {1}", Sample.L, Sample.R);
        }
    }
}
