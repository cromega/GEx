using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Threading;

namespace GraphExperiment {
    public abstract class AudioNode {
        private static Dictionary<short, AudioNode> Nodes = new Dictionary<short, AudioNode>();

        public readonly short Id;
        public AudioNode Previous;
        private Dictionary<string, Hashtable> Memory;
        private Hashtable State;

        public AudioNode(short id) {
            Id = id;
            Memory = new Dictionary<string, Hashtable>();
            Nodes.Add(id, this);
        }

        public virtual Packet[] Fetch() {
            while (Previous == null) {
                Thread.Sleep(1);
            }

            var packets = Previous.Fetch();
            foreach (var packet in packets) { Update(packet); }
            return packets;
        }

        public void Connect(AudioNode other) {
            other.Previous = this;
        }

        private void LoadState(string id) {
            if (!Memory.ContainsKey(id)) {
                Memory[id] = new Hashtable();
            }
            State = Memory[id];
        }

        protected virtual void Update(Packet packet) { }

        protected T Fetch<T>(string key) {
            return (T)(State[key]);
        }

        protected T Fetch<T>(string key, object defaultValue) {
            var savedValue = State[key] ?? defaultValue;
            return (T)savedValue;
        }

        protected void Save(string key, object value) {
            State[key] = value;
        }

        public string Type() {
            return GetType().Name;
        }

        public static AudioNode Find(short nodeId) {
            return Nodes[nodeId];
        }
    }
}
