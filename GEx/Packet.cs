using System.Diagnostics;

namespace GEx {
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
   }
}
