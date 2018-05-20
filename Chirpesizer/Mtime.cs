using System;

namespace Chirpesizer {

    ///
    /// converts between frames and milliseconds
    ///
    public class MTime {
        public int Frames;
        public int Milliseconds {
            get { return ToMilliseconds(); }
        }

        public MTime(int frames) {
            Frames = frames;
        }

        private int ToMilliseconds() {
            return (int)Math.Round(Frames / 44.1);
        }        

        public static MTime FromMs(int ms) {
            return new MTime((int)Math.Round(ms * 44.1));
        }
    }
}