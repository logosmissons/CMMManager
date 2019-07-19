namespace CMMManager
{
    partial class frmCaseForCommunication
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
            this.grpCaseStatus = new System.Windows.Forms.GroupBox();
            this.chkOnGoing = new System.Windows.Forms.CheckBox();
            this.chkProcessing = new System.Windows.Forms.CheckBox();
            this.chkClosed = new System.Windows.Forms.CheckBox();
            this.comboCases = new System.Windows.Forms.ComboBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.grpCaseStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpCaseStatus
            // 
            this.grpCaseStatus.Controls.Add(this.chkClosed);
            this.grpCaseStatus.Controls.Add(this.chkProcessing);
            this.grpCaseStatus.Controls.Add(this.chkOnGoing);
            this.grpCaseStatus.Location = new System.Drawing.Point(35, 26);
            this.grpCaseStatus.Name = "grpCaseStatus";
            this.grpCaseStatus.Size = new System.Drawing.Size(265, 70);
            this.grpCaseStatus.TabIndex = 2;
            this.grpCaseStatus.TabStop = false;
            this.grpCaseStatus.Text = "Select Case Status";
            // 
            // chkOnGoing
            // 
            this.chkOnGoing.AutoSize = true;
            this.chkOnGoing.Location = new System.Drawing.Point(21, 29);
            this.chkOnGoing.Name = "chkOnGoing";
            this.chkOnGoing.Size = new System.Drawing.Size(71, 17);
            this.chkOnGoing.TabIndex = 0;
            this.chkOnGoing.Text = "On Going";
            this.chkOnGoing.UseVisualStyleBackColor = true;
            this.chkOnGoing.CheckedChanged += new System.EventHandler(this.chkOnGoing_CheckedChanged);
            // 
            // chkProcessing
            // 
            this.chkProcessing.AutoSize = true;
            this.chkProcessing.Location = new System.Drawing.Point(108, 29);
            this.chkProcessing.Name = "chkProcessing";
            this.chkProcessing.Size = new System.Drawing.Size(78, 17);
            this.chkProcessing.TabIndex = 1;
            this.chkProcessing.Text = "Processing";
            this.chkProcessing.UseVisualStyleBackColor = true;
            this.chkProcessing.CheckedChanged += new System.EventHandler(this.chkProcessing_CheckedChanged);
            // 
            // chkClosed
            // 
            this.chkClosed.AutoSize = true;
            this.chkClosed.Location = new System.Drawing.Point(192, 29);
            this.chkClosed.Name = "chkClosed";
            this.chkClosed.Size = new System.Drawing.Size(58, 17);
            this.chkClosed.TabIndex = 2;
            this.chkClosed.Text = "Closed";
            this.chkClosed.UseVisualStyleBackColor = true;
            this.chkClosed.CheckedChanged += new System.EventHandler(this.chkClosed_CheckedChanged);
            // 
            // comboCases
            // 
            this.comboCases.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboCases.FormattingEnabled = true;
            this.comboCases.Location = new System.Drawing.Point(35, 118);
            this.comboCases.Name = "comboCases";
            this.comboCases.Size = new System.Drawing.Size(127, 24);
            this.comboCases.TabIndex = 3;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(184, 118);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(116, 24);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // frmCaseForCommunication
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 175);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.comboCases);
            this.Controls.Add(this.grpCaseStatus);
            this.Name = "frmCaseForCommunication";
            this.Text = "Select a Case";
            this.Load += new System.EventHandler(this.frmCaseForCommunication_Load);
            this.grpCaseStatus.ResumeLayout(false);
            this.grpCaseStatus.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpCaseStatus;
        private System.Windows.Forms.CheckBox chkClosed;
        private System.Windows.Forms.CheckBox chkProcessing;
        private System.Windows.Forms.CheckBox chkOnGoing;
        private System.Windows.Forms.ComboBox comboCases;
        private System.Windows.Forms.Button btnOK;
    }
}