namespace CMMManager
{
    partial class frmLogCommunication
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
            this.comboCommunicationType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCommunicationNo = new System.Windows.Forms.TextBox();
            this.txtCommunicationSubject = new System.Windows.Forms.TextBox();
            this.txtCommunicationBody = new System.Windows.Forms.TextBox();
            this.btnSaveCommunication = new System.Windows.Forms.Button();
            this.btnCommunicationCancel = new System.Windows.Forms.Button();
            this.txtCommunicationIndividualId = new System.Windows.Forms.TextBox();
            this.comboCaseNo = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(26, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Individual Id:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(309, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Communication No:";
            // 
            // comboCommunicationType
            // 
            this.comboCommunicationType.FormattingEnabled = true;
            this.comboCommunicationType.Location = new System.Drawing.Point(166, 156);
            this.comboCommunicationType.Name = "comboCommunicationType";
            this.comboCommunicationType.Size = new System.Drawing.Size(121, 21);
            this.comboCommunicationType.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(26, 158);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(138, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "Communication Type:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(186, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(173, 25);
            this.label4.TabIndex = 4;
            this.label4.Text = "Communication";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(26, 118);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 16);
            this.label5.TabIndex = 5;
            this.label5.Text = "Case No:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(26, 198);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 16);
            this.label6.TabIndex = 6;
            this.label6.Text = "Subject:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(25, 238);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(40, 16);
            this.label7.TabIndex = 7;
            this.label7.Text = "Body";
            // 
            // txtCommunicationNo
            // 
            this.txtCommunicationNo.Location = new System.Drawing.Point(436, 74);
            this.txtCommunicationNo.Name = "txtCommunicationNo";
            this.txtCommunicationNo.ReadOnly = true;
            this.txtCommunicationNo.Size = new System.Drawing.Size(121, 20);
            this.txtCommunicationNo.TabIndex = 8;
            // 
            // txtCommunicationSubject
            // 
            this.txtCommunicationSubject.Location = new System.Drawing.Point(89, 197);
            this.txtCommunicationSubject.Name = "txtCommunicationSubject";
            this.txtCommunicationSubject.Size = new System.Drawing.Size(468, 20);
            this.txtCommunicationSubject.TabIndex = 10;
            // 
            // txtCommunicationBody
            // 
            this.txtCommunicationBody.Location = new System.Drawing.Point(28, 258);
            this.txtCommunicationBody.Multiline = true;
            this.txtCommunicationBody.Name = "txtCommunicationBody";
            this.txtCommunicationBody.Size = new System.Drawing.Size(529, 222);
            this.txtCommunicationBody.TabIndex = 11;
            // 
            // btnSaveCommunication
            // 
            this.btnSaveCommunication.Location = new System.Drawing.Point(326, 507);
            this.btnSaveCommunication.Name = "btnSaveCommunication";
            this.btnSaveCommunication.Size = new System.Drawing.Size(102, 33);
            this.btnSaveCommunication.TabIndex = 12;
            this.btnSaveCommunication.Text = "Save";
            this.btnSaveCommunication.UseVisualStyleBackColor = true;
            this.btnSaveCommunication.Click += new System.EventHandler(this.btnSaveCommunication_Click);
            // 
            // btnCommunicationCancel
            // 
            this.btnCommunicationCancel.Location = new System.Drawing.Point(455, 507);
            this.btnCommunicationCancel.Name = "btnCommunicationCancel";
            this.btnCommunicationCancel.Size = new System.Drawing.Size(102, 33);
            this.btnCommunicationCancel.TabIndex = 13;
            this.btnCommunicationCancel.Text = "Cancel";
            this.btnCommunicationCancel.UseVisualStyleBackColor = true;
            this.btnCommunicationCancel.Click += new System.EventHandler(this.btnCommunicationCancel_Click);
            // 
            // txtCommunicationIndividualId
            // 
            this.txtCommunicationIndividualId.Location = new System.Drawing.Point(166, 76);
            this.txtCommunicationIndividualId.Name = "txtCommunicationIndividualId";
            this.txtCommunicationIndividualId.ReadOnly = true;
            this.txtCommunicationIndividualId.Size = new System.Drawing.Size(121, 20);
            this.txtCommunicationIndividualId.TabIndex = 14;
            // 
            // comboCaseNo
            // 
            this.comboCaseNo.FormattingEnabled = true;
            this.comboCaseNo.Location = new System.Drawing.Point(166, 117);
            this.comboCaseNo.Name = "comboCaseNo";
            this.comboCaseNo.Size = new System.Drawing.Size(121, 21);
            this.comboCaseNo.TabIndex = 15;
            // 
            // frmLogCommunication
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 567);
            this.Controls.Add(this.comboCaseNo);
            this.Controls.Add(this.txtCommunicationIndividualId);
            this.Controls.Add(this.btnCommunicationCancel);
            this.Controls.Add(this.btnSaveCommunication);
            this.Controls.Add(this.txtCommunicationBody);
            this.Controls.Add(this.txtCommunicationSubject);
            this.Controls.Add(this.txtCommunicationNo);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboCommunicationType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmLogCommunication";
            this.Text = "Log a Communication";
            this.Load += new System.EventHandler(this.frmLogCommunication_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboCommunicationType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtCommunicationNo;
        private System.Windows.Forms.TextBox txtCommunicationSubject;
        private System.Windows.Forms.TextBox txtCommunicationBody;
        private System.Windows.Forms.Button btnSaveCommunication;
        private System.Windows.Forms.Button btnCommunicationCancel;
        private System.Windows.Forms.TextBox txtCommunicationIndividualId;
        private System.Windows.Forms.ComboBox comboCaseNo;
    }
}