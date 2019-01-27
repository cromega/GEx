using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphExperiment {
    public class Parser {
        struct NodeInfo {
            public AudioNode Node;
            public int Id;
            public int Target;
        }

        public Parser() {
        }

        public Machine ParseMachine(string data) {
            var m = new Machine();

            var nodes = data.Split(';').
                Select(ParseNode).
                ToList();

            nodes.ForEach(node => {
                m.Add(node.Node);
                m.Connect(node.Node, nodes.Find(n => n.Id == node.Id).Node);
            });

            return m;
        }

        private NodeInfo ParseNode(string node) {
            var parts = node.Split(':', '>');
            var id = int.Parse(node[0].ToString());
            var className = parts[0].Substring(1);
            className = char.ToUpper(className[0]) + className.Substring(1);
            var fullClassName = $"GraphExperiment.{className},GraphExperiment";
            var nodeType = Type.GetType(fullClassName, true);

            var ni = new NodeInfo {
                Node = (AudioNode)nodeType.GetMethod("Parse").Invoke(null, new object[] { parts[1] }),
                Id = id,
                Target = parts.Last() == "-" ? -1 : int.Parse(parts.Last())
            };

            return ni;
        }
    }
}
