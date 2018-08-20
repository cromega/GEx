using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows;

namespace GexUI {
    public class Connection {
        public AudioNode Source;
        public AudioNode Target;
        public Line Wire;

        public Connection(AudioNode source, AudioNode target) {
            Source = source;
            Target = target;
            Wire = new Line() {
                Stroke = new SolidColorBrush(Colors.Black),
                StrokeThickness = 3,
            };
            Update();
        }

        public void Update() {
            //FIXME contain this logic in the nodes probably
            Wire.X2 = Canvas.GetLeft(Source) + Source.ActualWidth;
            Wire.Y2 = Canvas.GetTop(Source) + Source.ActualHeight / 2;
            Wire.X1 = Canvas.GetLeft(Target);
            Wire.Y1 = Canvas.GetTop(Target) + Target.ActualHeight / 2;
        }

        public bool IsAttachedTo(AudioNode node) {
            return Source == node || Target == node;
        }
    }
}
