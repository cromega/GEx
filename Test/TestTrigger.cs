using System;
using Chirpesizer;
using Xunit;

namespace Test {
    public class TestTrigger {

        [Fact]
        public void WhenActive() {
            var t = new Trigger(new Oscillator(OscillatorType.Noise), new StaticValue(440), 10);
            t.Tick();
            Assert.Equal(1, t.Age);
            Assert.Equal(9, t.TTL);
        }

        [Fact]
        public void WhenEnds() {
            var t = new Trigger(new Oscillator(OscillatorType.Noise), new StaticValue(440), 2);
            t.Tick();
            Assert.Equal(1, t.Age);
            Assert.Equal(1, t.TTL);
            Assert.True(t.IsActive);

            t.Tick();
            Assert.Equal(0, t.Age);
            Assert.Equal(0, t.TTL);
            Assert.False(t.IsActive);

            t.Tick();
            Assert.Equal(1, t.Age);
            Assert.Equal(0, t.TTL);
            Assert.False(t.IsActive);

            t.Tick();
            Assert.Equal(2, t.Age);
            Assert.Equal(0, t.TTL);
            Assert.False(t.IsActive);
        }
    }
}
