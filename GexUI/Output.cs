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

        public Output(short id) : base(id) {
            Audio = new SoundSystem(2205); ;
            Task.Run(() => Run());
        }

        public void Run() {
            for (; ; ) {
                //FIXME
                if (Previous == null) { Thread.Sleep(1); continue; }

                var buffer = new short[Audio.Frames * 2];

                Packet packet = Packet.Empty();
                for (int i = 0; i < buffer.Length; i += 2) {
                    var packets = Previous.Next();
                    if (packets.Length == 0) { break; }

                    packet = packets[0];
                    buffer[i] = (short)(packet.Sample.L);
                    buffer[i + 1] = (short)(packet.Sample.R);
                }

                if (packet.Control == Control.End) {
                    TriggerEnded(this, new TriggerEndedEventArgs(packet.TriggerID));
                }

                Audio.Write(buffer);
            }
        }
    }
}
