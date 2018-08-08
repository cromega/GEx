using System;

namespace GraphExperiment {
    public enum AudioNodeDirection {
        InputOutput,
        OutputOnly,
        InputOnly,
    }

    public class AudioNodeAttribute : Attribute {
        public AudioNodeDirection Direction;
    }

    public class AudioNodeParameterAttribute : Attribute {}
}
