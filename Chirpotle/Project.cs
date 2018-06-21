using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chirpesizer;

namespace Chirpotle {
    public class Project {
        public BindingList<InstrumentItem> Instruments;

        public Project() {
            Instruments = new BindingList<InstrumentItem>();
        }
    }
}
