using System;
using System.Runtime.InteropServices;
using SharpDX;

namespace Chirpesizer {
    public class ChirpBuffer {
        public readonly short[] Memory;
        public readonly DataPointer Pointer;

        public ChirpBuffer(int size) {
            Memory = new short[size];
            var ptr = GCHandle.Alloc(Memory, GCHandleType.Pinned).AddrOfPinnedObject();
            Pointer = new DataPointer(ptr, Memory.Length * sizeof(short));
        }
    }
}