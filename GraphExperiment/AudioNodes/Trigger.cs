using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphExperiment{
    [AudioNode(Direction = AudioNodeDirection.OutputOnly)]
    public class Trigger : AudioNode {
        class TriggerInstance {
            public string ID;
            public double Frequency;
            public int Time;
            public bool Triggered;

            public TriggerInstance(double frequency) {
                ID = Guid.NewGuid().ToString();
                Frequency = frequency;
                Triggered = true;
                Time = 0;
            }
        }

        private readonly object Lock;
        private List<TriggerInstance> Triggers;

        public Trigger(short id) : base(id) {
            Triggers = new List<TriggerInstance>();
            Lock = new object();
        }

        protected override Packet[] Fetch() {
            var packets = new List<Packet>();
            Triggers.ForEach(trigger => {
                var state = trigger.Triggered ? Control.Signal : Control.End;
                packets.Add(new Packet(trigger.ID, state, new Sample(trigger.Frequency), trigger.Time++));
            });

            return packets.ToArray();
        }

        public string Start(double frequency) {
            TriggerInstance trigger = null;
            lock (Lock) {
                trigger = new TriggerInstance(frequency);
                Triggers.Add(trigger);
            }
            return trigger.ID;
        }

        public void Release(string triggerID) {
            lock(Lock) {
                Triggers.First(trigger => trigger.ID == triggerID).Triggered = false;
            }
        }

        public void Remove(string triggerID) {
            lock (Lock) {
                Triggers.RemoveAll(trigger => trigger.ID == triggerID);
            }
        }
    }
}
