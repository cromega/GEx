using System;
using System.Runtime.InteropServices;
using System.IO;

namespace chirpcore
{
    class Program
    {
        [DllImport("Ole32.dll")]
        static extern int CoInitialize(IntPtr pvReserve);

        static void Main(string[] args)
        {
            CoInitialize(IntPtr.Zero);
            Logger.On();

            var sound = new SoundSystem();
            var song = new Song(File.ReadAllText("song.txt"));

            do {
                var buffers = song.RenderNext(4410);
                sound.AddBuffers(buffers);
            } while (!song.Ended());

            System.Threading.Thread.Sleep(300);
        }
    }
}
