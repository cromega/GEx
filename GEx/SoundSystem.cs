using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Collections.Concurrent;

namespace GEx {
    public class SoundSystem {

        private IntPtr DeviceHandle;
        private BlockingCollection<IntPtr> BufferQueue;
        private object Lock = new object();
        private WinMM.waveOutHandler callbackHandle;
        public readonly int Frames;

        public SoundSystem(int frames) {
            Frames = frames;

            callbackHandle = new WinMM.waveOutHandler(WaveOutHandler);
            var wf = new WinMM.WaveFormatEx(44100, 16, 2);
            var ret = WinMM.waveOutOpen(ref DeviceHandle, WinMM.WAVE_MAPPER, ref wf, callbackHandle, IntPtr.Zero, WinMM.CALLBACK_FUNCTION);
            if (ret != WinMM.MMSYS_NOERROR) {
                throw new Exception(String.Format("failed to open audio device: {0}", ret));
            }

            BufferQueue = new BlockingCollection<IntPtr>() {
                Marshal.AllocHGlobal(Frames * 2 * sizeof(short)),
                Marshal.AllocHGlobal(Frames * 2 * sizeof(short)),
            };
        }

        public void Write(Sample[] chunk) {
            var buffer = BufferQueue.Take();
#if DEBUG
            Logger.Log("prepping buffer {0}", buffer);
#endif
            var raw = SamplesToRaw(chunk);
            Marshal.Copy(raw, 0, buffer, raw.Length);
            var whdr = new WinMM.WaveHeader { lpData = buffer, dwBufferLength = (uint)raw.Length * sizeof(short) };
            var whdrptr = Marshal.AllocHGlobal(Marshal.SizeOf(whdr));
            Marshal.StructureToPtr(whdr, whdrptr, fDeleteOld: false);

            lock (Lock) {
                var ret = WinMM.waveOutPrepareHeader(DeviceHandle, whdrptr, (uint)Marshal.SizeOf(whdr));
                if (ret != WinMM.MMSYS_NOERROR) {
                    throw new Exception(String.Format("failed to prepare header: {0}", ret));
                }

                ret = WinMM.waveOutWrite(DeviceHandle, whdrptr, (uint)Marshal.SizeOf(whdr));
                if (ret != WinMM.MMSYS_NOERROR) {
                    throw new Exception(String.Format("failed to write audio buffer: {0}", ret));
                }
            }
        }

        public void Close() {
            var ret = WinMM.waveOutClose(DeviceHandle);
            if (ret != WinMM.MMSYS_NOERROR) {
                throw new Exception(String.Format("failed to close audio device: {0}", ret));
            }
        }

        private void WaveOutHandler(IntPtr handle, uint message, IntPtr instance, IntPtr param1, IntPtr param2) {
            switch (message) {
#if DEBUG
                case WinMM.WOM_OPEN:
                    Logger.Log("Audio Device opened.");
                    break;
#endif
                case WinMM.WOM_DONE:
                    var whdr = Marshal.PtrToStructure<WinMM.WaveHeader>(param1);
#if DEBUG
                    Logger.Log("playback done on {0}", whdr.lpData);
#endif
                    BufferQueue.Add(whdr.lpData);
                    lock (Lock) { WinMM.waveOutUnprepareHeader(DeviceHandle, param1, (uint)Marshal.SizeOf(whdr)); }
                    Marshal.FreeHGlobal(param1);
                    break;
#if DEBUG
                case WinMM.WOM_CLOSE:
                    Logger.Log("Audio device closed.");
                    break;
#endif
            }
        }

        private short[] SamplesToRaw(Sample[] chunk) {
            var data = new short[chunk.Length * 2];
            for (int i=0; i<chunk.Length; i++) {
                data[i*2] = (short)chunk[i].L;
                data[i*2+1] = (short)chunk[i].R;
            }
            return data;
        }
    }
}
