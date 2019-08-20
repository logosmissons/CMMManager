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
//using System.Linq;
using System.IO;

namespace CMMManager
{
    //public enum RelatedName { Individual, Prospect };
    //public enum RelatedToTable { Membership, Case, Illness, Incident, MedicalBill, Settlement };
    //public enum TaskStatus { NotStarted, InProgress, Completed, WaitingOnSomeoneElse, Deferred, Solved };
    //public enum TaskPriority { High, Normal, Low };
    //public enum TaskMode { AddNew, EditInDashboard, EditInMedBill }

    public partial class frmTaskCreationPage : Form
    {

        private TaskMode taskMode;
        private String TaskCreatorName;
        private String WhoId;
        private RelatedToTable relatedTable;
        private String IndividualName;
        private String WhatId;
        private UserInfo TaskCreatorInfo;
        private UserInfo LoggedInuserInfo;
        private UserInfo AssignedToStaffInfo;
        private List<UserInfo> lstUserInfo;

        private StringBuilder sbComment;
        private StringBuilder sbSolution;

        /// <summary>
        /// For modifying Task from Med Bill form
        /// </summary>
        //taskMode = mode;
        private int? nTaskId;
        //private int nLoggedInUserId;
        //private UserRole nLoggedInUserRoleId;
        //private Department LoggedInUserDepartment;


        private SqlConnection connRN;
        private SqlConnection connSalesForce;
        private String connStringRN;
        private String connStringSalesForce;

        public frmTaskCreationPage()
        {
            InitializeComponent();
            //InitializeTaskForm();
            LoggedInuserInfo = new UserInfo();
            AssignedToStaffInfo = new UserInfo();
            lstUserInfo = new List<UserInfo>();

            connStringRN = @"Data Source=CMM-2014U\CMM; Initial Catalog=RN_DB;Integrated Security=True;";
            connStringSalesForce = @"Data Source=CMM-2014U\CMM; Initial Catalog=SalesForce; Integrated Security=True";

            connRN = new SqlConnection(connStringRN);
            connSalesForce = new SqlConnection(connStringSalesForce);

            sbComment = new StringBuilder();
            sbSolution = new StringBuilder();
        }

        public frmTaskCreationPage(int task_id,
                                   TaskMode mode)
        {
            InitializeComponent();

            nTaskId = task_id;
            taskMode = mode;

            TaskCreatorInfo = new UserInfo();
            LoggedInuserInfo = new UserInfo();
            AssignedToStaffInfo = new UserInfo();
            lstUserInfo = new List<UserInfo>();

            connStringRN = @"Data Source=CMM-2014U\CMM; Initial Catalog=RN_DB;Integrated Security=True;";
            connStringSalesForce = @"Data Source=CMM-2014U\CMM; Initial Catalog=SalesForce; Integrated Security=True";

            connRN = new SqlConnection(connStringRN);
            connSalesForce = new SqlConnection(connStringSalesForce);

            sbComment = new StringBuilder();
            sbSolution = new StringBuilder();
        }

        public frmTaskCreationPage(String IndividualId,
                                   String individualName,
                                   int loggedInUserId,
                                   String loggedInUserName,
                                   UserRole loggedInUserRoleId,
                                   Department loggedInUserDepartment,
                                   TaskMode mode,
                                   String email_case,
                                   String email_subject,
                                   String email_body)
        {
            InitializeComponent();
            taskMode = mode;
            WhoId = IndividualId;
            IndividualName = individualName;

            LoggedInuserInfo = new UserInfo();

            LoggedInuserInfo.UserId = loggedInUserId;
            LoggedInuserInfo.UserName = loggedInUserName;
            LoggedInuserInfo.UserRoleId = loggedInUserRoleId;
            LoggedInuserInfo.departmentInfo.DepartmentId = loggedInUserDepartment;

            AssignedToStaffInfo = new UserInfo();

            lstUserInfo = new List<UserInfo>();

            connStringRN = @"Data Source=CMM-2014U\CMM; Initial Catalog=RN_DB;Integrated Security=True;";
            connStringSalesForce = @"Data Source=CMM-2014U\CMM; Initial Catalog=SalesForce; Integrated Security=True";

            connRN = new SqlConnection(connStringRN);
            connSalesForce = new SqlConnection(connStringSalesForce);

            sbComment = new StringBuilder();           
            sbComment.AppendLine(email_case);
            sbComment.AppendLine(email_subject);
            sbComment.AppendLine(email_body);
            sbSolution = new StringBuilder();
        }

        public frmTaskCreationPage(String IndividualId, 
                                   RelatedToTable relatedTo, 
                                   String individualName, 
                                   String MedBillId, 
                                   int loggedInUserId,
                                   String loggedInUserName,
                                   UserRole loggedInUserRoleId, 
                                   Department loggedInUserDepartment,
                                   TaskMode mode)
        {

            taskMode = mode;
            WhoId = IndividualId;
            relatedTable = relatedTo;
            IndividualName = individualName;
            WhatId = MedBillId;

            LoggedInuserInfo = new UserInfo();

            LoggedInuserInfo.UserId = loggedInUserId;
            LoggedInuserInfo.UserName = loggedInUserName;
            LoggedInuserInfo.UserRoleId = loggedInUserRoleId;
            LoggedInuserInfo.departmentInfo.DepartmentId = loggedInUserDepartment;

            AssignedToStaffInfo = new UserInfo();

            InitializeComponent();
            lstUserInfo = new List<UserInfo>();

            connStringRN = @"Data Source=CMM-2014U\CMM; Initial Catalog=RN_DB;Integrated Security=True;";
            connStringSalesForce = @"Data Source=CMM-2014U\CMM; Initial Catalog=SalesForce; Integrated Security=True";

            connRN = new SqlConnection(connStringRN);
            connSalesForce = new SqlConnection(connStringSalesForce);

            sbComment = new StringBuilder();
            sbSolution = new StringBuilder();

        }

        public frmTaskCreationPage(String IndividualId,
                           RelatedToTable relatedTo,
                           String individualName,
                           String MembershipId,
                           String MedBillId,
                           int loggedInUserId,
                           String loggedInUserName,
                           UserRole loggedInUserRoleId,
                           Department loggedInUserDepartment,
                           TaskMode mode)
        {

            taskMode = mode;
            WhoId = IndividualId;
            relatedTable = relatedTo;
            IndividualName = individualName;
            WhatId = MembershipId;

            LoggedInuserInfo = new UserInfo();

            LoggedInuserInfo.UserId = loggedInUserId;
            LoggedInuserInfo.UserName = loggedInUserName;
            LoggedInuserInfo.UserRoleId = loggedInUserRoleId;
            LoggedInuserInfo.departmentInfo.DepartmentId = loggedInUserDepartment;

            AssignedToStaffInfo = new UserInfo();

            InitializeComponent();
            lstUserInfo = new List<UserInfo>();

            connStringRN = @"Data Source=CMM-2014U\CMM; Initial Catalog=RN_DB;Integrated Security=True;";
            connStringSalesForce = @"Data Source=CMM-2014U\CMM; Initial Catalog=SalesForce; Integrated Security=True";

            connRN = new SqlConnection(connStringRN);
            connSalesForce = new SqlConnection(connStringSalesForce);

            sbComment = new StringBuilder();
            sbSolution = new StringBuilder();

        }

