namespace CMMManager
{
    partial class frmIllnessCreationPage
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
            this.grpIllnessNote = new System.Windows.Forms.GroupBox();
            this.txtConclusion = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtIllnessNote = new System.Windows.Forms.TextBox();
            this.dtpCreateDate = new System.Windows.Forms.DateTimePicker();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSaveIllness = new System.Windows.Forms.Button();
            this.txtICD10Code = new System.Windows.Forms.TextBox();
            this.txtIndividualNo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDiseaseName = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.dtpDateOfDiagnosis = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.comboLimitedSharing = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtLimitedSharingYearlyLimit = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtIllnessNo = new System.Windows.Forms.TextBox();
            this.rbEligible = new System.Windows.Forms.RadioButton();
            this.rbIneligible = new System.Windows.Forms.RadioButton();
            this.label13 = new System.Windows.Forms.Label();
            this.txtYearlyLimitBalance = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.comboIllnessProgram = new System.Windows.Forms.ComboBox();
            this.comboCaseNoIllness = new System.Windows.Forms.ComboBox();
            this.grpAttachment = new System.Windows.Forms.GroupBox();
            this.btnDeleteAttachment = new System.Windows.Forms.Button();
            this.btnAddAttachment = new System.Windows.Forms.Button();
            this.gvIllnessAttachment = new System.Windows.Forms.DataGridView();
            this.SelectedIllnessAttachment = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IllnessAttachmentNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UploadIllnessAttachment = new System.Windows.Forms.DataGridViewButtonColumn();
            this.IllnessAttachmentFileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IllnessAttachmentView = new System.Windows.Forms.DataGridViewButtonColumn();
            this.CreatedByIllnessAttachment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreateDateIllnessAttachment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpIllnessNote.SuspendLayout();
            this.grpAttachment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvIllnessAttachment)).BeginInit();
            this.SuspendLayout();
            // 
            // grpIllnessNote
            // 
            this.grpIllnessNote.Controls.Add(this.txtConclusion);
            this.grpIllnessNote.Controls.Add(this.label7);
            this.grpIllnessNote.Controls.Add(this.label6);
            this.grpIllnessNote.Controls.Add(this.txtIllnessNote);
            this.grpIllnessNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpIllnessNote.Location = new System.Drawing.Point(32, 249);
            this.grpIllnessNote.Name = "grpIllnessNote";
            this.grpIllnessNote.Size = new System.Drawing.Size(817, 423);
            this.grpIllnessNote.TabIndex = 26;
            this.grpIllnessNote.TabStop = false;
            this.grpIllnessNote.Text = "Note";
            // 
            // txtConclusion
            // 
            this.txtConclusion.Location = new System.Drawing.Point(24, 204);
            this.txtConclusion.Multiline = true;
            this.txtConclusion.Name = "txtConclusion";
            this.txtConclusion.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtConclusion.Size = new System.Drawing.Size(773, 202);
            this.txtConclusion.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(21, 185);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 16);
            this.label7.TabIndex = 13;
            this.label7.Text = "Review Note";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(78, 16);
            this.label6.TabIndex = 12;
            this.label6.Text = "Illness Note";
            // 
            // txtIllnessNote
            // 
            this.txtIllnessNote.Location = new System.Drawing.Point(24, 45);
            this.txtIllnessNote.Multiline = true;
            this.txtIllnessNote.Name = "txtIllnessNote";
            this.txtIllnessNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtIllnessNote.Size = new System.Drawing.Size(773, 127);
            this.txtIllnessNote.TabIndex = 9;
            // 
            // dtpCreateDate
            // 
            this.dtpCreateDate.Enabled = false;
            this.dtpCreateDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpCreateDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpCreateDate.Location = new System.Drawing.Point(441, 173);
            this.dtpCreateDate.Name = "dtpCreateDate";
            this.dtpCreateDate.Size = new System.Drawing.Size(137, 22);
            this.dtpCreateDate.TabIndex = 25;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(750, 883);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(99, 29);
            this.btnClose.TabIndex = 23;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSaveIllness
            // 
            this.btnSaveIllness.Location = new System.Drawing.Point(635, 883);
            this.btnSaveIllness.Name = "btnSaveIllness";
            this.btnSaveIllness.Size = new System.Drawing.Size(99, 29);
            this.btnSaveIllness.TabIndex = 22;
            this.btnSaveIllness.Text = "Save";
            this.btnSaveIllness.UseVisualStyleBackColor = true;
            this.btnSaveIllness.Click += new System.EventHandler(this.btnSaveIllness_Click);
            // 
            // txtICD10Code
            // 
            this.txtICD10Code.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtICD10Code.Location = new System.Drawing.Point(138, 117);
            this.txtICD10Code.Name = "txtICD10Code";
            this.txtICD10Code.Size = new System.Drawing.Size(150, 22);
            this.txtICD10Code.TabIndex = 20;
            this.txtICD10Code.TextChanged += new System.EventHandler(this.txtICD10Code_TextChanged);
            // 
            // txtIndividualNo
            // 
            this.txtIndividualNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIndividualNo.Location = new System.Drawing.Point(440, 80);
            this.txtIndividualNo.Name = "txtIndividualNo";
            this.txtIndividualNo.ReadOnly = true;
            this.txtIndividualNo.Size = new System.Drawing.Size(138, 22);
            this.txtIndividualNo.TabIndex = 19;
            this.txtIndividualNo.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(305, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(157, 29);
            this.label5.TabIndex = 18;
            this.label5.Text = "Illness Form";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(333, 176);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 16);
            this.label3.TabIndex = 17;
            this.label3.Text = "Create Date:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(29, 119);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 16);
            this.label2.TabIndex = 16;
            this.label2.Text = "ICD10 Code:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(345, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 16);
            this.label1.TabIndex = 15;
            this.label1.Text = "Individual No:";
            // 
            // txtDiseaseName
            // 
            this.txtDiseaseName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiseaseName.Location = new System.Drawing.Point(294, 117);
            this.txtDiseaseName.Name = "txtDiseaseName";
            this.txtDiseaseName.Size = new System.Drawing.Size(555, 22);
            this.txtDiseaseName.TabIndex = 27;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(29, 176);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(103, 16);
            this.label8.TabIndex = 28;
            this.label8.Text = "Date of Service:";
            // 
            // dtpDateOfDiagnosis
            // 
            this.dtpDateOfDiagnosis.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDateOfDiagnosis.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDateOfDiagnosis.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateOfDiagnosis.Location = new System.Drawing.Point(138, 173);
            this.dtpDateOfDiagnosis.Name = "dtpDateOfDiagnosis";
            this.dtpDateOfDiagnosis.Size = new System.Drawing.Size(150, 22);
            this.dtpDateOfDiagnosis.TabIndex = 29;
            this.dtpDateOfDiagnosis.ValueChanged += new System.EventHandler(this.dtpDateOfDiagnosis_ValueChanged);
            this.dtpDateOfDiagnosis.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpDateOfDiagnosis_KeyDown);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(624, 83);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(64, 16);
            this.label9.TabIndex = 31;
            this.label9.Text = "Case No:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(29, 212);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(103, 16);
            this.label10.TabIndex = 33;
            this.label10.Text = "Limited Sharing:";
            // 
            // comboLimitedSharing
            // 
            this.comboLimitedSharing.FormattingEnabled = true;
            this.comboLimitedSharing.Location = new System.Drawing.Point(138, 210);
            this.comboLimitedSharing.Name = "comboLimitedSharing";
            this.comboLimitedSharing.Size = new System.Drawing.Size(150, 21);
            this.comboLimitedSharing.TabIndex = 34;
            this.comboLimitedSharing.SelectedIndexChanged += new System.EventHandler(this.comboLimitedSharing_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(333, 212);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(87, 16);
            this.label11.TabIndex = 35;
            this.label11.Text = "Yearly Limits:";
            // 
            // txtLimitedSharingYearlyLimit
            // 
            this.txtLimitedSharingYearlyLimit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLimitedSharingYearlyLimit.Location = new System.Drawing.Point(441, 208);
            this.txtLimitedSharingYearlyLimit.Name = "txtLimitedSharingYearlyLimit";
            this.txtLimitedSharingYearlyLimit.ReadOnly = true;
            this.txtLimitedSharingYearlyLimit.Size = new System.Drawing.Size(137, 22);
            this.txtLimitedSharingYearlyLimit.TabIndex = 36;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(29, 83);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(70, 16);
            this.label12.TabIndex = 37;
            this.label12.Text = "Illness No:";
            // 
            // txtIllnessNo
            // 
            this.txtIllnessNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIllnessNo.Location = new System.Drawing.Point(138, 80);
            this.txtIllnessNo.Name = "txtIllnessNo";
            this.txtIllnessNo.ReadOnly = true;
            this.txtIllnessNo.Size = new System.Drawing.Size(150, 22);
            this.txtIllnessNo.TabIndex = 38;
            // 
            // rbEligible
            // 
            this.rbEligible.AutoCheck = false;
            this.rbEligible.AutoSize = true;
            this.rbEligible.Location = new System.Drawing.Point(305, 147);
            this.rbEligible.Name = "rbEligible";
            this.rbEligible.Size = new System.Drawing.Size(58, 17);
            this.rbEligible.TabIndex = 39;
            this.rbEligible.TabStop = true;
            this.rbEligible.Text = "Eligible";
            this.rbEligible.UseVisualStyleBackColor = true;
            // 
            // rbIneligible
            // 
            this.rbIneligible.AutoCheck = false;
            this.rbIneligible.AutoSize = true;
            this.rbIneligible.Location = new System.Drawing.Point(390, 147);
            this.rbIneligible.Name = "rbIneligible";
            this.rbIneligible.Size = new System.Drawing.Size(66, 17);
            this.rbIneligible.TabIndex = 40;
            this.rbIneligible.TabStop = true;
            this.rbIneligible.Text = "Ineligible";
            this.rbIneligible.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(625, 212);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(61, 16);
            this.label13.TabIndex = 41;
            this.label13.Text = "Balance:";
            // 
            // txtYearlyLimitBalance
            // 
            this.txtYearlyLimitBalance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtYearlyLimitBalance.Location = new System.Drawing.Point(694, 208);
            this.txtYearlyLimitBalance.Name = "txtYearlyLimitBalance";
            this.txtYearlyLimitBalance.ReadOnly = true;
            this.txtYearlyLimitBalance.Size = new System.Drawing.Size(155, 22);
            this.txtYearlyLimitBalance.TabIndex = 42;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(625, 176);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(63, 16);
            this.label14.TabIndex = 46;
            this.label14.Text = "Program:";
            // 
            // comboIllnessProgram
            // 
            this.comboIllnessProgram.FormattingEnabled = true;
            this.comboIllnessProgram.Location = new System.Drawing.Point(694, 174);
            this.comboIllnessProgram.Name = "comboIllnessProgram";
            this.comboIllnessProgram.Size = new System.Drawing.Size(155, 21);
            this.comboIllnessProgram.TabIndex = 47;
            // 
            // comboCaseNoIllness
            // 
            this.comboCaseNoIllness.FormattingEnabled = true;
            this.comboCaseNoIllness.Location = new System.Drawing.Point(694, 81);
            this.comboCaseNoIllness.Name = "comboCaseNoIllness";
            this.comboCaseNoIllness.Size = new System.Drawing.Size(155, 21);
            this.comboCaseNoIllness.TabIndex = 48;
            // 
            // grpAttachment
            // 
            this.grpAttachment.Controls.Add(this.btnDeleteAttachment);
            this.grpAttachment.Controls.Add(this.btnAddAttachment);
            this.grpAttachment.Controls.Add(this.gvIllnessAttachment);
            this.grpAttachment.Location = new System.Drawing.Point(32, 699);
            this.grpAttachment.Name = "grpAttachment";
            this.grpAttachment.Size = new System.Drawing.Size(817, 166);
            this.grpAttachment.TabIndex = 49;
            this.grpAttachment.TabStop = false;
            this.grpAttachment.Text = "Attachments";
            // 
            // btnDeleteAttachment
            // 
            this.btnDeleteAttachment.Location = new System.Drawing.Point(161, 20);
            this.btnDeleteAttachment.Name = "btnDeleteAttachment";
            this.btnDeleteAttachment.Size = new System.Drawing.Size(112, 23);
            this.btnDeleteAttachment.TabIndex = 2;
            this.btnDeleteAttachment.Text = "Delete";
            this.btnDeleteAttachment.UseVisualStyleBackColor = true;
            this.btnDeleteAttachment.Click += new System.EventHandler(this.btnDeleteAttachment_Click);
            // 
            // btnAddAttachment
            // 
            this.btnAddAttachment.Location = new System.Drawing.Point(24, 20);
            this.btnAddAttachment.Name = "btnAddAttachment";
            this.btnAddAttachment.Size = new System.Drawing.Size(112, 23);
            this.btnAddAttachment.TabIndex = 1;
            this.btnAddAttachment.Text = "Add";
            this.btnAddAttachment.UseVisualStyleBackColor = true;
            this.btnAddAttachment.Click += new System.EventHandler(this.btnAddAttachment_Click);
            // 
            // gvIllnessAttachment
            // 
            this.gvIllnessAttachment.AllowUserToAddRows = false;
            this.gvIllnessAttachment.AllowUserToDeleteRows = false;
            this.gvIllnessAttachment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvIllnessAttachment.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SelectedIllnessAttachment,
            this.IllnessAttachmentNo,
            this.UploadIllnessAttachment,
            this.IllnessAttachmentFileName,
            this.IllnessAttachmentView,
            this.CreatedByIllnessAttachment,
            this.CreateDateIllnessAttachment});
            this.gvIllnessAttachment.Location = new System.Drawing.Point(24, 51);
            this.gvIllnessAttachment.Name = "gvIllnessAttachment";
            this.gvIllnessAttachment.Size = new System.Drawing.Size(773, 99);
            this.gvIllnessAttachment.TabIndex = 0;
            this.gvIllnessAttachment.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvIllnessAttachment_CellClick);
            // 
            // SelectedIllnessAttachment
            // 
            this.SelectedIllnessAttachment.HeaderText = "";
            this.SelectedIllnessAttachment.Name = "SelectedIllnessAttachment";
            this.SelectedIllnessAttachment.Width = 40;
            // 
            // IllnessAttachmentNo
            // 
            this.IllnessAttachmentNo.HeaderText = "Att No";
            this.IllnessAttachmentNo.Name = "IllnessAttachmentNo";
            this.IllnessAttachmentNo.Width = 120;
            // 
            // UploadIllnessAttachment
            // 
            this.UploadIllnessAttachment.HeaderText = "Upload";
            this.UploadIllnessAttachment.Name = "UploadIllnessAttachment";
            this.UploadIllnessAttachment.Width = 80;
            // 
            // IllnessAttachmentFileName
            // 
            this.IllnessAttachmentFileName.HeaderText = "Path Name";
            this.IllnessAttachmentFileName.Name = "IllnessAttachmentFileName";
            this.IllnessAttachmentFileName.Width = 160;
            // 
            // IllnessAttachmentView
            // 
            this.IllnessAttachmentView.HeaderText = "View";
            this.IllnessAttachmentView.Name = "IllnessAttachmentView";
            this.IllnessAttachmentView.Width = 80;
            // 
            // CreatedByIllnessAttachment
            // 
            this.CreatedByIllnessAttachment.HeaderText = "Created By";
            this.CreatedByIllnessAttachment.Name = "CreatedByIllnessAttachment";
            this.CreatedByIllnessAttachment.Width = 140;
            // 
            // CreateDateIllnessAttachment
            // 
            this.CreateDateIllnessAttachment.HeaderText = "Create Date";
            this.CreateDateIllnessAttachment.Name = "CreateDateIllnessAttachment";
            // 
            // frmIllnessCreationPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(877, 924);
            this.ControlBox = false;
            this.Controls.Add(this.grpAttachment);
            this.Controls.Add(this.comboCaseNoIllness);
            this.Controls.Add(this.comboIllnessProgram);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.txtYearlyLimitBalance);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.rbIneligible);
            this.Controls.Add(this.rbEligible);
            this.Controls.Add(this.txtIllnessNo);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtLimitedSharingYearlyLimit);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.comboLimitedSharing);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.dtpDateOfDiagnosis);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtDiseaseName);
            this.Controls.Add(this.grpIllnessNote);
            this.Controls.Add(this.dtpCreateDate);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSaveIllness);
            this.Controls.Add(this.txtICD10Code);
            this.Controls.Add(this.txtIndividualNo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmIllnessCreationPage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmIllnessCreationPage";
            this.Load += new System.EventHandler(this.frmIllnessCreationPage_Load);
            this.grpIllnessNote.ResumeLayout(false);
            this.grpIllnessNote.PerformLayout();
            this.grpAttachment.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvIllnessAttachment)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpIllnessNote;
        //private System.Windows.Forms.TextBox txtConclusion;
        public System.Windows.Forms.TextBox txtConclusion;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.TextBox txtIllnessNote;
        public System.Windows.Forms.DateTimePicker dtpCreateDate;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSaveIllness;
        public System.Windows.Forms.TextBox txtICD10Code;
        //private System.Windows.Forms.TextBox txtIndividualNo;
        public System.Windows.Forms.TextBox txtIndividualNo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtDiseaseName;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.DateTimePicker dtpDateOfDiagnosis;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox comboLimitedSharing;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtLimitedSharingYearlyLimit;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtIllnessNo;
        private System.Windows.Forms.RadioButton rbEligible;
        private System.Windows.Forms.RadioButton rbIneligible;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtYearlyLimitBalance;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox comboIllnessProgram;
        private System.Windows.Forms.ComboBox comboCaseNoIllness;
        private System.Windows.Forms.GroupBox grpAttachment;
        private System.Windows.Forms.Button btnDeleteAttachment;
        private System.Windows.Forms.Button btnAddAttachment;
        private System.Windows.Forms.DataGridView gvIllnessAttachment;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SelectedIllnessAttachment;
        private System.Windows.Forms.DataGridViewTextBoxColumn IllnessAttachmentNo;
        private System.Windows.Forms.DataGridViewButtonColumn UploadIllnessAttachment;
        private System.Windows.Forms.DataGridViewTextBoxColumn IllnessAttachmentFileName;
        private System.Windows.Forms.DataGridViewButtonColumn IllnessAttachmentView;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreatedByIllnessAttachment;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreateDateIllnessAttachment;
    }
}