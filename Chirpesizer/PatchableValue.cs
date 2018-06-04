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

        public double Get(int time, bool isActive) {
            if (Modulators.Count == 0 && !isActive) { return 0; }

            var value = Value;
            Modulators.ForEach(mod => {
                value *= mod.Get(time, isActive);
            });
            return value;
        }
    }
}
