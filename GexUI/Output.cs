using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GraphExperiment;

namespace GexUI {
    public class TriggerEndedEventArgs : EventArgs {
        public string TriggerID;
        public TriggerEndedEventArgs(string triggerId) {
            TriggerID = triggerId;
        }
    }

    [AudioNode(Direction = AudioNodeDirection.InputOnly)]
    class Output : GraphExperiment.AudioNode {
        private SoundSystem Audio;
        public event EventHandler<TriggerEndedEventArgs> TriggerEnded;
        private long Tick;

        public Output(short id) : base(id) {
            Audio = new SoundSystem(2205); ;
            Task.Run(() => Run());
            Tick = 0;
        }

        private void Run() {
            for (; ; ) {
                //FIXME
                if (Previous == null) { Thread.Sleep(1); continue; }

                var buffer = new short[Audio.Frames * 2];

                for (int i = 0; i < buffer.Length; i += 2) {
                    var packets = Previous.SelectMany(node => node.Next(Tick++)).ToArray();
                    if (packets.Length == 0) { break; }

                    double mixedL = 0d;
                    double mixedR = 0d;
                    foreach (var packet in packets) {
                        if (packet.Control == Control.End) {
                            TriggerEnded(this, new TriggerEndedEventArgs(packet.TriggerID));
                            continue;
                        }
                        mixedL += packet.Sample.L;
                        mixedR += packet.Sample.R;
                    }

                    buffer[i] = (short)mixedL;
                    buffer[i + 1] = (short)mixedR;
                }


                Audio.Write(buffer);
            }
        }
    }
}
