namespace CMMManager
{
    partial class frmAssignedTo
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
            this.btnAssignedTo = new System.Windows.Forms.Button();
            this.txtAssignedTo = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tvStaffNames = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(166, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select Staff Names";
            // 
            // btnAssignedTo
            // 
            this.btnAssignedTo.Location = new System.Drawing.Point(12, 280);
            this.btnAssignedTo.Name = "btnAssignedTo";
            this.btnAssignedTo.Size = new System.Drawing.Size(102, 25);
            this.btnAssignedTo.TabIndex = 2;
            this.btnAssignedTo.Text = "Assigned To ->";
            this.btnAssignedTo.UseVisualStyleBackColor = true;
            // 
            // txtAssignedTo
            // 
            this.txtAssignedTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAssignedTo.Location = new System.Drawing.Point(120, 281);
            this.txtAssignedTo.Name = "txtAssignedTo";
            this.txtAssignedTo.ReadOnly = true;
            this.txtAssignedTo.Size = new System.Drawing.Size(389, 22);
            this.txtAssignedTo.TabIndex = 3;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(311, 320);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(93, 31);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(415, 320);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(93, 31);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // tvStaffNames
            // 
            this.tvStaffNames.Location = new System.Drawing.Point(12, 50);
            this.tvStaffNames.Name = "tvStaffNames";
            this.tvStaffNames.Size = new System.Drawing.Size(496, 225);
            this.tvStaffNames.TabIndex = 6;
            this.tvStaffNames.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvStaffNames_NodeMouseDoubleClick);
            // 
            // frmAssignedTo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 364);
            this.Controls.Add(this.tvStaffNames);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtAssignedTo);
            this.Controls.Add(this.btnAssignedTo);
            this.Controls.Add(this.label1);
            this.Name = "frmAssignedTo";
            this.Text = "Select Staff Names";
            this.Load += new System.EventHandler(this.frmAssignedTo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAssignedTo;
        private System.Windows.Forms.TextBox txtAssignedTo;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TreeView tvStaffNames;
    }
}