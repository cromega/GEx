using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphExperiment{
    public class Trigger {
        private Machine Machine;
        private double Frequency;
        private string Id;
        private bool IsActive;

        public Trigger() {
            IsActive = true;
            Id = Guid.NewGuid().ToString();
        }

        public Packet Next(long tick) {
            Machine.In(new Packet(Id, IsActive ? Signal.Active : Signal.End, new Sample(Frequency), tick));
            return Machine.Out(tick);
        }

        public void Release() {
            IsActive = false;
        }
    }
}
