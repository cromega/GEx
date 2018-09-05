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
            PatchEditor.MouseLeftButtonDown += PatchEditor_MouseLeftButtonDown;
            PatchEditor.KeyDown += PatchEditor_KeyDown;
            PatchEditor.KeyUp += PatchEditor_KeyUp;

            Connections = new ObservableCollection<Connection>();
            Connections.CollectionChanged += UpdateConnections;
            Instrument = new Instrument();

            IdGenerator = new NodeIdGenerator();
            AddAudioControls();
        }

        private const double BASE_FREQUENCY = 16.35d;
        private Key[] NoteKeys = new Key[] {
            Key.Q,
            Key.A,
            Key.W,
            Key.S,
            Key.E,
            Key.R,
            Key.F,
            Key.T,
            Key.G,
            Key.Y,
            Key.H,
            Key.U,
            Key.I,
        };
        private Dictionary<Key, string> ActiveTriggeres = new Dictionary<Key, string>();

        private void PatchEditor_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            PatchEditor.Focus();
        }

        private double GetNoteFrequency(int noteIndex) {
            return BASE_FREQUENCY * Math.Pow(1.059463094, noteIndex);
        }

        private void PatchEditor_KeyDown(object sender, KeyEventArgs e) {
            if (e.IsRepeat) { return; }

            var freq = GetNoteFrequency(48 + Array.IndexOf(NoteKeys, e.Key));
            var triggerId = Instrument.Start(freq);
            ActiveTriggeres.Add(e.Key, triggerId);
        }

        private void PatchEditor_KeyUp(object sender, KeyEventArgs e) {
            if (ActiveTriggeres.ContainsKey(e.Key)) {
                var triggerId = ActiveTriggeres[e.Key];
                Instrument.Release(triggerId);
                ActiveTriggeres.Remove(e.Key);
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
                if (connection.IsAttachedTo(node)) { Connections.Remove(connection); }
            }
        }

        private void Node_NodeConnected(object sender, NodeConnectedEventArgs e) {
            var source = e.Target;
            var target = sender as AudioNode;

            var connection = new Connection(source, target);
            target.AudioControl.Connect(source.AudioControl);
            Connections.Add(connection);
        }

        private void AddAudioControls() {
            foreach (Type type in GetAudioControls()) {
               NodeList.Items.Add(type.Name);
            }
        }

        private IEnumerable<Type> GetAudioControls() {
            var assemblyNames = new List<AssemblyName> {
                Assembly.GetExecutingAssembly().GetName()
            };
            assemblyNames.AddRange(Assembly.GetExecutingAssembly().GetReferencedAssemblies());

            foreach (var name in assemblyNames) {
                var asm = Assembly.Load(name);
                foreach (var type in asm.GetTypes()) {
                    if (type.HasAttribute(typeof(AudioNodeAttribute))) {
                        yield return type;
                    }
                }
            }
        }
    }
}
