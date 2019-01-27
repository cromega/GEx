using System;
using GraphExperiment;

using Xunit;
using FluentAssertions;

namespace TestGEx {
    public class TestParser {
        [Fact]
        public void ParsesNodeGraph() {
            var p = new Parser();
            var data = "0trigger:>1;1generator:0>-";
            var machine = p.ParseMachine(data);
            machine.Nodes.Should().HaveCount(2);
        }
    }
}
