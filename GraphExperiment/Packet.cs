using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphExperiment {
    public enum Signal {
        Active,
        End,
    }

#if DEBUG
    [DebuggerDisplay("L: {Sample.L}, R: {Sample.R}")]
#endif
    public class Packet {
        public string TriggerID;
        public Sample Sample;
        public Signal Signal;
        public long Tick;
        public long TriggerLife;

        public Packet(string triggerId, Signal control, Sample sample, long tick, long triggerLife) {
            TriggerID = triggerId;
            Signal = control;
            Sample = sample;
            Tick = tick;
            TriggerLife = triggerLife;
        }

        public static Packet Empty() {
            return new Packet("", Signal.Active, new Sample(), 0, 0);
        }
   }
}
