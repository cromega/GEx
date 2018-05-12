using System;

namespace chirpcore {
    public interface IGenerator {
        void Fill(double[] buffer, double frequency);
    }
}