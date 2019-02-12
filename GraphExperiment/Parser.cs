using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphExperiment {
    public struct NodeInfo {
        public AudioNode Node;
        public char Id;
        public char Target;

        public bool IsOutput {
            get { return Target == '-'; }
        }
    }

    public class Parser {

        public Machine ParseMachine(string data) {
            var m = new Machine();

            data.Replace(" ", "").Split(';').
                Select(ParseNode).
                ToList().
                ForEach(node => m.Add(node));


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
                Id = id,
                Target = parts.Last()[0],
            };

            return ni;
        }
    }
}
