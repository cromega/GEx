using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chirpesizer {
    public static class Oscillator {
        public static IGenerator Create(OscillatorType type, IValue frequency) {
            switch (type) {
                case OscillatorType.Noise: return new NoiseGenerator();
                case OscillatorType.Sine: return new SineGenerator(frequency);
                case OscillatorType.Square: return new SquareGenerator(frequency);
            }

            throw new Exception("don't know how to create oscillator");
        }
    }
}
