using System;
using Chirpesizer;
using Xunit;

namespace Test {
    public class TestIntrumentParser {
        [Fact]
        public void TestParseInstrument() {
            var instrumentData = "1;0.5:e10,20,0.5,30;01,10,0.2";
            var instrument = InstrumentParser.Parse(instrumentData);
            Assert.Equal(OscillatorType.Sine, instrument.)
        }
    }
}
