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
        public List<AudioNode> Previous;
        private Dictionary<string, Hashtable> Memory;
        private Hashtable State;

        public AudioNode(short id) {
            Id = id;
            Memory = new Dictionary<string, Hashtable>();
            Nodes.Add(id, this);
            Previous = new List<AudioNode>();
        }

        public Packet[] Next() {
            var packets = Fetch();

            //mux samples by trigger id
            var mixedTriggers = packets.GroupBy(sample => sample.TriggerID).
                Select(group =>
                    new Packet(
                        group.Key,
                        group.First().Control,
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

        protected virtual Packet[] Fetch() {
            //FIXME
            while (Previous == null) {
                Thread.Sleep(1);
            }

            return Previous.
                SelectMany(node => node.Next())
                .ToArray();
        }

        public void Connect(AudioNode other) {
            Previous.Add(other);
        }

        public void Disconnect() {
            Previous = null;
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
