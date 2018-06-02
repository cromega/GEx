using System;

namespace Chirpesizer {
    public interface IValue {
        double Get(int time, bool isActive);
    }
}
