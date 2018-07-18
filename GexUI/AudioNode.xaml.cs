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

namespace GexUI {
    /// <summary>
    /// Interaction logic for AudioNode.xaml
    /// </summary>
    partial class AudioNode : UserControl {
        public AudioNode(string name, List<NodeParameter> nodeParams) {
            InitializeComponent();
            Border.Header = name;
            AddControls(nodeParams);
        }

        private void AddControls(List<NodeParameter> nodeParams) {
            foreach (var node in nodeParams) {
                AddParam(node);
            }
        }

        private void AddParam(NodeParameter node) {
            var box = new GroupBox() { Header = node.Name };
            var input = new TextBox();
            box.Content = input;
            Container.Children.Add(box);
        }
    }
}
