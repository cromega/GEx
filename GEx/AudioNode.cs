using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Threading;

namespace GEx {
    public interface INode {
        Packet[] Next(long tick);
    }

    public abstract class AudioNode : INode {
        public List<INode> Previous;
        protected StateStorage Memory;

        public AudioNode() {
            Memory = new StateStorage();
            Previous = new List<INode>();
        }

        public Packet[] Next(long tick) {
            var packets = Fetch(tick);
            for (int i=0; i<packets.Length; i++) {
                Memory.SetState(packets[i].TriggerID);
                packets[i] = Update(packets[i]);
            }
            return packets;
        }

        protected virtual Packet Update(Packet packet) {
            return packet;
        }

        protected virtual Packet[] Fetch(long tick) {
            return Previous
                .SelectMany(node => node.Next(tick))
                .ToArray();
        }

        public void Connect(INode previous) {
            Previous.Add(previous);
        }
    }
}
