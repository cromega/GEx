using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphExperiment;

namespace TestGEx.Support {
    class SimpleGenerator : AudioNode {
        private List<Tuple<string, double>> SampleSets;

        public SimpleGenerator(short id) : base(id) {
            SampleSets = new List<Tuple<string, double>>();
        }

        public void AddSample(string triggerId, double value) {
            SampleSets.Add(new Tuple<string, double>(triggerId, value));
        }

        protected override Packet[] Fetch(long tick) {
            var output = SampleSets.
                Select(sample => new Packet(sample.Item1, Signal.Active, new Sample(sample.Item2), tick)).
                ToArray();
            SampleSets.Clear();

            return output;
        }
    }
}
