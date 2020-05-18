using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GEx
{
    static class WinMM
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct WaveFormatEx
        {
            public ushort wFormatTag;
            public ushort nChannels;
            public uint nSamplesPerSec;
            public uint nAvgBytesPerSec;
            public ushort nBlockAlign;
            public ushort wBitsPerSample;
            public ushort cbSize;

            public WaveFormatEx(uint frequency, ushort bps, ushort channels) {
                wFormatTag = WAVE_FORMAT_PCM;
                nSamplesPerSec = frequency;
                wBitsPerSample = bps;
                nChannels = channels;
                nBlockAlign = (ushort)(nChannels * wBitsPerSample / 8);
                nAvgBytesPerSec = nSamplesPerSec * nBlockAlign;
                cbSize = 0;
            }
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct WaveHeader
        {
            public IntPtr lpData;
            public uint dwBufferLength;
            public uint dwBytesRecorded;
            public IntPtr dwUser;
            public uint dwFlags;
            public uint dwLoops;
            public IntPtr lpNext;
            public IntPtr reserved;
        }

        public const int CALLBACK_FUNCTION = 0x30000;
        public const int MMSYS_NOERROR = 0;
        public const int WOM_OPEN = 955;
        public const int WOM_CLOSE = 956;
        public const int WOM_DONE = 957;
        public const int WAVE_FORMAT_PCM = 1;
        public const uint WAVE_MAPPER = 4294967295;

        public delegate void waveOutHandler(IntPtr handle, uint message, IntPtr instance, IntPtr param1, IntPtr param2);

        [DllImport("winmm.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern uint waveOutOpen(ref IntPtr hWaveOut, uint uDeviceID, ref WaveFormatEx lpFormat, waveOutHandler dwCallback, IntPtr dwInstance, uint dwFlags);

        [DllImport("winmm.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern uint waveOutClose(IntPtr hwo);

        [DllImport("winmm.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern uint waveOutPrepareHeader(IntPtr hWaveOut, IntPtr pwh, uint uSize);

        [DllImport("winmm.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern uint waveOutUnprepareHeader(IntPtr hWaveOut, IntPtr pwh, uint uSize);

        [DllImport("winmm.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern uint waveOutWrite(IntPtr hwo, IntPtr pwh, uint cbwh);
    }
}
