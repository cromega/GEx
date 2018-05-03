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
            var sound = new Sound();
            sound.Test();
        }
    }
}
