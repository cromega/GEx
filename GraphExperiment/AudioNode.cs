using System;
using System.Collections.Generic;
using System.Linq;
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

            packets = Fetch(tick);
            for (int i=0; i<packets.Length; i++) {
                LoadState(packets[i].TriggerID);
                packets[i] = Update(packets[i]);
            }
            return packets;
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
            var value = State[key] ?? defaultValue;
            return (T)value;
        }

        protected void Save(string key, object value) {
            State[key] = value;
        }
    }
}
