using System;
using SharpDX.XAudio2;
using SharpDX.Multimedia;
using System.Threading;


namespace Chirpesizer {
    public class SoundSystem {
        private XAudio2 XAudio;
        private MasteringVoice masteringVoice;
        private SourceVoice sourceVoice;
        private BufferRing Buffers;
        private ManualResetEvent semaphore;
        private int buffersSubmmitted;
        private int framesInBuffer;

        public SoundSystem(int frames) {
            semaphore = new ManualResetEvent(initialState: true);
            XAudio = new XAudio2();
            masteringVoice = new MasteringVoice(XAudio, inputChannels: 2, inputSampleRate: 44100);
            var wf = new WaveFormat(44100, 16, 2);
            sourceVoice = new SourceVoice(XAudio, wf);
            sourceVoice.BufferStart += (ptr) => { Logger.Log("buffer {0} started", ptr); };
            sourceVoice.BufferEnd += (ptr) => {
                semaphore.Set();
                Logger.Log("buffer {0} finished", ptr);
            };

            framesInBuffer = frames;
            Buffers = new BufferRing(2, framesInBuffer);
            buffersSubmmitted = 0;
        }

        public void Write(short[] outputData) {
            WaitForBuffer();
            var buffer = Buffers.Next();
            Buffer.BlockCopy(outputData, 0, buffer.Memory, 0, outputData.Length * 2);
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
            if (buffersSubmmitted < 1) { return; }

            semaphore.WaitOne();
            semaphore.Reset();
        }

        public void Close() {
            sourceVoice.Dispose();
            masteringVoice.Dispose();
            XAudio.Dispose();
        }
    }
}