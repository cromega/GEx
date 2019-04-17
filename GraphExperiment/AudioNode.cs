using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Threading;

namespace GraphExperiment {
    public interface INode {
        Packet[] Next(long tick);
    }

    public abstract class AudioNode : INode {
        struct FetchData {
            public long Tick;
            public Packet[] Data;
        }

        public List<INode> Previous;
        private Dictionary<string, Hashtable> Memory;
        private Hashtable State;
        private FetchData PreviousFetch;

        public AudioNode() {
            Memory = new Dictionary<string, Hashtable>();
            Previous = new List<INode>();
            PreviousFetch = new FetchData { Tick = -1 };
        }

        public Packet[] Next(long tick) {

            Packet[] packets;
            if (tick == PreviousFetch.Tick) {
                packets = PreviousFetch.Data;
            } else {
                packets = Fetch(tick);
                PreviousFetch = new FetchData { Tick = tick, Data = packets };
            }

            //mux samples by trigger id
            var mixedTriggers = packets.GroupBy(sample => sample.TriggerID).
                Select(group =>
                    new Packet(
                        group.Key,
                        group.First().Signal,
                        group.Aggregate(new Sample(0), (sample, packet) => sample + packet.Sample),
                        group.First().Tick
                     )
                 ).ToArray();


            for (int i = 0; i < mixedTriggers.Count(); i++) {
                LoadState(mixedTriggers[i].TriggerID);
                mixedTriggers[i] = Update(mixedTriggers[i]);
            }

            return mixedTriggers;
        }

        protected virtual Packet Update(Packet packet) {
            return packet;
        }

        protected virtual Packet[] Fetch(long tick) {
            return Previous.
                SelectMany(node => node.Next(tick))
                .ToArray();
        }

        public void Connect(INode previous) {
            Previous.Add(previous);
        }

        private void LoadState(string id) {
            if (!Memory.ContainsKey(id)) {
                Memory[id] = new Hashtable();
            }
            State = Memory[id];
        }

        protected T Get<T>(string key) {
            return (T)(State[key]);
        }

        protected T Get<T>(string key, object defaultValue) {
            var savedValue = State[key] ?? defaultValue;
            return (T)savedValue;
        }

        protected void Save(string key, object value) {
            State[key] = value;
        }
    }
}
