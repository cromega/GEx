using System;
using Chirpesizer;
using Xunit;

namespace Test {
    public class TestValueParser {
        [Fact]
        public void TestParseStaticValue() {
            var valueData = "440";
            var value = ValueParser.Parse(valueData);
            Assert.IsType<StaticValue>(value);
            Assert.Equal(440, value.Get(123, true));
        }

        [Fact]
        public void TestParseLFOModulatedValue() {
            var valueData = "440:l1,10,50";
            var v = ValueParser.Parse(valueData);
            Assert.IsType<ModulatedValue>(v);
            var value = (ModulatedValue)v;

            Assert.Equal(440, value.Value);
            Assert.Equal(10, value.Frequency);
            Assert.Equal(50, value.Height);
            Assert.NotNull(value.Osc);
        }

        [Fact]
        public void TestParseEnvelopeModulatedValue() {
            var valueData = "440:e10,20,0.2,30";
            var v = ValueParser.Parse(valueData);
            Assert.IsType<ModulatedValue>(v);
            var value = (ModulatedValue)v;

            Assert.Equal(440, value.Value);
            Assert.NotNull(value.Envelope);
            // envelope values get converted to frames
            Assert.Equal(441, value.Envelope.Attack);
            Assert.Equal(882, value.Envelope.Decay);
            Assert.Equal(0.2, value.Envelope.Sustain);
            Assert.Equal(1323, value.Envelope.Release);
        }
    }
}
