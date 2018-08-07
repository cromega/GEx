using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
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
using System.Collections.Specialized;
using GraphExperiment;

namespace GexUI {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window {
        private static double ZOOM_FACTOR = 1.1;
        private ObservableCollection<Connection> Connections;
        private NodeIdGenerator IdGenerator;

        public MainWindow() {
            Logger.On();
            InitializeComponent();
            NodeList.MouseDoubleClick += InstrumentsList_MouseDoubleClick;
            PatchEditor.MouseWheel += PatchEditor_MouseWheel;
            PatchEditor.MouseMove += PatchEditor_MouseMove;
            PatchEditor.MouseRightButtonDown += PatchEditor_MouseRightButtonDown;

            Connections = new ObservableCollection<Connection>();
            Connections.CollectionChanged += Connections_CollectionChanged;

            IdGenerator = new NodeIdGenerator();
            AddAudioControls();
        }

        private void PatchEditor_MouseRightButtonDown(object sender, MouseButtonEventArgs e) {
            var point = e.GetPosition(PatchEditor);
            DeleteConnectionCloseToClick(point);
        }

        private void DeleteConnectionCloseToClick(Point point) {
            foreach (var connection in Connections.ToList()) {
                var line = connection.Wire;
                var distance = Math.Abs((line.X2 - line.X1) * (line.Y1 - point.Y) - (line.X1 - point.X) * (line.Y2 - line.Y1)) / Math.Sqrt(Math.Pow(line.X2 - line.X1, 2) + Math.Pow(line.Y2 - line.Y1, 2));
                if (distance < 10) {
                    Connections.Remove(connection);
                    break;
                }
            }
        }

        private void Connections_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            switch (e.Action) {
                case NotifyCollectionChangedAction.Add:
                    var newWire = Connections[e.NewStartingIndex];
                    PatchEditor.Children.Add(newWire.Wire);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (Connection connection in e.OldItems) {
                        PatchEditor.Children.Remove(connection.Wire);
                    }
                    break;
            }
        }

        private void PatchEditor_MouseMove(object sender, MouseEventArgs e) {
            UpdateConnections();
        }

        private void UpdateConnections() {
            foreach (var connection in Connections) {
                connection.Update();
            }
        }

        private void PatchEditor_MouseWheel(object sender, MouseWheelEventArgs e) {
            var transforms = (TransformGroup)PatchEditor.LayoutTransform;
            var scaler = (ScaleTransform)transforms.Children.First(transform => transform is ScaleTransform);

            if (e.Delta > 0) {
                scaler.ScaleX *= ZOOM_FACTOR;
                scaler.ScaleY *= ZOOM_FACTOR;
            } else {
                scaler.ScaleX /= ZOOM_FACTOR;
                scaler.ScaleY /= ZOOM_FACTOR;
            }
        }

        private void InstrumentsList_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            var list = (ListBox)sender;
            var controlName = (string)list.SelectedItem;

            var node = new AudioNode(controlName, IdGenerator.Next());
            node.NodeConnected += Node_NodeConnected;
            node.ControlRemoved += Node_ControlRemoved;
            PatchEditor.Children.Add(node);
        }

        private void Node_ControlRemoved(object sender, EventArgs e) {
            var node = sender as AudioNode;
            PatchEditor.Children.Remove(node);

            foreach (var connection in Connections.ToList()) {
                if (connection.Source == node || connection.Target == node) { Connections.Remove(connection);  };
            }
        }

        private void Node_NodeConnected(object sender, NodeConnectedEventArgs e) {
            var source = sender as AudioNode;
            var target = e.Target;

            var connection = new Connection(source, target);
            Connections.Add(connection);
        }

        private void AddAudioControls() {
            foreach (Type type in GetAudioControls()) {
               NodeList.Items.Add(type.Name);
            }
        }

        private IEnumerable<Type> GetAudioControls() {
            foreach (var reference in Assembly.GetExecutingAssembly().GetReferencedAssemblies()) {
                var asm = Assembly.Load(reference);
                foreach (var type in asm.GetTypes()) {
                    if (type.HasAttribute(typeof(AudioNodeAttribute))) {
                        yield return type;
                    }
                }
            }
        }
    }
}
