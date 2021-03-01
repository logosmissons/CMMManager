namespace CMMManager
{
    partial class frmAddEmailCc
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
            this.label2 = new System.Windows.Forms.Label();
            this.tvFamilyEmail = new System.Windows.Forms.TreeView();
            this.lbEmailCc = new System.Windows.Forms.ListBox();
            this.btnCancelCc = new System.Windows.Forms.Button();
            this.btnOkCc = new System.Windows.Forms.Button();
            this.btnRemoveEmailFromCc = new System.Windows.Forms.Button();
            this.btnAddEmailToCc = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Select Email Address to Cc";
            // 
            // tvFamilyEmail
            // 
            this.tvFamilyEmail.Location = new System.Drawing.Point(15, 49);
            this.tvFamilyEmail.Name = "tvFamilyEmail";
            this.tvFamilyEmail.Size = new System.Drawing.Size(368, 193);
            this.tvFamilyEmail.TabIndex = 2;
            this.tvFamilyEmail.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvFamilyEmail_NodeMouseDoubleClick);
            // 
            // lbEmailCc
            // 
            this.lbEmailCc.FormattingEnabled = true;
            this.lbEmailCc.Location = new System.Drawing.Point(13, 249);
            this.lbEmailCc.Name = "lbEmailCc";
            this.lbEmailCc.Size = new System.Drawing.Size(260, 95);
            this.lbEmailCc.TabIndex = 3;
            // 
            // btnCancelCc
            // 
            this.btnCancelCc.Location = new System.Drawing.Point(279, 377);
            this.btnCancelCc.Name = "btnCancelCc";
            this.btnCancelCc.Size = new System.Drawing.Size(104, 32);
            this.btnCancelCc.TabIndex = 10;
            this.btnCancelCc.Text = "Cancel";
            this.btnCancelCc.UseVisualStyleBackColor = true;
            this.btnCancelCc.Click += new System.EventHandler(this.btnCancelCc_Click);
            // 
            // btnOkCc
            // 
            this.btnOkCc.Location = new System.Drawing.Point(167, 377);
            this.btnOkCc.Name = "btnOkCc";
            this.btnOkCc.Size = new System.Drawing.Size(98, 32);
            this.btnOkCc.TabIndex = 9;
            this.btnOkCc.Text = "Ok";
            this.btnOkCc.UseVisualStyleBackColor = true;
            this.btnOkCc.Click += new System.EventHandler(this.btnOkCc_Click);
            // 
            // btnRemoveEmailFromCc
            // 
            this.btnRemoveEmailFromCc.Location = new System.Drawing.Point(279, 311);
            this.btnRemoveEmailFromCc.Name = "btnRemoveEmailFromCc";
            this.btnRemoveEmailFromCc.Size = new System.Drawing.Size(104, 33);
            this.btnRemoveEmailFromCc.TabIndex = 8;
            this.btnRemoveEmailFromCc.Text = "Remove";
            this.btnRemoveEmailFromCc.UseVisualStyleBackColor = true;
            this.btnRemoveEmailFromCc.Click += new System.EventHandler(this.btnRemoveEmailFromCc_Click);
            // 
            // btnAddEmailToCc
            // 
            this.btnAddEmailToCc.Location = new System.Drawing.Point(279, 249);
            this.btnAddEmailToCc.Name = "btnAddEmailToCc";
            this.btnAddEmailToCc.Size = new System.Drawing.Size(104, 56);
            this.btnAddEmailToCc.TabIndex = 7;
            this.btnAddEmailToCc.Text = "Add";
            this.btnAddEmailToCc.UseVisualStyleBackColor = true;
            this.btnAddEmailToCc.Click += new System.EventHandler(this.btnAddEmailToCc_Click);
            // 
            // frmAddEmailCc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(398, 425);
            this.Controls.Add(this.btnCancelCc);
            this.Controls.Add(this.btnOkCc);
            this.Controls.Add(this.btnRemoveEmailFromCc);
            this.Controls.Add(this.btnAddEmailToCc);
            this.Controls.Add(this.lbEmailCc);
            this.Controls.Add(this.tvFamilyEmail);
            this.Controls.Add(this.label2);
            this.Name = "frmAddEmailCc";
            this.Text = "Email To Cc";
            this.Load += new System.EventHandler(this.frmAddEmailCc_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TreeView tvFamilyEmail;
        private System.Windows.Forms.ListBox lbEmailCc;
        private System.Windows.Forms.Button btnCancelCc;
        private System.Windows.Forms.Button btnOkCc;
        private System.Windows.Forms.Button btnRemoveEmailFromCc;
        private System.Windows.Forms.Button btnAddEmailToCc;
    }
}