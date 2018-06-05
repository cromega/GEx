namespace Chirpotle {
    partial class LFOControl {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.AmplitudeValue = new System.Windows.Forms.NumericUpDown();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.FrequencyValue = new System.Windows.Forms.NumericUpDown();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.waveSelector1 = new Chirpotle.WaveSelector();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.patchableValueSelector1 = new Chirpotle.PatchableValueSelector();
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AmplitudeValue)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FrequencyValue)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.patchableValueSelector1);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(177, 252);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "LFO";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.AmplitudeValue);
            this.groupBox5.Location = new System.Drawing.Point(6, 189);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(146, 48);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Amplitude";
            // 
            // AmplitudeValue
            // 
            this.AmplitudeValue.DecimalPlaces = 1;
            this.AmplitudeValue.Location = new System.Drawing.Point(6, 19);
            this.AmplitudeValue.Name = "AmplitudeValue";
            this.AmplitudeValue.Size = new System.Drawing.Size(128, 20);
            this.AmplitudeValue.TabIndex = 0;
            this.AmplitudeValue.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.FrequencyValue);
            this.groupBox4.Location = new System.Drawing.Point(6, 135);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(146, 48);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Frequency";
            // 
            // FrequencyValue
            // 
            this.FrequencyValue.DecimalPlaces = 1;
            this.FrequencyValue.Location = new System.Drawing.Point(6, 19);
            this.FrequencyValue.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.FrequencyValue.Name = "FrequencyValue";
            this.FrequencyValue.Size = new System.Drawing.Size(134, 20);
            this.FrequencyValue.TabIndex = 0;
            this.FrequencyValue.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.waveSelector1);
            this.groupBox3.Controls.Add(this.groupBox2);
            this.groupBox3.Location = new System.Drawing.Point(6, 81);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(146, 50);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Signal Type";
            // 
            // waveSelector1
            // 
            this.waveSelector1.Location = new System.Drawing.Point(6, 19);
            this.waveSelector1.Name = "waveSelector1";
            this.waveSelector1.Size = new System.Drawing.Size(134, 29);
            this.waveSelector1.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.numericUpDown1);
            this.groupBox2.Location = new System.Drawing.Point(0, 54);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(146, 46);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Frequency";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.DecimalPlaces = 1;
            this.numericUpDown1.Location = new System.Drawing.Point(6, 19);
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown1.TabIndex = 1;
            this.numericUpDown1.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // patchableValueSelector1
            // 
            this.patchableValueSelector1.Location = new System.Drawing.Point(6, 19);
            this.patchableValueSelector1.Name = "patchableValueSelector1";
            this.patchableValueSelector1.Size = new System.Drawing.Size(152, 73);
            this.patchableValueSelector1.TabIndex = 0;
            // 
            // LFOControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.groupBox1);
            this.Name = "LFOControl";
            this.Size = new System.Drawing.Size(184, 257);
            this.groupBox1.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.AmplitudeValue)).EndInit();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FrequencyValue)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.NumericUpDown AmplitudeValue;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.NumericUpDown FrequencyValue;
        private System.Windows.Forms.GroupBox groupBox3;
        private WaveSelector waveSelector1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private PatchableValueSelector patchableValueSelector1;
    }
}
