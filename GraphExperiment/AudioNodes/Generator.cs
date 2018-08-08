using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphExperiment {
    public enum SignalType : int {
        Sine = 0,
        Square = 1,
        Noise = 2,
    }

    [AudioNode]
    public class Generator : AudioNode {
        class Trigger {
            public string ID;
            public double Frequency;
            public int Time;
            public bool Triggered;

            public Trigger(double frequency) {
                ID = Guid.NewGuid().ToString();
                Frequency = frequency;
                Triggered = true;
                Time = 0;
            }
        }

        public double Frequency;
        [AudioNodeParameter]
        public SignalType SignalType;
        private Random Rnd;
        private List<Trigger> Triggers;
        private object Lock = new object();

        public Generator(short id) : base(id) {
            Triggers = new List<Trigger>();
            Rnd = new Random();
        }

        public override Packet[] Fetch() {
            var packets = new List<Packet>();
            Triggers.ForEach(trigger => {
                double sample = 0;
                switch (SignalType) {
                    case SignalType.Sine: sample = Sine(trigger.Frequency, trigger.Time); break;
                    case SignalType.Square: sample = Square(trigger.Frequency, trigger.Time); break;
                    case SignalType.Noise: sample = Noise(); break;
                }
                packets.Add(new Packet(trigger.ID, trigger.Triggered ? Control.Signal : Control.End, new Sample(sample), trigger.Time++));
            });

            return packets.ToArray();
        }

        public string Start(double frequency) {
            var trigger = new Trigger(frequency);
            lock (Lock) {
                Triggers.Add(trigger);
            }
            Logger.Log("Starting trigger {0}", trigger.ID);
            return trigger.ID;
        }

        public void Release(string triggerID) {
            lock(Lock) {
                Logger.Log("releasing trigger {0}", triggerID);
                Triggers.First(trigger => trigger.ID == triggerID).Triggered = false;
            }
        }

        public void Remove(string triggerID) {
            lock (Lock) {
                Logger.Log("Removing trigger {0}", triggerID);
                Logger.Log("Removed {0} triggers", Triggers.RemoveAll(trigger => trigger.ID == triggerID));
            }
        }

        private double Sine(double frequency, int t) {
            return Math.Sin(frequency * Math.PI * 2 * t / 44100);
        }

        private double Square(double frequency, int t) {
            return Sine(frequency, t) > 0 ? 1 : -1;
        }

        private double Noise() {
            return Rnd.NextDouble() * 2 - 1;
        }
    }
}
