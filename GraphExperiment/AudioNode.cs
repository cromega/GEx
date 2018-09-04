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

        public Packet[] Next() {
            var packets = Fetch();
            packets = Update(packets);

            for (int i=0; i<packets.Length; i++) {
                LoadState(packets[i].TriggerID);
                packets[i] = Update(packets[i]);
            }

            return packets;
        }

        protected virtual Packet[] Update(Packet[] packets) {
            return packets;
        }

        protected virtual Packet Update(Packet packet) {
            return packet;
        }

        protected virtual Packet[] Fetch() {
            //FIXME
            while (Previous == null) {
                Thread.Sleep(1);
            }

            return Previous.Next();
        }

        public void Connect(AudioNode other) {
            Previous = other;
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

        public string Type() {
            return GetType().Name;
        }

        public static AudioNode Find(short nodeId) {
            return Nodes[nodeId];
        }
    }
}
