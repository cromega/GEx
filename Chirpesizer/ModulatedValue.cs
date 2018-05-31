using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chirpesizer {
    public class ModulatedValue : IValue {
        public readonly double Value;
        public readonly Oscillator Osc;
        public readonly double Height;
        public readonly double Frequency;
        public readonly Envelope Envelope;


        public ModulatedValue(double startValue, OscillatorType oscType, double frequency, double height) {
            Value = startValue;
            Height = height;
            Frequency = frequency;
            Osc = new Oscillator(oscType);
        }

        public ModulatedValue(double startValue, Envelope envelope) {
            Value = startValue;
            Envelope = envelope;
        }

        public double Get(int time, bool isActive) {
            if (Osc != null) {
                return Value + Osc.Next(Frequency) * Height;
            }
            return Value * Envelope.Next(time, isActive);
        }
    }
}
