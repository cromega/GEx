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
            AddControls(className);
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

        private void AddControls(string className) {
            var container = new GroupBox() { Header = className };
            /*
             * namespace MySpace.Common.IO.JSON.Utilities
{
    internal static class ReflectionUtils
    {

        /// <summary>
        /// Gets the member's underlying type.
        /// </summary>
        /// <param name="member">The member.</param>
        /// <returns>The underlying type of the member.</returns>
        public static Type GetMemberUnderlyingType(MemberInfo member)
        {
            switch (member.MemberType)
            {
                case MemberTypes.Field:
                    return ((FieldInfo)member).FieldType;
                case MemberTypes.Property:
                    return ((PropertyInfo)member).PropertyType;
                case MemberTypes.Event:
                    return ((EventInfo)member).EventHandlerType;
                default:
                    throw new ArgumentException("MemberInfo must be if type FieldInfo, PropertyInfo or EventInfo", "member");
            }
        }
    }
}
*/

            var members = Type.GetType(String.Format("GraphExperiment.{0},GraphExperiment", className), throwOnError: true).GetMembers();
            foreach (var member in members) {
                if (member.GetCustomAttributes(typeof(AudioNodeParameterAttribute), false).Length == 0) { continue; }
                if (member.GetType().Name == "ASD") {
                    var ctrl = new ComboBox();
                    //foreach (var enumValue in Enum.GetValues(nodeParam.GetType())) {
                    //    ctrl.Items.Add(enumValue);
                    //}

                    container.Content = ctrl;
                }
            }
            Container.Children.Add(container);
        }
    }
}
