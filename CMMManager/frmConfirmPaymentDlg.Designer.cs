namespace CMMManager
{
    partial class frmConfirmPaymentDlg
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
            this.btnYes = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtIndividualId = new System.Windows.Forms.TextBox();
            this.txtSettlementAmount = new System.Windows.Forms.TextBox();
            this.txtPaymentAmount = new System.Windows.Forms.TextBox();
            this.txtCreditCardNo = new System.Windows.Forms.TextBox();
            this.txtMedicalProviderName = new System.Windows.Forms.TextBox();
            this.txtIndividualName = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtSettlementNo = new System.Windows.Forms.TextBox();
            this.txtMedicalBillNo = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtMedicalBillAmount = new System.Windows.Forms.TextBox();
            this.txtSettlementNote = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(94, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(374, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Do you want to save the payment?";
            // 
            // btnYes
            // 
            this.btnYes.Location = new System.Drawing.Point(349, 446);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new System.Drawing.Size(104, 33);
            this.btnYes.TabIndex = 1;
            this.btnYes.Text = "Yes";
            this.btnYes.UseVisualStyleBackColor = true;
            this.btnYes.Click += new System.EventHandler(this.btnYes_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(474, 446);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(104, 33);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Individual ID:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 135);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Individual Name:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 168);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Medical Provider Name:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(322, 102);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Credit Card No:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(322, 168);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(99, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Settlement Amount:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(322, 201);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(90, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Payment Amount:";
            // 
            // txtIndividualId
            // 
            this.txtIndividualId.Location = new System.Drawing.Point(147, 99);
            this.txtIndividualId.Name = "txtIndividualId";
            this.txtIndividualId.ReadOnly = true;
            this.txtIndividualId.Size = new System.Drawing.Size(141, 20);
            this.txtIndividualId.TabIndex = 10;
            // 
            // txtSettlementAmount
            // 
            this.txtSettlementAmount.Location = new System.Drawing.Point(437, 165);
            this.txtSettlementAmount.Name = "txtSettlementAmount";
            this.txtSettlementAmount.ReadOnly = true;
            this.txtSettlementAmount.Size = new System.Drawing.Size(141, 20);
            this.txtSettlementAmount.TabIndex = 11;
            // 
            // txtPaymentAmount
            // 
            this.txtPaymentAmount.Location = new System.Drawing.Point(437, 198);
            this.txtPaymentAmount.Name = "txtPaymentAmount";
            this.txtPaymentAmount.ReadOnly = true;
            this.txtPaymentAmount.Size = new System.Drawing.Size(141, 20);
            this.txtPaymentAmount.TabIndex = 12;
            // 
            // txtCreditCardNo
            // 
            this.txtCreditCardNo.Location = new System.Drawing.Point(437, 99);
            this.txtCreditCardNo.Name = "txtCreditCardNo";
            this.txtCreditCardNo.ReadOnly = true;
            this.txtCreditCardNo.Size = new System.Drawing.Size(141, 20);
            this.txtCreditCardNo.TabIndex = 14;
            // 
            // txtMedicalProviderName
            // 
            this.txtMedicalProviderName.Location = new System.Drawing.Point(147, 165);
            this.txtMedicalProviderName.Name = "txtMedicalProviderName";
            this.txtMedicalProviderName.ReadOnly = true;
            this.txtMedicalProviderName.Size = new System.Drawing.Size(141, 20);
            this.txtMedicalProviderName.TabIndex = 15;
            // 
            // txtIndividualName
            // 
            this.txtIndividualName.Location = new System.Drawing.Point(147, 132);
            this.txtIndividualName.Name = "txtIndividualName";
            this.txtIndividualName.ReadOnly = true;
            this.txtIndividualName.Size = new System.Drawing.Size(141, 20);
            this.txtIndividualName.TabIndex = 16;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(25, 234);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "Settlement No:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(25, 201);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(80, 13);
            this.label9.TabIndex = 20;
            this.label9.Text = "Medical Bill No:";
            // 
            // txtSettlementNo
            // 
            this.txtSettlementNo.Location = new System.Drawing.Point(147, 231);
            this.txtSettlementNo.Name = "txtSettlementNo";
            this.txtSettlementNo.ReadOnly = true;
            this.txtSettlementNo.Size = new System.Drawing.Size(141, 20);
            this.txtSettlementNo.TabIndex = 21;
            // 
            // txtMedicalBillNo
            // 
            this.txtMedicalBillNo.Location = new System.Drawing.Point(147, 198);
            this.txtMedicalBillNo.Name = "txtMedicalBillNo";
            this.txtMedicalBillNo.ReadOnly = true;
            this.txtMedicalBillNo.Size = new System.Drawing.Size(141, 20);
            this.txtMedicalBillNo.TabIndex = 22;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(322, 135);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(102, 13);
            this.label10.TabIndex = 23;
            this.label10.Text = "Medical Bill Amount:";
            // 
            // txtMedicalBillAmount
            // 
            this.txtMedicalBillAmount.Location = new System.Drawing.Point(437, 132);
            this.txtMedicalBillAmount.Name = "txtMedicalBillAmount";
            this.txtMedicalBillAmount.ReadOnly = true;
            this.txtMedicalBillAmount.Size = new System.Drawing.Size(141, 20);
            this.txtMedicalBillAmount.TabIndex = 24;
            // 
            // txtSettlementNote
            // 
            this.txtSettlementNote.Location = new System.Drawing.Point(28, 306);
            this.txtSettlementNote.Multiline = true;
            this.txtSettlementNote.Name = "txtSettlementNote";
            this.txtSettlementNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSettlementNote.Size = new System.Drawing.Size(550, 118);
            this.txtSettlementNote.TabIndex = 25;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(25, 280);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(30, 13);
            this.label11.TabIndex = 26;
            this.label11.Text = "Note";
            // 
            // frmConfirmPaymentDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 506);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtSettlementNote);
            this.Controls.Add(this.txtMedicalBillAmount);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtMedicalBillNo);
            this.Controls.Add(this.txtSettlementNo);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtIndividualName);
            this.Controls.Add(this.txtMedicalProviderName);
            this.Controls.Add(this.txtCreditCardNo);
            this.Controls.Add(this.txtPaymentAmount);
            this.Controls.Add(this.txtSettlementAmount);
            this.Controls.Add(this.txtIndividualId);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnYes);
            this.Controls.Add(this.label1);
            this.Name = "frmConfirmPaymentDlg";
            this.Text = "Payment Confirmation";
            this.Load += new System.EventHandler(this.frmConfirmPaymentDlg_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnYes;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtIndividualId;
        private System.Windows.Forms.TextBox txtSettlementAmount;
        private System.Windows.Forms.TextBox txtPaymentAmount;
        private System.Windows.Forms.TextBox txtCreditCardNo;
        private System.Windows.Forms.TextBox txtMedicalProviderName;
        private System.Windows.Forms.TextBox txtIndividualName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtSettlementNo;
        private System.Windows.Forms.TextBox txtMedicalBillNo;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtMedicalBillAmount;
        private System.Windows.Forms.TextBox txtSettlementNote;
        private System.Windows.Forms.Label label11;
    }
}