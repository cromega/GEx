using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphExperiment {
    public class Machine {
        class Source : INode {
            public Packet[] packets = new Packet[1];

            public void Add(Packet packet) {
                packets[0] = packet;
            }
            public Packet[] Next(long tick) {
                return packets;
            }
        }

        private List<NodeInfo> NodeInfos;

        private Packet PacketIn;
        private List<AudioNode> Outputs;
        private Source Src;
        private Muxer Muxer;

        public Machine() {
            Outputs = new List<AudioNode>();
            NodeInfos = new List<NodeInfo>();
            Src = new Source();
            Muxer = new Muxer();
        }

        public void Add(NodeInfo ni) {
            NodeInfos.Add(ni);
        }

        public void In(Packet packet) {
            Src.Add(packet);
        }

        public Packet Out(long tick) {
            var packets = Outputs.SelectMany(node => node.Next(tick));
            var triggerId = packets.First().TriggerID;

            packets.ToList().ForEach(p => Muxer.Add(p.Sample));
            var sample = Muxer.Mux();

            return new Packet(triggerId, packets.Any(p => p.Signal == Signal.Active) ? Signal.Active : Signal.End, sample, tick);
        }

        public void Setup(INode source) {
            //var data = "0Generator:0>2; 1Envelope:5,5,0.5,5>-";
            NodeInfos.ForEach(ni => {
                if (!ni.IsOutput) {
                    Connect(ni.Node, NodeInfos.Find(n => n.Id == ni.Target).Node);
                }
                if (ni.IsOutput) { Outputs.Add(ni.Node); }
            });
            NodeInfos.First().Node.Previous.Add(Src);


        }

        private void Connect(AudioNode source, AudioNode target) {
            target.Connect(source);
        }
    }
}
