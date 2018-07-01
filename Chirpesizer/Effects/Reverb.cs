using System;

namespace Chirpesizer.Effects {
    public class Reverb : IEffect {
        public readonly int Delay;
        public readonly double Dampen;

        private double[] DampeningMap;

        public Reverb(int delay, double dampen) {
            Delay = delay;
            Dampen = dampen;
            DampeningMap = new double[delay];
        }

        public void Apply(double[] input) {
            for (int i=0; i<input.Length; i++) {
                input[i] += DampeningMap[i];
            }

            Buffer.BlockCopy(DampeningMap, input.Length * sizeof(double), DampeningMap, 0, (DampeningMap.Length - input.Length) * sizeof(double));
            for (int i=0; i<input.Length; i++) {
                DampeningMap[DampeningMap.Length - input.Length + i] = input[i] * Dampen;
            }
        }
    }
}
