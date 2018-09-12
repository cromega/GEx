using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GraphExperiment;
using System.Reflection;
using System.Windows.Shapes;
using System.Linq;
using System.Windows.Media;
using System.Collections.Generic;

namespace GexUI {
    /// <summary>
    /// Interaction logic for AudioNode.xaml
    /// </summary>
    public class NodeConnectedEventArgs : EventArgs {
        public AudioNode Target;

        public NodeConnectedEventArgs(AudioNode target) {
            Target = target;
        }
    }

    partial class AudioNode : UserControl {
        public readonly GraphExperiment.AudioNode AudioControl;
        private short NodeId;

        private Point? dragStartPosition;
        private Dictionary<UIElement, string> DynamicControls;
        public event EventHandler<NodeConnectedEventArgs> NodeConnected;
        public event EventHandler ControlRemoved;
        public event EventHandler PositionChanged;

        public AudioNode(string className, short id) {
            InitializeComponent();
            Title.Content = className;

            DeleteButton.Click += DeleteButton_Click;
            MouseLeftButtonDown += MouseLeftButtonDownHandler;
            MouseLeftButtonUp += Node_MouseLeftButtonUp;
            MouseMove += Node_MouseMove;
            OutputAnchor.MouseDown += OutputAnchor_MouseDown;
            Node.Drop += Node_Drop;

            Canvas.SetLeft(this, 0);
            Canvas.SetTop(this, 0);

            SetAnchors(className);

            AddDynamicControls(className);
            NodeId = id;
            AudioControl = CreateAudioControl(className);
        }

        public string Type() {
            return AudioControl.Type();
        }

        private void SetAnchors(string className) {
            var type = Utils.GetControlType(className);
            var attr = type.GetCustomAttribute<AudioNodeAttribute>() as AudioNodeAttribute;
            switch (attr.Direction) {
                case AudioNodeDirection.InputOnly:
                    OutputAnchor.Visibility = Visibility.Hidden; break;
                case AudioNodeDirection.OutputOnly:
                    InputAnchor.Visibility = Visibility.Hidden; break;
            }
        }

        private GraphExperiment.AudioNode CreateAudioControl(string className) {
            var type = Utils.GetControlType(className);
            var control = type.GetConstructors().First().Invoke(new object[] { NodeId });
            return (GraphExperiment.AudioNode)control;
        }

        private void Node_Drop(object sender, DragEventArgs e) {
            var target = e.Data.GetData("stuff") as AudioNode;
            if (target == null) {
                return;
            }

            var args = new NodeConnectedEventArgs(target);
            NodeConnected(this, args);
        }

        private void OutputAnchor_MouseDown(object sender, MouseButtonEventArgs e) {
            var anchor = (DependencyObject)sender;
            DragDrop.DoDragDrop(anchor, new DataObject("stuff", this), DragDropEffects.Link);
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e) {
            EventHandler removedEvent = ControlRemoved;
            removedEvent(this, EventArgs.Empty);
        }

        private void MouseLeftButtonDownHandler(object sender, MouseButtonEventArgs e) {
            var point = e.GetPosition(this);
            // on drag drop a mouse button down is triggered with weird coordinates, ignore it
            if (point.X < 0 || point.Y < 0) { return; }
            dragStartPosition = point;
            CaptureMouse();
        }

        private void Node_MouseMove(object sender, MouseEventArgs e) {
            if (dragStartPosition != null && e.LeftButton == MouseButtonState.Pressed) {
                var parent = VisualTreeHelper.GetParent(this);
                var newPos = e.GetPosition(parent as UIElement);
                Canvas.SetLeft(this, newPos.X - dragStartPosition.Value.X);
                Canvas.SetTop(this, newPos.Y - dragStartPosition.Value.Y);
                PositionChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private void Node_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            dragStartPosition = null;
            ReleaseMouseCapture();
        }

        private void AddDynamicControls(string className) {
            DynamicControls = new Dictionary<UIElement, string>();

            var type = Utils.GetControlType(className);
            var members = from m in type.GetMembers()
                          where m.HasAttribute(typeof(AudioNodeParameterAttribute))
                          select m;

            foreach (var member in members) {
                var control = CreateDynamicControl(member);
                Container.Children.Add(control);
            }
        }

        private UIElement CreateDynamicControl(MemberInfo member) {
            var nodeControlContainer = new GroupBox() { Header = member.Name };

            UIElement nodeControl = null;
            var memberType = member.GetMemberUnderlyingType();
            if (memberType.IsEnum) {
                var control = new ComboBox();
                foreach (var enumValue in Enum.GetValues(memberType)) {
                    control.Items.Add(enumValue);
                }
                control.SelectedIndex = 0;
                control.SelectionChanged += UpdateDynamicFields;
                nodeControl = control;
            } else if (memberType == typeof(double) || memberType == typeof(int)) {
                var control = new TextBox();
                control.TextChanged += UpdateDynamicFields;
                nodeControl = control;
            } else {
                throw new NotImplementedException();
            }

            DynamicControls.Add(nodeControl, member.Name);
            nodeControlContainer.Content = nodeControl;
            return nodeControlContainer;
        }

        private void UpdateDynamicFields(object sender, EventArgs e) {
            var memberName = DynamicControls[sender as UIElement];
            var field = AudioControl.GetType().GetField(memberName);
            var fieldType = field.GetMemberUnderlyingType();

            try {
                switch (sender.GetType().Name) {
                    case "ComboBox": {
                            var ctrl = sender as ComboBox;
                            var value = ctrl.SelectedItem;
                            field.SetValue(AudioControl, value);
                            break;
                        }
                    case "TextBox": {
                            var ctrl = sender as TextBox;
                            var value = ctrl.Text;
                            if (field.GetMemberUnderlyingType() == typeof(double)) {
                                field.SetValue(AudioControl, double.Parse(value));
                            } else if (field.GetMemberUnderlyingType() == typeof(int)) {
                                field.SetValue(AudioControl, int.Parse(value));
                            } else {
                                throw new NotImplementedException();
                            }
                            break;
                        }
                    default:
                        throw new NotImplementedException();
                }
            } catch (FormatException) {
                MessageBox.Show("wrong value");
            }
        }
    }
}
