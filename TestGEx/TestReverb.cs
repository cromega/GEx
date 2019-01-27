using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphExperiment;
using Xunit;
using FluentAssertions;
using TestGEx.Support;

namespace TestGEx {
    public class TestReverb {
        [Fact]
        public void TestReverbEffect() {
            var g = new SimpleGenerator();

            var effect = new Reverb();
            effect.Delay = 2;
            effect.Decay = 0.5;

            effect.Connect(g);

            Packet[] packets;

            g.AddSample("t", 1);
            packets = effect.Next(1);
            packets[0].Sample.L.Should().Be(1);

            g.AddSample("t", 0);
            packets = effect.Next(2);
            packets[0].Sample.L.Should().Be(0);

            g.AddSample("t", 0);
            packets = effect.Next(3);
            packets[0].Sample.L.Should().Be(0.5);

            g.AddSample("t", 0);
            packets = effect.Next(4);
            packets[0].Sample.L.Should().Be(0);

            g.AddSample("t", 0);
            packets = effect.Next(5);
            packets[0].Sample.L.Should().Be(0.25);

            g.AddSample("t", 0);
            packets = effect.Next(6);
            packets[0].Sample.L.Should().Be(0);
        }
    }
}
