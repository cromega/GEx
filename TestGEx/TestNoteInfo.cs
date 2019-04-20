using GraphExperiment;
using Xunit;
using FluentAssertions;

namespace TestGEx {
    public class TestNoteInfo {
        [Fact]
        public void TestParsingNotesWithFlatNote() {
            var note = new NoteInfo("3,a4,32");
            note.Octave.Should().Be(4);
            note.Note.Should().Be("a");
            note.Frequency.Should().BeApproximately(440, 0.1);
            note.Machine.Should().Be(3);
        }

        [Fact]
        public void TestParsingNotesWithSharpNote() {
            var note = new NoteInfo("1,c#2,32");
            note.Octave.Should().Be(2);
            note.Note.Should().Be("c#");
            note.Machine.Should().Be(1);
        }
    }
}
