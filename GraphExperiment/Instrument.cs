using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace GraphExperiment {
    public class Instrument {
        private Generator Generator;
        private SoundSystem Output;
        AudioNode LastNode;
        public volatile bool IsActive;
        private List<AudioNode> Nodes;

        public Instrument(SoundSystem output) {
            Output = output;
            IsActive = false;
            Nodes = new List<AudioNode>();
        }

        public void AddNode(AudioNode node) {
            if (node.Type() == "Output" && Nodes.Any(n => n.Type() == "Output")) { throw new Exception("Only 1 output allowed"); }
            Nodes.Add(node);

            switch (node.Type()) {
                case "Generator": First(node as Generator); break;
                case "Output": Last(node); break;
            }
        }

        public void First(Generator node) {
            Generator = node;
        }

        public void Last(AudioNode node) {
            LastNode = node;
        }

        public void Run() {
            var buffer = new short[4410];
            for (; ; ) {
                if (LastNode == null) { System.Threading.Thread.Sleep(1); continue; }
                Packet packet = null;
                for (int i = 0; i < 4410; i += 2) {
                    var packets = LastNode.Fetch();
                    if (packets.Length == 0) { break; }

                    packet = packets[0];
                    packet.Sample *= 20000;
                    buffer[i] = (short)(packet.Sample.L);
                    buffer[i + 1] = (short)(packet.Sample.R);
                }

                if (packet.Control == Control.End) {
                    Generator.Remove(packet.TriggerID);
                }
                Output.Write(buffer);
            }
        }

        public string Trigger(double frequency) {
            return Generator.Start(frequency);
        }

        public void Release(string triggerId) {
            Generator.Release(triggerId);
        }
    }
}
