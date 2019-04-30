using System;
using System.Collections.Generic;
using System.Linq;

namespace GEx{
    public class Trigger {
        private Machine Machine;
        private double Frequency;
        private string Id;
        public bool IsActive {
            get { return TTL > 0; }
        }
        public bool Dead;
        private int TTL;
        private long Tick;

        public Trigger(Machine machine, double frequency, int ttl) {
            Id = Guid.NewGuid().ToString();
            Machine = machine;
            Frequency = frequency;
            TTL = ttl;
            Dead = false;
            Tick = 0;
        }

        public Packet Next(long tick) {
            var packet = Machine.Process(
                tick,
                new Packet(Id, IsActive ? Signal.Active : Signal.End, new Sample(Frequency), tick, Tick++));
            Dead = packet.Signal == Signal.End;
            TTL--;
            return packet;
        }
    }
}
