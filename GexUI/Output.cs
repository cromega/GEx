using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphExperiment;

namespace GexUI {
    [AudioNode(Direction = AudioNodeDirection.InputOnly)]
    class Output : GraphExperiment.AudioNode {
        public Output(short id) : base(id) { }

        protected override Packet Update(Packet packet) {
            return packet;
        }
    }
}
