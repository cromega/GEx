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

        private bool Released;
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

            Task.Run(() => {
                for (; ; ) {
                    lock (Lock) {
                        Triggers.ForEach(trigger => {
                            switch (SignalType) {
                                case SignalType.Sine: Send(new Packet(trigger.ID, trigger.Triggered ? Control.Signal : Control.End, new Sample(Sine(trigger.Time++)))); break;
                                case SignalType.Square: Send(new Packet(trigger.ID, trigger.Triggered ? Control.Signal : Control.End, new Sample(Sine(trigger.Time++) > 0 ? 1 : -1))); break;
                                case SignalType.Noise: Send(new Packet(trigger.ID, trigger.Triggered ? Control.Signal : Control.End, new Sample(Noise()))); break;
                            }
                        });
                    }
                }
            });
        }

        public string Start(double frequency) {
            var trigger = new Trigger(frequency);
            Triggers.Add(trigger);
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

        private double Sine(int t) {
            return Math.Sin(Frequency * Math.PI * 2 * t / 44100);
        }

        private double Noise() {
            return Rnd.NextDouble() * 2 - 1;
        }
    }
}
