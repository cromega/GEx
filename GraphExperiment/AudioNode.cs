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
            Run();
        }

        public AudioNode(int id, Wire connection) {
            Connection = connection;
            Input = new Wire(4410);
            //Nodes.Add(Id, this);
            Run();
        }

        public void Send(Packet packet) {
            Connection.Add(packet);
        }

        protected virtual void Run() {
            Task.Run(() => {
                for (; ; ) {
                    var packet = Read();
                    Send(Update(packet));
                }
            });
        }

        public Packet Read() {
            if (Input == null) { return null; }

            var packet = Input.Take();
            if (!Memory.ContainsKey(packet.TriggerID)) {
                Memory[packet.TriggerID] = new Hashtable();
            }
            State = Memory[packet.TriggerID];
            return packet;
        }

        protected abstract Packet Update(Packet packet);

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
    }
}
