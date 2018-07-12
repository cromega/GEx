using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Collections.Concurrent;

namespace GraphExperiment {
    public class SoundSystem {
        #region WinMM native stuff
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private struct WaveFormatEx {
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
        private struct WaveHeader {
            public IntPtr lpData;
            public uint dwBufferLength;
            public uint dwBytesRecorded;
            public IntPtr dwUser;
            public uint dwFlags;
            public uint dwLoops;
            public IntPtr lpNext;
            public IntPtr reserved;

            public WaveHeader(IntPtr buffer, int length) {
                lpData = buffer;
                dwBufferLength = (uint)length;
                dwBytesRecorded = 0;
                dwUser = IntPtr.Zero;
                dwFlags = WHDR_BEGINLOOP;
                dwLoops = 1;
                lpNext = IntPtr.Zero;
                reserved = IntPtr.Zero;
            }
        }

        private const int CALLBACK_FUNCTION = 0x30000;
        private const int MMSYS_NOERROR = 0;
        private const int WOM_OPEN = 955;
        private const int WOM_CLOSE = 956;
        private const int WOM_DONE = 957;
        private const int WHDR_BEGINLOOP = 4;
        private const int WAVE_FORMAT_PCM = 1;

        [DllImport("winmm.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern uint waveOutOpen(ref IntPtr hWaveOut, int uDeviceID, ref WaveFormatEx lpFormat, waveOutHandle dwCallback, IntPtr dwInstance, uint dwFlags);

        [DllImport("winmm.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern uint waveOutClose(IntPtr hwo);

        [DllImport("winmm.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern uint waveOutPrepareHeader(IntPtr hWaveOut, ref WaveHeader pwh, uint uSize);

        [DllImport("winmm.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern uint waveOutUnprepareHeader(IntPtr hWaveOut, IntPtr pwh, uint uSize);

        [DllImport("winmm.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern uint waveOutWrite(IntPtr hwo, ref WaveHeader pwh, uint cbwh);

        private delegate void waveOutHandle(IntPtr handle, uint message, IntPtr instance, IntPtr param1, IntPtr param2);
        #endregion

        private IntPtr Handle;
        private int ChunkSize;
        private BlockingCollection<Buffer> Buffers;


        public SoundSystem(int frames) {
            ChunkSize = frames;

            var wf = new WaveFormatEx(44100, 16, 2);
            var ret = waveOutOpen(ref Handle, -1, ref wf, waveOutHandler, IntPtr.Zero, CALLBACK_FUNCTION);
            if (ret != MMSYS_NOERROR) {
                throw new Exception(String.Format("failed to open audio device: {0}", ret));
            }

            Buffers = new BlockingCollection<Buffer>(new ConcurrentQueue<Buffer>()) {
                { new Buffer(frames) },
                { new Buffer(frames) },
            };
        }

        public void Write(short[] buffer) {
            var output = Buffers.Take();
            Array.Copy(buffer, output.Memory, buffer.Length);
            var hdr = new WaveHeader(output.Pointer, output.LengthInBytes);

            var ret = waveOutPrepareHeader(Handle, ref hdr, 32);
            if (ret != MMSYS_NOERROR) {
                throw new Exception(String.Format("failed to prepare header: {0}", ret));
            }

            ret = waveOutWrite(Handle, ref hdr, 32);
            if (ret != MMSYS_NOERROR) {
                throw new Exception(String.Format("failed to write audio buffer: {0}", ret));
            }
        }

        public void Close() {
            var ret = waveOutClose(Handle);
            if (ret != MMSYS_NOERROR) {
                throw new Exception(String.Format("failed to close audio device: {0}", ret));
            }
        }

        private void waveOutHandler(IntPtr handle, uint message, IntPtr instance, IntPtr param1, IntPtr param2) {
            switch (message) {
                case WOM_OPEN:
                    Console.WriteLine("Audio Device opened.");
                    break;
                case WOM_DONE:
                    Console.WriteLine("playback done");
                    waveOutUnprepareHeader(Handle, param1, 32);
                    break;
                case WOM_CLOSE:
                    Console.WriteLine("Audio device closed.");
                    break;
            }
        }
    }
}
