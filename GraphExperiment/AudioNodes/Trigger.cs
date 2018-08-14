using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphExperiment{
    [AudioNode(Direction = AudioNodeDirection.OutputOnly)]
    public class Trigger : AudioNode {
        private object Lock = new object();
        private List<TriggerInstance> Triggers;

        public Trigger(short id) : base(id) {
            Triggers = new List<TriggerInstance>();
        }

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

        protected override Packet[] Fetch() {
            var packets = new List<Packet>();
            Triggers.ForEach(trigger => {
                var state = trigger.Triggered ? Control.Signal : Control.End;
                packets.Add(new Packet(trigger.ID, state, new Sample(trigger.Frequency), trigger.Time++));
            });

            return packets.ToArray();
        }

        public string Start(double frequency) {
            var trigger = new TriggerInstance(frequency);
            lock (Lock) {
                Triggers.Add(trigger);
            }
            Logger.Log("Starting trigger {0}", trigger.ID);
            return trigger.ID;
        }

        public void Release(string triggerID) {
            lock(Lock) {
                Logger.Log("releasing trigger {0}", triggerID);
                Triggers.First(trigger => trigger.ID == triggerID).Triggered = false;
            }
        }

        public void Remove(string triggerID) {
            lock (Lock) {
                Logger.Log("Removing trigger {0}", triggerID);
                Logger.Log("Removed {0} triggers", Triggers.RemoveAll(trigger => trigger.ID == triggerID));
            }
        }
    }
}
