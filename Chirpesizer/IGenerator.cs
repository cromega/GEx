using System;

namespace Chirpesizer {
    public interface IGenerator {
        void Fill(double[] buffer, double frequency, int frames);
    }
}