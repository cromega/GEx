using System;
using Chirpesizer;

namespace Chirpotle {
    internal interface IModulatorControl {
        IModulator GetModulator();
    }
}