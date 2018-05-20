namespace Chirpotle {
    partial class Form1 {
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
            this.SquareGeneratorButton = new System.Windows.Forms.RadioButton();
            this.SineGeneratorButton = new System.Windows.Forms.RadioButton();
            this.GeneratorPanel = new System.Windows.Forms.GroupBox();
            this.NoiseGeneratorButton = new System.Windows.Forms.RadioButton();
            this.EnvelopePanel = new System.Windows.Forms.GroupBox();
            this.ReleaseValue = new System.Windows.Forms.NumericUpDown();
            this.SustainValue = new System.Windows.Forms.NumericUpDown();
            this.DecayValue = new System.Windows.Forms.NumericUpDown();
            this.AttackValue = new System.Windows.Forms.NumericUpDown();
            this.SustainLabel = new System.Windows.Forms.Label();
            this.ReleaseLabel = new System.Windows.Forms.Label();
            this.DecayLabel = new System.Windows.Forms.Label();
            this.AttackLabel = new System.Windows.Forms.Label();
            this.VolumeValue = new System.Windows.Forms.NumericUpDown();
            this.VolumeLabel = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.GeneratorPanel.SuspendLayout();
            this.EnvelopePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ReleaseValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SustainValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DecayValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AttackValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VolumeValue)).BeginInit();
            this.SuspendLayout();
            // 
            // SquareGeneratorButton
            // 
            this.SquareGeneratorButton.AutoSize = true;
            this.SquareGeneratorButton.Location = new System.Drawing.Point(6, 42);
            this.SquareGeneratorButton.Name = "SquareGeneratorButton";
            this.SquareGeneratorButton.Size = new System.Drawing.Size(59, 17);
            this.SquareGeneratorButton.TabIndex = 0;
            this.SquareGeneratorButton.Text = "Square";
            this.SquareGeneratorButton.UseVisualStyleBackColor = true;
            // 
            // SineGeneratorButton
            // 
            this.SineGeneratorButton.AutoSize = true;
            this.SineGeneratorButton.Checked = true;
            this.SineGeneratorButton.Location = new System.Drawing.Point(6, 19);
            this.SineGeneratorButton.Name = "SineGeneratorButton";
            this.SineGeneratorButton.Size = new System.Drawing.Size(46, 17);
            this.SineGeneratorButton.TabIndex = 1;
            this.SineGeneratorButton.TabStop = true;
            this.SineGeneratorButton.Text = "Sine";
            this.SineGeneratorButton.UseVisualStyleBackColor = true;
            // 
            // GeneratorPanel
            // 
            this.GeneratorPanel.Controls.Add(this.NoiseGeneratorButton);
            this.GeneratorPanel.Controls.Add(this.SineGeneratorButton);
            this.GeneratorPanel.Controls.Add(this.SquareGeneratorButton);
            this.GeneratorPanel.Location = new System.Drawing.Point(180, 69);
            this.GeneratorPanel.Name = "GeneratorPanel";
            this.GeneratorPanel.Size = new System.Drawing.Size(200, 100);
            this.GeneratorPanel.TabIndex = 2;
            this.GeneratorPanel.TabStop = false;
            this.GeneratorPanel.Text = "Generator";
            // 
            // NoiseGeneratorButton
            // 
            this.NoiseGeneratorButton.AutoSize = true;
            this.NoiseGeneratorButton.Location = new System.Drawing.Point(6, 65);
            this.NoiseGeneratorButton.Name = "NoiseGeneratorButton";
            this.NoiseGeneratorButton.Size = new System.Drawing.Size(52, 17);
            this.NoiseGeneratorButton.TabIndex = 2;
            this.NoiseGeneratorButton.Text = "Noise";
            this.NoiseGeneratorButton.UseVisualStyleBackColor = true;
            // 
            // EnvelopePanel
            // 
            this.EnvelopePanel.Controls.Add(this.ReleaseValue);
            this.EnvelopePanel.Controls.Add(this.SustainValue);
            this.EnvelopePanel.Controls.Add(this.DecayValue);
            this.EnvelopePanel.Controls.Add(this.AttackValue);
            this.EnvelopePanel.Controls.Add(this.SustainLabel);
            this.EnvelopePanel.Controls.Add(this.ReleaseLabel);
            this.EnvelopePanel.Controls.Add(this.DecayLabel);
            this.EnvelopePanel.Controls.Add(this.AttackLabel);
            this.EnvelopePanel.Location = new System.Drawing.Point(516, 77);
            this.EnvelopePanel.Name = "EnvelopePanel";
            this.EnvelopePanel.Size = new System.Drawing.Size(200, 127);
            this.EnvelopePanel.TabIndex = 3;
            this.EnvelopePanel.TabStop = false;
            this.EnvelopePanel.Text = "Envelope";
            // 
            // ReleaseValue
            // 
            this.ReleaseValue.Location = new System.Drawing.Point(58, 98);
            this.ReleaseValue.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.ReleaseValue.Name = "ReleaseValue";
            this.ReleaseValue.Size = new System.Drawing.Size(120, 20);
            this.ReleaseValue.TabIndex = 7;
            this.ReleaseValue.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // SustainValue
            // 
            this.SustainValue.DecimalPlaces = 1;
            this.SustainValue.Location = new System.Drawing.Point(58, 72);
            this.SustainValue.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.SustainValue.Name = "SustainValue";
            this.SustainValue.Size = new System.Drawing.Size(120, 20);
            this.SustainValue.TabIndex = 6;
            this.SustainValue.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            // 
            // DecayValue
            // 
            this.DecayValue.Location = new System.Drawing.Point(58, 45);
            this.DecayValue.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.DecayValue.Name = "DecayValue";
            this.DecayValue.Size = new System.Drawing.Size(120, 20);
            this.DecayValue.TabIndex = 5;
            this.DecayValue.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // AttackValue
            // 
            this.AttackValue.Location = new System.Drawing.Point(58, 19);
            this.AttackValue.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.AttackValue.Name = "AttackValue";
            this.AttackValue.Size = new System.Drawing.Size(120, 20);
            this.AttackValue.TabIndex = 4;
            this.AttackValue.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // SustainLabel
            // 
            this.SustainLabel.AutoSize = true;
            this.SustainLabel.Location = new System.Drawing.Point(3, 74);
            this.SustainLabel.Name = "SustainLabel";
            this.SustainLabel.Size = new System.Drawing.Size(42, 13);
            this.SustainLabel.TabIndex = 4;
            this.SustainLabel.Text = "Sustain";
            // 
            // ReleaseLabel
            // 
            this.ReleaseLabel.AutoSize = true;
            this.ReleaseLabel.Location = new System.Drawing.Point(3, 100);
            this.ReleaseLabel.Name = "ReleaseLabel";
            this.ReleaseLabel.Size = new System.Drawing.Size(46, 13);
            this.ReleaseLabel.TabIndex = 3;
            this.ReleaseLabel.Text = "Release";
            // 
            // DecayLabel
            // 
            this.DecayLabel.AutoSize = true;
            this.DecayLabel.Location = new System.Drawing.Point(3, 48);
            this.DecayLabel.Name = "DecayLabel";
            this.DecayLabel.Size = new System.Drawing.Size(38, 13);
            this.DecayLabel.TabIndex = 2;
            this.DecayLabel.Text = "Decay";
            // 
            // AttackLabel
            // 
            this.AttackLabel.AutoSize = true;
            this.AttackLabel.Location = new System.Drawing.Point(3, 22);
            this.AttackLabel.Name = "AttackLabel";
            this.AttackLabel.Size = new System.Drawing.Size(38, 13);
            this.AttackLabel.TabIndex = 1;
            this.AttackLabel.Text = "Attack";
            // 
            // VolumeValue
            // 
            this.VolumeValue.DecimalPlaces = 1;
            this.VolumeValue.Location = new System.Drawing.Point(260, 180);
            this.VolumeValue.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.VolumeValue.Name = "VolumeValue";
            this.VolumeValue.Size = new System.Drawing.Size(120, 20);
            this.VolumeValue.TabIndex = 8;
            this.VolumeValue.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            // 
            // VolumeLabel
            // 
            this.VolumeLabel.AutoSize = true;
            this.VolumeLabel.Location = new System.Drawing.Point(183, 182);
            this.VolumeLabel.Name = "VolumeLabel";
            this.VolumeLabel.Size = new System.Drawing.Size(42, 13);
            this.VolumeLabel.TabIndex = 9;
            this.VolumeLabel.Text = "Volume";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(641, 224);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "Play";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1342, 791);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.VolumeLabel);
            this.Controls.Add(this.VolumeValue);
            this.Controls.Add(this.EnvelopePanel);
            this.Controls.Add(this.GeneratorPanel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.GeneratorPanel.ResumeLayout(false);
            this.GeneratorPanel.PerformLayout();
            this.EnvelopePanel.ResumeLayout(false);
            this.EnvelopePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ReleaseValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SustainValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DecayValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AttackValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VolumeValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton SquareGeneratorButton;
        private System.Windows.Forms.RadioButton SineGeneratorButton;
        private System.Windows.Forms.GroupBox GeneratorPanel;
        private System.Windows.Forms.RadioButton NoiseGeneratorButton;
        private System.Windows.Forms.GroupBox EnvelopePanel;
        private System.Windows.Forms.Label SustainLabel;
        private System.Windows.Forms.Label ReleaseLabel;
        private System.Windows.Forms.Label DecayLabel;
        private System.Windows.Forms.Label AttackLabel;
        private System.Windows.Forms.NumericUpDown ReleaseValue;
        private System.Windows.Forms.NumericUpDown SustainValue;
        private System.Windows.Forms.NumericUpDown DecayValue;
        private System.Windows.Forms.NumericUpDown AttackValue;
        private System.Windows.Forms.NumericUpDown VolumeValue;
        private System.Windows.Forms.Label VolumeLabel;
        private System.Windows.Forms.Button button1;
    }
}

