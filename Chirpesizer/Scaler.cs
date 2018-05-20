using System;
using System.Collections.Generic;
using System.Text;

namespace Chirpesizer {
    public class Scaler {
        public void Scale(double[] buffer, double amount) {
            for (int i = 0; i < buffer.Length; i++) {
                buffer[i] *= amount;
            }
        }
    }
}
