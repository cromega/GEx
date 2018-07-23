using System;

namespace GraphExperiment {
    public enum AudioNodeDirection {
        OutputOnly,
        InputOutput,
    }

    public class AudioNodeAttribute : Attribute {
        public AudioNodeDirection Direction;
    }

    public class AudioNodeParameterAttribute : Attribute {}
}
