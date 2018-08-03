using System;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Controls;

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
            Wire.X1 = Canvas.GetLeft(Source) + Source.ActualWidth / 2;
            Wire.Y1 = Canvas.GetTop(Source);
            Wire.X2 = Canvas.GetLeft(Target) + Target.ActualWidth / 2;
            Wire.Y2 = Canvas.GetTop(Target) + Target.ActualHeight;
        }
    }
}
