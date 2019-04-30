using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEx {
    public class NoteInfo {
        private static string[] Notes = new string[] { "c", "c#", "d", "d#", "e", "f", "f#", "g", "g#", "a", "a#", "b" };

        public int Machine;
        public string Note;
        public float Length;
        public int Octave;
        public double Frequency {
            get { return ConvertToFrequency(Note, Octave); }
        }

        public NoteInfo(string noteData) {
            var parts = noteData.Split(',');
            Machine = Int32.Parse(parts[0]);
            Octave = Int32.Parse(parts[1].Substring(parts[1].Length - 1, 1));
            Note = parts[1].Remove(parts[1].Length - 1);
            Length = float.Parse(parts[2]);
        }


        private double ConvertToFrequency(string note, int Octave) {
            var noteIndex = Array.IndexOf(Notes, note) + Octave * 12;
            return 16.35 * Math.Pow(1.059463094, noteIndex);
        }
    }

    public class Track {
        double Tempo;
        private int Tick;
        private Machine[] Machines;
        private string[] Lines;
        private List<Trigger> Triggers;
        private int LastLineIndex;

        public Track(string[] trackData) {
            Tick = 0;
            LoadTrack(trackData);
            Triggers = new List<Trigger>();
            LastLineIndex = -1;
            Tempo = 2;
        }

        public Packet[] Next() {
            if (LineCounter() < Lines.Length && LineCounter() != LastLineIndex) {
                Lines[LineCounter()].
                    Split(';').
                    Where(note => note != "-").
                    Where(note => !string.IsNullOrEmpty(note)).
                    Select(note => new NoteInfo(note)).
                    ToList().
                    ForEach(note => Triggers.Add(new Trigger(Machines[note.Machine], note.Frequency, (int)(Tempo * 4410 / note.Length))));

                LastLineIndex++;
            }

            var packets = new List<Packet>();
            foreach (var trigger in Triggers) {
                packets.Add(trigger.Next(Tick));
            }
            Tick++;
            Triggers.Where(t => t.Dead).ToList().ForEach(t => Triggers.Remove(t));
            return packets.ToArray();
        }

        private int LineCounter() {
            return (int)(Tick / (4410 * Tempo));
        }

        public bool HasEnded() {
            return LineCounter() > Lines.Length && Triggers.Count == 0;
        }

        private void LoadTrack(string[] data) {
            var parser = new Parser();
            Machines = data.
                Where(line => line.StartsWith(">")).
                Select(parser.ParseMachine).
                ToArray();

            Lines = data.Where(line => !line.StartsWith(">")).ToArray();
        }
    }
}
