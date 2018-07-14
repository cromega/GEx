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
        public Sample Sample;
        public Control Control;

        public Packet(Control control, Sample sample) {
            Control = control;
            Sample = sample;
        }
    }
}
