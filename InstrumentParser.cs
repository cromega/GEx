using System;

namespace chirpcore {
    public class InstrumentParser {
        private string InstrumentData;
        public static Instrument Parse(string data) {
            return new InstrumentParser(data).CreateInstrument();
        }

        private InstrumentParser(string data) {
            InstrumentData = data;
        }

        private Instrument CreateInstrument() {
            switch (InstrumentData) {
                case "0":
                    return new Instrument(new NoiseGenerator());
                case "1":
                    return new Instrument(new SineGenerator());
                case "2":
                    return new Instrument(new SquareGenerator());
            }

            throw new Exception(String.Format("Can't create instrument from \"{0}\"", InstrumentData));
        }
    }
}