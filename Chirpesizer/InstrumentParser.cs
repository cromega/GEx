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
            var parts = InstrumentData.Split(":".ToCharArray());
            var volume = double.Parse(parts[1]);
            Envelope envelope = Envelope.Parse(parts[2]);
            switch (parts[0]) {
                case "0":
                    return new Instrument(Oscillator.Noise, volume, envelope);
                case "1":
                    return new Instrument(Oscillator.Sine, volume, envelope);
                case "2":
                    return new Instrument(Oscillator.Square, volume, envelope);
            }

            throw new Exception(String.Format("Can't create instrument from \"{0}\"", InstrumentData));
        }
    }
}