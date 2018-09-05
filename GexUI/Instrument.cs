using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphExperiment;

namespace GexUI {
    class Instrument {
        private Output Audio;
        List<GraphExperiment.AudioNode> Nodes;

        public Instrument() {
            Audio = null;
            Nodes = new List<GraphExperiment.AudioNode>();
        }

        public void AddNode(GraphExperiment.AudioNode node) {
            if (node is Output) {
                if (Audio != null) {
                    throw new Exception("There can be only 1 Output.");
                }
                Audio = node as Output;
                Audio.TriggerEnded += (s, e) => {
                    Stop(e.TriggerID);
                };
                return;
            }

            Nodes.Add(node);
        }

        private void Stop(string triggerID) {
            GetFirstTrigger().Remove(triggerID);
        }

        public string Start(double frequency) {
            return GetFirstTrigger().Start(frequency);
        }

        public void Release(string triggerID) {
            GetFirstTrigger().Release(triggerID);
        }

        private Trigger GetFirstTrigger() {
            return Nodes.Find(node => node is Trigger) as Trigger;
        }
    }
}
