namespace CMMManager
{
    partial class frmAddEmailBcc
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
            this.label1 = new System.Windows.Forms.Label();
            this.tvStaffEmail = new System.Windows.Forms.TreeView();
            this.lbEmailBcc = new System.Windows.Forms.ListBox();
            this.btnAddEmailToBcc = new System.Windows.Forms.Button();
            this.btnRemoveEmailFromBcc = new System.Windows.Forms.Button();
            this.btnOkBcc = new System.Windows.Forms.Button();
            this.btnCancelBcc = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select Email Address to Bcc";
            // 
            // tvStaffEmail
            // 
            this.tvStaffEmail.Location = new System.Drawing.Point(12, 39);
            this.tvStaffEmail.Name = "tvStaffEmail";
            this.tvStaffEmail.Size = new System.Drawing.Size(364, 195);
            this.tvStaffEmail.TabIndex = 1;
            // 
            // lbEmailBcc
            // 
            this.lbEmailBcc.FormattingEnabled = true;
            this.lbEmailBcc.Location = new System.Drawing.Point(12, 240);
            this.lbEmailBcc.Name = "lbEmailBcc";
            this.lbEmailBcc.Size = new System.Drawing.Size(251, 95);
            this.lbEmailBcc.TabIndex = 2;
            // 
            // btnAddEmailToBcc
            // 
            this.btnAddEmailToBcc.Location = new System.Drawing.Point(272, 240);
            this.btnAddEmailToBcc.Name = "btnAddEmailToBcc";
            this.btnAddEmailToBcc.Size = new System.Drawing.Size(104, 56);
            this.btnAddEmailToBcc.TabIndex = 3;
            this.btnAddEmailToBcc.Text = "Add";
            this.btnAddEmailToBcc.UseVisualStyleBackColor = true;
            // 
            // btnRemoveEmailFromBcc
            // 
            this.btnRemoveEmailFromBcc.Location = new System.Drawing.Point(272, 302);
            this.btnRemoveEmailFromBcc.Name = "btnRemoveEmailFromBcc";
            this.btnRemoveEmailFromBcc.Size = new System.Drawing.Size(104, 33);
            this.btnRemoveEmailFromBcc.TabIndex = 4;
            this.btnRemoveEmailFromBcc.Text = "Remove";
            this.btnRemoveEmailFromBcc.UseVisualStyleBackColor = true;
            // 
            // btnOkBcc
            // 
            this.btnOkBcc.Location = new System.Drawing.Point(165, 364);
            this.btnOkBcc.Name = "btnOkBcc";
            this.btnOkBcc.Size = new System.Drawing.Size(98, 32);
            this.btnOkBcc.TabIndex = 5;
            this.btnOkBcc.Text = "Ok";
            this.btnOkBcc.UseVisualStyleBackColor = true;
            // 
            // btnCancelBcc
            // 
            this.btnCancelBcc.Location = new System.Drawing.Point(278, 364);
            this.btnCancelBcc.Name = "btnCancelBcc";
            this.btnCancelBcc.Size = new System.Drawing.Size(98, 32);
            this.btnCancelBcc.TabIndex = 6;
            this.btnCancelBcc.Text = "Cancel";
            this.btnCancelBcc.UseVisualStyleBackColor = true;
            // 
            // frmAddEmailBcc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 417);
            this.Controls.Add(this.btnCancelBcc);
            this.Controls.Add(this.btnOkBcc);
            this.Controls.Add(this.btnRemoveEmailFromBcc);
            this.Controls.Add(this.btnAddEmailToBcc);
            this.Controls.Add(this.lbEmailBcc);
            this.Controls.Add(this.tvStaffEmail);
            this.Controls.Add(this.label1);
            this.Name = "frmAddEmailBcc";
            this.Text = "Email to Bcc";
            this.Load += new System.EventHandler(this.frmAddEmailBcc_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TreeView tvStaffEmail;
        private System.Windows.Forms.ListBox lbEmailBcc;
        private System.Windows.Forms.Button btnAddEmailToBcc;
        private System.Windows.Forms.Button btnRemoveEmailFromBcc;
        private System.Windows.Forms.Button btnOkBcc;
        private System.Windows.Forms.Button btnCancelBcc;
    }
}