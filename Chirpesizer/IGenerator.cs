using System;

namespace Chirpesizer {
    public interface IGenerator {
        void Fill(double[] buffer, int frames);
        double Next();
    }
}