        public frmTaskCreationPage(String IndividualId,
                           RelatedToTable relatedTo,
                           String individualName,
                           String CaseId,
                           int loggedInUserId,
                           String loggedInUserName,
                           UserRole loggedInUserRoleId,
                           Department loggedInUserDepartment,
                           TaskMode mode,
                           int CaseTask)
        {

            taskMode = mode;
            WhoId = IndividualId;
            relatedTable = relatedTo;
            IndividualName = individualName;
            WhatId = CaseId;

            LoggedInuserInfo = new UserInfo();

            LoggedInuserInfo.UserId = loggedInUserId;
            LoggedInuserInfo.UserName = loggedInUserName;
            LoggedInuserInfo.UserRoleId = loggedInUserRoleId;
            LoggedInuserInfo.departmentInfo.DepartmentId = loggedInUserDepartment;

            AssignedToStaffInfo = new UserInfo();

            InitializeComponent();
            lstUserInfo = new List<UserInfo>();

            connStringRN = @"Data Source=CMM-2014U\CMM; Initial Catalog=RN_DB;Integrated Security=True;";
            connStringSalesForce = @"Data Source=CMM-2014U\CMM; Initial Catalog=SalesForce; Integrated Security=True";

            connRN = new SqlConnection(connStringRN);
            connSalesForce = new SqlConnection(connStringSalesForce);

            sbComment = new StringBuilder();
            sbSolution = new StringBuilder();

        }
        public frmTaskCreationPage(int task_id, 
                                   String individual_id, 
                                   int login_user_id, 
                                   String login_user_name, 
                                   UserRole login_user_role_id, 
                                   Department login_user_department, 
                                   TaskMode mode)
        {

            taskMode = mode;
            nTaskId = task_id;
            WhoId = individual_id;

            LoggedInuserInfo = new UserInfo();
            lstUserInfo = new List<UserInfo>();
            InitializeComponent();

            LoggedInuserInfo.UserId = login_user_id;
            LoggedInuserInfo.UserName = login_user_name;
            LoggedInuserInfo.UserRoleId = login_user_role_id;
            LoggedInuserInfo.departmentInfo.DepartmentId = login_user_department;

            connStringRN = @"Data Source=CMM-2014U\CMM; Initial Catalog=RN_DB;Integrated Security=True;";
            connStringSalesForce = @"Data Source=CMM-2014U\CMM; Initial Catalog=SalesForce; Integrated Security=True";

            connRN = new SqlConnection(connStringRN);
            connSalesForce = new SqlConnection(connStringSalesForce);

            sbComment = new StringBuilder();
            sbSolution = new StringBuilder();
        }

        public frmTaskCreationPage(int task_id, 
                                   String individual_id, 
                                   String creator_name, 
                                   int creator_id, 
                                   int login_user_id, 
                                   String login_user_name, 
                                   UserRole login_user_role_id, 
                                   Department login_user_department, 
                                   TaskMode mode)
        {
            taskMode = mode;
            nTaskId = task_id;
            WhoId = individual_id;
            TaskCreatorName = creator_name;

            TaskCreatorInfo = new UserInfo();

            TaskCreatorInfo.UserId = creator_id;
            TaskCreatorInfo.UserName = creator_name;

            LoggedInuserInfo = new UserInfo();
            lstUserInfo = new List<UserInfo>();
            InitializeComponent();

            LoggedInuserInfo.UserId = login_user_id;
            LoggedInuserInfo.UserName = login_user_name;
            LoggedInuserInfo.UserRoleId = login_user_role_id;
            LoggedInuserInfo.departmentInfo.DepartmentId = login_user_department;

            connStringRN = @"Data Source=CMM-2014U\CMM; Initial Catalog=RN_DB;Integrated Security=True;";
            connStringSalesForce = @"Data Source=CMM-2014U\CMM; Initial Catalog=SalesForce; Integrated Security=True";

            connRN = new SqlConnection(connStringRN);
            connSalesForce = new SqlConnection(connStringSalesForce);

            sbComment = new StringBuilder();
            sbSolution = new StringBuilder();
        }

        private void InitializeTaskForm()
        {
            //nTaskId = null;
            txtTaskCreator.Text = String.Empty;
            txtTaskNameAssignedTo.Text = String.Empty;
            txtTaskSubject.Text = String.Empty;
            comboTaskRelatedTo.Items.Clear();
            txtTaskRelatedTo.Text = String.Empty;
            txtNameOnTask.Text = String.Empty;
            txtIndividualId.Text = String.Empty;
            txtTaskComments.Text = String.Empty;
            txtTaskSolution.Text = String.Empty;
            txtTaskPhone.Text = String.Empty;
            txtTaskEmail.Text = String.Empty;
            chkReminder.Checked = false;

        }


