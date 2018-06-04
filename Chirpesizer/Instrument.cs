using System;
using System.Collections.Generic;
using Chirpesizer.Effects;
using System.Linq;


namespace Chirpesizer {
    public class Instrument {
        public readonly OscillatorType Osc;
        public readonly double Volume;
        public readonly List<IModulator> Modulators;

        public Instrument(OscillatorType osc, double volume, List<IModulator>modulators) {
            Osc = osc;
            Volume = volume;
            Modulators = modulators;
        }

        public Trigger Activate(double frequency, int length) {
            var osc = new Oscillator(Osc);
            var trig = new Trigger(osc, GetFrequency(frequency), length, GetVolume());
            return trig;
        }

        private PatchableValue GetVolume() {
            var value = new PatchableValue(Volume, "v");
            GetModulatorsFor(value).ForEach(mod => value.AddModulator(mod));
            return value;
        }

        private PatchableValue GetFrequency(double frequency) {
            var value = new PatchableValue(frequency, "x");
            GetModulatorsFor(value).ForEach(mod => value.AddModulator(mod));
            return value;
        }

        private List<IModulator> GetModulatorsFor(PatchableValue value) {
            return Modulators.Where(mod => mod.GetTarget() == value.Id).ToList();

        }
    }
}