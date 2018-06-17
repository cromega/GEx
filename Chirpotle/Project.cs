using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chirpesizer;

namespace Chirpotle {
    public class Project {
        public List<Instrument> Instruments;
        public Project() {
            Instruments = new List<Instrument>();
        }
    }
}
