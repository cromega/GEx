using System;
using Xunit;
using FluentAssertions;
using GEx;

namespace TestGEx {
    public class TestSample {
        [Fact]
        public void TestSampleMultiplicationByDouble() {
            Sample sample;
            sample = new Sample(5) * 5;
            (sample.L).Should().Be(25);
            (sample.R).Should().Be(25);

            sample = new Sample(5) * 0.5;
            (sample.L).Should().Be(2.5);
            (sample.R).Should().Be(2.5);

            sample = new Sample(5);
            sample *= 2;
            (sample.L).Should().Be(10);
            (sample.R).Should().Be(10);
        }

        [Fact]
        public void TestSampleAdditionByDouble() {
            Sample sample;
            sample = new Sample(5) + 5;
            (sample.L).Should().Be(10);
            (sample.R).Should().Be(10);

            sample = new Sample(5);
            sample += -2;
            (sample.L).Should().Be(3);
            (sample.R).Should().Be(3);

            sample = new Sample(-5);
            sample += 2;
            (sample.L).Should().Be(-7);
            (sample.R).Should().Be(-7);
        }

        [Fact]
        public void TestSampleAdditionWithSample() {
            var s1 = new Sample(1);
            var s2 = new Sample(2);
            var sum = s1 + s2;

            sum.L.Should().Be(3);
            sum.R.Should().Be(3);
        }
    }
}
