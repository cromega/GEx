using System;
using Chirpesizer;
using Xunit;

namespace Test {
    public class TestNode {
        [Fact]
        void TestNodeParsing() {
            var nodeData = "1;0.5;58"; // A4 note
            var node = Node.Parse(nodeData);
            Assert.Equal(1, node.InstrumentIndex);
            Assert.Equal(0.5, node.Length);
            Assert.InRange<double>(node.Frequency, 439.9, 440.1);
        }
    }
}
