namespace CMMManager
{
    partial class frmPRCalculator
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
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtIndividualId = new System.Windows.Forms.TextBox();
            this.txtIndividualName = new System.Windows.Forms.TextBox();
            this.txtIndividualStartDate = new System.Windows.Forms.TextBox();
            this.gvProgramChangeHistory = new System.Windows.Forms.DataGridView();
            this.ChangDateProgramChange = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OldProgramProgramChange = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NewProgramProgramChange = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gvPersonalResponsibilityReport = new System.Windows.Forms.DataGridView();
            this.CaseNoPRCalc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IllnessNoPRCalc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IncidentNoPRCalc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MedBillNoPRCalc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SettlementNoPRCalc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PRAmountPRCalc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PRTypePRCalc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dtpAnivStartDate = new System.Windows.Forms.DateTimePicker();
            this.dtpAnivEndDate = new System.Windows.Forms.DateTimePicker();
            this.btnRunReport = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gvProgramChangeHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPersonalResponsibilityReport)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(25, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Individual Id:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(313, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Individual Name:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(25, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(130, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Individual Start Date:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(25, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(155, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "Program Change History";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(25, 361);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(194, 16);
            this.label5.TabIndex = 4;
            this.label5.Text = "Personal Responsibility Report";
            // 
            // txtIndividualId
            // 
            this.txtIndividualId.Location = new System.Drawing.Point(164, 24);
            this.txtIndividualId.Name = "txtIndividualId";
            this.txtIndividualId.ReadOnly = true;
            this.txtIndividualId.Size = new System.Drawing.Size(120, 20);
            this.txtIndividualId.TabIndex = 5;
            // 
            // txtIndividualName
            // 
            this.txtIndividualName.Location = new System.Drawing.Point(423, 24);
            this.txtIndividualName.Name = "txtIndividualName";
            this.txtIndividualName.ReadOnly = true;
            this.txtIndividualName.Size = new System.Drawing.Size(158, 20);
            this.txtIndividualName.TabIndex = 6;
            // 
            // txtIndividualStartDate
            // 
            this.txtIndividualStartDate.Location = new System.Drawing.Point(164, 60);
            this.txtIndividualStartDate.Name = "txtIndividualStartDate";
            this.txtIndividualStartDate.ReadOnly = true;
            this.txtIndividualStartDate.Size = new System.Drawing.Size(120, 20);
            this.txtIndividualStartDate.TabIndex = 7;
            // 
            // gvProgramChangeHistory
            // 
            this.gvProgramChangeHistory.AllowUserToAddRows = false;
            this.gvProgramChangeHistory.AllowUserToDeleteRows = false;
            this.gvProgramChangeHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvProgramChangeHistory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ChangDateProgramChange,
            this.OldProgramProgramChange,
            this.NewProgramProgramChange});
            this.gvProgramChangeHistory.Location = new System.Drawing.Point(25, 128);
            this.gvProgramChangeHistory.Name = "gvProgramChangeHistory";
            this.gvProgramChangeHistory.ReadOnly = true;
            this.gvProgramChangeHistory.Size = new System.Drawing.Size(765, 150);
            this.gvProgramChangeHistory.TabIndex = 8;
            // 
            // ChangDateProgramChange
            // 
            this.ChangDateProgramChange.HeaderText = "Change Date";
            this.ChangDateProgramChange.Name = "ChangDateProgramChange";
            this.ChangDateProgramChange.ReadOnly = true;
            // 
            // OldProgramProgramChange
            // 
            this.OldProgramProgramChange.HeaderText = "Old Program";
            this.OldProgramProgramChange.Name = "OldProgramProgramChange";
            this.OldProgramProgramChange.ReadOnly = true;
            this.OldProgramProgramChange.Width = 120;
            // 
            // NewProgramProgramChange
            // 
            this.NewProgramProgramChange.HeaderText = "New Program";
            this.NewProgramProgramChange.Name = "NewProgramProgramChange";
            this.NewProgramProgramChange.ReadOnly = true;
            this.NewProgramProgramChange.Width = 120;
            // 
            // gvPersonalResponsibilityReport
            // 
            this.gvPersonalResponsibilityReport.AllowUserToAddRows = false;
            this.gvPersonalResponsibilityReport.AllowUserToDeleteRows = false;
            this.gvPersonalResponsibilityReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvPersonalResponsibilityReport.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CaseNoPRCalc,
            this.IllnessNoPRCalc,
            this.IncidentNoPRCalc,
            this.MedBillNoPRCalc,
            this.SettlementNoPRCalc,
            this.PRAmountPRCalc,
            this.PRTypePRCalc});
            this.gvPersonalResponsibilityReport.Location = new System.Drawing.Point(25, 381);
            this.gvPersonalResponsibilityReport.Name = "gvPersonalResponsibilityReport";
            this.gvPersonalResponsibilityReport.ReadOnly = true;
            this.gvPersonalResponsibilityReport.Size = new System.Drawing.Size(765, 202);
            this.gvPersonalResponsibilityReport.TabIndex = 9;
            // 
            // CaseNoPRCalc
            // 
            this.CaseNoPRCalc.HeaderText = "Case No";
            this.CaseNoPRCalc.Name = "CaseNoPRCalc";
            this.CaseNoPRCalc.ReadOnly = true;
            this.CaseNoPRCalc.Width = 80;
            // 
            // IllnessNoPRCalc
            // 
            this.IllnessNoPRCalc.HeaderText = "Illness No";
            this.IllnessNoPRCalc.Name = "IllnessNoPRCalc";
            this.IllnessNoPRCalc.ReadOnly = true;
            // 
            // IncidentNoPRCalc
            // 
            this.IncidentNoPRCalc.HeaderText = "Incident No";
            this.IncidentNoPRCalc.Name = "IncidentNoPRCalc";
            this.IncidentNoPRCalc.ReadOnly = true;
            // 
            // MedBillNoPRCalc
            // 
            this.MedBillNoPRCalc.HeaderText = "Med Bill No";
            this.MedBillNoPRCalc.Name = "MedBillNoPRCalc";
            this.MedBillNoPRCalc.ReadOnly = true;
            // 
            // SettlementNoPRCalc
            // 
            this.SettlementNoPRCalc.HeaderText = "Settlement No";
            this.SettlementNoPRCalc.Name = "SettlementNoPRCalc";
            this.SettlementNoPRCalc.ReadOnly = true;
            this.SettlementNoPRCalc.Width = 120;
            // 
            // PRAmountPRCalc
            // 
            this.PRAmountPRCalc.HeaderText = "PR Amount";
            this.PRAmountPRCalc.Name = "PRAmountPRCalc";
            this.PRAmountPRCalc.ReadOnly = true;
            // 
            // PRTypePRCalc
            // 
            this.PRTypePRCalc.HeaderText = "PR Type";
            this.PRTypePRCalc.Name = "PRTypePRCalc";
            this.PRTypePRCalc.ReadOnly = true;
            this.PRTypePRCalc.Width = 120;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(25, 320);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(99, 16);
            this.label6.TabIndex = 11;
            this.label6.Text = "Aniv Start Date:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(249, 320);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(96, 16);
            this.label7.TabIndex = 12;
            this.label7.Text = "Aniv End Date:";
            // 
            // dtpAnivStartDate
            // 
            this.dtpAnivStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpAnivStartDate.Location = new System.Drawing.Point(127, 318);
            this.dtpAnivStartDate.Name = "dtpAnivStartDate";
            this.dtpAnivStartDate.Size = new System.Drawing.Size(107, 20);
            this.dtpAnivStartDate.TabIndex = 13;
            // 
            // dtpAnivEndDate
            // 
            this.dtpAnivEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpAnivEndDate.Location = new System.Drawing.Point(348, 318);
            this.dtpAnivEndDate.Name = "dtpAnivEndDate";
            this.dtpAnivEndDate.Size = new System.Drawing.Size(107, 20);
            this.dtpAnivEndDate.TabIndex = 14;
            // 
            // btnRunReport
            // 
            this.btnRunReport.Location = new System.Drawing.Point(489, 316);
            this.btnRunReport.Name = "btnRunReport";
            this.btnRunReport.Size = new System.Drawing.Size(124, 24);
            this.btnRunReport.TabIndex = 15;
            this.btnRunReport.Text = "Run Report";
            this.btnRunReport.UseVisualStyleBackColor = true;
            this.btnRunReport.Click += new System.EventHandler(this.btnRunReport_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(631, 316);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(124, 24);
            this.btnClose.TabIndex = 16;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmPRCalculator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(818, 609);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnRunReport);
            this.Controls.Add(this.dtpAnivEndDate);
            this.Controls.Add(this.dtpAnivStartDate);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.gvPersonalResponsibilityReport);
            this.Controls.Add(this.gvProgramChangeHistory);
            this.Controls.Add(this.txtIndividualStartDate);
            this.Controls.Add(this.txtIndividualName);
            this.Controls.Add(this.txtIndividualId);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmPRCalculator";
            this.Text = "Personal Responsibility Calculator";
            this.Load += new System.EventHandler(this.frmPRCalculator_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvProgramChangeHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPersonalResponsibilityReport)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtIndividualId;
        private System.Windows.Forms.TextBox txtIndividualName;
        private System.Windows.Forms.TextBox txtIndividualStartDate;
        private System.Windows.Forms.DataGridView gvProgramChangeHistory;
        private System.Windows.Forms.DataGridView gvPersonalResponsibilityReport;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtpAnivStartDate;
        private System.Windows.Forms.DateTimePicker dtpAnivEndDate;
        private System.Windows.Forms.Button btnRunReport;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChangDateProgramChange;
        private System.Windows.Forms.DataGridViewTextBoxColumn OldProgramProgramChange;
        private System.Windows.Forms.DataGridViewTextBoxColumn NewProgramProgramChange;
        private System.Windows.Forms.DataGridViewTextBoxColumn CaseNoPRCalc;
        private System.Windows.Forms.DataGridViewTextBoxColumn IllnessNoPRCalc;
        private System.Windows.Forms.DataGridViewTextBoxColumn IncidentNoPRCalc;
        private System.Windows.Forms.DataGridViewTextBoxColumn MedBillNoPRCalc;
        private System.Windows.Forms.DataGridViewTextBoxColumn SettlementNoPRCalc;
        private System.Windows.Forms.DataGridViewTextBoxColumn PRAmountPRCalc;
        private System.Windows.Forms.DataGridViewTextBoxColumn PRTypePRCalc;
    }
}