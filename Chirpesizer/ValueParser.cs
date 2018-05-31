using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chirpesizer {
    public class ValueParser {
        public static IValue Parse(string valueData) {
            if (valueData.Contains(":")) {
                return ParseModulatedValue(valueData);
            } else {
                var value = double.Parse(valueData);
                return new StaticValue(value);
            }
        }

        private static IValue ParseModulatedValue(string valueData) {
            var valueParts = valueData.Split(":".ToCharArray());
            var value = double.Parse(valueParts[0]);

            switch(valueParts[1][0]) {
                case 'e':
                    var envelope = Envelope.Decode(valueParts[1].Trim("e".ToCharArray()));
                    return new ModulatedValue(value, envelope);
                case 'l':
                    var modulationParts = valueParts[1].Trim("l".ToCharArray()).Split(",".ToCharArray());
                    var osc = (OscillatorType)int.Parse(modulationParts[0]);
                    var freq = double.Parse(modulationParts[1]);
                    var height = double.Parse(modulationParts[2]);
                    return new ModulatedValue(value, osc, freq, height);
                default:
                    throw new Exception("wtf");
            }
        }
    }

}
