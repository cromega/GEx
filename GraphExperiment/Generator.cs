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

        public Generator(int id, SignalType signalType) : base(id) {
            Triggers = new List<Trigger>();
            SignalType = signalType;
            Rnd = new Random();
        }

        protected override void Run() {
            Task.Run(() => {
                for (; ; ) {
                    lock (Lock) {
                        Triggers.ForEach(trigger => {
                            double sample = 0;
                            switch (SignalType) {
                                case SignalType.Sine: sample = Sine(trigger.Frequency, trigger.Time); break;
                                case SignalType.Square: sample = Square(trigger.Frequency, trigger.Time); break;
                                case SignalType.Noise: sample = Noise(); break;
                            }
                            var packet = new Packet(trigger.ID, trigger.Triggered ? Control.Signal : Control.End, new Sample(sample), trigger.Time);
                            trigger.Time++;
                            Send(packet);
                        });
                    }
                }
            });
        }

        protected override Packet Update(Packet packet) {
            return null;
        }

        public string Start(double frequency) {
            var trigger = new Trigger(frequency);
            lock (Lock) {
                Triggers.Add(trigger);
            }
            return trigger.ID;
        }

        public void Release(string triggerID) {
            lock(Lock) {
                Triggers.First(trigger => trigger.ID == triggerID).Triggered = false;
            }
        }

        public void Remove(string triggerID) {
            lock(Lock) {
                Triggers.RemoveAll(trigger => trigger.ID == triggerID);
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
