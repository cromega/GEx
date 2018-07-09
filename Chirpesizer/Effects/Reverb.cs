using System;

namespace Chirpesizer.Effects {
    public class Reverb : IEffect {
        public readonly int Delay;
        public readonly double Dampen;

        private double[] DelayBuffer;

        public Reverb(int delay, double dampen) {
            Delay = delay;
            Dampen = dampen;
            DelayBuffer = new double[delay];
        }

        public double[] Apply(double[] input) {
            var output = (double[])input.Clone();

            if (input.Length > Delay) {
                // apply carried over delay buffer
                for (int i=0; i < Delay; i++) {
                    output[i] += DelayBuffer[i];
                }

                // apply delay within input
                for (int i = 0; i < input.Length - Delay; i++) {
                    output[i + Delay] += output[i] * Dampen;
                }

                // update the delay buffer
                for (int i=0; i<Delay; i++) {
                    DelayBuffer[i] = input[input.Length - Delay + i] * Dampen;
                }
            } else { // if (Delay >= input.Length)
                // apply carried over delay buffer
                for (int i=0; i < input.Length; i++) {
                    output[i] += DelayBuffer[i];
                }

                // update the delay buffer
                Array.Copy(DelayBuffer, input.Length, DelayBuffer, 0, Delay - input.Length);
                for (int i=0; i<input.Length; i++) {
                    DelayBuffer[Delay - output.Length + i] = output[i] * Dampen;
                }
            }
            return output;
        }
    }
}
