using System;
using SharpDX;
using SharpDX.XAudio2;
using SharpDX.Multimedia;
using System.Runtime.InteropServices;


namespace chirpcore {
    public class SoundSystem {
        private XAudio2 XAudio;
        private MasteringVoice masteringVoice;
        private SourceVoice sourceVoice;
        private BufferRing Buffers;

        public SoundSystem(int Tempo) {
            XAudio = new XAudio2();
            masteringVoice = new MasteringVoice(XAudio, inputChannels: 2, inputSampleRate: 44100);
            var wf = new SharpDX.Multimedia.WaveFormat(44100, 16, 2);
            sourceVoice = new SourceVoice(XAudio, wf);
            sourceVoice.BufferStart += (_) => Console.WriteLine("starting buffer");
            Buffers = new BufferRing(2, 4410);

        }

        public void Render(Instrument instrument) {
            var buffer = Buffers.Next();
            instrument.Render(buffer.Memory);
            var ab = new AudioBuffer(buffer.Pointer);
            sourceVoice.SubmitSourceBuffer(ab, null);
            sourceVoice.Start();
        }

// V        public void Test() {

//             var e = new Envelope(4000, 0, 0.6, 100);
//             var beeper = new Instrument(new SineGenerator(), e);

//             AudioBuffer audioBuffer;
//             var Buffers = new BufferRing(2, 8820);
//             ChirpBuffer buffer;
//             beeper.Activate(440, 200);

//             sv.BufferStart += (_) => Console.WriteLine("sound test. buffer start");
//             sv.BufferEnd += (_) => {
//                 Console.WriteLine("sound test. buffer end");
//                 buffer = Buffers.Next();

//                 beeper.Render(buffer.Memory);
//                 audioBuffer = new AudioBuffer(buffer.Pointer);
                
//                 sv.SubmitSourceBuffer(audioBuffer, null);
//                 sv.Start();
//             };
//             buffer = Buffers.Next();

//             beeper.Render(buffer.Memory);
//             audioBuffer = new AudioBuffer(buffer.Pointer);
//             sv.SubmitSourceBuffer(audioBuffer, null);
//             sv.Start();
        //     System.Threading.Thread.Sleep(1200);
        //     Console.WriteLine("end");
        //     sv.DestroyVoice();
        //     sv.Dispose();
        // }


        public void Dispose() {
            masteringVoice.Dispose();
            XAudio.Dispose();
        }
    }
}