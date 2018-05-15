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
            var parts = InstrumentData.Split(":".ToCharArray());
            Envelope envelope = null;
            if (parts.Length > 1) { envelope = Envelope.Parse(parts[1]); }
            switch (parts[0]) {
                case "0":
                    return new Instrument(new NoiseGenerator(), envelope);
                case "1":
                    return new Instrument(new SineGenerator(), envelope);
                case "2":
                    return new Instrument(new SquareGenerator(), envelope);
            }

            throw new Exception(String.Format("Can't create instrument from \"{0}\"", InstrumentData));
        }
    }
}