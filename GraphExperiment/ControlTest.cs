﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphExperiment {
    public enum AudioNodeDirection {
        OutputOnly,
        InputOutput,
    }

    public class AudioNodeAttribute : Attribute {
        public AudioNodeDirection Direction;
    }
    public class AudioNodeParameterAttribute : Attribute {}

    [AudioNode(Direction = AudioNodeDirection.InputOutput)]
    public class ControlTest {
        [AudioNodeParameter]
        public double Frequency;

        [AudioNodeParameter]
        public int Count;
    }
}
