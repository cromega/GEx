using System;

namespace Chirpesizer {
    public class InstrumentParser {
        private string InstrumentData;
        public static Instrument Parse(string data) {
            return new InstrumentParser(data).CreateInstrument();
        }

        private InstrumentParser(string data) {
            InstrumentData = data;
        }

        private Instrument CreateInstrument() {
            //2;0.5:1,0.2;20,20,0.5,20;
            var parts = InstrumentData.Split(";".ToCharArray());
            OscillatorType osc = OscillatorType.Sine;
            switch (parts[0]) {
                case "0":
                    osc = OscillatorType.Noise;
                    break;
                case "1":
                    osc = OscillatorType.Sine;
                    break;
                case "2":
                    osc = OscillatorType.Square;
                    break;
            }
            var volume = ValueParser.Parse(parts[1]);
            Envelope envelope = Envelope.Parse(parts[2]);
            return new Instrument(osc, volume, envelope);

            throw new Exception(String.Format("Can't create instrument from \"{0}\"", InstrumentData));
        }
    }
}