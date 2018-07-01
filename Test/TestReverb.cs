using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chirpesizer.Effects;
using Xunit;

namespace Test {
    public class TestReverb {
        [Fact]
        public void TestReverEffect() {
            var data = new double[] { 10, 20, 30, 10, 20, 30, 10, 20, 30 };
            var reverb = new Reverb(5, 0.5);

            var buffer = data.Skip(0).Take(3).ToArray();
            reverb.Apply(buffer);
            Assert.Equal(new double[] { 10, 20, 30 }, buffer);

            buffer = data.Skip(3).Take(3).ToArray();
            reverb.Apply(buffer);
            Assert.Equal(new double[] { 10, 20, 35 }, buffer);

            buffer = data.Skip(6).Take(3).ToArray();
            reverb.Apply(buffer);
            Assert.Equal(new double[] { 20, 35, 35 }, buffer);
        }
    }
}
