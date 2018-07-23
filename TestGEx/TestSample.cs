using System;
using Xunit;
using FluentAssertions;
using GraphExperiment;

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
            sample += 2;
            (sample.L).Should().Be(7);
            (sample.R).Should().Be(7);
        }
    }
}
