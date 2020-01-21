namespace CMMManager
{
    partial class frmUploadNewCaseDoc
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
            this.chkAddOn = new System.Windows.Forms.CheckBox();
            this.comboDocumentType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnUpload = new System.Windows.Forms.Button();
            this.txtCaseDocDestinationPath = new System.Windows.Forms.TextBox();
            this.txtIndividualId = new System.Windows.Forms.TextBox();
            this.txtIndividualName = new System.Windows.Forms.TextBox();
            this.txtCaseNo = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCaseDocNo = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpCaseDocReceivedDate = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCaseDocNote = new System.Windows.Forms.TextBox();
            this.btnViewCaseDoc = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Individual Id:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Individual Name:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Case No:";
            // 
            // chkAddOn
            // 
            this.chkAddOn.AutoCheck = false;
            this.chkAddOn.AutoSize = true;
            this.chkAddOn.Location = new System.Drawing.Point(321, 102);
            this.chkAddOn.Name = "chkAddOn";
            this.chkAddOn.Size = new System.Drawing.Size(62, 17);
            this.chkAddOn.TabIndex = 3;
            this.chkAddOn.Text = "Add On";
            this.chkAddOn.UseVisualStyleBackColor = true;
            // 
            // comboDocumentType
            // 
            this.comboDocumentType.FormattingEnabled = true;
            this.comboDocumentType.Location = new System.Drawing.Point(117, 171);
            this.comboDocumentType.Name = "comboDocumentType";
            this.comboDocumentType.Size = new System.Drawing.Size(118, 21);
            this.comboDocumentType.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 175);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Document Type:";
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(24, 202);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(83, 25);
            this.btnUpload.TabIndex = 6;
            this.btnUpload.Text = "Upload";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // txtCaseDocDestinationPath
            // 
            this.txtCaseDocDestinationPath.Location = new System.Drawing.Point(117, 204);
            this.txtCaseDocDestinationPath.Name = "txtCaseDocDestinationPath";
            this.txtCaseDocDestinationPath.ReadOnly = true;
            this.txtCaseDocDestinationPath.Size = new System.Drawing.Size(177, 20);
            this.txtCaseDocDestinationPath.TabIndex = 7;
            // 
            // txtIndividualId
            // 
            this.txtIndividualId.Location = new System.Drawing.Point(116, 29);
            this.txtIndividualId.Name = "txtIndividualId";
            this.txtIndividualId.ReadOnly = true;
            this.txtIndividualId.Size = new System.Drawing.Size(267, 20);
            this.txtIndividualId.TabIndex = 8;
            // 
            // txtIndividualName
            // 
            this.txtIndividualName.Location = new System.Drawing.Point(116, 61);
            this.txtIndividualName.Name = "txtIndividualName";
            this.txtIndividualName.ReadOnly = true;
            this.txtIndividualName.Size = new System.Drawing.Size(267, 20);
            this.txtIndividualName.TabIndex = 9;
            // 
            // txtCaseNo
            // 
            this.txtCaseNo.Location = new System.Drawing.Point(117, 100);
            this.txtCaseNo.Name = "txtCaseNo";
            this.txtCaseNo.ReadOnly = true;
            this.txtCaseNo.Size = new System.Drawing.Size(198, 20);
            this.txtCaseNo.TabIndex = 10;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(185, 373);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 31);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(293, 373);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 31);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 136);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Case Doc No:";
            // 
            // txtCaseDocNo
            // 
            this.txtCaseDocNo.Location = new System.Drawing.Point(117, 132);
            this.txtCaseDocNo.Name = "txtCaseDocNo";
            this.txtCaseDocNo.ReadOnly = true;
            this.txtCaseDocNo.Size = new System.Drawing.Size(266, 20);
            this.txtCaseDocNo.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(24, 241);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Received Date:";
            // 
            // dtpCaseDocReceivedDate
            // 
            this.dtpCaseDocReceivedDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpCaseDocReceivedDate.Location = new System.Drawing.Point(117, 237);
            this.dtpCaseDocReceivedDate.Name = "dtpCaseDocReceivedDate";
            this.dtpCaseDocReceivedDate.Size = new System.Drawing.Size(118, 20);
            this.dtpCaseDocReceivedDate.TabIndex = 16;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(24, 275);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(30, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "Note";
            // 
            // txtCaseDocNote
            // 
            this.txtCaseDocNote.Location = new System.Drawing.Point(24, 291);
            this.txtCaseDocNote.Multiline = true;
            this.txtCaseDocNote.Name = "txtCaseDocNote";
            this.txtCaseDocNote.Size = new System.Drawing.Size(359, 60);
            this.txtCaseDocNote.TabIndex = 18;
            // 
            // btnViewCaseDoc
            // 
            this.btnViewCaseDoc.Location = new System.Drawing.Point(300, 202);
            this.btnViewCaseDoc.Name = "btnViewCaseDoc";
            this.btnViewCaseDoc.Size = new System.Drawing.Size(83, 25);
            this.btnViewCaseDoc.TabIndex = 19;
            this.btnViewCaseDoc.Text = "View";
            this.btnViewCaseDoc.UseVisualStyleBackColor = true;
            this.btnViewCaseDoc.Click += new System.EventHandler(this.btnViewCaseDoc_Click);
            // 
            // frmUploadNewCaseDoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(415, 425);
            this.Controls.Add(this.btnViewCaseDoc);
            this.Controls.Add(this.txtCaseDocNote);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dtpCaseDocReceivedDate);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtCaseDocNo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtCaseNo);
            this.Controls.Add(this.txtIndividualName);
            this.Controls.Add(this.txtIndividualId);
            this.Controls.Add(this.txtCaseDocDestinationPath);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboDocumentType);
            this.Controls.Add(this.chkAddOn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmUploadNewCaseDoc";
            this.Text = "Upload New Case Document";
            this.Load += new System.EventHandler(this.frmUploadNewCaseDoc_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkAddOn;
        private System.Windows.Forms.ComboBox comboDocumentType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.TextBox txtCaseDocDestinationPath;
        private System.Windows.Forms.TextBox txtIndividualId;
        private System.Windows.Forms.TextBox txtIndividualName;
        private System.Windows.Forms.TextBox txtCaseNo;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCaseDocNo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpCaseDocReceivedDate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtCaseDocNote;
        private System.Windows.Forms.Button btnViewCaseDoc;
    }
}