using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphExperiment{
    public class Reverb : AudioNode {
        public int Delay;
        public double Decay;

        protected override Packet Update(Packet packet) {
            var DelayBuffer = Get<Sample[]>("DelayBuffer") ?? new Sample[Delay];
            if (DelayBuffer.Length != Delay) { DelayBuffer = new Sample[Delay]; }
            var offset = Get<int>("Offset", 0);

            if (offset < Delay) {
                DelayBuffer[offset] = packet.Sample;
                Save("Offset", ++offset);
                Save("DelayBuffer", DelayBuffer);
                return packet;
            }

            packet.Sample += DelayBuffer[offset % Delay] * Decay;

            Array.Copy(DelayBuffer, 1, DelayBuffer, 0, DelayBuffer.Length - 1);
            DelayBuffer[DelayBuffer.Length - 1] = packet.Sample;

            Save("DelayBuffer", DelayBuffer);
            return packet;
        }
    }
}
