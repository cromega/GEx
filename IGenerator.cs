using System;

namespace chirpcore {
    public interface IGenerator {
        void Fill(short[] buffer, double frequency);
    }
}