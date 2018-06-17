using System.Collections.Generic;
using Chirpesizer;
using Chirpotle;
using Xunit;

namespace TestChirpotle {
    public class TestInstrumentSerializer {
        [Fact]
        public void TestInstrumentSerializerWithModulators() {
            var modulators = new List<IModulator>() {
                { new EnvelopeModulator(new Envelope(10, 20, 0.5, 30), "v") }, //main envelope
                { new LFOModulator(OscillatorType.Triangle, 10, 40, "x") }, //frequency LFO
            };
            var instrument = new Instrument(OscillatorType.Square, 0.2, modulators);
            var serializer = new InstrumentSerializer(instrument);
            Assert.Equal("2;pv0.2;mev10,20,0.5,30;mlx4,10,40", serializer.Serialize());
        }
    }
}
