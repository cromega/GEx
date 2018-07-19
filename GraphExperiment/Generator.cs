using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphExperiment {
    public enum SignalType : int {
        Sine = 0,
        Square = 1,
        Noise = 2,
    }

    [AudioNode]
    public class Generator : AudioNode {
        private bool Released;
        public double Frequency;
        [AudioNodeParameter]
        public SignalType SignalType;

        //public Source(BlockingCollection<Sample> output, SignalType st) {
        //    Output = output;
        //    Released = false;
        //    SignalType = st;
        //}

        public Generator(int id, SignalType signalType, Wire connection) : base(id, connection) {
            SignalType = signalType;
        }

        public void Trigger() {
            var t = 0;
            Task.Run(() => {
                for (; !Released; t++) {
                    switch (SignalType) {
                        case SignalType.Sine: Send(new Packet(Control.Signal, new Sample(Sine(t)))); break;
                        case SignalType.Square: Send(new Packet(Control.Signal, new Sample(Sine(t) > 0 ? 1 : -1))); break;
                    }
                }

                Send(new Packet(Control.End, new Sample(0)));
            });
        }

        public void Release() {
            Released = true;
        }

        private double Sine(int t) {
            return Math.Sin(Frequency * Math.PI * 2 * t / 44100);
        }
    }
}
