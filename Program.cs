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

            var song = new Song(File.ReadAllText("song.txt"));
            var sound = new SoundSystem(song.Tempo);
            song.Play();
        }
    }
}
