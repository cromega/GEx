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

        public Packet(string triggerId, Control control, Sample sample) {
            TriggerID = triggerId;
            Control = control;
            Sample = sample;
        }
    }
}
