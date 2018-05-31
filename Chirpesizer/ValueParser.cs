using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chirpesizer {
    public class ValueParser {
        public static IValue Parse(string valueData) {
            if (valueData.Contains(":")) {
                var frequencyParts = valueData.Split(":".ToCharArray());
                var valueParts = frequencyParts[1].Split(",".ToCharArray());
                //TODO: parse oscillator type as well?
                var value = double.Parse(frequencyParts[0]);
                var frequency = double.Parse(valueParts[1]);
                var oscillationHeight = double.Parse(valueParts[2]);
                return new ModulatedValue(value, frequency, oscillationHeight);
            } else {
                var value = double.Parse(valueData);
                return new StaticValue(value);
            }
        }
    }

}
