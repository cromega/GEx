using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphExperiment {
    class Track {
        private int Tick;
        private Machine[] Machines;
        private Muxer Muxer;

        public Track(string trackData, Machine[] machines) {
            Tick = 0;
            Machines = machines;
            Muxer = new Muxer();
        }

        public Packet[] Next(int size) {
            var packets = new Packet[size];

            for (var i=0; i<size; i++) {
                for (var m = 0; m < Machines.Length; m++) {
                }
            }
            return null;
        }

    }
}
