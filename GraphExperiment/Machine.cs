using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphExperiment {
    public class Machine {
        public List<AudioNode> Nodes;

        public Machine() {
            Nodes = new List<AudioNode>();
        }

        #region These may not be needed. The entire class may not be needed.
        public void Add(AudioNode node) {
            Nodes.Add(node);
        }

        public void Remove(AudioNode node) {
            Nodes.Remove(node);
        }
        #endregion

        public void Connect(AudioNode source, AudioNode target) {
            target.Connect(source);
        }

        public void Disconnect(AudioNode source, AudioNode target) {
            target.Disconnect(source);
        }

        public string Trigger(double frequency) {
            var triggerId = Guid.NewGuid().ToString();
            foreach (var t in GetTriggers()) { t.Start(frequency, triggerId); }
            return triggerId;
        }

        public void Release(string triggerId) {
            foreach (var t in GetTriggers()) { t.Release(triggerId); }
        }

        public void Stop(string triggerId) {
            foreach (var t in GetTriggers()) { t.Remove(triggerId); }
        }

        private Trigger[] GetTriggers() {
            return Nodes.
                Where(n => n.Type() == "Trigger").
                Select(n => n as Trigger).
                ToArray();
        }
    }
}
