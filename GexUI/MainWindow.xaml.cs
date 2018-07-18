﻿using System;
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
    public class NodeParameter {
        public string Name;
        public bool Patchable;
        public Type ParameterType;
    }

    public partial class MainWindow : Window {
        private double zoomFactor = 1.05;

        public MainWindow() {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            NodeList.MouseDoubleClick += InstrumentsList_MouseDoubleClick;
            PatchEditor.MouseWheel += PatchEditor_MouseWheel;
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
            var nodeParams = new List<NodeParameter>();

            foreach (var type in GetAudioControls()) {
                type.GetMembers().Where(m => m.GetCustomAttributes(typeof(AudioNodeParameterAttribute)).Count() > 0).ToList().ForEach(p => {
                    nodeParams.Add(new NodeParameter { Name = p.Name, ParameterType = typeof(int), Patchable = false });
                });
            }

            var node = new AudioNode((string)list.SelectedItem, nodeParams);
            PatchEditor.Children.Add(node);
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e) {
            foreach (Type type in GetAudioControls()) {
               NodeList.Items.Add(type.Name);
            }
        }

        private IEnumerable<Type> GetAudioControls() {
            foreach (var an in Assembly.GetExecutingAssembly().GetReferencedAssemblies()) {
                var asm = Assembly.Load(an);
                foreach (var type in asm.GetTypes()) {
                    if (type.GetCustomAttributes(typeof(AudioNodeAttribute), inherit: false).Length > 0) {
                        yield return type;
                    }
                }
            }
        }
    }
}