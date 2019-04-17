using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphExperiment{
    public class Trigger {
        private Machine Machine;
        private double Frequency;
        private string Id;
        private bool IsActive;

        public Trigger(Machine machine, double frequency) {
            IsActive = true;
            Id = Guid.NewGuid().ToString();
            Machine = machine;
            Frequency = frequency;
        }

        public Packet Next(long tick) {
            return Machine.Process(
                tick,
                new Packet(Id, IsActive ? Signal.Active : Signal.End, new Sample(Frequency), tick));
        }

        public void Release() {
            IsActive = false;
        }
    }
}
