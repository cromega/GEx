using System;
using NAudio.Wave;

namespace Utils
{
    public class WavWriter : IDisposable {
        private WaveFileWriter Writer;

        public WavWriter(string filename) {
            Writer = new WaveFileWriter(filename, new WaveFormat(44100, 16, 2));
        }

        public void Write(short[] buffer) {
            var data = new byte[buffer.Length * 2];
            Buffer.BlockCopy(buffer, 0, data, 0, data.Length);
            Writer.Write(data, 0, data.Length);
        }

        public void Close() {
            if (Writer != null) { Writer.Close(); }
        }

        public void Dispose() {
            Close();
        }
    }
}
