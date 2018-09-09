using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphExperiment {
    class Machine {
        public List<AudioNode> Nodes;

        public Machine() {
            Nodes = new List<AudioNode>();
        }

        public void Add(AudioNode node) {
            Nodes.Add(node);
        }

        public void Connect(AudioNode source, AudioNode target) {
            target.Connect(source);
        }
    }
}
