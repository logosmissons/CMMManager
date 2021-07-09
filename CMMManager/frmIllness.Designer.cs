namespace CMMManager
{
    partial class frmIllness
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
            this.gvIllness = new System.Windows.Forms.DataGridView();
            this.btnAddNew = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnCancel = new System.Windows.Forms.Button();
            this.Selected = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Illness_No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Individual_Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ICD10_Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IllnessICD10Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IllnessNote = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreateDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gvIllness)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(22, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Illness";
            // 
            // gvIllness
            // 
            this.gvIllness.AllowUserToAddRows = false;
            this.gvIllness.AllowUserToDeleteRows = false;
            this.gvIllness.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvIllness.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Selected,
            this.Illness_No,
            this.Individual_Id,
            this.ICD10_Code,
            this.IllnessICD10Description,
            this.IllnessNote,
            this.CreateDate});
            this.gvIllness.Location = new System.Drawing.Point(27, 77);
            this.gvIllness.MultiSelect = false;
            this.gvIllness.Name = "gvIllness";
            this.gvIllness.ReadOnly = true;
            this.gvIllness.Size = new System.Drawing.Size(902, 382);
            this.gvIllness.TabIndex = 1;
            this.gvIllness.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvIllness_CellContentClick);
            this.gvIllness.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gvIllness_ColumnHeaderMouseClick);
            // 
            // btnAddNew
            // 
            this.btnAddNew.Location = new System.Drawing.Point(372, 478);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(93, 31);
            this.btnAddNew.TabIndex = 2;
            this.btnAddNew.Text = "Add New";
            this.btnAddNew.UseVisualStyleBackColor = true;
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(604, 478);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(93, 31);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(488, 478);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(93, 31);
            this.btnEdit.TabIndex = 5;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(256, 478);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(93, 31);
            this.btnSelect.TabIndex = 6;
            this.btnSelect.Text = "Select";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(720, 478);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(93, 31);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Illness Id";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 80;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Individual Id";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "ICD 10 Code";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Create Date";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Illness Note";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 250;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(836, 478);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(93, 31);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // Selected
            // 
            this.Selected.HeaderText = "Select";
            this.Selected.Name = "Selected";
            this.Selected.ReadOnly = true;
            this.Selected.Width = 60;
            // 
            // Illness_No
            // 
            this.Illness_No.HeaderText = "Illness No";
            this.Illness_No.Name = "Illness_No";
            this.Illness_No.ReadOnly = true;
            this.Illness_No.Width = 80;
            // 
            // Individual_Id
            // 
            this.Individual_Id.HeaderText = "Individual Id";
            this.Individual_Id.Name = "Individual_Id";
            this.Individual_Id.ReadOnly = true;
            // 
            // ICD10_Code
            // 
            this.ICD10_Code.HeaderText = "ICD 10 Code";
            this.ICD10_Code.Name = "ICD10_Code";
            this.ICD10_Code.ReadOnly = true;
            // 
            // IllnessICD10Description
            // 
            this.IllnessICD10Description.HeaderText = "ICD 10 Description";
            this.IllnessICD10Description.Name = "IllnessICD10Description";
            this.IllnessICD10Description.ReadOnly = true;
            this.IllnessICD10Description.Width = 180;
            // 
            // IllnessNote
            // 
            this.IllnessNote.HeaderText = "Illness Note";
            this.IllnessNote.Name = "IllnessNote";
            this.IllnessNote.ReadOnly = true;
            this.IllnessNote.Width = 240;
            // 
            // CreateDate
            // 
            this.CreateDate.HeaderText = "Create Date";
            this.CreateDate.Name = "CreateDate";
            this.CreateDate.ReadOnly = true;
            this.CreateDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // frmIllness
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(951, 535);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnAddNew);
            this.Controls.Add(this.gvIllness);
            this.Controls.Add(this.label1);
            this.Name = "frmIllness";
            this.Text = "frmIllness";
            this.Load += new System.EventHandler(this.frmIllness_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvIllness)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView gvIllness;
        private System.Windows.Forms.Button btnAddNew;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Selected;
        private System.Windows.Forms.DataGridViewTextBoxColumn Illness_No;
        private System.Windows.Forms.DataGridViewTextBoxColumn Individual_Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn ICD10_Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn IllnessICD10Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn IllnessNote;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreateDate;
    }
}