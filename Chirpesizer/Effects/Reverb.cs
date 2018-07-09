using System;

namespace Chirpesizer.Effects {
    public class Reverb : IEffect {
        public readonly int Delay;
        public readonly double Decay;

        private double[] DelayBuffer;

        public Reverb(int delay, double decay) {
            Delay = delay;
            Decay = decay;
            DelayBuffer = new double[delay * 2];
        }

        public double[] Apply(double[] input) {
            var output = (double[])input.Clone();

            if (input.Length/2 > Delay) {
                // apply carried over delay buffer
                for (int i=0; i < Delay; i++) {
                    output[i*2] += DelayBuffer[i*2];
                    output[i*2+1] += DelayBuffer[i*2+1];
                }

                // apply delay within input
                for (int i = 0; i < input.Length/2 - Delay; i++) {
                    output[(i + Delay)*2] += output[i*2] * Decay;
                    output[(i + Delay)*2+1] += output[i*2+1] * Decay;
                }

                // copy the end of the input into the delay buffer
                for (int i=0; i<Delay; i++) {
                    DelayBuffer[i*2] = input[input.Length/2 - Delay + (i*2)] * Decay;
                    DelayBuffer[i*2+1] = input[input.Length/2 - Delay + (i*2+1)] * Decay;
                }
            } else { // if (Delay >= input.Length)
                // apply carried over delay buffer
                for (int i=0; i < input.Length/2; i++) {
                    output[i*2] += DelayBuffer[i*2];
                    output[i*2+1] += DelayBuffer[i*2+1];
                }

                // shift the unused part of the delay buffer to the front
                // and update the end with the input
                Array.Copy(DelayBuffer, input.Length, DelayBuffer, 0, Delay*2 - input.Length);
                for (int i=0; i<input.Length/2; i++) {
                    DelayBuffer[Delay - output.Length/2 + (i*2)] = output[i*2] * Decay;
                    DelayBuffer[Delay - output.Length/2 + (i*2+1)] = output[i*2+1] * Decay;
                }
            }
            return output;
        }
    }
}
