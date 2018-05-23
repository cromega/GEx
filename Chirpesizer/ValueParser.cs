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
                return new ModulatedValue(double.Parse(frequencyParts[0]), double.Parse(valueParts[1]), double.Parse(valueParts[2]));
            } else {
                var value = double.Parse(valueData);
                return new StaticValue(value);
            }
        }
    }

}
