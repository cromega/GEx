using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace GraphExperiment {
    public class SoundSystem {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private struct WaveFormatExtensible {
            public ushort wFormatTag;
            public ushort nChannels;
            public uint nSamplesPerSec;
            public uint nAvgBytesPerSec;
            public ushort nBlockAlign;
            public ushort wBitsPerSample;
            public ushort cbSize;

            public WaveFormatExtensible(uint frequency, ushort bps, ushort channels) {
                wFormatTag = 1;
                nSamplesPerSec = frequency;
                wBitsPerSample = bps;
                nChannels = channels;
                nBlockAlign = (ushort)(nChannels * wBitsPerSample / 8);
                nAvgBytesPerSec = nSamplesPerSec * nBlockAlign;
                cbSize = 0;
            }
        }

        private const uint WHDR_BEGINLOOP = 4;
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private struct WaveHeader {
            public IntPtr lpData;
            public uint dwBufferLength;
            public uint dwBytesRecorded;
            public IntPtr dwUser;
            public uint dwFlags;
            public uint dwLoops;
            //public IntPtr lpNext;
            //public IntPtr reserved;

            public WaveHeader(IntPtr buffer, uint length) {
                lpData = buffer;
                dwBufferLength = length;
                dwBytesRecorded = 0;
                dwUser = IntPtr.Zero;
                dwFlags = WHDR_BEGINLOOP;
                dwLoops = 1;
            }
        }

        private const int CALLBACK_FUNCTION = 0x30000;
        private const int MMSYS_NOERROR = 0;

        [DllImport("winmm.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern uint waveOutOpen(ref IntPtr hWaveOut, int uDeviceID, ref WaveFormatExtensible lpFormat, waveOutHandle dwCallback, IntPtr dwInstance, uint dwFlags);

        [DllImport("winmm.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern uint waveOutClose(IntPtr hwo);

        [DllImport("winmm.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern uint waveOutWrite(IntPtr hwo, ref WaveHeader pwh, uint cbwh);

        private delegate void waveOutHandle(IntPtr handle, uint message, IntPtr instance, IntPtr param1, IntPtr param2);

        private IntPtr Handle;
        private int ChunkSize;

        public SoundSystem(int frames) {
            ChunkSize = frames;

            var wwf = new WaveFormatExtensible(44100, 16, 2);
            var ret = waveOutOpen(ref Handle, -1, ref wwf, waveOutHandler, IntPtr.Zero, CALLBACK_FUNCTION);
            if (ret != MMSYS_NOERROR) {
                throw new Exception("failed to grab audio handle");
            }
        }

        public void Close() {
            var ret = waveOutClose(Handle);
            if (ret != MMSYS_NOERROR) {
                throw new Exception("failed to grab audio handle");
            }
        }

        private void waveOutHandler(IntPtr handle, uint message, IntPtr instance, IntPtr param1, IntPtr param2) {

        }
    }
}
