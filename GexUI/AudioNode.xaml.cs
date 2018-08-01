using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GraphExperiment;
using System.Reflection;
using System.Windows.Shapes;
using System.Linq;
using System.Windows.Media;

namespace GexUI {
    /// <summary>
    /// Interaction logic for AudioNode.xaml
    /// </summary>
    partial class AudioNode : UserControl {
        private Nullable<Point> dragStartPosition;

        public AudioNode(string className) {
            InitializeComponent();
            Title.Text = className;

            DeleteButton.Click += DeleteButton_Click;
            MouseLeftButtonDown += MouseLeftButtonDownHandler;
            MouseLeftButtonUp += node_MouseLeftButtonUp;
            MouseMove += node_MouseMove;
            OutputAnchor.MouseDown += OutputAnchor_MouseDown;
            Node.Drop += Node_Drop;

            AddDynamicControls(className);
        }

        private void Node_Drop(object sender, DragEventArgs e) {
            var canvas = (Parent as Canvas);
            var line = new Line();
            var output = e.Data.GetData("stuff") as Ellipse;
            Console.WriteLine("stuff dropped");
            (sender as UIElement).ReleaseMouseCapture();
            dragStartPosition = null;
        }

        private void OutputAnchor_MouseDown(object sender, MouseButtonEventArgs e) {
            var anchor = (DependencyObject)sender;
            DragDrop.DoDragDrop(anchor, new DataObject("stuff", anchor), DragDropEffects.Move);
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e) {
            (Parent as Panel).Children.Remove(this);
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

        private void AddDynamicControls(string className) {
            string fullClassName = String.Format("GraphExperiment.{0},GraphExperiment", className);
            var members = from m in Type.GetType(fullClassName, throwOnError: true).GetMembers()
                          where m.HasAttribute(typeof(AudioNodeParameterAttribute))
                          select m;

            foreach (var member in members) {
                AddDynamicControl(member);
            }
        }

        private void AddDynamicControl(MemberInfo member) {
            var nodeControlContainer = new GroupBox() { Header = member.Name };

            var memberType = member.GetMemberUnderlyingType();
            if (memberType.IsEnum) {
                var ctrl = new ComboBox();
                foreach (var enumValue in Enum.GetValues(memberType)) {
                    ctrl.Items.Add(enumValue);
                }
                ctrl.SelectedIndex = 0;
                nodeControlContainer.Content = ctrl;
            } else if (memberType == typeof(double) || memberType == typeof(int)) {
                nodeControlContainer.Content = new TextBox();
            }

            Container.Children.Add(nodeControlContainer);
        }
    }
}
