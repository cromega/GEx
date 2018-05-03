using System;
using SharpDX;
using SharpDX.XAudio2;
using System.Runtime.InteropServices;

namespace chirpcore
{
    class Program
    {
        [DllImport("Ole32.dll")]
        static extern int CoInitialize(IntPtr pvReserve);

        static void Main(string[] args)
        {
            CoInitialize(IntPtr.Zero);
            var audio = new XAudio2();
            var mv = new MasteringVoice(audio);
            var wf = new SharpDX.Multimedia.WaveFormat(44100, 16, 2);
            var sv = new SourceVoice(audio, wf, enableCallbackEvents: true);
            sv.BufferStart += (_) => Console.WriteLine("started playing buffer");
            sv.BufferEnd += (_) => Console.WriteLine("finished playing buffer");
            
            var data = new short[88200];
            Osc.Noise(data);

            var ds = DataStream.Create(data, canRead: true, canWrite: false);
            var b = new AudioBuffer(ds);
            sv.SubmitSourceBuffer(b, null);
            sv.Start();
            System.Threading.Thread.Sleep(1000);

            sv.DestroyVoice();
            sv.Dispose();
            b.Stream.Dispose();            
            mv.Dispose();
            audio.Dispose();
        }
    }
}
