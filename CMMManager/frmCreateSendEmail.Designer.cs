namespace CMMManager
{
    partial class frmCreateSendEmail
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboCase = new System.Windows.Forms.ComboBox();
            this.comboIncident = new System.Windows.Forms.ComboBox();
            this.comboIllness = new System.Windows.Forms.ComboBox();
            this.btnSendEmail = new System.Windows.Forms.Button();
            this.btnEmailFrom = new System.Windows.Forms.Button();
            this.btnEmailBcc = new System.Windows.Forms.Button();
            this.btnEmailTo = new System.Windows.Forms.Button();
            this.comboEmailFrom = new System.Windows.Forms.ComboBox();
            this.txtEmailTo = new System.Windows.Forms.TextBox();
            this.txtEmailBCC = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtEmailSubject = new System.Windows.Forms.TextBox();
            this.btnAttachFile = new System.Windows.Forms.Button();
            this.txtEmailBody = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDeleteAttachment = new System.Windows.Forms.Button();
            this.lbAttachments = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select Case:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Select Incident:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Select Illness:";
            // 
            // comboCase
            // 
            this.comboCase.FormattingEnabled = true;
            this.comboCase.Location = new System.Drawing.Point(125, 24);
            this.comboCase.Name = "comboCase";
            this.comboCase.Size = new System.Drawing.Size(166, 21);
            this.comboCase.TabIndex = 4;
            this.comboCase.SelectedIndexChanged += new System.EventHandler(this.comboCase_SelectedIndexChanged);
            // 
            // comboIncident
            // 
            this.comboIncident.FormattingEnabled = true;
            this.comboIncident.Location = new System.Drawing.Point(125, 94);
            this.comboIncident.Name = "comboIncident";
            this.comboIncident.Size = new System.Drawing.Size(166, 21);
            this.comboIncident.TabIndex = 5;
            // 
            // comboIllness
            // 
            this.comboIllness.FormattingEnabled = true;
            this.comboIllness.Location = new System.Drawing.Point(125, 59);
            this.comboIllness.Name = "comboIllness";
            this.comboIllness.Size = new System.Drawing.Size(166, 21);
            this.comboIllness.TabIndex = 6;
            this.comboIllness.SelectedIndexChanged += new System.EventHandler(this.comboIllness_SelectedIndexChanged);
            // 
            // btnSendEmail
            // 
            this.btnSendEmail.Enabled = false;
            this.btnSendEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSendEmail.Location = new System.Drawing.Point(679, 560);
            this.btnSendEmail.Name = "btnSendEmail";
            this.btnSendEmail.Size = new System.Drawing.Size(172, 40);
            this.btnSendEmail.TabIndex = 7;
            this.btnSendEmail.Text = "Send";
            this.btnSendEmail.UseVisualStyleBackColor = true;
            this.btnSendEmail.Click += new System.EventHandler(this.btnSendEmail_Click);
            // 
            // btnEmailFrom
            // 
            this.btnEmailFrom.Location = new System.Drawing.Point(316, 24);
            this.btnEmailFrom.Name = "btnEmailFrom";
            this.btnEmailFrom.Size = new System.Drawing.Size(83, 30);
            this.btnEmailFrom.TabIndex = 8;
            this.btnEmailFrom.Text = "From";
            this.btnEmailFrom.UseVisualStyleBackColor = true;
            // 
            // btnEmailBcc
            // 
            this.btnEmailBcc.Location = new System.Drawing.Point(316, 95);
            this.btnEmailBcc.Name = "btnEmailBcc";
            this.btnEmailBcc.Size = new System.Drawing.Size(83, 30);
            this.btnEmailBcc.TabIndex = 9;
            this.btnEmailBcc.Text = "Bcc...";
            this.btnEmailBcc.UseVisualStyleBackColor = true;
            this.btnEmailBcc.Click += new System.EventHandler(this.btnEmailBcc_Click);
            // 
            // btnEmailTo
            // 
            this.btnEmailTo.Location = new System.Drawing.Point(316, 59);
            this.btnEmailTo.Name = "btnEmailTo";
            this.btnEmailTo.Size = new System.Drawing.Size(83, 30);
            this.btnEmailTo.TabIndex = 10;
            this.btnEmailTo.Text = "To...";
            this.btnEmailTo.UseVisualStyleBackColor = true;
            // 
            // comboEmailFrom
            // 
            this.comboEmailFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboEmailFrom.FormattingEnabled = true;
            this.comboEmailFrom.Location = new System.Drawing.Point(405, 27);
            this.comboEmailFrom.Name = "comboEmailFrom";
            this.comboEmailFrom.Size = new System.Drawing.Size(553, 24);
            this.comboEmailFrom.TabIndex = 11;
            // 
            // txtEmailTo
            // 
            this.txtEmailTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmailTo.Location = new System.Drawing.Point(405, 63);
            this.txtEmailTo.Name = "txtEmailTo";
            this.txtEmailTo.Size = new System.Drawing.Size(553, 22);
            this.txtEmailTo.TabIndex = 12;
            // 
            // txtEmailBCC
            // 
            this.txtEmailBCC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmailBCC.Location = new System.Drawing.Point(405, 99);
            this.txtEmailBCC.Name = "txtEmailBCC";
            this.txtEmailBCC.ReadOnly = true;
            this.txtEmailBCC.Size = new System.Drawing.Size(553, 22);
            this.txtEmailBCC.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(343, 143);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 16);
            this.label4.TabIndex = 14;
            this.label4.Text = "Subject:";
            // 
            // txtEmailSubject
            // 
            this.txtEmailSubject.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmailSubject.Location = new System.Drawing.Point(405, 140);
            this.txtEmailSubject.Name = "txtEmailSubject";
            this.txtEmailSubject.Size = new System.Drawing.Size(553, 22);
            this.txtEmailSubject.TabIndex = 15;
            // 
            // btnAttachFile
            // 
            this.btnAttachFile.Location = new System.Drawing.Point(316, 183);
            this.btnAttachFile.Name = "btnAttachFile";
            this.btnAttachFile.Size = new System.Drawing.Size(83, 30);
            this.btnAttachFile.TabIndex = 16;
            this.btnAttachFile.Text = "Attach File";
            this.btnAttachFile.UseVisualStyleBackColor = true;
            this.btnAttachFile.Click += new System.EventHandler(this.btnAttachFile_Click);
            // 
            // txtEmailBody
            // 
            this.txtEmailBody.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmailBody.Location = new System.Drawing.Point(24, 271);
            this.txtEmailBody.Multiline = true;
            this.txtEmailBody.Name = "txtEmailBody";
            this.txtEmailBody.Size = new System.Drawing.Size(934, 262);
            this.txtEmailBody.TabIndex = 18;
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(868, 560);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 40);
            this.btnCancel.TabIndex = 19;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDeleteAttachment
            // 
            this.btnDeleteAttachment.Location = new System.Drawing.Point(316, 219);
            this.btnDeleteAttachment.Name = "btnDeleteAttachment";
            this.btnDeleteAttachment.Size = new System.Drawing.Size(83, 30);
            this.btnDeleteAttachment.TabIndex = 21;
            this.btnDeleteAttachment.Text = "Delete";
            this.btnDeleteAttachment.UseVisualStyleBackColor = true;
            this.btnDeleteAttachment.Click += new System.EventHandler(this.btnDeleteAttachment_Click);
            // 
            // lbAttachments
            // 
            this.lbAttachments.FormattingEnabled = true;
            this.lbAttachments.Location = new System.Drawing.Point(405, 183);
            this.lbAttachments.Name = "lbAttachments";
            this.lbAttachments.Size = new System.Drawing.Size(553, 82);
            this.lbAttachments.TabIndex = 22;
            // 
            // frmCreateSendEmail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(989, 623);
            this.Controls.Add(this.lbAttachments);
            this.Controls.Add(this.btnDeleteAttachment);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtEmailBody);
            this.Controls.Add(this.btnAttachFile);
            this.Controls.Add(this.txtEmailSubject);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtEmailBCC);
            this.Controls.Add(this.txtEmailTo);
            this.Controls.Add(this.comboEmailFrom);
            this.Controls.Add(this.btnEmailTo);
            this.Controls.Add(this.btnEmailBcc);
            this.Controls.Add(this.btnEmailFrom);
            this.Controls.Add(this.btnSendEmail);
            this.Controls.Add(this.comboIllness);
            this.Controls.Add(this.comboIncident);
            this.Controls.Add(this.comboCase);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmCreateSendEmail";
            this.Text = "Email Message";
            this.Load += new System.EventHandler(this.frmCreateSendEmail_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboCase;
        private System.Windows.Forms.ComboBox comboIncident;
        private System.Windows.Forms.ComboBox comboIllness;
        private System.Windows.Forms.Button btnSendEmail;
        private System.Windows.Forms.Button btnEmailFrom;
        private System.Windows.Forms.Button btnEmailBcc;
        private System.Windows.Forms.Button btnEmailTo;
        private System.Windows.Forms.ComboBox comboEmailFrom;
        private System.Windows.Forms.TextBox txtEmailTo;
        private System.Windows.Forms.TextBox txtEmailBCC;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtEmailSubject;
        private System.Windows.Forms.Button btnAttachFile;
        private System.Windows.Forms.TextBox txtEmailBody;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDeleteAttachment;
        private System.Windows.Forms.ListBox lbAttachments;
    }
}