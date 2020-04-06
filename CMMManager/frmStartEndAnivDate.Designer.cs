namespace CMMManager
{
    partial class frmStartEndAnivDate
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dtpStartAnivDate = new System.Windows.Forms.DateTimePicker();
            this.dtpEndAnivDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dtpStartAnivDate
            // 
            this.dtpStartAnivDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStartAnivDate.Location = new System.Drawing.Point(147, 53);
            this.dtpStartAnivDate.Name = "dtpStartAnivDate";
            this.dtpStartAnivDate.Size = new System.Drawing.Size(121, 20);
            this.dtpStartAnivDate.TabIndex = 0;
            // 
            // dtpEndAnivDate
            // 
            this.dtpEndAnivDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndAnivDate.Location = new System.Drawing.Point(147, 100);
            this.dtpEndAnivDate.Name = "dtpEndAnivDate";
            this.dtpEndAnivDate.Size = new System.Drawing.Size(121, 20);
            this.dtpEndAnivDate.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(49, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Start Aniv Date:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(49, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "End Aniv Date:";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(107, 282);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(91, 34);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(219, 282);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(91, 34);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // frmStartEndAnivDate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(322, 328);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpEndAnivDate);
            this.Controls.Add(this.dtpStartAnivDate);
            this.Name = "frmStartEndAnivDate";
            this.Text = "Select Start Aniv Date and End Aniv Date";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpStartAnivDate;
        private System.Windows.Forms.DateTimePicker dtpEndAnivDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
    }
}