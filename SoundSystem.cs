using System;
using SharpDX;
using SharpDX.XAudio2;
using SharpDX.Multimedia;
using System.Runtime.InteropServices;
using System.Threading;
using System.Collections.Generic;


namespace chirpcore {
    public class SoundSystem {
        private XAudio2 XAudio;
        private MasteringVoice masteringVoice;
        private SourceVoice sourceVoice;
        private BufferRing Buffers;
        private ManualResetEvent semaphore;
        private int buffersSubmmitted;

        public SoundSystem() {
            semaphore = new ManualResetEvent(initialState: true);
            XAudio = new XAudio2();
            masteringVoice = new MasteringVoice(XAudio, inputChannels: 2, inputSampleRate: 44100);
            var wf = new SharpDX.Multimedia.WaveFormat(44100, 16, 2);
            sourceVoice = new SourceVoice(XAudio, wf);
            sourceVoice.BufferStart += (ptr) => { Logger.Log("buffer {0} started", ptr); };
            sourceVoice.BufferEnd += (ptr) => {
                semaphore.Set();
                Logger.Log("buffer {0} finished", ptr);
            };

            Buffers = new BufferRing(2, 4410);
            buffersSubmmitted = 0;
        }

        public void AddBuffers(List<double[]> buffers) {
            var mixedBuffer = new double[8820];
            new Mixer().Mix(mixedBuffer, buffers);
            WaitForBuffer();
            var buffer = Buffers.Next();
            new Normalizer().Normalize(mixedBuffer, buffer.Memory);
            Write(buffer);
        }

        public void Write(ChirpBuffer buffer) {
            var ab = new AudioBuffer(buffer.Pointer) { 
                Flags = BufferFlags.None,
                Context = buffer.Pointer.Pointer
            };
            sourceVoice.SubmitSourceBuffer(ab, null);
            buffersSubmmitted++;
            sourceVoice.Start();
            Logger.Log("buffer {0} written", buffer.Pointer.Pointer);
        }

        public void WaitForBuffer() {
            // skip waiting after the first buffer to preload the second one
            if (buffersSubmmitted == 1) { return; }

            semaphore.WaitOne();
            semaphore.Reset();
        }
    }
}