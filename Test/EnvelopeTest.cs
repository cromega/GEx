using System;
using System.Linq;
using Xunit;
using chirpcore;

namespace Test2
{
    public class UnitTest1
    {
        [Fact]
        public void TestAttack()
        {
            var buf = new double[100];
            for (int i=0; i<buf.Length; i++) { buf[i] = 200; }
            var e = new chirpcore.Envelope(20, 20, 0.5, 20);
            e.Modulate(buf, new Trigger(440, 50));

            Assert.Equal(new double[20] { 0, 0, 5, 5, 10 ,10, 15, 15, 20, 20, 25, 25, 30, 30, 35, 35, 40, 40, 45, 45 }, buf.Take(40).ToArray());
        }
    }
}
