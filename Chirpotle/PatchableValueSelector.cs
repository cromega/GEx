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
            PatchSelector.Items.Add("Volume");
            PatchSelector.Items.Add("Frequency");
            PatchSelector.SelectedIndex = 0;
            PatchTargetId = "v";
        }

        public string PatchTargetId;

        public void SetPatchTarget(string id) {
            switch (id) {
                case "v":
                    PatchSelector.SelectedItem = "Volume";
                    break;
                case "x":
                    PatchSelector.SelectedItem = "Frequency";
                    break;
            }
        }

        private void PatchSelector_SelectedIndexChanged(object sender, EventArgs e) {
            switch(PatchSelector.Items[PatchSelector.SelectedIndex]) {
                case "Volume": PatchTargetId = "v"; break;
                case "Frequency": PatchTargetId = "x"; break;
            }
        }
    }
}
