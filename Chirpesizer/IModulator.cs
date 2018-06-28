using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chirpesizer {
    public interface IModulator {
        string GetTarget();
        double Get(double value, int triggerTime, bool isActive, int songTime);
    }
}
