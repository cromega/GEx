using System;
using System.Collections.Generic;
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
using GraphExperiment;

namespace GexUI {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public class NodeParameter {
        public string Name;
        public bool Patchable;
        public Type ParameterType;
    }

    public partial class MainWindow : Window {
        private static double ZOOM_FACTOR = 1.1;
        private SoundSystem Audio;
        private Generator Generator;

        public MainWindow() {
            Logger.On();
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            NodeList.MouseDoubleClick += InstrumentsList_MouseDoubleClick;
            PatchEditor.MouseWheel += PatchEditor_MouseWheel;
            PatchEditor.Focus();

            Audio = new SoundSystem(2205);
            var instrument = new Instrument(Audio);
            Generator = new Generator(1, SignalType.Sine);
            var envelope = new Envelope(2, 44100, 0, 1.0, 44100);
            Generator.Connection = envelope.Input;
            instrument.First(Generator);
            instrument.Last(envelope);
            instrument.Run();
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

            var node = new AudioNode(controlName);
            PatchEditor.Children.Add(node);
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e) {
            foreach (Type type in GetAudioControls()) {
               NodeList.Items.Add(type.Name);
            }

            //var trigger = Generator.Start(440);
            //var stopper = new Thread(new ThreadStart(() => {
            //    Thread.Sleep(1000);
            //    Generator.Release(trigger);
            //}));
            //stopper.Start();
        }

        private IEnumerable<Type> GetAudioControls() {
            foreach (var reference in Assembly.GetExecutingAssembly().GetReferencedAssemblies()) {
                var asm = Assembly.Load(reference);
                foreach (var type in asm.GetTypes()) {
                    if (type.GetCustomAttributes(typeof(AudioNodeAttribute), inherit: false).Length > 0) {
                        yield return type;
                    }
                }
            }
        }
    }
}
