namespace CMMManager
{
    partial class frmMedBillsForCase
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
            this.gvMedBillsForCase = new System.Windows.Forms.DataGridView();
            this.Selected = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.BillNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BillDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BillType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IllnessId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BillAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SettlementTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalSharedAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BillStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCaseNo = new System.Windows.Forms.TextBox();
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gvMedBillsForCase)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(26, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(246, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Medical Bills for Case";
            // 
            // gvMedBillsForCase
            // 
            this.gvMedBillsForCase.AllowUserToAddRows = false;
            this.gvMedBillsForCase.AllowUserToDeleteRows = false;
            this.gvMedBillsForCase.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvMedBillsForCase.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Selected,
            this.BillNo,
            this.BillDate,
            this.BillType,
            this.IllnessId,
            this.BillAmount,
            this.SettlementTotal,
            this.TotalSharedAmount,
            this.BillStatus});
            this.gvMedBillsForCase.Location = new System.Drawing.Point(31, 127);
            this.gvMedBillsForCase.Name = "gvMedBillsForCase";
            this.gvMedBillsForCase.ReadOnly = true;
            this.gvMedBillsForCase.Size = new System.Drawing.Size(963, 408);
            this.gvMedBillsForCase.TabIndex = 1;
            this.gvMedBillsForCase.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvMedBillsForCase_CellContentDoubleClick);
            // 
            // Selected
            // 
            this.Selected.HeaderText = "Selected";
            this.Selected.Name = "Selected";
            this.Selected.ReadOnly = true;
            this.Selected.Width = 80;
            // 
            // BillNo
            // 
            this.BillNo.HeaderText = "BillNo";
            this.BillNo.Name = "BillNo";
            this.BillNo.ReadOnly = true;
            // 
            // BillDate
            // 
            this.BillDate.HeaderText = "Bill Date";
            this.BillDate.Name = "BillDate";
            this.BillDate.ReadOnly = true;
            // 
            // BillType
            // 
            this.BillType.HeaderText = "Bill Type";
            this.BillType.Name = "BillType";
            this.BillType.ReadOnly = true;
            // 
            // IllnessId
            // 
            this.IllnessId.HeaderText = "Illness Id";
            this.IllnessId.Name = "IllnessId";
            this.IllnessId.ReadOnly = true;
            // 
            // BillAmount
            // 
            this.BillAmount.HeaderText = "Bill Amount";
            this.BillAmount.Name = "BillAmount";
            this.BillAmount.ReadOnly = true;
            // 
            // SettlementTotal
            // 
            this.SettlementTotal.HeaderText = "Settlement Total";
            this.SettlementTotal.Name = "SettlementTotal";
            this.SettlementTotal.ReadOnly = true;
            // 
            // TotalSharedAmount
            // 
            this.TotalSharedAmount.HeaderText = "Total Shared Amount";
            this.TotalSharedAmount.Name = "TotalSharedAmount";
            this.TotalSharedAmount.ReadOnly = true;
            // 
            // BillStatus
            // 
            this.BillStatus.HeaderText = "Bill Status";
            this.BillStatus.Name = "BillStatus";
            this.BillStatus.ReadOnly = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(31, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Case No";
            // 
            // txtCaseNo
            // 
            this.txtCaseNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCaseNo.Location = new System.Drawing.Point(98, 83);
            this.txtCaseNo.Name = "txtCaseNo";
            this.txtCaseNo.ReadOnly = true;
            this.txtCaseNo.Size = new System.Drawing.Size(100, 22);
            this.txtCaseNo.TabIndex = 3;
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(632, 79);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(104, 30);
            this.btnSelect.TabIndex = 4;
            this.btnSelect.Text = "Select";
            this.btnSelect.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(761, 79);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(104, 30);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(890, 79);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(104, 30);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmMedBillsForCase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1032, 563);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.txtCaseNo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.gvMedBillsForCase);
            this.Controls.Add(this.label1);
            this.Name = "frmMedBillsForCase";
            this.Text = "frmMedBillsForCase";
            this.Load += new System.EventHandler(this.frmMedBillsForCase_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvMedBillsForCase)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView gvMedBillsForCase;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Selected;
        private System.Windows.Forms.DataGridViewTextBoxColumn BillNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn BillDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn BillType;
        private System.Windows.Forms.DataGridViewTextBoxColumn IllnessId;
        private System.Windows.Forms.DataGridViewTextBoxColumn BillAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn SettlementTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalSharedAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn BillStatus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCaseNo;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClose;
    }
}