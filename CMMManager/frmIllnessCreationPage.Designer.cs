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
            this.txtIntroduction = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
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
            this.txtCaseNo = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.comboLimitedSharing = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtLimitedSharingYearlyLimit = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtIllnessNo = new System.Windows.Forms.TextBox();
            this.grpIllnessNote.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpIllnessNote
            // 
            this.grpIllnessNote.Controls.Add(this.txtConclusion);
            this.grpIllnessNote.Controls.Add(this.label7);
            this.grpIllnessNote.Controls.Add(this.label6);
            this.grpIllnessNote.Controls.Add(this.txtIntroduction);
            this.grpIllnessNote.Controls.Add(this.label4);
            this.grpIllnessNote.Controls.Add(this.txtIllnessNote);
            this.grpIllnessNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpIllnessNote.Location = new System.Drawing.Point(32, 264);
            this.grpIllnessNote.Name = "grpIllnessNote";
            this.grpIllnessNote.Size = new System.Drawing.Size(714, 352);
            this.grpIllnessNote.TabIndex = 26;
            this.grpIllnessNote.TabStop = false;
            this.grpIllnessNote.Text = "Illness Note";
            // 
            // txtConclusion
            // 
            this.txtConclusion.Location = new System.Drawing.Point(24, 309);
            this.txtConclusion.Name = "txtConclusion";
            this.txtConclusion.Size = new System.Drawing.Size(314, 22);
            this.txtConclusion.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(23, 290);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 16);
            this.label7.TabIndex = 13;
            this.label7.Text = "Conclusion";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 88);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 16);
            this.label6.TabIndex = 12;
            this.label6.Text = "Body";
            // 
            // txtIntroduction
            // 
            this.txtIntroduction.Location = new System.Drawing.Point(24, 54);
            this.txtIntroduction.Name = "txtIntroduction";
            this.txtIntroduction.Size = new System.Drawing.Size(316, 22);
            this.txtIntroduction.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 16);
            this.label4.TabIndex = 10;
            this.label4.Text = "Introduction";
            // 
            // txtIllnessNote
            // 
            this.txtIllnessNote.Location = new System.Drawing.Point(24, 109);
            this.txtIllnessNote.Multiline = true;
            this.txtIllnessNote.Name = "txtIllnessNote";
            this.txtIllnessNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtIllnessNote.Size = new System.Drawing.Size(668, 169);
            this.txtIllnessNote.TabIndex = 9;
            // 
            // dtpCreateDate
            // 
            this.dtpCreateDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpCreateDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpCreateDate.Location = new System.Drawing.Point(389, 164);
            this.dtpCreateDate.Name = "dtpCreateDate";
            this.dtpCreateDate.Size = new System.Drawing.Size(109, 22);
            this.dtpCreateDate.TabIndex = 25;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(646, 641);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(99, 29);
            this.btnClose.TabIndex = 23;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSaveIllness
            // 
            this.btnSaveIllness.Location = new System.Drawing.Point(526, 641);
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
            this.txtICD10Code.Location = new System.Drawing.Point(147, 127);
            this.txtICD10Code.Name = "txtICD10Code";
            this.txtICD10Code.Size = new System.Drawing.Size(117, 22);
            this.txtICD10Code.TabIndex = 20;
            this.txtICD10Code.TextChanged += new System.EventHandler(this.txtICD10Code_TextChanged);
            // 
            // txtIndividualNo
            // 
            this.txtIndividualNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIndividualNo.Location = new System.Drawing.Point(389, 88);
            this.txtIndividualNo.Name = "txtIndividualNo";
            this.txtIndividualNo.ReadOnly = true;
            this.txtIndividualNo.Size = new System.Drawing.Size(173, 22);
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
            this.label3.Location = new System.Drawing.Point(281, 167);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 16);
            this.label3.TabIndex = 17;
            this.label3.Text = "Received Date:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(29, 129);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 16);
            this.label2.TabIndex = 16;
            this.label2.Text = "ICD10 Code:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(294, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 16);
            this.label1.TabIndex = 15;
            this.label1.Text = "Individual No:";
            // 
            // txtDiseaseName
            // 
            this.txtDiseaseName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiseaseName.Location = new System.Drawing.Point(270, 127);
            this.txtDiseaseName.Name = "txtDiseaseName";
            this.txtDiseaseName.Size = new System.Drawing.Size(476, 22);
            this.txtDiseaseName.TabIndex = 27;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(29, 167);
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
            this.dtpDateOfDiagnosis.Location = new System.Drawing.Point(147, 164);
            this.dtpDateOfDiagnosis.Name = "dtpDateOfDiagnosis";
            this.dtpDateOfDiagnosis.Size = new System.Drawing.Size(117, 22);
            this.dtpDateOfDiagnosis.TabIndex = 29;
            this.dtpDateOfDiagnosis.ValueChanged += new System.EventHandler(this.dtpDateOfDiagnosis_ValueChanged);
            this.dtpDateOfDiagnosis.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpDateOfDiagnosis_KeyDown);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(576, 91);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(64, 16);
            this.label9.TabIndex = 31;
            this.label9.Text = "Case No:";
            // 
            // txtCaseNo
            // 
            this.txtCaseNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCaseNo.Location = new System.Drawing.Point(646, 88);
            this.txtCaseNo.Name = "txtCaseNo";
            this.txtCaseNo.ReadOnly = true;
            this.txtCaseNo.Size = new System.Drawing.Size(100, 22);
            this.txtCaseNo.TabIndex = 32;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(29, 206);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(103, 16);
            this.label10.TabIndex = 33;
            this.label10.Text = "Limited Sharing:";
            // 
            // comboLimitedSharing
            // 
            this.comboLimitedSharing.FormattingEnabled = true;
            this.comboLimitedSharing.Location = new System.Drawing.Point(147, 204);
            this.comboLimitedSharing.Name = "comboLimitedSharing";
            this.comboLimitedSharing.Size = new System.Drawing.Size(117, 21);
            this.comboLimitedSharing.TabIndex = 34;
            this.comboLimitedSharing.SelectedIndexChanged += new System.EventHandler(this.comboLimitedSharing_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(296, 206);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(87, 16);
            this.label11.TabIndex = 35;
            this.label11.Text = "Yearly Limits:";
            // 
            // txtLimitedSharingYearlyLimit
            // 
            this.txtLimitedSharingYearlyLimit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLimitedSharingYearlyLimit.Location = new System.Drawing.Point(389, 203);
            this.txtLimitedSharingYearlyLimit.Name = "txtLimitedSharingYearlyLimit";
            this.txtLimitedSharingYearlyLimit.ReadOnly = true;
            this.txtLimitedSharingYearlyLimit.Size = new System.Drawing.Size(109, 22);
            this.txtLimitedSharingYearlyLimit.TabIndex = 36;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(29, 91);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(70, 16);
            this.label12.TabIndex = 37;
            this.label12.Text = "Illness No:";
            // 
            // txtIllnessNo
            // 
            this.txtIllnessNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIllnessNo.Location = new System.Drawing.Point(147, 88);
            this.txtIllnessNo.Name = "txtIllnessNo";
            this.txtIllnessNo.ReadOnly = true;
            this.txtIllnessNo.Size = new System.Drawing.Size(117, 22);
            this.txtIllnessNo.TabIndex = 38;
            // 
            // frmIllnessCreationPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(775, 693);
            this.Controls.Add(this.txtIllnessNo);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtLimitedSharingYearlyLimit);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.comboLimitedSharing);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtCaseNo);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpIllnessNote;
        //private System.Windows.Forms.TextBox txtConclusion;
        public System.Windows.Forms.TextBox txtConclusion;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        //private System.Windows.Forms.TextBox txtIntroduction;
        public System.Windows.Forms.TextBox txtIntroduction;
        private System.Windows.Forms.Label label4;
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
        private System.Windows.Forms.TextBox txtCaseNo;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox comboLimitedSharing;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtLimitedSharingYearlyLimit;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtIllnessNo;
    }
}