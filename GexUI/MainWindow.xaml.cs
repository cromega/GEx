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
using System.Diagnostics;

namespace GexUI {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window {
        private static double ZOOM_FACTOR = 1.1;
        public ObservableCollection<Connection> Connections { get; set; }
        private NodeIdGenerator IdGenerator;

        private Instrument Instrument;

        public MainWindow() {
            Logger.On();

            Connections = new ObservableCollection<Connection>();

            InitializeComponent();
            NodeList.MouseDoubleClick += AddAudioNode;
            PatchEditor.MouseWheel += AdjustZoom;
            PatchEditor.MouseRightButtonDown += HighlightConnection;
            PatchEditor.MouseLeftButtonDown += PatchEditor_MouseLeftButtonDown;
            PatchEditor.KeyDown += PatchEditor_KeyDown;
            PatchEditor.KeyUp += PatchEditor_KeyUp;

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
            DeselectConnections();
        }

        private void DeselectConnections() {
            foreach (var conn in Connections) {
                conn.Deselect();
            }
        }

        private double GetNoteFrequency(int noteIndex) {
            return BASE_FREQUENCY * Math.Pow(1.059463094, noteIndex);
        }

        private void PatchEditor_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Delete) {
                DeleteSelectedConnetions();
                return;
            }

            if (e.IsRepeat) { return; }
            if (ActiveTriggeres.ContainsKey(e.Key)) { return; }
            if (!NoteKeys.Contains(e.Key)) { return; }

            var freq = GetNoteFrequency(48 + Array.IndexOf(NoteKeys, e.Key));
            var triggerId = Instrument.Start(freq);
            ActiveTriggeres.Add(e.Key, triggerId);
        }

        private void DeleteSelectedConnetions() {
            foreach (var conn in Connections.ToList()) {
                if (conn.IsSelected) { Connections.Remove(conn); }
            }
        }

        private void PatchEditor_KeyUp(object sender, KeyEventArgs e) {
            if (ActiveTriggeres.ContainsKey(e.Key)) {
                var triggerId = ActiveTriggeres[e.Key];
                Instrument.Release(triggerId);
                ActiveTriggeres.Remove(e.Key);
            }
        }

        private void HighlightConnection(object sender, MouseButtonEventArgs e) {
            var point = e.GetPosition(PatchEditor);
            var conn = Connections.FirstOrDefault(c => c.NearTo(point));
            if (conn == null) { return; }

            conn.Select();
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
                Instrument.AddNode(node);
                //(PatchEditor.Content as Canvas).Children.Add(node);
                PatchEditor.Children.Add(node);
            } catch (Exception ex) {
                var result = MessageBox.Show(ex.Message, "Error", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.Cancel) { throw; }
            }
        }

        private void Node_ControlRemoved(object sender, EventArgs e) {
            var node = sender as AudioNode;
            //(PatchEditor.Content as Canvas).Children.Remove(node);
            PatchEditor.Children.Remove(node);
            Instrument.RemoveNode(node);

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
            Debug.WriteLine("connection added");
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
