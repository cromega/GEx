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
            this.EnvelopePanel = new System.Windows.Forms.GroupBox();
            this.ReleaseValue = new System.Windows.Forms.NumericUpDown();
            this.SustainValue = new System.Windows.Forms.NumericUpDown();
            this.DecayValue = new System.Windows.Forms.NumericUpDown();
            this.AttackValue = new System.Windows.Forms.NumericUpDown();
            this.SustainLabel = new System.Windows.Forms.Label();
            this.ReleaseLabel = new System.Windows.Forms.Label();
            this.DecayLabel = new System.Windows.Forms.Label();
            this.AttackLabel = new System.Windows.Forms.Label();
            this.SquareGeneratorButton = new System.Windows.Forms.RadioButton();
            this.SineGeneratorButton = new System.Windows.Forms.RadioButton();
            this.NoiseGeneratorButton = new System.Windows.Forms.RadioButton();
            this.GeneratorPanel = new System.Windows.Forms.GroupBox();
            this.LFO1 = new System.Windows.Forms.GroupBox();
            this.LFO1NoiseGeneratorButton = new System.Windows.Forms.RadioButton();
            this.LFO1SineGeneratorButton = new System.Windows.Forms.RadioButton();
            this.LFO1SquareGeneratorButton = new System.Windows.Forms.RadioButton();
            this.LFO1RoutingSelector = new System.Windows.Forms.ComboBox();
            this.LFO2 = new System.Windows.Forms.GroupBox();
            this.LFO2RoutingSelector = new System.Windows.Forms.ComboBox();
            this.LFO2NoiseGeneratorButton = new System.Windows.Forms.RadioButton();
            this.LFO2SineGeneratorButton = new System.Windows.Forms.RadioButton();
            this.LFO2SquareGeneratorButton = new System.Windows.Forms.RadioButton();
            this.LFO1OnCheckbox = new System.Windows.Forms.CheckBox();
            this.LFO2OnCheckbox = new System.Windows.Forms.CheckBox();
            this.SaveButton = new System.Windows.Forms.Button();
            this.VolumeValue = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.LFO1FrequencyValue = new System.Windows.Forms.NumericUpDown();
            this.LFO2FrequencyValue = new System.Windows.Forms.NumericUpDown();
            this.EnvelopePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ReleaseValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SustainValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DecayValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AttackValue)).BeginInit();
            this.GeneratorPanel.SuspendLayout();
            this.LFO1.SuspendLayout();
            this.LFO2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VolumeValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LFO1FrequencyValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LFO2FrequencyValue)).BeginInit();
            this.SuspendLayout();
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
            this.EnvelopePanel.Location = new System.Drawing.Point(111, 12);
            this.EnvelopePanel.Name = "EnvelopePanel";
            this.EnvelopePanel.Size = new System.Drawing.Size(200, 127);
            this.EnvelopePanel.TabIndex = 4;
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
            // GeneratorPanel
            // 
            this.GeneratorPanel.Controls.Add(this.NoiseGeneratorButton);
            this.GeneratorPanel.Controls.Add(this.SineGeneratorButton);
            this.GeneratorPanel.Controls.Add(this.SquareGeneratorButton);
            this.GeneratorPanel.Location = new System.Drawing.Point(12, 12);
            this.GeneratorPanel.Name = "GeneratorPanel";
            this.GeneratorPanel.Size = new System.Drawing.Size(93, 100);
            this.GeneratorPanel.TabIndex = 3;
            this.GeneratorPanel.TabStop = false;
            this.GeneratorPanel.Text = "Generator";
            // 
            // LFO1
            // 
            this.LFO1.Controls.Add(this.LFO1FrequencyValue);
            this.LFO1.Controls.Add(this.LFO1OnCheckbox);
            this.LFO1.Controls.Add(this.LFO1RoutingSelector);
            this.LFO1.Controls.Add(this.LFO1NoiseGeneratorButton);
            this.LFO1.Controls.Add(this.LFO1SineGeneratorButton);
            this.LFO1.Controls.Add(this.LFO1SquareGeneratorButton);
            this.LFO1.Location = new System.Drawing.Point(317, 12);
            this.LFO1.Name = "LFO1";
            this.LFO1.Size = new System.Drawing.Size(93, 186);
            this.LFO1.TabIndex = 4;
            this.LFO1.TabStop = false;
            this.LFO1.Text = "LFO 1";
            // 
            // LFO1NoiseGeneratorButton
            // 
            this.LFO1NoiseGeneratorButton.AutoSize = true;
            this.LFO1NoiseGeneratorButton.Location = new System.Drawing.Point(6, 65);
            this.LFO1NoiseGeneratorButton.Name = "LFO1NoiseGeneratorButton";
            this.LFO1NoiseGeneratorButton.Size = new System.Drawing.Size(52, 17);
            this.LFO1NoiseGeneratorButton.TabIndex = 2;
            this.LFO1NoiseGeneratorButton.Text = "Noise";
            this.LFO1NoiseGeneratorButton.UseVisualStyleBackColor = true;
            // 
            // LFO1SineGeneratorButton
            // 
            this.LFO1SineGeneratorButton.AutoSize = true;
            this.LFO1SineGeneratorButton.Checked = true;
            this.LFO1SineGeneratorButton.Location = new System.Drawing.Point(6, 19);
            this.LFO1SineGeneratorButton.Name = "LFO1SineGeneratorButton";
            this.LFO1SineGeneratorButton.Size = new System.Drawing.Size(46, 17);
            this.LFO1SineGeneratorButton.TabIndex = 1;
            this.LFO1SineGeneratorButton.TabStop = true;
            this.LFO1SineGeneratorButton.Text = "Sine";
            this.LFO1SineGeneratorButton.UseVisualStyleBackColor = true;
            // 
            // LFO1SquareGeneratorButton
            // 
            this.LFO1SquareGeneratorButton.AutoSize = true;
            this.LFO1SquareGeneratorButton.Location = new System.Drawing.Point(6, 42);
            this.LFO1SquareGeneratorButton.Name = "LFO1SquareGeneratorButton";
            this.LFO1SquareGeneratorButton.Size = new System.Drawing.Size(59, 17);
            this.LFO1SquareGeneratorButton.TabIndex = 0;
            this.LFO1SquareGeneratorButton.Text = "Square";
            this.LFO1SquareGeneratorButton.UseVisualStyleBackColor = true;
            // 
            // LFO1RoutingSelector
            // 
            this.LFO1RoutingSelector.FormattingEnabled = true;
            this.LFO1RoutingSelector.Items.AddRange(new object[] {
            "Volume",
            "Frequency"});
            this.LFO1RoutingSelector.Location = new System.Drawing.Point(6, 120);
            this.LFO1RoutingSelector.Name = "LFO1RoutingSelector";
            this.LFO1RoutingSelector.Size = new System.Drawing.Size(81, 21);
            this.LFO1RoutingSelector.TabIndex = 5;
            // 
            // LFO2
            // 
            this.LFO2.Controls.Add(this.LFO2FrequencyValue);
            this.LFO2.Controls.Add(this.LFO2OnCheckbox);
            this.LFO2.Controls.Add(this.LFO2RoutingSelector);
            this.LFO2.Controls.Add(this.LFO2NoiseGeneratorButton);
            this.LFO2.Controls.Add(this.LFO2SineGeneratorButton);
            this.LFO2.Controls.Add(this.LFO2SquareGeneratorButton);
            this.LFO2.Location = new System.Drawing.Point(416, 12);
            this.LFO2.Name = "LFO2";
            this.LFO2.Size = new System.Drawing.Size(93, 186);
            this.LFO2.TabIndex = 6;
            this.LFO2.TabStop = false;
            this.LFO2.Text = "LFO 2";
            // 
            // LFO2RoutingSelector
            // 
            this.LFO2RoutingSelector.FormattingEnabled = true;
            this.LFO2RoutingSelector.Items.AddRange(new object[] {
            "Volume",
            "Frequency"});
            this.LFO2RoutingSelector.Location = new System.Drawing.Point(6, 120);
            this.LFO2RoutingSelector.Name = "LFO2RoutingSelector";
            this.LFO2RoutingSelector.Size = new System.Drawing.Size(81, 21);
            this.LFO2RoutingSelector.TabIndex = 5;
            // 
            // LFO2NoiseGeneratorButton
            // 
            this.LFO2NoiseGeneratorButton.AutoSize = true;
            this.LFO2NoiseGeneratorButton.Location = new System.Drawing.Point(6, 65);
            this.LFO2NoiseGeneratorButton.Name = "LFO2NoiseGeneratorButton";
            this.LFO2NoiseGeneratorButton.Size = new System.Drawing.Size(52, 17);
            this.LFO2NoiseGeneratorButton.TabIndex = 2;
            this.LFO2NoiseGeneratorButton.Text = "Noise";
            this.LFO2NoiseGeneratorButton.UseVisualStyleBackColor = true;
            // 
            // LFO2SineGeneratorButton
            // 
            this.LFO2SineGeneratorButton.AutoSize = true;
            this.LFO2SineGeneratorButton.Checked = true;
            this.LFO2SineGeneratorButton.Location = new System.Drawing.Point(6, 19);
            this.LFO2SineGeneratorButton.Name = "LFO2SineGeneratorButton";
            this.LFO2SineGeneratorButton.Size = new System.Drawing.Size(46, 17);
            this.LFO2SineGeneratorButton.TabIndex = 1;
            this.LFO2SineGeneratorButton.TabStop = true;
            this.LFO2SineGeneratorButton.Text = "Sine";
            this.LFO2SineGeneratorButton.UseVisualStyleBackColor = true;
            // 
            // LFO2SquareGeneratorButton
            // 
            this.LFO2SquareGeneratorButton.AutoSize = true;
            this.LFO2SquareGeneratorButton.Location = new System.Drawing.Point(6, 42);
            this.LFO2SquareGeneratorButton.Name = "LFO2SquareGeneratorButton";
            this.LFO2SquareGeneratorButton.Size = new System.Drawing.Size(59, 17);
            this.LFO2SquareGeneratorButton.TabIndex = 0;
            this.LFO2SquareGeneratorButton.Text = "Square";
            this.LFO2SquareGeneratorButton.UseVisualStyleBackColor = true;
            // 
            // LFO1OnCheckbox
            // 
            this.LFO1OnCheckbox.AutoSize = true;
            this.LFO1OnCheckbox.Location = new System.Drawing.Point(6, 147);
            this.LFO1OnCheckbox.Name = "LFO1OnCheckbox";
            this.LFO1OnCheckbox.Size = new System.Drawing.Size(40, 17);
            this.LFO1OnCheckbox.TabIndex = 6;
            this.LFO1OnCheckbox.Text = "On";
            this.LFO1OnCheckbox.UseVisualStyleBackColor = true;
            // 
            // LFO2OnCheckbox
            // 
            this.LFO2OnCheckbox.AutoSize = true;
            this.LFO2OnCheckbox.Location = new System.Drawing.Point(6, 147);
            this.LFO2OnCheckbox.Name = "LFO2OnCheckbox";
            this.LFO2OnCheckbox.Size = new System.Drawing.Size(40, 17);
            this.LFO2OnCheckbox.TabIndex = 7;
            this.LFO2OnCheckbox.Text = "On";
            this.LFO2OnCheckbox.UseVisualStyleBackColor = true;
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(12, 175);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 7;
            this.SaveButton.Text = "Done";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // VolumeValue
            // 
            this.VolumeValue.DecimalPlaces = 1;
            this.VolumeValue.Location = new System.Drawing.Point(580, 22);
            this.VolumeValue.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.VolumeValue.Name = "VolumeValue";
            this.VolumeValue.Size = new System.Drawing.Size(120, 20);
            this.VolumeValue.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(532, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Volume";
            // 
            // LFO1FrequencyValue
            // 
            this.LFO1FrequencyValue.Location = new System.Drawing.Point(6, 93);
            this.LFO1FrequencyValue.Name = "LFO1FrequencyValue";
            this.LFO1FrequencyValue.Size = new System.Drawing.Size(81, 20);
            this.LFO1FrequencyValue.TabIndex = 7;
            // 
            // LFO2FrequencyValue
            // 
            this.LFO2FrequencyValue.Location = new System.Drawing.Point(7, 93);
            this.LFO2FrequencyValue.Name = "LFO2FrequencyValue";
            this.LFO2FrequencyValue.Size = new System.Drawing.Size(81, 20);
            this.LFO2FrequencyValue.TabIndex = 8;
            // 
            // InstrumentEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.VolumeValue);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.LFO2);
            this.Controls.Add(this.LFO1);
            this.Controls.Add(this.EnvelopePanel);
            this.Controls.Add(this.GeneratorPanel);
            this.Name = "InstrumentEditor";
            this.Text = "InstrumentEditor";
            this.EnvelopePanel.ResumeLayout(false);
            this.EnvelopePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ReleaseValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SustainValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DecayValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AttackValue)).EndInit();
            this.GeneratorPanel.ResumeLayout(false);
            this.GeneratorPanel.PerformLayout();
            this.LFO1.ResumeLayout(false);
            this.LFO1.PerformLayout();
            this.LFO2.ResumeLayout(false);
            this.LFO2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VolumeValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LFO1FrequencyValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LFO2FrequencyValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox EnvelopePanel;
        private System.Windows.Forms.NumericUpDown ReleaseValue;
        private System.Windows.Forms.NumericUpDown SustainValue;
        private System.Windows.Forms.NumericUpDown DecayValue;
        private System.Windows.Forms.NumericUpDown AttackValue;
        private System.Windows.Forms.Label SustainLabel;
        private System.Windows.Forms.Label ReleaseLabel;
        private System.Windows.Forms.Label DecayLabel;
        private System.Windows.Forms.Label AttackLabel;
        private System.Windows.Forms.RadioButton SquareGeneratorButton;
        private System.Windows.Forms.RadioButton SineGeneratorButton;
        private System.Windows.Forms.RadioButton NoiseGeneratorButton;
        private System.Windows.Forms.GroupBox GeneratorPanel;
        private System.Windows.Forms.GroupBox LFO1;
        private System.Windows.Forms.GroupBox LFO2;
        private System.Windows.Forms.ComboBox LFO2RoutingSelector;
        private System.Windows.Forms.RadioButton LFO2NoiseGeneratorButton;
        private System.Windows.Forms.RadioButton LFO2SineGeneratorButton;
        private System.Windows.Forms.RadioButton LFO2SquareGeneratorButton;
        private System.Windows.Forms.ComboBox LFO1RoutingSelector;
        private System.Windows.Forms.RadioButton LFO1NoiseGeneratorButton;
        private System.Windows.Forms.RadioButton LFO1SineGeneratorButton;
        private System.Windows.Forms.RadioButton LFO1SquareGeneratorButton;
        private System.Windows.Forms.CheckBox LFO1OnCheckbox;
        private System.Windows.Forms.CheckBox LFO2OnCheckbox;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.NumericUpDown VolumeValue;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown LFO1FrequencyValue;
        private System.Windows.Forms.NumericUpDown LFO2FrequencyValue;
    }
}