namespace CMMManager
{
    partial class frmViewEmail
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
            this.btnReply = new System.Windows.Forms.Button();
            this.btnForward = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtRecipient = new System.Windows.Forms.TextBox();
            this.txtSubject = new System.Windows.Forms.TextBox();
            this.txtBody = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnReply
            // 
            this.btnReply.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReply.Location = new System.Drawing.Point(12, 16);
            this.btnReply.Name = "btnReply";
            this.btnReply.Size = new System.Drawing.Size(112, 56);
            this.btnReply.TabIndex = 0;
            this.btnReply.Text = "Reply";
            this.btnReply.UseVisualStyleBackColor = true;
            // 
            // btnForward
            // 
            this.btnForward.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnForward.Location = new System.Drawing.Point(144, 16);
            this.btnForward.Name = "btnForward";
            this.btnForward.Size = new System.Drawing.Size(107, 56);
            this.btnForward.TabIndex = 1;
            this.btnForward.Text = "Forward";
            this.btnForward.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Recipient";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Subject";
            // 
            // txtRecipient
            // 
            this.txtRecipient.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRecipient.Location = new System.Drawing.Point(80, 88);
            this.txtRecipient.Name = "txtRecipient";
            this.txtRecipient.ReadOnly = true;
            this.txtRecipient.Size = new System.Drawing.Size(171, 22);
            this.txtRecipient.TabIndex = 4;
            // 
            // txtSubject
            // 
            this.txtSubject.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubject.Location = new System.Drawing.Point(80, 124);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.ReadOnly = true;
            this.txtSubject.Size = new System.Drawing.Size(171, 22);
            this.txtSubject.TabIndex = 5;
            // 
            // txtBody
            // 
            this.txtBody.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBody.Location = new System.Drawing.Point(12, 162);
            this.txtBody.Multiline = true;
            this.txtBody.Name = "txtBody";
            this.txtBody.ReadOnly = true;
            this.txtBody.Size = new System.Drawing.Size(696, 351);
            this.txtBody.TabIndex = 6;
            // 
            // frmViewEmail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(719, 533);
            this.Controls.Add(this.txtBody);
            this.Controls.Add(this.txtSubject);
            this.Controls.Add(this.txtRecipient);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnForward);
            this.Controls.Add(this.btnReply);
            this.Name = "frmViewEmail";
            this.Text = "View Email";
            this.Load += new System.EventHandler(this.frmViewEmail_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnReply;
        private System.Windows.Forms.Button btnForward;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtRecipient;
        private System.Windows.Forms.TextBox txtSubject;
        private System.Windows.Forms.TextBox txtBody;
    }
}