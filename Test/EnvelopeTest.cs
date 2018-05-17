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
            var e = new Envelope(10, 20, 0.25, 30);
            e.Modulate(buf1, trigger);
            e.Modulate(buf2, trigger);

            for (int i=0; i<buf1.Length; i++) { buf1[i] = Math.Round(buf1[i], 2); }
            for (int i=0; i<buf2.Length; i++) { buf2[i] = Math.Round(buf2[i], 2); }

            Assert.Equal(new double[100] {
                // attack
                0, 0, 10, 10, 20, 20, 30, 30, 40, 40, 50, 50, 60, 60, 70, 70, 80, 80, 90, 90,
                // decay
                100.0, 100.0, 96.25, 96.25, 92.5, 92.5, 88.75, 88.75, 85.0, 85.0, 81.25, 81.25, 77.5, 77.5, 73.75, 73.75, 70.0, 70.0, 66.25, 66.25,
                62.5, 62.5, 58.75, 58.75, 55.0, 55.0, 51.25, 51.25, 47.5, 47.5, 43.75, 43.75, 40.0, 40.0, 36.25, 36.25, 32.5, 32.5, 28.75, 28.75,
                //sustain
                25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25,
                25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25
            }, buf1);

            Assert.Equal(new double[100] {
                //release
                25.00, 25.00, 24.17, 24.17, 23.33, 23.33, 22.50, 22.50, 21.67, 21.67, 20.83, 20.83, 20.00, 20.00, 19.17, 19.17, 18.33, 18.33, 17.50, 17.50,
                16.67, 16.67, 15.83, 15.83, 15.00, 15.00, 14.17, 14.17, 13.33, 13.33, 12.50, 12.50, 11.67, 11.67, 10.83, 10.83, 10.00, 10.00, 9.17, 9.17,
                8.33, 8.33, 7.50, 7.50, 6.67, 6.67, 5.83, 5.83, 5.00, 5.00, 4.17, 4.17, 3.33, 3.33, 2.50, 2.50, 1.67, 1.67, 0.83, 0.83,
                //silence
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
            }, buf2);

            Assert.True(trigger.Ended, "The trigger should have been marked as ended");
        }
    }
}
