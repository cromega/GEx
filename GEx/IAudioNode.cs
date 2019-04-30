using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphExperiment {
    public interface IAudioNode {
        void Connect(IAudioNode other);
        Packet[] Fetch();
        Packet[] Next();
    }
}
