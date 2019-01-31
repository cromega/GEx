using System;
using GraphExperiment;

using Xunit;
using FluentAssertions;

namespace TestGEx {
    public class TestMuxer {
        [Fact]
        public void TestMuxerMuxesPackets() {
            var muxer = new Muxer();
            muxer.Add(new Sample(2));
            muxer.Add(new Sample(4));

            var muxed = muxer.Mux();
            muxed.L.Should().Be(3);
            muxed.R.Should().Be(3);
        }

        [Fact]
        public void TestMuxerResetsafterMuxing() {
            var muxer = new Muxer();
            muxer.Add(new Sample(2));
            muxer.Mux();
            muxer.Add(new Sample(2));
            muxer.Add(new Sample(4));

            var muxed = muxer.Mux();
            muxed.L.Should().Be(3);
            muxed.R.Should().Be(3);
        }
    }
}
