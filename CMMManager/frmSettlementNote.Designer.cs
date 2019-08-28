namespace CMMManager
{
    partial class frmSettlementNote
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
            this.txtSettlementNo = new System.Windows.Forms.TextBox();
            this.txtSettlementNote = new System.Windows.Forms.TextBox();
            this.btnSaveSettlementNote = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Settlement No:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Note:";
            // 
            // txtSettlementNo
            // 
            this.txtSettlementNo.Location = new System.Drawing.Point(111, 25);
            this.txtSettlementNo.Name = "txtSettlementNo";
            this.txtSettlementNo.ReadOnly = true;
            this.txtSettlementNo.Size = new System.Drawing.Size(128, 20);
            this.txtSettlementNo.TabIndex = 2;
            // 
            // txtSettlementNote
            // 
            this.txtSettlementNote.Location = new System.Drawing.Point(31, 86);
            this.txtSettlementNote.Multiline = true;
            this.txtSettlementNote.Name = "txtSettlementNote";
            this.txtSettlementNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSettlementNote.Size = new System.Drawing.Size(398, 138);
            this.txtSettlementNote.TabIndex = 3;
            // 
            // btnSaveSettlementNote
            // 
            this.btnSaveSettlementNote.Location = new System.Drawing.Point(226, 247);
            this.btnSaveSettlementNote.Name = "btnSaveSettlementNote";
            this.btnSaveSettlementNote.Size = new System.Drawing.Size(91, 26);
            this.btnSaveSettlementNote.TabIndex = 4;
            this.btnSaveSettlementNote.Text = "Save";
            this.btnSaveSettlementNote.UseVisualStyleBackColor = true;
            this.btnSaveSettlementNote.Click += new System.EventHandler(this.btnSaveSettlementNote_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(339, 247);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(91, 26);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmSettlementNote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(469, 297);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSaveSettlementNote);
            this.Controls.Add(this.txtSettlementNote);
            this.Controls.Add(this.txtSettlementNo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmSettlementNote";
            this.Text = "Settlement Note";
            this.Load += new System.EventHandler(this.frmSettlementNote_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSettlementNo;
        private System.Windows.Forms.TextBox txtSettlementNote;
        private System.Windows.Forms.Button btnSaveSettlementNote;
        private System.Windows.Forms.Button btnCancel;
    }
}