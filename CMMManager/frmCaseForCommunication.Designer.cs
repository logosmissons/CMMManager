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
            this.chkClosed = new System.Windows.Forms.CheckBox();
            this.chkProcessing = new System.Windows.Forms.CheckBox();
            this.chkOnGoing = new System.Windows.Forms.CheckBox();
            this.comboCases = new System.Windows.Forms.ComboBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboIllnessNo = new System.Windows.Forms.ComboBox();
            this.comboIncidentNo = new System.Windows.Forms.ComboBox();
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
            this.grpCaseStatus.Size = new System.Drawing.Size(285, 70);
            this.grpCaseStatus.TabIndex = 2;
            this.grpCaseStatus.TabStop = false;
            this.grpCaseStatus.Text = "Select Case Status";
            // 
            // chkClosed
            // 
            this.chkClosed.AutoSize = true;
            this.chkClosed.Location = new System.Drawing.Point(194, 29);
            this.chkClosed.Name = "chkClosed";
            this.chkClosed.Size = new System.Drawing.Size(58, 17);
            this.chkClosed.TabIndex = 2;
            this.chkClosed.Text = "Closed";
            this.chkClosed.UseVisualStyleBackColor = true;
            this.chkClosed.CheckedChanged += new System.EventHandler(this.chkClosed_CheckedChanged);
            // 
            // chkProcessing
            // 
            this.chkProcessing.AutoSize = true;
            this.chkProcessing.Location = new System.Drawing.Point(107, 29);
            this.chkProcessing.Name = "chkProcessing";
            this.chkProcessing.Size = new System.Drawing.Size(78, 17);
            this.chkProcessing.TabIndex = 1;
            this.chkProcessing.Text = "Processing";
            this.chkProcessing.UseVisualStyleBackColor = true;
            this.chkProcessing.CheckedChanged += new System.EventHandler(this.chkProcessing_CheckedChanged);
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
            // comboCases
            // 
            this.comboCases.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboCases.FormattingEnabled = true;
            this.comboCases.Location = new System.Drawing.Point(133, 119);
            this.comboCases.Name = "comboCases";
            this.comboCases.Size = new System.Drawing.Size(170, 24);
            this.comboCases.TabIndex = 3;
            this.comboCases.SelectedIndexChanged += new System.EventHandler(this.comboCases_SelectedIndexChanged);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(54, 253);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(104, 31);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(184, 253);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(104, 31);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(48, 124);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Select Case:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(48, 163);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Select Illness:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(48, 202);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Select Incident:";
            // 
            // comboIllnessNo
            // 
            this.comboIllnessNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboIllnessNo.FormattingEnabled = true;
            this.comboIllnessNo.Location = new System.Drawing.Point(134, 158);
            this.comboIllnessNo.Name = "comboIllnessNo";
            this.comboIllnessNo.Size = new System.Drawing.Size(169, 24);
            this.comboIllnessNo.TabIndex = 9;
            this.comboIllnessNo.SelectedIndexChanged += new System.EventHandler(this.comboIllnessNo_SelectedIndexChanged);
            // 
            // comboIncidentNo
            // 
            this.comboIncidentNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboIncidentNo.FormattingEnabled = true;
            this.comboIncidentNo.Location = new System.Drawing.Point(134, 197);
            this.comboIncidentNo.Name = "comboIncidentNo";
            this.comboIncidentNo.Size = new System.Drawing.Size(169, 24);
            this.comboIncidentNo.TabIndex = 10;
            // 
            // frmCaseForCommunication
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(353, 314);
            this.ControlBox = false;
            this.Controls.Add(this.comboIncidentNo);
            this.Controls.Add(this.comboIllnessNo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.comboCases);
            this.Controls.Add(this.grpCaseStatus);
            this.Name = "frmCaseForCommunication";
            this.Text = "Case for Communication";
            this.Load += new System.EventHandler(this.frmCaseForCommunication_Load);
            this.grpCaseStatus.ResumeLayout(false);
            this.grpCaseStatus.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpCaseStatus;
        private System.Windows.Forms.CheckBox chkClosed;
        private System.Windows.Forms.CheckBox chkProcessing;
        private System.Windows.Forms.CheckBox chkOnGoing;
        private System.Windows.Forms.ComboBox comboCases;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboIllnessNo;
        private System.Windows.Forms.ComboBox comboIncidentNo;
    }
}