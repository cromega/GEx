using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chirpesizer;
using Chirpesizer.Effects;
using Xunit;

namespace Test {
    public class TestInstrument {
        [Fact]
        public void TestActivateWithVibratoReturnsTriggerWithLFOModulatedFrequency() {
            var effects = new List<IEffect>() { new Vibrato(OscillatorType.Sine, 10, 50) };
            var instrument = new Instrument(OscillatorType.Sine, new StaticValue(0.5), new Envelope(1, 2, 0.1, 3), effects);
            var trigger = instrument.Activate(440, 100);
            Assert.IsType<ModulatedValue>(trigger.Frequency);
            var frequency = (ModulatedValue)trigger.Frequency;
            Assert.NotNull(frequency.Osc);
            Assert.Null(frequency.Envelope);
        }

        [Fact]
        public void TestActivateWithPitchEnvelopeReturnsTriggerWithEnvelopeModulatedFrequency() {
            var effects = new List<IEffect>() { new PitchEnvelope(1, 2, 0.5, 3) };
            var instrument = new Instrument(OscillatorType.Sine, new StaticValue(0.5), new Envelope(1, 2, 0.1, 3), effects);
            var trigger = instrument.Activate(440, 100);
            Assert.IsType<ModulatedValue>(trigger.Frequency);
            var frequency = (ModulatedValue)trigger.Frequency;
            Assert.Null(frequency.Osc);
            Assert.NotNull(frequency.Envelope);
        }

        [Fact]
        public void TestActivateWithoutVibratoOrPitchEnvelopeReturnsTriggerWithStaticFrequency() {
            var effects = new List<IEffect>();
            var instrument = new Instrument(OscillatorType.Sine, new StaticValue(0.5), new Envelope(1, 2, 0.1, 3), effects);
            var trigger = instrument.Activate(440, 100);
            var frequency = (StaticValue)trigger.Frequency;
        }

    }
}
