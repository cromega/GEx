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
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.InstrumentSelector = new System.Windows.Forms.ListBox();
            this.EditInstrumentButton = new System.Windows.Forms.Button();
            this.AddInstrumentButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(389, 121);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "Play";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.InstrumentSelector);
            this.groupBox1.Controls.Add(this.EditInstrumentButton);
            this.groupBox1.Controls.Add(this.AddInstrumentButton);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(275, 132);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Instruments";
            // 
            // InstrumentSelector
            // 
            this.InstrumentSelector.FormattingEnabled = true;
            this.InstrumentSelector.Location = new System.Drawing.Point(6, 19);
            this.InstrumentSelector.Name = "InstrumentSelector";
            this.InstrumentSelector.Size = new System.Drawing.Size(120, 95);
            this.InstrumentSelector.TabIndex = 14;
            // 
            // EditInstrumentButton
            // 
            this.EditInstrumentButton.Location = new System.Drawing.Point(132, 48);
            this.EditInstrumentButton.Name = "EditInstrumentButton";
            this.EditInstrumentButton.Size = new System.Drawing.Size(75, 23);
            this.EditInstrumentButton.TabIndex = 13;
            this.EditInstrumentButton.Text = "Edit";
            this.EditInstrumentButton.UseVisualStyleBackColor = true;
            this.EditInstrumentButton.Click += new System.EventHandler(this.EditInstrumentButton_Click);
            // 
            // AddInstrumentButton
            // 
            this.AddInstrumentButton.Location = new System.Drawing.Point(132, 19);
            this.AddInstrumentButton.Name = "AddInstrumentButton";
            this.AddInstrumentButton.Size = new System.Drawing.Size(75, 23);
            this.AddInstrumentButton.TabIndex = 12;
            this.AddInstrumentButton.Text = "Add";
            this.AddInstrumentButton.UseVisualStyleBackColor = true;
            this.AddInstrumentButton.Click += new System.EventHandler(this.AddInstrumentButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1342, 791);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button EditInstrumentButton;
        private System.Windows.Forms.Button AddInstrumentButton;
        private System.Windows.Forms.ListBox InstrumentSelector;
    }
}

