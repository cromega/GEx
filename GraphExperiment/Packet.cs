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

        public int TimeMS {
            get { return (int)(Tick / 44.1); }
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
    }
}
