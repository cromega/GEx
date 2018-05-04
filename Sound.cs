using System;
using SharpDX;
using SharpDX.XAudio2;
using SharpDX.Multimedia;


namespace chirpcore {
    public class Sound {
        private XAudio2 XAudio;
        private MasteringVoice masteringVoice;
        public Sound() {
            XAudio = new XAudio2();
            masteringVoice = new MasteringVoice(XAudio, inputChannels: 2, inputSampleRate: 44100);
        }

        public void Test() {
            var wf = new SharpDX.Multimedia.WaveFormat(44100, 16, 2);
            var sv = new SourceVoice(XAudio, wf);
            sv.BufferStart += (_) => Console.WriteLine("sound test. buffer start");
            sv.BufferEnd += (_) => Console.WriteLine("sound test. buffer end");

            var buffer = new short[8820 * 5];
            var beeper = new Instrument(new SineGenerator());
            beeper.Activate(440);
            beeper.Render(buffer);
            var ds = DataStream.Create(buffer, canRead: true, canWrite: false);
            var ab = new AudioBuffer(ds);

            sv.SubmitSourceBuffer(ab, null);
            sv.Start();
            System.Threading.Thread.Sleep(1000);
            ds.Dispose();
            sv.DestroyVoice();
            sv.Dispose();
        }

        public void Dispose() {
            masteringVoice.Dispose();
            XAudio.Dispose();
        }
    }
}