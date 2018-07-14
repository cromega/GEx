using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphExperiment {
    public class Wire : BlockingCollection<Packet> {
        public Wire(int frames) : base(frames) { }
    }
}
