using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphExperiment{
    [AudioNode(Direction = AudioNodeDirection.InputOutput)]
    public class Reverb : AudioNode {
        [AudioNodeParameter]
        public int Delay;

        [AudioNodeParameter]
        public double Decay;

        public Reverb(short id) : base(id) {
        }

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
