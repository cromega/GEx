using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GEx;

namespace TestGEx.Support {
    class SimpleGenerator : AudioNode {
        private List<Tuple<string, double>> SampleSets;

        public SimpleGenerator() : base() {
            SampleSets = new List<Tuple<string, double>>();
        }

        public void AddSample(string triggerId, double value) {
            SampleSets.Add(new Tuple<string, double>(triggerId, value));
        }

        protected override Packet[] Fetch(long tick) {
            var output = SampleSets.
                Select(sample => new Packet(sample.Item1, Signal.Active, new Sample(sample.Item2), tick, tick)).
                ToArray();
            SampleSets.Clear();

            return output;
        }
    }
}
