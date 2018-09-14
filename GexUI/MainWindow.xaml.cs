using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using GraphExperiment;

namespace GexUI {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window {
        private static double ZOOM_FACTOR = 1.1;
        private NodeIdGenerator IdGenerator;

        private Instrument Instrument;

        public MainWindow() {
            Logger.On();

            Instrument = new Instrument();

            InitializeComponent();
            DataContext = Instrument;

            NodeList.MouseDoubleClick += AddAudioNode;
            PatchEditor.MouseWheel += AdjustZoom;
            PatchEditor.MouseRightButtonDown += HighlightConnection;
            PatchEditor.MouseLeftButtonDown += PatchEditor_MouseLeftButtonDown;
            PatchEditor.KeyDown += PatchEditor_KeyDown;
            PatchEditor.KeyUp += PatchEditor_KeyUp;


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
            if (e.Key == Key.Delete) {
                Instrument.DeleteSelectedConnections();
                return;
            }

            if (e.IsRepeat) { return; }
            if (ActiveTriggeres.ContainsKey(e.Key)) { return; }
            if (!NoteKeys.Contains(e.Key)) { return; }

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

        private void HighlightConnection(object sender, MouseButtonEventArgs e) {
            var point = e.GetPosition(PatchEditor);
            Instrument.SelectConnectionNearTo(point);
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
                var controlName = NodeList.SelectedItem.ToString();

                var node = new AudioNode(controlName, IdGenerator.Next());
                node.NodeConnected += Node_NodeConnected;
                node.ControlRemoved += Node_ControlRemoved;
                Instrument.AddNode(node);
            } catch (Exception ex) {
                var result = MessageBox.Show(ex.Message, "Error", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.Cancel) { throw; }
            }
        }

        private void Node_ControlRemoved(object sender, EventArgs e) {
            var node = sender as AudioNode;
            Instrument.RemoveNode(node);
        }

        private void Node_NodeConnected(object sender, NodeConnectedEventArgs e) {
            var source = e.Target;
            var target = sender as AudioNode;

            var connection = new Connection(source, target);
            target.AudioControl.Connect(source.AudioControl);
            Instrument.Connect(source, target);
        }

        private void AddAudioControls() {
            foreach (var controlClass in GetAudioControls()) {
                NodeList.Items.Add(controlClass.Name);
            }
        }

        private List<Type> GetAudioControls() {
            var runningAssembly = Assembly.GetExecutingAssembly();

            var assemblyNames = new List<AssemblyName> {
                runningAssembly.GetName()
            };
            assemblyNames.AddRange(runningAssembly.GetReferencedAssemblies());

            return assemblyNames.
                SelectMany(name => Assembly.Load(name).GetTypes()).
                Where(type => type.HasAttribute(typeof(AudioNodeAttribute))).
                ToList();
        }
    }
}