        private void frmTaskCreationPage_Load(object sender, EventArgs e)
        {
            if (taskMode == TaskMode.AddNew)
            {
                nTaskId = null;
                lstUserInfo.Clear();
                InitializeTaskForm();

                txtTaskCreator.Text = LoggedInuserInfo.UserName;

                //String strSqlQueryForAllStaff = "select [dbo].[tbl_user].[User_Id], [dbo].[tbl_user].[User_Name], [dbo].[tbl_user].[User_Email], " +
                //                                "[dbo].[tbl_user].[User_Role_Id], [dbo].[tbl_user].[Department_Id] " +
                //                                "from [dbo].[tbl_user]";

                //SqlCommand cmdQueryForAllStaff = new SqlCommand(strSqlQueryForAllStaff, connRN);
                //cmdQueryForAllStaff.CommandType = CommandType.Text;

                //if (connRN.State != ConnectionState.Closed)
                //{
                //    connRN.Close();
                //    connRN.Open();
                //}
                //else if (connRN.State == ConnectionState.Closed) connRN.Open();

                //SqlDataReader rdrUserInfo = cmdQueryForAllStaff.ExecuteReader();
                //if (rdrUserInfo.HasRows)
                //{
                //    while (rdrUserInfo.Read())
                //    {
                //        lstUserInfo.Add(new UserInfo
                //        {
                //            UserId = rdrUserInfo.GetInt16(0),
                //            UserName = rdrUserInfo.GetString(1),
                //            UserEmail = rdrUserInfo.GetString(2),
                //            UserRoleId = (UserRole)rdrUserInfo.GetInt16(3),
                //            departmentInfo = new DepartmentInfo { DepartmentId = (Department)rdrUserInfo.GetInt16(4), DepartmentName = string.Empty }
                //        });
                //    }
                //}
                //rdrUserInfo.Close();
                //if (connRN.State == ConnectionState.Open) connRN.Close();

                //var srcStaffInfo = new AutoCompleteStringCollection();

                //foreach (UserInfo info in lstUserInfo)
                //{
                //    srcStaffInfo.Add(info.UserName);
                //}

                //txtTaskNameAssignedTo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                //txtTaskNameAssignedTo.AutoCompleteSource = AutoCompleteSource.CustomSource;
                //txtTaskNameAssignedTo.AutoCompleteCustomSource = srcStaffInfo;

                // Populate the sending department combo box

                comboTaskRelatedTo.Items.Add(RelatedToTable.Membership);
                comboTaskRelatedTo.Items.Add(RelatedToTable.Case);
                comboTaskRelatedTo.Items.Add(RelatedToTable.Illness);
                comboTaskRelatedTo.Items.Add(RelatedToTable.Incident);
                comboTaskRelatedTo.Items.Add(RelatedToTable.MedicalBill);
                comboTaskRelatedTo.Items.Add(RelatedToTable.Settlement);

                String strSqlQueryForTaskRelatedTo = "select [dbo].[tbl_task_related_to_code].[TaskRelatedToValue] from [dbo].[tbl_task_related_to_code]";

                SqlCommand cmdQueryForTaskRelatedTo = new SqlCommand(strSqlQueryForTaskRelatedTo, connRN);
                cmdQueryForTaskRelatedTo.CommandType = CommandType.Text;

                if (connRN.State != ConnectionState.Closed)
                {
                    connRN.Close();
                    connRN.Open();
                }
                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                comboTaskRelatedTo.Items.Clear();
                SqlDataReader rdrTaskRelatedTo = cmdQueryForTaskRelatedTo.ExecuteReader();
                if (rdrTaskRelatedTo.HasRows)
                {
                    while(rdrTaskRelatedTo.Read())
                    {
                        comboTaskRelatedTo.Items.Add(rdrTaskRelatedTo.GetString(0));
                    }
                }
                rdrTaskRelatedTo.Close();
                if (connRN.State != ConnectionState.Closed) connRN.Close();

                comboTaskRelatedTo.SelectedIndex = (int)relatedTable;


                txtTaskRelatedTo.Text = WhatId;
                txtNameOnTask.Text = IndividualName;
                txtIndividualId.Text = WhoId;

                txtTaskComments.Text = sbComment.ToString();
                txtTaskSolution.Text = sbSolution.ToString();

                String PhoneNo = String.Empty;
                String Email = String.Empty;

                String strSqlQueryForPhoneAndEmail = "select [dbo].[contact].[HomePhone], [dbo].[contact].[Email] from [dbo].[contact] where [dbo].[contact].[Individual_ID__c] = @IndividualId";

                SqlCommand cmdQueryForPhoneAndEmail = new SqlCommand(strSqlQueryForPhoneAndEmail, connSalesForce);
                cmdQueryForPhoneAndEmail.CommandType = CommandType.Text;

                cmdQueryForPhoneAndEmail.Parameters.AddWithValue("@IndividualId", WhoId);

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
            else if (taskMode == TaskMode.EditInMedBill)
            {
                lstUserInfo.Clear();
                InitializeTaskForm();
                txtTaskCreator.Text = TaskCreatorInfo.UserName;
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
                        lstUserInfo.Add(new UserInfo
                        {
                            UserId = rdrUserInfo.GetInt16(0),
                            UserName = rdrUserInfo.GetString(1),
                            UserEmail = rdrUserInfo.GetString(2),
                            UserRoleId = (UserRole)rdrUserInfo.GetInt16(3),
                            departmentInfo = new DepartmentInfo { DepartmentId = (Department)rdrUserInfo.GetInt16(4), DepartmentName = string.Empty }
                        });
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

                // Populate the sending department combo box

                comboTaskRelatedTo.Items.Add(RelatedToTable.Membership);
                comboTaskRelatedTo.Items.Add(RelatedToTable.Case);
                comboTaskRelatedTo.Items.Add(RelatedToTable.Illness);
                comboTaskRelatedTo.Items.Add(RelatedToTable.Incident);
                comboTaskRelatedTo.Items.Add(RelatedToTable.MedicalBill);
                comboTaskRelatedTo.Items.Add(RelatedToTable.Settlement);

                //comboTaskRelatedTo.SelectedItem = RelatedToTable.MedicalBill;

                txtTaskRelatedTo.Text = WhatId;
                txtNameOnTask.Text = IndividualName;
                txtIndividualId.Text = WhoId;

                String PhoneNo = String.Empty;
                String Email = String.Empty;

                String strSqlQueryForPhoneAndEmail = "select [dbo].[contact].[HomePhone], [dbo].[contact].[Email] from [dbo].[contact] where [dbo].[contact].[Individual_ID__c] = @IndividualId";

                SqlCommand cmdQueryForPhoneAndEmail = new SqlCommand(strSqlQueryForPhoneAndEmail, connSalesForce);
                cmdQueryForPhoneAndEmail.CommandType = CommandType.Text;

                cmdQueryForPhoneAndEmail.Parameters.AddWithValue("@IndividualId", WhoId);

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

                String strSqlQeuryForTaskSelected = "select [dbo].[tbl_user].[User_Name], [dbo].[tbl_task].[Subject], [dbo].[tbl_task].[DueDate], [dbo].[tbl_task].[RelatedToTableId], " +
                                                    "[dbo].[tbl_task].[whatid], [dbo].[tbl_task].[IndividualName], [dbo].[tbl_task].[whoid], " +
                                                    "[dbo].[tbl_task].[Comment], [dbo].[tbl_task].[Solution], [dbo].[tbl_task].[Status], [dbo].[tbl_task].[Priority], " +
                                                    "[dbo].[tbl_task].[IsReminderSet], [dbo].[tbl_task].[ReminderDateTime] " +
                                                    "from [dbo].[tbl_task] " +
                                                    "inner join [dbo].[tbl_user] on [dbo].[tbl_task].[AssignedTo] = [dbo].[tbl_user].[User_Id] " +
                                                    "where [dbo].[tbl_task].[id] = @TaskId";

                SqlCommand cmdQueryForTaskSelected = new SqlCommand(strSqlQeuryForTaskSelected, connRN);
                cmdQueryForTaskSelected.CommandType = CommandType.Text;

                cmdQueryForTaskSelected.Parameters.AddWithValue("@TaskId", nTaskId);

                if (connRN.State != ConnectionState.Closed)
                {
                    connRN.Close();
                    connRN.Open();
                }
                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                SqlDataReader rdrTaskToModify = cmdQueryForTaskSelected.ExecuteReader();
                if (rdrTaskToModify.HasRows)
                {
                    rdrTaskToModify.Read();
                    if (!rdrTaskToModify.IsDBNull(0)) txtTaskNameAssignedTo.Text = rdrTaskToModify.GetString(0);
                    else txtTaskNameAssignedTo.Text = String.Empty;
                    if (!rdrTaskToModify.IsDBNull(1)) txtTaskSubject.Text = rdrTaskToModify.GetString(1);
                    else txtTaskSubject.Text = String.Empty;
                    if (!rdrTaskToModify.IsDBNull(2)) dtpTaskDueDate.Text = rdrTaskToModify.GetDateTime(2).ToString("MM/dd/yyyy");
                    if (!rdrTaskToModify.IsDBNull(3)) comboTaskRelatedTo.SelectedIndex = rdrTaskToModify.GetInt16(3);
                    else comboTaskRelatedTo.SelectedIndex = -1;
                    if (!rdrTaskToModify.IsDBNull(4)) txtTaskRelatedTo.Text = rdrTaskToModify.GetString(4);
                    else txtTaskRelatedTo.Text = String.Empty;
                    if (!rdrTaskToModify.IsDBNull(5)) txtNameOnTask.Text = rdrTaskToModify.GetString(5);
                    else txtNameOnTask.Text = String.Empty;
                    if (!rdrTaskToModify.IsDBNull(6)) txtIndividualId.Text = rdrTaskToModify.GetString(6);
                    else txtIndividualId.Text = String.Empty;
                    if (!rdrTaskToModify.IsDBNull(7)) txtTaskComments.Text = rdrTaskToModify.GetString(7);
                    else txtTaskComments.Text = String.Empty;
                    if (!rdrTaskToModify.IsDBNull(8)) txtTaskSolution.Text = rdrTaskToModify.GetString(8);
                    else txtTaskSolution.Text = String.Empty;
                    if (!rdrTaskToModify.IsDBNull(9)) comboTaskStatus.SelectedIndex = rdrTaskToModify.GetByte(9);
                    else comboTaskStatus.SelectedIndex = -1;
                    if (!rdrTaskToModify.IsDBNull(10)) comboTaskPriority.SelectedIndex = rdrTaskToModify.GetByte(10);
                    else comboTaskPriority.SelectedIndex = -1;
                    if (!rdrTaskToModify.IsDBNull(11)) chkReminder.Checked = rdrTaskToModify.GetBoolean(11);
                    else chkReminder.Checked = false;
                    if (!rdrTaskToModify.IsDBNull(12))
                    {
                        dtpReminderDatePicker.Text = rdrTaskToModify.GetDateTime(12).ToString("MM/dd/yyyy");
                        String strTime = rdrTaskToModify.GetDateTime(12).ToString("h:mm tt");
                        SetReminderTime(strTime);
                    }
                }
                rdrTaskToModify.Close();
                if (connRN.State == ConnectionState.Open) connRN.Close();

                if (LoggedInuserInfo.UserRoleId == UserRole.FDStaff ||
                    LoggedInuserInfo.UserRoleId == UserRole.NPStaff ||
                    LoggedInuserInfo.UserRoleId == UserRole.RNStaff)
                {
                    txtTaskNameAssignedTo.ReadOnly = true;
                    txtTaskSubject.ReadOnly = true;
                    dtpTaskDueDate.Enabled = false;
                    comboTaskRelatedTo.Enabled = false;
                    txtTaskRelatedTo.ReadOnly = true;
                    txtNameOnTask.ReadOnly = true;
                    txtIndividualId.ReadOnly = true;
                    txtTaskComments.ReadOnly = true;
                    chkReminder.Enabled = false;
                    dtpReminderDatePicker.Enabled = false;
                    comboReminderTimePicker.Enabled = false;
                }

            }
            else if (taskMode == TaskMode.EditInDashboard)
            {

            }
            else if (taskMode == TaskMode.EditInCase)
            {
                comboTaskRelatedTo.Items.Add(RelatedToTable.Membership);
                comboTaskRelatedTo.Items.Add(RelatedToTable.Case);
                comboTaskRelatedTo.Items.Add(RelatedToTable.Illness);
                comboTaskRelatedTo.Items.Add(RelatedToTable.Incident);
                comboTaskRelatedTo.Items.Add(RelatedToTable.MedicalBill);
                comboTaskRelatedTo.Items.Add(RelatedToTable.Settlement);

                String strSqlQueryForTaskInfo = "select [dbo].[tbl_task_created_by].[User_Name], [dbo].[tbl_task_assigned_to].[User_Name], " +
                                                "[dbo].[tbl_task].[Subject], [dbo].[tbl_task].[Comment], " +
                                                "[dbo].[tbl_task].[DueDate], [dbo].[tbl_task].[RelatedToTableId], [dbo].[tbl_task].[whatid], [dbo].[tbl_task].[IndividualName], [dbo].[tbl_task].[whoid], " +
                                                "[dbo].[tbl_task].[Comment], [dbo].[tbl_task].[Solution], [dbo].[tbl_task].[Status], [dbo].[tbl_task].[Priority], " +
                                                "[dbo].[tbl_task].[ReminderDateTime], [dbo].[tbl_task].[IsReminderSet] " +
                                                "from [dbo].[tbl_task] " +
                                                "inner join [dbo].[tbl_task_created_by] on [dbo].[tbl_task].[CreatedById] = [dbo].[tbl_task_created_by].[User_Id] " +
                                                "inner join [dbo].[tbl_task_assigned_to] on [dbo].[tbl_task].[AssignedTo] = [dbo].[tbl_task_assigned_to].[User_Id] " +
                                                "where [dbo].[tbl_task].[id] = @TaskId";

                SqlCommand cmdQueryForTaskInfo = new SqlCommand(strSqlQueryForTaskInfo, connRN);
                cmdQueryForTaskInfo.CommandType = CommandType.Text;

                cmdQueryForTaskInfo.Parameters.AddWithValue("@TaskId", nTaskId.Value);

                if (connRN.State != ConnectionState.Closed)
                {
                    connRN.Close();
                    connRN.Open();
                }
                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                SqlDataReader rdrTaskToEdit = cmdQueryForTaskInfo.ExecuteReader();
                if (rdrTaskToEdit.HasRows)
                {
                    rdrTaskToEdit.Read();
                    if (!rdrTaskToEdit.IsDBNull(0)) txtTaskCreator.Text = rdrTaskToEdit.GetString(0);
                    else txtTaskCreator.Text = String.Empty;
                    if (!rdrTaskToEdit.IsDBNull(1)) txtTaskNameAssignedTo.Text = rdrTaskToEdit.GetString(1);
                    else txtTaskNameAssignedTo.Text = String.Empty;
                    if (!rdrTaskToEdit.IsDBNull(2)) txtTaskSubject.Text = rdrTaskToEdit.GetString(2);
                    else txtTaskSubject.Text = String.Empty;
                    if (!rdrTaskToEdit.IsDBNull(3)) txtTaskComments.Text = rdrTaskToEdit.GetString(3);
                    else txtTaskComments.Text = String.Empty;
                    if (!rdrTaskToEdit.IsDBNull(4))
                    {
                        dtpTaskDueDate.Checked = true;
                        dtpTaskDueDate.Value = DateTime.Parse(rdrTaskToEdit.GetDateTime(4).ToString("MM/dd/yyyy"));
                        dtpTaskDueDate.Text = rdrTaskToEdit.GetDateTime(4).ToString("MM/dd/yyyy");
                    }
                    else
                    {
                        dtpTaskDueDate.Enabled = false;
                        dtpTaskDueDate.Format = DateTimePickerFormat.Custom;
                        dtpTaskDueDate.CustomFormat = " ";
                    }
                    if (!rdrTaskToEdit.IsDBNull(5)) comboTaskRelatedTo.SelectedIndex = rdrTaskToEdit.GetInt16(5);
                    if (!rdrTaskToEdit.IsDBNull(6)) txtTaskRelatedTo.Text = rdrTaskToEdit.GetString(6);
                    else txtTaskRelatedTo.Text = String.Empty;
                    if (!rdrTaskToEdit.IsDBNull(7)) txtNameOnTask.Text = rdrTaskToEdit.GetString(7);
                    else txtNameOnTask.Text = String.Empty;
                    if (!rdrTaskToEdit.IsDBNull(8)) txtIndividualId.Text = rdrTaskToEdit.GetString(8);
                    else txtIndividualId.Text = String.Empty;
                    if (!rdrTaskToEdit.IsDBNull(9)) txtTaskComments.Text = rdrTaskToEdit.GetString(9);
                    else txtTaskComments.Text = String.Empty;
                    if (!rdrTaskToEdit.IsDBNull(10)) txtTaskSolution.Text = rdrTaskToEdit.GetString(10);
                    else txtTaskSolution.Text = String.Empty;
                    if (!rdrTaskToEdit.IsDBNull(11)) comboTaskStatus.SelectedIndex = rdrTaskToEdit.GetByte(11);
                    if (!rdrTaskToEdit.IsDBNull(12)) comboTaskPriority.SelectedIndex = rdrTaskToEdit.GetByte(12);
                    if (!rdrTaskToEdit.IsDBNull(13))
                    {
                        dtpReminderDatePicker.Checked = true;
                        dtpReminderDatePicker.Value = DateTime.Parse(rdrTaskToEdit.GetDateTime(13).ToString("MM/dd/yyyy"));
                        dtpReminderDatePicker.Text = rdrTaskToEdit.GetDateTime(13).ToString("MM/dd/yyyy");                           
                    }
                    else
                    {
                        dtpReminderDatePicker.Format = DateTimePickerFormat.Custom;
                        dtpReminderDatePicker.CustomFormat = " ";
                    }
                    if (!rdrTaskToEdit.IsDBNull(14)) chkReminder.Checked = rdrTaskToEdit.GetBoolean(14);
                }
                rdrTaskToEdit.Close();
                if (connRN.State != ConnectionState.Closed) connRN.Close();
                // 03/04/19 - begin here : Disable, enable and show controls for replying and forwarding
            }
        }

        private void SetReminderTime(String time)
        {
            switch(time)
            {
                case "12:00 AM":
                    comboReminderTimePicker.SelectedIndex = 0;
                    break;

                case "12:30 AM":
                    comboReminderTimePicker.SelectedIndex = 1;
                    break;

                case "1:00 AM":
                    comboReminderTimePicker.SelectedIndex = 2;
                    break;

                case "1:30 AM":
                    comboReminderTimePicker.SelectedIndex = 3;
                    break;

                case "2:00 AM":
                    comboReminderTimePicker.SelectedIndex = 4;
                    break;

                case "2:30 AM":
                    comboReminderTimePicker.SelectedIndex = 5;
                    break;

                case "3:00 AM":
                    comboReminderTimePicker.SelectedIndex = 6;
                    break;

                case "3:30 AM":
                    comboReminderTimePicker.SelectedIndex = 7;
                    break;

                case "4:00 AM":
                    comboReminderTimePicker.SelectedIndex = 8;
                    break;

                case "4:30 AM":
                    comboReminderTimePicker.SelectedIndex = 9;
                    break;

                case "5:00 AM":
                    comboReminderTimePicker.SelectedIndex = 10;
                    break;

                case "5:30 AM":
                    comboReminderTimePicker.SelectedIndex = 11;
                    break;

                case "6:00 AM":
                    comboReminderTimePicker.SelectedIndex = 12;
                    break;

                case "6:30 AM":
                    comboReminderTimePicker.SelectedIndex = 13;
                    break;

                case "7:00 AM":
                    comboReminderTimePicker.SelectedIndex = 14;
                    break;

                case "7:30 AM":
                    comboReminderTimePicker.SelectedIndex = 15;
                    break;

                case "8:00 AM":
                    comboReminderTimePicker.SelectedIndex = 16;
                    break;

                case "8:30 AM":
                    comboReminderTimePicker.SelectedIndex = 17;
                    break;

                case "9:00 AM":
                    comboReminderTimePicker.SelectedIndex = 18;
                    break;

                case "9:30 AM":
                    comboReminderTimePicker.SelectedIndex = 19;
                    break;

                case "10:00 AM":
                    comboReminderTimePicker.SelectedIndex = 20;
                    break;

                case "10:30 AM":
                    comboReminderTimePicker.SelectedIndex = 21;
                    break;

                case "11:00 AM":
                    comboReminderTimePicker.SelectedIndex = 22;
                    break;

                case "11:30 AM":
                    comboReminderTimePicker.SelectedIndex = 23;
                    break;

                case "12:00 PM":
                    comboReminderTimePicker.SelectedIndex = 24;
                    break;

                case "12:30 PM":
                    comboReminderTimePicker.SelectedIndex = 25;
                    break;

                case "1:00 PM":
                    comboReminderTimePicker.SelectedIndex = 26;
                    break;

                case "1:30 PM":
                    comboReminderTimePicker.SelectedIndex = 27;
                    break;

                case "2:00 PM":
                    comboReminderTimePicker.SelectedIndex = 28;
                    break;

                case "2:30 PM":
                    comboReminderTimePicker.SelectedIndex = 29;
                    break;

                case "3:00 PM":
                    comboReminderTimePicker.SelectedIndex = 30;
                    break;

                case "3:30 PM":
                    comboReminderTimePicker.SelectedIndex = 31;
                    break;

                case "4:00 PM":
                    comboReminderTimePicker.SelectedIndex = 32;
                    break;

                case "4:30 PM":
                    comboReminderTimePicker.SelectedIndex = 33;
                    break;

                case "5:00 PM":
                    comboReminderTimePicker.SelectedIndex = 34;
                    break;

                case "5:30 PM":
                    comboReminderTimePicker.SelectedIndex = 35;
                    break;

                case "6:00 PM":
                    comboReminderTimePicker.SelectedIndex = 36;
                    break;

                case "6:30 PM":
                    comboReminderTimePicker.SelectedIndex = 37;
                    break;

                case "7:00 PM":
                    comboReminderTimePicker.SelectedIndex = 38;
                    break;

                case "7:30 PM":
                    comboReminderTimePicker.SelectedIndex = 39;
                    break;

                case "8:00 PM":
                    comboReminderTimePicker.SelectedIndex = 40;
                    break;

                case "8:30 PM":
                    comboReminderTimePicker.SelectedIndex = 41;
                    break;

                case "9:00 PM":
                    comboReminderTimePicker.SelectedIndex = 42;
                    break;

                case "9:30 PM":
                    comboReminderTimePicker.SelectedIndex = 43;
                    break;

                case "10:00 PM":
                    comboReminderTimePicker.SelectedIndex = 44;
                    break;

                case "10:30 PM":
                    comboReminderTimePicker.SelectedIndex = 45;
                    break;

                case "11:00 PM":
                    comboReminderTimePicker.SelectedIndex = 46;
                    break;

                case "11:30 PM":
                    comboReminderTimePicker.SelectedIndex = 47;
                    break;
            }
        }

        private void btnSaveTask_Click(object sender, EventArgs e)
        {
            if (nTaskId == null)    // Create new task
            {
                if (txtTaskNameAssignedTo.Text.Trim() == String.Empty)
                {
                    MessageBox.Show("Please enter the Assigned to name: ");
                    return;
                }

                String NameAssignedTo = txtTaskNameAssignedTo.Text.Trim();

                List<String> lstNamesAssignedTo = NameAssignedTo.Split(';').ToList();
                lstNamesAssignedTo.RemoveAt(lstNamesAssignedTo.Count - 1);      // remove last empty element

                List<UserInfo> lstStaffAssignedTo = new List<UserInfo>();

                foreach (String name in lstNamesAssignedTo)
                {
                    String strSqlQueryForAssignedToInfo = "select [dbo].[tbl_user].[User_Id], [dbo].[tbl_user].[Department_Id] from [dbo].[tbl_user] where [dbo].[tbl_user].[User_Name] = @UserName";

                    SqlCommand cmdQueryForAssignedToInfo = new SqlCommand(strSqlQueryForAssignedToInfo, connRN);
                    cmdQueryForAssignedToInfo.CommandType = CommandType.Text;

                    cmdQueryForAssignedToInfo.Parameters.AddWithValue("@UserName", name);

                    if (connRN.State != ConnectionState.Closed)
                    {
                        connRN.Close();
                        connRN.Open();
                    }
                    else if (connRN.State == ConnectionState.Closed) connRN.Open();

                    SqlDataReader rdrAssignedTo = cmdQueryForAssignedToInfo.ExecuteReader();
                    if (rdrAssignedTo.HasRows)
                    {
                        rdrAssignedTo.Read();
                        UserInfo staffInfo = new UserInfo();
                        if (!rdrAssignedTo.IsDBNull(0)) staffInfo.UserId = rdrAssignedTo.GetInt16(0);
                        if (!rdrAssignedTo.IsDBNull(1)) staffInfo.departmentInfo.DepartmentId = (Department)rdrAssignedTo.GetInt16(1);
                        lstStaffAssignedTo.Add(staffInfo);
                    }
                    if (connRN.State == ConnectionState.Open) connRN.Close();
                }

                String Subject = txtTaskSubject.Text.Trim();
                DateTime DueDate = dtpTaskDueDate.Value;

                String RelatedTableName = comboTaskRelatedTo.SelectedItem.ToString();
                String WhatId = txtTaskRelatedTo.Text.Trim();         // Related Id
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

                    //Reminder = new DateTime(Int16.Parse(strDate.Substring(6, 4)),
                    //                        Int16.Parse(strDate.Substring(0, 2)),
                    //                        Int16.Parse(strDate.Substring(3, 2)),
                    //                        Int16.Parse(strTime.Substring(0, strTime.IndexOf(':')),
                    //                        Int16.Parse(strTime.Substring(strTime.IndexOf(':') + 1, 2)),
                    //                        0);

                    int Year = Int16.Parse(strDate.Substring(6, 4));
                    int Month = Int16.Parse(strDate.Substring(0, 2));
                    int Day = Int16.Parse(strDate.Substring(3, 2));
                    String Hour = strTime.Substring(0, strTime.IndexOf(':'));
                    String Minute = strTime.Substring(strTime.IndexOf(':') + 1, 2);

                    Reminder = new DateTime(Year, Month, Day, Int16.Parse(Hour), Int16.Parse(Minute), 0);
                   
                }

                Cursor.Current = Cursors.WaitCursor;
                Boolean bTaskSent = true;
                foreach (UserInfo staffInfo in lstStaffAssignedTo)
                {

                    String strSqlInsertIntoTask = "insert into [dbo].[tbl_task] ([dbo].[tbl_task].[whoid], [dbo].[tbl_task].[IndividualName], [dbo].[tbl_task].[whatid], [dbo].[tbl_task].[IsDeleted], " +
                                                  "[dbo].[tbl_task].[AssignedTo], [dbo].[tbl_task].[SendingDepartment], [dbo].[tbl_task].[ReceivingDepartment], " +
                                                  "[dbo].[tbl_task].[Subject], [dbo].[tbl_task].[DueDate], [dbo].[tbl_task].[RelatedToTableId], " +
                                                  "[dbo].[tbl_task].[CreateDate], [dbo].[tbl_task].[CreatedById], [dbo].[tbl_task].[LastModifiedDate], [dbo].[tbl_task].[LastModifiedById], " +
                                                  "[dbo].[tbl_task].[ActivityDate], " +
                                                  "[dbo].[tbl_task].[Comment], [dbo].[tbl_task].[Solution], [dbo].[tbl_task].[Status], [dbo].[tbl_task].[Priority], " +
                                                  "[dbo].[tbl_task].[IsClosed], [dbo].[tbl_task].[ReminderDateTime], [dbo].[tbl_task].[IsReminderSet], [dbo].[tbl_task].[IsArchived]) " +
                                                  "values (@WhoId, @IndividualName, @WhatId, 0, @AssignedTo, @SendingDepartment, @ReceivingDepartment, " +
                                                  "@Subject, @DueDate, @RelatedTo, @CreateDate, @CreatedById, " +
                                                  "@LastModifiedDate, @LastModifiedById, @ActivityDate, " +
                                                  "@Comment, @Solution, @Status, @Priority, @IsClosed, @ReminderDateTime, @IsReminderSet, @IsArchived)";

                    SqlCommand cmdInsertIntoTask = new SqlCommand(strSqlInsertIntoTask, connRN);
                    cmdInsertIntoTask.CommandType = CommandType.Text;

                    cmdInsertIntoTask.Parameters.AddWithValue("@WhoId", WhoId);
                    cmdInsertIntoTask.Parameters.AddWithValue("@IndividualName", WhoName);
                    cmdInsertIntoTask.Parameters.AddWithValue("@WhatId", WhatId);
                    //cmdInsertIntoTask.Parameters.AddWithValue("@AssignedTo", AssignedToStaffInfo.UserId);
                    cmdInsertIntoTask.Parameters.AddWithValue("@AssignedTo", staffInfo.UserId);
                    cmdInsertIntoTask.Parameters.AddWithValue("@SendingDepartment", LoggedInuserInfo.departmentInfo.DepartmentId);
                    //cmdInsertIntoTask.Parameters.AddWithValue("@ReceivingDepartment", AssignedToStaffInfo.departmentInfo.DepartmentId);
                    cmdInsertIntoTask.Parameters.AddWithValue("@ReceivingDepartment", staffInfo.departmentInfo.DepartmentId);
                    cmdInsertIntoTask.Parameters.AddWithValue("@Subject", Subject);
                    cmdInsertIntoTask.Parameters.AddWithValue("@DueDate", DueDate);
                    cmdInsertIntoTask.Parameters.AddWithValue("@RelatedTo", comboTaskRelatedTo.SelectedIndex);
                    cmdInsertIntoTask.Parameters.AddWithValue("@CreateDate", DateTime.Now);
                    cmdInsertIntoTask.Parameters.AddWithValue("@CreatedById", LoggedInuserInfo.UserId);
                    cmdInsertIntoTask.Parameters.AddWithValue("@LastModifiedDate", DateTime.Today);
                    cmdInsertIntoTask.Parameters.AddWithValue("@LastModifiedById", LoggedInuserInfo.UserId);
                    cmdInsertIntoTask.Parameters.AddWithValue("@ActivityDate", DateTime.Today);
                    cmdInsertIntoTask.Parameters.AddWithValue("@Comment", Comment);
                    cmdInsertIntoTask.Parameters.AddWithValue("@Solution", Solution);
                    cmdInsertIntoTask.Parameters.AddWithValue("@Status", ts);
                    cmdInsertIntoTask.Parameters.AddWithValue("@Priority", tp);
                    cmdInsertIntoTask.Parameters.AddWithValue("@IsClosed", 0);
                    if (chkReminder.Checked) cmdInsertIntoTask.Parameters.AddWithValue("@ReminderDateTime", Reminder);
                    else cmdInsertIntoTask.Parameters.AddWithValue("@ReminderDateTime", DBNull.Value);
                    cmdInsertIntoTask.Parameters.AddWithValue("@IsReminderSet", chkReminder.Checked);
                    cmdInsertIntoTask.Parameters.AddWithValue("@IsArchived", 0);

                    if (connRN.State != ConnectionState.Closed)
                    {
                        connRN.Close();
                        connRN.Open();
                    }
                    else if (connRN.State == ConnectionState.Closed) connRN.Open();
                    int nRowInserted = cmdInsertIntoTask.ExecuteNonQuery();
                    if (connRN.State == ConnectionState.Open) connRN.Close();

                    if (nRowInserted == 0)
                    {
                        bTaskSent = false;
                    }

                    //if (nRowInserted == 1)
                    //{
                    //    DialogResult = DialogResult.OK;
                    //    MessageBox.Show("The task has been created.");
                    //    return;
                    //}
                    //else if (nRowInserted == 0)
                    //{
                    //    DialogResult = DialogResult.No;
                    //    MessageBox.Show("The task has not been created.");
                    //    return;
                    //}
                }

                Cursor.Current = Cursors.Default;
                if (bTaskSent)
                {
                    MessageBox.Show("Tasks have been sent to assignees.", "Info");
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else if (!bTaskSent)
                {
                    MessageBox.Show("At least one of task has not been sent.", "Error");
                    DialogResult = DialogResult.No;
                    Close();
                }
            }
            else // modify the task
            {
                if (LoggedInuserInfo.UserRoleId == UserRole.FDStaff ||
                    LoggedInuserInfo.UserRoleId == UserRole.NPStaff ||
                    LoggedInuserInfo.UserRoleId == UserRole.RNStaff)
                {
                    String strSqlUpdateTask = "update [dbo].[tbl_task] set [dbo].[tbl_task].[Solution] = @Solution, [dbo].[tbl_task].[Status] = @Status, [dbo].[tbl_task].[Priority] = @Priority " +
                                              "where [dbo].[tbl_task].[id] = @TaskId";

                    SqlCommand cmdUpdateTask = new SqlCommand(strSqlUpdateTask, connRN);
                    cmdUpdateTask.CommandType = CommandType.Text;

                    cmdUpdateTask.Parameters.AddWithValue("@Solution", txtTaskSolution.Text.Trim());
                    cmdUpdateTask.Parameters.AddWithValue("@Status", (TaskStatus)comboTaskStatus.SelectedIndex);
                    cmdUpdateTask.Parameters.AddWithValue("@Priority", (TaskPriority)comboTaskPriority.SelectedIndex);
                    cmdUpdateTask.Parameters.AddWithValue("@TaskId", nTaskId);

                    if (connRN.State != ConnectionState.Closed)
                    {
                        connRN.Close();
                        connRN.Open();
                    }
                    else if (connRN.State == ConnectionState.Closed) connRN.Open();
                    int nUpdatedRow = cmdUpdateTask.ExecuteNonQuery();
                    if (connRN.State == ConnectionState.Open) connRN.Close();

                    if (nUpdatedRow == 1)
                    {
                        DialogResult = DialogResult.OK;
                        MessageBox.Show("The task id: " + nTaskId.ToString() + " has been updated.");
                        Close();
                    }
                }

                if (LoggedInuserInfo.UserRoleId == UserRole.SuperAdmin ||
                    LoggedInuserInfo.UserRoleId == UserRole.Administrator ||
                    LoggedInuserInfo.UserRoleId == UserRole.FDManager ||
                    LoggedInuserInfo.UserRoleId == UserRole.NPManager ||
                    LoggedInuserInfo.UserRoleId == UserRole.RNManager)
                {
                    String NameAssignedTo = txtTaskNameAssignedTo.Text.Trim();
                    String strSqlQueryForUserInfo = "select [dbo].[tbl_user].[User_Id], [dbo].[tbl_user].[User_Role_Id], [dbo].[tbl_user].[Department_Id] from [dbo].[tbl_user] " +
                                                          "where [dbo].[tbl_user].[User_Name] = @UserName";

                    SqlCommand cmdQueryForUserInfo = new SqlCommand(strSqlQueryForUserInfo, connRN);
                    cmdQueryForUserInfo.CommandType = CommandType.Text;

                    cmdQueryForUserInfo.Parameters.AddWithValue("@UserName", NameAssignedTo);
                    UserInfo userInfo = new UserInfo();

                    if (connRN.State != ConnectionState.Closed)
                    {
                        connRN.Close();
                        connRN.Open();
                    }
                    else if (connRN.State == ConnectionState.Closed) connRN.Open();
                    SqlDataReader rdrUserInfo = cmdQueryForUserInfo.ExecuteReader();
                    if (rdrUserInfo.HasRows)
                    {
                        if (rdrUserInfo.Read())
                        {
                            if (!rdrUserInfo.IsDBNull(0)) userInfo.UserId = rdrUserInfo.GetInt16(0);
                            if (!rdrUserInfo.IsDBNull(1)) userInfo.UserRoleId = (UserRole)rdrUserInfo.GetInt16(1);
                            if (!rdrUserInfo.IsDBNull(2)) userInfo.departmentInfo.DepartmentId = (Department)rdrUserInfo.GetInt16(2);
                        }
                    }
                    rdrUserInfo.Close();
                    if (connRN.State == ConnectionState.Open) connRN.Close();

                    String Subject = txtTaskSubject.Text.Trim();
                    DateTime DueDate = dtpTaskDueDate.Value;
                    RelatedToTable relatedToTable = (RelatedToTable)comboTaskRelatedTo.SelectedIndex;
                    String WhatId = txtTaskRelatedTo.Text.Trim();
                    String WhoId = txtIndividualId.Text.Trim();
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
                        TmpTime = strTmpTime.Substring(strTmpTime.Length - 2, 2);

                        if (strTmpTime.Substring(strTmpTime.Length - 2, 2) == "PM")
                        {
                            int nTime = Int16.Parse(strTmpTime.Substring(0, strTmpTime.IndexOf(":")));
                            nTime += 12;
                            strTime = nTime.ToString() + ":" + strTmpTime.Substring(strTmpTime.IndexOf(":") + 1, 2) + "AM";
                        }
                        else strTime = strTmpTime;

                        int Year = Int16.Parse(strDate.Substring(6, 4));
                        int Month = Int16.Parse(strDate.Substring(0, 2));
                        int Day = Int16.Parse(strDate.Substring(3, 2));
                        String Hour = strTime.Substring(0, strTime.IndexOf(':'));
                        String Minute = strTime.Substring(strTime.IndexOf(':') + 1, 2);

                        Reminder = new DateTime(Year, Month, Day, Int16.Parse(Hour), Int16.Parse(Minute), 0);
                    }

                    String strSqlUpdateTask = "update [dbo].[tbl_task] set [dbo].[tbl_task].[AssignedTo] = @AssignedTo, " +
                                                "[dbo].[tbl_task].[Subject] = @TaskSubject, " +
                                                "[dbo].[tbl_task].[DueDate] = @TaskDueDate, " +
                                                "[dbo].[tbl_task].[RelatedToTableId] = @RelatedToTableId, " +
                                                "[dbo].[tbl_task].[whatid] = @WhatId, " +
                                                "[dbo].[tbl_task].[IndividualName] = @IndividualName, " +
                                                "[dbo].[tbl_task].[whoid] = @WhoId, " +
                                                "[dbo].[tbl_task].[Comment] = @TaskComment, " +
                                                "[dbo].[tbl_task].[Solution] = @TaskSolution, " +
                                                "[dbo].[tbl_task].[Status] = @TaskStatus, " +
                                                "[dbo].[tbl_task].[Priority] = @TaskPriority, " +
                                                "[dbo].[tbl_task].[IsReminderSet] = @ReminderSet, " +
                                                "[dbo].[tbl_task].[ReminderDateTime] = @ReminderDateTime " +
                                                "where [dbo].[tbl_task].[id] = @TaskId";

                    SqlCommand cmdUpdateTask = new SqlCommand(strSqlUpdateTask, connRN);
                    cmdUpdateTask.CommandType = CommandType.Text;

                    cmdUpdateTask.Parameters.AddWithValue("@AssignedTo", userInfo.UserId);
                    cmdUpdateTask.Parameters.AddWithValue("@TaskSubject", Subject);
                    cmdUpdateTask.Parameters.AddWithValue("@TaskDueDate", DueDate);
                    cmdUpdateTask.Parameters.AddWithValue("@RelatedToTableId", (int)relatedToTable);
                    cmdUpdateTask.Parameters.AddWithValue("@WhatId", WhatId);
                    cmdUpdateTask.Parameters.AddWithValue("@IndividualName", txtNameOnTask.Text.Trim());
                    cmdUpdateTask.Parameters.AddWithValue("@WhoId", WhoId);
                    cmdUpdateTask.Parameters.AddWithValue("@TaskComment", Comment);
                    cmdUpdateTask.Parameters.AddWithValue("@TaskSolution", Solution);
                    cmdUpdateTask.Parameters.AddWithValue("@TaskStatus", (int)ts);
                    cmdUpdateTask.Parameters.AddWithValue("@TaskPriority", (int)tp);
                    cmdUpdateTask.Parameters.AddWithValue("@ReminderSet", chkReminder.Checked);
                    if (chkReminder.Checked) cmdUpdateTask.Parameters.AddWithValue("@ReminderDateTime", Reminder);
                    else cmdUpdateTask.Parameters.AddWithValue("@ReminderDateTime", DBNull.Value);
                    cmdUpdateTask.Parameters.AddWithValue("@TaskId", nTaskId);

                    if (connRN.State != ConnectionState.Closed)
                    {
                        connRN.Close();
                        connRN.Open();
                    }
                    else if (connRN.State == ConnectionState.Closed) connRN.Open();
                    int nRowUpdated = cmdUpdateTask.ExecuteNonQuery();
                    if (connRN.State == ConnectionState.Open) connRN.Close();

                    if (nRowUpdated == 1)
                    {
                        DialogResult = DialogResult.OK;
                        MessageBox.Show("The Task id: " + nTaskId + " has been updated successfully.");
                        Close();
                    }
                }
            }
            Close();
        }

        private void btnCancelTask_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAssignedTo_Click(object sender, EventArgs e)
        {
            frmAssignedTo frmAssignedTo = new frmAssignedTo();

            if (frmAssignedTo.ShowDialog() == DialogResult.OK)
            {
                txtTaskNameAssignedTo.Text = frmAssignedTo.AssignedToList;
            }
        }

        private void btnReplyTask_Click(object sender, EventArgs e)
        {

        }

        private void btnForward_Click(object sender, EventArgs e)
        {

        }
    }
}
