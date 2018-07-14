using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphExperiment {
    public abstract class AudioNode {
        //public static Dictionary<int, AudioNode> Nodes = new Dictionary<int, AudioNode>();
        public readonly int Id;
        private Wire Connection;
        public readonly Wire Input;

        public AudioNode(int id, Wire connection) {
            Connection = connection;
            Input = new Wire(4410);
            //Nodes.Add(Id, this);
        }

        public void Send(Packet packet) {
            Connection.Add(packet);
        }

        public Packet Read() {
            return Input.Take();
        }
    }
}
