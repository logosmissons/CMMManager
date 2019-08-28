namespace CMMManager
{
    partial class frmMedicalProviderInfo
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
            this.gvNotesAboutMedicalProvider = new System.Windows.Forms.DataGridView();
            this.label83 = new System.Windows.Forms.Label();
            this.gvMedicalProviderDiscountHistory = new System.Windows.Forms.DataGridView();
            this.label77 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gvNotesAboutMedicalProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMedicalProviderDiscountHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // gvNotesAboutMedicalProvider
            // 
            this.gvNotesAboutMedicalProvider.AllowUserToAddRows = false;
            this.gvNotesAboutMedicalProvider.AllowUserToDeleteRows = false;
            this.gvNotesAboutMedicalProvider.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvNotesAboutMedicalProvider.Location = new System.Drawing.Point(26, 281);
            this.gvNotesAboutMedicalProvider.Name = "gvNotesAboutMedicalProvider";
            this.gvNotesAboutMedicalProvider.ReadOnly = true;
            this.gvNotesAboutMedicalProvider.Size = new System.Drawing.Size(686, 114);
            this.gvNotesAboutMedicalProvider.TabIndex = 134;
            // 
            // label83
            // 
            this.label83.AutoSize = true;
            this.label83.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label83.Location = new System.Drawing.Point(23, 262);
            this.label83.Name = "label83";
            this.label83.Size = new System.Drawing.Size(187, 16);
            this.label83.TabIndex = 133;
            this.label83.Text = "Notes About Medical Provider";
            // 
            // gvMedicalProviderDiscountHistory
            // 
            this.gvMedicalProviderDiscountHistory.AllowUserToAddRows = false;
            this.gvMedicalProviderDiscountHistory.AllowUserToDeleteRows = false;
            this.gvMedicalProviderDiscountHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvMedicalProviderDiscountHistory.Location = new System.Drawing.Point(26, 42);
            this.gvMedicalProviderDiscountHistory.Name = "gvMedicalProviderDiscountHistory";
            this.gvMedicalProviderDiscountHistory.ReadOnly = true;
            this.gvMedicalProviderDiscountHistory.Size = new System.Drawing.Size(686, 197);
            this.gvMedicalProviderDiscountHistory.TabIndex = 132;
            // 
            // label77
            // 
            this.label77.AutoSize = true;
            this.label77.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label77.Location = new System.Drawing.Point(23, 23);
            this.label77.Name = "label77";
            this.label77.Size = new System.Drawing.Size(210, 16);
            this.label77.TabIndex = 131;
            this.label77.Text = "Medical Provider Discount History";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(605, 426);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(107, 30);
            this.btnOk.TabIndex = 135;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // frmMedicalProviderInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(742, 484);
            this.ControlBox = false;
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.gvNotesAboutMedicalProvider);
            this.Controls.Add(this.label83);
            this.Controls.Add(this.gvMedicalProviderDiscountHistory);
            this.Controls.Add(this.label77);
            this.Name = "frmMedicalProviderInfo";
            this.Text = "Medical Provider Info";
            ((System.ComponentModel.ISupportInitialize)(this.gvNotesAboutMedicalProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMedicalProviderDiscountHistory)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gvNotesAboutMedicalProvider;
        private System.Windows.Forms.Label label83;
        private System.Windows.Forms.DataGridView gvMedicalProviderDiscountHistory;
        private System.Windows.Forms.Label label77;
        private System.Windows.Forms.Button btnOk;
    }
}