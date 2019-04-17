using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphExperiment {
    public struct NodeInfo {
        public AudioNode Node;
        public string Id;
        public string Target;

        public bool IsOutput {
            get { return Target == "-"; }
        }
    }

    public class Parser {
        public Machine ParseMachine(string data) {
            var m = new Machine();

            var parts = data.Replace(" ", "").Split(';');
            var receivers = parts[0].Substring(1).Split(',');

            var nodes = new Dictionary<string, NodeInfo>();
            parts.Skip(1).
                Select(ParseNode).
                ToList().
                ForEach(node => nodes[node.Id] = node);

            for (int i=0; i<receivers.Length; i++) {
                m.Receivers.Add(nodes[receivers[i]].Node);
            }

            foreach (var node in nodes.Values) {
                if (node.IsOutput) { m.Outputs.Add(node.Node); }
                else { nodes[node.Target].Node.Connect(node.Node); }
            }

            m.Setup();
            return m;
        }

        private NodeInfo ParseNode(string node) {
            // 0: id+class name, 1: node constructor params, 2: connection
            var parts = node.Split(':', '>');
            var id = node[0];
            var className = parts[0].Substring(1);
            var fullClassName = $"GraphExperiment.{className},GraphExperiment";
            var nodeType = Type.GetType(fullClassName, true);

            var ni = new NodeInfo {
                Node = (AudioNode)nodeType.GetMethod("Parse").Invoke(null, new object[] { parts[1] }),
                Id = id.ToString(),
                Target = parts[2].ToString(),
            };

            return ni;
        }
    }
}
