using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Linq;
using System.IO;

namespace CMMManager
{
    //public enum RelatedName { Individual, Prospect };
    public enum RelatedToTable { Membership, Case, Illness, Incident, MedicalBill, Settlement };
    public enum TaskStatus { NotStarted, InProgress, Completed, WaitingOnSomeoneElse, Deferred, Solved };
    public enum TaskPriority { High, Normal, Low };

    public partial class frmTaskCreationPage : Form
    {
        private String IndividualId;
        private RelatedToTable relatedTable;
        //private RelatedName relatedName;
        private String IndividualName;
        private String TableName;
        private int LoggedInUserId;

        private List<String> lstAttachedFilesNames;
        private String AttachmentDestinationPath = @"\\cmm-2014u\Sharefolder\TaskAttachments\";

        private List<UserInfo> lstUserInfo;

        private SqlConnection connRN;
        private SqlConnection connSalesForce;
        private String connStringRN;
        private String connStringSalesForce;

        public frmTaskCreationPage()
        {
            InitializeComponent();
        }

        public frmTaskCreationPage(String individualId, RelatedToTable relatedTo, String individualName, String relatedToTable, int loggedInUserId)
        {
            IndividualId = individualId;
            relatedTable = relatedTo;
            //relatedName = related_name;
            IndividualName = individualName;
            TableName = relatedToTable;
            LoggedInUserId = loggedInUserId;

            InitializeComponent();

            lstAttachedFilesNames = new List<string>();
            lstUserInfo = new List<UserInfo>();

            connStringRN = @"Data Source=CMM-2014U\CMM; Initial Catalog=RN_DB;Integrated Security=True;";
            connStringSalesForce = @"Data Source=CMM-2014U\CMM; Initial Catalog=SalesForce; Integrated Security=True";

            connRN = new SqlConnection(connStringRN);
            connSalesForce = new SqlConnection(connStringSalesForce);
        }

        private void frmTaskCreationPage_Load(object sender, EventArgs e)
        {
            lstUserInfo.Clear();
            lstAttachedFilesNames.Clear();

            String strSqlQueryForAllStaff = "select [dbo].[tbl_user].[User_Id], [dbo].[tbl_user].[User_Name], [dbo].[tbl_user].[User_Email], " +
                                            "[dbo].[tbl_user].[User_Role_Id], [dbo].[tbl_user].[Department_Id] " +
                                            "from [dbo].[tbl_user]";

            SqlCommand cmdQueryForAllStaff = new SqlCommand(strSqlQueryForAllStaff, connRN);
            cmdQueryForAllStaff.CommandType = CommandType.Text;

            if (connRN.State != ConnectionState.Closed)
            {
                connRN.Close();
                connRN.Open();
            }
            else if (connRN.State == ConnectionState.Closed) connRN.Open();

            SqlDataReader rdrUserInfo = cmdQueryForAllStaff.ExecuteReader();
            if (rdrUserInfo.HasRows)
            {
                while (rdrUserInfo.Read())
                {
                    lstUserInfo.Add(new UserInfo { UserId = rdrUserInfo.GetInt16(0),
                                                   UserName = rdrUserInfo.GetString(1),
                                                   UserEmail = rdrUserInfo.GetString(2),
                                                   UserRoleId = rdrUserInfo.GetInt16(3),
                                                   DepartmentId = rdrUserInfo.GetInt16(4) });
                }
            }
            rdrUserInfo.Close();
            if (connRN.State == ConnectionState.Open) connRN.Close();

            var srcStaffInfo = new AutoCompleteStringCollection();

            foreach (UserInfo info in lstUserInfo)
            {
                srcStaffInfo.Add(info.UserName);
            }

            txtTaskNameAssignedTo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtTaskNameAssignedTo.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtTaskNameAssignedTo.AutoCompleteCustomSource = srcStaffInfo;

            comboTaskRelatedTo.Items.Add(RelatedToTable.Membership);
            comboTaskRelatedTo.Items.Add(RelatedToTable.Case);
            comboTaskRelatedTo.Items.Add(RelatedToTable.Illness);
            comboTaskRelatedTo.Items.Add(RelatedToTable.Incident);
            comboTaskRelatedTo.Items.Add(RelatedToTable.MedicalBill);
            comboTaskRelatedTo.Items.Add(RelatedToTable.Settlement);

            comboTaskRelatedTo.SelectedItem = RelatedToTable.MedicalBill;

            //comboTaskName.Items.Add(RelatedName.Individual);
            //comboTaskName.Items.Add(RelatedName.Prospect);

            //comboTaskName.SelectedItem = RelatedName.Individual;
            txtTaskRelatedTo.Text = TableName;
            txtNameOnTask.Text = IndividualName;
            txtIndividualId.Text = IndividualId;

            String PhoneNo = String.Empty;
            String Email = String.Empty;

            String strSqlQueryForPhoneAndEmail = "select [dbo].[contact].[HomePhone], [dbo].[contact].[Email] from [dbo].[contact] where [dbo].[contact].[Individual_ID__c] = @IndividualId";

            SqlCommand cmdQueryForPhoneAndEmail = new SqlCommand(strSqlQueryForPhoneAndEmail, connSalesForce);
            cmdQueryForPhoneAndEmail.CommandType = CommandType.Text;

            cmdQueryForPhoneAndEmail.Parameters.AddWithValue("@IndividualId", IndividualId);

            if (connSalesForce.State != ConnectionState.Closed)
            {
                connSalesForce.Close();
                connSalesForce.Open();
            }
            else if (connSalesForce.State == ConnectionState.Closed) connSalesForce.Open();
            SqlDataReader rdrPhoneAndEmail = cmdQueryForPhoneAndEmail.ExecuteReader();
            if (rdrPhoneAndEmail.HasRows)
            {
                rdrPhoneAndEmail.Read();
                if (!rdrPhoneAndEmail.IsDBNull(0)) PhoneNo = rdrPhoneAndEmail.GetString(0);
                if (!rdrPhoneAndEmail.IsDBNull(1)) Email = rdrPhoneAndEmail.GetString(1);
            }
            rdrPhoneAndEmail.Close();
            if (connSalesForce.State == ConnectionState.Open) connSalesForce.Close();

            txtTaskPhone.Text = PhoneNo;
            txtTaskEmail.Text = Email;

            comboTaskStatus.SelectedIndex = 0;
            comboTaskPriority.SelectedIndex = 1;

            comboReminderTimePicker.SelectedIndex = 18;
        }

