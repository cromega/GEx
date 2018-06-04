using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chirpesizer;
using Xunit;

namespace Test {
    class HalvingModulator : IModulator {
        public double Get(int time, bool isActive) {
            return 0.5;
        }

        public string GetTarget() { return "test"; }
    }

    public class TestPatchableValue {
        [Fact]
        public void TestPatchableValueWithTestModulators() {
            var testValue = new PatchableValue(1, "a");
            testValue.AddModulator(new HalvingModulator());
            testValue.AddModulator(new HalvingModulator());
            Assert.Equal(0.25, testValue.Get(0, true));
        }
    }
}
