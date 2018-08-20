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

        private Instrument Instrument;

        public MainWindow() {
            Logger.On();
            InitializeComponent();
            NodeList.MouseDoubleClick += AddAudioNode;
            PatchEditor.MouseWheel += AdjustZoom;
            PatchEditor.MouseMove += PatchEditor_MouseMove;
            PatchEditor.MouseRightButtonDown += DeleteConnection;

            Connections = new ObservableCollection<Connection>();
            Connections.CollectionChanged += UpdateConnections;

            IdGenerator = new NodeIdGenerator();
            AddAudioControls();
            Instrument = new Instrument(new SoundSystem(2205));
            Task.Run(() => Instrument.Run());

            BeepButton.Click += BeepButton_Click;
        }

        private void BeepButton_Click(object sender, RoutedEventArgs e) {
            try {
                var trigger = Instrument.Start(440);
                Task.Run(() => {
                    Thread.Sleep(1000);
                    Instrument.Release(trigger);
                });
            } catch {
                MessageBox.Show("wtf");
            }
        }

        private void DeleteConnection(object sender, MouseButtonEventArgs e) {
            var point = e.GetPosition(PatchEditor);

            foreach (var connection in Connections.ToList()) {
                var line = connection.Wire;
                var distance = Math.Abs((line.X2 - line.X1) * (line.Y1 - point.Y) - (line.X1 - point.X) * (line.Y2 - line.Y1)) / Math.Sqrt(Math.Pow(line.X2 - line.X1, 2) + Math.Pow(line.Y2 - line.Y1, 2));
                if (distance < 10) {
                    Connections.Remove(connection);
                    break;
                }
            }
        }

        private void UpdateConnections(object sender, NotifyCollectionChangedEventArgs e) {
            switch (e.Action) {
                case NotifyCollectionChangedAction.Add:
                    var newWire = Connections[e.NewStartingIndex];
                    PatchEditor.Children.Add(newWire.Wire);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (Connection connection in e.OldItems) {
                        PatchEditor.Children.Remove(connection.Wire);
                        connection.Target.AudioControl.Previous = null;
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

        private void AdjustZoom(object sender, MouseWheelEventArgs e) {
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

        private void AddAudioNode(object sender, MouseButtonEventArgs e) {
            try {
                var list = (ListBox)sender;
                var controlName = (string)list.SelectedItem;

                var node = new AudioNode(controlName, IdGenerator.Next());
                node.NodeConnected += Node_NodeConnected;
                node.ControlRemoved += Node_ControlRemoved;
                Instrument.AddNode(node.AudioControl);
                PatchEditor.Children.Add(node);
            } catch (Exception ex) {
                var result = MessageBox.Show(ex.Message, "Error", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.Cancel) { throw; }
            }
        }

        private void Node_ControlRemoved(object sender, EventArgs e) {
            var node = sender as AudioNode;
            PatchEditor.Children.Remove(node);

            foreach (var connection in Connections.ToList()) {
                if (connection.Source == node || connection.Target == node) { Connections.Remove(connection); }
            }
        }

        private void Node_NodeConnected(object sender, NodeConnectedEventArgs e) {
            var source = e.Target;
            var target = sender as AudioNode;

            var connection = new Connection(source, target);
            source.AudioControl.Connect(target.AudioControl);
            Connections.Add(connection);
        }

        private void AddAudioControls() {
            foreach (Type type in GetAudioControls()) {
               NodeList.Items.Add(type.Name);
            }
        }

        private IEnumerable<Type> GetAudioControls() {
            foreach (var type in Assembly.GetExecutingAssembly().GetTypes()) {
                if (type.HasAttribute(typeof(AudioNodeAttribute))) {
                    yield return type;
                }
            }

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
