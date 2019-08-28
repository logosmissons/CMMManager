namespace CMMManager
{
    partial class frmIncidentEditPage
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnUpdateIncident = new System.Windows.Forms.Button();
            this.txtIncidentNote = new System.Windows.Forms.TextBox();
            this.txtDate = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtICD10Code = new System.Windows.Forms.TextBox();
            this.txtIllnessNo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(296, 356);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 41;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnUpdateIncident
            // 
            this.btnUpdateIncident.Location = new System.Drawing.Point(200, 356);
            this.btnUpdateIncident.Name = "btnUpdateIncident";
            this.btnUpdateIncident.Size = new System.Drawing.Size(75, 23);
            this.btnUpdateIncident.TabIndex = 40;
            this.btnUpdateIncident.Text = "Update";
            this.btnUpdateIncident.UseVisualStyleBackColor = true;
            // 
            // txtIncidentNote
            // 
            this.txtIncidentNote.Location = new System.Drawing.Point(40, 261);
            this.txtIncidentNote.Multiline = true;
            this.txtIncidentNote.Name = "txtIncidentNote";
            this.txtIncidentNote.Size = new System.Drawing.Size(333, 72);
            this.txtIncidentNote.TabIndex = 39;
            // 
            // txtDate
            // 
            this.txtDate.Location = new System.Drawing.Point(133, 194);
            this.txtDate.Name = "txtDate";
            this.txtDate.Size = new System.Drawing.Size(238, 20);
            this.txtDate.TabIndex = 38;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(270, 144);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(101, 23);
            this.btnSearch.TabIndex = 37;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            // 
            // txtICD10Code
            // 
            this.txtICD10Code.Location = new System.Drawing.Point(133, 146);
            this.txtICD10Code.Name = "txtICD10Code";
            this.txtICD10Code.Size = new System.Drawing.Size(131, 20);
            this.txtICD10Code.TabIndex = 36;
            // 
            // txtIllnessNo
            // 
            this.txtIllnessNo.Location = new System.Drawing.Point(133, 99);
            this.txtIllnessNo.Name = "txtIllnessNo";
            this.txtIllnessNo.ReadOnly = true;
            this.txtIllnessNo.Size = new System.Drawing.Size(238, 20);
            this.txtIllnessNo.TabIndex = 35;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(37, 241);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 16);
            this.label4.TabIndex = 34;
            this.label4.Text = "Incident Note:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(37, 194);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 16);
            this.label3.TabIndex = 33;
            this.label3.Text = "Date:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(37, 147);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 16);
            this.label2.TabIndex = 32;
            this.label2.Text = "ICD10 Code:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(37, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 16);
            this.label1.TabIndex = 31;
            this.label1.Text = "Illness No:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(71, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(226, 29);
            this.label5.TabIndex = 30;
            this.label5.Text = "Incident Edit Form";
            // 
            // frmIncidentEditPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 410);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnUpdateIncident);
            this.Controls.Add(this.txtIncidentNote);
            this.Controls.Add(this.txtDate);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtICD10Code);
            this.Controls.Add(this.txtIllnessNo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label5);
            this.Name = "frmIncidentEditPage";
            this.Text = "Incident Edit";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnUpdateIncident;
        private System.Windows.Forms.TextBox txtIncidentNote;
        private System.Windows.Forms.TextBox txtDate;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtICD10Code;
        private System.Windows.Forms.TextBox txtIllnessNo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
    }
}