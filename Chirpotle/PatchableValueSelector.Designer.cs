namespace Chirpotle {
    partial class PatchableValueSelector {
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
            this.PatchSelector = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // PatchSelector
            // 
            this.PatchSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PatchSelector.FormattingEnabled = true;
            this.PatchSelector.Location = new System.Drawing.Point(6, 19);
            this.PatchSelector.Name = "PatchSelector";
            this.PatchSelector.Size = new System.Drawing.Size(121, 21);
            this.PatchSelector.TabIndex = 0;
            this.PatchSelector.SelectedIndexChanged += new System.EventHandler(this.PatchSelector_SelectedIndexChanged);
            this.PatchSelector.SelectedValueChanged += new System.EventHandler(this.PatchSelector_SelectedValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.PatchSelector);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(144, 57);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Patch Target";
            // 
            // PatchableValueSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "PatchableValueSelector";
            this.Size = new System.Drawing.Size(152, 63);
            this.Load += new System.EventHandler(this.PatchableValueSelector_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox PatchSelector;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}
