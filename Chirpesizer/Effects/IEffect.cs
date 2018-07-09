using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chirpesizer.Effects {
    public interface IEffect {
        double[] Apply(double[] input);
    }
}