        private void btnSaveTask_Click(object sender, EventArgs e)
        {
            String NameAssignedTo = txtTaskNameAssignedTo.Text.Trim();

            String strSqlQueryForUserId = "select [dbo].[tbl_user].[User_Id] from [dbo].[tbl_user] where [dbo].[tbl_user].[User_Name] = @UserName";

            SqlCommand cmdQueryForUserId = new SqlCommand(strSqlQueryForUserId, connRN);
            cmdQueryForUserId.CommandType = CommandType.Text;

            cmdQueryForUserId.Parameters.AddWithValue("@UserName", NameAssignedTo);

            if (connRN.State != ConnectionState.Closed)
            {
                connRN.Close();
                connRN.Open();
            }
            else if (connRN.State == ConnectionState.Closed) connRN.Open();
            Object objUserId = cmdQueryForUserId.ExecuteScalar();
            int? nUserId = null;    // This variable would be used in insert sql statement for Task table
            if (objUserId != null) nUserId = Int16.Parse(objUserId.ToString());
            if (connRN.State == ConnectionState.Open) connRN.Close();

            String Subject = txtTaskSubject.Text.Trim();
            DateTime DueDate = dtpTaskDueDate.Value;

            String RelatedTableName = comboTaskRelatedTo.SelectedItem.ToString();
            String WhatId = txtTaskRelatedTo.Text.Trim();         // Related Id
            //String RelatedName = comboTaskName.SelectedItem.ToString();
            String WhoId = txtIndividualId.Text.Trim();             // Related name
            String WhoName = txtNameOnTask.Text.Trim();             // Individual Name

            String Comment = txtTaskComments.Text.Trim();
            String Solution = txtTaskSolution.Text.Trim();

            TaskStatus ts = (TaskStatus)comboTaskStatus.SelectedIndex;
            TaskPriority tp = (TaskPriority)comboTaskPriority.SelectedIndex;

            String strDate = String.Empty;
            String strTmpTime = String.Empty;
            String strTime = String.Empty;
            String TmpTime = String.Empty;
            DateTime? Reminder = null;

            if (chkReminder.Checked)
            {
                strDate = dtpReminderDatePicker.Value.ToString("MM/dd/yyyy");
                strTmpTime = comboReminderTimePicker.SelectedItem.ToString();
                strTime = String.Empty;
                TmpTime = strTmpTime.Substring(strTmpTime.Length - 2, 2);

                if (strTmpTime.Substring(strTmpTime.Length - 2, 2) == "PM")
                {
                    int nTime = Int16.Parse(strTmpTime.Substring(0, strTmpTime.IndexOf(':')));
                    nTime += 12;
                    strTime = nTime.ToString() + ':' + strTmpTime.Substring(strTmpTime.IndexOf(':') + 1, 2) + " AM";
                }
                else strTime = strTmpTime;

                Reminder = new DateTime(Int16.Parse(strDate.Substring(6, 4)),
                                        Int16.Parse(strDate.Substring(0, 2)),
                                        Int16.Parse(strDate.Substring(3, 2)),
                                        Int16.Parse(strTime.Substring(0, 2)),
                                        Int16.Parse(strTime.Substring(3, 2)),
                                        0);
            }

            String strSqlQueryForAccountId = "select [dbo].[contact].[AccountId] from [dbo].[contact] where [dbo].[contact].[Individual_ID__c] = @IndividualId";

            SqlCommand cmdQueryForAccountId = new SqlCommand(strSqlQueryForAccountId, connSalesForce);
            cmdQueryForAccountId.CommandType = CommandType.Text;

            cmdQueryForAccountId.Parameters.AddWithValue("@IndividualId", IndividualId);

            if (connSalesForce.State != ConnectionState.Closed)
            {
                connSalesForce.Close();
                connSalesForce.Open();
            }
            else if (connSalesForce.State == ConnectionState.Closed) connSalesForce.Open();
            Object objAccountId = cmdQueryForAccountId.ExecuteScalar();
            if (connSalesForce.State == ConnectionState.Open) connSalesForce.Close();

            String AccountId = String.Empty;
            if (objAccountId != null) AccountId = objAccountId.ToString();

            String strSqlInsertIntoTask = "insert into [dbo].[tbl_task] ([dbo].[tbl_task].[whoid], [dbo].[tbl_task].[IndividualName], [dbo].[tbl_task].[whatid], [dbo].[tbl_task].[IsDeleted], " +
                                          "[dbo].[tbl_task].[AssignedTo], [dbo].[tbl_task].[Subject], [dbo].[tbl_task].[DueDate], " +
                                          "[dbo].[tbl_task].[CreateDate], [dbo].[tbl_task].[CreatedById], [dbo].[tbl_task].[LastModifiedDate], [dbo].[tbl_task].[LastModifiedById], " +
                                          "[dbo].[tbl_task].[ActivityDate], " +
                                          "[dbo].[tbl_task].[Comment], [dbo].[tbl_task].[Solution], [dbo].[tbl_task].[Status], [dbo].[tbl_task].[Priority], " +
                                          "[dbo].[tbl_task].[AccountId], [dbo].[tbl_task].[IsClosed], [dbo].[tbl_task].[ReminderDateTime], [dbo].[tbl_task].[IsReminderSet], " +
                                          "[dbo].[tbl_task].[IsArchived], [dbo].[tbl_task].[HasAttachment], [dbo].[tbl_task].[Attachment]) " +
                                          "values (@WhoId, @IndividualName, @WhatId, 0, @AssignedTo, @Subject, @DueDate, @CreateDate, @CreatedById, " +
                                          "@LastModifiedDate, @LastModifiedById, @ActivityDate, " +
                                          "@Comment, @Solution, @Status, @Priority, @AccountId, @IsClosed, @ReminderDateTime, @IsReminderSet, @IsArchived, @HasAttachment, @Attachment)";

            SqlCommand cmdInsertIntoTask = new SqlCommand(strSqlInsertIntoTask, connRN);
            cmdInsertIntoTask.CommandType = CommandType.Text;

            cmdInsertIntoTask.Parameters.AddWithValue("@WhoId", WhoId);
            cmdInsertIntoTask.Parameters.AddWithValue("@IndividualName", WhoName);
            cmdInsertIntoTask.Parameters.AddWithValue("@WhatId", WhatId);
            cmdInsertIntoTask.Parameters.AddWithValue("@AssignedTo", nUserId);
            cmdInsertIntoTask.Parameters.AddWithValue("@Subject", Subject);
            cmdInsertIntoTask.Parameters.AddWithValue("@DueDate", DueDate);
            cmdInsertIntoTask.Parameters.AddWithValue("@CreateDate", DateTime.Today);
            cmdInsertIntoTask.Parameters.AddWithValue("@CreatedById", LoggedInUserId);
            cmdInsertIntoTask.Parameters.AddWithValue("@LastModifiedDate", DateTime.Today);
            cmdInsertIntoTask.Parameters.AddWithValue("@LastModifiedById", LoggedInUserId);
            cmdInsertIntoTask.Parameters.AddWithValue("@ActivityDate", DateTime.Today);
            cmdInsertIntoTask.Parameters.AddWithValue("@Comment", Comment);
            cmdInsertIntoTask.Parameters.AddWithValue("@Solution", Solution);
            cmdInsertIntoTask.Parameters.AddWithValue("@Status", ts);
            cmdInsertIntoTask.Parameters.AddWithValue("@Priority", tp);
            cmdInsertIntoTask.Parameters.AddWithValue("@AccountId", AccountId);
            cmdInsertIntoTask.Parameters.AddWithValue("@IsClosed", 0);
            if (chkReminder.Checked) cmdInsertIntoTask.Parameters.AddWithValue("@ReminderDateTime", Reminder);
            else cmdInsertIntoTask.Parameters.AddWithValue("@ReminderDateTime", DBNull.Value);
            cmdInsertIntoTask.Parameters.AddWithValue("@IsReminderSet", chkReminder.Checked);
            cmdInsertIntoTask.Parameters.AddWithValue("@IsArchived", 0);
            if (lstAttachments.Items.Count > 0)
            {
                cmdInsertIntoTask.Parameters.AddWithValue("@HasAttachment", 1);
            }
            else if (lstAttachments.Items.Count == 0)
            {
                cmdInsertIntoTask.Parameters.AddWithValue("@HasAttachment", 0);
            }
            String AttachedFileNames = String.Empty;

            for (int i = 0; i < lstAttachments.Items.Count; i++)
            {
                AttachedFileNames += lstAttachments.Items[i].ToString() + "; ";
                File.Copy(lstAttachments.Items[i].ToString(), AttachmentDestinationPath + lstAttachedFilesNames[i]);
            }
            cmdInsertIntoTask.Parameters.AddWithValue("@Attachment", AttachedFileNames);

            if (connRN.State != ConnectionState.Closed)
            {
                connRN.Close();
                connRN.Open();
            }
            else if (connRN.State == ConnectionState.Closed) connRN.Open();
            int nRowInserted = cmdInsertIntoTask.ExecuteNonQuery();
            if (connRN.State == ConnectionState.Open) connRN.Close();

            if (nRowInserted == 1)
            {
                MessageBox.Show("The task has been created.");
                return;
            }
            else if (nRowInserted == 0)
            {
                MessageBox.Show("The task has not been created.");
                return;
            }
        }

        private void btnAttachFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDlg = new OpenFileDialog();

            openFileDlg.Filter = "PDF Files | *.pdf; | JPG Files | *.jpg; *.jpef";
            openFileDlg.DefaultExt = "pdf";
            openFileDlg.RestoreDirectory = true;

            if (openFileDlg.ShowDialog() == DialogResult.OK)
            {
                lstAttachments.Items.Add(openFileDlg.FileName);
                lstAttachedFilesNames.Add(openFileDlg.SafeFileName);
                return;
            }
            else return;
        }
    }

    public class UserInfo
    {
        public int? UserId;
        public String UserName;
        public String UserEmail;
        public int? UserRoleId;
        public int? DepartmentId;

        public UserInfo()
        {
            UserId = null;
            UserName = String.Empty;
            UserEmail = String.Empty;
            UserRoleId = null;
            DepartmentId = null;
        }

        public UserInfo(int user_id, String user_name, String user_email, int user_role_id, int department_id)
        {
            UserId = user_id;
            UserName = user_name;
            UserEmail = user_email;
            UserRoleId = user_role_id;
            DepartmentId = department_id;
        }
    }
}
