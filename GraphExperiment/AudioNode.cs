using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace GraphExperiment {
    public abstract class AudioNode {
        //public static Dictionary<int, AudioNode> Nodes = new Dictionary<int, AudioNode>();
        public readonly int Id;
        public Wire Connection;
        public Wire Input;
        private Dictionary<string, Hashtable> Memory;
        private Hashtable State;

        public AudioNode(int id) {
            Input = new Wire(4410);
            Memory = new Dictionary<string, Hashtable>();
        }

        public AudioNode(int id, Wire connection) {
            Connection = connection;
            Input = new Wire(4410);
            //Nodes.Add(Id, this);
        }

        public void Send(Packet packet) {
            Connection.Add(packet);
        }

        public Packet Read() {
            var packet = Input.Take();
            if (!Memory.ContainsKey(packet.TriggerID)) {
                Memory[packet.TriggerID] = new Hashtable();
            }
            State = Memory[packet.TriggerID];
            return packet;

            //Send(Update(packet));
        }

        //protected abstract Packet Update(Packet packet);

        protected T Fetch<T>(string key) {
            return (T)(State[key]);
        }

        protected void Save(string key, object value) {
            State[key] = value;
        }

        protected bool IsSaved(string key) {
            return State.ContainsKey(key);
        }
    }
}
