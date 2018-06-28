using System;
using System.Collections.Generic;
using System.Linq;


namespace Chirpesizer {
    public class Instrument {
        public readonly OscillatorType Osc;
        public readonly double Volume;
        public readonly List<IModulator> Modulators;
        private List<Trigger> ActiveTriggers;

        public Instrument(OscillatorType osc, double volume, List<IModulator>modulators) {
            Osc = osc;
            Volume = volume;
            Modulators = modulators;
            ActiveTriggers = new List<Trigger>();
        }

        public Trigger Activate(double frequency, int length) {
            var osc = new Oscillator(Osc);
            var trig = new Trigger(osc, GetFrequency(frequency), length, GetVolume());
            ActiveTriggers.Add(trig);
            return trig;
        }

        public List<double[]> RenderTriggers(int frames, int songTime) {
            PrepareSegment(frames, songTime);
            var buffers = new List<double[]>();
            foreach (var trigger in ActiveTriggers) {
                buffers.Add(trigger.Render(frames, songTime));
            }
            ActiveTriggers.RemoveAll(trigger => trigger.Finished);

            return buffers;
        }

        private PatchableValue GetVolume() {
            var value = new PatchableValue(Volume, "v");
            GetModulatorsFor(value).ForEach(mod => value.AddModulator(mod));
            return value;
        }

        private PatchableValue GetFrequency(double frequency) {
            var value = new PatchableValue(frequency, "x");
            GetModulatorsFor(value).ForEach(mod => value.AddModulator(mod));
            return value;
        }

        private List<IModulator> GetModulatorsFor(PatchableValue value) {
            return Modulators.Where(mod => mod.GetTarget() == value.Id).ToList();

        }

        private void PrepareSegment(int frames, int songTime) {
            Modulators.Where(mod => mod.GetType().Name == "LFOModulator").ToList().ForEach(lfo => ((LFOModulator)lfo).Prepare(frames, songTime));
        }
    }
}