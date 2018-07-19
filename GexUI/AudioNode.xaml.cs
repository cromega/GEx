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
using GraphExperiment;
using System.Reflection;

namespace GexUI {
    /// <summary>
    /// Interaction logic for AudioNode.xaml
    /// </summary>
    partial class AudioNode : UserControl {
        private Nullable<Point> dragStartPosition;

        public AudioNode(string className) {
            InitializeComponent();
            Title.Text = className;
            DeleteButton.MouseLeftButtonDown += DeleteButton_MouseLeftButtonDown;
            MouseLeftButtonDown += MouseLeftButtonDownHandler;
            MouseLeftButtonUp += node_MouseLeftButtonUp;
            MouseMove += node_MouseMove;

            AddDynamicControls(className);
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

        private void AddDynamicControls(string className) {
            var members = Type.GetType(String.Format("GraphExperiment.{0},GraphExperiment", className), throwOnError: true).GetMembers();
            foreach (var member in members) {
                if (member.HasAttribute(typeof(AudioNodeParameterAttribute))) {
                    AddControlForNodeMember(member);
                }
            }
        }

        private void AddControlForNodeMember(MemberInfo member) {
            var nodeControlContainer = new GroupBox() { Header = member.Name };

            var memberType = member.GetMemberUnderlyingType();
            if (memberType.IsEnum) {
                var ctrl = new ComboBox();
                foreach (var enumValue in Enum.GetValues(memberType)) {
                    ctrl.Items.Add(enumValue);
                }
                ctrl.SelectedIndex = 0;
                nodeControlContainer.Content = ctrl;
                Container.Children.Add(nodeControlContainer);
            }
        }
    }
}
