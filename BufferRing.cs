using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace chirpcore {
    public class BufferRing {
        private ChirpBuffer[] Buffers;
        private int index;
        public BufferRing(int numBuffers, int samples) {
            Buffers = new ChirpBuffer[numBuffers];
            for (int i=0; i<2; i++) {
                Buffers[i] = new ChirpBuffer(samples);
            }
        }

        public ChirpBuffer Next() {
            return Buffers[index++ % Buffers.Length];
        }
    }
}