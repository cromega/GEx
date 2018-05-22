using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chirpotle {
    public partial class InstrumentEditor : Form {
        public string InstrumentData;

        public InstrumentEditor() {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, EventArgs e) {
            var instr = new StringBuilder();
            if (SineGeneratorButton.Checked) {
                instr.Append("1:");
            } else if (SquareGeneratorButton.Checked) {
                instr.Append("2:");
            } else if (NoiseGeneratorButton.Checked) {
                instr.Append("0:");
            }
            instr.AppendFormat("{0}:", VolumeValue.Value);
            instr.AppendFormat("{0},{1},{2},{3}:", AttackValue.Value, DecayValue.Value, SustainValue.Value, ReleaseValue.Value);
            
            if (LFO1OnCheckbox.Checked) {
                instr.AppendFormat("l");
                if (LFO1SineGeneratorButton.Checked) {
                    instr.Append("1-");
                } else if (LFO1SquareGeneratorButton.Checked) {
                    instr.Append("2-");
                } else if (LFO1NoiseGeneratorButton.Checked) {
                    instr.Append("0-");
                }
                instr.AppendFormat("{0}-", LFO1FrequencyValue.Value);
                instr.Append(LFO1RoutingSelector.SelectedIndex);
            }
            instr.Append(":");
            if (LFO2OnCheckbox.Checked) {
                instr.AppendFormat("l");
                if (LFO2SineGeneratorButton.Checked) {
                    instr.Append("1-");
                } else if (LFO2SquareGeneratorButton.Checked) {
                    instr.Append("2-");
                } else if (LFO2NoiseGeneratorButton.Checked) {
                    instr.Append("0-");
                }
                instr.AppendFormat("{0}-", LFO2FrequencyValue.Value);
                instr.Append(LFO2RoutingSelector.SelectedIndex);
            }

            InstrumentData = instr.ToString();
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
