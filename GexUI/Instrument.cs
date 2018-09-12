using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace GexUI {
    public class Instrument : ObservableObject {
        private List<AudioNode> _Nodes;
        public List<AudioNode> Nodes {
            get { return _Nodes.ToList(); }
        }

        private GraphExperiment.Machine Machine;

        public Instrument() {
            _Nodes = new List<AudioNode>();
            Machine = new GraphExperiment.Machine();
        }

        public void AddNode(AudioNode node) {
            if (node.Type() == "Output") {
                SetUpOutput(node);
            }

            _Nodes.Add(node);
            Machine.Add(node.AudioControl);
            _PropertyChanged("Nodes");
        }

        public void RemoveNode(AudioNode node) {
            _Nodes.Remove(node);
            Machine.Remove(node.AudioControl);
            _PropertyChanged("Nodes");
        }

        private void SetUpOutput(AudioNode node) {
            if (Nodes.Any(n => n.Type() == "Output")) {
                throw new Exception("There can be only 1 Output.");
            }
            var output = node.AudioControl as Output;
            output.TriggerEnded += (s, e) => {
                Stop(e.TriggerID);
            };
        }

        public string Start(double frequency) {
            return Machine.Trigger(frequency);
        }

        private void Stop(string triggerID) {
            Machine.Stop(triggerID);
        }

        public void Release(string triggerID) {
            Machine.Release(triggerID);
        }

    }
}
