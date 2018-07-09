using System.Linq;
using Chirpesizer.Effects;
using Xunit;
using FluentAssertions;

namespace Test {
    public class TestReverb {
        [Fact]
        public void TestFeedback() {
            var data = new double[] { 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            var reverb = new Reverb(2, 0.5);
            var output = reverb.Apply(data);
            output.Should().Equal(20, 20, 0, 0, 10, 10, 0, 0, 5, 5, 0, 0);
        }

        [Fact]
        public void TestFeedbackWithMultipleChunks() {
            var data = new double[] { 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            var reverb = new Reverb(2, 0.5);
            var chunk = reverb.Apply(data.Take(4).ToArray());
            chunk.Should().Equal(20, 20, 0, 0);

            chunk = reverb.Apply(data.Skip(4).Take(4).ToArray());
            chunk.Should().Equal(10, 10, 0, 0);

            chunk = reverb.Apply(data.Skip(8).Take(4).ToArray());
            chunk.Should().Equal(5, 5, 0, 0);
        }

    }
}
