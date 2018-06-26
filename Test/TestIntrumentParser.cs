using System;
using System.Collections.Generic;
using System.Reflection;
using Chirpesizer;
using Xunit;

namespace Test {
    public class TestIntrumentParser {
        [Fact]
        public void TestParseWithVolumeAndEnvelope() {
            var instrumentData = "1;pv0.5;mev10,20,0.5,30";
            var instrument = InstrumentParser.Parse(instrumentData);
            Assert.Equal(OscillatorType.Sine, instrument.Osc);
            Assert.Equal(0.5, instrument.Volume);
            var envelope = (EnvelopeModulator)instrument.Modulators[0];
            Assert.Equal(441, envelope.Envelope.Attack); // value converted from ms to frames
        }

        [Fact]
        public void TestParseWithVolumeAndLFO() {
            var instrumentData = "1;pv0.5;mlv1,10,0.2";
            var instrument = InstrumentParser.Parse(instrumentData);
            Assert.Equal(OscillatorType.Sine, instrument.Osc);
            Assert.Equal(0.5, instrument.Volume);
            var lfo = (LFOModulator)instrument.Modulators[0];
            Assert.Equal(OscillatorType.Sine, lfo.Oscillator.OscillatorType);
            Assert.Equal(0.2, lfo.Amplitude);
        }

        [Fact]
        public void TestParseWithEnvelopeAndLFO() {
            var instrumentData = "1;pv0.5;mev10,20,0.5,30;mlv1,10,0.5";
            var instrument = InstrumentParser.Parse(instrumentData);
            Assert.Equal(OscillatorType.Sine, instrument.Osc);
            Assert.Equal(0.5, instrument.Volume);
            Assert.Equal(2, instrument.Modulators.Count);
        }

        //[Fact]
        //public void TestParseInstrumentWithVolumeEnvelope() {
        //    var instrumentData = "1;0.5;10,20,0.5,30";
        //    var instrument = InstrumentParser.Parse(instrumentData);
        //    Assert.Equal(OscillatorType.Sine, instrument.Osc);
        //    Assert.IsType<StaticValue>(instrument.Volume);
        //    Assert.Equal(441, instrument.Envelope.Attack); //converted to frames
        //}

        //[Fact]
        //public void TestParseInstrumentWithTremolo() {
        //    var instrumentData = "1;0.5:l1,10,0.5;10,20,0.5,30";
        //    var instrument = InstrumentParser.Parse(instrumentData);
        //    Assert.Equal(OscillatorType.Sine, instrument.Osc);
        //    Assert.IsType<ModulatedValue>(instrument.Volume);
        //}

        //[Fact]
        //public void TestParseInstrumentWithVibrato() {
        //    var instrumentData = "1;0.5:l1,10,0.5;10,20,0.5,30;11,10,50";
        //    var instrument = InstrumentParser.Parse(instrumentData);
        //    Assert.IsType<Vibrato>(instrument.Effects[0]);
        //}

        //[Fact]
        //public void TestParseInstrumentWithPitchEnvelope() {
        //    var instrumentData = "1;0.5:l1,10,0.5;10,20,0.5,30;210,20,0.5,30";
        //    var instrument = InstrumentParser.Parse(instrumentData);
        //    Assert.IsType<PitchEnvelope>(instrument.Effects[0]);

        //}

        //[Fact]
        //public void TestParseInstrumentThrowsErrorIfVibratoAndPitchEnvelopeAreDefined() {
        //    var instrumentData = "1;0.5:l1,10,0.5;10,20,0.5,30;11,10,50;210,20,0.5,30";
        //    var ex = Assert.Throws<Exception>(() => {
        //        InstrumentParser.Parse(instrumentData);
        //    });
        //    Assert.Equal("Can't have vibrato and pitch envelope at the same time", ex.Message);
        //}
    }
}
