using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chirpesizer {
    public struct Node {
        public int InstrumentIndex;
        public double Length;
        public double Frequency;
        
        public static Node Parse(string data) {
            var parts = data.Split(";".ToCharArray());
            var index = int.Parse(parts[0]);
            var length = double.Parse(parts[1]);
            var frequency = GetNoteFrequency(int.Parse(parts[2]));
            return new Node() { InstrumentIndex = index, Length = length, Frequency = frequency };
        }

        public static Node[] ParseAll(string line) {
            var nodes = new List<Node>();
            if (line == "-") { return nodes.ToArray(); }

            var nodeDescriptions = line.Split(" ".ToCharArray());
            foreach(string nodeDescription in nodeDescriptions) {
                nodes.Add(Parse(nodeDescription));
            }

            return nodes.ToArray();
        }

        private static double GetNoteFrequency(int noteIndex) {
            return 16.35 * Math.Pow(1.059463094, noteIndex - 1);
        }
    }
}
