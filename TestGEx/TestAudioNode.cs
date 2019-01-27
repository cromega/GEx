using System;
using System.Collections.Generic;
using System.Linq;
using GraphExperiment;
using TestGEx.Support;

using Xunit;
using FluentAssertions;

namespace TestGEx {
    // a node that sends a set of preloaded samples to the downstream node

    internal class AccumulatingTestNode : AudioNode {
        public AccumulatingTestNode() : base() { }

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
            var g = new SimpleGenerator();
            var testNode = new AccumulatingTestNode();
            testNode.Connect(g);

            g.AddSample("a", 1);
            g.AddSample("b", 2);

            var output = testNode.Next(0);

            output.Length.Should().Be(2);
            output[0].Sample.L.Should().Be(1);
            output[1].Sample.L.Should().Be(2);

            g.AddSample("a", 1);
            g.AddSample("b", 2);

            output = testNode.Next(1);
            output.Length.Should().Be(2);
            output[0].Sample.L.Should().Be(2);
            output[1].Sample.L.Should().Be(4);
        }

        [Fact]
        public void TestSingleTriggerFromMultipleInputs() {
            var g1 = new SimpleGenerator();
            var g2 = new SimpleGenerator();
            var testNode = new AccumulatingTestNode();
            testNode.Connect(g1);
            testNode.Connect(g2);

            g1.AddSample("a", 1);
            g2.AddSample("a", 2);

            var output = testNode.Next(0);

            output.Length.Should().Be(1);
            output[0].Sample.L.Should().Be(3);
        }

        [Fact]
        public void TestMultipleInputs() {
            var g = new SimpleGenerator();
            g.AddSample("t", 1);
            var testNode1 = new AccumulatingTestNode();
            var testNode2 = new AccumulatingTestNode();
            var testNode3 = new AccumulatingTestNode();

            testNode3.Connect(testNode2);
            testNode3.Connect(testNode1);
            testNode2.Connect(g);
            testNode1.Connect(g);

            var output = testNode3.Next(9001);
            output[0].Sample.L.Should().Be(2);
        }
    }
}
