using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Chirpesizer {
    public class BufferRing {
        private ChirpBuffer[] Buffers;
        private int index;
        public BufferRing(int numBuffers, int frames) {
            Buffers = new ChirpBuffer[numBuffers];
            for (int i=0; i<numBuffers; i++) {
                Buffers[i] = new ChirpBuffer(frames * 2);
            }
        }

        public ChirpBuffer Next() {
            return Buffers[index++ % Buffers.Length];
        }
    }
}