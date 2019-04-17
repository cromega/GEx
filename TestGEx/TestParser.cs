using System;
using GraphExperiment;

using Xunit;
using FluentAssertions;

namespace TestGEx {
    public class TestParser {
        [Fact]
        public void ParsesNodeGraph() {
            var p = new Parser();
            var data = ">0; 0Hydra:0,2>1; 1Envelope:5,5,0.5,5>-";
            var machine = p.ParseMachine(data);
            machine.Receivers.Should().HaveCount(1);
            machine.Outputs.Should().HaveCount(1);
        }

        [Fact]
        public void ParseNodeGraphWithMultipleReceivers() {
            var p = new Parser();
            var data = ">0,1; 0Generator:0>-; 1Generator:1>-"; 
            var machine = p.ParseMachine(data);
            machine.Receivers.Should().HaveCount(2);
            machine.Outputs.Should().HaveCount(2);
        }
    }
}
