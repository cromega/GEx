using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace GraphExperiment {
    public abstract class AudioNode {
        private static Dictionary<short, AudioNode> Nodes = new Dictionary<short, AudioNode>();

        public readonly short Id;
        //public Wire Output;
        //public Wire Input;
        public AudioNode Previous;
        private Dictionary<string, Hashtable> Memory;
        private Hashtable State;

        public AudioNode(short id) {
            Id = id;
            //Input = new Wire(4410);
            Memory = new Dictionary<string, Hashtable>();
            Nodes.Add(id, this);
            //Run();
        }

        //public void Send(Packet packet) {
        //    Output.Add(packet);
        //}

        //protected virtual void Run() {
        //    Task.Run(() => {
        //        for (; ; ) {
        //            var packet = Read();
        //            Send(Update(packet));
        //        }
        //    });
        //}

        public virtual Packet[] Fetch() {
            var packets = Previous.Fetch();
            foreach (var packet in packets) { Update(packet); }
            return packets;
        }

        public void Connect(AudioNode other) {
            other.Previous = this;
            //Output = other.Input;
        }

        private void LoadState(string id) {
            if (!Memory.ContainsKey(id)) {
                Memory[id] = new Hashtable();
            }
            State = Memory[id];
        }

        //public Packet Read() {
        //    if (Input == null) { return null; }

        //    var packet = Input.Take();
        //    if (!Memory.ContainsKey(packet.TriggerID)) {
        //        Memory[packet.TriggerID] = new Hashtable();
        //    }
        //    State = Memory[packet.TriggerID];
        //    return packet;
        //}

        protected virtual Packet Update(Packet packet) {
            return packet;
        }

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
