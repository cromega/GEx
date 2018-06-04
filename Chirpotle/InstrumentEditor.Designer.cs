namespace Chirpotle {
    partial class InstrumentEditor {
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.SaveButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.VolumeValue = new System.Windows.Forms.NumericUpDown();
            this.NameEdit = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.waveSelector1 = new Chirpotle.WaveSelector();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VolumeValue)).BeginInit();
            this.SuspendLayout();
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(250, 14);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 7;
            this.SaveButton.Text = "Done";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Volume";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.VolumeValue);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(28, 77);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 37);
            this.panel1.TabIndex = 13;
            // 
            // VolumeValue
            // 
            this.VolumeValue.DecimalPlaces = 2;
            this.VolumeValue.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.VolumeValue.Location = new System.Drawing.Point(61, 8);
            this.VolumeValue.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.VolumeValue.Name = "VolumeValue";
            this.VolumeValue.Size = new System.Drawing.Size(120, 20);
            this.VolumeValue.TabIndex = 10;
            this.VolumeValue.Value = new decimal(new int[] {
            2,
            0,
            0,
            65536});
            // 
            // NameEdit
            // 
            this.NameEdit.Location = new System.Drawing.Point(66, 16);
            this.NameEdit.Name = "NameEdit";
            this.NameEdit.Size = new System.Drawing.Size(100, 20);
            this.NameEdit.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Name";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.YellowGreen;
            this.panel2.Location = new System.Drawing.Point(45, 278);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(669, 71);
            this.panel2.TabIndex = 16;
            this.panel2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel2_MouseClick);
            // 
            // waveSelector1
            // 
            this.waveSelector1.Location = new System.Drawing.Point(28, 42);
            this.waveSelector1.Name = "waveSelector1";
            this.waveSelector1.Size = new System.Drawing.Size(150, 29);
            this.waveSelector1.TabIndex = 10;
            // 
            // InstrumentEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.ControlBox = false;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.NameEdit);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.waveSelector1);
            this.Controls.Add(this.SaveButton);
            this.KeyPreview = true;
            this.Name = "InstrumentEditor";
            this.Text = "InstrumentEditor";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.InstrumentEditor_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.InstrumentEditor_KeyUp);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VolumeValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Label label1;
        private WaveSelector waveSelector1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.NumericUpDown VolumeValue;
        private System.Windows.Forms.TextBox NameEdit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
    }
}