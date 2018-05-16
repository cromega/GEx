using System;
using System.Linq;
using Xunit;
using chirpcore;

namespace Test
{
    public class EnvelopeTest
    {
        [Fact]
        public void TheEnvelopeModulatesTheShit()
        {
            Console.WriteLine("TheEnvelopeModulatesTheShit");
            
            var buf1 = new double[100];
            for (int i=0; i<buf1.Length; i++) { buf1[i] = 100; }
            var buf2 = new double[100];
            for (int i=0; i<buf2.Length; i++) { buf2[i] = 100; }
            var trigger = new Trigger(440, 50);
            var e = new Envelope(20, 20, 0.5, 20);
            e.Modulate(buf1, trigger);
            e.Modulate(buf2, trigger);

            for (int i=0; i<buf1.Length; i++) { buf1[i] = Math.Round(buf1[i], 2); }
            for (int i=0; i<buf2.Length; i++) { buf2[i] = Math.Round(buf2[i], 2); }

            Assert.Equal(new double[100] {
                // attack
                0, 0, 5, 5, 10, 10, 15, 15, 20, 20, 25, 25, 30, 30, 35, 35, 40, 40, 45, 45,
                50, 50, 55, 55, 60, 60, 65, 65, 70, 70, 75, 75, 80, 80, 85, 85, 90, 90, 95, 95,
                // decay
                100, 100, 97.5, 97.5, 95, 95, 92.5, 92.5, 90, 90, 87.5, 87.5, 85, 85, 82.5, 82.5, 80, 80, 77.5, 77.5,
                75, 75, 72.5, 72.5, 70, 70, 67.5, 67.5, 65, 65, 62.5, 62.5, 60, 60, 57.5, 57.5, 55, 55, 52.5, 52.5,
                //sustain
                50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50
            }, buf1);

            Assert.Equal(new double[100] {
                //release
                50, 50, 47.5, 47.5, 45, 45, 42.5, 42.5, 40, 40, 37.5, 37.5, 35, 35, 32.5, 32.5, 30, 30, 27.5, 27.5,
                25, 25, 22.5, 22.5, 20, 20, 17.5, 17.5, 15, 15, 12.5, 12.5, 10, 10, 7.5, 7.5, 5, 5, 2.5, 2.5,
                //silence
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
            }, buf2);

            Assert.True(trigger.Ended, "The trigger should have been marked as ended");
        }
    }
}
