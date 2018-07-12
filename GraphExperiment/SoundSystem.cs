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
        }

        private const int CALLBACK_FUNCTION = 0x30000;
        private const int MMSYS_NOERROR = 0;
        private const int WOM_OPEN = 955;
        private const int WOM_CLOSE = 956;
        private const int WOM_DONE = 957;
        private const int WAVE_FORMAT_PCM = 1;
        private const uint WAVE_MAPPER = 4294967295;

        [DllImport("winmm.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern uint waveOutOpen(ref IntPtr hWaveOut, uint uDeviceID, ref WaveFormatEx lpFormat, waveOutHandle dwCallback, IntPtr dwInstance, uint dwFlags);

        [DllImport("winmm.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern uint waveOutClose(IntPtr hwo);

        [DllImport("winmm.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern uint waveOutPrepareHeader(IntPtr hWaveOut, IntPtr pwh, uint uSize);

        [DllImport("winmm.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern uint waveOutUnprepareHeader(IntPtr hWaveOut, IntPtr pwh, uint uSize);

        [DllImport("winmm.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern uint waveOutWrite(IntPtr hwo, IntPtr pwh, uint cbwh);

        private delegate void waveOutHandle(IntPtr handle, uint message, IntPtr instance, IntPtr param1, IntPtr param2);
        #endregion

        private IntPtr Handle;
        private BlockingCollection<IntPtr> BufferQueue;


        public SoundSystem(int frames) {
            var wf = new WaveFormatEx(44100, 16, 2);
            var ret = waveOutOpen(ref Handle, WAVE_MAPPER, ref wf, WaveOutHandler, IntPtr.Zero, CALLBACK_FUNCTION);
            if (ret != MMSYS_NOERROR) {
                throw new Exception(String.Format("failed to open audio device: {0}", ret));
            }

            BufferQueue = new BlockingCollection<IntPtr>(new ConcurrentQueue<IntPtr>()) {
                GCHandle.Alloc(new short[frames * 2], GCHandleType.Pinned).AddrOfPinnedObject(),
                GCHandle.Alloc(new short[frames * 2], GCHandleType.Pinned).AddrOfPinnedObject()
            };
        }

        public void Write(short[] data) {
            var buffer = BufferQueue.Take();
            Logger.Log("prepping buffer {0}", buffer);
            Marshal.Copy(data, 0, buffer, data.Length);
            var whdr = new WaveHeader { lpData = buffer, dwBufferLength = (uint)data.Length * sizeof(short) };
            var ptr = Marshal.AllocHGlobal(Marshal.SizeOf(whdr));
            Marshal.StructureToPtr(whdr, ptr, fDeleteOld: false);

            var ret = waveOutPrepareHeader(Handle, ptr, (uint)Marshal.SizeOf<WaveHeader>());
            if (ret != MMSYS_NOERROR) {
                throw new Exception(String.Format("failed to prepare header: {0}", ret));
            }

            ret = waveOutWrite(Handle, ptr, (uint)Marshal.SizeOf<WaveHeader>());
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

        private void WaveOutHandler(IntPtr handle, uint message, IntPtr instance, IntPtr param1, IntPtr param2) {
            switch (message) {
                case WOM_OPEN:
                    Logger.Log("Audio Device opened.");
                    break;
                case WOM_DONE:
                    var whdr = Marshal.PtrToStructure<WaveHeader>(param1);
                    Logger.Log("playback done on {0}", whdr.lpData);
                    BufferQueue.Add(whdr.lpData);
                    waveOutUnprepareHeader(Handle, param1, 32);
                    Marshal.FreeHGlobal(param1);
                    break;
                case WOM_CLOSE:
                    Logger.Log("Audio device closed.");
                    break;
            }
        }
    }
}
