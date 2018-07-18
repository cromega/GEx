using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GexUI {
    /// <summary>
    /// Interaction logic for AudioNode.xaml
    /// </summary>
    partial class AudioNode : UserControl {
        private Nullable<Point> dragStartPosition;

        public AudioNode(string name, List<NodeParameter> nodeParams) {
            InitializeComponent();
            Title.Text = name;
            AddControls(nodeParams);
            DeleteButton.MouseLeftButtonDown += DeleteButton_MouseLeftButtonDown;
            MouseLeftButtonDown += MouseLeftButtonDownHandler;
            MouseLeftButtonUp += node_MouseLeftButtonUp;
            MouseMove += node_MouseMove;
        }

        private void MouseLeftButtonDownHandler(object sender, MouseButtonEventArgs e) {
            var node = sender as UIElement;
            dragStartPosition = e.GetPosition(node);
            node.CaptureMouse();
        }

        private void node_MouseMove(object sender, MouseEventArgs e) {
            if (dragStartPosition != null && e.LeftButton == MouseButtonState.Pressed) {
                var node = sender as UIElement;
                var newPos = e.GetPosition(Parent as UIElement);
                Canvas.SetLeft(node, newPos.X - dragStartPosition.Value.X);
                Canvas.SetTop(node, newPos.Y - dragStartPosition.Value.Y);
            }
        }

        private void node_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            var node = sender as UIElement;
            dragStartPosition = null;
            node.ReleaseMouseCapture();
        }

        private void DeleteButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            (Parent as Panel).Children.Remove(this);

        }

        private void AddControls(List<NodeParameter> nodeParams) {
            foreach (var node in nodeParams) {
                AddParam(node);
            }
        }

        private void AddParam(NodeParameter node) {
            var box = new GroupBox() { Header = node.Name };
            var input = new TextBox();
            box.Content = input;
            Container.Children.Add(box);
        }
    }
}
