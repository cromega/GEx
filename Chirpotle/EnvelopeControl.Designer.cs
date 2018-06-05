namespace Chirpotle {
    partial class EnvelopeControl {
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
            this.SustainValue = new System.Windows.Forms.NumericUpDown();
            this.ReleaseValue = new System.Windows.Forms.NumericUpDown();
            this.DecayValue = new System.Windows.Forms.NumericUpDown();
            this.AttackValue = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SustainValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReleaseValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DecayValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AttackValue)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.SustainValue);
            this.groupBox1.Controls.Add(this.ReleaseValue);
            this.groupBox1.Controls.Add(this.DecayValue);
            this.groupBox1.Controls.Add(this.AttackValue);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(180, 136);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Envelope";
            // 
            // SustainValue
            // 
            this.SustainValue.DecimalPlaces = 1;
            this.SustainValue.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.SustainValue.Location = new System.Drawing.Point(36, 97);
            this.SustainValue.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.SustainValue.Name = "SustainValue";
            this.SustainValue.Size = new System.Drawing.Size(120, 20);
            this.SustainValue.TabIndex = 8;
            this.SustainValue.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            // 
            // ReleaseValue
            // 
            this.ReleaseValue.Location = new System.Drawing.Point(36, 71);
            this.ReleaseValue.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.ReleaseValue.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.ReleaseValue.Name = "ReleaseValue";
            this.ReleaseValue.Size = new System.Drawing.Size(120, 20);
            this.ReleaseValue.TabIndex = 7;
            this.ReleaseValue.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // DecayValue
            // 
            this.DecayValue.Location = new System.Drawing.Point(36, 45);
            this.DecayValue.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.DecayValue.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.DecayValue.Name = "DecayValue";
            this.DecayValue.Size = new System.Drawing.Size(120, 20);
            this.DecayValue.TabIndex = 6;
            this.DecayValue.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // AttackValue
            // 
            this.AttackValue.Location = new System.Drawing.Point(36, 19);
            this.AttackValue.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.AttackValue.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.AttackValue.Name = "AttackValue";
            this.AttackValue.Size = new System.Drawing.Size(120, 20);
            this.AttackValue.TabIndex = 5;
            this.AttackValue.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "S";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "R";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "D";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "A";
            // 
            // EnvelopeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.groupBox1);
            this.Name = "EnvelopeControl";
            this.Size = new System.Drawing.Size(189, 146);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SustainValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReleaseValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DecayValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AttackValue)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown SustainValue;
        private System.Windows.Forms.NumericUpDown ReleaseValue;
        private System.Windows.Forms.NumericUpDown DecayValue;
        private System.Windows.Forms.NumericUpDown AttackValue;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
    }
}
