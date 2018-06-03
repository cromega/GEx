using System;
using Chirpesizer;
using Chirpesizer.Effects;
using Xunit;

namespace Test {
    public class TestIntrumentParser {
        [Fact]
        public void TestParseInstrumentWithVolumeEnvelope() {
            var instrumentData = "1;0.5;10,20,0.5,30";
            var instrument = InstrumentParser.Parse(instrumentData);
            Assert.Equal(OscillatorType.Sine, instrument.Osc);
            Assert.IsType<StaticValue>(instrument.Volume);
            Assert.Equal(441, instrument.Envelope.Attack); //converted to frames
        }

        [Fact]
        public void TestParseInstrumentWithTremolo() {
            var instrumentData = "1;0.5:l1,10,0.5;10,20,0.5,30";
            var instrument = InstrumentParser.Parse(instrumentData);
            Assert.Equal(OscillatorType.Sine, instrument.Osc);
            Assert.IsType<ModulatedValue>(instrument.Volume);
        }

        [Fact]
        public void TestParseInstrumentWithVibrato() {
            var instrumentData = "1;0.5:l1,10,0.5;10,20,0.5,30;11,10,50";
            var instrument = InstrumentParser.Parse(instrumentData);
            Assert.IsType<Vibrato>(instrument.Effects[0]);
        }

        [Fact]
        public void TestParseInstrumentWithPitchEnvelope() {
            var instrumentData = "1;0.5:l1,10,0.5;10,20,0.5,30;210,20,0.5,30";
            var instrument = InstrumentParser.Parse(instrumentData);
            Assert.IsType<PitchEnvelope>(instrument.Effects[0]);

        }

        [Fact]
        public void TestParseInstrumentThrowsErrorIfVibratoAndPitchEnvelopeAreDefined() {
            var instrumentData = "1;0.5:l1,10,0.5;10,20,0.5,30;11,10,50;210,20,0.5,30";
            var ex = Assert.Throws<Exception>(() => {
                InstrumentParser.Parse(instrumentData);
            });
            Assert.Equal("Can't have vibrato and pitch envelope at the same time", ex.Message);
        }
    }
}
