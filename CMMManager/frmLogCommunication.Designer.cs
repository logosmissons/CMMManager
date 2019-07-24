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
            this.gvCommunicationAttachment = new System.Windows.Forms.DataGridView();
            this.label8 = new System.Windows.Forms.Label();
            this.btnAddNewAttachment = new System.Windows.Forms.Button();
            this.btnDeleteAttachment = new System.Windows.Forms.Button();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SelectedCommunicationAttachment = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.CommunicationAttachmentNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UploadCommunicationAttachment = new System.Windows.Forms.DataGridViewButtonColumn();
            this.CommunicationAttachmentFileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ViewCommunicationAttachment = new System.Windows.Forms.DataGridViewButtonColumn();
            this.CreatedByCommunicationAttachment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreateDateCommunicationAttachment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gvCommunicationAttachment)).BeginInit();
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
            this.label2.Location = new System.Drawing.Point(397, 78);
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
            this.comboCommunicationType.Size = new System.Drawing.Size(167, 21);
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
            this.label4.Location = new System.Drawing.Point(244, 24);
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
            this.txtCommunicationNo.Location = new System.Drawing.Point(524, 76);
            this.txtCommunicationNo.Name = "txtCommunicationNo";
            this.txtCommunicationNo.ReadOnly = true;
            this.txtCommunicationNo.Size = new System.Drawing.Size(121, 20);
            this.txtCommunicationNo.TabIndex = 8;
            // 
            // txtCommunicationSubject
            // 
            this.txtCommunicationSubject.Location = new System.Drawing.Point(89, 197);
            this.txtCommunicationSubject.Name = "txtCommunicationSubject";
            this.txtCommunicationSubject.Size = new System.Drawing.Size(556, 20);
            this.txtCommunicationSubject.TabIndex = 10;
            // 
            // txtCommunicationBody
            // 
            this.txtCommunicationBody.Location = new System.Drawing.Point(29, 257);
            this.txtCommunicationBody.Multiline = true;
            this.txtCommunicationBody.Name = "txtCommunicationBody";
            this.txtCommunicationBody.Size = new System.Drawing.Size(616, 110);
            this.txtCommunicationBody.TabIndex = 11;
            // 
            // btnSaveCommunication
            // 
            this.btnSaveCommunication.Location = new System.Drawing.Point(414, 599);
            this.btnSaveCommunication.Name = "btnSaveCommunication";
            this.btnSaveCommunication.Size = new System.Drawing.Size(102, 33);
            this.btnSaveCommunication.TabIndex = 12;
            this.btnSaveCommunication.Text = "Save";
            this.btnSaveCommunication.UseVisualStyleBackColor = true;
            this.btnSaveCommunication.Click += new System.EventHandler(this.btnSaveCommunication_Click);
            // 
            // btnCommunicationCancel
            // 
            this.btnCommunicationCancel.Location = new System.Drawing.Point(543, 599);
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
            this.txtCommunicationIndividualId.Size = new System.Drawing.Size(167, 20);
            this.txtCommunicationIndividualId.TabIndex = 14;
            // 
            // comboCaseNo
            // 
            this.comboCaseNo.FormattingEnabled = true;
            this.comboCaseNo.Location = new System.Drawing.Point(166, 117);
            this.comboCaseNo.Name = "comboCaseNo";
            this.comboCaseNo.Size = new System.Drawing.Size(167, 21);
            this.comboCaseNo.TabIndex = 15;
            // 
            // gvCommunicationAttachment
            // 
            this.gvCommunicationAttachment.AllowUserToAddRows = false;
            this.gvCommunicationAttachment.AllowUserToDeleteRows = false;
            this.gvCommunicationAttachment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvCommunicationAttachment.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SelectedCommunicationAttachment,
            this.CommunicationAttachmentNo,
            this.UploadCommunicationAttachment,
            this.CommunicationAttachmentFileName,
            this.ViewCommunicationAttachment,
            this.CreatedByCommunicationAttachment,
            this.CreateDateCommunicationAttachment});
            this.gvCommunicationAttachment.Location = new System.Drawing.Point(29, 417);
            this.gvCommunicationAttachment.Name = "gvCommunicationAttachment";
            this.gvCommunicationAttachment.Size = new System.Drawing.Size(616, 158);
            this.gvCommunicationAttachment.TabIndex = 16;
            this.gvCommunicationAttachment.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvCommunicationAttachment_CellClick);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(27, 398);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 16);
            this.label8.TabIndex = 17;
            this.label8.Text = "Attachment";
            // 
            // btnAddNewAttachment
            // 
            this.btnAddNewAttachment.Location = new System.Drawing.Point(119, 383);
            this.btnAddNewAttachment.Name = "btnAddNewAttachment";
            this.btnAddNewAttachment.Size = new System.Drawing.Size(96, 28);
            this.btnAddNewAttachment.TabIndex = 18;
            this.btnAddNewAttachment.Text = "Add New";
            this.btnAddNewAttachment.UseVisualStyleBackColor = true;
            this.btnAddNewAttachment.Click += new System.EventHandler(this.btnAddNewAttachment_Click);
            // 
            // btnDeleteAttachment
            // 
            this.btnDeleteAttachment.Location = new System.Drawing.Point(237, 383);
            this.btnDeleteAttachment.Name = "btnDeleteAttachment";
            this.btnDeleteAttachment.Size = new System.Drawing.Size(96, 28);
            this.btnDeleteAttachment.TabIndex = 19;
            this.btnDeleteAttachment.Text = "Delete";
            this.btnDeleteAttachment.UseVisualStyleBackColor = true;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Destination Path";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 180;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Created By";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 90;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Create Date";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 90;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Create Date";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 90;
            // 
            // SelectedCommunicationAttachment
            // 
            this.SelectedCommunicationAttachment.HeaderText = "";
            this.SelectedCommunicationAttachment.Name = "SelectedCommunicationAttachment";
            this.SelectedCommunicationAttachment.Width = 30;
            // 
            // CommunicationAttachmentNo
            // 
            this.CommunicationAttachmentNo.HeaderText = "Att No.";
            this.CommunicationAttachmentNo.Name = "CommunicationAttachmentNo";
            this.CommunicationAttachmentNo.Width = 80;
            // 
            // UploadCommunicationAttachment
            // 
            this.UploadCommunicationAttachment.HeaderText = "Upload";
            this.UploadCommunicationAttachment.Name = "UploadCommunicationAttachment";
            this.UploadCommunicationAttachment.Width = 70;
            // 
            // CommunicationAttachmentFileName
            // 
            this.CommunicationAttachmentFileName.HeaderText = "File Name";
            this.CommunicationAttachmentFileName.Name = "CommunicationAttachmentFileName";
            this.CommunicationAttachmentFileName.ReadOnly = true;
            this.CommunicationAttachmentFileName.Width = 200;
            // 
            // ViewCommunicationAttachment
            // 
            this.ViewCommunicationAttachment.HeaderText = "View";
            this.ViewCommunicationAttachment.Name = "ViewCommunicationAttachment";
            this.ViewCommunicationAttachment.Width = 70;
            // 
            // CreatedByCommunicationAttachment
            // 
            this.CreatedByCommunicationAttachment.HeaderText = "Created By";
            this.CreatedByCommunicationAttachment.Name = "CreatedByCommunicationAttachment";
            this.CreatedByCommunicationAttachment.Width = 90;
            // 
            // CreateDateCommunicationAttachment
            // 
            this.CreateDateCommunicationAttachment.HeaderText = "Create Date";
            this.CreateDateCommunicationAttachment.Name = "CreateDateCommunicationAttachment";
            this.CreateDateCommunicationAttachment.Width = 120;
            // 
            // frmLogCommunication
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(683, 644);
            this.Controls.Add(this.btnDeleteAttachment);
            this.Controls.Add(this.btnAddNewAttachment);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.gvCommunicationAttachment);
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
            ((System.ComponentModel.ISupportInitialize)(this.gvCommunicationAttachment)).EndInit();
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
        private System.Windows.Forms.DataGridView gvCommunicationAttachment;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnAddNewAttachment;
        private System.Windows.Forms.Button btnDeleteAttachment;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SelectedCommunicationAttachment;
        private System.Windows.Forms.DataGridViewTextBoxColumn CommunicationAttachmentNo;
        private System.Windows.Forms.DataGridViewButtonColumn UploadCommunicationAttachment;
        private System.Windows.Forms.DataGridViewTextBoxColumn CommunicationAttachmentFileName;
        private System.Windows.Forms.DataGridViewButtonColumn ViewCommunicationAttachment;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreatedByCommunicationAttachment;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreateDateCommunicationAttachment;
    }
}