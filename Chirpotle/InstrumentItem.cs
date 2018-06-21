using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chirpesizer;

namespace Chirpotle {
    public class InstrumentItem {
        private string _Name;
        public string Name {
            get { return _Name; }
        }
        public string InstrumentData {
            get { return getInstrumentData(); }
        }

        private Instrument _Instrument;

        public InstrumentItem(string name, Instrument instrument) {
            _Name = name;
            _Instrument = instrument;
        }

        public Instrument GetInstrument() {
            return _Instrument;
        }

        private string getInstrumentData() {
            return new InstrumentSerializer(_Instrument).Serialize();
        }
    }
}
