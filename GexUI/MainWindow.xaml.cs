using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace GexUI {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private Nullable<Point> nodeDragStart;
        private double zoomFactor = 1.05;

        public MainWindow() {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            NodeList.MouseDoubleClick += InstrumentsList_MouseDoubleClick;
            PatchEditor.MouseWheel += PatchEditor_MouseWheel;
        }

        private void node_MouseMove(object sender, MouseEventArgs e) {
            if (nodeDragStart != null && e.LeftButton == MouseButtonState.Pressed) {
                var node = sender as UIElement;
                var newPos = e.GetPosition(PatchEditor);
                Canvas.SetLeft(node, newPos.X - nodeDragStart.Value.X - node.RenderSize.Width);
                Canvas.SetTop(node, newPos.Y - nodeDragStart.Value.Y - node.RenderSize.Height);
            }
        }

        private void node_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            var node = sender as UIElement;
            nodeDragStart = null;
            node.ReleaseMouseCapture();
        }

        private void node_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            var node = sender as UIElement;
            nodeDragStart = e.GetPosition(node);
            node.CaptureMouse();
        }

        private void PatchEditor_MouseWheel(object sender, MouseWheelEventArgs e) {
            var transforms = (TransformGroup)PatchEditor.LayoutTransform;
            var scaler = (ScaleTransform)transforms.Children.First(transform => transform is ScaleTransform);

            if (e.Delta > 0) {
                scaler.ScaleX *= zoomFactor;
                scaler.ScaleY *= zoomFactor;
            } else {
                scaler.ScaleX /= zoomFactor;
                scaler.ScaleY /= zoomFactor;
            }
        }

        private void InstrumentsList_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            var list = (ListBox)sender;
            var node = new AudioNode((string)list.SelectedItem);
            node.Margin = new Thickness(100, 100, 0, 0);
            node.MouseLeftButtonDown += node_MouseLeftButtonDown;
            node.MouseLeftButtonUp += node_MouseLeftButtonUp;
            node.MouseMove += node_MouseMove;
            PatchEditor.Children.Add(node);
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e) {
            foreach (string name in GetAudioControls()) {
               NodeList.Items.Add(name);
            }
        }

        private IEnumerable<string> GetAudioControls() {
            foreach (var an in Assembly.GetExecutingAssembly().GetReferencedAssemblies()) {
                var asm = Assembly.Load(an);
                foreach (var type in asm.GetTypes()) {
                    if (type.GetCustomAttributes(typeof(AudioNodeAttribute), inherit: false).Length > 0) {
                        yield return type.Name;
                    }
                }
            }
        }
    }
}
