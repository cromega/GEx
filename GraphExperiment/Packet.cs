using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphExperiment {
    public enum Control {
        Signal,
        End,
    }

    public class Packet {
        public string TriggerID;
        public Sample Sample;
        public Control Control;
        public int Tick;

        public double TimeMS {
            get { return Tick / 44.1; }
        }

        public Packet(string triggerId, Control control, Sample sample, int tick) {
            TriggerID = triggerId;
            Control = control;
            Sample = sample;
            Tick = tick;
        }

        public static Packet Empty() {
            return new Packet("", Control.Signal, new Sample(), 0);
        }

        public override string ToString() {
            return String.Format("L: {0}, R: {1}", Sample.L, Sample.R);
        }
    }
}
