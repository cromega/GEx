using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GexUI {
    class NodeIdGenerator {
        private int Last;

        public NodeIdGenerator() {
            Last = 0;
        }

        public short Next() {
            return (short)Last++;
        }
    }
}
