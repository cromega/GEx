using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphExperiment {
    public class Generator : AudioNode {
        public SignalType SignalType;

        public Generator() : base() { }

        protected override Packet Update(Packet packet) {
            var osc = Get<Oscillator>("Oscillator") ?? new Oscillator();

            osc.SetFrequency(packet.Sample.L);
            packet.Sample = new Sample(osc.Next(SignalType));
            Save("Oscillator", osc);
            return packet;
        }

        public static Generator Parse(string data) {
            return new Generator() {
                SignalType = (SignalType)int.Parse(data)
            };
        }
    }
}
