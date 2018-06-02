using System;
using Chirpesizer;
using Xunit;

namespace ChirpesizerTest {
    public class TestEnvelope {

        [Fact]
        public void WhenAttackIsZero() {
            var e = new Envelope(0, 5, 0.5, 0);
            Assert.Equal(1, e.Next(0, true));
            Assert.Equal(0.9, e.Next(1, true));
        }

        [Fact]
        public void WhenAttackAndDecayAreZero() {
            var e = new Envelope(0, 0, 0.5, 0);
            Assert.Equal(0.5, e.Next(0, true));
            Assert.Equal(0.5, e.Next(1, true));
        }

        [Fact]
        public void TestRelease() {
            var e = new Envelope(0, 10, 0.5, 5);
            Assert.Equal(0.4, e.Next(1, false));
            Assert.Equal(0.3, e.Next(2, false));
        }

        [Fact]
        public void WhenReleaseIsZero() {
            var e = new Envelope(5, 10, 0.5, 0);
            Assert.Equal(0, e.Next(1, false));
        }

        [Fact]
        public void WhenAgeIsHigherThanRelease() {
            var e = new Envelope(5, 10, 0.5, 20);
            Assert.Equal(0, e.Next(21, false));
        }
    }
}
