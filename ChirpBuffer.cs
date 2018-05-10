using System;
using System.Runtime.InteropServices;
using SharpDX;

namespace chirpcore {
    public class ChirpBuffer {
        public short[] Memory;
        public DataPointer Pointer;

        public ChirpBuffer(int size) {
            Memory = new short[size];
            var ptr = GCHandle.Alloc(Memory, GCHandleType.Pinned).AddrOfPinnedObject();
            Pointer = new DataPointer(ptr, Memory.Length * sizeof(short));
        }
    }
}