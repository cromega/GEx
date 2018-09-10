using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows;
using System;

namespace GexUI {
    public class Connection {
        public AudioNode Source;
        public AudioNode Target;
        public Line Wire;
        public bool IsSelected;

        private SolidColorBrush Stroke;

        public Connection(AudioNode source, AudioNode target) {
            Source = source;
            Target = target;
            Stroke = new SolidColorBrush(Colors.Black);
            Wire = new Line() {
                Stroke = Stroke,
                StrokeThickness = 3,
            };

            Update();
            IsSelected = false;
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

        public bool NearTo(Point point) {
            var distance = Math.Abs((Wire.X2 - Wire.X1) * (Wire.Y1 - point.Y) - (Wire.X1 - point.X) * (Wire.Y2 - Wire.Y1)) / Math.Sqrt(Math.Pow(Wire.X2 - Wire.X1, 2) + Math.Pow(Wire.Y2 - Wire.Y1, 2));
            return distance < 10;
        }

        public void Select() {
            Stroke.Color = Colors.Yellow;
            IsSelected = true;
        }

        public void Deselect() {
            Stroke.Color = Colors.Black;
            IsSelected = false;
        }
    }
}
