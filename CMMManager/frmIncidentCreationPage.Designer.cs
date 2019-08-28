namespace CMMManager
{
    partial class frmIncidentCreationPage
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
            this.btnCloseIncident = new System.Windows.Forms.Button();
            this.btnSaveIncident = new System.Windows.Forms.Button();
            this.txtIncidentNote = new System.Windows.Forms.TextBox();
            this.txtICD10Code = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtICD10Name = new System.Windows.Forms.TextBox();
            this.dtpCreateDate = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCaseNo = new System.Windows.Forms.TextBox();
            this.txtIllnessNo = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.comboProgram = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpModifiedDate = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.txtIncidentNo = new System.Windows.Forms.TextBox();
            this.chkWellBeing = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.dtpIncdOccurrenceDate = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // btnCloseIncident
            // 
            this.btnCloseIncident.Location = new System.Drawing.Point(427, 564);
            this.btnCloseIncident.Name = "btnCloseIncident";
            this.btnCloseIncident.Size = new System.Drawing.Size(97, 31);
            this.btnCloseIncident.TabIndex = 29;
            this.btnCloseIncident.Text = "Close";
            this.btnCloseIncident.UseVisualStyleBackColor = true;
            this.btnCloseIncident.Click += new System.EventHandler(this.btnCloseIncident_Click);
            // 
            // btnSaveIncident
            // 
            this.btnSaveIncident.Location = new System.Drawing.Point(306, 564);
            this.btnSaveIncident.Name = "btnSaveIncident";
            this.btnSaveIncident.Size = new System.Drawing.Size(97, 31);
            this.btnSaveIncident.TabIndex = 28;
            this.btnSaveIncident.Text = "Save";
            this.btnSaveIncident.UseVisualStyleBackColor = true;
            this.btnSaveIncident.Click += new System.EventHandler(this.btnSaveIncident_Click);
            // 
            // txtIncidentNote
            // 
            this.txtIncidentNote.Location = new System.Drawing.Point(40, 331);
            this.txtIncidentNote.Multiline = true;
            this.txtIncidentNote.Name = "txtIncidentNote";
            this.txtIncidentNote.Size = new System.Drawing.Size(484, 211);
            this.txtIncidentNote.TabIndex = 27;
            // 
            // txtICD10Code
            // 
            this.txtICD10Code.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtICD10Code.Location = new System.Drawing.Point(152, 166);
            this.txtICD10Code.Name = "txtICD10Code";
            this.txtICD10Code.ReadOnly = true;
            this.txtICD10Code.Size = new System.Drawing.Size(103, 22);
            this.txtICD10Code.TabIndex = 24;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(37, 312);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 16);
            this.label4.TabIndex = 22;
            this.label4.Text = "Incident Note:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(37, 270);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 16);
            this.label3.TabIndex = 21;
            this.label3.Text = "Create Date:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(37, 168);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 16);
            this.label2.TabIndex = 20;
            this.label2.Text = "ICD10 Code:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(228, 41);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(105, 29);
            this.label5.TabIndex = 18;
            this.label5.Text = "Incident";
            // 
            // txtICD10Name
            // 
            this.txtICD10Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtICD10Name.Location = new System.Drawing.Point(264, 166);
            this.txtICD10Name.Name = "txtICD10Name";
            this.txtICD10Name.ReadOnly = true;
            this.txtICD10Name.Size = new System.Drawing.Size(260, 22);
            this.txtICD10Name.TabIndex = 31;
            // 
            // dtpCreateDate
            // 
            this.dtpCreateDate.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpCreateDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpCreateDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpCreateDate.Location = new System.Drawing.Point(152, 268);
            this.dtpCreateDate.Name = "dtpCreateDate";
            this.dtpCreateDate.Size = new System.Drawing.Size(128, 22);
            this.dtpCreateDate.TabIndex = 32;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(37, 100);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 16);
            this.label6.TabIndex = 34;
            this.label6.Text = "Case No:";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(309, 100);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 16);
            this.label7.TabIndex = 36;
            this.label7.Text = "Illness No:";
            // 
            // txtCaseNo
            // 
            this.txtCaseNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCaseNo.Location = new System.Drawing.Point(152, 98);
            this.txtCaseNo.Name = "txtCaseNo";
            this.txtCaseNo.ReadOnly = true;
            this.txtCaseNo.Size = new System.Drawing.Size(141, 22);
            this.txtCaseNo.TabIndex = 39;
            // 
            // txtIllnessNo
            // 
            this.txtIllnessNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIllnessNo.Location = new System.Drawing.Point(393, 98);
            this.txtIllnessNo.Name = "txtIllnessNo";
            this.txtIllnessNo.ReadOnly = true;
            this.txtIllnessNo.Size = new System.Drawing.Size(130, 22);
            this.txtIllnessNo.TabIndex = 40;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(37, 202);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(63, 16);
            this.label8.TabIndex = 42;
            this.label8.Text = "Program:";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // comboProgram
            // 
            this.comboProgram.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboProgram.FormattingEnabled = true;
            this.comboProgram.Location = new System.Drawing.Point(152, 199);
            this.comboProgram.Name = "comboProgram";
            this.comboProgram.Size = new System.Drawing.Size(128, 24);
            this.comboProgram.TabIndex = 43;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(284, 270);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 16);
            this.label1.TabIndex = 44;
            this.label1.Text = "Last Modified Date:";
            // 
            // dtpModifiedDate
            // 
            this.dtpModifiedDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpModifiedDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpModifiedDate.Location = new System.Drawing.Point(410, 268);
            this.dtpModifiedDate.Name = "dtpModifiedDate";
            this.dtpModifiedDate.Size = new System.Drawing.Size(114, 22);
            this.dtpModifiedDate.TabIndex = 45;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(37, 134);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(78, 16);
            this.label9.TabIndex = 46;
            this.label9.Text = "Incident No:";
            this.label9.Click += new System.EventHandler(this.label9_Click);
            // 
            // txtIncidentNo
            // 
            this.txtIncidentNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIncidentNo.Location = new System.Drawing.Point(152, 132);
            this.txtIncidentNo.Name = "txtIncidentNo";
            this.txtIncidentNo.ReadOnly = true;
            this.txtIncidentNo.Size = new System.Drawing.Size(141, 22);
            this.txtIncidentNo.TabIndex = 47;
            // 
            // chkWellBeing
            // 
            this.chkWellBeing.AutoSize = true;
            this.chkWellBeing.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkWellBeing.Location = new System.Drawing.Point(301, 200);
            this.chkWellBeing.Name = "chkWellBeing";
            this.chkWellBeing.Size = new System.Drawing.Size(92, 20);
            this.chkWellBeing.TabIndex = 48;
            this.chkWellBeing.Text = "Well Being";
            this.chkWellBeing.UseVisualStyleBackColor = true;
            this.chkWellBeing.CheckedChanged += new System.EventHandler(this.chkWellBeing_CheckedChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(37, 236);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(112, 16);
            this.label10.TabIndex = 49;
            this.label10.Text = "Occurrence Date:";
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // dtpIncdOccurrenceDate
            // 
            this.dtpIncdOccurrenceDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpIncdOccurrenceDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpIncdOccurrenceDate.Location = new System.Drawing.Point(152, 234);
            this.dtpIncdOccurrenceDate.Name = "dtpIncdOccurrenceDate";
            this.dtpIncdOccurrenceDate.Size = new System.Drawing.Size(128, 22);
            this.dtpIncdOccurrenceDate.TabIndex = 50;
            // 
            // frmIncidentCreationPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(563, 607);
            this.ControlBox = false;
            this.Controls.Add(this.dtpIncdOccurrenceDate);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.chkWellBeing);
            this.Controls.Add(this.txtIncidentNo);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.dtpModifiedDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboProgram);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtIllnessNo);
            this.Controls.Add(this.txtCaseNo);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dtpCreateDate);
            this.Controls.Add(this.txtICD10Name);
            this.Controls.Add(this.btnCloseIncident);
            this.Controls.Add(this.btnSaveIncident);
            this.Controls.Add(this.txtIncidentNote);
            this.Controls.Add(this.txtICD10Code);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Name = "frmIncidentCreationPage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Incident Creation";
            this.Load += new System.EventHandler(this.frmIncidentCreationPage_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnCloseIncident;
        private System.Windows.Forms.Button btnSaveIncident;
        private System.Windows.Forms.TextBox txtIncidentNote;
        private System.Windows.Forms.TextBox txtICD10Code;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtICD10Name;
        private System.Windows.Forms.DateTimePicker dtpCreateDate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtCaseNo;
        private System.Windows.Forms.TextBox txtIllnessNo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboProgram;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpModifiedDate;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtIncidentNo;
        private System.Windows.Forms.CheckBox chkWellBeing;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DateTimePicker dtpIncdOccurrenceDate;
    }
}