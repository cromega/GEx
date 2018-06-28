using System;
using System.Linq;
using System.Collections.Generic;

namespace Chirpesizer {
    public class InstrumentParser {
        public static Instrument Parse(string data) {
            return new InstrumentParser(data).CreateInstrument();
        }

        private List<string> InstrumentParts;
        //private List<PatchableValue> PatchableValues;
        private InstrumentParser(string data) {
            InstrumentParts = new List<string>();
            //PatchableValues = new List<PatchableValue>();

            data.Split(";".ToCharArray()).ToList().ForEach(part => {
                InstrumentParts.Add(part);
            });
        }

        private Instrument CreateInstrument() {
            var osc = GetOscillatorType();
            var volume = GetVolume();
            var modulators = GetModulators();

            //modulators.ForEach(modulator => {
            //    PatchableValues.FindAll(value => value.Id == modulator.GetTarget()).ForEach(value => value.AddModulator(modulator));
            //});

            return new Instrument(osc, volume, modulators);

            //throw new Exception(String.Format("Can't create instrument from \"{0}\"", InstrumentData));
        }

        private OscillatorType GetOscillatorType() {
            return (OscillatorType)int.Parse(InstrumentParts.First());
        }

        private double GetVolume() {
            var valueData = InstrumentParts[1];
            var id = valueData.Substring(1, 1);
            var value = double.Parse(valueData.Substring(2));
            return value;
        }

        private List<IModulator> GetModulators() {
            //mea10,20,0.5,30
            var modulators = new List<IModulator>();
            var modulatorsRaw = InstrumentParts.FindAll(part => part.StartsWith("m")).Select(part => part.Substring(1)).ToList();
            modulatorsRaw.ForEach(modulatorData => {
                var target = modulatorData[1].ToString();
                switch (modulatorData[0]) {
                    case 'e': modulators.Add(new EnvelopeModulator(Envelope.Parse(modulatorData.Substring(2)), target)); break;
                    case 'l':
                        var lfoParts = modulatorData.Substring(2).Split(",".ToCharArray());
                        modulators.Add(new LFOModulator((OscillatorType)int.Parse(lfoParts[0]), double.Parse(lfoParts[1]), double.Parse(lfoParts[2]), target));
                        break;
                }
            });

            return modulators;
        }
    }
}