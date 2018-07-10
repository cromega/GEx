using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chirpesizer {
    public class PatchableValue {
        private double Value;
        private List<IModulator> Modulators;
        public readonly string Id;

        public PatchableValue(double initialValue, string id) {
            Value = initialValue;
            Id = id;
            Modulators = new List<IModulator>();
        }

        public void AddModulator(IModulator modulator) {
            Modulators.Add(modulator);
        }

        public double Get(int time, bool isActive, int songTime) {
            var value = Value;
            Modulators.ForEach(mod => {
                value = mod.Get(value, time, isActive, songTime);
            });
            return value;
        }

        public int FadeoutTime() {
            var releaseTimes = Modulators.Where(mod => mod.GetType().Name == "EnvelopeModulator").
                Select(mod => ((EnvelopeModulator)mod).Envelope.Release);

            if (releaseTimes.Any()) { return releaseTimes.Max(); }
            return 0;
        }
    }
}
