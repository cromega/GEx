using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Chirpesizer {
    public class Scaler {
        public void Scale(double[] buffer, IValue amount) {
            double modulatedAmount;
            for (int i = 0; i < buffer.Length / 2; i++) {
                modulatedAmount = amount.Get();
                buffer[i * 2] *= modulatedAmount;
                buffer[i * 2 + 1] *= modulatedAmount;
            }
        }
    }
}
