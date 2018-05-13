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

        public SoundSystem() {
            semaphore = new ManualResetEvent(initialState: true);
            XAudio = new XAudio2();
            masteringVoice = new MasteringVoice(XAudio, inputChannels: 2, inputSampleRate: 44100);
            var wf = new SharpDX.Multimedia.WaveFormat(44100, 16, 2);
            sourceVoice = new SourceVoice(XAudio, wf);
            sourceVoice.BufferStart += (_) => { Logger.Log("buffer started"); };
            sourceVoice.BufferEnd += (_) => {
                semaphore.Set();
                Logger.Log("buffer finished");
            };

            Buffers = new BufferRing(2, 4410);
        }

        public void AddBuffers(List<double[]> buffers) {
            var buffer = Buffers.Next();
            var mixedBuffer = new double[8820];
            new Mixer().Mix(mixedBuffer, buffers);
            new Normalizer().Normalize(mixedBuffer, buffer.Memory);
            WaitForBuffer();
            Write(buffer);
        }

        public void Write(ChirpBuffer buffer) {
            var ab = new AudioBuffer(buffer.Pointer);
            sourceVoice.SubmitSourceBuffer(ab, null);
            sourceVoice.Start();
            Logger.Log("buffer {0} written", buffer.Pointer.Pointer);
        }

        public void WaitForBuffer() {
            semaphore.WaitOne();
            semaphore.Reset();
        }
    }
}