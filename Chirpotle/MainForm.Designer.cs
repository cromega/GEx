namespace Chirpotle {
    partial class MainForm {
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
            this.PlayButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.InstrumentSelector = new System.Windows.Forms.ListBox();
            this.EditInstrumentButton = new System.Windows.Forms.Button();
            this.AddInstrumentButton = new System.Windows.Forms.Button();
            this.Sequencer = new System.Windows.Forms.WebBrowser();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // PlayButton
            // 
            this.PlayButton.Location = new System.Drawing.Point(24, 185);
            this.PlayButton.Margin = new System.Windows.Forms.Padding(4);
            this.PlayButton.Name = "PlayButton";
            this.PlayButton.Size = new System.Drawing.Size(100, 28);
            this.PlayButton.TabIndex = 10;
            this.PlayButton.Text = "Play";
            this.PlayButton.UseVisualStyleBackColor = true;
            this.PlayButton.Click += new System.EventHandler(this.PlayButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.InstrumentSelector);
            this.groupBox1.Controls.Add(this.EditInstrumentButton);
            this.groupBox1.Controls.Add(this.AddInstrumentButton);
            this.groupBox1.Location = new System.Drawing.Point(16, 15);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(367, 162);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Instruments";
            // 
            // InstrumentSelector
            // 
            this.InstrumentSelector.FormattingEnabled = true;
            this.InstrumentSelector.ItemHeight = 16;
            this.InstrumentSelector.Location = new System.Drawing.Point(8, 23);
            this.InstrumentSelector.Margin = new System.Windows.Forms.Padding(4);
            this.InstrumentSelector.Name = "InstrumentSelector";
            this.InstrumentSelector.Size = new System.Drawing.Size(159, 116);
            this.InstrumentSelector.TabIndex = 14;
            // 
            // EditInstrumentButton
            // 
            this.EditInstrumentButton.Location = new System.Drawing.Point(176, 59);
            this.EditInstrumentButton.Margin = new System.Windows.Forms.Padding(4);
            this.EditInstrumentButton.Name = "EditInstrumentButton";
            this.EditInstrumentButton.Size = new System.Drawing.Size(100, 28);
            this.EditInstrumentButton.TabIndex = 13;
            this.EditInstrumentButton.Text = "Edit";
            this.EditInstrumentButton.UseVisualStyleBackColor = true;
            this.EditInstrumentButton.Click += new System.EventHandler(this.EditInstrumentButton_Click);
            // 
            // AddInstrumentButton
            // 
            this.AddInstrumentButton.Location = new System.Drawing.Point(176, 23);
            this.AddInstrumentButton.Margin = new System.Windows.Forms.Padding(4);
            this.AddInstrumentButton.Name = "AddInstrumentButton";
            this.AddInstrumentButton.Size = new System.Drawing.Size(100, 28);
            this.AddInstrumentButton.TabIndex = 12;
            this.AddInstrumentButton.Text = "Add";
            this.AddInstrumentButton.UseVisualStyleBackColor = true;
            this.AddInstrumentButton.Click += new System.EventHandler(this.AddInstrumentButton_Click);
            // 
            // Sequencer
            // 
            this.Sequencer.AllowWebBrowserDrop = false;
            this.Sequencer.IsWebBrowserContextMenuEnabled = false;
            this.Sequencer.Location = new System.Drawing.Point(391, 15);
            this.Sequencer.Margin = new System.Windows.Forms.Padding(4);
            this.Sequencer.MinimumSize = new System.Drawing.Size(27, 25);
            this.Sequencer.Name = "Sequencer";
            this.Sequencer.Size = new System.Drawing.Size(911, 1009);
            this.Sequencer.TabIndex = 13;
            this.Sequencer.Url = new System.Uri("", System.UriKind.Relative);
            this.Sequencer.WebBrowserShortcutsEnabled = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1437, 1055);
            this.Controls.Add(this.Sequencer);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.PlayButton);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "Chirpotle";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button PlayButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button EditInstrumentButton;
        private System.Windows.Forms.Button AddInstrumentButton;
        private System.Windows.Forms.ListBox InstrumentSelector;
        private System.Windows.Forms.WebBrowser Sequencer;
    }
}

