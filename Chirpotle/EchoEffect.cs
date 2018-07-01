using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Chirpesizer.Effects;

namespace Chirpotle {
    public partial class EchoEffect : UserControl, IEffectControl {
        public EchoEffect() {
            InitializeComponent();
        }

        public IEffect GetEffect() {
            return new Reverb((int)DelayValue.Value, (double)FeedbackValue.Value);
        }

        private void button1_Click(object sender, EventArgs e) {
            Parent.Controls.Remove(this);
            Dispose(true);
        }
    }
}
