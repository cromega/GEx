using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphExperiment{
    public class Trigger : INode {
        private Machine Machine;
        private double Frequency;
        private string Id;

        public Trigger() {
            Id = Guid.NewGuid().ToString();
        }

        public Packet Next(long tick) {
            Machine.In(new Packet(Id, Signal.Active, new Sample(Frequency), tick));
            return Machine.Out(tick);
        }

        public void Release() {

        }

        public Packet[] Next(long tick) {
            var packet = new Packet(Guid.NewGuid().ToString(), Signal.Active, new Sample(Frequency), tick);
            return new Packet[] { packet };
        }

        public Packet[] Fetch(long tick) {
            var packets = new List<Packet>();
            Triggers.ToList().ForEach(trigger => {
                var state = trigger.Triggered ? Signal.Active : Signal.End;
                packets.Add(new Packet(trigger.ID, state, new Sample(trigger.Frequency), trigger.Time++));
            });

            return packets.ToArray();
        }
    }
}
