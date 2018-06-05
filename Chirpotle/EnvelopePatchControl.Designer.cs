namespace Chirpotle {
    partial class EnvelopePatchControl {
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
            this.button1 = new System.Windows.Forms.Button();
            this.envelopeControl1 = new Chirpotle.EnvelopeControl();
            this.patchableValueSelector1 = new Chirpotle.PatchableValueSelector();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.envelopeControl1);
            this.groupBox1.Controls.Add(this.patchableValueSelector1);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(203, 267);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Envelope";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(181, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(22, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "X";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // envelopeControl1
            // 
            this.envelopeControl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.envelopeControl1.Location = new System.Drawing.Point(7, 89);
            this.envelopeControl1.Name = "envelopeControl1";
            this.envelopeControl1.Size = new System.Drawing.Size(189, 146);
            this.envelopeControl1.TabIndex = 1;
            // 
            // patchableValueSelector1
            // 
            this.patchableValueSelector1.Location = new System.Drawing.Point(7, 20);
            this.patchableValueSelector1.Name = "patchableValueSelector1";
            this.patchableValueSelector1.Size = new System.Drawing.Size(152, 63);
            this.patchableValueSelector1.TabIndex = 0;
            // 
            // EnvelopePatchControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "EnvelopePatchControl";
            this.Size = new System.Drawing.Size(211, 283);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private PatchableValueSelector patchableValueSelector1;
        private EnvelopeControl envelopeControl1;
        private System.Windows.Forms.Button button1;
    }
}
