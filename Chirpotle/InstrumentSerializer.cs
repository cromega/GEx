using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chirpesizer;

namespace Chirpotle {
    public class InstrumentSerializer {
        private Instrument Instrument;
        public InstrumentSerializer(Instrument instrument) {
            Instrument = instrument;
        }

        public string Serialize() {
            var output = new StringBuilder();
            output.AppendFormat("{0};", (int)Instrument.Osc);
            output.AppendFormat("pv{0};", Instrument.Volume);
            foreach (IModulator mod in Instrument.Modulators) {
                output.AppendFormat("{0};", FormatModulator(mod));                
            }

            return output.ToString().TrimEnd(";".ToCharArray());
        }

        private string FormatModulator(IModulator modulator) {
            var output = new StringBuilder("m");
            switch (modulator.GetType().Name) {
                case "EnvelopeModulator": {
                        var m = (EnvelopeModulator)modulator;
                        output.AppendFormat("e{0}{1},{2},{3},{4}",
                            m.GetTarget(),
                            m.Envelope.Attack,
                            m.Envelope.Decay,
                            m.Envelope.Sustain,
                            m.Envelope.Release);
                        break;
                    }
                case "LFOModulator": {
                        var m = (LFOModulator)modulator;
                        output.AppendFormat("l{0}{1},{2},{3}", m.GetTarget(), (int)m.Oscillator.OscillatorType, m.Frequency, m.Amplitude);
                        break;
                    }
            }

            return output.ToString();
        }
    }
}
