namespace CMMManager
{
    partial class frmShowIllnessReviewNote
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
            this.lblIndividualId = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtIndividualId = new System.Windows.Forms.TextBox();
            this.txtIllnessNo = new System.Windows.Forms.TextBox();
            this.txtIllnessReviewNote = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblIndividualId
            // 
            this.lblIndividualId.AutoSize = true;
            this.lblIndividualId.Location = new System.Drawing.Point(53, 40);
            this.lblIndividualId.Name = "lblIndividualId";
            this.lblIndividualId.Size = new System.Drawing.Size(67, 13);
            this.lblIndividualId.TabIndex = 0;
            this.lblIndividualId.Text = "Individual Id:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(53, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Illness No:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(53, 122);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Illness Review Note:";
            // 
            // txtIndividualId
            // 
            this.txtIndividualId.Location = new System.Drawing.Point(126, 37);
            this.txtIndividualId.Name = "txtIndividualId";
            this.txtIndividualId.ReadOnly = true;
            this.txtIndividualId.Size = new System.Drawing.Size(126, 20);
            this.txtIndividualId.TabIndex = 3;
            // 
            // txtIllnessNo
            // 
            this.txtIllnessNo.Location = new System.Drawing.Point(126, 82);
            this.txtIllnessNo.Name = "txtIllnessNo";
            this.txtIllnessNo.ReadOnly = true;
            this.txtIllnessNo.Size = new System.Drawing.Size(126, 20);
            this.txtIllnessNo.TabIndex = 4;
            // 
            // txtIllnessReviewNote
            // 
            this.txtIllnessReviewNote.Location = new System.Drawing.Point(56, 139);
            this.txtIllnessReviewNote.Multiline = true;
            this.txtIllnessReviewNote.Name = "txtIllnessReviewNote";
            this.txtIllnessReviewNote.ReadOnly = true;
            this.txtIllnessReviewNote.Size = new System.Drawing.Size(400, 167);
            this.txtIllnessReviewNote.TabIndex = 5;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(347, 339);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(109, 29);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // frmShowIllnessReviewNote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 404);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.txtIllnessReviewNote);
            this.Controls.Add(this.txtIllnessNo);
            this.Controls.Add(this.txtIndividualId);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblIndividualId);
            this.Name = "frmShowIllnessReviewNote";
            this.Text = "Review Notes By James Ahn";
            this.Load += new System.EventHandler(this.frmShowIllnessReviewNote_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblIndividualId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtIndividualId;
        private System.Windows.Forms.TextBox txtIllnessNo;
        private System.Windows.Forms.TextBox txtIllnessReviewNote;
        private System.Windows.Forms.Button btnOk;
    }
}