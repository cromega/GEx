using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GraphExperiment {
    class Buffer {
        public readonly short[] Memory;
        public readonly IntPtr Pointer;
        public int LengthInBytes {
            get { return Memory.Length * sizeof(short); }
        }

        public Buffer(int frames) {
            Memory = new short[frames * 2];
            Pointer = GCHandle.Alloc(Memory, GCHandleType.Pinned).AddrOfPinnedObject();
        }
    }
}
