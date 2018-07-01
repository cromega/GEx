namespace Chirpotle {
    partial class EchoEffect {
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.DelayValue = new System.Windows.Forms.NumericUpDown();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.FeedbackValue = new System.Windows.Forms.NumericUpDown();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DelayValue)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FeedbackValue)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(142, 221);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Echo";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.DelayValue);
            this.groupBox2.Location = new System.Drawing.Point(6, 19);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(136, 46);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Delay";
            // 
            // DelayValue
            // 
            this.DelayValue.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.DelayValue.Location = new System.Drawing.Point(6, 19);
            this.DelayValue.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.DelayValue.Name = "DelayValue";
            this.DelayValue.Size = new System.Drawing.Size(120, 20);
            this.DelayValue.TabIndex = 0;
            this.DelayValue.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.FeedbackValue);
            this.groupBox3.Location = new System.Drawing.Point(0, 71);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(142, 50);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Feedback";
            // 
            // FeedbackValue
            // 
            this.FeedbackValue.DecimalPlaces = 1;
            this.FeedbackValue.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.FeedbackValue.Location = new System.Drawing.Point(12, 19);
            this.FeedbackValue.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.FeedbackValue.Name = "FeedbackValue";
            this.FeedbackValue.Size = new System.Drawing.Size(120, 20);
            this.FeedbackValue.TabIndex = 0;
            this.FeedbackValue.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(112, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(30, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "X";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // EchoEffect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "EchoEffect";
            this.Size = new System.Drawing.Size(152, 227);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DelayValue)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FeedbackValue)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.NumericUpDown FeedbackValue;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown DelayValue;
    }
}
