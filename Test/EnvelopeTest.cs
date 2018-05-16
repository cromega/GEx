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
            var e = new Envelope(20, 20, 0.25, 20);
            e.Modulate(buf1, trigger);
            e.Modulate(buf2, trigger);

            for (int i=0; i<buf1.Length; i++) { buf1[i] = Math.Round(buf1[i], 2); }
            for (int i=0; i<buf2.Length; i++) { buf2[i] = Math.Round(buf2[i], 2); }

            Assert.Equal(new double[100] {
                // attack
                0, 0, 5, 5, 10, 10, 15, 15, 20, 20, 25, 25, 30, 30, 35, 35, 40, 40, 45, 45,
                50, 50, 55, 55, 60, 60, 65, 65, 70, 70, 75, 75, 80, 80, 85, 85, 90, 90, 95, 95,
                // decay
                100.0, 100.0, 96.25, 96.25, 92.5, 92.5, 88.75, 88.75, 85.0, 85.0, 81.25, 81.25, 77.5, 77.5, 73.75, 73.75, 70.0, 70.0, 66.25, 66.25,
                62.5, 62.5, 58.75, 58.75, 55.0, 55.0, 51.25, 51.25, 47.5, 47.5, 43.75, 43.75, 40.0, 40.0, 36.25, 36.25, 32.5, 32.5, 28.75, 28.75,
                //sustain
                25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25
            }, buf1);

            Assert.Equal(new double[100] {
                //release
                25.0, 25.0, 23.75, 23.75, 22.5, 22.5, 21.25, 21.25, 20.0, 20.0, 18.75, 18.75, 17.5, 17.5, 16.25, 16.25, 15.0, 15.0, 13.75, 13.75,
                12.5, 12.5, 11.25, 11.25, 10.0, 10.0, 8.75, 8.75, 7.5, 7.5, 6.25, 6.25, 5.0, 5.0, 3.75, 3.75, 2.5, 2.5, 1.25, 1.25,
                //silence
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
            }, buf2);

            Assert.True(trigger.Ended, "The trigger should have been marked as ended");
        }
    }
}
