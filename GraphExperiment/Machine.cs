using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphExperiment {
    public class Machine {
        class Source : INode {
            private volatile Packet Packet;
            public void Set(Packet packet) { Packet = packet; }
            public Packet[] Next(long tick) { return new Packet[] { Packet }; }
        }

        public List<AudioNode> Outputs;
        public List<AudioNode> Receivers;
        private Muxer Muxer;
        private Source Src;


        public Machine() {
            Outputs = new List<AudioNode>();
            Receivers = new List<AudioNode>();
            Muxer = new Muxer();
            Src = new Source();
        }

        public void Setup() {
            foreach (var node in Receivers) { node.Connect(Src); }
        }

        public Packet Process(long tick, Packet packet) {
            Src.Set(packet);
            var packets = Outputs.SelectMany(node => node.Next(tick)).ToArray();
            for (int i = 0; i < packets.Count(); i++) { Muxer.Add(packets[i].Sample); }
            packet.Sample = Muxer.Mux();
            return packet;
        }
    }
}
