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

            public TriggerInstance(double frequency, string id) {
                ID = id;
                Frequency = frequency;
                Triggered = true;
                Time = 0;
            }
        }

        private readonly object Lock;
        private List<TriggerInstance> Triggers;

        public Trigger() : base() {
            Triggers = new List<TriggerInstance>();
            Lock = new object();
        }

        protected override Packet[] Fetch(long tick) {
            var packets = new List<Packet>();
            Triggers.ToList().ForEach(trigger => {
                var state = trigger.Triggered ? Signal.Active : Signal.End;
                packets.Add(new Packet(trigger.ID, state, new Sample(trigger.Frequency), trigger.Time++));
            });

            return packets.ToArray();
        }

        public string Start(double frequency, string id) {
            TriggerInstance trigger = null;
            lock (Lock) {
                trigger = new TriggerInstance(frequency, id);
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

        public static AudioNode Parse(string data) {
            return new Trigger();
        }
    }
}
