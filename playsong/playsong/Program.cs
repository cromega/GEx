using System;
using System.Runtime.InteropServices;
using System.IO;
using chirpcore;

namespace playsong {
    class Program {
        static void Main(string[] args) {
            Logger.On();

            var sound = new SoundSystem(4410);
            var song = new Song(File.ReadAllText("song.txt"));

            do {
                var buffers = song.RenderNext(4410);
                sound.AddBuffers(buffers);
            } while (!song.Ended());

            System.Threading.Thread.Sleep(300);
        }
    }
}
