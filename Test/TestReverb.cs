using System.Linq;
using Chirpesizer.Effects;
using Xunit;
using FluentAssertions;

namespace Test {
    public class TestReverb {
        [Fact]
        public void TestEchoEffect() {
            var data = new double[] { 10, 20, 30, 10, 20, 30, 10, 20, 30 };
            var reverb = new Reverb(5, 0.5);

            var chunk = reverb.Apply(data.Take(3).ToArray());
            chunk.Should().Equal(10, 20, 30);

            chunk = reverb.Apply(data.Skip(3).Take(3).ToArray());
            chunk.Should().Equal(10, 20, 35);

            chunk = reverb.Apply(data.Skip(6).Take(3).ToArray());
            chunk.Should().Equal(20, 35, 35);
        }

        [Fact]
        public void TestReverbWithBufferLongerThanDelay() {
            var data = new double[] { 10, 20, 30, 10, 20, 30, 10, 20, 30 };
            var reverb = new Reverb(5, 0.5);
            var output = reverb.Apply(data);
            output.Should().Equal(10, 20, 30, 10, 20, 35, 20, 35, 35);
        }

        [Fact]
        public void TestFeedback() {
            var data = new double[] { 20, 0, 0, 0, 0, 0 };
            var reverb = new Reverb(2, 0.5);
            var output = reverb.Apply(data);
            output.Should().Equal(20, 0, 10, 0, 5, 0);
        }

        [Fact]
        public void TestFeedbackWithMultipleChunks() {
            var data = new double[] { 20, 0, 0, 0, 0, 0 };
            var reverb = new Reverb(2, 0.5);
            var chunk = reverb.Apply(data.Take(2).ToArray());
            chunk.Should().Equal(20, 0);

            chunk = reverb.Apply(data.Skip(2).Take(2).ToArray());
            chunk.Should().Equal(10, 0);

            chunk = reverb.Apply(data.Skip(4).Take(2).ToArray());
            chunk.Should().Equal(5, 0);
        }

    }
}
