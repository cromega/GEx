using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chirpotle {
    public partial class PatchableValueSelector : UserControl {
        public PatchableValueSelector() {
            InitializeComponent();
        }

        public string PatchTargetId;

        private void PatchableValueSelector_Load(object sender, EventArgs e) {
            PatchSelector.Items.Add("Volume");
            PatchSelector.Items.Add("Frequency");
            PatchSelector.SelectedIndex = 0;
            PatchTargetId = "v";
        }

        private void PatchSelector_SelectedValueChanged(object sender, EventArgs e) {
        }

        private void PatchSelector_SelectedIndexChanged(object sender, EventArgs e) {
            switch(PatchSelector.Items[PatchSelector.SelectedIndex]) {
                case "Volume": PatchTargetId = "v"; break;
                case "Frequency": PatchTargetId = "x"; break;
            }
        }
    }
}
