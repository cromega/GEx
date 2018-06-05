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
            this.MainEnvelope = new Chirpotle.EnvelopeControl();
            this.waveSelector1 = new Chirpotle.WaveSelector();
            this.envelopeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VolumeValue)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(629, 42);
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
            // TestPanel
            // 
            this.TestPanel.BackColor = System.Drawing.Color.YellowGreen;
            this.TestPanel.Location = new System.Drawing.Point(38, 543);
            this.TestPanel.Name = "TestPanel";
            this.TestPanel.Size = new System.Drawing.Size(669, 71);
            this.TestPanel.TabIndex = 16;
            this.TestPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel2_MouseClick);
            // 
            // EffectsPanel
            // 
            this.EffectsPanel.AutoScroll = true;
            this.EffectsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.EffectsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EffectsPanel.Location = new System.Drawing.Point(3, 40);
            this.EffectsPanel.Name = "EffectsPanel";
            this.EffectsPanel.Size = new System.Drawing.Size(666, 307);
            this.EffectsPanel.TabIndex = 17;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.EffectsPanel);
            this.groupBox1.Controls.Add(this.menuStrip1);
            this.groupBox1.Location = new System.Drawing.Point(35, 187);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(672, 350);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Patch panel";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(3, 16);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(666, 24);
            this.menuStrip1.TabIndex = 18;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modlulatorsToolStripMenuItem});
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.addToolStripMenuItem.Text = "Add";
            // 
            // modlulatorsToolStripMenuItem
            // 
            this.modlulatorsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lFOToolStripMenuItem,
            this.envelopeToolStripMenuItem});
            this.modlulatorsToolStripMenuItem.Name = "modlulatorsToolStripMenuItem";
            this.modlulatorsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.modlulatorsToolStripMenuItem.Text = "Modlulators";
            // 
            // lFOToolStripMenuItem
            // 
            this.lFOToolStripMenuItem.Name = "lFOToolStripMenuItem";
            this.lFOToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.lFOToolStripMenuItem.Text = "LFO";
            this.lFOToolStripMenuItem.Click += new System.EventHandler(this.lFOToolStripMenuItem_Click);
            // 
            // MainEnvelope
            // 
            this.MainEnvelope.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MainEnvelope.Location = new System.Drawing.Point(234, 12);
            this.MainEnvelope.Name = "MainEnvelope";
            this.MainEnvelope.Size = new System.Drawing.Size(189, 147);
            this.MainEnvelope.TabIndex = 19;
            // 
            // waveSelector1
            // 
            this.waveSelector1.Location = new System.Drawing.Point(28, 42);
            this.waveSelector1.Name = "waveSelector1";
            this.waveSelector1.Size = new System.Drawing.Size(150, 29);
            this.waveSelector1.TabIndex = 10;
            // 
            // envelopeToolStripMenuItem
            // 
            this.envelopeToolStripMenuItem.Name = "envelopeToolStripMenuItem";
            this.envelopeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.envelopeToolStripMenuItem.Text = "Envelope";
            this.envelopeToolStripMenuItem.Click += new System.EventHandler(this.envelopeToolStripMenuItem_Click);
            // 
            // InstrumentEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1059, 740);
            this.ControlBox = false;
            this.Controls.Add(this.MainEnvelope);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.TestPanel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.NameEdit);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.waveSelector1);
            this.Controls.Add(this.SaveButton);
            this.KeyPreview = true;
            this.Name = "InstrumentEditor";
            this.Text = "InstrumentEditor";
            this.Load += new System.EventHandler(this.InstrumentEditor_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.InstrumentEditor_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.InstrumentEditor_KeyUp);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VolumeValue)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
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
        private EnvelopeControl MainEnvelope;
        private System.Windows.Forms.ToolStripMenuItem lFOToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem envelopeToolStripMenuItem;
    }
}