namespace CMMManager
{
    partial class frmCommunicationHelper
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
            this.gvCommunicationList = new System.Windows.Forms.DataGridView();
            this.CommNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CommType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Subject = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Body = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Solution = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gvTaskList = new System.Windows.Forms.DataGridView();
            this.TaskIdTask = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SubjectTask = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CommentTask = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SolutionTask = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtCommunicationCreatedBy = new System.Windows.Forms.TextBox();
            this.txtCommunicationCreateDate = new System.Windows.Forms.TextBox();
            this.txtCommunicationBody = new System.Windows.Forms.TextBox();
            this.txtCommunicationSubject = new System.Windows.Forms.TextBox();
            this.txtCommunicationType = new System.Windows.Forms.TextBox();
            this.txtIndividualName = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.txtTaskCreatedBy = new System.Windows.Forms.TextBox();
            this.txtTaskCreateDate = new System.Windows.Forms.TextBox();
            this.txtTaskAssignedTo = new System.Windows.Forms.TextBox();
            this.txtTaskDueDate = new System.Windows.Forms.TextBox();
            this.txtTaskSubject = new System.Windows.Forms.TextBox();
            this.txtTaskComment = new System.Windows.Forms.TextBox();
            this.txtTaskSolution = new System.Windows.Forms.TextBox();
            this.txtTaskStatus = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.txtMembershipNo = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.comboCaseNo = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboIllnessNo = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.comboIncidentNo = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCommunicationSolution = new System.Windows.Forms.TextBox();
            this.btnSaveSolution = new System.Windows.Forms.Button();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label22 = new System.Windows.Forms.Label();
            this.txtCommunicationNo = new System.Windows.Forms.TextBox();
            this.txtDiseaseName = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.gvCommunicationList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTaskList)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 120);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(166, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Communication List";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(534, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Task List";
            // 
            // gvCommunicationList
            // 
            this.gvCommunicationList.AllowUserToAddRows = false;
            this.gvCommunicationList.AllowUserToDeleteRows = false;
            this.gvCommunicationList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvCommunicationList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CommNo,
            this.CommType,
            this.Subject,
            this.Body,
            this.Solution});
            this.gvCommunicationList.Location = new System.Drawing.Point(18, 143);
            this.gvCommunicationList.Name = "gvCommunicationList";
            this.gvCommunicationList.ReadOnly = true;
            this.gvCommunicationList.Size = new System.Drawing.Size(489, 149);
            this.gvCommunicationList.TabIndex = 2;
            this.gvCommunicationList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvCommunicationList_CellDoubleClick);
            // 
            // CommNo
            // 
            this.CommNo.HeaderText = "Comm No";
            this.CommNo.Name = "CommNo";
            this.CommNo.ReadOnly = true;
            this.CommNo.Width = 80;
            // 
            // CommType
            // 
            this.CommType.HeaderText = "Comm Type";
            this.CommType.Name = "CommType";
            this.CommType.ReadOnly = true;
            // 
            // Subject
            // 
            this.Subject.HeaderText = "Subject";
            this.Subject.Name = "Subject";
            this.Subject.ReadOnly = true;
            this.Subject.Width = 80;
            // 
            // Body
            // 
            this.Body.HeaderText = "Body";
            this.Body.Name = "Body";
            this.Body.ReadOnly = true;
            // 
            // Solution
            // 
            this.Solution.HeaderText = "Solution";
            this.Solution.Name = "Solution";
            this.Solution.ReadOnly = true;
            // 
            // gvTaskList
            // 
            this.gvTaskList.AllowUserToAddRows = false;
            this.gvTaskList.AllowUserToDeleteRows = false;
            this.gvTaskList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvTaskList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TaskIdTask,
            this.SubjectTask,
            this.CommentTask,
            this.SolutionTask});
            this.gvTaskList.Location = new System.Drawing.Point(536, 143);
            this.gvTaskList.Name = "gvTaskList";
            this.gvTaskList.ReadOnly = true;
            this.gvTaskList.Size = new System.Drawing.Size(489, 149);
            this.gvTaskList.TabIndex = 3;
            this.gvTaskList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvTaskList_CellDoubleClick);
            // 
            // TaskIdTask
            // 
            this.TaskIdTask.HeaderText = "Task Id";
            this.TaskIdTask.Name = "TaskIdTask";
            this.TaskIdTask.ReadOnly = true;
            this.TaskIdTask.Width = 75;
            // 
            // SubjectTask
            // 
            this.SubjectTask.HeaderText = "Subject";
            this.SubjectTask.Name = "SubjectTask";
            this.SubjectTask.ReadOnly = true;
            this.SubjectTask.Width = 90;
            // 
            // CommentTask
            // 
            this.CommentTask.HeaderText = "Comment";
            this.CommentTask.Name = "CommentTask";
            this.CommentTask.ReadOnly = true;
            // 
            // SolutionTask
            // 
            this.SolutionTask.HeaderText = "Solution";
            this.SolutionTask.Name = "SolutionTask";
            this.SolutionTask.ReadOnly = true;
            this.SolutionTask.Width = 120;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(23, 422);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 16);
            this.label5.TabIndex = 6;
            this.label5.Text = "Subject:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(15, 76);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 16);
            this.label6.TabIndex = 7;
            this.label6.Text = "Case No:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(261, 354);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 16);
            this.label7.TabIndex = 8;
            this.label7.Text = "Comm Type:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(15, 38);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(108, 16);
            this.label8.TabIndex = 9;
            this.label8.Text = "Individual Name:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(23, 388);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(78, 16);
            this.label11.TabIndex = 12;
            this.label11.Text = "Created By:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(261, 389);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(83, 16);
            this.label12.TabIndex = 13;
            this.label12.Text = "Create Date:";
            // 
            // txtCommunicationCreatedBy
            // 
            this.txtCommunicationCreatedBy.Location = new System.Drawing.Point(109, 387);
            this.txtCommunicationCreatedBy.Name = "txtCommunicationCreatedBy";
            this.txtCommunicationCreatedBy.ReadOnly = true;
            this.txtCommunicationCreatedBy.Size = new System.Drawing.Size(137, 20);
            this.txtCommunicationCreatedBy.TabIndex = 15;
            // 
            // txtCommunicationCreateDate
            // 
            this.txtCommunicationCreateDate.Location = new System.Drawing.Point(352, 387);
            this.txtCommunicationCreateDate.Name = "txtCommunicationCreateDate";
            this.txtCommunicationCreateDate.ReadOnly = true;
            this.txtCommunicationCreateDate.Size = new System.Drawing.Size(155, 20);
            this.txtCommunicationCreateDate.TabIndex = 16;
            // 
            // txtCommunicationBody
            // 
            this.txtCommunicationBody.Location = new System.Drawing.Point(28, 476);
            this.txtCommunicationBody.Multiline = true;
            this.txtCommunicationBody.Name = "txtCommunicationBody";
            this.txtCommunicationBody.ReadOnly = true;
            this.txtCommunicationBody.Size = new System.Drawing.Size(481, 93);
            this.txtCommunicationBody.TabIndex = 17;
            // 
            // txtCommunicationSubject
            // 
            this.txtCommunicationSubject.Location = new System.Drawing.Point(109, 421);
            this.txtCommunicationSubject.Name = "txtCommunicationSubject";
            this.txtCommunicationSubject.ReadOnly = true;
            this.txtCommunicationSubject.Size = new System.Drawing.Size(398, 20);
            this.txtCommunicationSubject.TabIndex = 18;
            // 
            // txtCommunicationType
            // 
            this.txtCommunicationType.Location = new System.Drawing.Point(352, 353);
            this.txtCommunicationType.Name = "txtCommunicationType";
            this.txtCommunicationType.ReadOnly = true;
            this.txtCommunicationType.Size = new System.Drawing.Size(155, 20);
            this.txtCommunicationType.TabIndex = 20;
            // 
            // txtIndividualName
            // 
            this.txtIndividualName.Location = new System.Drawing.Point(125, 37);
            this.txtIndividualName.Name = "txtIndividualName";
            this.txtIndividualName.ReadOnly = true;
            this.txtIndividualName.Size = new System.Drawing.Size(133, 20);
            this.txtIndividualName.TabIndex = 21;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(24, 310);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(184, 20);
            this.label9.TabIndex = 22;
            this.label9.Text = "Communication Detail";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(532, 308);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(99, 20);
            this.label10.TabIndex = 23;
            this.label10.Text = "Task Detail";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(799, 354);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(83, 16);
            this.label13.TabIndex = 31;
            this.label13.Text = "Create Date:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(535, 476);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(68, 16);
            this.label14.TabIndex = 30;
            this.label14.Text = "Comment:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(535, 354);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(78, 16);
            this.label16.TabIndex = 28;
            this.label16.Text = "Created By:";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(535, 389);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(88, 16);
            this.label17.TabIndex = 27;
            this.label17.Text = "Assigned To:";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(535, 422);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(56, 16);
            this.label18.TabIndex = 26;
            this.label18.Text = "Subject:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(799, 389);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(68, 16);
            this.label19.TabIndex = 25;
            this.label19.Text = "Due Date:";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(535, 674);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(48, 16);
            this.label23.TabIndex = 34;
            this.label23.Text = "Status:";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(535, 572);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(59, 16);
            this.label24.TabIndex = 35;
            this.label24.Text = "Solution:";
            // 
            // txtTaskCreatedBy
            // 
            this.txtTaskCreatedBy.Location = new System.Drawing.Point(619, 353);
            this.txtTaskCreatedBy.Name = "txtTaskCreatedBy";
            this.txtTaskCreatedBy.ReadOnly = true;
            this.txtTaskCreatedBy.Size = new System.Drawing.Size(158, 20);
            this.txtTaskCreatedBy.TabIndex = 36;
            // 
            // txtTaskCreateDate
            // 
            this.txtTaskCreateDate.Location = new System.Drawing.Point(888, 353);
            this.txtTaskCreateDate.Name = "txtTaskCreateDate";
            this.txtTaskCreateDate.ReadOnly = true;
            this.txtTaskCreateDate.Size = new System.Drawing.Size(137, 20);
            this.txtTaskCreateDate.TabIndex = 37;
            // 
            // txtTaskAssignedTo
            // 
            this.txtTaskAssignedTo.Location = new System.Drawing.Point(619, 387);
            this.txtTaskAssignedTo.Name = "txtTaskAssignedTo";
            this.txtTaskAssignedTo.ReadOnly = true;
            this.txtTaskAssignedTo.Size = new System.Drawing.Size(158, 20);
            this.txtTaskAssignedTo.TabIndex = 38;
            // 
            // txtTaskDueDate
            // 
            this.txtTaskDueDate.Location = new System.Drawing.Point(888, 387);
            this.txtTaskDueDate.Name = "txtTaskDueDate";
            this.txtTaskDueDate.ReadOnly = true;
            this.txtTaskDueDate.Size = new System.Drawing.Size(137, 20);
            this.txtTaskDueDate.TabIndex = 39;
            // 
            // txtTaskSubject
            // 
            this.txtTaskSubject.Location = new System.Drawing.Point(619, 421);
            this.txtTaskSubject.Name = "txtTaskSubject";
            this.txtTaskSubject.ReadOnly = true;
            this.txtTaskSubject.Size = new System.Drawing.Size(406, 20);
            this.txtTaskSubject.TabIndex = 40;
            // 
            // txtTaskComment
            // 
            this.txtTaskComment.Location = new System.Drawing.Point(619, 475);
            this.txtTaskComment.Multiline = true;
            this.txtTaskComment.Name = "txtTaskComment";
            this.txtTaskComment.ReadOnly = true;
            this.txtTaskComment.Size = new System.Drawing.Size(406, 91);
            this.txtTaskComment.TabIndex = 41;
            // 
            // txtTaskSolution
            // 
            this.txtTaskSolution.Location = new System.Drawing.Point(619, 572);
            this.txtTaskSolution.Multiline = true;
            this.txtTaskSolution.Name = "txtTaskSolution";
            this.txtTaskSolution.ReadOnly = true;
            this.txtTaskSolution.Size = new System.Drawing.Size(406, 87);
            this.txtTaskSolution.TabIndex = 42;
            // 
            // txtTaskStatus
            // 
            this.txtTaskStatus.Location = new System.Drawing.Point(619, 674);
            this.txtTaskStatus.Name = "txtTaskStatus";
            this.txtTaskStatus.ReadOnly = true;
            this.txtTaskStatus.Size = new System.Drawing.Size(158, 20);
            this.txtTaskStatus.TabIndex = 43;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(927, 715);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(98, 31);
            this.btnClose.TabIndex = 44;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtMembershipNo
            // 
            this.txtMembershipNo.Location = new System.Drawing.Point(347, 37);
            this.txtMembershipNo.Name = "txtMembershipNo";
            this.txtMembershipNo.ReadOnly = true;
            this.txtMembershipNo.Size = new System.Drawing.Size(160, 20);
            this.txtMembershipNo.TabIndex = 46;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(276, 38);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(72, 16);
            this.label15.TabIndex = 45;
            this.label15.Text = "MEMB No:";
            // 
            // comboCaseNo
            // 
            this.comboCaseNo.FormattingEnabled = true;
            this.comboCaseNo.Location = new System.Drawing.Point(125, 75);
            this.comboCaseNo.Name = "comboCaseNo";
            this.comboCaseNo.Size = new System.Drawing.Size(133, 21);
            this.comboCaseNo.TabIndex = 47;
            this.comboCaseNo.SelectedIndexChanged += new System.EventHandler(this.comboCaseNo_SelectedIndexChanged);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(23, 457);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(40, 16);
            this.label20.TabIndex = 48;
            this.label20.Text = "Body";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(276, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 16);
            this.label3.TabIndex = 49;
            this.label3.Text = "Illness No:";
            // 
            // comboIllnessNo
            // 
            this.comboIllnessNo.FormattingEnabled = true;
            this.comboIllnessNo.Location = new System.Drawing.Point(347, 75);
            this.comboIllnessNo.Name = "comboIllnessNo";
            this.comboIllnessNo.Size = new System.Drawing.Size(115, 21);
            this.comboIllnessNo.TabIndex = 50;
            this.comboIllnessNo.SelectedIndexChanged += new System.EventHandler(this.comboIllnessNo_SelectedIndexChanged);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(781, 76);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(78, 16);
            this.label21.TabIndex = 51;
            this.label21.Text = "Incident No:";
            // 
            // comboIncidentNo
            // 
            this.comboIncidentNo.FormattingEnabled = true;
            this.comboIncidentNo.Location = new System.Drawing.Point(867, 75);
            this.comboIncidentNo.Name = "comboIncidentNo";
            this.comboIncidentNo.Size = new System.Drawing.Size(158, 21);
            this.comboIncidentNo.TabIndex = 52;
            this.comboIncidentNo.SelectedIndexChanged += new System.EventHandler(this.comboIncidentNo_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(25, 572);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 16);
            this.label4.TabIndex = 53;
            this.label4.Text = "Solution";
            // 
            // txtCommunicationSolution
            // 
            this.txtCommunicationSolution.Location = new System.Drawing.Point(28, 591);
            this.txtCommunicationSolution.Multiline = true;
            this.txtCommunicationSolution.Name = "txtCommunicationSolution";
            this.txtCommunicationSolution.Size = new System.Drawing.Size(481, 103);
            this.txtCommunicationSolution.TabIndex = 54;
            // 
            // btnSaveSolution
            // 
            this.btnSaveSolution.Location = new System.Drawing.Point(370, 715);
            this.btnSaveSolution.Name = "btnSaveSolution";
            this.btnSaveSolution.Size = new System.Drawing.Size(139, 31);
            this.btnSaveSolution.TabIndex = 55;
            this.btnSaveSolution.Text = "Save Solution";
            this.btnSaveSolution.UseVisualStyleBackColor = true;
            this.btnSaveSolution.Click += new System.EventHandler(this.btnSaveSolution_Click);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Comm No";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 80;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Comm Type";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Subject";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 80;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Body";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Task Id";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 80;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "Subject";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 90;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "Comment";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.HeaderText = "Solution";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.Width = 120;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(23, 353);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(71, 16);
            this.label22.TabIndex = 56;
            this.label22.Text = "Comm No:";
            // 
            // txtCommunicationNo
            // 
            this.txtCommunicationNo.Location = new System.Drawing.Point(109, 353);
            this.txtCommunicationNo.Name = "txtCommunicationNo";
            this.txtCommunicationNo.ReadOnly = true;
            this.txtCommunicationNo.Size = new System.Drawing.Size(137, 20);
            this.txtCommunicationNo.TabIndex = 57;
            // 
            // txtDiseaseName
            // 
            this.txtDiseaseName.Location = new System.Drawing.Point(468, 75);
            this.txtDiseaseName.Name = "txtDiseaseName";
            this.txtDiseaseName.ReadOnly = true;
            this.txtDiseaseName.Size = new System.Drawing.Size(287, 20);
            this.txtDiseaseName.TabIndex = 58;
            // 
            // frmCommunicationHelper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1043, 766);
            this.Controls.Add(this.txtDiseaseName);
            this.Controls.Add(this.txtCommunicationNo);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.btnSaveSolution);
            this.Controls.Add(this.txtCommunicationSolution);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboIncidentNo);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.comboIllnessNo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.comboCaseNo);
            this.Controls.Add(this.txtMembershipNo);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.txtTaskStatus);
            this.Controls.Add(this.txtTaskSolution);
            this.Controls.Add(this.txtTaskComment);
            this.Controls.Add(this.txtTaskSubject);
            this.Controls.Add(this.txtTaskDueDate);
            this.Controls.Add(this.txtTaskAssignedTo);
            this.Controls.Add(this.txtTaskCreateDate);
            this.Controls.Add(this.txtTaskCreatedBy);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtIndividualName);
            this.Controls.Add(this.txtCommunicationType);
            this.Controls.Add(this.txtCommunicationSubject);
            this.Controls.Add(this.txtCommunicationBody);
            this.Controls.Add(this.txtCommunicationCreateDate);
            this.Controls.Add(this.txtCommunicationCreatedBy);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.gvTaskList);
            this.Controls.Add(this.gvCommunicationList);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmCommunicationHelper";
            this.Text = "Communication Helper";
            this.Load += new System.EventHandler(this.frmCommunicationHelper_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvCommunicationList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTaskList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView gvCommunicationList;
        private System.Windows.Forms.DataGridView gvTaskList;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtCommunicationCreatedBy;
        private System.Windows.Forms.TextBox txtCommunicationCreateDate;
        private System.Windows.Forms.TextBox txtCommunicationBody;
        private System.Windows.Forms.TextBox txtCommunicationSubject;
        private System.Windows.Forms.TextBox txtCommunicationType;
        private System.Windows.Forms.TextBox txtIndividualName;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox txtTaskCreatedBy;
        private System.Windows.Forms.TextBox txtTaskCreateDate;
        private System.Windows.Forms.TextBox txtTaskAssignedTo;
        private System.Windows.Forms.TextBox txtTaskDueDate;
        private System.Windows.Forms.TextBox txtTaskSubject;
        private System.Windows.Forms.TextBox txtTaskComment;
        private System.Windows.Forms.TextBox txtTaskSolution;
        private System.Windows.Forms.TextBox txtTaskStatus;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TextBox txtMembershipNo;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn TaskIdTask;
        private System.Windows.Forms.DataGridViewTextBoxColumn SubjectTask;
        private System.Windows.Forms.DataGridViewTextBoxColumn CommentTask;
        private System.Windows.Forms.DataGridViewTextBoxColumn SolutionTask;
        private System.Windows.Forms.ComboBox comboCaseNo;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboIllnessNo;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ComboBox comboIncidentNo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCommunicationSolution;
        private System.Windows.Forms.Button btnSaveSolution;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txtCommunicationNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn CommNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn CommType;
        private System.Windows.Forms.DataGridViewTextBoxColumn Subject;
        private System.Windows.Forms.DataGridViewTextBoxColumn Body;
        private System.Windows.Forms.DataGridViewTextBoxColumn Solution;
        private System.Windows.Forms.TextBox txtDiseaseName;
    }
}