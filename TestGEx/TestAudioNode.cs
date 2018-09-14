using System;
using System.Collections.Generic;
using System.Linq;
using GraphExperiment;

using Xunit;
using FluentAssertions;

namespace TestGEx {
    // a node that sends a set of preloaded samples to the downstream node
    class TestGenerator : AudioNode {
        private List<Tuple<string, double>> SampleSets;

        public TestGenerator(short id) : base(id) {
            SampleSets = new List<Tuple<string, double>>();
        }

        public void AddSample(string triggerId, double value) {
            SampleSets.Add(new Tuple<string, double>(triggerId, value));
        }

        protected override Packet[] Fetch() {
            var output = SampleSets.
                Select(sample => new Packet(sample.Item1, Control.Signal, new Sample(sample.Item2), 0)).
                ToArray();
            SampleSets.Clear();

            return output;
        }
    }

    internal class AccumulatingTestNode : AudioNode {
        public AccumulatingTestNode(short id) : base(id) { }

        protected override Packet Update(Packet packet) {
            var value = Get<double>("value", 0d);
            value += packet.Sample.L;
            Save("value", value);
            packet.Sample = new Sample(value);
            return packet;
        }
    }

    public class TestAudioNode {
        public TestAudioNode() {
            var type = typeof(AudioNode);
            var field = type.GetField("Nodes", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            var nodes = field.GetValue(null) as IDictionary<short, AudioNode>;
            nodes.Clear();
        }

        [Fact]
        public void TestMultipleTriggersFromSingleInput() {
            var g = new TestGenerator(0);
            var testNode = new AccumulatingTestNode(1);
            testNode.Connect(g);

            g.AddSample("a", 1);
            g.AddSample("b", 2);

            var output = testNode.Next();

            output.Length.Should().Be(2);
            output[0].Sample.L.Should().Be(1);
            output[1].Sample.L.Should().Be(2);

            g.AddSample("a", 1);
            g.AddSample("b", 2);

            output = testNode.Next();
            output.Length.Should().Be(2);
            output[0].Sample.L.Should().Be(2);
            output[1].Sample.L.Should().Be(4);
        }

        [Fact]
        public void TestSingleTriggerFromMultipleInputs() {
            var g1 = new TestGenerator(0);
            var g2 = new TestGenerator(1);
            var testNode = new AccumulatingTestNode(2);
            testNode.Connect(g1);
            testNode.Connect(g2);

            g1.AddSample("a", 1);
            g2.AddSample("a", 1);

            var output = testNode.Next();

            output.Length.Should().Be(1);
            output[0].Sample.L.Should().Be(2);
        }
    }
}
