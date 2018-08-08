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
        private Wire Connection;
        public volatile bool IsActive;
        private List<AudioNode> Nodes;

        public Instrument(SoundSystem output) {
            Output = output;
            Connection = new Wire(2205);
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
            node.Output = Connection;
        }

        public void Run() {
            var buffer = new short[4410];
            Task.Run(() => {
                for (; ; ) {
                    Packet packet = null;
                    for (int i = 0; i < 4410; i += 2) {
                        packet = Connection.Take();
                        packet.Sample *= 20000;
                        buffer[i] = (short)(packet.Sample.L);
                        buffer[i + 1] = (short)(packet.Sample.R);
                    }

                    if (packet.Control == Control.End) {
                        Generator.Remove(packet.TriggerID);
                    }
                    Output.Write(buffer);
                }
            });
        }

        public string Trigger(double frequency) {
            return Generator.Start(frequency);
        }

        public void Release(string triggerId) {
            Generator.Release(triggerId);
        }
    }
}
