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
    public partial class EnvelopeControl : UserControl {
        public int Attack {
            get { return Convert.ToInt32(AttackValue.Value); }
            set { AttackValue.Value = value; }
        }

        public int Decay {
            get { return Convert.ToInt32(DecayValue.Value); }
            set { DecayValue.Value = value; }
        }

        public double Sustain {
            get { return Convert.ToDouble(SustainValue.Value); }
            set { SustainValue.Value = (decimal)value; }
        }

        public int Release {
            get { return Convert.ToInt32(ReleaseValue.Value); }
            set { ReleaseValue.Value = value; }
        }

        public EnvelopeControl() {
            InitializeComponent();
        }

        public EnvelopeControl(string title) {
            InitializeComponent();
            groupBox1.Text = title;
        }
    }
}
