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
        }

        public int Decay {
            get { return Convert.ToInt32(DecayValue.Value); }
        }

        public double Sustain {
            get { return Convert.ToDouble(SustainValue.Value); }
        }

        public int Release {
            get { return Convert.ToInt32(ReleaseValue.Value); }
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
