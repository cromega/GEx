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
            this.TestPanel = new System.Windows.Forms.Panel();
            this.EffectsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modlulatorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lFOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.envelopeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.effectsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.echoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.waveSelector1 = new Chirpotle.WaveSelector();
            this.OctaveValue = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VolumeValue)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OctaveValue)).BeginInit();
            this.SuspendLayout();
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(839, 52);
            this.SaveButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(100, 28);
            this.SaveButton.TabIndex = 7;
            this.SaveButton.Text = "Done";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 17);
            this.label1.TabIndex = 9;
            this.label1.Text = "Volume";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.VolumeValue);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(37, 95);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(267, 46);
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
            this.VolumeValue.Location = new System.Drawing.Point(81, 10);
            this.VolumeValue.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.VolumeValue.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.VolumeValue.Name = "VolumeValue";
            this.VolumeValue.Size = new System.Drawing.Size(160, 22);
            this.VolumeValue.TabIndex = 10;
            this.VolumeValue.Value = new decimal(new int[] {
            2,
            0,
            0,
            65536});
            // 
            // NameEdit
            // 
            this.NameEdit.Location = new System.Drawing.Point(88, 20);
            this.NameEdit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.NameEdit.Name = "NameEdit";
            this.NameEdit.Size = new System.Drawing.Size(132, 22);
            this.NameEdit.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 23);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 17);
            this.label2.TabIndex = 15;
            this.label2.Text = "Name";
            // 
            // TestPanel
            // 
            this.TestPanel.BackColor = System.Drawing.Color.YellowGreen;
            this.TestPanel.Location = new System.Drawing.Point(51, 668);
            this.TestPanel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TestPanel.Name = "TestPanel";
            this.TestPanel.Size = new System.Drawing.Size(892, 87);
            this.TestPanel.TabIndex = 16;
            this.TestPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel2_MouseClick);
            // 
            // EffectsPanel
            // 
            this.EffectsPanel.AutoScroll = true;
            this.EffectsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.EffectsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EffectsPanel.Location = new System.Drawing.Point(4, 47);
            this.EffectsPanel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.EffectsPanel.Name = "EffectsPanel";
            this.EffectsPanel.Size = new System.Drawing.Size(888, 380);
            this.EffectsPanel.TabIndex = 17;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.EffectsPanel);
            this.groupBox1.Controls.Add(this.menuStrip1);
            this.groupBox1.Location = new System.Drawing.Point(47, 230);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(896, 431);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Patch panel";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(4, 19);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(888, 28);
            this.menuStrip1.TabIndex = 18;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modlulatorsToolStripMenuItem,
            this.effectsToolStripMenuItem});
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(49, 24);
            this.addToolStripMenuItem.Text = "Add";
            // 
            // modlulatorsToolStripMenuItem
            // 
            this.modlulatorsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lFOToolStripMenuItem,
            this.envelopeToolStripMenuItem});
            this.modlulatorsToolStripMenuItem.Name = "modlulatorsToolStripMenuItem";
            this.modlulatorsToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.modlulatorsToolStripMenuItem.Text = "Modulators";
            // 
            // lFOToolStripMenuItem
            // 
            this.lFOToolStripMenuItem.Name = "lFOToolStripMenuItem";
            this.lFOToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.lFOToolStripMenuItem.Text = "LFO";
            this.lFOToolStripMenuItem.Click += new System.EventHandler(this.lFOToolStripMenuItem_Click);
            // 
            // envelopeToolStripMenuItem
            // 
            this.envelopeToolStripMenuItem.Name = "envelopeToolStripMenuItem";
            this.envelopeToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.envelopeToolStripMenuItem.Text = "Envelope";
            this.envelopeToolStripMenuItem.Click += new System.EventHandler(this.envelopeToolStripMenuItem_Click);
            // 
            // effectsToolStripMenuItem
            // 
            this.effectsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.echoToolStripMenuItem});
            this.effectsToolStripMenuItem.Name = "effectsToolStripMenuItem";
            this.effectsToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.effectsToolStripMenuItem.Text = "Effects";
            // 
            // echoToolStripMenuItem
            // 
            this.echoToolStripMenuItem.Name = "echoToolStripMenuItem";
            this.echoToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.echoToolStripMenuItem.Text = "Echo";
            this.echoToolStripMenuItem.Click += new System.EventHandler(this.echoToolStripMenuItem_Click);
            // 
            // waveSelector1
            // 
            this.waveSelector1.Location = new System.Drawing.Point(37, 52);
            this.waveSelector1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.waveSelector1.Name = "waveSelector1";
            this.waveSelector1.Size = new System.Drawing.Size(200, 36);
            this.waveSelector1.TabIndex = 10;
            // 
            // OctaveValue
            // 
            this.OctaveValue.Location = new System.Drawing.Point(97, 167);
            this.OctaveValue.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.OctaveValue.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.OctaveValue.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.OctaveValue.Name = "OctaveValue";
            this.OctaveValue.Size = new System.Drawing.Size(160, 22);
            this.OctaveValue.TabIndex = 19;
            this.OctaveValue.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 170);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 17);
            this.label3.TabIndex = 20;
            this.label3.Text = "Octave";
            // 
            // InstrumentEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1412, 911);
            this.ControlBox = false;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.OctaveValue);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.TestPanel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.NameEdit);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.waveSelector1);
            this.Controls.Add(this.SaveButton);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "InstrumentEditor";
            this.Text = "InstrumentEditor";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.InstrumentEditor_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.InstrumentEditor_KeyUp);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VolumeValue)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OctaveValue)).EndInit();
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
        private System.Windows.Forms.Panel TestPanel;
        private System.Windows.Forms.FlowLayoutPanel EffectsPanel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modlulatorsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lFOToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem envelopeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem effectsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem echoToolStripMenuItem;
        private System.Windows.Forms.NumericUpDown OctaveValue;
        private System.Windows.Forms.Label label3;
    }
}