using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphExperiment {
    public class AudioNodeAttribute : Attribute {}
    public class AudioNodeParameterAttribute : Attribute {}

    [AudioNode]
    public class ControlTest {
        [AudioNodeParameter]
        public double Frequency;

        [AudioNodeParameter]
        public int Count;
    }
}
