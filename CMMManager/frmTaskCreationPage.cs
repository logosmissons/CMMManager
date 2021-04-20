using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using System.Linq;

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
        private UserInfo LoggedInUserInfo;
        private UserInfo AssignedToStaffInfo;
        private TaskUserInfo LoggedInTaskUserInfo;        

        private List<UserInfo> lstUserInfo;

        private List<TaskStatusInfo> lstTaskStatusInfo;

        private int? nOriginalTask_Id;
        private int nTaskReplySender_Id;
        private int nTaskReplyAssigned_to_Id;

        private Boolean bTaskSentToITManager;
        private Boolean bTaskSentToMSManager;
        private Boolean bTaskSentToNPManager;
        private Boolean bTaskSentToRNManager;
        private Boolean bTaskSentToFDManager;

        private Boolean bTaskSentToManagerOfSender;
        private Boolean bTaskSentToITManagerOfITSender;
        private Boolean bTaskSentToMSManagerOfMSSender;
        private Boolean bTaskSentToNPManagerOfNPSender;
        private Boolean bTaskSentToRNManagerOfRNSender;
        private Boolean bTaskSentToFDManagerOfFDSender;

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
            LoggedInUserInfo = new UserInfo();
            AssignedToStaffInfo = new UserInfo();
            lstUserInfo = new List<UserInfo>();

            connStringRN = @"Data Source=cmm-2019data\CMM; Initial Catalog=RN_DB;Integrated Security=True; Max Pool Size=200; MultipleActiveResultSets=True";
            //connStringRN = @"Data Source=cmm-2019data\CMM; Initial Catalog=RN_DB;User ID=sa;Password=Yny00516; Max Pool Size=200; MultipleActiveResultSets=True";
            connStringSalesForce = @"Data Source=cmm-2019data\CMM; Initial Catalog=SalesForce; Integrated Security=True; Max Pool Size=200; MultipleActiveResultSets=True";
            //connStringSalesForce = @"Data Source=cmm-2019data\CMM; Initial Catalog=SalesForce; User ID=sa;Password=Yny00516; Max Pool Size=200; MultipleActiveResultSets=True";

            connRN = new SqlConnection(connStringRN);
            connSalesForce = new SqlConnection(connStringSalesForce);

            sbComment = new StringBuilder();
            sbSolution = new StringBuilder();

            //private Boolean bTaskSentToMSManager;
            //private Boolean bTaskSentToNPgManager;
            //private Boolean bTaskSentToRNManager;
            //private Boolean bTaskSentToFDManager;

            bTaskSentToITManager = false;
            bTaskSentToFDManager = false;
            bTaskSentToMSManager = false;
            bTaskSentToNPManager = false;
            bTaskSentToRNManager = false;

            bTaskSentToITManagerOfITSender = false;
            bTaskSentToMSManagerOfMSSender = false;
            bTaskSentToNPManagerOfNPSender = false;
            bTaskSentToRNManagerOfRNSender = false;
            bTaskSentToFDManagerOfFDSender = false;

    }

        public frmTaskCreationPage(int task_id,
                                   TaskMode mode)
        {
            InitializeComponent();

            nTaskId = task_id;
            taskMode = mode;

            TaskCreatorInfo = new UserInfo();
            LoggedInUserInfo = new UserInfo();
            AssignedToStaffInfo = new UserInfo();
            lstUserInfo = new List<UserInfo>();

            connStringRN = @"Data Source=cmm-2019data\CMM; Initial Catalog=RN_DB;Integrated Security=True; Max Pool Size=200; MultipleActiveResultSets=True";
            connStringSalesForce = @"Data Source=cmm-2019data\CMM; Initial Catalog=SalesForce; Integrated Security=True; Max Pool Size=200; MultipleActiveResultSets=True";

            //connStringRN = @"Data Source=cmm-2019data\CMM; Initial Catalog=RN_DB;User ID=sa;Password=Yny00516; Max Pool Size=200; MultipleActiveResultSets=True";
            //connStringSalesForce = @"Data Source=cmm-2019data\CMM; Initial Catalog=SalesForce; User ID=sa;Password=Yny00516; Max Pool Size=200; MultipleActiveResultSets=True";

            connRN = new SqlConnection(connStringRN);
            connSalesForce = new SqlConnection(connStringSalesForce);

            sbComment = new StringBuilder();
            sbSolution = new StringBuilder();

            bTaskSentToITManager = false;
            bTaskSentToFDManager = false;
            bTaskSentToMSManager = false;
            bTaskSentToNPManager = false;
            bTaskSentToRNManager = false;

            bTaskSentToITManagerOfITSender = false;
            bTaskSentToMSManagerOfMSSender = false;
            bTaskSentToNPManagerOfNPSender = false;
            bTaskSentToRNManagerOfRNSender = false;
            bTaskSentToFDManagerOfFDSender = false;
        }

        public frmTaskCreationPage(int original_task_id,
                                   int creator_id,
                                   int assigned_to_id,
                                   int loggedInUserId,
                                   TaskMode mode)
        {
            InitializeComponent();

            //nTaskId = task_id;
            taskMode = mode;

            nOriginalTask_Id = original_task_id;
            nTaskReplySender_Id = creator_id;
            nTaskReplyAssigned_to_Id = assigned_to_id;

            TaskCreatorInfo = new UserInfo();
            LoggedInUserInfo = new UserInfo();
            AssignedToStaffInfo = new UserInfo();
            lstUserInfo = new List<UserInfo>();

            LoggedInUserInfo.UserId = loggedInUserId;

            connStringRN = @"Data Source=cmm-2019data\CMM; Initial Catalog=RN_DB;Integrated Security=True; Max Pool Size=200; MultipleActiveResultSets=True";
            connStringSalesForce = @"Data Source=cmm-2019data\CMM; Initial Catalog=SalesForce; Integrated Security=True; Max Pool Size=200; MultipleActiveResultSets=True";

            //connStringRN = @"Data Source=cmm-2019data\CMM; Initial Catalog=RN_DB;User ID=sa;Password=Yny00516; Max Pool Size=200; MultipleActiveResultSets=True";
            //connStringSalesForce = @"Data Source=cmm-2019data\CMM; Initial Catalog=SalesForce; User ID=sa;Password=Yny00516; Max Pool Size=200; MultipleActiveResultSets=True";

            connRN = new SqlConnection(connStringRN);
            connSalesForce = new SqlConnection(connStringSalesForce);

            sbComment = new StringBuilder();
            sbSolution = new StringBuilder();

            bTaskSentToITManager = false;
            bTaskSentToFDManager = false;
            bTaskSentToMSManager = false;
            bTaskSentToNPManager = false;
            bTaskSentToRNManager = false;

            bTaskSentToITManagerOfITSender = false;
            bTaskSentToMSManagerOfMSSender = false;
            bTaskSentToNPManagerOfNPSender = false;
            bTaskSentToRNManagerOfRNSender = false;
            bTaskSentToFDManagerOfFDSender = false;
        }

        public frmTaskCreationPage(int task_id,
                           String individual_id,
                           TaskMode mode)
        {
            InitializeComponent();

            nTaskId = task_id;
            WhoId = individual_id;
            taskMode = mode;

            TaskCreatorInfo = new UserInfo();
            LoggedInUserInfo = new UserInfo();
            AssignedToStaffInfo = new UserInfo();
            lstUserInfo = new List<UserInfo>();

            connStringRN = @"Data Source=cmm-2019data\CMM; Initial Catalog=RN_DB;Integrated Security=True; Max Pool Size=200; MultipleActiveResultSets=True";
            connStringSalesForce = @"Data Source=cmm-2019data\CMM; Initial Catalog=SalesForce; Integrated Security=True; Max Pool Size=200; MultipleActiveResultSets=True";

            //connStringRN = @"Data Source=cmm-2019data\CMM; Initial Catalog=RN_DB;User ID=sa;Password=Yny00516; Max Pool Size=200; MultipleActiveResultSets=True";
            //connStringSalesForce = @"Data Source=cmm-2019data\CMM; Initial Catalog=SalesForce; User ID=sa;Password=Yny00516; Max Pool Size=200; MultipleActiveResultSets=True";

            connRN = new SqlConnection(connStringRN);
            connSalesForce = new SqlConnection(connStringSalesForce);

            sbComment = new StringBuilder();
            sbSolution = new StringBuilder();

            bTaskSentToITManager = false;
            bTaskSentToFDManager = false;
            bTaskSentToMSManager = false;
            bTaskSentToNPManager = false;
            bTaskSentToRNManager = false;

            bTaskSentToITManagerOfITSender = false;
            bTaskSentToMSManagerOfMSSender = false;
            bTaskSentToNPManagerOfNPSender = false;
            bTaskSentToRNManagerOfRNSender = false;
            bTaskSentToFDManagerOfFDSender = false;
        }

        //public frmTaskCreationPage(int task_id,
        //                           String individual_id,
        //                           int loggedInUserId,
        //                           String loggedInUserName,
        //                           UserRole loggedInUserRoleId,
        //                           Department loggedInUserDepartment,
        //                           TaskMode mode)
        //{
        //    InitializeComponent();

        //    nTaskId = task_id;
        //    WhoId = individual_id;
        //    taskMode = mode;

        //    TaskCreatorInfo = new UserInfo();
        //    LoggedInUserInfo = new UserInfo();
        //    AssignedToStaffInfo = new UserInfo();
        //    lstUserInfo = new List<UserInfo>();

        //    LoggedInUserInfo.UserId = loggedInUserId;
        //    LoggedInUserInfo.UserName = loggedInUserName;
        //    LoggedInUserInfo.UserRoleId = loggedInUserRoleId;
        //    LoggedInUserInfo.departmentInfo.DepartmentId = loggedInUserDepartment;

        //    connStringRN = @"Data Source=cmm-2019data\CMM; Initial Catalog=RN_DB;Integrated Security=True; Max Pool Size=200; MultipleActiveResultSets=True";
        //    connStringSalesForce = @"Data Source=cmm-2019data\CMM; Initial Catalog=SalesForce; Integrated Security=True; Max Pool Size=200; MultipleActiveResultSets=True";

        //    connRN = new SqlConnection(connStringRN);
        //    connSalesForce = new SqlConnection(connStringSalesForce);

        //    sbComment = new StringBuilder();
        //    sbSolution = new StringBuilder();


        //}

        //public frmTaskCreationPage(int task_id,
        //                    String IndividualId,
        //                    int loggedInUserId,
        //                    String loggedInUserName,
        //                    UserRole loggedInUserRoleId,
        //                    Department loggedInUserDepartment,
        //                    TaskMode mode)
        //{
        //    InitializeComponent();

        //    nTaskId = task_id;
        //    taskMode = mode;
        //    WhoId = IndividualId;

        //    LoggedInUserInfo = new UserInfo();

        //    LoggedInUserInfo.UserId = loggedInUserId;
        //    LoggedInUserInfo.UserName = loggedInUserName;
        //    LoggedInUserInfo.UserRoleId = loggedInUserRoleId;
        //    LoggedInUserInfo.departmentInfo.DepartmentId = loggedInUserDepartment;

        //    AssignedToStaffInfo = new UserInfo();

        //    lstUserInfo = new List<UserInfo>();

        //    connStringRN = @"Data Source=cmm-2019data\CMM; Initial Catalog=RN_DB;Integrated Security=True; Max Pool Size=200; MultipleActiveResultSets=True";
        //    connStringSalesForce = @"Data Source=cmm-2019data\CMM; Initial Catalog=SalesForce; Integrated Security=True; Max Pool Size=200; MultipleActiveResultSets=True";

        //    connRN = new SqlConnection(connStringRN);
        //    connSalesForce = new SqlConnection(connStringSalesForce);
        //}



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

            LoggedInUserInfo = new UserInfo();

            LoggedInUserInfo.UserId = loggedInUserId;
            LoggedInUserInfo.UserName = loggedInUserName;
            LoggedInUserInfo.UserRoleId = loggedInUserRoleId;
            LoggedInUserInfo.departmentInfo.DepartmentId = loggedInUserDepartment;

            AssignedToStaffInfo = new UserInfo();

            lstUserInfo = new List<UserInfo>();

            connStringRN = @"Data Source=cmm-2019data\CMM; Initial Catalog=RN_DB;Integrated Security=True; Max Pool Size=200; MultipleActiveResultSets=True";
            connStringSalesForce = @"Data Source=cmm-2019data\CMM; Initial Catalog=SalesForce; Integrated Security=True; Max Pool Size=200; MultipleActiveResultSets=True";

            //connStringRN = @"Data Source=cmm-2019data\CMM; Initial Catalog=RN_DB;User ID=sa;Password=Yny00516; Max Pool Size=200; MultipleActiveResultSets=True";
            //connStringSalesForce = @"Data Source=cmm-2019data\CMM; Initial Catalog=SalesForce; User ID=sa;Password=Yny00516; Max Pool Size=200; MultipleActiveResultSets=True";

            connRN = new SqlConnection(connStringRN);
            connSalesForce = new SqlConnection(connStringSalesForce);

            sbComment = new StringBuilder();           
            sbComment.AppendLine(email_case);
            sbComment.AppendLine(email_subject);
            sbComment.AppendLine(email_body);
            sbSolution = new StringBuilder();

            bTaskSentToITManager = false;
            bTaskSentToFDManager = false;
            bTaskSentToMSManager = false;
            bTaskSentToNPManager = false;
            bTaskSentToRNManager = false;

            bTaskSentToITManagerOfITSender = false;
            bTaskSentToMSManagerOfMSSender = false;
            bTaskSentToNPManagerOfNPSender = false;
            bTaskSentToRNManagerOfRNSender = false;
            bTaskSentToFDManagerOfFDSender = false;
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

            LoggedInUserInfo = new UserInfo();

            LoggedInUserInfo.UserId = loggedInUserId;
            LoggedInUserInfo.UserName = loggedInUserName;
            LoggedInUserInfo.UserRoleId = loggedInUserRoleId;
            LoggedInUserInfo.departmentInfo.DepartmentId = loggedInUserDepartment;

            AssignedToStaffInfo = new UserInfo();

            InitializeComponent();
            lstUserInfo = new List<UserInfo>();

            connStringRN = @"Data Source=cmm-2019data\CMM; Initial Catalog=RN_DB;Integrated Security=True; Max Pool Size=200; MultipleActiveResultSets=True";
            connStringSalesForce = @"Data Source=cmm-2019data\CMM; Initial Catalog=SalesForce; Integrated Security=True; Max Pool Size=200; MultipleActiveResultSets=True";

            //connStringRN = @"Data Source=cmm-2019data\CMM; Initial Catalog=RN_DB;User ID=sa;Password=Yny00516; Max Pool Size=200; MultipleActiveResultSets=True";
            //connStringSalesForce = @"Data Source=cmm-2019data\CMM; Initial Catalog=SalesForce; User ID=sa;Password=Yny00516; Max Pool Size=200; MultipleActiveResultSets=True";

            connRN = new SqlConnection(connStringRN);
            connSalesForce = new SqlConnection(connStringSalesForce);

            sbComment = new StringBuilder();
            sbSolution = new StringBuilder();

            bTaskSentToITManager = false;
            bTaskSentToFDManager = false;
            bTaskSentToMSManager = false;
            bTaskSentToNPManager = false;
            bTaskSentToRNManager = false;

            bTaskSentToITManagerOfITSender = false;
            bTaskSentToMSManagerOfMSSender = false;
            bTaskSentToNPManagerOfNPSender = false;
            bTaskSentToRNManagerOfRNSender = false;
            bTaskSentToFDManagerOfFDSender = false;

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

            LoggedInUserInfo = new UserInfo();

            LoggedInUserInfo.UserId = loggedInUserId;
            LoggedInUserInfo.UserName = loggedInUserName;
            LoggedInUserInfo.UserRoleId = loggedInUserRoleId;
            LoggedInUserInfo.departmentInfo.DepartmentId = loggedInUserDepartment;

            AssignedToStaffInfo = new UserInfo();

            InitializeComponent();
            lstUserInfo = new List<UserInfo>();

            connStringRN = @"Data Source=cmm-2019data\CMM; Initial Catalog=RN_DB;Integrated Security=True; Max Pool Size=200; MultipleActiveResultSets=True";
            connStringSalesForce = @"Data Source=cmm-2019data\CMM; Initial Catalog=SalesForce; Integrated Security=True; Max Pool Size=200; MultipleActiveResultSets=True";

            //connStringRN = @"Data Source=cmm-2019data\CMM; Initial Catalog=RN_DB;User ID=sa;Password=Yny00516; Max Pool Size=200; MultipleActiveResultSets=True";
            //connStringSalesForce = @"Data Source=cmm-2019data\CMM; Initial Catalog=SalesForce; User ID=sa;Password=Yny00516; Max Pool Size=200; MultipleActiveResultSets=True";

            connRN = new SqlConnection(connStringRN);
            connSalesForce = new SqlConnection(connStringSalesForce);

            sbComment = new StringBuilder();
            sbSolution = new StringBuilder();

            bTaskSentToITManager = false;
            bTaskSentToFDManager = false;
            bTaskSentToMSManager = false;
            bTaskSentToNPManager = false;
            bTaskSentToRNManager = false;

            bTaskSentToITManagerOfITSender = false;
            bTaskSentToMSManagerOfMSSender = false;
            bTaskSentToNPManagerOfNPSender = false;
            bTaskSentToRNManagerOfRNSender = false;
            bTaskSentToFDManagerOfFDSender = false;

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

            LoggedInUserInfo = new UserInfo();

            LoggedInUserInfo.UserId = loggedInUserId;
            LoggedInUserInfo.UserName = loggedInUserName;
            LoggedInUserInfo.UserRoleId = loggedInUserRoleId;
            LoggedInUserInfo.departmentInfo.DepartmentId = loggedInUserDepartment;

            AssignedToStaffInfo = new UserInfo();

            InitializeComponent();
            lstUserInfo = new List<UserInfo>();

            connStringRN = @"Data Source=cmm-2019data\CMM; Initial Catalog=RN_DB;Integrated Security=True; Max Pool Size=200; MultipleActiveResultSets=True";
            connStringSalesForce = @"Data Source=cmm-2019data\CMM; Initial Catalog=SalesForce; Integrated Security=True; Max Pool Size=200; MultipleActiveResultSets=True";

            //connStringRN = @"Data Source=cmm-2019data\CMM; Initial Catalog=RN_DB;User ID=sa;Password=Yny00516; Max Pool Size=200; MultipleActiveResultSets=True";
            //connStringSalesForce = @"Data Source=cmm-2019data\CMM; Initial Catalog=SalesForce; User ID=sa;Password=Yny00516; Max Pool Size=200; MultipleActiveResultSets=True";

            connRN = new SqlConnection(connStringRN);
            connSalesForce = new SqlConnection(connStringSalesForce);

            sbComment = new StringBuilder();
            sbSolution = new StringBuilder();

            bTaskSentToITManager = false;
            bTaskSentToFDManager = false;
            bTaskSentToMSManager = false;
            bTaskSentToNPManager = false;
            bTaskSentToRNManager = false;

            bTaskSentToITManagerOfITSender = false;
            bTaskSentToMSManagerOfMSSender = false;
            bTaskSentToNPManagerOfNPSender = false;
            bTaskSentToRNManagerOfRNSender = false;
            bTaskSentToFDManagerOfFDSender = false;

        }
        public frmTaskCreationPage(int task_id, 
                                   String individual_id, 
                                   int login_user_id, 
                                   String login_user_name,
                                   String login_user_email,
                                   UserRole login_user_role_id, 
                                   Department login_user_department, 
                                   TaskMode mode)
        {

            taskMode = mode;
            nTaskId = task_id;
            WhoId = individual_id;

            LoggedInUserInfo = new UserInfo();
            lstUserInfo = new List<UserInfo>();
            InitializeComponent();

            LoggedInUserInfo.UserId = login_user_id;
            LoggedInUserInfo.UserName = login_user_name;
            LoggedInUserInfo.UserEmail = login_user_email;
            LoggedInUserInfo.UserRoleId = login_user_role_id;
            LoggedInUserInfo.departmentInfo.DepartmentId = login_user_department;

            connStringRN = @"Data Source=cmm-2019data\CMM; Initial Catalog=RN_DB; Integrated Security=True; Max Pool Size=200; MultipleActiveResultSets=True";
            connStringSalesForce = @"Data Source=cmm-2019data\CMM; Initial Catalog=SalesForce; Integrated Security=True; Max Pool Size=200; MultipleActiveResultSets=True";

            //connStringRN = @"Data Source=cmm-2019data\CMM; Initial Catalog=RN_DB; User ID=sa;Password=Yny00516; Max Pool Size=200; MultipleActiveResultSets=True";
            //connStringSalesForce = @"Data Source=cmm-2019data\CMM; Initial Catalog=SalesForce;User ID=sa;Password=Yny00516; Max Pool Size=200; MultipleActiveResultSets=True";

            connRN = new SqlConnection(connStringRN);
            connSalesForce = new SqlConnection(connStringSalesForce);

            sbComment = new StringBuilder();
            sbSolution = new StringBuilder();

            bTaskSentToITManager = false;
            bTaskSentToFDManager = false;
            bTaskSentToMSManager = false;
            bTaskSentToNPManager = false;
            bTaskSentToRNManager = false;

            bTaskSentToITManagerOfITSender = false;
            bTaskSentToMSManagerOfMSSender = false;
            bTaskSentToNPManagerOfNPSender = false;
            bTaskSentToRNManagerOfRNSender = false;
            bTaskSentToFDManagerOfFDSender = false;
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

            //TaskCreatorInfo = new UserInfo();

            //TaskCreatorInfo.UserId = creator_id;
            //TaskCreatorInfo.UserName = creator_name;

            LoggedInUserInfo = new UserInfo();
            lstUserInfo = new List<UserInfo>();
            InitializeComponent();

            LoggedInUserInfo.UserId = login_user_id;
            LoggedInUserInfo.UserName = login_user_name;
            LoggedInUserInfo.UserRoleId = login_user_role_id;
            LoggedInUserInfo.departmentInfo.DepartmentId = login_user_department;

            connStringRN = @"Data Source=cmm-2019data\CMM; Initial Catalog=RN_DB; Integrated Security=True; Max Pool Size=200; MultipleActiveResultSets=True";
            connStringSalesForce = @"Data Source=cmm-2019data\CMM; Initial Catalog=SalesForce; Integrated Security=True; Max Pool Size=200; MultipleActiveResultSets=True";

            //connStringRN = @"Data Source=cmm-2019data\CMM; Initial Catalog=RN_DB; User ID=sa;Password=Yny00516; Max Pool Size=200; MultipleActiveResultSets=True";
            //connStringSalesForce = @"Data Source=cmm-2019data\CMM; Initial Catalog=SalesForce; User ID=sa;Password=Yny00516; Max Pool Size=200; MultipleActiveResultSets=True";

            connRN = new SqlConnection(connStringRN);
            connSalesForce = new SqlConnection(connStringSalesForce);

            sbComment = new StringBuilder();
            sbSolution = new StringBuilder();

            bTaskSentToITManager = false;
            bTaskSentToFDManager = false;
            bTaskSentToMSManager = false;
            bTaskSentToNPManager = false;
            bTaskSentToRNManager = false;

            bTaskSentToITManagerOfITSender = false;
            bTaskSentToMSManagerOfMSSender = false;
            bTaskSentToNPManagerOfNPSender = false;
            bTaskSentToRNManagerOfRNSender = false;
            bTaskSentToFDManagerOfFDSender = false;
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

            comboTaskRelatedTo.Items.Clear();
            String strSqlQueryForRelatedToTable = "select [dbo].[tbl_task_related_to_code].[TaskRelatedToValue] from [dbo].[tbl_task_related_to_code]";

            SqlCommand cmdQueryForRelatedToTable = new SqlCommand(strSqlQueryForRelatedToTable, connRN);
            cmdQueryForRelatedToTable.CommandType = CommandType.Text;

            if (connRN.State != ConnectionState.Closed)
            {
                connRN.Close();
                connRN.Open();
            }
            else if (connRN.State == ConnectionState.Closed) connRN.Open();
            SqlDataReader rdrRelatedToTable = cmdQueryForRelatedToTable.ExecuteReader();
            if (rdrRelatedToTable.HasRows)
            {
                while (rdrRelatedToTable.Read())
                {
                    if (!rdrRelatedToTable.IsDBNull(0)) comboTaskRelatedTo.Items.Add(rdrRelatedToTable.GetString(0));
                }
            }
            rdrRelatedToTable.Close();
            if (connRN.State != ConnectionState.Closed) connRN.Close();

            comboTaskStatus.Items.Clear();

            lstTaskStatusInfo = new List<TaskStatusInfo>();
            List<TaskStatusInfo> lstTaskStatusInfoUnsorted = new List<TaskStatusInfo>();

            String strSqlQueryForTaskStatus = "select [dbo].[tbl_task_status_code].[TaskStatusCode], [dbo].[tbl_task_status_code].[TaskStatusValue] " +
                                              "from [dbo].[tbl_task_status_code] " +
                                              "where ([dbo].[tbl_task_status_code].[IsDeleted] = 0 or [dbo].[tbl_task_status_code].[IsDeleted] IS NULL)";

            SqlCommand cmdQueryForTaskStatus = new SqlCommand(strSqlQueryForTaskStatus, connRN);
            cmdQueryForTaskStatus.CommandType = CommandType.Text;

            if (connRN.State != ConnectionState.Closed)
            {
                connRN.Close();
                connRN.Open();
            }
            else if (connRN.State == ConnectionState.Closed) connRN.Open();
            SqlDataReader rdrTaskStatus = cmdQueryForTaskStatus.ExecuteReader();
            if (rdrTaskStatus.HasRows)
            {
                while (rdrTaskStatus.Read())
                {
                    //if (!rdrTaskStatus.IsDBNull(0)) comboTaskStatus.Items.Add(rdrTaskStatus.GetString(0));
                    TaskStatusInfo info = new TaskStatusInfo();
                    if (!rdrTaskStatus.IsDBNull(0)) info.nTaskStatusCode = rdrTaskStatus.GetInt16(0);
                    else info.nTaskStatusCode = null;
                    if (!rdrTaskStatus.IsDBNull(1))
                    {
                        switch (rdrTaskStatus.GetString(1))
                        {
                            case "Not Started":
                                info.TaskStatusValue = TaskStatus.NotStarted;
                                break;
                            case "In Progress":
                                info.TaskStatusValue = TaskStatus.InProgress;
                                break;
                            case "Completed":
                                info.TaskStatusValue = TaskStatus.Completed;
                                break;
                            case "Checked":
                                info.TaskStatusValue = TaskStatus.Checked;
                                break;
                        }
                    }
                    else info.TaskStatusValue = null;

                    lstTaskStatusInfoUnsorted.Add(info);
                    //lstTaskStatusInfo.Add(info);
                }
            }
            rdrTaskStatus.Close();
            if (connRN.State != ConnectionState.Closed) connRN.Close();

            lstTaskStatusInfo = lstTaskStatusInfoUnsorted.OrderBy(info => info.nTaskStatusCode).ToList();

            for (int i = 0; i < lstTaskStatusInfo.Count; i++)
            {
                lstTaskStatusInfo[i].nTaskStatusSelectedIndex = i;
            }

            foreach (TaskStatusInfo info in lstTaskStatusInfo)
            {
                comboTaskStatus.Items.Add(info.TaskStatusValue.ToString());
            }

            comboTaskPriority.Items.Clear();
            String strSqlQueryForTaskPriority = "select [dbo].[tbl_task_priority_code].[TaskPriorityValue] from [dbo].[tbl_task_priority_code]";

            SqlCommand cmdQueryForTaskPriority = new SqlCommand(strSqlQueryForTaskPriority, connRN);
            cmdQueryForTaskPriority.CommandType = CommandType.Text;

            if (connRN.State != ConnectionState.Closed)
            {
                connRN.Close();
                connRN.Open();
            }
            else if (connRN.State == ConnectionState.Closed) connRN.Open();
            SqlDataReader rdrTaskPriority = cmdQueryForTaskPriority.ExecuteReader();
            if (rdrTaskPriority.HasRows)
            {
                while (rdrTaskPriority.Read())
                {
                    if (!rdrTaskPriority.IsDBNull(0)) comboTaskPriority.Items.Add(rdrTaskPriority.GetString(0));
                }
            }
            rdrTaskPriority.Close();
            if (connRN.State != ConnectionState.Closed) connRN.Close();

        }

        private void FillTaskFormWithTaskInfo()
        {


            String strSqlQueryForTaskInfo = "select [dbo].[tbl_task_created_by].[User_Name], [dbo].[tbl_task_assigned_to].[User_Name], " +
                                            "[dbo].[tbl_task].[whoid], [dbo].[tbl_task].[IndividualName], " +
                                            "[dbo].[tbl_task].[DueDate], [dbo].[tbl_task].[RelatedToTableId], [dbo].[tbl_task].[whatid], " +
                                            "[dbo].[tbl_task].[Subject], [dbo].[tbl_task].[Comment], [dbo].[tbl_task].[Solution], " +
                                            "[dbo].[tbl_task].[Status], [dbo].[tbl_task].[Priority], [dbo].[tbl_task].[PhoneNo], [dbo].[tbl_task].[Email], " +
                                            "[dbo].[tbl_task].[IsReminderSet], [dbo].[tbl_task].[ReminderDateTime] " +
                                            "from [dbo].[tbl_task] " +
                                            "inner join [dbo].[tbl_task_created_by] on [dbo].[tbl_task].[CreatedById] = [dbo].[tbl_task_created_by].[User_Id] " +
                                            "inner join [dbo].[tbl_task_assigned_to] on [dbo].[tbl_task].[AssignedTo] = [dbo].[tbl_task_assigned_to].[User_Id] " +
                                            "where [dbo].[tbl_task].[id] = @TaskId";

            SqlCommand cmdQueryForTaskInfo = new SqlCommand(strSqlQueryForTaskInfo, connRN);
            cmdQueryForTaskInfo.CommandType = CommandType.Text;

            cmdQueryForTaskInfo.Parameters.AddWithValue("@TaskId", nTaskId);

            if (connRN.State != ConnectionState.Closed)
            {
                connRN.Close();
                connRN.Open();
            }
            else if (connRN.State == ConnectionState.Closed) connRN.Open();
            SqlDataReader rdrTaskInfo = cmdQueryForTaskInfo.ExecuteReader();
            if (rdrTaskInfo.HasRows)
            {
                rdrTaskInfo.Read();
                if (!rdrTaskInfo.IsDBNull(0)) txtTaskCreator.Text = rdrTaskInfo.GetString(0);
                else txtTaskCreator.Text = String.Empty;
                if (!rdrTaskInfo.IsDBNull(1)) txtTaskNameAssignedTo.Text = rdrTaskInfo.GetString(1);
                else txtTaskNameAssignedTo.Text = String.Empty;
                if (!rdrTaskInfo.IsDBNull(2)) txtIndividualId.Text = rdrTaskInfo.GetString(2);
                else txtIndividualId.Text = String.Empty;
                if (!rdrTaskInfo.IsDBNull(3)) txtNameOnTask.Text = rdrTaskInfo.GetString(3);
                else txtNameOnTask.Text = String.Empty;
                if (!rdrTaskInfo.IsDBNull(4)) dtpTaskDueDate.Text = rdrTaskInfo.GetDateTime(4).ToString("MM/dd/yyyy");
                else dtpTaskDueDate.Text = String.Empty;
                if (!rdrTaskInfo.IsDBNull(5)) comboTaskRelatedTo.SelectedIndex = rdrTaskInfo.GetInt16(5);
                if (!rdrTaskInfo.IsDBNull(6)) txtTaskRelatedTo.Text = rdrTaskInfo.GetString(6);
                else txtTaskRelatedTo.Text = String.Empty;
                if (!rdrTaskInfo.IsDBNull(7)) txtTaskSubject.Text = rdrTaskInfo.GetString(7);
                else txtTaskSubject.Text = String.Empty;
                if (!rdrTaskInfo.IsDBNull(8)) txtTaskComments.Text = rdrTaskInfo.GetString(8);
                else txtTaskComments.Text = String.Empty;
                if (!rdrTaskInfo.IsDBNull(9)) txtTaskSolution.Text = rdrTaskInfo.GetString(9);
                else txtTaskSolution.Text = String.Empty;
                if (!rdrTaskInfo.IsDBNull(10))
                {
                    //comboTaskStatus.SelectedIndex = rdrTaskInfo.GetByte(10);
                    for (int i = 0; i < lstTaskStatusInfo.Count; i++)
                    {
                        if (rdrTaskInfo.GetByte(10) < 3)
                        {
                            comboTaskStatus.SelectedIndex = rdrTaskInfo.GetByte(10);
                        }
                        else
                        {
                            comboTaskStatus.SelectedIndex = rdrTaskInfo.GetByte(10) - 3;
                        }
                    }
                }
                if (!rdrTaskInfo.IsDBNull(11)) comboTaskPriority.SelectedIndex = rdrTaskInfo.GetByte(11);
                if (!rdrTaskInfo.IsDBNull(12)) txtTaskPhone.Text = rdrTaskInfo.GetString(12);
                else txtTaskPhone.Text = String.Empty;
                if (!rdrTaskInfo.IsDBNull(13)) txtTaskEmail.Text = rdrTaskInfo.GetString(13);
                else txtTaskEmail.Text = String.Empty;
                if (!rdrTaskInfo.IsDBNull(14)) chkReminder.Checked = rdrTaskInfo.GetBoolean(14);
                else chkReminder.Checked = false;
                if (!rdrTaskInfo.IsDBNull(15)) dtpReminderDatePicker.Value = rdrTaskInfo.GetDateTime(15);
                if (!rdrTaskInfo.IsDBNull(15)) comboReminderTimePicker.Text = rdrTaskInfo.GetDateTime(15).ToString("hh:mm tt");
                else comboReminderTimePicker.Text = String.Empty;
                
            }
            rdrTaskInfo.Close();
            if (connRN.State != ConnectionState.Closed) connRN.Close();
        }


        private void frmTaskCreationPage_Load(object sender, EventArgs e)
        {
            if (taskMode == TaskMode.AddNew)
            {
                nTaskId = null;
                //lstUserInfo.Clear();
                InitializeTaskForm();

                txtTaskCreator.Text = LoggedInUserInfo.UserName;

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

                btnReplyTask.Enabled = false;
                btnForward.Enabled = false;
                btnSave.Enabled = false;
            }
            else if (taskMode == TaskMode.EditInMedBill)
            {
                //lstUserInfo.Clear();
                InitializeTaskForm();

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

                if (LoggedInUserInfo.UserRoleId == UserRole.FDStaff ||
                    LoggedInUserInfo.UserRoleId == UserRole.NPStaff ||
                    LoggedInUserInfo.UserRoleId == UserRole.RNStaff)
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
            else if (taskMode == TaskMode.EditInRNManagerDashboard)
            {
                InitializeTaskForm();
                FillTaskFormWithTaskInfo();
                //String strSqlQueryForTaskInfo = "select [dbo].[tbl_task_created_by].[User_Name], [dbo].[tbl_task_assigned_to].[User_Name], " +
                //                                "[dbo].[tbl_task].[whoid], [dbo].[tbl_task].[IndividualName], " +
                //                                "[dbo].[tbl_task].[DueDate], [dbo].[tbl_task].[RelatedToTableId], [dbo].[tbl_task].[whatid], " +
                //                                "[dbo].[tbl_task].[Subject], [dbo].[tbl_task].[Comment], [dbo].[tbl_task].[Solution], " +
                //                                "[dbo].[tbl_task].[Status], [dbo].[tbl_task].[Priority], [dbo].[tbl_task].[PhoneNo], [dbo].[tbl_task].[Email], " +
                //                                "[dbo].[tbl_task].[IsReminderSet], [dbo].[tbl_task].[ReminderDateTime] " +
                //                                "from [dbo].[tbl_task] " +
                //                                "inner join [dbo].[tbl_task_created_by] on [dbo].[tbl_task].[CreatedById] = [dbo].[tbl_task_created_by].[User_Id] " +
                //                                "inner join [dbo].[tbl_task_assigned_to] on [dbo].[tbl_task].[AssignedTo] = [dbo].[tbl_task_assigned_to].[User_Id] " +
                //                                "where [dbo].[tbl_task].[id] = @TaskId";

                //SqlCommand cmdQueryForTaskInfo = new SqlCommand(strSqlQueryForTaskInfo, connRN);
                //cmdQueryForTaskInfo.CommandType = CommandType.Text;

                //cmdQueryForTaskInfo.Parameters.AddWithValue("@TaskId", nTaskId);

                //if (connRN.State != ConnectionState.Closed)
                //{
                //    connRN.Close();
                //    connRN.Open();
                //}
                //else if (connRN.State == ConnectionState.Closed) connRN.Open();
                //SqlDataReader rdrTaskInfo = cmdQueryForTaskInfo.ExecuteReader();
                //if (rdrTaskInfo.HasRows)
                //{
                //    rdrTaskInfo.Read();
                //    if (!rdrTaskInfo.IsDBNull(0)) txtTaskCreator.Text = rdrTaskInfo.GetString(0);
                //    else txtTaskCreator.Text = String.Empty;
                //    if (!rdrTaskInfo.IsDBNull(1)) txtTaskNameAssignedTo.Text = rdrTaskInfo.GetString(1);
                //    else txtTaskNameAssignedTo.Text = String.Empty;
                //    if (!rdrTaskInfo.IsDBNull(2)) txtIndividualId.Text = rdrTaskInfo.GetString(2);
                //    else txtIndividualId.Text = String.Empty;
                //    if (!rdrTaskInfo.IsDBNull(3)) txtNameOnTask.Text = rdrTaskInfo.GetString(3);
                //    else txtNameOnTask.Text = String.Empty;
                //    if (!rdrTaskInfo.IsDBNull(4)) dtpTaskDueDate.Text = rdrTaskInfo.GetDateTime(4).ToString("MM/dd/yyyy");
                //    else dtpTaskDueDate.Text = String.Empty;
                //    if (!rdrTaskInfo.IsDBNull(5)) comboTaskRelatedTo.SelectedIndex = rdrTaskInfo.GetInt16(5);
                //    if (!rdrTaskInfo.IsDBNull(6)) txtTaskRelatedTo.Text = rdrTaskInfo.GetString(6);
                //    else txtTaskRelatedTo.Text = String.Empty;
                //    if (!rdrTaskInfo.IsDBNull(7)) txtTaskSubject.Text = rdrTaskInfo.GetString(7);
                //    else txtTaskSubject.Text = String.Empty;
                //    if (!rdrTaskInfo.IsDBNull(8)) txtTaskComments.Text = rdrTaskInfo.GetString(8);
                //    else txtTaskComments.Text = String.Empty;
                //    if (!rdrTaskInfo.IsDBNull(9)) txtTaskSolution.Text = rdrTaskInfo.GetString(9);
                //    else txtTaskSolution.Text = String.Empty;
                //    if (!rdrTaskInfo.IsDBNull(10)) comboTaskStatus.SelectedIndex = rdrTaskInfo.GetByte(10);
                //    if (!rdrTaskInfo.IsDBNull(11)) comboTaskPriority.SelectedIndex = rdrTaskInfo.GetByte(11);
                //    if (!rdrTaskInfo.IsDBNull(12)) txtTaskPhone.Text = rdrTaskInfo.GetString(12);
                //    else txtTaskPhone.Text = String.Empty;
                //    if (!rdrTaskInfo.IsDBNull(13)) txtTaskEmail.Text = rdrTaskInfo.GetString(13);
                //    else txtTaskEmail.Text = String.Empty;
                //    if (!rdrTaskInfo.IsDBNull(14)) chkReminder.Checked = rdrTaskInfo.GetBoolean(14);
                //    else chkReminder.Checked = false;
                //    if (!rdrTaskInfo.IsDBNull(15)) comboReminderTimePicker.Text = rdrTaskInfo.GetDateTime(15).ToString("MM/dd/yyyy");
                //    else comboReminderTimePicker.Text = String.Empty;
                //}
                //rdrTaskInfo.Close();
                //if (connRN.State != ConnectionState.Closed) connRN.Close();

                btnSaveTask.Enabled = false;
                btnReplyTask.Enabled = true;
                btnForward.Enabled = true;
                btnSave.Enabled = true;
            }
            else if (taskMode == TaskMode.EditInRNStaffDashboard)
            {
                InitializeTaskForm();
                FillTaskFormWithTaskInfo();

                //String strSqlQueryForTaskInfo = "select [dbo].[tbl_task_created_by].[User_Name], [dbo].[tbl_task_assigned_to].[User_Name], " +
                //                                "[dbo].[tbl_task].[whoid], [dbo].[tbl_task].[IndividualName], " +
                //                                "[dbo].[tbl_task].[DueDate], [dbo].[tbl_task].[RelatedToTableId], [dbo].[tbl_task].[whatid], " +
                //                                "[dbo].[tbl_task].[Subject], [dbo].[tbl_task].[Comment], [dbo].[tbl_task].[Solution], " +
                //                                "[dbo].[tbl_task].[Status], [dbo].[tbl_task].[Priority], [dbo].[tbl_task].[PhoneNo], [dbo].[tbl_task].[Email], " +
                //                                "[dbo].[tbl_task].[IsReminderSet], [dbo].[tbl_task].[ReminderDateTime] " +
                //                                "from [dbo].[tbl_task] " +
                //                                "inner join [dbo].[tbl_task_created_by] on [dbo].[tbl_task].[CreatedById] = [dbo].[tbl_task_created_by].[User_Id] " +
                //                                "inner join [dbo].[tbl_task_assigned_to] on [dbo].[tbl_task].[AssignedTo] = [dbo].[tbl_task_assigned_to].[User_Id] " +
                //                                "where [dbo].[tbl_task].[id] = @TaskId";

                //SqlCommand cmdQueryForTaskInfo = new SqlCommand(strSqlQueryForTaskInfo, connRN);
                //cmdQueryForTaskInfo.CommandType = CommandType.Text;

                //cmdQueryForTaskInfo.Parameters.AddWithValue("@TaskId", nTaskId);

                //if (connRN.State != ConnectionState.Closed)
                //{
                //    connRN.Close();
                //    connRN.Open();
                //}
                //else if (connRN.State == ConnectionState.Closed) connRN.Open();
                //SqlDataReader rdrTaskInfo = cmdQueryForTaskInfo.ExecuteReader();
                //if (rdrTaskInfo.HasRows)
                //{
                //    rdrTaskInfo.Read();
                //    if (!rdrTaskInfo.IsDBNull(0)) txtTaskCreator.Text = rdrTaskInfo.GetString(0);
                //    else txtTaskCreator.Text = String.Empty;
                //    if (!rdrTaskInfo.IsDBNull(1)) txtTaskNameAssignedTo.Text = rdrTaskInfo.GetString(1);
                //    else txtTaskNameAssignedTo.Text = String.Empty;
                //    if (!rdrTaskInfo.IsDBNull(2)) txtIndividualId.Text = rdrTaskInfo.GetString(2);
                //    else txtIndividualId.Text = String.Empty;
                //    if (!rdrTaskInfo.IsDBNull(3)) txtNameOnTask.Text = rdrTaskInfo.GetString(3);
                //    else txtNameOnTask.Text = String.Empty;
                //    if (!rdrTaskInfo.IsDBNull(4)) dtpTaskDueDate.Text = rdrTaskInfo.GetDateTime(4).ToString("MM/dd/yyyy");
                //    else dtpTaskDueDate.Text = String.Empty;
                //    if (!rdrTaskInfo.IsDBNull(5)) comboTaskRelatedTo.SelectedIndex = rdrTaskInfo.GetInt16(5);
                //    if (!rdrTaskInfo.IsDBNull(6)) txtTaskRelatedTo.Text = rdrTaskInfo.GetString(6);
                //    else txtTaskRelatedTo.Text = String.Empty;
                //    if (!rdrTaskInfo.IsDBNull(7)) txtTaskSubject.Text = rdrTaskInfo.GetString(7);
                //    else txtTaskSubject.Text = String.Empty;
                //    if (!rdrTaskInfo.IsDBNull(8)) txtTaskComments.Text = rdrTaskInfo.GetString(8);
                //    else txtTaskComments.Text = String.Empty;
                //    if (!rdrTaskInfo.IsDBNull(9)) txtTaskSolution.Text = rdrTaskInfo.GetString(9);
                //    else txtTaskSolution.Text = String.Empty;
                //    if (!rdrTaskInfo.IsDBNull(10)) comboTaskStatus.SelectedIndex = rdrTaskInfo.GetByte(10);
                //    if (!rdrTaskInfo.IsDBNull(11)) comboTaskPriority.SelectedIndex = rdrTaskInfo.GetByte(11);
                //    if (!rdrTaskInfo.IsDBNull(12)) txtTaskPhone.Text = rdrTaskInfo.GetString(12);
                //    else txtTaskPhone.Text = String.Empty;
                //    if (!rdrTaskInfo.IsDBNull(13)) txtTaskEmail.Text = rdrTaskInfo.GetString(13);
                //    else txtTaskEmail.Text = String.Empty;
                //    if (!rdrTaskInfo.IsDBNull(14)) chkReminder.Checked = rdrTaskInfo.GetBoolean(14);
                //    else chkReminder.Checked = false;
                //    if (!rdrTaskInfo.IsDBNull(15)) comboReminderTimePicker.Text = rdrTaskInfo.GetDateTime(15).ToString("MM/dd/yyyy");
                //    else comboReminderTimePicker.Text = String.Empty;
                //}
                //rdrTaskInfo.Close();
                //if (connRN.State != ConnectionState.Closed) connRN.Close();

                btnSaveTask.Enabled = false;
                btnReplyTask.Enabled = true;
                btnForward.Enabled = true;
                btnSave.Enabled = true;
            }
            else if (taskMode == TaskMode.EditInNPManagerDashboard)
            {
                InitializeTaskForm();
                FillTaskFormWithTaskInfo();

                //String strSqlQueryForTaskInfo = "select [dbo].[tbl_task_created_by].[User_Name], [dbo].[tbl_task_assigned_to].[User_Name], " +
                //                                "[dbo].[tbl_task].[whoid], [dbo].[tbl_task].[IndividualName], " +
                //                                "[dbo].[tbl_task].[DueDate], [dbo].[tbl_task].[RelatedToTableId], [dbo].[tbl_task].[whatid], " +
                //                                "[dbo].[tbl_task].[Subject], [dbo].[tbl_task].[Comment], [dbo].[tbl_task].[Solution], " +
                //                                "[dbo].[tbl_task].[Status], [dbo].[tbl_task].[Priority], [dbo].[tbl_task].[PhoneNo], [dbo].[tbl_task].[Email], " +
                //                                "[dbo].[tbl_task].[IsReminderSet], [dbo].[tbl_task].[ReminderDateTime] " +
                //                                "from [dbo].[tbl_task] " +
                //                                "inner join [dbo].[tbl_task_created_by] on [dbo].[tbl_task].[CreatedById] = [dbo].[tbl_task_created_by].[User_Id] " +
                //                                "inner join [dbo].[tbl_task_assigned_to] on [dbo].[tbl_task].[AssignedTo] = [dbo].[tbl_task_assigned_to].[User_Id] " +
                //                                "where [dbo].[tbl_task].[id] = @TaskId";

                //SqlCommand cmdQueryForTaskInfo = new SqlCommand(strSqlQueryForTaskInfo, connRN);
                //cmdQueryForTaskInfo.CommandType = CommandType.Text;

                //cmdQueryForTaskInfo.Parameters.AddWithValue("@TaskId", nTaskId);

                //if (connRN.State != ConnectionState.Closed)
                //{
                //    connRN.Close();
                //    connRN.Open();
                //}
                //else if (connRN.State == ConnectionState.Closed) connRN.Open();
                //SqlDataReader rdrTaskInfo = cmdQueryForTaskInfo.ExecuteReader();
                //if (rdrTaskInfo.HasRows)
                //{
                //    rdrTaskInfo.Read();
                //    if (!rdrTaskInfo.IsDBNull(0)) txtTaskCreator.Text = rdrTaskInfo.GetString(0);
                //    else txtTaskCreator.Text = String.Empty;
                //    if (!rdrTaskInfo.IsDBNull(1)) txtTaskNameAssignedTo.Text = rdrTaskInfo.GetString(1);
                //    else txtTaskNameAssignedTo.Text = String.Empty;
                //    if (!rdrTaskInfo.IsDBNull(2)) txtIndividualId.Text = rdrTaskInfo.GetString(2);
                //    else txtIndividualId.Text = String.Empty;
                //    if (!rdrTaskInfo.IsDBNull(3)) txtNameOnTask.Text = rdrTaskInfo.GetString(3);
                //    else txtNameOnTask.Text = String.Empty;
                //    if (!rdrTaskInfo.IsDBNull(4)) dtpTaskDueDate.Text = rdrTaskInfo.GetDateTime(4).ToString("MM/dd/yyyy");
                //    else dtpTaskDueDate.Text = String.Empty;
                //    if (!rdrTaskInfo.IsDBNull(5)) comboTaskRelatedTo.SelectedIndex = rdrTaskInfo.GetInt16(5);
                //    if (!rdrTaskInfo.IsDBNull(6)) txtTaskRelatedTo.Text = rdrTaskInfo.GetString(6);
                //    else txtTaskRelatedTo.Text = String.Empty;
                //    if (!rdrTaskInfo.IsDBNull(7)) txtTaskSubject.Text = rdrTaskInfo.GetString(7);
                //    else txtTaskSubject.Text = String.Empty;
                //    if (!rdrTaskInfo.IsDBNull(8)) txtTaskComments.Text = rdrTaskInfo.GetString(8);
                //    else txtTaskComments.Text = String.Empty;
                //    if (!rdrTaskInfo.IsDBNull(9)) txtTaskSolution.Text = rdrTaskInfo.GetString(9);
                //    else txtTaskSolution.Text = String.Empty;
                //    if (!rdrTaskInfo.IsDBNull(10)) comboTaskStatus.SelectedIndex = rdrTaskInfo.GetByte(10);
                //    if (!rdrTaskInfo.IsDBNull(11)) comboTaskPriority.SelectedIndex = rdrTaskInfo.GetByte(11);
                //    if (!rdrTaskInfo.IsDBNull(12)) txtTaskPhone.Text = rdrTaskInfo.GetString(12);
                //    else txtTaskPhone.Text = String.Empty;
                //    if (!rdrTaskInfo.IsDBNull(13)) txtTaskEmail.Text = rdrTaskInfo.GetString(13);
                //    else txtTaskEmail.Text = String.Empty;
                //    if (!rdrTaskInfo.IsDBNull(14)) chkReminder.Checked = rdrTaskInfo.GetBoolean(14);
                //    else chkReminder.Checked = false;
                //    if (!rdrTaskInfo.IsDBNull(15)) comboReminderTimePicker.Text = rdrTaskInfo.GetDateTime(15).ToString("MM/dd/yyyy");
                //    else comboReminderTimePicker.Text = String.Empty;
                //}
                //rdrTaskInfo.Close();
                //if (connRN.State != ConnectionState.Closed) connRN.Close();

                btnSaveTask.Enabled = false;
                btnReplyTask.Enabled = true;
                btnForward.Enabled = true;
                btnSave.Enabled = true;
            }
            else if (taskMode == TaskMode.EditInNPStaffDashboard)
            {

                InitializeTaskForm();
                FillTaskFormWithTaskInfo();

                //String strSqlQueryForTaskInfo = "select [dbo].[tbl_task_created_by].[User_Name], [dbo].[tbl_task_assigned_to].[User_Name], " +
                //                                "[dbo].[tbl_task].[whoid], [dbo].[tbl_task].[IndividualName], " +
                //                                "[dbo].[tbl_task].[DueDate], [dbo].[tbl_task].[RelatedToTableId], [dbo].[tbl_task].[whatid], " +
                //                                "[dbo].[tbl_task].[Subject], [dbo].[tbl_task].[Comment], [dbo].[tbl_task].[Solution], " +
                //                                "[dbo].[tbl_task].[Status], [dbo].[tbl_task].[Priority], [dbo].[tbl_task].[PhoneNo], [dbo].[tbl_task].[Email], " +
                //                                "[dbo].[tbl_task].[IsReminderSet], [dbo].[tbl_task].[ReminderDateTime] " +
                //                                "from [dbo].[tbl_task] " +
                //                                "inner join [dbo].[tbl_task_created_by] on [dbo].[tbl_task].[CreatedById] = [dbo].[tbl_task_created_by].[User_Id] " +
                //                                "inner join [dbo].[tbl_task_assigned_to] on [dbo].[tbl_task].[AssignedTo] = [dbo].[tbl_task_assigned_to].[User_Id] " +
                //                                "where [dbo].[tbl_task].[id] = @TaskId";

                //SqlCommand cmdQueryForTaskInfo = new SqlCommand(strSqlQueryForTaskInfo, connRN);
                //cmdQueryForTaskInfo.CommandType = CommandType.Text;

                //cmdQueryForTaskInfo.Parameters.AddWithValue("@TaskId", nTaskId);

                //if (connRN.State != ConnectionState.Closed)
                //{
                //    connRN.Close();
                //    connRN.Open();
                //}
                //else if (connRN.State == ConnectionState.Closed) connRN.Open();
                //SqlDataReader rdrTaskInfo = cmdQueryForTaskInfo.ExecuteReader();
                //if (rdrTaskInfo.HasRows)
                //{
                //    rdrTaskInfo.Read();
                //    if (!rdrTaskInfo.IsDBNull(0)) txtTaskCreator.Text = rdrTaskInfo.GetString(0);
                //    else txtTaskCreator.Text = String.Empty;
                //    if (!rdrTaskInfo.IsDBNull(1)) txtTaskNameAssignedTo.Text = rdrTaskInfo.GetString(1);
                //    else txtTaskNameAssignedTo.Text = String.Empty;
                //    if (!rdrTaskInfo.IsDBNull(2)) txtIndividualId.Text = rdrTaskInfo.GetString(2);
                //    else txtIndividualId.Text = String.Empty;
                //    if (!rdrTaskInfo.IsDBNull(3)) txtNameOnTask.Text = rdrTaskInfo.GetString(3);
                //    else txtNameOnTask.Text = String.Empty;
                //    if (!rdrTaskInfo.IsDBNull(4)) dtpTaskDueDate.Text = rdrTaskInfo.GetDateTime(4).ToString("MM/dd/yyyy");
                //    else dtpTaskDueDate.Text = String.Empty;
                //    if (!rdrTaskInfo.IsDBNull(5)) comboTaskRelatedTo.SelectedIndex = rdrTaskInfo.GetInt16(5);
                //    if (!rdrTaskInfo.IsDBNull(6)) txtTaskRelatedTo.Text = rdrTaskInfo.GetString(6);
                //    else txtTaskRelatedTo.Text = String.Empty;
                //    if (!rdrTaskInfo.IsDBNull(7)) txtTaskSubject.Text = rdrTaskInfo.GetString(7);
                //    else txtTaskSubject.Text = String.Empty;
                //    if (!rdrTaskInfo.IsDBNull(8)) txtTaskComments.Text = rdrTaskInfo.GetString(8);
                //    else txtTaskComments.Text = String.Empty;
                //    if (!rdrTaskInfo.IsDBNull(9)) txtTaskSolution.Text = rdrTaskInfo.GetString(9);
                //    else txtTaskSolution.Text = String.Empty;
                //    if (!rdrTaskInfo.IsDBNull(10)) comboTaskStatus.SelectedIndex = rdrTaskInfo.GetByte(10);
                //    if (!rdrTaskInfo.IsDBNull(11)) comboTaskPriority.SelectedIndex = rdrTaskInfo.GetByte(11);
                //    if (!rdrTaskInfo.IsDBNull(12)) txtTaskPhone.Text = rdrTaskInfo.GetString(12);
                //    else txtTaskPhone.Text = String.Empty;
                //    if (!rdrTaskInfo.IsDBNull(13)) txtTaskEmail.Text = rdrTaskInfo.GetString(13);
                //    else txtTaskEmail.Text = String.Empty;
                //    if (!rdrTaskInfo.IsDBNull(14)) chkReminder.Checked = rdrTaskInfo.GetBoolean(14);
                //    else chkReminder.Checked = false;
                //    if (!rdrTaskInfo.IsDBNull(15)) comboReminderTimePicker.Text = rdrTaskInfo.GetDateTime(15).ToString("MM/dd/yyyy");
                //    else comboReminderTimePicker.Text = String.Empty;
                //}
                //rdrTaskInfo.Close();
                //if (connRN.State != ConnectionState.Closed) connRN.Close();

                btnSaveTask.Enabled = false;
                btnForward.Enabled = true;
                btnReplyTask.Enabled = true;
                btnSave.Enabled = true;
            }
            else if (taskMode == TaskMode.EditInFDManagerDashboard)
            {
                InitializeTaskForm();
                FillTaskFormWithTaskInfo();

                btnSaveTask.Enabled = false;
                btnForward.Enabled = true;
                btnReplyTask.Enabled = true;
                btnSave.Enabled = true;

            }
            else if (taskMode == TaskMode.EditInFDStaffDashboard)
            {
                InitializeTaskForm();
                FillTaskFormWithTaskInfo();

                btnSaveTask.Enabled = false;
                btnForward.Enabled = true;
                btnReplyTask.Enabled = true;
                btnSave.Enabled = true;
            }
            else if (taskMode == TaskMode.EditInFDStaffDashboard)
            {
                InitializeTaskForm();
                FillTaskFormWithTaskInfo();

                btnSaveTask.Enabled = false;
                btnForward.Enabled = true;
                btnReplyTask.Enabled = true;
                btnSave.Enabled = true;
            }
            else if (taskMode == TaskMode.EditInCase)
            {
                InitializeTaskForm();
                FillTaskFormWithTaskInfo();

                //String strSqlQueryForTaskInfo = "select [dbo].[tbl_task_created_by].[User_Name], [dbo].[tbl_task_assigned_to].[User_Name], " +
                //                                "[dbo].[tbl_task].[Subject], [dbo].[tbl_task].[Comment], " +
                //                                "[dbo].[tbl_task].[DueDate], [dbo].[tbl_task].[RelatedToTableId], [dbo].[tbl_task].[whatid], [dbo].[tbl_task].[IndividualName], [dbo].[tbl_task].[whoid], " +
                //                                "[dbo].[tbl_task].[Comment], [dbo].[tbl_task].[Solution], [dbo].[tbl_task].[Status], [dbo].[tbl_task].[Priority], " +
                //                                "[dbo].[tbl_task].[ReminderDateTime], [dbo].[tbl_task].[IsReminderSet] " +
                //                                "from [dbo].[tbl_task] " +
                //                                "inner join [dbo].[tbl_task_created_by] on [dbo].[tbl_task].[CreatedById] = [dbo].[tbl_task_created_by].[User_Id] " +
                //                                "inner join [dbo].[tbl_task_assigned_to] on [dbo].[tbl_task].[AssignedTo] = [dbo].[tbl_task_assigned_to].[User_Id] " +
                //                                "where [dbo].[tbl_task].[id] = @TaskId";

                //SqlCommand cmdQueryForTaskInfo = new SqlCommand(strSqlQueryForTaskInfo, connRN);
                //cmdQueryForTaskInfo.CommandType = CommandType.Text;

                //cmdQueryForTaskInfo.Parameters.AddWithValue("@TaskId", nTaskId.Value);

                //if (connRN.State != ConnectionState.Closed)
                //{
                //    connRN.Close();
                //    connRN.Open();
                //}
                //else if (connRN.State == ConnectionState.Closed) connRN.Open();
                //SqlDataReader rdrTaskToEdit = cmdQueryForTaskInfo.ExecuteReader();
                //if (rdrTaskToEdit.HasRows)
                //{
                //    rdrTaskToEdit.Read();
                //    if (!rdrTaskToEdit.IsDBNull(0)) txtTaskCreator.Text = rdrTaskToEdit.GetString(0);
                //    else txtTaskCreator.Text = String.Empty;
                //    if (!rdrTaskToEdit.IsDBNull(1)) txtTaskNameAssignedTo.Text = rdrTaskToEdit.GetString(1);
                //    else txtTaskNameAssignedTo.Text = String.Empty;
                //    if (!rdrTaskToEdit.IsDBNull(2)) txtTaskSubject.Text = rdrTaskToEdit.GetString(2);
                //    else txtTaskSubject.Text = String.Empty;
                //    if (!rdrTaskToEdit.IsDBNull(3)) txtTaskComments.Text = rdrTaskToEdit.GetString(3);
                //    else txtTaskComments.Text = String.Empty;
                //    if (!rdrTaskToEdit.IsDBNull(4))
                //    {
                //        dtpTaskDueDate.Checked = true;
                //        dtpTaskDueDate.Value = DateTime.Parse(rdrTaskToEdit.GetDateTime(4).ToString("MM/dd/yyyy"));
                //        dtpTaskDueDate.Text = rdrTaskToEdit.GetDateTime(4).ToString("MM/dd/yyyy");
                //    }
                //    else
                //    {
                //        dtpTaskDueDate.Enabled = false;
                //        dtpTaskDueDate.Format = DateTimePickerFormat.Custom;
                //        dtpTaskDueDate.CustomFormat = " ";
                //    }
                //    if (!rdrTaskToEdit.IsDBNull(5)) comboTaskRelatedTo.SelectedIndex = rdrTaskToEdit.GetInt16(5);
                //    if (!rdrTaskToEdit.IsDBNull(6)) txtTaskRelatedTo.Text = rdrTaskToEdit.GetString(6);
                //    else txtTaskRelatedTo.Text = String.Empty;
                //    if (!rdrTaskToEdit.IsDBNull(7)) txtNameOnTask.Text = rdrTaskToEdit.GetString(7);
                //    else txtNameOnTask.Text = String.Empty;
                //    if (!rdrTaskToEdit.IsDBNull(8)) txtIndividualId.Text = rdrTaskToEdit.GetString(8);
                //    else txtIndividualId.Text = String.Empty;
                //    if (!rdrTaskToEdit.IsDBNull(9)) txtTaskComments.Text = rdrTaskToEdit.GetString(9);
                //    else txtTaskComments.Text = String.Empty;
                //    if (!rdrTaskToEdit.IsDBNull(10)) txtTaskSolution.Text = rdrTaskToEdit.GetString(10);
                //    else txtTaskSolution.Text = String.Empty;
                //    if (!rdrTaskToEdit.IsDBNull(11)) comboTaskStatus.SelectedIndex = rdrTaskToEdit.GetByte(11);
                //    if (!rdrTaskToEdit.IsDBNull(12)) comboTaskPriority.SelectedIndex = rdrTaskToEdit.GetByte(12);
                //    if (!rdrTaskToEdit.IsDBNull(13))
                //    {
                //        dtpReminderDatePicker.Checked = true;
                //        dtpReminderDatePicker.Value = DateTime.Parse(rdrTaskToEdit.GetDateTime(13).ToString("MM/dd/yyyy"));
                //        dtpReminderDatePicker.Text = rdrTaskToEdit.GetDateTime(13).ToString("MM/dd/yyyy");                           
                //    }
                //    else
                //    {
                //        dtpReminderDatePicker.Format = DateTimePickerFormat.Custom;
                //        dtpReminderDatePicker.CustomFormat = " ";
                //    }
                //    if (!rdrTaskToEdit.IsDBNull(14)) chkReminder.Checked = rdrTaskToEdit.GetBoolean(14);
                //}
                //rdrTaskToEdit.Close();
                //if (connRN.State != ConnectionState.Closed) connRN.Close();

                btnSaveTask.Enabled = false;
                btnForward.Enabled = true;
                btnReplyTask.Enabled = true;
                btnSave.Enabled = true;
            }
            else if (taskMode == TaskMode.EditInIndividual)
            {
                InitializeTaskForm();
                FillTaskFormWithTaskInfo();

                // nTaskId

                String strSqlQueryForTaskAssignedToId = "select [dbo].[tbl_task].[AssignedTo] from [dbo].[tbl_task] where [dbo].[tbl_task].[id] = @TaskId";

                SqlCommand cmdQueryForTaskAssignedToId = new SqlCommand(strSqlQueryForTaskAssignedToId, connRN);
                cmdQueryForTaskAssignedToId.CommandType = CommandType.Text;

                cmdQueryForTaskAssignedToId.Parameters.AddWithValue("@TaskId", nTaskId.Value);

                if (connRN.State != ConnectionState.Closed)
                {
                    connRN.Close();
                    connRN.Open();
                }
                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                Object objTaskAssignedToId = cmdQueryForTaskAssignedToId.ExecuteScalar();
                if (connRN.State != ConnectionState.Closed) connRN.Close();

                String TaskAssignedToId = objTaskAssignedToId?.ToString();

                if (TaskAssignedToId == null) return;

                int? nTaskAssignedToId = null;
                int resultTaskAssignedTo;

                if (Int32.TryParse(TaskAssignedToId, out resultTaskAssignedTo)) nTaskAssignedToId = resultTaskAssignedTo;

                if (nTaskAssignedToId.Value != LoggedInUserInfo.UserId)
                {
                    //MakeTaskFormReadonly();
                }
                


                //String strSqlQueryForTask = "select [dbo].[tbl_task].[whoid], [dbo].[tbl_task].[IndividualName], [dbo].[tbl_task].[whatid], " +
                //            "assigned_to.[User_Name], " +
                //            "[dbo].[tbl_task].[Subject], [dbo].[tbl_task].[DueDate], [dbo].[tbl_task].[RelatedToTableId], " +
                //            "[dbo].[tbl_task].[CreateDate], [dbo].[tbl_task_created_by].[User_Name], " +
                //            "[dbo].[tbl_task].[LastModifiedDate], last_modified_by.[User_Name], " +
                //            "[dbo].[tbl_task].[Comment], [dbo].[tbl_task].[Solution], [dbo].[tbl_task].[Status], " +
                //            "[dbo].[tbl_task].[Priority], " +
                //            "[dbo].[tbl_task].[PhoneNo], [dbo].[tbl_task].[Email], [dbo].[tbl_task].[IsClosed], " +
                //            "[dbo].[tbl_task].[IsReminderSet], [dbo].[tbl_task].[ReminderDateTime] " +
                //            "from [dbo].[tbl_task] " +
                //            "inner join [dbo].[tbl_department] sending_department on [dbo].[tbl_task].[SendingDepartment] = sending_department.[Department_Id] " +
                //            "inner join [dbo].[tbl_department] receiving_department on [dbo].[tbl_task].[ReceivingDepartment] = receiving_department.[Department_Id] " +
                //            "inner join [dbo].[tbl_task_created_by] on [dbo].[tbl_task].[CreatedById] = [dbo].[tbl_task_created_by].[User_Id] " +
                //            "inner join [dbo].[tbl_task_assigned_to] assigned_to on [dbo].[tbl_task].[AssignedTo] = assigned_to.[User_Id] " +
                //            "inner join [dbo].[tbl_task_assigned_to] last_modified_by on [dbo].[tbl_task].[LastModifiedById] = last_modified_by.[User_Id] " +
                //            "where [dbo].[tbl_task].[id] = @TaskId";

                //SqlCommand cmdQueryForTask = new SqlCommand(strSqlQueryForTask, connRN);
                //cmdQueryForTask.CommandType = CommandType.Text;

                //cmdQueryForTask.Parameters.AddWithValue("@TaskId", nTaskId);

                //if (connRN.State != ConnectionState.Closed)
                //{
                //    connRN.Close();
                //    connRN.Open();
                //}
                //else if (connRN.State == ConnectionState.Closed) connRN.Open();
                //SqlDataReader rdrQueryForTask = cmdQueryForTask.ExecuteReader();
                //if (rdrQueryForTask.HasRows)
                //{
                //    rdrQueryForTask.Read();
                //    if (!rdrQueryForTask.IsDBNull(0)) txtIndividualId.Text = rdrQueryForTask.GetString(0);
                //    else txtIndividualId.Text = String.Empty;
                //    if (!rdrQueryForTask.IsDBNull(1)) txtNameOnTask.Text = rdrQueryForTask.GetString(1);
                //    else txtNameOnTask.Text = String.Empty;
                //    if (!rdrQueryForTask.IsDBNull(2)) txtTaskRelatedTo.Text = rdrQueryForTask.GetString(2);
                //    else txtTaskRelatedTo.Text = String.Empty;
                //    if (!rdrQueryForTask.IsDBNull(3)) txtTaskNameAssignedTo.Text = rdrQueryForTask.GetString(3);
                //    else txtTaskNameAssignedTo.Text = String.Empty;
                //    if (!rdrQueryForTask.IsDBNull(4)) txtTaskSubject.Text = rdrQueryForTask.GetString(4);
                //    else txtTaskSubject.Text = String.Empty;
                //    if (!rdrQueryForTask.IsDBNull(5)) dtpTaskDueDate.Text = rdrQueryForTask.GetDateTime(5).ToString("MM/dd/yyyy");
                //    else dtpTaskDueDate.Text = String.Empty;
                //    if (!rdrQueryForTask.IsDBNull(6)) comboTaskRelatedTo.SelectedIndex = rdrQueryForTask.GetInt16(6);
                //    else comboTaskRelatedTo.SelectedItem = String.Empty;
                //    if (!rdrQueryForTask.IsDBNull(8)) txtTaskCreator.Text = rdrQueryForTask.GetString(8);
                //    else txtTaskCreator.Text = String.Empty;
                //    //if (!rdrQueryForTask.IsDBNull(10)) txtTaskNameAssignedTo.Text = rdrQueryForTask.GetString(10);
                //    //else txtTaskNameAssignedTo.Text = String.Empty;
                //    if (!rdrQueryForTask.IsDBNull(11)) txtTaskComments.Text = rdrQueryForTask.GetString(11);
                //    else txtTaskComments.Text = String.Empty;
                //    if (!rdrQueryForTask.IsDBNull(12)) txtTaskSolution.Text = rdrQueryForTask.GetString(12);
                //    else txtTaskSolution.Text = String.Empty;
                //    if (!rdrQueryForTask.IsDBNull(13)) comboTaskStatus.SelectedIndex = rdrQueryForTask.GetByte(13);
                //    else comboTaskStatus.SelectedItem = String.Empty;
                //    if (!rdrQueryForTask.IsDBNull(14)) comboTaskPriority.SelectedIndex = rdrQueryForTask.GetByte(14);
                //    else comboTaskPriority.SelectedItem = String.Empty;
                //    if (!rdrQueryForTask.IsDBNull(15)) txtTaskPhone.Text = rdrQueryForTask.GetString(15);
                //    else txtTaskPhone.Text = String.Empty;
                //    if (!rdrQueryForTask.IsDBNull(16)) txtTaskEmail.Text = rdrQueryForTask.GetString(16);
                //    else txtTaskEmail.Text = String.Empty;
                //    if (!rdrQueryForTask.IsDBNull(18)) chkReminder.Checked = rdrQueryForTask.GetBoolean(18);
                //    else chkReminder.Checked = false;
                //    if (!rdrQueryForTask.IsDBNull(19)) dtpReminderDatePicker.Text = rdrQueryForTask.GetDateTime(19).ToString("MM/dd/yyyy");
                //    else dtpReminderDatePicker.Text = String.Empty;

                //}
                //rdrQueryForTask.Close();
                //if (connRN.State != ConnectionState.Closed) connRN.Close();

                //btnSaveTask.Enabled = false;
                //btnReplyTask.Enabled = true;
                //btnForward.Enabled = true;
                //btnSave.Enabled = true;
            }
            else if (taskMode == TaskMode.Reply)
            {
                //InitializeTaskForm();

                //comboTaskRelatedTo.Items.Clear();
                //String strSqlQueryForRelatedToTable = "select [dbo].[tbl_task_related_to_code].[TaskRelatedToValue] from [dbo].[tbl_task_related_to_code]";

                //SqlCommand cmdQueryForRelatedToTable = new SqlCommand(strSqlQueryForRelatedToTable, connRN);
                //cmdQueryForRelatedToTable.CommandType = CommandType.Text;

                //if (connRN.State != ConnectionState.Closed)
                //{
                //    connRN.Close();
                //    connRN.Open();
                //}
                //else if (connRN.State == ConnectionState.Closed) connRN.Open();
                //SqlDataReader rdrRelatedToTable = cmdQueryForRelatedToTable.ExecuteReader();
                //if (rdrRelatedToTable.HasRows)
                //{
                //    while (rdrRelatedToTable.Read())
                //    {
                //        if (!rdrRelatedToTable.IsDBNull(0)) comboTaskRelatedTo.Items.Add(rdrRelatedToTable.GetString(0));
                //    }
                //}
                //rdrRelatedToTable.Close();
                //if (connRN.State != ConnectionState.Closed) connRN.Close();

                //comboTaskStatus.Items.Clear();
                //String strSqlQueryForTaskStatus = "select [dbo].[tbl_task_status_code].[TaskStatusValue] from [dbo].[tbl_task_status_code]";

                //SqlCommand cmdQueryForTaskStatus = new SqlCommand(strSqlQueryForTaskStatus, connRN);
                //cmdQueryForTaskStatus.CommandType = CommandType.Text;

                //if (connRN.State != ConnectionState.Closed)
                //{
                //    connRN.Close();
                //    connRN.Open();
                //}
                //else if (connRN.State == ConnectionState.Closed) connRN.Open();
                //SqlDataReader rdrTaskStatus = cmdQueryForTaskStatus.ExecuteReader();
                //if (rdrTaskStatus.HasRows)
                //{
                //    while (rdrTaskStatus.Read())
                //    {
                //        if (!rdrTaskStatus.IsDBNull(0)) comboTaskStatus.Items.Add(rdrTaskStatus.GetString(0));
                //    }
                //}
                //rdrTaskStatus.Close();
                //if (connRN.State != ConnectionState.Closed) connRN.Close();

                //comboTaskPriority.Items.Clear();
                //String strSqlQueryForTaskPriority = "select [dbo].[tbl_task_priority_code].[TaskPriorityValue] from [dbo].[tbl_task_priority_code]";

                //SqlCommand cmdQueryForTaskPriority = new SqlCommand(strSqlQueryForTaskPriority, connRN);
                //cmdQueryForTaskPriority.CommandType = CommandType.Text;

                //if (connRN.State != ConnectionState.Closed)
                //{
                //    connRN.Close();
                //    connRN.Open();
                //}
                //else if (connRN.State == ConnectionState.Closed) connRN.Open();
                //SqlDataReader rdrTaskPriority = cmdQueryForTaskPriority.ExecuteReader();
                //if (rdrTaskPriority.HasRows)
                //{
                //    while (rdrTaskPriority.Read())
                //    {
                //        if (!rdrTaskPriority.IsDBNull(0)) comboTaskPriority.Items.Add(rdrTaskPriority.GetString(0));
                //    }
                //}
                //rdrTaskPriority.Close();
                //if (connRN.State != ConnectionState.Closed) connRN.Close();

                //String strSqlQueryForTaskInfo = "select [dbo].[tbl_task_created_by].[User_Name], [dbo].[tbl_task_assigned_to].[User_Name], " +
                //                                "[dbo].[tbl_task].[whoid], [dbo].[tbl_task].[IndividualName], " +
                //                                "[dbo].[tbl_task].[DueDate], [dbo].[tbl_task].[RelatedToTableId], [dbo].[tbl_task].[whatid], " +
                //                                "[dbo].[tbl_task].[Subject], [dbo].[tbl_task].[Comment], [dbo].[tbl_task].[Solution], " +
                //                                "[dbo].[tbl_task].[Status], [dbo].[tbl_task].[Priority], [dbo].[tbl_task].[PhoneNo], [dbo].[tbl_task].[Email], " +
                //                                "[dbo].[tbl_task].[IsReminderSet], [dbo].[tbl_task].[ReminderDateTime] " +
                //                                "from [dbo].[tbl_task] " +
                //                                "inner join [dbo].[tbl_task_created_by] on [dbo].[tbl_task].[CreatedById] = [dbo].[tbl_task_created_by].[User_Id] " +
                //                                "inner join [dbo].[tbl_task_assigned_to] on [dbo].[tbl_task].[AssignedTo] = [dbo].[tbl_task_assigned_to].[User_Id] " +
                //                                "where [dbo].[tbl_task].[id] = @TaskId";

                //SqlCommand cmdQueryForTaskInfo = new SqlCommand(strSqlQueryForTaskInfo, connRN);
                //cmdQueryForTaskInfo.CommandType = CommandType.Text;

                //cmdQueryForTaskInfo.Parameters.AddWithValue("@TaskId", nOriginalTask_Id.Value);

                //if (connRN.State != ConnectionState.Closed)
                //{
                //    connRN.Close();
                //    connRN.Open();
                //}
                //else if (connRN.State == ConnectionState.Closed) connRN.Open();
                //SqlDataReader rdrTaskInfo = cmdQueryForTaskInfo.ExecuteReader();
                //if (rdrTaskInfo.HasRows)
                //{
                //    rdrTaskInfo.Read();
                //    if (!rdrTaskInfo.IsDBNull(0)) txtTaskCreator.Text = rdrTaskInfo.GetString(0);
                //    else txtTaskCreator.Text = String.Empty;
                //    if (!rdrTaskInfo.IsDBNull(1)) txtTaskNameAssignedTo.Text = rdrTaskInfo.GetString(1);
                //    else txtTaskNameAssignedTo.Text = String.Empty;
                //    if (!rdrTaskInfo.IsDBNull(2)) txtIndividualId.Text = rdrTaskInfo.GetString(2);
                //    else txtIndividualId.Text = String.Empty;
                //    if (!rdrTaskInfo.IsDBNull(3)) txtNameOnTask.Text = rdrTaskInfo.GetString(3);
                //    else txtNameOnTask.Text = String.Empty;
                //    if (!rdrTaskInfo.IsDBNull(4)) dtpTaskDueDate.Text = rdrTaskInfo.GetDateTime(4).ToString("MM/dd/yyyy");
                //    else dtpTaskDueDate.Text = String.Empty;
                //    if (!rdrTaskInfo.IsDBNull(5)) comboTaskRelatedTo.SelectedIndex = rdrTaskInfo.GetInt16(5);
                //    if (!rdrTaskInfo.IsDBNull(6)) txtTaskRelatedTo.Text = rdrTaskInfo.GetString(6);
                //    else txtTaskRelatedTo.Text = String.Empty;
                //    if (!rdrTaskInfo.IsDBNull(7)) txtTaskSubject.Text = rdrTaskInfo.GetString(7);
                //    else txtTaskSubject.Text = String.Empty;
                //    if (!rdrTaskInfo.IsDBNull(8))
                //    {

                //        txtTaskComments.Text = rdrTaskInfo.GetString(8);
                //        txtTaskComments.AppendText(Environment.NewLine);
                //        txtTaskComments.AppendText("---------------------------------------------------------------------------------------");
                //        txtTaskComments.Select(0, txtTaskComments.Text.Length);
                //        txtTaskComments.SelectionProtected = true;
                //        txtTaskComments.AppendText(Environment.NewLine);
                //        this.ActiveControl = txtTaskComments;

                //    }
                //    else txtTaskComments.Text = String.Empty;
                //    if (!rdrTaskInfo.IsDBNull(9))
                //    {
                //        if (rdrTaskInfo.GetString(9).Trim() != String.Empty)
                //        {
                //            txtTaskSolution.Text = rdrTaskInfo.GetString(9);
                //            txtTaskSolution.Text += Environment.NewLine;
                //            txtTaskSolution.Text += ("---------------------------------------------------------------------------------------");
                //            txtTaskSolution.Text += Environment.NewLine;
                //        }
                //    }
                //    else txtTaskSolution.Text = String.Empty;
                //    if (!rdrTaskInfo.IsDBNull(10)) comboTaskStatus.SelectedIndex = rdrTaskInfo.GetByte(10);
                //    if (!rdrTaskInfo.IsDBNull(11)) comboTaskPriority.SelectedIndex = rdrTaskInfo.GetByte(11);
                //    if (!rdrTaskInfo.IsDBNull(12)) txtTaskPhone.Text = rdrTaskInfo.GetString(12);
                //    else txtTaskPhone.Text = String.Empty;
                //    if (!rdrTaskInfo.IsDBNull(13)) txtTaskEmail.Text = rdrTaskInfo.GetString(13);
                //    else txtTaskEmail.Text = String.Empty;
                //    if (!rdrTaskInfo.IsDBNull(14)) chkReminder.Checked = rdrTaskInfo.GetBoolean(14);
                //    else chkReminder.Checked = false;
                //    if (!rdrTaskInfo.IsDBNull(15)) comboReminderTimePicker.Text = rdrTaskInfo.GetDateTime(15).ToString("MM/dd/yyyy");
                //    else comboReminderTimePicker.Text = String.Empty;

                //    //txtReplyTo.Text = rdrTaskInfo.GetString(0);
                //}
                //rdrTaskInfo.Close();
                //if (connRN.State != ConnectionState.Closed) connRN.Close();

                //btnAssignedTo.Enabled = false;
                //txtTaskNameAssignedTo.ReadOnly = true;

                //btnReplyTask.Enabled = false;

                ////btnSaveTask.Enabled = false;
                //btnForward.Enabled = true;
                ////txtReplyTo.Enabled = true;
                ////txtReplyTo.ReadOnly = false;

                ////this.ActiveControl = txtTaskComments;
                ////txtTaskComments.Select(txtTaskComments.Text.Length, 0);


            }

            if (taskMode != TaskMode.AddNew)
            {
                String[] TaskAssigneeNames = txtTaskNameAssignedTo.Text.Split(';');

                String TaskAssigneeName = TaskAssigneeNames[0];

                if (TaskAssigneeName != LoggedInUserInfo.UserName) MakeTaskFormReadonly();                
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
            if (nTaskId == null && nOriginalTask_Id == null && taskMode == TaskMode.AddNew)    // Create new task
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
                    String strSqlQueryForAssignedToInfo = "select [dbo].[tbl_user].[User_Id], [dbo].[tbl_user].[Department_Id], " +
                                                          "[dbo].[tbl_user].[User_Role_Id], [dbo].[tbl_user].[Task_Role_Id] from [dbo].[tbl_user] " +
                                                          "where [dbo].[tbl_user].[User_Name] = @UserName";

                    SqlCommand cmdQueryForAssignedToInfo = new SqlCommand(strSqlQueryForAssignedToInfo, connRN);
                    cmdQueryForAssignedToInfo.CommandType = CommandType.Text;

                    cmdQueryForAssignedToInfo.Parameters.AddWithValue("@UserName", name.Trim());

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
                        if (!rdrAssignedTo.IsDBNull(2)) staffInfo.UserRoleId = (UserRole)rdrAssignedTo.GetInt16(2);
                        if (!rdrAssignedTo.IsDBNull(3)) staffInfo.TaskUserRoleId = (TaskUserRole)rdrAssignedTo.GetInt16(3);
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

                //TaskStatus ts = (TaskStatus)comboTaskStatus.SelectedIndex;
                TaskStatus ts;

                if (comboTaskStatus.SelectedIndex < 3) ts = (TaskStatus)comboTaskStatus.SelectedIndex;
                else ts = (TaskStatus)(comboTaskStatus.SelectedIndex + 3);

                TaskPriority tp = (TaskPriority)comboTaskPriority.SelectedIndex;

                String PhoneNo = txtTaskPhone.Text.Trim();
                String Email = txtTaskEmail.Text.Trim();

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

                    int Year = Int16.Parse(strDate.Substring(6, 4));
                    int Month = Int16.Parse(strDate.Substring(0, 2));
                    int Day = Int16.Parse(strDate.Substring(3, 2));
                    String Hour = strTime.Substring(0, strTime.IndexOf(':'));
                    String Minute = strTime.Substring(strTime.IndexOf(':') + 1, 2);

                    Reminder = new DateTime(Year, Month, Day, Int16.Parse(Hour), Int16.Parse(Minute), 0);                  
                }

                Cursor.Current = Cursors.WaitCursor;
                Boolean bTaskSent = true;

                bTaskSentToITManager = false;
                bTaskSentToFDManager = false;
                bTaskSentToMSManager = false;
                bTaskSentToNPManager = false;
                bTaskSentToRNManager = false;

                bTaskSentToITManagerOfITSender = false;
                bTaskSentToMSManagerOfMSSender = false;
                bTaskSentToNPManagerOfNPSender = false;
                bTaskSentToRNManagerOfRNSender = false;
                bTaskSentToFDManagerOfFDSender = false;

                foreach (UserInfo staffInfo in lstStaffAssignedTo)
                {

                    String strSqlInsertIntoTask = "insert into [dbo].[tbl_task] ([dbo].[tbl_task].[whoid], [dbo].[tbl_task].[IndividualName], [dbo].[tbl_task].[whatid], " +
                                                  "[dbo].[tbl_task].[IsDeleted], " +
                                                  "[dbo].[tbl_task].[AssignedTo], [dbo].[tbl_task].[SendingDepartment], [dbo].[tbl_task].[ReceivingDepartment], " +
                                                  "[dbo].[tbl_task].[Subject], [dbo].[tbl_task].[DueDate], [dbo].[tbl_task].[RelatedToTableId], " +
                                                  "[dbo].[tbl_task].[CreateDate], [dbo].[tbl_task].[CreatedById], [dbo].[tbl_task].[LastModifiedDate], [dbo].[tbl_task].[LastModifiedById], " +
                                                  "[dbo].[tbl_task].[ActivityDate], " +
                                                  "[dbo].[tbl_task].[Comment], [dbo].[tbl_task].[Solution], [dbo].[tbl_task].[Status], [dbo].[tbl_task].[Priority], " +
                                                  "[dbo].[tbl_task].[PhoneNo], [dbo].[tbl_task].[Email], " +
                                                  "[dbo].[tbl_task].[IsClosed], [dbo].[tbl_task].[ReminderDateTime], [dbo].[tbl_task].[IsReminderSet], [dbo].[tbl_task].[IsArchived]) " +
                                                  "values (@WhoId, @IndividualName, @WhatId, 0, @AssignedTo, @SendingDepartment, @ReceivingDepartment, " +
                                                  "@Subject, @DueDate, @RelatedTo, @CreateDate, @CreatedById, " +
                                                  "@LastModifiedDate, @LastModifiedById, @ActivityDate, " +
                                                  "@Comment, @Solution, @Status, @Priority, @PhoneNo, @Email, " +
                                                  "@IsClosed, @ReminderDateTime, @IsReminderSet, @IsArchived); " +
                                                  "select scope_identity();";

                    SqlCommand cmdInsertIntoTask = new SqlCommand(strSqlInsertIntoTask, connRN);
                    cmdInsertIntoTask.CommandType = CommandType.Text;

                    cmdInsertIntoTask.Parameters.AddWithValue("@WhoId", WhoId);
                    cmdInsertIntoTask.Parameters.AddWithValue("@IndividualName", WhoName);
                    cmdInsertIntoTask.Parameters.AddWithValue("@WhatId", WhatId);
                    //cmdInsertIntoTask.Parameters.AddWithValue("@AssignedTo", AssignedToStaffInfo.UserId);
                    cmdInsertIntoTask.Parameters.AddWithValue("@AssignedTo", staffInfo.UserId);
                    cmdInsertIntoTask.Parameters.AddWithValue("@SendingDepartment", LoggedInUserInfo.departmentInfo.DepartmentId);
                    //cmdInsertIntoTask.Parameters.AddWithValue("@ReceivingDepartment", AssignedToStaffInfo.departmentInfo.DepartmentId);
                    cmdInsertIntoTask.Parameters.AddWithValue("@ReceivingDepartment", staffInfo.departmentInfo.DepartmentId);
                    cmdInsertIntoTask.Parameters.AddWithValue("@Subject", Subject);
                    cmdInsertIntoTask.Parameters.AddWithValue("@DueDate", DueDate);
                    cmdInsertIntoTask.Parameters.AddWithValue("@RelatedTo", comboTaskRelatedTo.SelectedIndex);
                    cmdInsertIntoTask.Parameters.AddWithValue("@CreateDate", DateTime.Now);
                    cmdInsertIntoTask.Parameters.AddWithValue("@CreatedById", LoggedInUserInfo.UserId);
                    cmdInsertIntoTask.Parameters.AddWithValue("@LastModifiedDate", DateTime.Now);
                    cmdInsertIntoTask.Parameters.AddWithValue("@LastModifiedById", LoggedInUserInfo.UserId);
                    cmdInsertIntoTask.Parameters.AddWithValue("@ActivityDate", DateTime.Now);
                    cmdInsertIntoTask.Parameters.AddWithValue("@Comment", Comment);
                    cmdInsertIntoTask.Parameters.AddWithValue("@Solution", Solution);
                    cmdInsertIntoTask.Parameters.AddWithValue("@Status", ts);
                    cmdInsertIntoTask.Parameters.AddWithValue("@Priority", tp);
                    cmdInsertIntoTask.Parameters.AddWithValue("@PhoneNo", PhoneNo);
                    cmdInsertIntoTask.Parameters.AddWithValue("@Email", Email);
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
                    //int nRowInserted = cmdInsertIntoTask.ExecuteNonQuery();
                    //Int32? nTaskIdSent = (int)cmdInsertIntoTask.ExecuteScalar();
                    Object objTaskIdSent = cmdInsertIntoTask.ExecuteScalar();
                    if (connRN.State == ConnectionState.Open) connRN.Close();

                    if (objTaskIdSent == null)
                    {
                        bTaskSent = false;
                    }
                    else
                    {


                        bTaskSentToManagerOfSender = false;

                        bTaskSent = true;
                        Int32? TaskIdInserted = null;
                        Int32 resultTaskIdInserted;                 

                        if (Int32.TryParse(objTaskIdSent.ToString(), out resultTaskIdInserted)) TaskIdInserted = resultTaskIdInserted;

                        TaskUserInfo TaskSenderInfo = new TaskUserInfo();
                        TaskUserInfo TaskReceiverInfo = new TaskUserInfo();

                        String strSqlQueryForTaskSenderInfo = "select [dbo].[tbl_user].[Task_Role_Id] from [dbo].[tbl_user] " +
                                                              "where [dbo].[tbl_user].[User_Id] = @TaskSenderId and " +
                                                              "([dbo].[tbl_user].[IsDeleted] = 0 or [dbo].[tbl_user].[IsDeleted] IS NULL)";

                        SqlCommand cmdQueryForTaskSenderInfo = new SqlCommand(strSqlQueryForTaskSenderInfo, connRN);
                        cmdQueryForTaskSenderInfo.CommandType = CommandType.Text;

                        cmdQueryForTaskSenderInfo.Parameters.AddWithValue("@TaskSenderId", LoggedInUserInfo.UserId);

                        if (connRN.State != ConnectionState.Closed)
                        {
                            connRN.Close();
                            connRN.Open();
                        }
                        else if (connRN.State == ConnectionState.Closed) connRN.Open();
                        Object objTaskSenderRoleId = cmdQueryForTaskSenderInfo.ExecuteScalar();
                        if (connRN.State != ConnectionState.Closed) connRN.Close();

                        TaskSenderInfo.UserId = LoggedInUserInfo.UserId;
                        TaskSenderInfo.UserName = LoggedInUserInfo.UserName;
                        TaskSenderInfo.UserEmail = LoggedInUserInfo.UserEmail;
                        TaskSenderInfo.departmentInfo.DepartmentId = LoggedInUserInfo.departmentInfo.DepartmentId;

                        if (objTaskSenderRoleId != null)
                        {
                            int resultTaskSenderRole;
                            if (Int32.TryParse(objTaskSenderRoleId.ToString(), out resultTaskSenderRole)) TaskSenderInfo.TaskUserRoleId = (TaskUserRole)resultTaskSenderRole;
                        }

                        String strSqlQueryForTaskReceiverInfo = "select [dbo].[tbl_user].[Task_Role_Id] from [dbo].[tbl_user] " +
                                                                "where [dbo].[tbl_user].[User_Id] = @TaskReceiverId and " +
                                                                "([dbo].[tbl_user].[IsDeleted] = 0 or [dbo].[tbl_user].[IsDeleted] IS NULL)";

                        SqlCommand cmdQueryForTaskReceiverInfo = new SqlCommand(strSqlQueryForTaskReceiverInfo, connRN);
                        cmdQueryForTaskReceiverInfo.CommandType = CommandType.Text;

                        cmdQueryForTaskReceiverInfo.Parameters.AddWithValue("@TaskReceiverId", staffInfo.UserId);

                        if (connRN.State != ConnectionState.Closed)
                        {
                            connRN.Close();
                            connRN.Open();
                        }
                        else if (connRN.State == ConnectionState.Closed) connRN.Open();
                        Object objTaskReceiverRoleId = cmdQueryForTaskReceiverInfo.ExecuteScalar();
                        if (connRN.State != ConnectionState.Closed) connRN.Close();

                        TaskReceiverInfo.UserId = staffInfo.UserId;
                        TaskReceiverInfo.UserName = staffInfo.UserName;
                        TaskReceiverInfo.UserEmail = staffInfo.UserEmail;
                        TaskReceiverInfo.departmentInfo.DepartmentId = staffInfo.departmentInfo.DepartmentId;

                        if (objTaskReceiverRoleId != null)
                        {
                            int resultTaskReceiverRole;
                            if (Int32.TryParse(objTaskReceiverRoleId.ToString(), out resultTaskReceiverRole)) TaskReceiverInfo.TaskUserRoleId = (TaskUserRole)resultTaskReceiverRole;
                        }

                        FDManagerTaskIDs FDManagerTaskId = new FDManagerTaskIDs(); 

                        String strSqlQueryForFDManagerId = "select [dbo].[tbl_user].[User_Id] from [dbo].[tbl_user] " +
                                                           "where [dbo].[tbl_user].[Task_Role_Id] = @FDManagerTaskRoleId and " +
                                                           "([dbo].[tbl_user].[IsDeleted] = 0 or [dbo].[tbl_user].[IsDeleted] IS NULL)";

                        SqlCommand cmdQueryForFDManagerId = new SqlCommand(strSqlQueryForFDManagerId, connRN);
                        cmdQueryForFDManagerId.CommandType = CommandType.Text;

                        cmdQueryForFDManagerId.Parameters.AddWithValue("@FDManagerTaskRoleId", (Int16)FDManagerTaskId.FDManagerTaskRoleId);

                        if (connRN.State != ConnectionState.Closed)
                        {
                            connRN.Close();
                            connRN.Open();
                        }
                        else if (connRN.State == ConnectionState.Closed) connRN.Open();
                        Object objFDManagerTaskUserId = cmdQueryForFDManagerId.ExecuteScalar();
                        if (connRN.State != ConnectionState.Closed) connRN.Close();

                        if (objFDManagerTaskUserId != null)
                        {
                            int resultTaskUserId;
                            if (Int32.TryParse(objFDManagerTaskUserId.ToString(), out resultTaskUserId)) FDManagerTaskId.FDManagerTaskUserId = resultTaskUserId;
                        }

                        RNManagerTaskIDs RNManagerTaskId = new RNManagerTaskIDs();

                        String strSqlQueryForRNManagerId = "select [dbo].[tbl_user].[User_Id] from [dbo].[tbl_user] " +
                                                           "where [dbo].[tbl_user].[Task_Role_Id] = @RNManagerTaskRoleId and " +
                                                           "([dbo].[tbl_user].[IsDeleted] = 0 or [dbo].[tbl_user].[IsDeleted] IS NULL)";

                        SqlCommand cmdQueryForRNManagerId = new SqlCommand(strSqlQueryForRNManagerId, connRN);
                        cmdQueryForRNManagerId.CommandType = CommandType.Text;

                        cmdQueryForRNManagerId.Parameters.AddWithValue("@RNManagerTaskRoleId", (Int16)RNManagerTaskId.RNManagerTaskRoleId);

                        if (connRN.State != ConnectionState.Closed)
                        {
                            connRN.Close();
                            connRN.Open();
                        }
                        else if (connRN.State == ConnectionState.Closed) connRN.Open();
                        Object objRNManagerTaskUserId = cmdQueryForRNManagerId.ExecuteScalar();
                        if (connRN.State != ConnectionState.Closed) connRN.Close();

                        if (objRNManagerTaskUserId != null)
                        {
                            int resultTaskUserId;
                            if (Int32.TryParse(objRNManagerTaskUserId.ToString(), out resultTaskUserId)) RNManagerTaskId.RNManagerTaskUserId = resultTaskUserId;
                        }

                        NPManagerTaskIDs NPManagerTaskId = new NPManagerTaskIDs();

                        String strSqlQueryForNPManagerId = "select [dbo].[tbl_user].[User_Id] from [dbo].[tbl_user] " +
                                                           "where [dbo].[tbl_user].[Task_Role_Id] = @NPManagerTaskRoleId and " +
                                                           "([dbo].[tbl_user].[IsDeleted] = 0 or [dbo].[tbl_user].[IsDeleted] IS NULL)";

                        SqlCommand cmdQueryForNPManagerId = new SqlCommand(strSqlQueryForNPManagerId, connRN);
                        cmdQueryForNPManagerId.CommandType = CommandType.Text;

                        cmdQueryForNPManagerId.Parameters.AddWithValue("@NPManagerTaskRoleId", (Int16)NPManagerTaskId.NPManagerTaskRoleId);

                        if (connRN.State != ConnectionState.Closed)
                        {
                            connRN.Close();
                            connRN.Open();
                        }
                        else if (connRN.State == ConnectionState.Closed) connRN.Open();
                        Object objNPManagerTaskUserId = cmdQueryForNPManagerId.ExecuteScalar();

                        if (objNPManagerTaskUserId != null)
                        {
                            int resultTaskUserId;
                            if (Int32.TryParse(objNPManagerTaskUserId.ToString(), out resultTaskUserId)) NPManagerTaskId.NPManagerTaskUserId = resultTaskUserId;
                        }

                        if (connRN.State != ConnectionState.Closed) connRN.Close();

                        MSManagerTaskIDs MSManagerTaskId = new MSManagerTaskIDs();

                        String strSqlQueryForMSManagerId = "select [dbo].[tbl_user].[User_Id] from [dbo].[tbl_user] " +
                                                           "where [dbo].[tbl_user].[Task_Role_Id] = @MSManagerTaskRoleId and " +
                                                           "([dbo].[tbl_user].[IsDeleted]= 0 or [dbo].[tbl_user].[IsDeleted] IS NULL)";

                        SqlCommand cmdQueryForMSManagerId = new SqlCommand(strSqlQueryForMSManagerId, connRN);
                        cmdQueryForMSManagerId.CommandType = CommandType.Text;

                        cmdQueryForMSManagerId.Parameters.AddWithValue("@MSManagerTaskRoleId", (Int16)MSManagerTaskId.MSManagerTaskRoleId);

                        if (connRN.State != ConnectionState.Closed)
                        {
                            connRN.Close();
                            connRN.Open();
                        }
                        else if (connRN.State == ConnectionState.Closed) connRN.Open();
                        Object objMSManagerTaskUserId = cmdQueryForMSManagerId.ExecuteScalar();
                        if (connRN.State != ConnectionState.Closed) connRN.Close();

                        if (objMSManagerTaskUserId != null)
                        {
                            int resultTaskUserId;
                            if (Int32.TryParse(objMSManagerTaskUserId.ToString(), out resultTaskUserId)) MSManagerTaskId.MSManagerTaskUserId = resultTaskUserId;
                        }


                        //if (TaskIdInserted != null && !bTaskSentToManagerOfSender)
                        if (objTaskIdSent != null)
                        {
                            // send the task to sending user department manager
                            //LoggedInUserInfo.departmentInfo.DepartmentId
                            //Department? SendingDepartment = null;
                            //Int32? nSendingStaffDepartmentManagerId = null;

                            switch (LoggedInUserInfo.departmentInfo.DepartmentId)
                            {
                                case Department.MemberService:      // Sender's is MS staff
                                    if (!bTaskSentToMSManagerOfMSSender && 
                                        LoggedInUserInfo.UserRoleId != UserRole.MSManager && 
                                        staffInfo.UserRoleId != UserRole.MSManager)                                        
                                    {
                                        AssignTaskToManager(MSManagerTaskId.MSManagerTaskUserId.Value, LoggedInUserInfo, TaskIdInserted.Value);     // assign a task to MS Manager
                                        bTaskSentToMSManagerOfMSSender = true;
                                    }
                                    break;

                                case Department.NeedsProcessing:    // Sender is NP staff
                                    if (!bTaskSentToNPManagerOfNPSender && 
                                        LoggedInUserInfo.UserRoleId != UserRole.NPManager && 
                                        staffInfo.UserRoleId != UserRole.NPManager)
                                    {
                                        AssignTaskToManager(NPManagerTaskId.NPManagerTaskUserId.Value, LoggedInUserInfo, TaskIdInserted.Value);     // assign a task to NP Manager
                                        bTaskSentToNPManagerOfNPSender = true;
                                    }
                                    break;
                                case Department.ReviewAndNegotiation:   // Sender is RN staff
                                    if ((!bTaskSentToRNManagerOfRNSender && 
                                        LoggedInUserInfo.UserRoleId != UserRole.RNManager && 
                                        staffInfo.UserRoleId != UserRole.RNManager)||
                                        (!bTaskSentToRNManagerOfRNSender && 
                                        TaskSenderInfo.TaskUserRoleId == TaskUserRole.RNAssistantManager &&
                                        staffInfo.UserRoleId != UserRole.RNManager)||
                                        (!bTaskSentToRNManagerOfRNSender &&
                                        TaskSenderInfo.TaskUserRoleId == TaskUserRole.RNStaff &&
                                        staffInfo.TaskUserRoleId == TaskUserRole.RNAssistantManager))
                                    {
                                        AssignTaskToManager(RNManagerTaskId.RNManagerTaskUserId.Value, LoggedInUserInfo, TaskIdInserted.Value);     // assign a task RN Manager
                                        bTaskSentToRNManagerOfRNSender = true;
                                    }
                                    break;
                                case Department.Finance:                // Sender is FD staff
                                    if (!bTaskSentToFDManagerOfFDSender && 
                                        LoggedInUserInfo.UserRoleId != UserRole.FDManager && 
                                        staffInfo.UserRoleId != UserRole.FDManager)
                                    {
                                        AssignTaskToManager(FDManagerTaskId.FDManagerTaskUserId.Value, LoggedInUserInfo, TaskIdInserted.Value);     // assign a task FD Manager
                                        bTaskSentToFDManagerOfFDSender = true;
                                    }
                                    break;
                            }
                        }

                        if (TaskIdInserted != null)
                        {
                            //Int32? nDepartmentManagerId = null;

                            switch (staffInfo.departmentInfo.DepartmentId)  // original task the assigned to staff info
                            {
                                case Department.MemberService:  // assigned to is MS staff
                                    if (!bTaskSentToMSManager && 
                                        LoggedInUserInfo.UserRoleId != UserRole.MSManager && 
                                        staffInfo.UserRoleId != UserRole.MSManager &&
                                        LoggedInUserInfo.departmentInfo.DepartmentId != staffInfo.departmentInfo.DepartmentId)
                                    {
                                        //AssignTaskToManager(MSManagerTaskId.MSManagerTaskUserId.Value, LoggedInUserInfo, TaskIdInserted.Value);
                                        AssignTaskToManager(MSManagerTaskId.MSManagerTaskUserId.Value, staffInfo, TaskIdInserted.Value);
                                        bTaskSentToMSManager = true;
                                    }
                                    break;
                                case Department.NeedsProcessing:    // assigned to is NP staff
                                    if (!bTaskSentToNPManager && 
                                        LoggedInUserInfo.UserRoleId != UserRole.NPManager && 
                                        staffInfo.UserRoleId != UserRole.NPManager &&
                                        LoggedInUserInfo.departmentInfo.DepartmentId != staffInfo.departmentInfo.DepartmentId)
                                    {
                                        //AssignTaskToManager(NPManagerTaskId.NPManagerTaskUserId.Value, LoggedInUserInfo, TaskIdInserted.Value);
                                        AssignTaskToManager(NPManagerTaskId.NPManagerTaskUserId.Value, staffInfo, TaskIdInserted.Value);
                                        bTaskSentToNPManager = true;
                                    }
                                    break;
                                case Department.Finance:    // assigned to is FD staff
                                    if (!bTaskSentToFDManager && 
                                        LoggedInUserInfo.UserRoleId != UserRole.FDManager && 
                                        staffInfo.UserRoleId != UserRole.FDManager &&
                                        LoggedInUserInfo.departmentInfo.DepartmentId != staffInfo.departmentInfo.DepartmentId)
                                    {
                                        //AssignTaskToManager(FDManagerTaskId.FDManagerTaskUserId.Value, LoggedInUserInfo, TaskIdInserted.Value);
                                        AssignTaskToManager(FDManagerTaskId.FDManagerTaskUserId.Value, staffInfo, TaskIdInserted.Value);
                                        bTaskSentToFDManager = true;
                                    }
                                    break;
                                case Department.ReviewAndNegotiation:   // assigned to is RN staff
                                    if ((!bTaskSentToRNManager && 
                                        LoggedInUserInfo.UserRoleId != UserRole.RNManager && 
                                        staffInfo.UserRoleId != UserRole.RNManager &&
                                        LoggedInUserInfo.departmentInfo.DepartmentId != staffInfo.departmentInfo.DepartmentId)||
                                        (!bTaskSentToRNManager && 
                                        TaskReceiverInfo.TaskUserRoleId == TaskUserRole.RNAssistantManager &&
                                        LoggedInUserInfo.departmentInfo.DepartmentId != staffInfo.departmentInfo.DepartmentId)||
                                        (!bTaskSentToRNManager &&
                                        staffInfo.TaskUserRoleId == TaskUserRole.RNAssistantManager &&
                                        LoggedInUserInfo.departmentInfo.DepartmentId != staffInfo.departmentInfo.DepartmentId))
                                    {
                                        //AssignTaskToManager(nDepartmentManagerId.Value, staffInfo, TaskIdInserted.Value);
                                        //AssignTaskToManager(RNManagerTaskId.RNManagerTaskUserId.Value, LoggedInUserInfo, TaskIdInserted.Value);
                                        AssignTaskToManager(RNManagerTaskId.RNManagerTaskUserId.Value, staffInfo, TaskIdInserted.Value);
                                        bTaskSentToRNManager = true;
                                    }
                                    break;
                            }

                            //String strSqlAssignTaskToManager = "insert into [dbo].[tbl_task] ([dbo].[tbl_task].[whoid], [dbo].[tbl_task].[IndividualName], [dbo].[tbl_task].[whatid], [dbo].[tbl_task].[IsDeleted], " +
                            //                      "[dbo].[tbl_task].[AssignedTo], [dbo].[tbl_task].[SendingDepartment], [dbo].[tbl_task].[ReceivingDepartment], " +
                            //                      "[dbo].[tbl_task].[Subject], [dbo].[tbl_task].[DueDate], [dbo].[tbl_task].[RelatedToTableId], " +
                            //                      "[dbo].[tbl_task].[CreateDate], [dbo].[tbl_task].[CreatedById], [dbo].[tbl_task].[LastModifiedDate], [dbo].[tbl_task].[LastModifiedById], " +
                            //                      "[dbo].[tbl_task].[ActivityDate], " +
                            //                      "[dbo].[tbl_task].[Comment], [dbo].[tbl_task].[Solution], [dbo].[tbl_task].[Status], [dbo].[tbl_task].[Priority], " +
                            //                      "[dbo].[tbl_task].[IsClosed], [dbo].[tbl_task].[ReminderDateTime], [dbo].[tbl_task].[IsReminderSet], [dbo].[tbl_task].[IsArchived], " +
                            //                      "[dbo].[tbl_task].[ManagerTaskId]) " +
                            //                      //"output inserted.id " +
                            //                      "values (@WhoId, @IndividualName, @WhatId, 0, @AssignedTo, @SendingDepartment, @ReceivingDepartment, " +
                            //                      "@Subject, @DueDate, @RelatedTo, @CreateDate, @CreatedById, " +
                            //                      "@LastModifiedDate, @LastModifiedById, @ActivityDate, " +
                            //                      "@Comment, @Solution, @Status, @Priority, @IsClosed, @ReminderDateTime, @IsReminderSet, @IsArchived, @ManagerTaskId)";

                            //SqlCommand cmdInsertIntoAssignTaskToManager = new SqlCommand(strSqlAssignTaskToManager, connRN);
                            //cmdInsertIntoAssignTaskToManager.CommandType = CommandType.Text;

                            //cmdInsertIntoAssignTaskToManager.Parameters.AddWithValue("@WhoId", WhoId);
                            //cmdInsertIntoAssignTaskToManager.Parameters.AddWithValue("@IndividualName", WhoName);
                            //cmdInsertIntoAssignTaskToManager.Parameters.AddWithValue("@WhatId", WhatId);
                            ////cmdInsertIntoTask.Parameters.AddWithValue("@AssignedTo", AssignedToStaffInfo.UserId);
                            ////cmdInsertIntoAssignTaskToManager.Parameters.AddWithValue("@AssignedTo", staffInfo.UserId);
                            //cmdInsertIntoAssignTaskToManager.Parameters.AddWithValue("@AssignedTo", nDepartmentManagerId);
                            //cmdInsertIntoAssignTaskToManager.Parameters.AddWithValue("@SendingDepartment", LoggedInUserInfo.departmentInfo.DepartmentId);
                            ////cmdInsertIntoTask.Parameters.AddWithValue("@ReceivingDepartment", AssignedToStaffInfo.departmentInfo.DepartmentId);
                            //cmdInsertIntoAssignTaskToManager.Parameters.AddWithValue("@ReceivingDepartment", staffInfo.departmentInfo.DepartmentId);
                            //cmdInsertIntoAssignTaskToManager.Parameters.AddWithValue("@Subject", Subject);
                            //cmdInsertIntoAssignTaskToManager.Parameters.AddWithValue("@DueDate", DueDate);
                            //cmdInsertIntoAssignTaskToManager.Parameters.AddWithValue("@RelatedTo", comboTaskRelatedTo.SelectedIndex);
                            //cmdInsertIntoAssignTaskToManager.Parameters.AddWithValue("@CreateDate", DateTime.Now);
                            //cmdInsertIntoAssignTaskToManager.Parameters.AddWithValue("@CreatedById", LoggedInUserInfo.UserId);
                            //cmdInsertIntoAssignTaskToManager.Parameters.AddWithValue("@LastModifiedDate", DateTime.Now);
                            //cmdInsertIntoAssignTaskToManager.Parameters.AddWithValue("@LastModifiedById", LoggedInUserInfo.UserId);
                            //cmdInsertIntoAssignTaskToManager.Parameters.AddWithValue("@ActivityDate", DateTime.Now);
                            //cmdInsertIntoAssignTaskToManager.Parameters.AddWithValue("@Comment", Comment);
                            //cmdInsertIntoAssignTaskToManager.Parameters.AddWithValue("@Solution", Solution);
                            //cmdInsertIntoAssignTaskToManager.Parameters.AddWithValue("@Status", ts);
                            //cmdInsertIntoAssignTaskToManager.Parameters.AddWithValue("@Priority", tp);
                            //cmdInsertIntoAssignTaskToManager.Parameters.AddWithValue("@IsClosed", 0);
                            //if (chkReminder.Checked) cmdInsertIntoAssignTaskToManager.Parameters.AddWithValue("@ReminderDateTime", Reminder);
                            //else cmdInsertIntoAssignTaskToManager.Parameters.AddWithValue("@ReminderDateTime", DBNull.Value);
                            //cmdInsertIntoAssignTaskToManager.Parameters.AddWithValue("@IsReminderSet", chkReminder.Checked);
                            //cmdInsertIntoAssignTaskToManager.Parameters.AddWithValue("@IsArchived", 0);
                            //cmdInsertIntoAssignTaskToManager.Parameters.AddWithValue("@ManagerTaskId", TaskIdInserted.Value);

                            //if (connRN.State != ConnectionState.Closed)
                            //{
                            //    connRN.Close();
                            //    connRN.Open();
                            //}
                            //else if (connRN.State == ConnectionState.Closed) connRN.Open();
                            //int nManagerTaskInserted = cmdInsertIntoAssignTaskToManager.ExecuteNonQuery();
                            //if (connRN.State != ConnectionState.Closed) connRN.Close();
                        }
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
            else if (nTaskId == null && nOriginalTask_Id != null && taskMode == TaskMode.Reply) // reply to task creator
            {
                //String Subject = txtTaskSubject.Text.Trim();
                //DateTime DueDate = dtpTaskDueDate.Value;

                //String RelatedTableName = comboTaskRelatedTo.SelectedItem.ToString();
                //String WhatId = txtTaskRelatedTo.Text.Trim();         // Related Id
                //String WhoId = txtIndividualId.Text.Trim();             // Related name
                //String WhoName = txtNameOnTask.Text.Trim();             // Individual Name

                //String Comment = txtTaskComments.Text.Trim();
                //String Solution = txtTaskSolution.Text.Trim();

                //TaskStatus ts = (TaskStatus)comboTaskStatus.SelectedIndex;
                //TaskPriority tp = (TaskPriority)comboTaskPriority.SelectedIndex;

                //String strDate = String.Empty;
                //String strTmpTime = String.Empty;
                //String strTime = String.Empty;
                //String TmpTime = String.Empty;
                //DateTime? Reminder = null;

                //if (LoggedInUserInfo.UserId == null) return;

                //String strSqlQueryForLoggedInUserInfo = "select [dbo].[tbl_user].[User_Name], [dbo].[tbl_user].[User_Role_Id], [dbo].[tbl_user].[Department_Id], [dbo].[tbl_user].[User_Email] " +
                //                                        "from [dbo].[tbl_user] " +
                //                                        "where [dbo].[tbl_user].[User_Id] = @LoggedInUserId";

                //SqlCommand cmdQueryForLoggedInUserInfo = new SqlCommand(strSqlQueryForLoggedInUserInfo, connRN);
                //cmdQueryForLoggedInUserInfo.CommandType = CommandType.Text;

                //cmdQueryForLoggedInUserInfo.Parameters.AddWithValue("@LoggedInUserId", LoggedInUserInfo.UserId);

                //if (connRN.State != ConnectionState.Closed)
                //{
                //    connRN.Close();
                //    connRN.Open();
                //}
                //else if (connRN.State == ConnectionState.Closed) connRN.Open();
                //SqlDataReader rdrLoggedInUserInfo = cmdQueryForLoggedInUserInfo.ExecuteReader();
                //if (rdrLoggedInUserInfo.HasRows)
                //{
                //    rdrLoggedInUserInfo.Read();
                //    if (!rdrLoggedInUserInfo.IsDBNull(0)) LoggedInUserInfo.UserName = rdrLoggedInUserInfo.GetString(0);
                //    if (!rdrLoggedInUserInfo.IsDBNull(1)) LoggedInUserInfo.UserRoleId = (UserRole)rdrLoggedInUserInfo.GetInt16(1);
                //    if (!rdrLoggedInUserInfo.IsDBNull(2)) LoggedInUserInfo.departmentInfo.DepartmentId = (Department)rdrLoggedInUserInfo.GetInt16(2);
                //    if (!rdrLoggedInUserInfo.IsDBNull(3)) LoggedInUserInfo.UserEmail = rdrLoggedInUserInfo.GetString(3);
                //}
                //rdrLoggedInUserInfo.Close();
                //if (connRN.State != ConnectionState.Closed) connRN.Close();

                ////String strReplyToName = txtAssignmentHistory.Text?.Trim();
                //String strReplyToName = txtTaskCreator.Text?.Trim();

                //if (strReplyToName == null || strReplyToName == String.Empty) return;

                //UserInfo replyToUserInfo = new UserInfo();

                //String strSqlQueryForReplyToUserInfo = "select [dbo].[tbl_user].[User_Id], [dbo].[tbl_user].[Department_Id], [dbo].[tbl_user].[User_Role_Id] from [dbo].[tbl_user] " +
                //                                       "where [dbo].[tbl_user].[User_Name] = @UserName";

                //SqlCommand cmdQueryForReplyToUserInfo = new SqlCommand(strSqlQueryForReplyToUserInfo, connRN);
                //cmdQueryForReplyToUserInfo.CommandType = CommandType.Text;

                //cmdQueryForReplyToUserInfo.Parameters.AddWithValue("@UserName", strReplyToName);

                //if (connRN.State != ConnectionState.Closed)
                //{
                //    connRN.Close();
                //    connRN.Open();
                //}
                //else if (connRN.State == ConnectionState.Closed) connRN.Open();
                //SqlDataReader rdrReplyToUserInfo = cmdQueryForReplyToUserInfo.ExecuteReader();
                //if (rdrReplyToUserInfo.HasRows)
                //{
                //    rdrReplyToUserInfo.Read();
                //    if (!rdrReplyToUserInfo.IsDBNull(0)) replyToUserInfo.UserId = rdrReplyToUserInfo.GetInt16(0);
                //    if (!rdrReplyToUserInfo.IsDBNull(1)) replyToUserInfo.departmentInfo.DepartmentId = (Department)rdrReplyToUserInfo.GetInt16(1);
                //    if (!rdrReplyToUserInfo.IsDBNull(2)) replyToUserInfo.UserRoleId = (UserRole)rdrReplyToUserInfo.GetInt16(2);
                //}
                //rdrReplyToUserInfo.Close();
                //if (connRN.State != ConnectionState.Closed) connRN.Close();

                //int OriginalTaskId = nOriginalTask_Id.Value;
                //int TaskReplySenderId = nTaskReplySender_Id;
                //int TaskReplyAssignedToId = nTaskReplyAssigned_to_Id;

                //if (chkReminder.Checked)
                //{
                //    strDate = dtpReminderDatePicker.Value.ToString("MM/dd/yyyy");
                //    strTmpTime = comboReminderTimePicker.SelectedItem.ToString();
                //    strTime = String.Empty;
                //    TmpTime = strTmpTime.Substring(strTmpTime.Length - 2, 2);

                //    if (strTmpTime.Substring(strTmpTime.Length - 2, 2) == "PM")
                //    {
                //        int nTime = Int16.Parse(strTmpTime.Substring(0, strTmpTime.IndexOf(':')));
                //        nTime += 12;
                //        strTime = nTime.ToString() + ':' + strTmpTime.Substring(strTmpTime.IndexOf(':') + 1, 2) + " AM";
                //    }
                //    else strTime = strTmpTime;

                //    int Year = Int16.Parse(strDate.Substring(6, 4));
                //    int Month = Int16.Parse(strDate.Substring(0, 2));
                //    int Day = Int16.Parse(strDate.Substring(3, 2));
                //    String Hour = strTime.Substring(0, strTime.IndexOf(':'));
                //    String Minute = strTime.Substring(strTime.IndexOf(':') + 1, 2);

                //    Reminder = new DateTime(Year, Month, Day, Int16.Parse(Hour), Int16.Parse(Minute), 0);
                //}

                //Cursor.Current = Cursors.WaitCursor;
                //Boolean bTaskSent = true;


                //String strSqlInsertReplyTask = "insert into [dbo].[tbl_task] ([dbo].[tbl_task].[whoid], [dbo].[tbl_task].[IndividualName], [dbo].[tbl_task].[whatid], [dbo].[tbl_task].[IsDeleted], " +
                //                                  "[dbo].[tbl_task].[AssignedTo], [dbo].[tbl_task].[SendingDepartment], [dbo].[tbl_task].[ReceivingDepartment], " +
                //                                  "[dbo].[tbl_task].[Subject], [dbo].[tbl_task].[DueDate], [dbo].[tbl_task].[RelatedToTableId], " +
                //                                  "[dbo].[tbl_task].[CreateDate], [dbo].[tbl_task].[CreatedById], [dbo].[tbl_task].[LastModifiedDate], [dbo].[tbl_task].[LastModifiedById], " +
                //                                  "[dbo].[tbl_task].[ActivityDate], " +
                //                                  "[dbo].[tbl_task].[Comment], [dbo].[tbl_task].[Solution], [dbo].[tbl_task].[Status], [dbo].[tbl_task].[Priority], " +
                //                                  "[dbo].[tbl_task].[IsClosed], [dbo].[tbl_task].[ReminderDateTime], [dbo].[tbl_task].[IsReminderSet], [dbo].[tbl_task].[IsArchived], " +
                //                                  "[dbo].[tbl_task].[OriginalTaskId], [dbo].[tbl_task].[ReplyToStaffId]) " +
                //                                  "values (@WhoId, @IndividualName, @WhatId, 0, @AssignedTo, @SendingDepartment, @ReceivingDepartment, " +
                //                                  "@Subject, @DueDate, @RelatedTo, @CreateDate, @CreatedById, " +
                //                                  "@LastModifiedDate, @LastModifiedById, @ActivityDate, " +
                //                                  "@Comment, @Solution, @Status, @Priority, @IsClosed, @ReminderDateTime, @IsReminderSet, @IsArchived," +
                //                                  "@OriginalTaskId, @ReplyToStaffId)";

                //SqlCommand cmdInsertReplyTask = new SqlCommand(strSqlInsertReplyTask, connRN);
                //cmdInsertReplyTask.CommandType = CommandType.Text;

                //cmdInsertReplyTask.Parameters.AddWithValue("@WhoId", WhoId);
                //cmdInsertReplyTask.Parameters.AddWithValue("@IndividualName", WhoName);
                //cmdInsertReplyTask.Parameters.AddWithValue("@WhatId", WhatId);
                //cmdInsertReplyTask.Parameters.AddWithValue("@AssignedTo", replyToUserInfo.UserId);
                //cmdInsertReplyTask.Parameters.AddWithValue("@SendingDepartment", LoggedInUserInfo.departmentInfo.DepartmentId);
                //cmdInsertReplyTask.Parameters.AddWithValue("@ReceivingDepartment", replyToUserInfo.departmentInfo.DepartmentId);
                //cmdInsertReplyTask.Parameters.AddWithValue("@Subject", Subject);
                //cmdInsertReplyTask.Parameters.AddWithValue("@DueDate", DueDate);
                //cmdInsertReplyTask.Parameters.AddWithValue("@RelatedTo", comboTaskRelatedTo.SelectedIndex);
                //cmdInsertReplyTask.Parameters.AddWithValue("@CreateById", LoggedInUserInfo.UserId);
                //cmdInsertReplyTask.Parameters.AddWithValue("@CreateDate", DateTime.Now);
                //cmdInsertReplyTask.Parameters.AddWithValue("@CreatedById", LoggedInUserInfo.UserId);
                //cmdInsertReplyTask.Parameters.AddWithValue("@LastModifiedDate", DateTime.Now);
                //cmdInsertReplyTask.Parameters.AddWithValue("@LastModifiedById", LoggedInUserInfo.UserId);
                //cmdInsertReplyTask.Parameters.AddWithValue("@ActivityDate", DateTime.Now);
                //cmdInsertReplyTask.Parameters.AddWithValue("@Comment", Comment);
                //cmdInsertReplyTask.Parameters.AddWithValue("@Solution", Solution);
                //cmdInsertReplyTask.Parameters.AddWithValue("@Status", ts);
                //cmdInsertReplyTask.Parameters.AddWithValue("@Priority", tp);
                //cmdInsertReplyTask.Parameters.AddWithValue("@IsClosed", 0);
                //if (chkReminder.Checked) cmdInsertReplyTask.Parameters.AddWithValue("@ReminderDateTime", Reminder);
                //else cmdInsertReplyTask.Parameters.AddWithValue("@ReminderDateTime", DBNull.Value);
                //cmdInsertReplyTask.Parameters.AddWithValue("@IsReminderSet", chkReminder.Checked);
                //cmdInsertReplyTask.Parameters.AddWithValue("@IsArchived", 0);
                //cmdInsertReplyTask.Parameters.AddWithValue("@OriginalTaskId", nOriginalTask_Id.Value);
                //cmdInsertReplyTask.Parameters.AddWithValue("@ReplyToStaffId", replyToUserInfo.UserId);

                //if (connRN.State != ConnectionState.Closed)
                //{
                //    connRN.Close();
                //    connRN.Open();
                //}
                //else if (connRN.State == ConnectionState.Closed) connRN.Open();
                //int nReplyTaskInserted = cmdInsertReplyTask.ExecuteNonQuery();
                //if (connRN.State != ConnectionState.Closed) connRN.Close();

                //if (nReplyTaskInserted == 1)
                //{
                //    MessageBox.Show("The reply TASK has been sent.", "Info");
                //    Close();
                //    //return;
                //}
            }
            else // modify the task
            {
                //if (LoggedInUserInfo.UserRoleId == UserRole.FDStaff ||
                //    LoggedInUserInfo.UserRoleId == UserRole.NPStaff ||
                //    LoggedInUserInfo.UserRoleId == UserRole.RNStaff)
                //{
                //    String strSqlUpdateTask = "update [dbo].[tbl_task] set [dbo].[tbl_task].[Solution] = @Solution, [dbo].[tbl_task].[Status] = @Status, [dbo].[tbl_task].[Priority] = @Priority, " +
                //                              "[dbo].[tbl_task].[ActivityDate] = @ActivityDate, [dbo].[tbl_task].[LastModifiedDate] = @LastModifiedDate " +
                //                              "where [dbo].[tbl_task].[id] = @TaskId";

                //    SqlCommand cmdUpdateTask = new SqlCommand(strSqlUpdateTask, connRN);
                //    cmdUpdateTask.CommandType = CommandType.Text;

                //    cmdUpdateTask.Parameters.AddWithValue("@Solution", txtTaskSolution.Text.Trim());
                //    cmdUpdateTask.Parameters.AddWithValue("@Status", (TaskStatus)comboTaskStatus.SelectedIndex);
                //    cmdUpdateTask.Parameters.AddWithValue("@Priority", (TaskPriority)comboTaskPriority.SelectedIndex);
                //    cmdUpdateTask.Parameters.AddWithValue("@ActivityDate", DateTime.Now);
                //    cmdUpdateTask.Parameters.AddWithValue("@LastModifiedDate", DateTime.Now);
                //    cmdUpdateTask.Parameters.AddWithValue("@TaskId", nTaskId);

                //    if (connRN.State != ConnectionState.Closed)
                //    {
                //        connRN.Close();
                //        connRN.Open();
                //    }
                //    else if (connRN.State == ConnectionState.Closed) connRN.Open();
                //    int nUpdatedRow = cmdUpdateTask.ExecuteNonQuery();
                //    if (connRN.State == ConnectionState.Open) connRN.Close();

                //    if (nUpdatedRow == 1)
                //    {
                //        DialogResult = DialogResult.OK;
                //        MessageBox.Show("The task id: " + nTaskId.ToString() + " has been updated.");
                //        Close();
                //    }
                //}

                //if (LoggedInUserInfo.UserRoleId == UserRole.SuperAdmin ||
                //    LoggedInUserInfo.UserRoleId == UserRole.Administrator ||
                //    LoggedInUserInfo.UserRoleId == UserRole.FDManager ||
                //    LoggedInUserInfo.UserRoleId == UserRole.NPManager ||
                //    LoggedInUserInfo.UserRoleId == UserRole.RNManager)
                //{
                //    String NameAssignedTo = txtTaskNameAssignedTo.Text.Trim();
                //    String strSqlQueryForUserInfo = "select [dbo].[tbl_user].[User_Id], [dbo].[tbl_user].[User_Role_Id], [dbo].[tbl_user].[Department_Id] from [dbo].[tbl_user] " +
                //                                          "where [dbo].[tbl_user].[User_Name] = @UserName";

                //    SqlCommand cmdQueryForUserInfo = new SqlCommand(strSqlQueryForUserInfo, connRN);
                //    cmdQueryForUserInfo.CommandType = CommandType.Text;

                //    cmdQueryForUserInfo.Parameters.AddWithValue("@UserName", NameAssignedTo);
                //    UserInfo userInfo = new UserInfo();

                //    if (connRN.State != ConnectionState.Closed)
                //    {
                //        connRN.Close();
                //        connRN.Open();
                //    }
                //    else if (connRN.State == ConnectionState.Closed) connRN.Open();
                //    SqlDataReader rdrUserInfo = cmdQueryForUserInfo.ExecuteReader();
                //    if (rdrUserInfo.HasRows)
                //    {
                //        if (rdrUserInfo.Read())
                //        {
                //            if (!rdrUserInfo.IsDBNull(0)) userInfo.UserId = rdrUserInfo.GetInt16(0);
                //            if (!rdrUserInfo.IsDBNull(1)) userInfo.UserRoleId = (UserRole)rdrUserInfo.GetInt16(1);
                //            if (!rdrUserInfo.IsDBNull(2)) userInfo.departmentInfo.DepartmentId = (Department)rdrUserInfo.GetInt16(2);
                //        }
                //    }
                //    rdrUserInfo.Close();
                //    if (connRN.State == ConnectionState.Open) connRN.Close();

                //    String Subject = txtTaskSubject.Text.Trim();
                //    DateTime DueDate = dtpTaskDueDate.Value;
                //    RelatedToTable relatedToTable = (RelatedToTable)comboTaskRelatedTo.SelectedIndex;
                //    String WhatId = txtTaskRelatedTo.Text.Trim();
                //    String WhoId = txtIndividualId.Text.Trim();
                //    String Comment = txtTaskComments.Text.Trim();
                //    String Solution = txtTaskSolution.Text.Trim();

                //    TaskStatus ts = (TaskStatus)comboTaskStatus.SelectedIndex;
                //    TaskPriority tp = (TaskPriority)comboTaskPriority.SelectedIndex;

                //    String strDate = String.Empty;
                //    String strTmpTime = String.Empty;
                //    String strTime = String.Empty;
                //    String TmpTime = String.Empty;
                //    DateTime? Reminder = null;

                //    if (chkReminder.Checked)
                //    {
                //        strDate = dtpReminderDatePicker.Value.ToString("MM/dd/yyyy");
                //        if (comboReminderTimePicker.SelectedItem != null)
                //        {
                //            strTmpTime = comboReminderTimePicker.SelectedItem.ToString();
                //            TmpTime = strTmpTime.Substring(strTmpTime.Length - 2, 2);

                //            if (strTmpTime.Substring(strTmpTime.Length - 2, 2) == "PM")
                //            {
                //                int nTime = Int16.Parse(strTmpTime.Substring(0, strTmpTime.IndexOf(":")));
                //                nTime += 12;
                //                strTime = nTime.ToString() + ":" + strTmpTime.Substring(strTmpTime.IndexOf(":") + 1, 2) + "AM";
                //            }
                //            else strTime = strTmpTime;
                //        }

                //        int Year = Int16.Parse(strDate.Substring(6, 4));
                //        int Month = Int16.Parse(strDate.Substring(0, 2));
                //        int Day = Int16.Parse(strDate.Substring(3, 2));
                //        String Hour = strTime.Substring(0, strTime.IndexOf(':'));
                //        String Minute = strTime.Substring(strTime.IndexOf(':') + 1, 2);

                //        Reminder = new DateTime(Year, Month, Day, Int16.Parse(Hour), Int16.Parse(Minute), 0);
                //    }

                //    String strSqlUpdateTask = "update [dbo].[tbl_task] set [dbo].[tbl_task].[AssignedTo] = @AssignedTo, " +
                //                                "[dbo].[tbl_task].[Subject] = @TaskSubject, " +
                //                                "[dbo].[tbl_task].[DueDate] = @TaskDueDate, " +
                //                                "[dbo].[tbl_task].[RelatedToTableId] = @RelatedToTableId, " +
                //                                "[dbo].[tbl_task].[whatid] = @WhatId, " +
                //                                "[dbo].[tbl_task].[IndividualName] = @IndividualName, " +
                //                                "[dbo].[tbl_task].[whoid] = @WhoId, " +
                //                                "[dbo].[tbl_task].[Comment] = @TaskComment, " +
                //                                "[dbo].[tbl_task].[Solution] = @TaskSolution, " +
                //                                "[dbo].[tbl_task].[Status] = @TaskStatus, " +
                //                                "[dbo].[tbl_task].[Priority] = @TaskPriority, " +
                //                                "[dbo].[tbl_task].[ActivityDate] = @ActivityDate, " +
                //                                "[dbo].[tbl_task].[IsReminderSet] = @ReminderSet, " +
                //                                "[dbo].[tbl_task].[ReminderDateTime] = @ReminderDateTime, " +
                //                                "[dbo].[tbl_task].[LastModifiedDate] = @LastModifiedDate, " +
                //                                "[dbo].[tbl_task].[LastModifiedById] = @LastModifiedById " +                                                
                //                                "where [dbo].[tbl_task].[id] = @TaskId";

                //    SqlCommand cmdUpdateTask = new SqlCommand(strSqlUpdateTask, connRN);
                //    cmdUpdateTask.CommandType = CommandType.Text;

                //    cmdUpdateTask.Parameters.AddWithValue("@AssignedTo", userInfo.UserId);
                //    cmdUpdateTask.Parameters.AddWithValue("@TaskSubject", Subject);
                //    cmdUpdateTask.Parameters.AddWithValue("@TaskDueDate", DueDate);
                //    cmdUpdateTask.Parameters.AddWithValue("@RelatedToTableId", (int)relatedToTable);
                //    cmdUpdateTask.Parameters.AddWithValue("@WhatId", WhatId);
                //    cmdUpdateTask.Parameters.AddWithValue("@IndividualName", txtNameOnTask.Text.Trim());
                //    cmdUpdateTask.Parameters.AddWithValue("@WhoId", WhoId);
                //    cmdUpdateTask.Parameters.AddWithValue("@TaskComment", Comment);
                //    cmdUpdateTask.Parameters.AddWithValue("@TaskSolution", Solution);
                //    cmdUpdateTask.Parameters.AddWithValue("@TaskStatus", (int)ts);
                //    cmdUpdateTask.Parameters.AddWithValue("@TaskPriority", (int)tp);
                //    cmdUpdateTask.Parameters.AddWithValue("@ActivityDate", DateTime.Now);
                //    cmdUpdateTask.Parameters.AddWithValue("@ReminderSet", chkReminder.Checked);
                //    if (chkReminder.Checked) cmdUpdateTask.Parameters.AddWithValue("@ReminderDateTime", Reminder);
                //    else cmdUpdateTask.Parameters.AddWithValue("@ReminderDateTime", DBNull.Value);
                //    cmdUpdateTask.Parameters.AddWithValue("@LastModifiedDate", DateTime.Now);
                //    cmdUpdateTask.Parameters.AddWithValue("@LastModifiedById", LoggedInUserInfo.UserId);
                //    cmdUpdateTask.Parameters.AddWithValue("@TaskId", nTaskId);

                //    if (connRN.State != ConnectionState.Closed)
                //    {
                //        connRN.Close();
                //        connRN.Open();
                //    }
                //    else if (connRN.State == ConnectionState.Closed) connRN.Open();
                //    int nRowUpdated = cmdUpdateTask.ExecuteNonQuery();
                //    if (connRN.State == ConnectionState.Open) connRN.Close();

                //    if (nRowUpdated == 1)
                //    {
                //        DialogResult = DialogResult.OK;
                //        MessageBox.Show("The Task id: " + nTaskId + " has been updated successfully.");
                //        Close();
                //    }
                //}
            }
            Close();
        }

        private int AssignTaskToManager(int department_manager_id, UserInfo receiving_staff_info, int task_id_inserted)
        {
            String Subject = txtTaskSubject.Text.Trim();
            DateTime DueDate = dtpTaskDueDate.Value;

            String RelatedTableName = comboTaskRelatedTo.SelectedItem.ToString();
            String WhatId = txtTaskRelatedTo.Text.Trim();         // Related Id
            String WhoId = txtIndividualId.Text.Trim();             // Related name
            String WhoName = txtNameOnTask.Text.Trim();             // Individual Name

            String Comment = txtTaskComments.Text.Trim();
            String Solution = txtTaskSolution.Text.Trim();

            //TaskStatus ts = (TaskStatus)comboTaskStatus.SelectedIndex;

            TaskStatus ts;

            if (comboTaskStatus.SelectedIndex < 3) ts = (TaskStatus)comboTaskStatus.SelectedIndex;
            else ts = (TaskStatus)(comboTaskStatus.SelectedIndex + 3);

            TaskPriority tp = (TaskPriority)comboTaskPriority.SelectedIndex;

            String PhoneNo = txtTaskPhone.Text.Trim();
            String Email = txtTaskEmail.Text.Trim();

            String strDate = String.Empty;
            String strTmpTime = String.Empty;
            String strTime = String.Empty;
            String TmpTime = String.Empty;
            DateTime? Reminder = null;


            String strSqlAssignTaskToManager = "insert into [dbo].[tbl_task] ([dbo].[tbl_task].[whoid], [dbo].[tbl_task].[IndividualName], " +
                    "[dbo].[tbl_task].[whatid], [dbo].[tbl_task].[IsDeleted], " +
                      "[dbo].[tbl_task].[AssignedTo], [dbo].[tbl_task].[SendingDepartment], [dbo].[tbl_task].[ReceivingDepartment], " +
                      "[dbo].[tbl_task].[Subject], [dbo].[tbl_task].[DueDate], [dbo].[tbl_task].[RelatedToTableId], " +
                      "[dbo].[tbl_task].[CreateDate], [dbo].[tbl_task].[CreatedById], [dbo].[tbl_task].[LastModifiedDate], [dbo].[tbl_task].[LastModifiedById], " +
                      "[dbo].[tbl_task].[ActivityDate], " +
                      "[dbo].[tbl_task].[Comment], [dbo].[tbl_task].[Solution], [dbo].[tbl_task].[Status], [dbo].[tbl_task].[Priority], " +
                      "[dbo].[tbl_task].[PhoneNo], [dbo].[tbl_task].[Email], " +
                      "[dbo].[tbl_task].[IsClosed], [dbo].[tbl_task].[ReminderDateTime], [dbo].[tbl_task].[IsReminderSet], [dbo].[tbl_task].[IsArchived], " +
                      "[dbo].[tbl_task].[ManagerTaskId]) " +
                      //"output inserted.id " +
                      "values (@WhoId, @IndividualName, @WhatId, 0, @AssignedTo, @SendingDepartment, @ReceivingDepartment, " +
                      "@Subject, @DueDate, @RelatedTo, @CreateDate, @CreatedById, " +
                      "@LastModifiedDate, @LastModifiedById, @ActivityDate, " +
                      "@Comment, @Solution, @Status, @Priority, @PhoneNo, @Email, " +
                      "@IsClosed, @ReminderDateTime, @IsReminderSet, @IsArchived, @ManagerTaskId)";

            SqlCommand cmdInsertIntoAssignTaskToManager = new SqlCommand(strSqlAssignTaskToManager, connRN);
            cmdInsertIntoAssignTaskToManager.CommandType = CommandType.Text;

            cmdInsertIntoAssignTaskToManager.Parameters.AddWithValue("@WhoId", WhoId);
            cmdInsertIntoAssignTaskToManager.Parameters.AddWithValue("@IndividualName", WhoName);
            cmdInsertIntoAssignTaskToManager.Parameters.AddWithValue("@WhatId", WhatId);
            //cmdInsertIntoTask.Parameters.AddWithValue("@AssignedTo", AssignedToStaffInfo.UserId);
            //cmdInsertIntoAssignTaskToManager.Parameters.AddWithValue("@AssignedTo", staffInfo.UserId);
            cmdInsertIntoAssignTaskToManager.Parameters.AddWithValue("@AssignedTo", department_manager_id);
            cmdInsertIntoAssignTaskToManager.Parameters.AddWithValue("@SendingDepartment", LoggedInUserInfo.departmentInfo.DepartmentId);
            //cmdInsertIntoTask.Parameters.AddWithValue("@ReceivingDepartment", AssignedToStaffInfo.departmentInfo.DepartmentId);
            //cmdInsertIntoAssignTaskToManager.Parameters.AddWithValue("@ReceivingDepartment", staffInfo.departmentInfo.DepartmentId);
            cmdInsertIntoAssignTaskToManager.Parameters.AddWithValue("@ReceivingDepartment", receiving_staff_info.departmentInfo.DepartmentId);
            cmdInsertIntoAssignTaskToManager.Parameters.AddWithValue("@Subject", Subject);
            cmdInsertIntoAssignTaskToManager.Parameters.AddWithValue("@DueDate", DueDate);
            cmdInsertIntoAssignTaskToManager.Parameters.AddWithValue("@RelatedTo", comboTaskRelatedTo.SelectedIndex);
            cmdInsertIntoAssignTaskToManager.Parameters.AddWithValue("@CreateDate", DateTime.Now);
            cmdInsertIntoAssignTaskToManager.Parameters.AddWithValue("@CreatedById", LoggedInUserInfo.UserId);
            cmdInsertIntoAssignTaskToManager.Parameters.AddWithValue("@LastModifiedDate", DateTime.Now);
            cmdInsertIntoAssignTaskToManager.Parameters.AddWithValue("@LastModifiedById", LoggedInUserInfo.UserId);
            cmdInsertIntoAssignTaskToManager.Parameters.AddWithValue("@ActivityDate", DateTime.Now);
            cmdInsertIntoAssignTaskToManager.Parameters.AddWithValue("@Comment", Comment);
            cmdInsertIntoAssignTaskToManager.Parameters.AddWithValue("@Solution", Solution);
            cmdInsertIntoAssignTaskToManager.Parameters.AddWithValue("@Status", ts);
            cmdInsertIntoAssignTaskToManager.Parameters.AddWithValue("@Priority", tp);
            cmdInsertIntoAssignTaskToManager.Parameters.AddWithValue("@PhoneNo", PhoneNo);
            cmdInsertIntoAssignTaskToManager.Parameters.AddWithValue("@Email", Email);
            cmdInsertIntoAssignTaskToManager.Parameters.AddWithValue("@IsClosed", 0);
            if (chkReminder.Checked) cmdInsertIntoAssignTaskToManager.Parameters.AddWithValue("@ReminderDateTime", Reminder);
            else cmdInsertIntoAssignTaskToManager.Parameters.AddWithValue("@ReminderDateTime", DBNull.Value);
            cmdInsertIntoAssignTaskToManager.Parameters.AddWithValue("@IsReminderSet", chkReminder.Checked);
            cmdInsertIntoAssignTaskToManager.Parameters.AddWithValue("@IsArchived", 0);
            cmdInsertIntoAssignTaskToManager.Parameters.AddWithValue("@ManagerTaskId", task_id_inserted);

            if (connRN.State != ConnectionState.Closed)
            {
                connRN.Close();
                connRN.Open();
            }
            else if (connRN.State == ConnectionState.Closed) connRN.Open();
            int nManagerTaskInserted = cmdInsertIntoAssignTaskToManager.ExecuteNonQuery();
            if (connRN.State != ConnectionState.Closed) connRN.Close();

            return nManagerTaskInserted;
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
            TaskType? NewTaskType = null;

            String creator_name = txtTaskCreator.Text.Trim();
            String assigned_to_name = txtTaskNameAssignedTo.Text.Trim();

            if (creator_name == null || creator_name == String.Empty) return;
            if (assigned_to_name == null || assigned_to_name == String.Empty) return;

            UserInfo taskReplySenderInfo = new UserInfo();

            String strSqlQueryForTaskSenderInfo = "select [dbo].[tbl_user].[User_Id], [dbo].[tbl_user].[User_Name], [dbo].[tbl_user].[User_Email], " +
                                                "[dbo].[tbl_user].[User_Role_Id], [dbo].[tbl_user].[Department_Id] " +
                                                "from [dbo].[tbl_user] where [dbo].[tbl_user].[User_Name] = @CreatorName";

            SqlCommand cmdQueryForTaskSenderInfo = new SqlCommand(strSqlQueryForTaskSenderInfo, connRN);
            cmdQueryForTaskSenderInfo.CommandType = CommandType.Text;

            cmdQueryForTaskSenderInfo.Parameters.AddWithValue("@CreatorName", assigned_to_name);

            if (connRN.State != ConnectionState.Closed)
            {
                connRN.Close();
                connRN.Open();
            }
            else if (connRN.State == ConnectionState.Closed) connRN.Open();
            //Object objTaskCreatorId = cmdQueryForTaskSenderId.ExecuteScalar();
            SqlDataReader rdrTaskSenderInfo = cmdQueryForTaskSenderInfo.ExecuteReader();
            if (rdrTaskSenderInfo.HasRows)
            {
                rdrTaskSenderInfo.Read();
                if (!rdrTaskSenderInfo.IsDBNull(0)) taskReplySenderInfo.UserId = rdrTaskSenderInfo.GetInt16(0);
                if (!rdrTaskSenderInfo.IsDBNull(1)) taskReplySenderInfo.UserName = rdrTaskSenderInfo.GetString(1);
                if (!rdrTaskSenderInfo.IsDBNull(2)) taskReplySenderInfo.UserEmail = rdrTaskSenderInfo.GetString(2);
                if (!rdrTaskSenderInfo.IsDBNull(3)) taskReplySenderInfo.UserRoleId = (UserRole)rdrTaskSenderInfo.GetInt16(3);
                if (!rdrTaskSenderInfo.IsDBNull(4)) taskReplySenderInfo.departmentInfo.DepartmentId = (Department)rdrTaskSenderInfo.GetInt16(4);
            }
            rdrTaskSenderInfo.Close();
            if (connRN.State != ConnectionState.Closed) connRN.Close();

            //int? nTaskCreatorId = null;

            //if (objTaskCreatorId != null)
            //{
            //    int nResultTaskCreator;
            //    if (Int32.TryParse(objTaskCreatorId.ToString(), out nResultTaskCreator)) nTaskCreatorId = nResultTaskCreator;
            //}

            //if (nTaskCreatorId == null) return;
            UserInfo taskReplyReceiverInfo = new UserInfo();

            String strSqlQueryForTaskAssignedToUserInfo = "select [dbo].[tbl_user].[User_Id], [dbo].[tbl_user].[User_Name], [dbo].[tbl_user].[User_Email], " +
                                                    "[dbo].[tbl_user].[User_Role_Id], [dbo].[tbl_user].[Department_Id] " + 
                                                    "from [dbo].[tbl_user] where [dbo].[tbl_user].[User_Name] = @AssignedToName";

            SqlCommand cmdQueryForTaskAssignedToUserInfo = new SqlCommand(strSqlQueryForTaskAssignedToUserInfo, connRN);
            cmdQueryForTaskAssignedToUserInfo.CommandType = CommandType.Text;

            cmdQueryForTaskAssignedToUserInfo.Parameters.AddWithValue("@AssignedToName", creator_name);

            if (connRN.State != ConnectionState.Closed)
            {
                connRN.Close();
                connRN.Open();
            }
            else if (connRN.State == ConnectionState.Closed) connRN.Open();
            //Object objTaskAssignedToId = cmdQueryForTaskAssignedToUserInfo.ExecuteScalar();
            SqlDataReader rdrTaskAssignedToUserInfo = cmdQueryForTaskAssignedToUserInfo.ExecuteReader();
            if (rdrTaskAssignedToUserInfo.HasRows)
            {
                rdrTaskAssignedToUserInfo.Read();
                if (!rdrTaskAssignedToUserInfo.IsDBNull(0)) taskReplyReceiverInfo.UserId = rdrTaskAssignedToUserInfo.GetInt16(0);
                if (!rdrTaskAssignedToUserInfo.IsDBNull(1)) taskReplyReceiverInfo.UserName = rdrTaskAssignedToUserInfo.GetString(1);
                if (!rdrTaskAssignedToUserInfo.IsDBNull(2)) taskReplyReceiverInfo.UserEmail = rdrTaskAssignedToUserInfo.GetString(2);
                if (!rdrTaskAssignedToUserInfo.IsDBNull(3)) taskReplyReceiverInfo.UserRoleId = (UserRole)rdrTaskAssignedToUserInfo.GetInt16(3);
                if (!rdrTaskAssignedToUserInfo.IsDBNull(4)) taskReplyReceiverInfo.departmentInfo.DepartmentId = (Department)rdrTaskAssignedToUserInfo.GetInt16(4);
            }
            rdrTaskAssignedToUserInfo.Close();
            if (connRN.State != ConnectionState.Closed) connRN.Close();

            //int? nTaskAssignedToId = null;

            //if (objTaskAssignedToId != null)
            //{
            //    int nResultTaskAssignedTo;
            //    if (Int32.TryParse(objTaskAssignedToId.ToString(), out nResultTaskAssignedTo)) nTaskAssignedToId = nResultTaskAssignedTo;
            //}

            //if (nTaskAssignedToId == null) return;

            //nTaskId.Value;
            String Subject = txtTaskSubject.Text.Trim();
            DateTime? DueDate = dtpTaskDueDate.Value;
            if (DueDate == null) return;

            String RelatedTableName = comboTaskRelatedTo.SelectedItem?.ToString();
            if (RelatedTableName == null) return;

            String WhatId = txtTaskRelatedTo.Text?.Trim();
            if (WhatId == null) return;

            String WhoId = txtIndividualId.Text?.Trim();
            if (WhoId == null) return;

            String WhoName = txtNameOnTask.Text?.Trim();
            if (WhoName == null) return;

            String Comment = txtTaskComments.Text?.Trim();
            if (Comment == null) return;

            String Solution = txtTaskSolution.Text?.Trim();
            if (Solution == null) return;

            //TaskStatus ts = (TaskStatus)comboTaskStatus.SelectedIndex;
            TaskStatus ts;

            if (comboTaskStatus.SelectedIndex < 3) ts = (TaskStatus)comboTaskStatus.SelectedIndex;
            else ts = (TaskStatus)(comboTaskStatus.SelectedIndex + 3);

            TaskPriority tp = (TaskPriority)comboTaskPriority.SelectedIndex;

            String PhoneNo = txtTaskPhone.Text.Trim();
            String Email = txtTaskEmail.Text.Trim();

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

                int Year = Int16.Parse(strDate.Substring(6, 4));
                int Month = Int16.Parse(strDate.Substring(0, 2));
                int Day = Int16.Parse(strDate.Substring(3, 2));
                String Hour = strTime.Substring(0, strTime.IndexOf(':'));
                String Minute = strTime.Substring(strTime.IndexOf(':') + 1, 2);

                Reminder = new DateTime(Year, Month, Day, Int16.Parse(Hour), Int16.Parse(Minute), 0);
            }

            Boolean bTaskReplyReceiverMSManagerReceived = false;
            Boolean bTaskReplyReceiverNPManagerReceived = false;
            Boolean bTaskReplyReceiverRNManagerReceived = false;
            Boolean bTaskReplyReceiverFDManagerReceived = false;

            Boolean bTaskReplySenderMSManagerReceived = false;
            Boolean bTaskReplySenderNPManagerReceived = false;
            Boolean bTaskReplySenderRNManagerReceived = false;
            Boolean bTaskReplySenderFDManagerReceived = false;

            

            String strSqlReplyToTask = "update [dbo].[tbl_task] set [dbo].[tbl_task].[Replied] = 1, " +
                                       "[dbo].[tbl_task].[CreatedById] = @Replier, " +
                                       "[dbo].[tbl_task].[AssignedTo] = @ReplyTo, " +
                                       "[dbo].[tbl_task].[ReplyToStaffId] = @ReplyToStaffId, " +                                       
                                       "[dbo].[tbl_task].[whoid] = @WhoId, " +
                                       "[dbo].[tbl_task].[IndividualName] = @WhoName, " +
                                       "[dbo].[tbl_task].[WhatId] = @WhatId, " +
                                       "[dbo].[tbl_task].[SendingDepartment] = @SendingDepartment, " +
                                       "[dbo].[tbl_task].[ReceivingDepartment] = @ReceivingDepartment, " +
                                       "[dbo].[tbl_task].[Subject] = @Subject, " +
                                       "[dbo].[tbl_task].[DueDate] = @DueDate, " +
                                       "[dbo].[tbl_task].[RelatedToTableId] = @RelatedTo, " +
                                       "[dbo].[tbl_task].[LastModifiedDate] = @LastModifiedDate, " +
                                       "[dbo].[tbl_task].[LastModifiedById] = @LastModifiedById," +
                                       "[dbo].[tbl_task].[ActivityDate] = @ActivityDate, " +
                                       "[dbo].[tbl_task].[Comment] = @Comment, " +
                                       "[dbo].[tbl_task].[Solution] = @Solution, " +
                                       "[dbo].[tbl_task].[Status] = @TaskStatus, " +
                                       "[dbo].[tbl_task].[Priority] = @TaskPriority, " +
                                       "[dbo].[tbl_task].[PhoneNo] = @PhoneNo, " +
                                       "[dbo].[tbl_task].[Email] = @Email, " +
                                       "[dbo].[tbl_task].[ReminderDateTime] = @ReminderDateTime, " +
                                       "[dbo].[tbl_task].[IsReminderSet] = @IsReminderSet " +
                                       "where [dbo].[tbl_task].[id] = @TaskId";

            SqlCommand cmdUpdateTaskForReply = new SqlCommand(strSqlReplyToTask, connRN);
            cmdUpdateTaskForReply.CommandType = CommandType.Text;

            cmdUpdateTaskForReply.Parameters.AddWithValue("@Replier", taskReplySenderInfo.UserId);
            cmdUpdateTaskForReply.Parameters.AddWithValue("@ReplyTo", taskReplyReceiverInfo.UserId);
            cmdUpdateTaskForReply.Parameters.AddWithValue("@ReplyToStaffId", taskReplyReceiverInfo.UserId);
            cmdUpdateTaskForReply.Parameters.AddWithValue("@WhoId", WhoId);
            cmdUpdateTaskForReply.Parameters.AddWithValue("@WhoName", WhoName);
            cmdUpdateTaskForReply.Parameters.AddWithValue("@WhatId", WhatId);
            cmdUpdateTaskForReply.Parameters.AddWithValue("@SendingDepartment", taskReplySenderInfo.departmentInfo.DepartmentId);
            cmdUpdateTaskForReply.Parameters.AddWithValue("@ReceivingDepartment", taskReplyReceiverInfo.departmentInfo.DepartmentId);
            cmdUpdateTaskForReply.Parameters.AddWithValue("@Subject", Subject);
            cmdUpdateTaskForReply.Parameters.AddWithValue("@DueDate", DueDate);
            cmdUpdateTaskForReply.Parameters.AddWithValue("@RelatedTo", comboTaskRelatedTo.SelectedIndex);
            cmdUpdateTaskForReply.Parameters.AddWithValue("@LastModifiedDate", DateTime.Now);
            cmdUpdateTaskForReply.Parameters.AddWithValue("@LastModifiedById", LoggedInUserInfo.UserId);
            cmdUpdateTaskForReply.Parameters.AddWithValue("@ActivityDate", DateTime.Now);
            cmdUpdateTaskForReply.Parameters.AddWithValue("@Comment", Comment);
            cmdUpdateTaskForReply.Parameters.AddWithValue("@Solution", Solution);
            cmdUpdateTaskForReply.Parameters.AddWithValue("@TaskStatus", ts);
            cmdUpdateTaskForReply.Parameters.AddWithValue("@TaskPriority", tp);
            cmdUpdateTaskForReply.Parameters.AddWithValue("@PhoneNo", PhoneNo);
            cmdUpdateTaskForReply.Parameters.AddWithValue("@Email", Email);
            if (chkReminder.Checked) cmdUpdateTaskForReply.Parameters.AddWithValue("@ReminderDateTime", Reminder);
            else cmdUpdateTaskForReply.Parameters.AddWithValue("@ReminderDateTime", DBNull.Value);
            cmdUpdateTaskForReply.Parameters.AddWithValue("@IsReminderSet", chkReminder.Checked);
            cmdUpdateTaskForReply.Parameters.AddWithValue("@TaskId", nTaskId.Value);

            if (connRN.State != ConnectionState.Closed)
            {
                connRN.Close();
                connRN.Open();
            }
            else if (connRN.State == ConnectionState.Closed) connRN.Open();
            int nTaskReplied = cmdUpdateTaskForReply.ExecuteNonQuery();
            if (connRN.State != ConnectionState.Closed) connRN.Close();

            if (nTaskReplied == 1)
            {
                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ///
                TaskUserInfo TaskReplySenderInfo = new TaskUserInfo();
                TaskUserInfo TaskReplyReceiverInfo = new TaskUserInfo();

                String strSqlQueryForTaskReplySenderInfo = "select [dbo].[tbl_user].[Task_Role_Id] from [dbo].[tbl_user] " +
                                                           "where [dbo].[tbl_user].[User_Id] = @TaskReplySenderId and " +
                                                           "([dbo].[tbl_user].[IsDeleted] = 0 or [dbo].[tbl_user].[IsDeleted] IS NULL)";

                SqlCommand cmdQueryForTaskReplySenderInfo = new SqlCommand(strSqlQueryForTaskReplySenderInfo, connRN);
                cmdQueryForTaskReplySenderInfo.CommandType = CommandType.Text;

                cmdQueryForTaskReplySenderInfo.Parameters.AddWithValue("@TaskReplySenderId", LoggedInUserInfo.UserId);

                if (connRN.State != ConnectionState.Closed)
                {
                    connRN.Close();
                    connRN.Open();
                }
                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                Object objTaskReplySenderRoleId = cmdQueryForTaskReplySenderInfo.ExecuteScalar();
                if (connRN.State != ConnectionState.Closed) connRN.Close();

                TaskReplySenderInfo.UserId = LoggedInUserInfo.UserId;
                TaskReplySenderInfo.UserName = LoggedInUserInfo.UserName;
                TaskReplySenderInfo.UserEmail = LoggedInUserInfo.UserEmail;
                TaskReplySenderInfo.departmentInfo.DepartmentId = LoggedInUserInfo.departmentInfo.DepartmentId;

                if (objTaskReplySenderRoleId != null)
                {
                    int resultTaskReplySender;
                    if (Int32.TryParse(objTaskReplySenderRoleId.ToString(), out resultTaskReplySender)) TaskReplySenderInfo.TaskUserRoleId = (TaskUserRole)resultTaskReplySender;                            
                }

                String strSqlQueryForTaskReplyReceiverInfo = "select [dbo].[tbl_user].[Task_Role_Id] from [dbo].[tbl_user] " +
                                                             "where [dbo].[tbl_user].[User_Id] = @TaskReplyReceiverId and " +
                                                             "([dbo].[tbl_user].[IsDeleted] = 0 or [dbo].[tbl_user].[IsDeleted] IS NULL)";

                SqlCommand cmdQueryForTaskReplyReceiverInfo = new SqlCommand(strSqlQueryForTaskReplyReceiverInfo, connRN);
                cmdQueryForTaskReplyReceiverInfo.CommandType = CommandType.Text;

                cmdQueryForTaskReplyReceiverInfo.Parameters.AddWithValue("@TaskReplyReceiverId", taskReplyReceiverInfo.UserId);

                if (connRN.State != ConnectionState.Closed)
                {
                    connRN.Close();
                    connRN.Open();
                }
                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                Object objTaskReplyReceiverRoleId = cmdQueryForTaskReplyReceiverInfo.ExecuteScalar();
                if (connRN.State != ConnectionState.Closed) connRN.Close();

                TaskReplyReceiverInfo.UserId = taskReplyReceiverInfo.UserId;
                TaskReplyReceiverInfo.UserName = taskReplyReceiverInfo.UserName;
                TaskReplyReceiverInfo.UserEmail = taskReplyReceiverInfo.UserEmail;
                TaskReplyReceiverInfo.departmentInfo.DepartmentId = taskReplyReceiverInfo.departmentInfo.DepartmentId;

                if (objTaskReplyReceiverRoleId != null)
                {
                    int resultTaskReplyReceiverRole;
                    if (Int32.TryParse(objTaskReplyReceiverRoleId.ToString(), out resultTaskReplyReceiverRole))
                        TaskReplyReceiverInfo.TaskUserRoleId = (TaskUserRole)resultTaskReplyReceiverRole;
                }

                FDManagerTaskIDs FDManagerTaskId = new FDManagerTaskIDs();

                String strSqlQueryForFDManagerId = "select [dbo].[tbl_user].[User_Id] from [dbo].[tbl_user] " +
                                                   "where [dbo].[tbl_user].[Task_Role_Id] = @FDManagerTaskRoleId and " +
                                                   "([dbo].[tbl_user].[IsDeleted] = 0 or [dbo].[tbl_user].[IsDeleted] IS NULL)";

                SqlCommand cmdQueryForFDManagerId = new SqlCommand(strSqlQueryForFDManagerId, connRN);
                cmdQueryForFDManagerId.CommandType = CommandType.Text;

                cmdQueryForFDManagerId.Parameters.AddWithValue("@FDManagerTaskRoleId", (Int16)FDManagerTaskId.FDManagerTaskRoleId);

                if (connRN.State != ConnectionState.Closed)
                {
                    connRN.Close();
                    connRN.Open();
                }
                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                Object objFDManagerTaskUserId = cmdQueryForFDManagerId.ExecuteScalar();
                if (connRN.State != ConnectionState.Closed) connRN.Close();

                if (objFDManagerTaskUserId != null)
                {
                    int resultTaskUserId;
                    if (Int32.TryParse(objFDManagerTaskUserId.ToString(), out resultTaskUserId)) FDManagerTaskId.FDManagerTaskUserId = resultTaskUserId;
                }

                RNManagerTaskIDs RNManagerTaskId = new RNManagerTaskIDs();

                String strSqlQueryForRNManagerTaskId = "select [dbo].[tbl_user].[User_Id] from [dbo].[tbl_user] " +
                                                       "where [dbo].[tbl_user].[Task_Role_Id] = @RNManagerTaskRoleId and " +
                                                       "([dbo].[tbl_user].[IsDeleted] = 0 or [dbo].[tbl_user].[IsDeleted] IS NULL)";

                SqlCommand cmdQueryForRNManagerId = new SqlCommand(strSqlQueryForRNManagerTaskId, connRN);
                cmdQueryForRNManagerId.CommandType = CommandType.Text;

                cmdQueryForRNManagerId.Parameters.AddWithValue("@RNManagerTaskRoleId", (Int16)RNManagerTaskId.RNManagerTaskRoleId);

                if (connRN.State != ConnectionState.Closed)
                {
                    connRN.Close();
                    connRN.Open();
                }
                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                Object objRNManagerTaskUserId = cmdQueryForRNManagerId.ExecuteScalar();
                if (connRN.State != ConnectionState.Closed) connRN.Close();

                if (objRNManagerTaskUserId != null)
                {
                    int resultTaskUserId;
                    if (Int32.TryParse(objRNManagerTaskUserId.ToString(), out resultTaskUserId)) RNManagerTaskId.RNManagerTaskUserId = (Int16)resultTaskUserId;
                }

                NPManagerTaskIDs NPManagerTaskId = new NPManagerTaskIDs();

                String strSqlQueryForNPManagerTaskId = "select [dbo].[tbl_user].[User_Id] from [dbo].[tbl_user] " +
                                                       "where [dbo].[tbl_user].[Task_Role_Id] = @NPManagerTaskRoleId and " +
                                                       "([dbo].[tbl_user].[IsDeleted] = 0 or [dbo].[tbl_user].[IsDeleted] IS NULL)";

                SqlCommand cmdQueryForNPManagerTaskId = new SqlCommand(strSqlQueryForNPManagerTaskId, connRN);
                cmdQueryForNPManagerTaskId.CommandType = CommandType.Text;

                cmdQueryForNPManagerTaskId.Parameters.AddWithValue("@NPManagerTaskRoleId", (Int16)NPManagerTaskId.NPManagerTaskRoleId);

                if (connRN.State != ConnectionState.Closed)
                {
                    connRN.Close();
                    connRN.Open();
                }
                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                Object objNPManagerTaskUserId = cmdQueryForNPManagerTaskId.ExecuteScalar();
                if (connRN.State != ConnectionState.Open) connRN.Close();

                if (objNPManagerTaskUserId != null)
                {
                    int resultTaskUserId;
                    if (Int32.TryParse(objNPManagerTaskUserId.ToString(), out resultTaskUserId)) NPManagerTaskId.NPManagerTaskUserId = (Int16)resultTaskUserId;
                }

                MSManagerTaskIDs MSManagerTaskId = new MSManagerTaskIDs();

                String strSqlQueryForMSManagerTaskId = "select [dbo].[tbl_user].[User_Id] from [dbo].[tbl_user] " +
                                                       "where [dbo].[tbl_user].[Task_Role_Id] = @MSManagerTaskRoleId and " +
                                                       "([dbo].[tbl_user].[IsDeleted] = 0 or [dbo].[tbl_user].[IsDeleted] IS NULL)";

                SqlCommand cmdQueryForMSManagerTaskId = new SqlCommand(strSqlQueryForMSManagerTaskId, connRN);
                cmdQueryForMSManagerTaskId.CommandType = CommandType.Text;

                cmdQueryForMSManagerTaskId.Parameters.AddWithValue("@MSManagerTaskRoleId", (Int16)MSManagerTaskId.MSManagerTaskRoleId);

                if (connRN.State != ConnectionState.Closed)
                {
                    connRN.Close();
                    connRN.Open();
                }
                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                Object objMSManagerTaskUserId = cmdQueryForMSManagerTaskId.ExecuteScalar();
                if (connRN.State != ConnectionState.Closed) connRN.Close();

                if (objMSManagerTaskUserId != null)
                {
                    int resultTaskUserId;
                    if (Int32.TryParse(objMSManagerTaskUserId.ToString(), out resultTaskUserId)) MSManagerTaskId.MSManagerTaskUserId = (Int16)resultTaskUserId;
                }


                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                Department? ReplyingDepartment = null;
                Int32? nReplyReceiverStaffDepartmentManagerId = null;

                //switch (taskReplySenderInfo.departmentInfo.DepartmentId)
                switch (taskReplyReceiverInfo.departmentInfo.DepartmentId)
                {
                    case Department.MemberService:
                        if (!bTaskReplyReceiverMSManagerReceived && 
                            taskReplySenderInfo.UserRoleId != UserRole.MSManager && 
                            taskReplyReceiverInfo.UserRoleId != UserRole.MSManager)
                        {
                            //nReplyReceiverStaffDepartmentManagerId = 18;
                            //AssignTaskToManager(nReplyReceiverStaffDepartmentManagerId.Value, taskReplyReceiverInfo, nTaskId.Value);
                            AssignTaskToManager(MSManagerTaskId.MSManagerTaskUserId.Value, taskReplyReceiverInfo, nTaskId.Value);
                            bTaskReplyReceiverMSManagerReceived = true;
                        }
                        break;
                    case Department.NeedsProcessing:
                        if (!bTaskReplyReceiverNPManagerReceived &&
                            taskReplySenderInfo.UserRoleId != UserRole.NPManager &&
                            taskReplyReceiverInfo.UserRoleId != UserRole.NPManager)
                        {
                            //nReplyReceiverStaffDepartmentManagerId = 9;
                            //AssignTaskToManager(nReplyReceiverStaffDepartmentManagerId.Value, taskReplyReceiverInfo, nTaskId.Value);
                            AssignTaskToManager(NPManagerTaskId.NPManagerTaskUserId.Value, taskReplyReceiverInfo, nTaskId.Value);
                            bTaskReplyReceiverNPManagerReceived = true;
                        }
                        break;
                    case Department.ReviewAndNegotiation:
                        if ((!bTaskReplyReceiverRNManagerReceived &&
                            taskReplySenderInfo.UserRoleId != UserRole.RNManager &&
                            taskReplyReceiverInfo.UserRoleId != UserRole.RNManager) ||
                            (!bTaskReplyReceiverRNManagerReceived &&
                             TaskReplyReceiverInfo.TaskUserRoleId == TaskUserRole.RNAssistantManager &&
                             TaskReplySenderInfo.TaskUserRoleId != TaskUserRole.RNManager))
                        {
                            //nReplyReceiverStaffDepartmentManagerId = 13;
                            //AssignTaskToManager(nReplyReceiverStaffDepartmentManagerId.Value, taskReplyReceiverInfo, nTaskId.Value);
                            AssignTaskToManager(RNManagerTaskId.RNManagerTaskUserId.Value, taskReplyReceiverInfo, nTaskId.Value);
                            bTaskReplyReceiverRNManagerReceived = true;
                        }
                        break;
                    case Department.Finance:
                        if (!bTaskReplyReceiverFDManagerReceived &&
                            taskReplySenderInfo.UserRoleId != UserRole.FDManager &&
                            taskReplyReceiverInfo.UserRoleId != UserRole.FDManager)
                        {
                            //nReplyReceiverStaffDepartmentManagerId = 16;
                            //AssignTaskToManager(nReplyReceiverStaffDepartmentManagerId.Value, taskReplyReceiverInfo, nTaskId.Value);
                            AssignTaskToManager(FDManagerTaskId.FDManagerTaskUserId.Value, taskReplyReceiverInfo, nTaskId.Value);
                            bTaskReplyReceiverFDManagerReceived = true;
                        }
                        break;
                }

                Int32? nReplySenderStaffDepartmentManagerId = null;

                //switch (taskReplyReceiverInfo.departmentInfo.DepartmentId)
                //switch (taskReplySenderInfo.departmentInfo.DepartmentId)
                //switch (LoggedInTaskUserInfo.departmentInfo.DepartmentId)
                switch (LoggedInUserInfo.departmentInfo.DepartmentId)
                {
                    case Department.MemberService:
                        if (!bTaskReplySenderMSManagerReceived &&
                            taskReplySenderInfo.UserRoleId != UserRole.MSManager &&
                            taskReplyReceiverInfo.UserRoleId != UserRole.MSManager &&
                            LoggedInUserInfo.departmentInfo.DepartmentId != taskReplyReceiverInfo.departmentInfo.DepartmentId)
                        {
                            //nReplySenderStaffDepartmentManagerId = 18;
                            //AssignTaskToManager(nReplySenderStaffDepartmentManagerId.Value, taskReplySenderInfo, nTaskId.Value);
                            //AssignTaskToManager(MSManagerTaskId.MSManagerTaskUserId.Value, taskReplySenderInfo, nTaskId.Value);
                            AssignTaskToManager(MSManagerTaskId.MSManagerTaskUserId.Value, LoggedInUserInfo, nTaskId.Value);
                            bTaskReplySenderMSManagerReceived = true;                            
                        }
                        break;
                    case Department.NeedsProcessing:
                        if (!bTaskReplySenderNPManagerReceived &&
                            taskReplySenderInfo.UserRoleId != UserRole.NPManager &&
                            taskReplyReceiverInfo.UserRoleId != UserRole.NPManager &&
                            LoggedInUserInfo.departmentInfo.DepartmentId != taskReplyReceiverInfo.departmentInfo.DepartmentId)
                        {
                            //nReplySenderStaffDepartmentManagerId = 9;
                            //AssignTaskToManager(nReplySenderStaffDepartmentManagerId.Value, taskReplySenderInfo, nTaskId.Value);
                            //AssignTaskToManager(NPManagerTaskId.NPManagerTaskUserId.Value, taskReplySenderInfo, nTaskId.Value);
                            AssignTaskToManager(NPManagerTaskId.NPManagerTaskUserId.Value, LoggedInUserInfo, nTaskId.Value);
                            bTaskReplySenderNPManagerReceived = true;
                        }
                        break;
                    case Department.ReviewAndNegotiation:
                        if ((!bTaskReplySenderRNManagerReceived &&
                            taskReplySenderInfo.UserRoleId != UserRole.RNManager &&
                            taskReplyReceiverInfo.UserRoleId != UserRole.RNManager &&
                            LoggedInUserInfo.departmentInfo.DepartmentId != taskReplyReceiverInfo.departmentInfo.DepartmentId)||
                            (!bTaskReplySenderRNManagerReceived &&
                            TaskReplySenderInfo.TaskUserRoleId == TaskUserRole.RNAssistantManager &&
                            LoggedInUserInfo.departmentInfo.DepartmentId != TaskReplyReceiverInfo.departmentInfo.DepartmentId))
                        {
                            //nReplySenderStaffDepartmentManagerId = 13;
                            //AssignTaskToManager(nReplySenderStaffDepartmentManagerId.Value, taskReplySenderInfo, nTaskId.Value);
                            //AssignTaskToManager(RNManagerTaskId.RNManagerTaskUserId.Value, taskReplySenderInfo, nTaskId.Value);
                            AssignTaskToManager(RNManagerTaskId.RNManagerTaskUserId.Value, LoggedInUserInfo, nTaskId.Value);
                            bTaskReplySenderRNManagerReceived = true;
                        }
                        break;
                    case Department.Finance:
                        if (!bTaskReplySenderFDManagerReceived &&
                            taskReplySenderInfo.UserRoleId != UserRole.FDManager &&
                            taskReplyReceiverInfo.UserRoleId != UserRole.FDManager &&
                            LoggedInUserInfo.departmentInfo.DepartmentId != taskReplyReceiverInfo.departmentInfo.DepartmentId)
                        {
                            //nReplySenderStaffDepartmentManagerId = 16;
                            //AssignTaskToManager(nReplySenderStaffDepartmentManagerId.Value, taskReplySenderInfo, nTaskId.Value);
                            //AssignTaskToManager(FDManagerTaskId.FDManagerTaskUserId.Value, taskReplySenderInfo, nTaskId.Value);
                            AssignTaskToManager(FDManagerTaskId.FDManagerTaskUserId.Value, LoggedInUserInfo, nTaskId.Value);
                            bTaskReplySenderFDManagerReceived = true;
                        }
                        break;
                }


                MessageBox.Show("The task has been replied.", "Info");
                //Close();
                //return;
            }

            //if (nTaskReplied == 1 && (ts == TaskStatus.Completed || ts == TaskStatus.Solved))
            //{
            //    List<int> lstTaskIdSentToManager = new List<int>();

            //    String strSqlQueryForTaskIdsSentToManager = "select [dbo].[tbl_task].[id] from [dbo].[tbl_task] where [dbo].[tbl_task].[ManagerTaskId] = @ManagerTaskId";

            //    SqlCommand cmdQueryForTaskIdsSentToManager = new SqlCommand(strSqlQueryForTaskIdsSentToManager, connRN);
            //    cmdQueryForTaskIdsSentToManager.CommandType = CommandType.Text;

            //    cmdQueryForTaskIdsSentToManager.Parameters.AddWithValue("@ManagerTaskId", nTaskId.Value);

            //    if (connRN.State != ConnectionState.Closed)
            //    {
            //        connRN.Close();
            //        connRN.Open();
            //    }
            //    else if (connRN.State == ConnectionState.Closed) connRN.Open();
            //    SqlDataReader rdrTaskIdSentToManager = cmdQueryForTaskIdsSentToManager.ExecuteReader();
            //    if (rdrTaskIdSentToManager.HasRows)
            //    {
            //        while (rdrTaskIdSentToManager.Read())
            //        {
            //            int? TaskId = null;
            //            if (!rdrTaskIdSentToManager.IsDBNull(0)) TaskId = rdrTaskIdSentToManager.GetInt32(0);
            //            if (TaskId != null) lstTaskIdSentToManager.Add(TaskId.Value);
            //        }
            //    }
            //    rdrTaskIdSentToManager.Close();
            //    if (connRN.State != ConnectionState.Closed) connRN.Close();

            //    foreach (int TaskId in lstTaskIdSentToManager)
            //    {
            //        String strSqlCompleteSolveTaskSentToManager = "update [dbo].[tbl_task] " +
            //                                             "set [dbo].[tbl_task].[whoid] = @WhoId, " +
            //                                             "[dbo].[tbl_task].[IndividualName] = @WhoName, " +
            //                                             "[dbo].[tbl_task].[WhatId] = @WhatId, " +
            //                                             "[dbo].[tbl_task].[Subject] = @Subject, " +
            //                                             "[dbo].[tbl_task].[DueDate] = @DueDate, " +
            //                                             "[dbo].[tbl_task].[RelatedToTableId] = @RelatedTo, " +
            //                                             "[dbo].[tbl_task].[LastModifiedDate] = @LastModifiedDate, " +
            //                                             "[dbo].[tbl_task].[LastModifiedById] = @LastModifiedById, " +
            //                                             "[dbo].[tbl_task].[ActivityDate] = @ActivityDate, " +
            //                                             "[dbo].[tbl_task].[Comment] = @Comment, " +
            //                                             "[dbo].[tbl_task].[Solution] = @Solution, " +
            //                                             "[dbo].[tbl_task].[Status] = @TaskStatus, " +
            //                                             "[dbo].[tbl_task].[Priority] = @TaskPriority, " +
            //                                             "[dbo].[tbl_task].[PhoneNo] = @PhoneNo, " +
            //                                             "[dbo].[tbl_task].[Email] = @Email, " +
            //                                             "[dbo].[tbl_task].[ReminderDateTime] = @ReminderDateTime, " +
            //                                             "[dbo].[tbl_task].[IsReminderSet] = @IsReminderSet " +
            //                                             "where [dbo].[tbl_task].[id] = @TaskId";

            //        SqlCommand cmdSqlCompleteSolveTaskSentToManager = new SqlCommand(strSqlCompleteSolveTaskSentToManager, connRN);
            //        cmdSqlCompleteSolveTaskSentToManager.CommandType = CommandType.Text;

            //        cmdSqlCompleteSolveTaskSentToManager.Parameters.AddWithValue("@WhoId", WhoId);
            //        cmdSqlCompleteSolveTaskSentToManager.Parameters.AddWithValue("@WhoName", WhoName);
            //        cmdSqlCompleteSolveTaskSentToManager.Parameters.AddWithValue("@WhatId", WhatId);
            //        cmdSqlCompleteSolveTaskSentToManager.Parameters.AddWithValue("@Subject", Subject);
            //        cmdSqlCompleteSolveTaskSentToManager.Parameters.AddWithValue("@DueDate", DueDate);
            //        cmdSqlCompleteSolveTaskSentToManager.Parameters.AddWithValue("@RelatedTo", comboTaskRelatedTo.SelectedIndex);
            //        cmdSqlCompleteSolveTaskSentToManager.Parameters.AddWithValue("@LastModifiedDate", DateTime.Now);
            //        cmdSqlCompleteSolveTaskSentToManager.Parameters.AddWithValue("@LastModifiedById", LoggedInUserInfo.UserId);
            //        cmdSqlCompleteSolveTaskSentToManager.Parameters.AddWithValue("@ActivityDate", DateTime.Now);
            //        cmdSqlCompleteSolveTaskSentToManager.Parameters.AddWithValue("@Comment", Comment);
            //        cmdSqlCompleteSolveTaskSentToManager.Parameters.AddWithValue("@Solution", Solution);
            //        cmdSqlCompleteSolveTaskSentToManager.Parameters.AddWithValue("@TaskStatus", ts);
            //        cmdSqlCompleteSolveTaskSentToManager.Parameters.AddWithValue("@TaskPriority", tp);
            //        cmdSqlCompleteSolveTaskSentToManager.Parameters.AddWithValue("@PhoneNo", PhoneNo);
            //        cmdSqlCompleteSolveTaskSentToManager.Parameters.AddWithValue("@Email", Email);
            //        if (chkReminder.Checked) cmdSqlCompleteSolveTaskSentToManager.Parameters.AddWithValue("@ReminderDateTime", Reminder);
            //        else cmdSqlCompleteSolveTaskSentToManager.Parameters.AddWithValue("@ReminderDateTime", DBNull.Value);
            //        cmdSqlCompleteSolveTaskSentToManager.Parameters.AddWithValue("@IsReminderSet", chkReminder.Checked);
            //        cmdSqlCompleteSolveTaskSentToManager.Parameters.AddWithValue("@TaskId", TaskId);

            //        if (connRN.State != ConnectionState.Closed)
            //        {
            //            connRN.Close();
            //            connRN.Open();
            //        }
            //        else if (connRN.State == ConnectionState.Closed) connRN.Open();
            //        cmdSqlCompleteSolveTaskSentToManager.ExecuteNonQuery();
            //        if (connRN.State != ConnectionState.Closed) connRN.Close();

            //    }
            //}

            Close();
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            String StaffAssigned = txtTaskNameAssignedTo.Text?.Trim();

            String strSqlQueryForStaffNameTaskAssigned = "select [dbo].[tbl_user].[User_Name] from [dbo].[tbl_task] " +
                                                         "inner join [dbo].[tbl_user] on [dbo].[tbl_task].[AssignedTo] = [dbo].[tbl_user].[User_Id] " +
                                                         "where [dbo].[tbl_task].[id] = @TaskId";

            SqlCommand cmdQueryForStaffNameTaskAssigned = new SqlCommand(strSqlQueryForStaffNameTaskAssigned, connRN);
            cmdQueryForStaffNameTaskAssigned.CommandType = CommandType.Text;

            cmdQueryForStaffNameTaskAssigned.Parameters.AddWithValue("@TaskId", nTaskId.Value);

            if (connRN.State != ConnectionState.Closed)
            {
                connRN.Close();
                connRN.Open();
            }
            else if (connRN.State == ConnectionState.Closed) connRN.Open();
            Object objStaffNameTaskAssigned = cmdQueryForStaffNameTaskAssigned.ExecuteScalar();
            if (connRN.State != ConnectionState.Closed) connRN.Close();

            String StaffNameTaskAssigned = objStaffNameTaskAssigned?.ToString()?.Trim();

            if (StaffAssigned == StaffNameTaskAssigned)
            {
                MessageBox.Show("Please choose a receiving staff of the task forwarded.", "Alert");
                return;
            }

            String Subject = txtTaskSubject.Text.Trim();
            DateTime? DueDate = dtpTaskDueDate.Value;
            if (DueDate == null) return;

            //String RelatedTableName = comboTaskRelatedTo.SelectedItem?.ToString();
            //if (RelatedTableName == null) return;
            int? nRelatedToTableNameIndex = null;
            if (comboTaskRelatedTo.SelectedIndex != -1) nRelatedToTableNameIndex = comboTaskRelatedTo.SelectedIndex;

            String WhatId = txtTaskRelatedTo.Text?.Trim();
            if (WhatId == null) return;

            String WhoId = txtIndividualId.Text?.Trim();
            if (WhoId == null) return;

            String WhoName = txtNameOnTask.Text?.Trim();
            if (WhoName == null) return;

            String Comment = txtTaskComments.Text?.Trim();
            if (Comment == null) return;

            String Solution = txtTaskSolution.Text?.Trim();
            if (Solution == null) return;

            //TaskStatus ts = (TaskStatus)comboTaskStatus.SelectedIndex;

            TaskStatus ts;

            if (comboTaskStatus.SelectedIndex < 3) ts = (TaskStatus)comboTaskStatus.SelectedIndex;
            else ts = (TaskStatus)(comboTaskStatus.SelectedIndex + 3);

            TaskPriority tp = (TaskPriority)comboTaskPriority.SelectedIndex;

            String PhoneNo = txtTaskPhone.Text.Trim();
            String Email = txtTaskEmail.Text.Trim();

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

                int Year = Int16.Parse(strDate.Substring(6, 4));
                int Month = Int16.Parse(strDate.Substring(0, 2));
                int Day = Int16.Parse(strDate.Substring(3, 2));
                String Hour = strTime.Substring(0, strTime.IndexOf(':'));
                String Minute = strTime.Substring(strTime.IndexOf(':') + 1, 2);

                Reminder = new DateTime(Year, Month, Day, Int16.Parse(Hour), Int16.Parse(Minute), 0);
            }


            String strStaffForwardingTask = StaffNameTaskAssigned;
            String strStaffReceivingTask = StaffAssigned.Remove(StaffAssigned.Length - 1);

            int? nReceivingStaffId = null;
            int? nReceivingStaffDepartmentId = null;

            String strSqlQueryForReceivingUserInfo = "select [dbo].[tbl_user].[User_Id], [dbo].[tbl_user].[Department_Id] from [dbo].[tbl_user] " +
                                                         "where [dbo].[tbl_user].[User_Name] = @StaffNameReceivingTask";

            SqlCommand cmdQueryForReceivingUserInfo = new SqlCommand(strSqlQueryForReceivingUserInfo, connRN);
            cmdQueryForReceivingUserInfo.CommandType = CommandType.Text;

            cmdQueryForReceivingUserInfo.Parameters.AddWithValue("@StaffNameReceivingTask", strStaffReceivingTask);

            if (connRN.State != ConnectionState.Closed)
            {
                connRN.Close();
                connRN.Open();
            }
            else if (connRN.State == ConnectionState.Closed) connRN.Open();
            SqlDataReader rdrReceivingUserInfo = cmdQueryForReceivingUserInfo.ExecuteReader();
            if (rdrReceivingUserInfo.HasRows)
            {
                rdrReceivingUserInfo.Read();
                if (!rdrReceivingUserInfo.IsDBNull(0)) nReceivingStaffId = rdrReceivingUserInfo.GetInt16(0);
                else nReceivingStaffId = null;
                if (!rdrReceivingUserInfo.IsDBNull(1)) nReceivingStaffDepartmentId = rdrReceivingUserInfo.GetInt16(1);
                else nReceivingStaffDepartmentId = null;
            }
            if (connRN.State != ConnectionState.Closed) connRN.Close();

            if (nReceivingStaffId == null || nReceivingStaffDepartmentId == null)
            {
                MessageBox.Show("Invalid staff name.", "Error");
                return;
            }

            String strSqlForwardTask = "update [dbo].[tbl_task] " +
                                       "set [dbo].[tbl_task].[AssignedTo] = @StaffReceivingTask, " +
                                       "[dbo].[tbl_task].[ForwardedToStaffId] = @StaffReceivingForwardedTask, " +
                                       "[dbo].[tbl_task].[CreatedById] = @StaffForwardingTask, " +
                                       "[dbo].[tbl_task].[Forwarded] = 1, " +
                                       "[dbo].[tbl_task].[whoid] = @WhoId, " +
                                       "[dbo].[tbl_task].[IndividualName] = @WhoName, " +
                                       "[dbo].[tbl_task].[WhatId] = @WhatId, " +
                                       "[dbo].[tbl_task].[SendingDepartment] = @SendingDepartment, " +
                                       "[dbo].[tbl_task].[ReceivingDepartment] = @ReceivingDepartment, " +
                                       "[dbo].[tbl_task].[Subject] = @Subject, " +
                                       "[dbo].[tbl_task].[DueDate] = @DueDate, " +
                                       "[dbo].[tbl_task].[RelatedToTableId] = @RelatedTo, " +
                                       "[dbo].[tbl_task].[LastModifiedDate] = @LastModifiedDate, " +
                                       "[dbo].[tbl_task].[LastModifiedById] = @LastModifiedById, " +
                                       "[dbo].[tbl_task].[ActivityDate] = @ActivityDate, " +
                                       "[dbo].[tbl_task].[Comment] = @Comment, " +
                                       "[dbo].[tbl_task].[Solution] = @Solution, " +
                                       "[dbo].[tbl_task].[Status] = @TaskStatus, " +
                                       "[dbo].[tbl_task].[Priority] = @TaskPriority, " +
                                       "[dbo].[tbl_task].[PhoneNo] = @PhoneNo, " +
                                       "[dbo].[tbl_task].[Email] = @Email, " +
                                       "[dbo].[tbl_task].[ReminderDateTime] = @ReminderDateTime, " +
                                       "[dbo].[tbl_task].[IsReminderSet] = @IsReminderSet " +
                                       "where [dbo].[tbl_task].[id] = @TaskId";

            SqlCommand cmdForwardTask = new SqlCommand(strSqlForwardTask, connRN);
            cmdForwardTask.CommandType = CommandType.Text;

            cmdForwardTask.Parameters.AddWithValue("@StaffReceivingTask", nReceivingStaffId.Value);
            cmdForwardTask.Parameters.AddWithValue("@StaffReceivingForwardedTask", nReceivingStaffId.Value);
            cmdForwardTask.Parameters.AddWithValue("@StaffForwardingTask", LoggedInUserInfo.UserId);
            cmdForwardTask.Parameters.AddWithValue("@WhoId", WhoId);
            cmdForwardTask.Parameters.AddWithValue("@WhoName", WhoName);
            cmdForwardTask.Parameters.AddWithValue("@WhatId", WhatId);
            cmdForwardTask.Parameters.AddWithValue("@SendingDepartment", LoggedInUserInfo.departmentInfo.DepartmentId);
            cmdForwardTask.Parameters.AddWithValue("@ReceivingDepartment", nReceivingStaffDepartmentId.Value);
            cmdForwardTask.Parameters.AddWithValue("@Subject", Subject);
            cmdForwardTask.Parameters.AddWithValue("@DueDate", DueDate.Value);
            cmdForwardTask.Parameters.AddWithValue("@RelatedTo", nRelatedToTableNameIndex);
            cmdForwardTask.Parameters.AddWithValue("@LastModifiedDate", DateTime.Now);
            cmdForwardTask.Parameters.AddWithValue("@LastModifiedById", LoggedInUserInfo.UserId);
            cmdForwardTask.Parameters.AddWithValue("@ActivityDate", DateTime.Now);
            cmdForwardTask.Parameters.AddWithValue("@Comment", Comment);
            cmdForwardTask.Parameters.AddWithValue("@Solution", Solution);
            cmdForwardTask.Parameters.AddWithValue("@TaskStatus", ts);
            cmdForwardTask.Parameters.AddWithValue("@TaskPriority", tp);
            cmdForwardTask.Parameters.AddWithValue("@PhoneNo", PhoneNo);
            cmdForwardTask.Parameters.AddWithValue("@Email", Email);

            if (chkReminder.Checked) cmdForwardTask.Parameters.AddWithValue("@ReminderDateTime", Reminder);
            else cmdForwardTask.Parameters.AddWithValue("@ReminderDateTime", DBNull.Value);
            cmdForwardTask.Parameters.AddWithValue("@IsReminderSet", chkReminder.Checked);
            //else cmdForwardTask.Parameters.AddWithValue("@IsReminderSet", 0);
            cmdForwardTask.Parameters.AddWithValue("@TaskId", nTaskId.Value);

            if (connRN.State != ConnectionState.Closed)
            {
                connRN.Close();
                connRN.Open();
            }
            else if (connRN.State == ConnectionState.Closed) connRN.Open();
            int nTaskForwarded = cmdForwardTask.ExecuteNonQuery();
            if (connRN.State != ConnectionState.Closed) connRN.Close();

            if (nTaskForwarded == 1)
            {
                String taskCreatorName = txtTaskCreator.Text.Trim();

                List<String> lstStaffTaskForwarded = txtTaskNameAssignedTo.Text.Split(';').ToList();
                lstStaffTaskForwarded.RemoveAt(lstStaffTaskForwarded.Count - 1);

                String taskAssignedToName = lstStaffTaskForwarded[0];

                //List<String> lstNamesAssignedTo = NameAssignedTo.Split(';').ToList();
                //lstNamesAssignedTo.RemoveAt(lstNamesAssignedTo.Count - 1);      // remove last empty element

                if (taskCreatorName == null || taskCreatorName == String.Empty) return;
                if (taskAssignedToName == null || taskAssignedToName == String.Empty) return;

                //UserInfo taskForwarderInfo = new UserInfo();

                //String strSqlQueryForTaskForwarderInfo = "select [dbo].[tbl_user].[User_Id], [dbo].[tbl_user].[User_Name], [dbo].[tbl_user].[User_Email], " +
                //                                         "[dbo].[tbl_user].[User_Role_Id], [dbo].[tbl_user].[Department_Id] " +
                //                                         "from [dbo].[tbl_user where [dbo].[tbl_user].[User_Name] = @TaskForwarderName";

                //SqlCommand cmdQueryForTaskForwarderInfo = new SqlCommand(strSqlQueryForTaskForwarderInfo, connRN);
                //cmdQueryForTaskForwarderInfo.CommandType = CommandType.Text;

                //cmdQueryForTaskForwarderInfo.Parameters.AddWithValue("@TaskForwarderName", LoggedInUserInfo.UserName);

                //if (connRN.State != ConnectionState.Closed)
                //{
                //    connRN.Close();
                //    connRN.Open();
                //}
                //else if (connRN.State == ConnectionState.Closed) connRN.Open();
                //SqlDataReader rdrTaskForwarderInfo = cmdQueryForTaskForwarderInfo.ExecuteReader();
                //if (rdrTaskForwarderInfo.HasRows)
                //{
                //    rdrTaskForwarderInfo.Read();
                //    if (!rdrTaskForwarderInfo.IsDBNull(0)) taskForwarderInfo.UserId = rdrTaskForwarderInfo.GetInt16(0);
                //    if (!rdrTaskForwarderInfo.IsDBNull(1)) taskForwarderInfo.UserName = rdrTaskForwarderInfo.GetString(1);
                //    if (!rdrTaskForwarderInfo.IsDBNull(2)) taskForwarderInfo.UserEmail = rdrTaskForwarderInfo.GetString(2);
                //    if (!rdrTaskForwarderInfo.IsDBNull(3)) taskForwarderInfo.UserRoleId = (UserRole)rdrTaskForwarderInfo.GetInt16(3);
                //    if (!rdrTaskForwarderInfo.IsDBNull(4)) taskForwarderInfo.departmentInfo.DepartmentId = (Department)rdrTaskForwarderInfo.GetInt16(4);
                //}
                //rdrTaskForwarderInfo.Close();
                //if (connRN.State != ConnectionState.Closed) connRN.Close();

                Boolean bTaskForwardedSenderMSManagerReceived = false;
                Boolean bTaskForwardedSenderNPManagerReceived = false;
                Boolean bTaskForwardedSenderRNManagerReceived = false;
                Boolean bTaskForwardedSenderFDManagerReceived = false;

                Boolean bTaskForwardedReceiverMSManagerReceived = false;
                Boolean bTaskForwardedReceiverNPManagerReceived = false;
                Boolean bTaskForwardedReceiverRNManagerReceived = false;
                Boolean bTaskForwardedReceiverFDManagerReceived = false;

                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ///
                //TaskUserInfo TaskForwardReceiverInfo = new TaskUserInfo();

                //String strSqlQueryForTaskForwardReceiverInfo = "select [dbo].[tbl_user].[User_Id], [dbo].[tbl_user].[User_Name], [dbo].[tbl_user].[User_Email], " +
                //                                               "[dbo].[tbl_user].[Task_Role_Id], [dbo].[tbl_user].[Department_Id] " +
                //                                               "from [dbo].[tbl_user] where [dbo].[tbl_user].[User_Name] = @TaskForwardReceiverName";

                //SqlCommand cmdQueryForTaskForwardReceiverInfo = new SqlCommand(strSqlQueryForTaskForwardReceiverInfo, connRN);
                //cmdQueryForTaskForwardReceiverInfo.CommandType = CommandType.Text;

                //cmdQueryForTaskForwardReceiverInfo.Parameters.AddWithValue("@TaskForwardReceiverName", taskAssignedToName);

                //if (connRN.State != ConnectionState.Closed)
                //{
                //    connRN.Close();
                //    connRN.Open();
                //}
                //else if (connRN.State == ConnectionState.Closed) connRN.Open();
                //SqlDataReader rdrTaskForwardReceiverInfo = cmdQueryForTaskForwardReceiverInfo.ExecuteReader();
                //if (rdrTaskForwardReceiverInfo.HasRows)
                //{
                //    rdrTaskForwardReceiverInfo.Read();
                //    if (!rdrTaskForwardReceiverInfo.IsDBNull(0)) TaskForwardReceiverInfo.UserId = rdrTaskForwardReceiverInfo.GetInt16(0);
                //    if (!rdrTaskForwardReceiverInfo.IsDBNull(1)) TaskForwardReceiverInfo.UserName = rdrTaskForwardReceiverInfo.GetString(1);
                //    if (!rdrTaskForwardReceiverInfo.IsDBNull(2)) TaskForwardReceiverInfo.UserEmail = rdrTaskForwardReceiverInfo.GetString(2);
                //    if (!rdrTaskForwardReceiverInfo.IsDBNull(3)) TaskForwardReceiverInfo.TaskUserRoleId = (TaskUserRole)rdrTaskForwardReceiverInfo.GetInt16(3);
                //    if (!rdrTaskForwardReceiverInfo.IsDBNull(4)) TaskForwardReceiverInfo.departmentInfo.DepartmentId = (Department)rdrTaskForwardReceiverInfo.GetInt16(4);
                //}
                //rdrTaskForwardReceiverInfo.Close();
                //if (connRN.State != ConnectionState.Closed) connRN.Close();
                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                TaskUserInfo TaskForwarderInfo = new TaskUserInfo();

                String strSqlQueryForTaskForwarderTaskRoleId = "select [dbo].[tbl_user].[Task_Role_Id] from [dbo].[tbl_user] " +
                                                               "where [dbo].[tbl_user].[User_Id] = @TaskForwarderId and " +
                                                               "([dbo].[tbl_user].[IsDeleted] = 0 or [dbo].[tbl_user].[IsDeleted] IS NULL)";

                SqlCommand cmdQueryForTaskForwarderTaskRoleId = new SqlCommand(strSqlQueryForTaskForwarderTaskRoleId, connRN);
                cmdQueryForTaskForwarderTaskRoleId.CommandType = CommandType.Text;

                cmdQueryForTaskForwarderTaskRoleId.Parameters.AddWithValue("@TaskForwarderId", LoggedInUserInfo.UserId);

                if (connRN.State != ConnectionState.Closed)
                {
                    connRN.Close();
                    connRN.Open();
                }
                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                Object objTaskForwarderTaskRoleId = cmdQueryForTaskForwarderTaskRoleId.ExecuteScalar();
                if (connRN.State != ConnectionState.Closed) connRN.Close();

                TaskForwarderInfo.UserId = LoggedInUserInfo.UserId;
                TaskForwarderInfo.UserName = LoggedInUserInfo.UserName;
                TaskForwarderInfo.UserEmail = LoggedInUserInfo.UserEmail;
                TaskForwarderInfo.departmentInfo.DepartmentId = LoggedInUserInfo.departmentInfo.DepartmentId;

                if (objTaskForwarderTaskRoleId != null)
                {
                    int resultTaskRole;
                    if (Int32.TryParse(objTaskForwarderTaskRoleId.ToString(), out resultTaskRole)) TaskForwarderInfo.TaskUserRoleId = (TaskUserRole)resultTaskRole;
                }

                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                UserInfo taskForwardReceiverInfo = new UserInfo();

                String strSqlQueryForTaskForwardReceiverInfo = "select [dbo].[tbl_user].[User_Id], [dbo].[tbl_user].[User_Name], [dbo].[tbl_user].[User_Email], " +
                                                               "[dbo].[tbl_user].[User_Role_Id], [dbo].[tbl_user].[Department_Id] " +
                                                               "from [dbo].[tbl_user] where [dbo].[tbl_user].[User_Name] = @TaskForwardReceiverName";

                SqlCommand cmdQueryForTaskForwardReceiverInfo = new SqlCommand(strSqlQueryForTaskForwardReceiverInfo, connRN);
                cmdQueryForTaskForwardReceiverInfo.CommandType = CommandType.Text;

                cmdQueryForTaskForwardReceiverInfo.Parameters.AddWithValue("@TaskForwardReceiverName", taskAssignedToName);

                if (connRN.State != ConnectionState.Closed)
                {
                    connRN.Close();
                    connRN.Open();
                }
                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                SqlDataReader rdrTaskForwardReceiverInfo = cmdQueryForTaskForwardReceiverInfo.ExecuteReader();
                if (rdrTaskForwardReceiverInfo.HasRows)
                {
                    rdrTaskForwardReceiverInfo.Read();
                    if (!rdrTaskForwardReceiverInfo.IsDBNull(0)) taskForwardReceiverInfo.UserId = rdrTaskForwardReceiverInfo.GetInt16(0);
                    if (!rdrTaskForwardReceiverInfo.IsDBNull(1)) taskForwardReceiverInfo.UserName = rdrTaskForwardReceiverInfo.GetString(1);
                    if (!rdrTaskForwardReceiverInfo.IsDBNull(2)) taskForwardReceiverInfo.UserEmail = rdrTaskForwardReceiverInfo.GetString(2);
                    if (!rdrTaskForwardReceiverInfo.IsDBNull(3)) taskForwardReceiverInfo.UserRoleId = (UserRole)rdrTaskForwardReceiverInfo.GetInt16(3);
                    if (!rdrTaskForwardReceiverInfo.IsDBNull(4)) taskForwardReceiverInfo.departmentInfo.DepartmentId = (Department)rdrTaskForwardReceiverInfo.GetInt16(4);
                }
                rdrTaskForwardReceiverInfo.Close();
                if (connRN.State != ConnectionState.Closed) connRN.Close();

                TaskUserInfo TaskRoleReceiverInfo = new TaskUserInfo();

                TaskRoleReceiverInfo.UserId = taskForwardReceiverInfo.UserId;
                TaskRoleReceiverInfo.UserName = taskForwardReceiverInfo.UserName;
                TaskRoleReceiverInfo.UserEmail = taskForwardReceiverInfo.UserEmail;
                TaskRoleReceiverInfo.departmentInfo.DepartmentId = taskForwardReceiverInfo.departmentInfo.DepartmentId;

                String strSqlQueryForReceiverTaskRoleId = "select [dbo].[tbl_user].[Task_Role_Id] from [dbo].[tbl_user] " +
                                                          "where [dbo].[tbl_user].[User_Id] = @TaskReceiverRoleId and " +
                                                          "([dbo].[tbl_user].[IsDeleted] = 0 or [dbo].[tbl_user].[IsDeleted] IS NULL)";

                SqlCommand cmdQueryForReceiverTaskRoleId = new SqlCommand(strSqlQueryForReceiverTaskRoleId, connRN);
                cmdQueryForReceiverTaskRoleId.CommandType = CommandType.Text;

                cmdQueryForReceiverTaskRoleId.Parameters.AddWithValue("@TaskReceiverRoleId", taskForwardReceiverInfo.UserRoleId);

                if (connRN.State != ConnectionState.Closed)
                {
                    connRN.Close();
                    connRN.Open();
                }
                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                Object objReceiverTaskRoleId = cmdQueryForReceiverTaskRoleId.ExecuteScalar();
                if (connRN.State != ConnectionState.Closed) connRN.Close();

                if (objReceiverTaskRoleId != null)
                {
                    int resultTaskRoleId;
                    if (Int32.TryParse(objReceiverTaskRoleId.ToString(), out resultTaskRoleId)) TaskRoleReceiverInfo.TaskUserRoleId = (TaskUserRole)resultTaskRoleId;
                }

                FDManagerTaskIDs FDManagerTaskId = new FDManagerTaskIDs();

                String strSqlQueryForFDManagerId = "select [dbo].[tbl_user].[User_Id] from [dbo].[tbl_user] " +
                                                   "where [dbo].[tbl_user].[Task_Role_Id] = @FDManagerTaskRoleId and " +
                                                   "([dbo].[tbl_user].[IsDeleted] = 0 or [dbo].[tbl_user].[IsDeleted] IS NULL)";

                SqlCommand cmdQueryForFDManagerId = new SqlCommand(strSqlQueryForFDManagerId, connRN);
                cmdQueryForFDManagerId.CommandType = CommandType.Text;

                cmdQueryForFDManagerId.Parameters.AddWithValue("@FDManagerTaskRoleId", FDManagerTaskId.FDManagerTaskRoleId);

                if (connRN.State != ConnectionState.Closed)
                {
                    connRN.Close();
                    connRN.Open();
                }
                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                Object objFDManagerTaskUserId = cmdQueryForFDManagerId.ExecuteScalar();
                if (connRN.State != ConnectionState.Closed) connRN.Close();

                if (objFDManagerTaskUserId != null)
                {
                    int resultTaskUserId;
                    if (Int32.TryParse(objFDManagerTaskUserId.ToString(), out resultTaskUserId)) FDManagerTaskId.FDManagerTaskUserId = resultTaskUserId;
                }

                RNManagerTaskIDs RNManagerTaskId = new RNManagerTaskIDs();

                String strSqlQueryForRNManagerId = "select [dbo].[tbl_user].[User_Id] from [dbo].[tbl_user] " +
                                                   "where [dbo].[tbl_user].[Task_Role_Id] = @RNManagerTaskRoleId and " +
                                                   "([dbo].[tbl_user].[IsDeleted] = 0 or [dbo].[tbl_user].[IsDeleted] IS NULL)";

                SqlCommand cmdQueryForRNManagerId = new SqlCommand(strSqlQueryForRNManagerId, connRN);
                cmdQueryForRNManagerId.CommandType = CommandType.Text;

                cmdQueryForRNManagerId.Parameters.AddWithValue("@RNManagerTaskRoleId", RNManagerTaskId.RNManagerTaskRoleId);

                if (connRN.State != ConnectionState.Closed)
                {
                    connRN.Close();
                    connRN.Open();
                }
                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                Object objRNManagerTaskUserId = cmdQueryForRNManagerId.ExecuteScalar();
                if (connRN.State != ConnectionState.Closed) connRN.Close();

                if (objRNManagerTaskUserId != null)
                {
                    int resultTaskUserId;
                    if (Int32.TryParse(objRNManagerTaskUserId.ToString(), out resultTaskUserId)) RNManagerTaskId.RNManagerTaskUserId = resultTaskUserId;
                }

                NPManagerTaskIDs NPManagerTaskId = new NPManagerTaskIDs();

                String strSqlQueryForNPManagerId = "select [dbo].[tbl_user].[User_Id] from [dbo].[tbl_user] " +
                                                   "where [dbo].[tbl_user].[Task_Role_Id] = @NPManagerTaskRoleId and " +
                                                   "([dbo].[tbl_user].[IsDeleted] = 0 or [dbo].[tbl_user].[IsDeleted] IS NULL)";

                SqlCommand cmdQueryForNPManagerId = new SqlCommand(strSqlQueryForNPManagerId, connRN);
                cmdQueryForNPManagerId.CommandType = CommandType.Text;

                cmdQueryForNPManagerId.Parameters.AddWithValue("@NPManagerTaskRoleId", NPManagerTaskId.NPManagerTaskRoleId);

                if (connRN.State != ConnectionState.Closed)
                {
                    connRN.Close();
                    connRN.Open();
                }
                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                Object objNPManagerTaskUserId = cmdQueryForNPManagerId.ExecuteScalar();
                if (connRN.State != ConnectionState.Closed) connRN.Close();

                if (objNPManagerTaskUserId != null)
                {
                    int resultTaskUserId;
                    if (Int32.TryParse(objNPManagerTaskUserId.ToString(), out resultTaskUserId)) NPManagerTaskId.NPManagerTaskUserId = resultTaskUserId;
                }

                MSManagerTaskIDs MSManagerTaskId = new MSManagerTaskIDs();

                String strSqlQueryForMSManagerId = "select [dbo].[tbl_user].[User_Id] from [dbo].[tbl_user] " +
                                                   "where [dbo].[tbl_user].[Task_Role_Id] = @MSManagerTaskRoleId and " +
                                                   "([dbo].[tbl_user].[IsDeleted] = 0 or [dbo].[tbl_user].[IsDeleted] IS NULL)";

                SqlCommand cmdQueryForMSManagerId = new SqlCommand(strSqlQueryForMSManagerId, connRN);
                cmdQueryForMSManagerId.CommandType = CommandType.Text;

                cmdQueryForMSManagerId.Parameters.AddWithValue("@MSManagerTaskRoleId", MSManagerTaskId.MSManagerTaskRoleId);

                if (connRN.State != ConnectionState.Closed)
                {
                    connRN.Close();
                    connRN.Open();
                }
                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                Object objMSManagerTaskUserId = cmdQueryForMSManagerId.ExecuteScalar();
                if (connRN.State != ConnectionState.Closed) connRN.Close();

                if (objMSManagerTaskUserId != null)
                {
                    int resultTaskUserId;
                    if (Int32.TryParse(objMSManagerTaskUserId.ToString(), out resultTaskUserId)) MSManagerTaskId.MSManagerTaskUserId = resultTaskUserId;
                }


                Int32? nForwardingStaffDepartmentManagerId = null;
                switch (LoggedInUserInfo.departmentInfo.DepartmentId)
                {
                    case Department.MemberService:
                        if (!bTaskForwardedSenderMSManagerReceived &&
                            LoggedInUserInfo.UserRoleId != UserRole.MSManager && 
                            (UserRole)nReceivingStaffDepartmentId.Value != UserRole.MSManager)
                        {
                            //nForwardingStaffDepartmentManagerId = 18;
                            //AssignTaskToManager(nForwardingStaffDepartmentManagerId.Value, LoggedInUserInfo, nTaskId.Value);
                            AssignTaskToManager(MSManagerTaskId.MSManagerTaskUserId.Value, LoggedInUserInfo, nTaskId.Value);
                            bTaskForwardedSenderMSManagerReceived = true;
                        }
                        break;
                    case Department.NeedsProcessing:
                        if (!bTaskForwardedSenderNPManagerReceived &&
                            LoggedInUserInfo.UserRoleId != UserRole.NPManager && 
                            (UserRole)nReceivingStaffDepartmentId.Value != UserRole.NPManager)
                        {
                            //nForwardingStaffDepartmentManagerId = 9;
                            //AssignTaskToManager(nForwardingStaffDepartmentManagerId.Value, LoggedInUserInfo, nTaskId.Value);
                            AssignTaskToManager(NPManagerTaskId.NPManagerTaskUserId.Value, LoggedInUserInfo, nTaskId.Value);
                            bTaskForwardedSenderNPManagerReceived = true;
                        }
                        break;
                    case Department.ReviewAndNegotiation:
                        if ((!bTaskForwardedSenderRNManagerReceived &&
                            LoggedInUserInfo.UserRoleId != UserRole.RNManager && 
                            (UserRole)nReceivingStaffDepartmentId.Value != UserRole.RNManager) ||
                            (!bTaskForwardedSenderRNManagerReceived &&
                            TaskForwarderInfo.TaskUserRoleId == TaskUserRole.RNAssistantManager &&
                            taskForwardReceiverInfo.UserRoleId != UserRole.RNManager))
                        {
                            //nForwardingStaffDepartmentManagerId = 13;
                            //AssignTaskToManager(nForwardingStaffDepartmentManagerId.Value, LoggedInUserInfo, nTaskId.Value);
                            AssignTaskToManager(RNManagerTaskId.RNManagerTaskUserId.Value, LoggedInUserInfo, nTaskId.Value);
                            bTaskForwardedSenderRNManagerReceived = true;
                        }
                        break;
                    case Department.Finance:
                        if (!bTaskForwardedSenderFDManagerReceived &&
                            LoggedInUserInfo.UserRoleId != UserRole.FDManager && 
                            (UserRole)nReceivingStaffDepartmentId.Value != UserRole.FDManager)
                        {
                            //nForwardingStaffDepartmentManagerId = 16;
                            //AssignTaskToManager(nForwardingStaffDepartmentManagerId.Value, LoggedInUserInfo, nTaskId.Value);
                            AssignTaskToManager(FDManagerTaskId.FDManagerTaskUserId.Value, LoggedInUserInfo, nTaskId.Value);
                            bTaskForwardedSenderFDManagerReceived = true;
                        }
                        break;
                }

                Int32? nForwardedTaskReceiverDepartmentManagerId = null;
                
                switch (taskForwardReceiverInfo.departmentInfo.DepartmentId) 
                {
                    case Department.MemberService:
                        if (!bTaskForwardedReceiverMSManagerReceived &&
                            LoggedInUserInfo.UserRoleId != UserRole.MSManager &&
                            taskForwardReceiverInfo.UserRoleId != UserRole.MSManager &&
                            LoggedInUserInfo.departmentInfo.DepartmentId != taskForwardReceiverInfo.departmentInfo.DepartmentId)
                        {
                            //nForwardedTaskReceiverDepartmentManagerId = 18;
                            //AssignTaskToManager(nForwardedTaskReceiverDepartmentManagerId.Value, taskForwardReceiverInfo, nTaskId.Value);
                            AssignTaskToManager(MSManagerTaskId.MSManagerTaskUserId.Value, taskForwardReceiverInfo, nTaskId.Value);
                            bTaskForwardedReceiverMSManagerReceived = true;
                        }
                        break;
                    case Department.NeedsProcessing:
                        if (!bTaskForwardedReceiverNPManagerReceived &&
                            LoggedInUserInfo.UserRoleId != UserRole.NPManager &&
                            taskForwardReceiverInfo.UserRoleId != UserRole.NPManager &&
                            LoggedInUserInfo.departmentInfo.DepartmentId != taskForwardReceiverInfo.departmentInfo.DepartmentId)
                        {
                            //nForwardedTaskReceiverDepartmentManagerId = 9;
                            //AssignTaskToManager(nForwardedTaskReceiverDepartmentManagerId.Value, taskForwardReceiverInfo, nTaskId.Value);
                            AssignTaskToManager(NPManagerTaskId.NPManagerTaskUserId.Value, taskForwardReceiverInfo, nTaskId.Value);
                            bTaskForwardedReceiverNPManagerReceived = true;
                        }
                        break;
                    case Department.ReviewAndNegotiation:
                        if ((!bTaskForwardedReceiverRNManagerReceived &&
                            LoggedInUserInfo.UserRoleId != UserRole.RNManager &&
                            taskForwardReceiverInfo.UserRoleId != UserRole.RNManager &&
                            LoggedInUserInfo.departmentInfo.DepartmentId != taskForwardReceiverInfo.departmentInfo.DepartmentId) ||
                            (!bTaskForwardedReceiverRNManagerReceived &&
                            TaskRoleReceiverInfo.TaskUserRoleId == TaskUserRole.RNAssistantManager &&
                            LoggedInUserInfo.departmentInfo.DepartmentId != taskForwardReceiverInfo.departmentInfo.DepartmentId))
                        {
                            //nForwardedTaskReceiverDepartmentManagerId = 13;
                            //AssignTaskToManager(nForwardedTaskReceiverDepartmentManagerId.Value, taskForwardReceiverInfo, nTaskId.Value);
                            AssignTaskToManager(RNManagerTaskId.RNManagerTaskUserId.Value, taskForwardReceiverInfo, nTaskId.Value);
                            bTaskForwardedReceiverRNManagerReceived = true;
                        }
                        break;
                    case Department.Finance:
                        if (!bTaskForwardedReceiverFDManagerReceived &&
                            LoggedInUserInfo.UserRoleId != UserRole.FDManager &&
                            taskForwardReceiverInfo.UserRoleId != UserRole.FDManager &&
                            LoggedInUserInfo.departmentInfo.DepartmentId != taskForwardReceiverInfo.departmentInfo.DepartmentId)
                        {
                            //nForwardedTaskReceiverDepartmentManagerId = 16;
                            //AssignTaskToManager(nForwardedTaskReceiverDepartmentManagerId.Value, taskForwardReceiverInfo, nTaskId.Value);
                            AssignTaskToManager(FDManagerTaskId.FDManagerTaskUserId.Value, taskForwardReceiverInfo, nTaskId.Value);
                            bTaskForwardedReceiverFDManagerReceived = true;
                        }
                        break;
                }
                MessageBox.Show("The task has been forwarded successfully.", "info");
            }
            else
            {
                MessageBox.Show("The task has not been forwarded.", "Error");
            }
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            String StaffAssigned = txtTaskNameAssignedTo.Text?.Trim();

            String strSqlQueryForStaffNameTaskAssigned = "select [dbo].[tbl_user].[User_Name] from [dbo].[tbl_task] " +
                                                         "inner join [dbo].[tbl_user] on [dbo].[tbl_task].[AssignedTo] = [dbo].[tbl_user].[User_Id] " +
                                                         "where [dbo].[tbl_task].[id] = @TaskId";

            SqlCommand cmdQueryForStaffNameTaskAssigned = new SqlCommand(strSqlQueryForStaffNameTaskAssigned, connRN);
            cmdQueryForStaffNameTaskAssigned.CommandType = CommandType.Text;

            cmdQueryForStaffNameTaskAssigned.Parameters.AddWithValue("@TaskId", nTaskId.Value);

            if (connRN.State != ConnectionState.Closed)
            {
                connRN.Close();
                connRN.Open();
            }
            else if (connRN.State == ConnectionState.Closed) connRN.Open();
            Object objStaffNameTaskAssigned = cmdQueryForStaffNameTaskAssigned.ExecuteScalar();
            if (connRN.State != ConnectionState.Closed) connRN.Close();

            String StaffNameTaskAssigned = objStaffNameTaskAssigned?.ToString()?.Trim();

            if (StaffAssigned != StaffNameTaskAssigned)
            {
                //MessageBox.Show("Please choose a receiving staff of the task forwarded.", "Alert");
                MessageBox.Show("You cannot change the Assigned Staff in saving the task.", "Alert");
                return;
            }

            String creator_name = txtTaskCreator.Text.Trim();
            String assigned_to_name = txtTaskNameAssignedTo.Text.Trim();

            if (creator_name == null || creator_name == String.Empty) return;
            if (assigned_to_name == null || assigned_to_name == String.Empty) return;

            UserInfo taskCreatorInfo = new UserInfo();

            String strSqlQueryForTaskCreatorInfo = "select [dbo].[tbl_user].[User_Id], [dbo].[tbl_user].[User_Name], [dbo].[tbl_user].[User_Email], " +
                                                   "[dbo].[tbl_user].[User_Role_Id], [dbo].[tbl_user].[Department_Id] " +
                                                   "from [dbo].[tbl_user] where [dbo].[tbl_user].[User_Name] = @TaskCreatorName";

            SqlCommand cmdQueryForTaskCreatorInfo = new SqlCommand(strSqlQueryForTaskCreatorInfo, connRN);
            cmdQueryForTaskCreatorInfo.CommandType = CommandType.Text;

            cmdQueryForTaskCreatorInfo.Parameters.AddWithValue("@TaskCreatorName", creator_name);

            if (connRN.State != ConnectionState.Closed)
            {
                connRN.Close();
                connRN.Open();
            }
            else if (connRN.State == ConnectionState.Closed) connRN.Open();
            SqlDataReader rdrTaskCreatorInfo = cmdQueryForTaskCreatorInfo.ExecuteReader();
            if (rdrTaskCreatorInfo.HasRows)
            {
                rdrTaskCreatorInfo.Read();
                if (!rdrTaskCreatorInfo.IsDBNull(0)) taskCreatorInfo.UserId = rdrTaskCreatorInfo.GetInt16(0);
                if (!rdrTaskCreatorInfo.IsDBNull(1)) taskCreatorInfo.UserName = rdrTaskCreatorInfo.GetString(1);
                if (!rdrTaskCreatorInfo.IsDBNull(2)) taskCreatorInfo.UserEmail = rdrTaskCreatorInfo.GetString(2);
                if (!rdrTaskCreatorInfo.IsDBNull(3)) taskCreatorInfo.UserRoleId = (UserRole)rdrTaskCreatorInfo.GetInt16(3);
                if (!rdrTaskCreatorInfo.IsDBNull(4)) taskCreatorInfo.departmentInfo.DepartmentId = (Department)rdrTaskCreatorInfo.GetInt16(4);
            }
            rdrTaskCreatorInfo.Close();
            if (connRN.State != ConnectionState.Closed) connRN.Close();

            UserInfo taskReceiverInfo = new UserInfo();

            String strSqlQueryForTaskReceiverInfo = "select [dbo].[tbl_user].[User_Id], [dbo].[tbl_user].[User_Name], [dbo].[tbl_user].[User_Email], " +
                                                    "[dbo].[tbl_user].[User_Role_Id], [dbo].[tbl_user].[Department_Id] " +
                                                    "from [dbo].[tbl_user] where [dbo].[tbl_user].[User_Name] = @TaskReceiverName";

            SqlCommand cmdQueryForTaskReceiverInfo = new SqlCommand(strSqlQueryForTaskReceiverInfo, connRN);
            cmdQueryForTaskReceiverInfo.CommandType = CommandType.Text;

            cmdQueryForTaskReceiverInfo.Parameters.AddWithValue("@TaskReceiverName", assigned_to_name);

            if (connRN.State != ConnectionState.Closed)
            {
                connRN.Close();
                connRN.Open();
            }
            else if (connRN.State == ConnectionState.Closed) connRN.Open();
            SqlDataReader rdrTaskReceiverInfo = cmdQueryForTaskReceiverInfo.ExecuteReader();
            if (rdrTaskReceiverInfo.HasRows)
            {
                rdrTaskReceiverInfo.Read();
                if (!rdrTaskReceiverInfo.IsDBNull(0)) taskReceiverInfo.UserId = rdrTaskReceiverInfo.GetInt16(0);
                if (!rdrTaskReceiverInfo.IsDBNull(1)) taskReceiverInfo.UserName = rdrTaskReceiverInfo.GetString(1);
                if (!rdrTaskReceiverInfo.IsDBNull(2)) taskReceiverInfo.UserEmail = rdrTaskReceiverInfo.GetString(2);
                if (!rdrTaskReceiverInfo.IsDBNull(3)) taskReceiverInfo.UserRoleId = (UserRole)rdrTaskReceiverInfo.GetInt16(3);
                if (!rdrTaskReceiverInfo.IsDBNull(4)) taskReceiverInfo.departmentInfo.DepartmentId = (Department)rdrTaskReceiverInfo.GetInt16(4);
            }
            rdrTaskReceiverInfo.Close();
            if (connRN.State != ConnectionState.Closed) connRN.Close();

            

            int? nRelatedToTableNameIndex = null;
            if (comboTaskRelatedTo.SelectedIndex != -1) nRelatedToTableNameIndex = comboTaskRelatedTo.SelectedIndex;

            String WhatId = txtTaskRelatedTo.Text?.Trim();
            if (WhatId == null) return;

            String WhoId = txtIndividualId.Text?.Trim();
            if (WhoId == null) return;

            String WhoName = txtNameOnTask.Text?.Trim();
            if (WhoName == null) return;

            String Comment = txtTaskComments.Text?.Trim();
            if (Comment == null) return;

            String Solution = txtTaskSolution.Text?.Trim();
            if (Solution == null) return;

            //TaskStatus ts = (TaskStatus)comboTaskStatus.SelectedIndex;

            TaskStatus ts;

            if (comboTaskStatus.SelectedIndex < 3) ts = (TaskStatus)comboTaskStatus.SelectedIndex;
            else ts = (TaskStatus)(comboTaskStatus.SelectedIndex + 3);

            TaskPriority tp = (TaskPriority)comboTaskPriority.SelectedIndex;

            String PhoneNo = txtTaskPhone.Text.Trim();
            String Email = txtTaskEmail.Text.Trim();

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

                int Year = Int16.Parse(strDate.Substring(6, 4));
                int Month = Int16.Parse(strDate.Substring(0, 2));
                int Day = Int16.Parse(strDate.Substring(3, 2));
                String Hour = strTime.Substring(0, strTime.IndexOf(':'));
                String Minute = strTime.Substring(strTime.IndexOf(':') + 1, 2);

                Reminder = new DateTime(Year, Month, Day, Int16.Parse(Hour), Int16.Parse(Minute), 0);
            }

            String Subject = txtTaskSubject.Text.Trim();
            DateTime? DueDate = dtpTaskDueDate.Value;
            if (DueDate == null) return;

            Boolean bTaskModifiedReceiverMSManagerReceived = false;
            Boolean bTaskModifiedReceiverNPManagerReceived = false;
            Boolean bTaskModifiedReceiverRNManagerReceived = false;
            Boolean bTaskModifiedReceiverFDManagerReceived = false;

            Boolean bTaskModifiedSenderMSManagerReceived = false;
            Boolean bTaskModifiedSenderNPManagerReceived = false;
            Boolean bTaskModifiedSenderRNManagerReceived = false;
            Boolean bTaskModifiedSenderFDManagerReceived = false;

            String strSqlUpdateTask = "update [dbo].[tbl_task] " +
                           "set [dbo].[tbl_task].[whoid] = @WhoId, " +
                           "[dbo].[tbl_task].[IndividualName] = @WhoName, " +
                           "[dbo].[tbl_task].[WhatId] = @WhatId, " +
                           "[dbo].[tbl_task].[Subject] = @Subject, " +
                           "[dbo].[tbl_task].[DueDate] = @DueDate, " +
                           "[dbo].[tbl_task].[RelatedToTableId] = @RelatedTo, " +
                           "[dbo].[tbl_task].[LastModifiedDate] = @LastModifiedDate, " +
                           "[dbo].[tbl_task].[LastModifiedById] = @LastModifiedById, " +
                           "[dbo].[tbl_task].[ActivityDate] = @ActivityDate, " +
                           "[dbo].[tbl_task].[Comment] = @Comment, " +
                           "[dbo].[tbl_task].[Solution] = @Solution, " +
                           "[dbo].[tbl_task].[Status] = @TaskStatus, " +
                           "[dbo].[tbl_task].[Priority] = @TaskPriority, " +
                           "[dbo].[tbl_task].[PhoneNo] = @PhoneNo, " +
                           "[dbo].[tbl_task].[Email] = @Email, " +
                           "[dbo].[tbl_task].[ReminderDateTime] = @ReminderDateTime, " +
                           "[dbo].[tbl_task].[IsReminderSet] = @IsReminderSet " +
                           "where [dbo].[tbl_task].[id] = @TaskId";

            SqlCommand cmdUpdateTask = new SqlCommand(strSqlUpdateTask, connRN);
            cmdUpdateTask.CommandType = CommandType.Text;

            cmdUpdateTask.Parameters.AddWithValue("@WhoId", WhoId);
            cmdUpdateTask.Parameters.AddWithValue("@WhatId", WhatId);
            cmdUpdateTask.Parameters.AddWithValue("@WhoName", WhoName);
            cmdUpdateTask.Parameters.AddWithValue("@Subject", Subject);
            cmdUpdateTask.Parameters.AddWithValue("@DueDate", DueDate);
            cmdUpdateTask.Parameters.AddWithValue("@RelatedTo", nRelatedToTableNameIndex);
            cmdUpdateTask.Parameters.AddWithValue("@LastModifiedDate", DateTime.Now);
            cmdUpdateTask.Parameters.AddWithValue("@LastModifiedById", LoggedInUserInfo.UserId);
            cmdUpdateTask.Parameters.AddWithValue("@ActivityDate", DateTime.Now);
            cmdUpdateTask.Parameters.AddWithValue("@Comment", Comment);
            cmdUpdateTask.Parameters.AddWithValue("@Solution", Solution);
            cmdUpdateTask.Parameters.AddWithValue("@TaskStatus", ts);
            cmdUpdateTask.Parameters.AddWithValue("@TaskPriority", tp);
            cmdUpdateTask.Parameters.AddWithValue("@PhoneNo", PhoneNo);
            cmdUpdateTask.Parameters.AddWithValue("@Email", Email);

            if (chkReminder.Checked) cmdUpdateTask.Parameters.AddWithValue("@ReminderDateTime", Reminder);
            else cmdUpdateTask.Parameters.AddWithValue("@ReminderDateTime", DBNull.Value);
            cmdUpdateTask.Parameters.AddWithValue("@IsReminderSet", chkReminder.Checked);
            cmdUpdateTask.Parameters.AddWithValue("@TaskId", nTaskId.Value);

            if (connRN.State != ConnectionState.Closed)
            {
                connRN.Close();
                connRN.Open();
            }
            else if (connRN.State == ConnectionState.Closed) connRN.Open();
            int nTaskUpdated = cmdUpdateTask.ExecuteNonQuery();
            if (connRN.State != ConnectionState.Closed) connRN.Close();
           
            if (nTaskUpdated == 1)
            {
                Int32? nTaskModifiedReceiverDepartmentManagerId = null;

                //switch (taskReceiverInfo.departmentInfo.DepartmentId)
                //{
                //    case Department.MemberService:
                //        if (!bTaskModifiedReceiverMSManagerReceived && 
                //            taskReceiverInfo.UserRoleId != UserRole.MSManager &&
                //            taskCreatorInfo.UserRoleId != UserRole.MSManager)
                //        {
                //            nTaskModifiedReceiverDepartmentManagerId = 18;
                //            AssignTaskToManager(nTaskModifiedReceiverDepartmentManagerId.Value, taskReceiverInfo, nTaskId.Value);
                //            bTaskModifiedReceiverMSManagerReceived = true;
                //        }
                //        break;

                //    case Department.NeedsProcessing:
                //        if (!bTaskModifiedReceiverNPManagerReceived &&
                //            taskReceiverInfo.UserRoleId != UserRole.NPManager &&
                //            taskCreatorInfo.UserRoleId != UserRole.NPManager)
                //        {
                //            nTaskModifiedReceiverDepartmentManagerId = 9;
                //            AssignTaskToManager(nTaskModifiedReceiverDepartmentManagerId.Value, taskReceiverInfo, nTaskId.Value);
                //            bTaskModifiedReceiverNPManagerReceived = true;
                //        }
                //        break;

                //    case Department.ReviewAndNegotiation:
                //        if (!bTaskModifiedReceiverRNManagerReceived &&
                //            taskReceiverInfo.UserRoleId != UserRole.RNManager &&
                //            taskCreatorInfo.UserRoleId != UserRole.RNManager)
                //        {
                //            nTaskModifiedReceiverDepartmentManagerId = 13;
                //            AssignTaskToManager(nTaskModifiedReceiverDepartmentManagerId.Value, taskReceiverInfo, nTaskId.Value);
                //            bTaskModifiedReceiverRNManagerReceived = true;
                //        }
                //        break;

                //    case Department.Finance:
                //        if (!bTaskModifiedReceiverFDManagerReceived &&
                //            taskReceiverInfo.UserRoleId != UserRole.FDManager &&
                //            taskCreatorInfo.UserRoleId != UserRole.FDManager)
                //        {
                //            nTaskModifiedReceiverDepartmentManagerId = 16;
                //            AssignTaskToManager(nTaskModifiedReceiverDepartmentManagerId.Value, taskReceiverInfo, nTaskId.Value);
                //            bTaskModifiedReceiverFDManagerReceived = true;
                //        }
                //        break;
                //}

                //Int32? nTaskModifiedCreatorDepartmentManagerId = null;

                //switch (taskCreatorInfo.departmentInfo.DepartmentId)
                //{
                //    case Department.MemberService:
                //        if (!bTaskModifiedSenderMSManagerReceived &&
                //            taskCreatorInfo.UserRoleId != UserRole.MSManager &&
                //            taskReceiverInfo.UserRoleId != UserRole.MSManager &&
                //            taskCreatorInfo.departmentInfo.DepartmentId != taskReceiverInfo.departmentInfo.DepartmentId)
                //        {
                //            nTaskModifiedCreatorDepartmentManagerId = 18;
                //            AssignTaskToManager(nTaskModifiedCreatorDepartmentManagerId.Value, taskCreatorInfo, nTaskId.Value);
                //            bTaskModifiedSenderMSManagerReceived = true;
                //        }
                //        break;
                //    case Department.NeedsProcessing:
                //        if (!bTaskModifiedSenderNPManagerReceived &&
                //            taskCreatorInfo.UserRoleId != UserRole.NPManager &&
                //            taskReceiverInfo.UserRoleId != UserRole.MSManager &&
                //            taskCreatorInfo.departmentInfo.DepartmentId != taskReceiverInfo.departmentInfo.DepartmentId)
                //        {
                //            nTaskModifiedCreatorDepartmentManagerId = 9;
                //            AssignTaskToManager(nTaskModifiedCreatorDepartmentManagerId.Value, taskCreatorInfo, nTaskId.Value);
                //            bTaskModifiedSenderNPManagerReceived = true;
                //        }
                //        break;
                //    case Department.ReviewAndNegotiation:
                //        if (!bTaskModifiedSenderRNManagerReceived &&
                //            taskCreatorInfo.UserRoleId != UserRole.RNManager &&
                //            taskReceiverInfo.UserRoleId != UserRole.RNManager &&
                //            taskCreatorInfo.departmentInfo.DepartmentId != taskReceiverInfo.departmentInfo.DepartmentId)
                //        {
                //            nTaskModifiedCreatorDepartmentManagerId = 13;
                //            AssignTaskToManager(nTaskModifiedCreatorDepartmentManagerId.Value, taskCreatorInfo, nTaskId.Value);
                //            bTaskModifiedSenderRNManagerReceived = true;
                //        }
                //        break;
                //    case Department.Finance:
                //        if (!bTaskModifiedSenderFDManagerReceived &&
                //            taskCreatorInfo.UserRoleId != UserRole.FDManager &&
                //            taskReceiverInfo.UserRoleId != UserRole.FDManager &&
                //            taskCreatorInfo.departmentInfo.DepartmentId != taskReceiverInfo.departmentInfo.DepartmentId)
                //        {
                //            nTaskModifiedCreatorDepartmentManagerId = 16;
                //            AssignTaskToManager(nTaskModifiedCreatorDepartmentManagerId.Value, taskCreatorInfo, nTaskId.Value);
                //            bTaskModifiedSenderFDManagerReceived = true;
                //        }
                //        break;
                //}                

                MessageBox.Show("The task has been saved.", "Info");
                Close();
                return;
            }
            else
            {
                MessageBox.Show("The task has not been saved.", "Info");
                Close();
                return;
            }

        }

        private void MakeTaskFormReadonly()
        {
            //grpTaskInfo.Enabled = false;
            //grpAdditionalInfo.Enabled = false;

            btnSaveTask.Enabled = false;

            comboTaskRelatedTo.Enabled = false;
            dtpTaskDueDate.Enabled = false;
            txtTaskSubject.ReadOnly = true;
            txtTaskRelatedTo.ReadOnly = true;
            txtNameOnTask.ReadOnly = true;
            txtTaskNameAssignedTo.ReadOnly = true;
            txtTaskEmail.ReadOnly = true;
            txtTaskPhone.ReadOnly = true;
            comboTaskPriority.Enabled = false;
            comboTaskStatus.Enabled = false;
            comboReminderTimePicker.Enabled = false;
            dtpReminderDatePicker.Enabled = false;
            chkReminder.Enabled = false;
            txtIndividualId.ReadOnly = true;
            txtTaskCreator.ReadOnly = true;
            btnReplyTask.Enabled = false;
            btnForward.Enabled = false;
            btnAssignedTo.Enabled = false;
            btnSave.Enabled = false;
            txtTaskSolution.ReadOnly = true;
            txtTaskComments.ReadOnly = true;
        }
    }
}
