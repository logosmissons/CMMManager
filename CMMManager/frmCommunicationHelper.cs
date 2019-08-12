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

namespace CMMManager
{
    public partial class frmCommunicationHelper : Form
    {
        private String IndividualId;

        private String RNConnString;
        private SqlConnection connRN;

        private String SalesforceConnString;
        private SqlConnection connSalesforce;

        public frmCommunicationHelper()
        {
            InitializeComponent();
            IndividualId = String.Empty;
            RNConnString = @"Data Source=12.230.174.166\cmm; Initial Catalog=RN_DB; Integrated Security=True; Max Pool Size=200";
            connRN = new SqlConnection(RNConnString);

            SalesforceConnString = @"Data Source=12.230.174.166\cmm; Initial Catalog=SalesForce; Integrated Security=True";
            connSalesforce = new SqlConnection(SalesforceConnString);
        }

        public frmCommunicationHelper(String individual_id)
        {
            InitializeComponent();
            IndividualId = individual_id;
            RNConnString = @"Data Source=12.230.174.166\cmm; Initial Catalog=RN_DB; Integrated Security=True; Max Pool Size=200";
            connRN = new SqlConnection(RNConnString);

            SalesforceConnString = @"Data Source=12.230.174.166\cmm; Initial Catalog=SalesForce; Integrated Security=True";
            connSalesforce = new SqlConnection(SalesforceConnString);
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmCommunicationHelper_Load(object sender, EventArgs e)
        {
            String IndividualName = String.Empty;
            String IndividualMembershipNo = String.Empty;

            String strSqlQueryForIndividaulInfo = "select [dbo].[Contact].[Name], [dbo].[Contact].[Membership_Number__c] from [dbo].[Contact] " +
                                                    "where [dbo].[Contact].[Individual_ID__c] = @IndividualId";

            SqlCommand cmdQueryForIndividualInfo = new SqlCommand(strSqlQueryForIndividaulInfo, connSalesforce);
            cmdQueryForIndividualInfo.CommandType = CommandType.Text;

            cmdQueryForIndividualInfo.Parameters.AddWithValue("@IndividualId", IndividualId);

            if (connSalesforce.State != ConnectionState.Closed)
            {
                connSalesforce.Close();
                connSalesforce.Open();
            }
            else if (connSalesforce.State == ConnectionState.Closed) connSalesforce.Open();
            SqlDataReader rdrIndividualInfo = cmdQueryForIndividualInfo.ExecuteReader();
            if (rdrIndividualInfo.HasRows)
            {
                while (rdrIndividualInfo.Read())
                {
                    if (!rdrIndividualInfo.IsDBNull(0)) IndividualName = rdrIndividualInfo.GetString(0);
                    if (!rdrIndividualInfo.IsDBNull(1)) IndividualMembershipNo = rdrIndividualInfo.GetString(1);
                }
            }
            rdrIndividualInfo.Close();
            if (connSalesforce.State != ConnectionState.Closed) connSalesforce.Close();

            txtIndividualName.Text = IndividualName;
            txtMembershipNo.Text = IndividualMembershipNo;


            comboCaseNo.Items.Add("All");
            comboIllnessNo.Enabled = false;
            comboIncidentNo.Enabled = false;

            String strSqlQueryForCases = "select [dbo].[tbl_case].[Case_Name] from [dbo].[tbl_case] where [dbo].[tbl_case].[individual_id] = @IndividualId";

            SqlCommand cmdQueryForCases = new SqlCommand(strSqlQueryForCases, connRN);
            cmdQueryForCases.CommandType = CommandType.Text;

            cmdQueryForCases.Parameters.AddWithValue("@IndividualId", IndividualId);

            if (connRN.State != ConnectionState.Closed)
            {
                connRN.Close();
                connRN.Open();
            }
            else if (connRN.State == ConnectionState.Closed) connRN.Open();
            SqlDataReader rdrCases = cmdQueryForCases.ExecuteReader();
            if (rdrCases.HasRows)
            {
                while (rdrCases.Read())
                {
                    if (!rdrCases.IsDBNull(0)) comboCaseNo.Items.Add(rdrCases.GetString(0));
                }
            }
            rdrCases.Close();
            if (connRN.State != ConnectionState.Closed) connRN.Close();

            comboCaseNo.SelectedIndexChanged -= comboCaseNo_SelectedIndexChanged;
            comboCaseNo.SelectedIndex = 0;
            comboCaseNo.SelectedIndexChanged += comboCaseNo_SelectedIndexChanged;

            gvCommunicationList.Rows.Clear();
            String strSqlQueryForCommunication = "select [dbo].[tbl_Communication].[CommunicationNo], [dbo].[tbl_Communication].[CommunicationType], " +
                                                 "[dbo].[tbl_Communication].[Subject], [dbo].[tbl_Communication].[Body] " +
                                                 "from [dbo].[tbl_Communication] " +
                                                 "where [dbo].[tbl_Communication].[Individual_Id] = @IndividualId " +
                                                 "order by [dbo].[tbl_Communication].[CreateDate] Desc";
            //"order by convert(datetime, [dbo].[tbl_Communication].[CreateDate]) Desc";

            SqlCommand cmdQueryForCommunication = new SqlCommand(strSqlQueryForCommunication, connRN);
            cmdQueryForCommunication.CommandType = CommandType.Text;

            cmdQueryForCommunication.Parameters.AddWithValue("@IndividualId", IndividualId);

            if (connRN.State != ConnectionState.Closed)
            {
                connRN.Close();
                connRN.Open();
            }
            else if (connRN.State == ConnectionState.Closed) connRN.Open();
            SqlDataReader rdrCommunication = cmdQueryForCommunication.ExecuteReader();
            if (rdrCommunication.HasRows)
            {
                while (rdrCommunication.Read())
                {
                    DataGridViewRow row = new DataGridViewRow();
                    if (!rdrCommunication.IsDBNull(0)) row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrCommunication.GetString(0) });
                    else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                    if (!rdrCommunication.IsDBNull(1))
                    {
                        switch ((CommunicationType)rdrCommunication.GetByte(1))
                        {
                            case CommunicationType.IncomingCall:
                                row.Cells.Add(new DataGridViewTextBoxCell { Value = CommunicationType.IncomingCall.ToString() });
                                break;
                            case CommunicationType.OutgoingCall:
                                row.Cells.Add(new DataGridViewTextBoxCell { Value = CommunicationType.OutgoingCall.ToString() });
                                break;
                            case CommunicationType.IncommingFax:
                                row.Cells.Add(new DataGridViewTextBoxCell { Value = CommunicationType.IncommingFax.ToString() });
                                break;
                            case CommunicationType.OutgoingFax:
                                row.Cells.Add(new DataGridViewTextBoxCell { Value = CommunicationType.OutgoingFax.ToString() });
                                break;
                            case CommunicationType.IncomingEFax:
                                row.Cells.Add(new DataGridViewTextBoxCell { Value = CommunicationType.IncomingEFax.ToString() });
                                break;
                            case CommunicationType.OutgoingEFax:
                                row.Cells.Add(new DataGridViewTextBoxCell { Value = CommunicationType.OutgoingEFax.ToString() });
                                break;
                            case CommunicationType.EmailReceived:
                                row.Cells.Add(new DataGridViewTextBoxCell { Value = CommunicationType.EmailReceived.ToString() });
                                break;
                            case CommunicationType.EmailSent:
                                row.Cells.Add(new DataGridViewTextBoxCell { Value = CommunicationType.EmailSent.ToString() });
                                break;
                            case CommunicationType.LetterReceived:
                                row.Cells.Add(new DataGridViewTextBoxCell { Value = CommunicationType.LetterReceived.ToString() });
                                break;
                            case CommunicationType.LetterMailed:
                                row.Cells.Add(new DataGridViewTextBoxCell { Value = CommunicationType.LetterMailed.ToString() });
                                break;
                            case CommunicationType.Other:
                                row.Cells.Add(new DataGridViewTextBoxCell { Value = CommunicationType.Other.ToString() });
                                break;
                        }
                    }
                    else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                    if (!rdrCommunication.IsDBNull(2)) row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrCommunication.GetString(2) });
                    else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                    if (!rdrCommunication.IsDBNull(3)) row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrCommunication.GetString(3) });
                    else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });

                    gvCommunicationList.Rows.Add(row);
                }
            }
            rdrCommunication.Close();
            if (connRN.State != ConnectionState.Closed) connRN.Close();

            

            List<TaskInfo> lstTaskInfo = new List<TaskInfo>();

            String strSqlQueryForTaskForMembership = "select [dbo].[tbl_task].[id], " +
                                                     "[dbo].[tbl_task].[Subject], [dbo].[tbl_task].[Comment], [dbo].[tbl_task].[Solution], [dbo].[tbl_task].[CreateDate] " +
                                                     "from [dbo].[tbl_task] " +
                                                     "where [dbo].[tbl_task].[whoid] = @IndividualId and [dbo].[tbl_task].[whatid] = @MembershipNo";
            //"order by [dbo].[tbl_task].[CreateDate] desc";

            SqlCommand cmdQueryForTaskForMembership = new SqlCommand(strSqlQueryForTaskForMembership, connRN);
            cmdQueryForTaskForMembership.CommandType = CommandType.Text;

            //txtIndividualName.Text = IndividualName;
            //txtMembershipNo.Text = IndividualMembershipNo;

            cmdQueryForTaskForMembership.Parameters.AddWithValue("@IndividualId", IndividualId);
            cmdQueryForTaskForMembership.Parameters.AddWithValue("@MembershipNo", IndividualMembershipNo);

            if (connRN.State != ConnectionState.Closed)
            {
                connRN.Close();
                connRN.Open();
            }
            else if (connRN.State == ConnectionState.Closed) connRN.Open();
            SqlDataReader rdrTaskForMembership = cmdQueryForTaskForMembership.ExecuteReader();
            if (rdrTaskForMembership.HasRows)
            {
                while (rdrTaskForMembership.Read())
                {
                    TaskInfo info = new TaskInfo();

                    if (!rdrTaskForMembership.IsDBNull(0)) info.TaskId = rdrTaskForMembership.GetInt32(0);
                    else info.TaskId = null;
                    if (!rdrTaskForMembership.IsDBNull(1)) info.Subject = rdrTaskForMembership.GetString(1);
                    else info.Subject = String.Empty;
                    if (!rdrTaskForMembership.IsDBNull(2)) info.Comment = rdrTaskForMembership.GetString(2);
                    else info.Comment = String.Empty;
                    if (!rdrTaskForMembership.IsDBNull(3)) info.Solution = rdrTaskForMembership.GetString(3);
                    else info.Solution = String.Empty;
                    if (!rdrTaskForMembership.IsDBNull(4)) info.CreateDate = rdrTaskForMembership.GetDateTime(4);
                    else info.CreateDate = null;

                    lstTaskInfo.Add(info);
                }
            }
            rdrTaskForMembership.Close();
            if (connRN.State != ConnectionState.Closed) connRN.Close();

            String strSqlQueryForTaskForIndividual = "select [dbo].[tbl_task].[id], " +
                                                     "[dbo].[tbl_task].[Subject], [dbo].[tbl_task].[Comment], [dbo].[tbl_task].[Solution], [dbo].[tbl_task].[CreateDate] " +
                                                     "from [dbo].[tbl_task] " +
                                                     "where [dbo].[tbl_task].[whoid] = @IndividualId and [dbo].[tbl_task].[whatid] <> @MembershipNo";
            //"order by [dbo].[tbl_task].[CreateDate] Desc";

            SqlCommand cmdQueryForTaskForIndividual = new SqlCommand(strSqlQueryForTaskForIndividual, connRN);
            cmdQueryForTaskForIndividual.CommandType = CommandType.Text;

            cmdQueryForTaskForIndividual.Parameters.AddWithValue("@IndividualId", IndividualId);
            cmdQueryForTaskForIndividual.Parameters.AddWithValue("@MembershipNo", IndividualMembershipNo);

            if (connRN.State != ConnectionState.Closed)
            {
                connRN.Close();
                connRN.Open();
            }
            else if (connRN.State == ConnectionState.Closed) connRN.Open();
            SqlDataReader rdrTaskForIndividual = cmdQueryForTaskForIndividual.ExecuteReader();
            if (rdrTaskForIndividual.HasRows)
            {
                while (rdrTaskForIndividual.Read())
                {
                    TaskInfo info = new TaskInfo();

                    if (!rdrTaskForIndividual.IsDBNull(0)) info.TaskId = rdrTaskForIndividual.GetInt32(0);
                    else info.TaskId = null;
                    if (!rdrTaskForIndividual.IsDBNull(1)) info.Subject = rdrTaskForIndividual.GetString(1);
                    else info.Subject = String.Empty;
                    if (!rdrTaskForIndividual.IsDBNull(2)) info.Comment = rdrTaskForIndividual.GetString(2);
                    else info.Comment = String.Empty;
                    if (!rdrTaskForIndividual.IsDBNull(3)) info.Solution = rdrTaskForIndividual.GetString(3);
                    else info.Solution = String.Empty;
                    if (!rdrTaskForIndividual.IsDBNull(4)) info.CreateDate = rdrTaskForIndividual.GetDateTime(4);
                    else info.CreateDate = null;

                    lstTaskInfo.Add(info);
                }
            }
            rdrTaskForIndividual.Close();
            if (connRN.State != ConnectionState.Closed) connRN.Close();

            lstTaskInfo.Sort(delegate (TaskInfo info1, TaskInfo info2)
            {
                if (info1.CreateDate == null && info2.CreateDate == null) return 0;
                else if (info1.CreateDate == null) return -1;
                else if (info2.CreateDate == null) return 1;
                else return (-1) * info1.CreateDate.Value.CompareTo(info2.CreateDate.Value);
            });

            gvTaskList.Rows.Clear();
            foreach (TaskInfo info in lstTaskInfo)
            {
                DataGridViewRow row = new DataGridViewRow();

                row.Cells.Add(new DataGridViewTextBoxCell { Value = info.TaskId.ToString() });
                row.Cells.Add(new DataGridViewTextBoxCell { Value = info.Subject });
                row.Cells.Add(new DataGridViewTextBoxCell { Value = info.Comment });
                row.Cells.Add(new DataGridViewTextBoxCell { Value = info.Solution });

                gvTaskList.Rows.Add(row);
            }

        }

        private void InitializeCommunicationDetail()
        {
            txtCommunicationType.Text = String.Empty;
            txtCommunicationCreatedBy.Text = String.Empty;
            txtCommunicationCreateDate.Text = String.Empty;
            txtCommunicationSubject.Text = String.Empty;
            txtCommunicationBody.Text = String.Empty;
        }

        private void InitializeTaskDetail()
        {
            txtTaskCreatedBy.Text = String.Empty;
            txtTaskAssignedTo.Text = String.Empty;
            txtTaskCreateDate.Text = String.Empty;
            txtTaskDueDate.Text = String.Empty;
            txtTaskSubject.Text = String.Empty;
            txtTaskComment.Text = String.Empty;
            txtTaskSolution.Text = String.Empty;
            txtTaskStatus.Text = String.Empty;
        }

        private void gvCommunicationList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView gvCommunication = sender as DataGridView;

            InitializeCommunicationDetail();

            String CommunicationNo = String.Empty;
            if (gvCommunication["CommNo", e.RowIndex]?.Value != null) CommunicationNo = gvCommunication["CommNo", e.RowIndex]?.Value?.ToString();

            String strSqlQueryForCommuniation = "select [dbo].[tbl_communication_type_code].[CommunicationTypeValue], [dbo].[tbl_CreateStaff].[Staff_Name], " +
                                                "[dbo].[tbl_Communication].[CreateDate], [dbo].[tbl_Communication].[Subject], [dbo].[tbl_Communication].[Body] " +
                                                "from [dbo].[tbl_Communication] " +
                                                "inner join [dbo].[tbl_communication_type_code] " +
                                                "on [dbo].[tbl_Communication].[CommunicationType] = [dbo].[tbl_communication_type_code].[CommunicationTypeId] " +
                                                "inner join [dbo].[tbl_CreateStaff] on [dbo].[tbl_Communication].[CreatedBy] = [dbo].[tbl_CreateStaff].[CreateStaff_Id] " +
                                                "where [dbo].[tbl_Communication].[CommunicationNo] = @CommunicationNo";

            SqlCommand cmdQueryForCommunication = new SqlCommand(strSqlQueryForCommuniation, connRN);
            cmdQueryForCommunication.CommandType = CommandType.Text;

            cmdQueryForCommunication.Parameters.AddWithValue("@CommunicationNo", CommunicationNo);

            if (connRN.State != ConnectionState.Closed)
            {
                connRN.Close();
                connRN.Open();
            }
            else if (connRN.State == ConnectionState.Closed) connRN.Open();
            SqlDataReader rdrCommuniationDetail = cmdQueryForCommunication.ExecuteReader();
            if (rdrCommuniationDetail.HasRows)
            {
                while (rdrCommuniationDetail.Read())
                {
                    if (!rdrCommuniationDetail.IsDBNull(0)) txtCommunicationType.Text = rdrCommuniationDetail.GetString(0);
                    else txtCommunicationType.Text = String.Empty;
                    if (!rdrCommuniationDetail.IsDBNull(1)) txtCommunicationCreatedBy.Text = rdrCommuniationDetail.GetString(1);
                    else txtCommunicationCreatedBy.Text = String.Empty;
                    if (!rdrCommuniationDetail.IsDBNull(2)) txtCommunicationCreateDate.Text = rdrCommuniationDetail.GetDateTime(2).ToString();
                    else txtCommunicationCreateDate.Text = String.Empty;
                    if (!rdrCommuniationDetail.IsDBNull(3)) txtCommunicationSubject.Text = rdrCommuniationDetail.GetString(3);
                    else txtCommunicationSubject.Text = String.Empty;
                    if (!rdrCommuniationDetail.IsDBNull(4)) txtCommunicationBody.Text = rdrCommuniationDetail.GetString(4);
                    else txtCommunicationBody.Text = String.Empty;
                }
            }
            rdrCommuniationDetail.Close();
            if (connRN.State != ConnectionState.Closed) connRN.Close();
        }

        //private void InitializeTaskDetail()
        //{
        //    txtTaskCreatedBy.Text = String.Empty;
        //    txtTaskCreateDate.Text = String.Empty;
        //    txtTaskAssignedTo.Text = String.Empty;
        //    txtTaskDueDate.Text = String.Empty;
        //    txtTaskSubject.Text = String.Empty;
        //    txtTaskComment.Text = String.Empty;
        //    txtTaskSolution.Text = String.Empty;
        //    txtTaskStatus.Text = String.Empty;
        //}

        private void gvTaskList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView gvTask = sender as DataGridView;

            InitializeTaskDetail();

            String TaskId = String.Empty;
            if (gvTask["TaskIdTask", e.RowIndex]?.Value != null) TaskId = gvTask["TaskIdTask", e.RowIndex]?.Value?.ToString();


            int? nTaskId = null;
            if (TaskId != String.Empty) nTaskId = Int32.Parse(TaskId);

            String strSqlQueryForTaskDetail = "select [dbo].[tbl_CreateStaff].[Staff_Name], [dbo].[tbl_task].[CreateDate], [dbo].[tbl_task_assigned_to].[User_Name], " +
                                              "[dbo].[tbl_task].[DueDate], [dbo].[tbl_task].[Subject], [dbo].[tbl_task].[Comment], [dbo].[tbl_task].[Solution], " +
                                              "[dbo].[tbl_task_status_code].[TaskStatusValue] " +
                                              "from [dbo].[tbl_task] " +
                                              "inner join [dbo].[tbl_CreateStaff] on [dbo].[tbl_task].[CreatedById] = [dbo].[tbl_CreateStaff].[CreateStaff_Id] " +
                                              "inner join [dbo].[tbl_task_assigned_to] on [dbo].[tbl_task].[AssignedTo] = [dbo].[tbl_task_assigned_to].[User_Id] " +
                                              "inner join [dbo].[tbl_task_status_code] on [dbo].[tbl_task].[Status] = [dbo].[tbl_task_status_code].[TaskStatusCode] " +
                                              "where [dbo].[tbl_Task].[id] = @TaskId";

            SqlCommand cmdQueryForTaskDetail = new SqlCommand(strSqlQueryForTaskDetail, connRN);
            cmdQueryForTaskDetail.CommandType = CommandType.Text;

            cmdQueryForTaskDetail.Parameters.AddWithValue("@TaskId", nTaskId);

            if (connRN.State != ConnectionState.Closed)
            {
                connRN.Close();
                connRN.Open();
            }
            else if (connRN.State == ConnectionState.Closed) connRN.Open();
            SqlDataReader rdrTaskDetail = cmdQueryForTaskDetail.ExecuteReader();
            if (rdrTaskDetail.HasRows)
            {
                while (rdrTaskDetail.Read())
                {
                    if (!rdrTaskDetail.IsDBNull(0)) txtTaskCreatedBy.Text = rdrTaskDetail.GetString(0);
                    else txtTaskCreatedBy.Text = String.Empty;
                    if (!rdrTaskDetail.IsDBNull(1)) txtTaskCreateDate.Text = rdrTaskDetail.GetDateTime(1).ToString("MM/dd/yyyy HH:mm:ss");
                    else txtTaskCreateDate.Text = String.Empty;
                    if (!rdrTaskDetail.IsDBNull(2)) txtTaskAssignedTo.Text = rdrTaskDetail.GetString(2);
                    else txtTaskAssignedTo.Text = String.Empty;
                    if (!rdrTaskDetail.IsDBNull(3)) txtTaskDueDate.Text = rdrTaskDetail.GetDateTime(3).ToString("MM/dd/yyyy HH:mm:ss");
                    else txtTaskDueDate.Text = String.Empty;
                    if (!rdrTaskDetail.IsDBNull(4)) txtTaskSubject.Text = rdrTaskDetail.GetString(4);
                    else txtTaskSubject.Text = String.Empty;
                    if (!rdrTaskDetail.IsDBNull(5)) txtTaskComment.Text = rdrTaskDetail.GetString(5);
                    else txtTaskComment.Text = String.Empty;
                    if (!rdrTaskDetail.IsDBNull(6)) txtTaskSolution.Text = rdrTaskDetail.GetString(6);
                    else txtTaskSolution.Text = String.Empty;
                    if (!rdrTaskDetail.IsDBNull(7)) txtTaskStatus.Text = rdrTaskDetail.GetString(7);
                    else txtTaskStatus.Text = String.Empty;
                }
            }
            rdrTaskDetail.Close();
            if (connRN.State != ConnectionState.Closed) connRN.Close();
        }

        private void comboCaseNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cbCaseNo = sender as ComboBox;

            if (cbCaseNo.SelectedItem != null)
            {
                if (cbCaseNo.SelectedItem.ToString() != "All")
                {
                    String SelectedCaseNo = cbCaseNo.SelectedItem.ToString();

                    comboIllnessNo.Items.Clear();
                    comboIllnessNo.Text = String.Empty;

                    String strSqlQueryForIllnessNo = "select [dbo].[tbl_illness].[IllnessNo] from [dbo].[tbl_illness] where [dbo].[tbl_illness].[Case_Id] = @CaseNo";

                    SqlCommand cmdQueryForIllnessNo = new SqlCommand(strSqlQueryForIllnessNo, connRN);
                    cmdQueryForIllnessNo.CommandType = CommandType.Text;

                    cmdQueryForIllnessNo.Parameters.AddWithValue("@CaseNo", SelectedCaseNo);

                    if (connRN.State != ConnectionState.Closed)
                    {
                        connRN.Close();
                        connRN.Open();
                    }
                    else if (connRN.State == ConnectionState.Closed) connRN.Open();
                    SqlDataReader rdrIllnessNo = cmdQueryForIllnessNo.ExecuteReader();
                    if (rdrIllnessNo.HasRows)
                    {
                        while (rdrIllnessNo.Read())
                        {
                            if (!rdrIllnessNo.IsDBNull(0)) comboIllnessNo.Items.Add(rdrIllnessNo.GetString(0));
                        }
                    }
                    rdrIllnessNo.Close();
                    if (connRN.State != ConnectionState.Closed) connRN.Close();

                    if (comboIllnessNo.Items.Count > 0) comboIllnessNo.Items.Insert(0, "All");

                    if (comboIllnessNo.Items.Count > 0)
                    {
                        comboIllnessNo.SelectedIndexChanged -= comboIllnessNo_SelectedIndexChanged;
                        comboIllnessNo.SelectedIndex = 0;
                        comboIllnessNo.SelectedIndexChanged += comboIllnessNo_SelectedIndexChanged;
                        comboIllnessNo.Enabled = true;
                    }

                    gvCommunicationList.Rows.Clear();
                    InitializeCommunicationDetail();

                    String strSqlQueryForCommunication = "select [dbo].[tbl_Communication].[CommunicationNo], [dbo].[tbl_Communication].[CommunicationType], " +
                                                         "[dbo].[tbl_Communication].[Subject], [dbo].[tbl_Communication].[Body] " +
                                                         "from [dbo].[tbl_Communication] " +
                                                         "where [dbo].[tbl_Communication].[Individual_Id] = @IndividualId and " +
                                                         "[dbo].[tbl_Communication].[CaseNo] = @SelectedCaseNo " +
                                                         "order by [dbo].[tbl_Communication].[CreateDate] desc";

                    SqlCommand cmdQueryForCommunication = new SqlCommand(strSqlQueryForCommunication, connRN);
                    cmdQueryForCommunication.CommandType = CommandType.Text;

                    cmdQueryForCommunication.Parameters.AddWithValue("@IndividualId", IndividualId);
                    cmdQueryForCommunication.Parameters.AddWithValue("@SelectedCaseNo", SelectedCaseNo);

                    if (connRN.State != ConnectionState.Closed)
                    {
                        connRN.Close();
                        connRN.Open();
                    }
                    else if (connRN.State == ConnectionState.Closed) connRN.Open();
                    SqlDataReader rdrCommunication = cmdQueryForCommunication.ExecuteReader();
                    if (rdrCommunication.HasRows)
                    {
                        while (rdrCommunication.Read())
                        {
                            DataGridViewRow row = new DataGridViewRow();
                            if (!rdrCommunication.IsDBNull(0)) row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrCommunication.GetString(0) });
                            else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                            if (!rdrCommunication.IsDBNull(1))
                            {
                                switch ((CommunicationType)rdrCommunication.GetByte(1))
                                {
                                    case CommunicationType.IncomingCall:
                                        row.Cells.Add(new DataGridViewTextBoxCell { Value = CommunicationType.IncomingCall.ToString() });
                                        break;
                                    case CommunicationType.OutgoingCall:
                                        row.Cells.Add(new DataGridViewTextBoxCell { Value = CommunicationType.OutgoingCall.ToString() });
                                        break;
                                    case CommunicationType.IncommingFax:
                                        row.Cells.Add(new DataGridViewTextBoxCell { Value = CommunicationType.IncommingFax.ToString() });
                                        break;
                                    case CommunicationType.OutgoingFax:
                                        row.Cells.Add(new DataGridViewTextBoxCell { Value = CommunicationType.OutgoingFax.ToString() });
                                        break;
                                    case CommunicationType.IncomingEFax:
                                        row.Cells.Add(new DataGridViewTextBoxCell { Value = CommunicationType.IncomingEFax.ToString() });
                                        break;
                                    case CommunicationType.OutgoingEFax:
                                        row.Cells.Add(new DataGridViewTextBoxCell { Value = CommunicationType.OutgoingEFax.ToString() });
                                        break;
                                    case CommunicationType.EmailReceived:
                                        row.Cells.Add(new DataGridViewTextBoxCell { Value = CommunicationType.EmailReceived.ToString() });
                                        break;
                                    case CommunicationType.EmailSent:
                                        row.Cells.Add(new DataGridViewTextBoxCell { Value = CommunicationType.EmailSent.ToString() });
                                        break;
                                    case CommunicationType.LetterReceived:
                                        row.Cells.Add(new DataGridViewTextBoxCell { Value = CommunicationType.LetterReceived.ToString() });
                                        break;
                                    case CommunicationType.LetterMailed:
                                        row.Cells.Add(new DataGridViewTextBoxCell { Value = CommunicationType.LetterMailed.ToString() });
                                        break;
                                    case CommunicationType.Other:
                                        row.Cells.Add(new DataGridViewTextBoxCell { Value = CommunicationType.Other.ToString() });
                                        break;
                                }
                            }
                            else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                            if (!rdrCommunication.IsDBNull(2)) row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrCommunication.GetString(2) });
                            else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                            if (!rdrCommunication.IsDBNull(3)) row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrCommunication.GetString(3) });
                            else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });

                            gvCommunicationList.Rows.Add(row);
                        }
                    }
                    rdrCommunication.Close();
                    if (connRN.State != ConnectionState.Closed) connRN.Close();


                    List<TaskInfo> lstTaskInfo = new List<TaskInfo>();

                    gvTaskList.Rows.Clear();
                    InitializeTaskDetail();

                    String strSqlQueryForTaskInCase = "select [dbo].[tbl_task].[id], [dbo].[tbl_task].[Subject], [dbo].[tbl_task].[Comment], [dbo].[tbl_task].[Solution], " +
                                                      "[dbo].[tbl_task].[CreateDate] " +
                                                      "from [dbo].[tbl_task] " +
                                                      "where [dbo].[tbl_task].[whoid] = @IndividualId and " +
                                                      "[dbo].[tbl_task].[whatid] = @CaseNoSelected";
                                                      //"order by [dbo].[tbl_task].[CreateDate] desc";

                    SqlCommand cmdQueryForTaskInCase = new SqlCommand(strSqlQueryForTaskInCase, connRN);
                    cmdQueryForTaskInCase.CommandType = CommandType.Text;

                    cmdQueryForTaskInCase.Parameters.AddWithValue("@IndividualId", IndividualId);
                    cmdQueryForTaskInCase.Parameters.AddWithValue("@CaseNoSelected", SelectedCaseNo);

                    if (connRN.State != ConnectionState.Closed)
                    {
                        connRN.Close();
                        connRN.Open();
                    }
                    else if (connRN.State == ConnectionState.Closed) connRN.Open();
                    SqlDataReader rdrTaskInCase = cmdQueryForTaskInCase.ExecuteReader();
                    if (rdrTaskInCase.HasRows)
                    {
                        while (rdrTaskInCase.Read())
                        {
                            TaskInfo info = new TaskInfo();
                            if (!rdrTaskInCase.IsDBNull(0)) info.TaskId = rdrTaskInCase.GetInt32(0);
                            else info.TaskId = null;
                            if (!rdrTaskInCase.IsDBNull(1)) info.Subject = rdrTaskInCase.GetString(1);
                            else info.Subject = String.Empty;
                            if (!rdrTaskInCase.IsDBNull(2)) info.Comment = rdrTaskInCase.GetString(2);
                            else info.Comment = String.Empty;
                            if (!rdrTaskInCase.IsDBNull(3)) info.Solution = rdrTaskInCase.GetString(3);
                            else info.Solution = String.Empty;
                            if (!rdrTaskInCase.IsDBNull(4)) info.CreateDate = rdrTaskInCase.GetDateTime(4);
                            else info.CreateDate = null;

                            lstTaskInfo.Add(info);
                        }
                    }
                    rdrTaskInCase.Close();
                    if (connRN.State != ConnectionState.Closed) connRN.Close();

                    // 08/07/19 - begin here to write sql query for illness Id and illness no for given case no
                    List<IllnessInfo> lstIllnessInfo = new List<IllnessInfo>();

                    String strSqlQueryForIllnessInfo = "select [dbo].[tbl_illness].[Illness_Id], [dbo].[tbl_illness].[IllnessNo] from [dbo].[tbl_illness] " +
                                                       "where [dbo].[tbl_illness].[Individual_Id] = @IndividualId and " +
                                                       "[dbo].[tbl_illness].[Case_Id] = @CaseNo";

                    SqlCommand cmdQueryForIllnessInfo = new SqlCommand(strSqlQueryForIllnessInfo, connRN);
                    cmdQueryForIllnessInfo.CommandType = CommandType.Text;

                    cmdQueryForIllnessInfo.Parameters.AddWithValue("@IndividualId", IndividualId);
                    cmdQueryForIllnessInfo.Parameters.AddWithValue("@CaseNo", SelectedCaseNo);

                    if (connRN.State != ConnectionState.Closed)
                    {
                        connRN.Close();
                        connRN.Open();
                    }
                    else if (connRN.State == ConnectionState.Closed) connRN.Open();
                    SqlDataReader rdrIllnessInfo = cmdQueryForIllnessInfo.ExecuteReader();
                    if (rdrIllnessInfo.HasRows)
                    {
                        while (rdrIllnessInfo.Read())
                        {
                            IllnessInfo info = new IllnessInfo();

                            if (!rdrIllnessInfo.IsDBNull(0)) info.IllnessId = rdrIllnessInfo.GetInt32(0);
                            else info.IllnessId = null;
                            if (!rdrIllnessInfo.IsDBNull(1)) info.IllnessNo = rdrIllnessInfo.GetString(1);
                            else info.IllnessNo = String.Empty;

                            lstIllnessInfo.Add(info);
                        }
                    }
                    rdrIllnessInfo.Close();
                    if (connRN.State != ConnectionState.Closed) connRN.Close();

                    if (lstIllnessInfo.Count > 0)
                    {
                        foreach (IllnessInfo illness_info in lstIllnessInfo)
                        {
                            String strSqlQueryForTaskForIllnessNo = "select [dbo].[tbl_task].[id], [dbo].[tbl_task].[Subject], [dbo].[tbl_task].[Comment], [dbo].[tbl_task].[Solution], " +
                                                                    "[dbo].[tbl_task].[CreateDate] " +
                                                                    "from [dbo].[tbl_task] " +
                                                                    "where [dbo].[tbl_task].[whoid] = @IndividualId and " +
                                                                    "[dbo].[tbl_task].[whatid] = @IllnessNo";

                            SqlCommand cmdQueryForTaskForIllnessNo = new SqlCommand(strSqlQueryForTaskForIllnessNo, connRN);
                            cmdQueryForTaskForIllnessNo.CommandType = CommandType.Text;

                            cmdQueryForTaskForIllnessNo.Parameters.AddWithValue("@IndividualId", IndividualId);
                            cmdQueryForTaskForIllnessNo.Parameters.AddWithValue("@IllnessNo", illness_info.IllnessNo);

                            if (connRN.State != ConnectionState.Closed)
                            {
                                connRN.Close();
                                connRN.Open();
                            }
                            else if (connRN.State == ConnectionState.Closed) connRN.Open();
                            SqlDataReader rdrTaskForIllnessNo = cmdQueryForTaskForIllnessNo.ExecuteReader();
                            if (rdrTaskForIllnessNo.HasRows)
                            {
                                while (rdrTaskForIllnessNo.Read())
                                {
                                    TaskInfo info = new TaskInfo();

                                    if (!rdrTaskForIllnessNo.IsDBNull(0)) info.TaskId = rdrTaskForIllnessNo.GetInt32(0);
                                    else info.TaskId = null;
                                    if (!rdrTaskForIllnessNo.IsDBNull(1)) info.Subject = rdrTaskForIllnessNo.GetString(1);
                                    else info.Subject = String.Empty;
                                    if (!rdrTaskForIllnessNo.IsDBNull(2)) info.Comment = rdrTaskForIllnessNo.GetString(2);
                                    else info.Comment = String.Empty;
                                    if (!rdrTaskForIllnessNo.IsDBNull(3)) info.Solution = rdrTaskForIllnessNo.GetString(3);
                                    else info.Solution = String.Empty;

                                    lstTaskInfo.Add(info);
                                }
                            }
                            rdrTaskForIllnessNo.Close();
                            if (connRN.State != ConnectionState.Closed) connRN.Close();
                        }
                    }
                    else
                    {
                        comboIllnessNo.Text = String.Empty;
                        comboIllnessNo.Items.Clear();
                        comboIllnessNo.Enabled = false;
                    }

                    List<String> lstIncidentNo = new List<string>();

                    foreach (IllnessInfo illness_info in lstIllnessInfo)
                    {
                        String strSqlQueryForIncidentNo = "select [dbo].[tbl_incident].[IncidentNo] from [dbo].[tbl_incident] " +
                                                          "where [dbo].[tbl_incident].[Individual_id] = @IndividualId and " +
                                                          "[dbo].[tbl_incident].[Case_id] = @CaseNo and " +
                                                          "[dbo].[tbl_incident].[Illness_id] = @IllnessId";

                        SqlCommand cmdQueryForIncidentNo = new SqlCommand(strSqlQueryForIncidentNo, connRN);
                        cmdQueryForIncidentNo.CommandType = CommandType.Text;

                        cmdQueryForIncidentNo.Parameters.AddWithValue("@IndividualId", IndividualId);
                        cmdQueryForIncidentNo.Parameters.AddWithValue("@CaseNo", SelectedCaseNo);
                        cmdQueryForIncidentNo.Parameters.AddWithValue("@IllnessId", illness_info.IllnessId);

                        if (connRN.State != ConnectionState.Closed)
                        {
                            connRN.Close();
                            connRN.Open();
                        }
                        else if (connRN.State == ConnectionState.Closed) connRN.Open();
                        SqlDataReader rdrIncidentNo = cmdQueryForIncidentNo.ExecuteReader();
                        if (rdrIncidentNo.HasRows)
                        {
                            while (rdrIncidentNo.Read())
                            {
                                if (!rdrIncidentNo.IsDBNull(0)) lstIncidentNo.Add(rdrIncidentNo.GetString(0));
                            }
                        }
                        rdrIncidentNo.Close();
                        if (connRN.State != ConnectionState.Closed) connRN.Close();
                    }

                    if (lstIncidentNo.Count > 0)
                    {
                        foreach (String incd_no in lstIncidentNo)
                        {
                            String strSqlQueryForTaskInfo = "select [dbo].[tbl_task].[id], [dbo].[tbl_task].[Subject], [dbo].[tbl_task].[Comment], [dbo].[tbl_task].[Solution], " +
                                                            "[dbo].[tbl_task].[CreateDate] " +
                                                            "from [dbo].[tbl_task] " +
                                                            "where [dbo].[tbl_task].[whoid] = @IndividualId and [dbo].[tbl_task].[whatid] = @IncidentNo";

                            SqlCommand cmdQueryForTaskInfo = new SqlCommand(strSqlQueryForTaskInfo, connRN);
                            cmdQueryForTaskInfo.CommandType = CommandType.Text;

                            cmdQueryForTaskInfo.Parameters.AddWithValue("@IndividualId", IndividualId);
                            cmdQueryForTaskInfo.Parameters.AddWithValue("@IncidentNo", incd_no);

                            if (connRN.State != ConnectionState.Closed)
                            {
                                connRN.Close();
                                connRN.Open();
                            }
                            else if (connRN.State == ConnectionState.Closed) connRN.Open();
                            SqlDataReader rdrTaskInfo = cmdQueryForTaskInfo.ExecuteReader();
                            if (rdrTaskInfo.HasRows)
                            {
                                while (rdrTaskInfo.Read())
                                {
                                    TaskInfo info = new TaskInfo();
                                    if (!rdrTaskInfo.IsDBNull(0)) info.TaskId = rdrTaskInfo.GetInt32(0);
                                    else info.TaskId = null;
                                    if (!rdrTaskInfo.IsDBNull(1)) info.Subject = rdrTaskInfo.GetString(1);
                                    else info.Subject = String.Empty;
                                    if (!rdrTaskInfo.IsDBNull(2)) info.Comment = rdrTaskInfo.GetString(2);
                                    else info.Comment = String.Empty;
                                    if (!rdrTaskInfo.IsDBNull(3)) info.Solution = rdrTaskInfo.GetString(3);
                                    else info.Solution = String.Empty;
                                    if (!rdrTaskInfo.IsDBNull(4)) info.CreateDate = rdrTaskInfo.GetDateTime(4);
                                    else info.CreateDate = null;

                                    lstTaskInfo.Add(info);
                                }
                            }
                            rdrTaskInfo.Close();
                            if (connRN.State != ConnectionState.Closed) connRN.Close();
                        }

                        List<int?> lstIncidentId = new List<int?>();

                        foreach (String incd_no in lstIncidentNo)
                        {
                            String strSqlQueryForIncidentId = "select [dbo].[tbl_incident].[Incident_id] from [dbo].[tbl_incident] where [dbo].[tbl_incident].[IncidentNo] = @IncidentNo";

                            SqlCommand cmdQueryForIncidentId = new SqlCommand(strSqlQueryForIncidentId, connRN);
                            cmdQueryForIncidentId.CommandType = CommandType.Text;

                            cmdQueryForIncidentId.Parameters.AddWithValue("@IncidentNo", incd_no);

                            if (connRN.State != ConnectionState.Closed)
                            {
                                connRN.Close();
                                connRN.Open();
                            }
                            else if (connRN.State == ConnectionState.Closed) connRN.Open();
                            SqlDataReader rdrIncidentId = cmdQueryForIncidentId.ExecuteReader();
                            if (rdrIncidentId.HasRows)
                            {
                                while (rdrIncidentId.Read())
                                {
                                    int? nIncidentId = null;
                                    if (!rdrIncidentId.IsDBNull(0)) nIncidentId = rdrIncidentId.GetInt32(0);
                                    else nIncidentId = null;

                                    lstIncidentId.Add(nIncidentId);
                                }
                            }
                            rdrIncidentId.Close();
                            if (connRN.State != ConnectionState.Closed) connRN.Close();
                        }

                        List<String> lstMedBillNo = new List<string>();

                        foreach (IllnessInfo ill_info in lstIllnessInfo)
                        {
                            foreach (int? nIncidentId in lstIncidentId)
                            {
                                String strSqlQueryForMedBillNo = "select [dbo].[tbl_medbill].[BillNo] from [dbo].[tbl_medbill] " +
                                                                 "where [dbo].[tbl_medbill].[Individual_Id] = @IndividualId and " +
                                                                 "[dbo].[tbl_medbill].[Case_Id] = @CaseNo and " +
                                                                 "[dbo].[tbl_medbill].[Illness_Id] = @IllnessId and " +
                                                                 "[dbo].[tbl_medbill].[Incident_Id] = @IncidentId";

                                SqlCommand cmdQueryForMedBillNo = new SqlCommand(strSqlQueryForMedBillNo, connRN);
                                cmdQueryForMedBillNo.CommandType = CommandType.Text;

                                cmdQueryForMedBillNo.Parameters.AddWithValue("@IndividualId", IndividualId);
                                cmdQueryForMedBillNo.Parameters.AddWithValue("@CaseNo", SelectedCaseNo);
                                cmdQueryForMedBillNo.Parameters.AddWithValue("@IllnessId", ill_info.IllnessId.Value);
                                cmdQueryForMedBillNo.Parameters.AddWithValue("@IncidentId", nIncidentId.Value);

                                if (connRN.State != ConnectionState.Closed)
                                {
                                    connRN.Close();
                                    connRN.Open();
                                }
                                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                                SqlDataReader rdrMedBillNo = cmdQueryForMedBillNo.ExecuteReader();
                                if (rdrMedBillNo.HasRows)
                                {
                                    while (rdrMedBillNo.Read())
                                    {
                                        String MedBillNo = String.Empty;
                                        if (!rdrMedBillNo.IsDBNull(0)) MedBillNo = rdrMedBillNo.GetString(0);
                                        else MedBillNo = String.Empty;

                                        lstMedBillNo.Add(MedBillNo);
                                    }
                                }
                                rdrMedBillNo.Close();
                                if (connRN.State != ConnectionState.Closed) connRN.Close();
                            }
                        }

                        foreach (String medbill_no in lstMedBillNo)
                        {
                            String strSqlQueryForTaskForMedBill = "select [dbo].[tbl_task].[id], [dbo].[tbl_task].[Subject], [dbo].[tbl_task].[Comment], [dbo].[tbl_task].[Solution] " +
                                                                  "from [dbo].[tbl_task] " +
                                                                  "where [dbo].[tbl_task].[whoid] = @IndividualId and [dbo].[tbl_task].[whatid] = @MedBillNo";

                            SqlCommand cmdQueryForTaskForMedBill = new SqlCommand(strSqlQueryForTaskForMedBill, connRN);
                            cmdQueryForTaskForMedBill.CommandType = CommandType.Text;

                            cmdQueryForTaskForMedBill.Parameters.AddWithValue("@IndividualId", IndividualId);
                            cmdQueryForTaskForMedBill.Parameters.AddWithValue("@MedBillNo", medbill_no);

                            if (connRN.State != ConnectionState.Closed)
                            {
                                connRN.Close();
                                connRN.Open();
                            }
                            else if (connRN.State == ConnectionState.Closed) connRN.Open();
                            SqlDataReader rdrTaskForMedBill = cmdQueryForTaskForMedBill.ExecuteReader();
                            if (rdrTaskForMedBill.HasRows)
                            {
                                while (rdrTaskForMedBill.Read())
                                {
                                    TaskInfo info = new TaskInfo();

                                    if (!rdrTaskForMedBill.IsDBNull(0)) info.TaskId = rdrTaskForMedBill.GetInt32(0);
                                    else info.TaskId = null;
                                    if (!rdrTaskForMedBill.IsDBNull(1)) info.Subject = rdrTaskForMedBill.GetString(1);
                                    else info.Subject = String.Empty;
                                    if (!rdrTaskForMedBill.IsDBNull(2)) info.Comment = rdrTaskForMedBill.GetString(2);
                                    else info.Comment = String.Empty;
                                    if (!rdrTaskForMedBill.IsDBNull(3)) info.Solution = rdrTaskForMedBill.GetString(3);
                                    else info.Solution = String.Empty;

                                    lstTaskInfo.Add(info);
                                }
                            }
                            rdrTaskForMedBill.Close();
                            if (connRN.State != ConnectionState.Closed) connRN.Close();
                        }

                        List<String> lstSettlementNo = new List<string>();

                        foreach (String medbill_no in lstMedBillNo)
                        {
                            String strSqlQueryForSettlementNo = "select [dbo].[tbl_settlement].[Name] from [dbo].[tbl_settlement] where [dbo].[tbl_settlement].[MedicalBillID] = @MedBillNo";

                            SqlCommand cmdQueryForSettlementNo = new SqlCommand(strSqlQueryForSettlementNo, connRN);
                            cmdQueryForSettlementNo.Parameters.AddWithValue("@MedBillNo", medbill_no);

                            if (connRN.State != ConnectionState.Closed)
                            {
                                connRN.Close();
                                connRN.Open();
                            }
                            else if (connRN.State == ConnectionState.Closed) connRN.Open();
                            SqlDataReader rdrSettlementNo = cmdQueryForSettlementNo.ExecuteReader();
                            if (rdrSettlementNo.HasRows)
                            {
                                while (rdrSettlementNo.Read())
                                {
                                    String SettlementNo = String.Empty;
                                    if (!rdrSettlementNo.IsDBNull(0)) SettlementNo = rdrSettlementNo.GetString(0);
                                    else SettlementNo = String.Empty;

                                    lstSettlementNo.Add(SettlementNo);
                                }
                            }
                            if (connRN.State != ConnectionState.Closed) connRN.Close();
                        }

                        foreach (String settlement_no in lstSettlementNo)
                        {
                            String strSqlQueryForTaskInfo = "select [dbo].[tbl_task].[id], [dbo].[tbl_task].[Subject], [dbo].[tbl_task].[Comment], [dbo].[tbl_task].[Solution] " +
                                                            "from [dbo].[tbl_task] " +
                                                            "where [dbo].[tbl_task].[whoid] = @IndividualId and [dbo].[tbl_task].[whatid] = @SettlementNo";

                            SqlCommand cmdQueryForTaskInfo = new SqlCommand(strSqlQueryForTaskInfo, connRN);
                            cmdQueryForTaskInfo.CommandType = CommandType.Text;

                            cmdQueryForTaskInfo.Parameters.AddWithValue("@IndividualId", IndividualId);
                            cmdQueryForTaskInfo.Parameters.AddWithValue("@SettlementNo", settlement_no);

                            if (connRN.State != ConnectionState.Closed)
                            {
                                connRN.Close();
                                connRN.Open();
                            }
                            else if (connRN.State == ConnectionState.Closed) connRN.Open();
                            SqlDataReader rdrTaskInfo = cmdQueryForTaskInfo.ExecuteReader();
                            if (rdrTaskInfo.HasRows)
                            {
                                while (rdrTaskInfo.Read())
                                {
                                    TaskInfo info = new TaskInfo();
                                    if (!rdrTaskInfo.IsDBNull(0)) info.TaskId = rdrTaskInfo.GetInt32(0);
                                    else info.TaskId = null;
                                    if (!rdrTaskInfo.IsDBNull(1)) info.Subject = rdrTaskInfo.GetString(1);
                                    else info.Subject = String.Empty;
                                    if (!rdrTaskInfo.IsDBNull(2)) info.Comment = rdrTaskInfo.GetString(2);
                                    else info.Comment = String.Empty;
                                    if (!rdrTaskInfo.IsDBNull(3)) info.Solution = rdrTaskInfo.GetString(3);
                                    else info.Solution = String.Empty;

                                    lstTaskInfo.Add(info);
                                }
                            }
                            if (connRN.State != ConnectionState.Closed) connRN.Close();
                        }
                    }
                    else
                    {
                        comboIncidentNo.Text = String.Empty;
                        comboIncidentNo.Items.Clear();
                        comboIncidentNo.Enabled = false;
                    }

                    lstTaskInfo.Sort(delegate (TaskInfo info1, TaskInfo info2)
                    {
                        if (info1.CreateDate == null && info2.CreateDate == null) return 0;
                        else if (info1.CreateDate == null) return -1;
                        else if (info2.CreateDate == null) return 1;
                        else return (-1) * info1.CreateDate.Value.CompareTo(info2.CreateDate.Value);
                    });

                    if (lstTaskInfo.Count > 0)
                    {
                        foreach (TaskInfo info in lstTaskInfo)
                        {
                            DataGridViewRow row = new DataGridViewRow();

                            row.Cells.Add(new DataGridViewTextBoxCell { Value = info.TaskId.ToString() });
                            row.Cells.Add(new DataGridViewTextBoxCell { Value = info.Subject });
                            row.Cells.Add(new DataGridViewTextBoxCell { Value = info.Comment });
                            row.Cells.Add(new DataGridViewTextBoxCell { Value = info.Solution });

                            gvTaskList.Rows.Add(row);
                        }
                    }
                }
                else
                {
                    comboIllnessNo.Items.Clear();
                    comboIllnessNo.Text = String.Empty;
                    comboIncidentNo.Items.Clear();
                    comboIncidentNo.Text = String.Empty;
                    comboIllnessNo.Enabled = false;
                    comboIncidentNo.Enabled = false;

                    gvCommunicationList.Rows.Clear();
                    String strSqlQueryForCommunication = "select [dbo].[tbl_Communication].[CommunicationNo], [dbo].[tbl_Communication].[CommunicationType], " +
                                                         "[dbo].[tbl_Communication].[Subject], [dbo].[tbl_Communication].[Body] " +
                                                         "from [dbo].[tbl_Communication] " +
                                                         "where [dbo].[tbl_Communication].[Individual_Id] = @IndividualId " +
                                                         "order by [dbo].[tbl_Communication].[CreateDate] desc";

                    SqlCommand cmdQueryForCommunication = new SqlCommand(strSqlQueryForCommunication, connRN);
                    cmdQueryForCommunication.CommandType = CommandType.Text;

                    cmdQueryForCommunication.Parameters.AddWithValue("@IndividualId", IndividualId);

                    if (connRN.State != ConnectionState.Closed)
                    {
                        connRN.Close();
                        connRN.Open();
                    }
                    else if (connRN.State == ConnectionState.Closed) connRN.Open();
                    SqlDataReader rdrCommunication = cmdQueryForCommunication.ExecuteReader();
                    if (rdrCommunication.HasRows)
                    {
                        while (rdrCommunication.Read())
                        {
                            DataGridViewRow row = new DataGridViewRow();
                            if (!rdrCommunication.IsDBNull(0)) row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrCommunication.GetString(0) });
                            else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                            if (!rdrCommunication.IsDBNull(1))
                            {
                                switch ((CommunicationType)rdrCommunication.GetByte(1))
                                {
                                    case CommunicationType.IncomingCall:
                                        row.Cells.Add(new DataGridViewTextBoxCell { Value = CommunicationType.IncomingCall.ToString() });
                                        break;
                                    case CommunicationType.OutgoingCall:
                                        row.Cells.Add(new DataGridViewTextBoxCell { Value = CommunicationType.OutgoingCall.ToString() });
                                        break;
                                    case CommunicationType.IncommingFax:
                                        row.Cells.Add(new DataGridViewTextBoxCell { Value = CommunicationType.IncommingFax.ToString() });
                                        break;
                                    case CommunicationType.OutgoingFax:
                                        row.Cells.Add(new DataGridViewTextBoxCell { Value = CommunicationType.OutgoingFax.ToString() });
                                        break;
                                    case CommunicationType.IncomingEFax:
                                        row.Cells.Add(new DataGridViewTextBoxCell { Value = CommunicationType.IncomingEFax.ToString() });
                                        break;
                                    case CommunicationType.OutgoingEFax:
                                        row.Cells.Add(new DataGridViewTextBoxCell { Value = CommunicationType.OutgoingEFax.ToString() });
                                        break;
                                    case CommunicationType.EmailReceived:
                                        row.Cells.Add(new DataGridViewTextBoxCell { Value = CommunicationType.EmailReceived.ToString() });
                                        break;
                                    case CommunicationType.EmailSent:
                                        row.Cells.Add(new DataGridViewTextBoxCell { Value = CommunicationType.EmailSent.ToString() });
                                        break;
                                    case CommunicationType.LetterReceived:
                                        row.Cells.Add(new DataGridViewTextBoxCell { Value = CommunicationType.LetterReceived.ToString() });
                                        break;
                                    case CommunicationType.LetterMailed:
                                        row.Cells.Add(new DataGridViewTextBoxCell { Value = CommunicationType.LetterMailed.ToString() });
                                        break;
                                    case CommunicationType.Other:
                                        row.Cells.Add(new DataGridViewTextBoxCell { Value = CommunicationType.Other.ToString() });
                                        break;
                                }
                            }
                            else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                            if (!rdrCommunication.IsDBNull(2)) row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrCommunication.GetString(2) });
                            else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                            if (!rdrCommunication.IsDBNull(3)) row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrCommunication.GetString(3) });
                            else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });

                            gvCommunicationList.Rows.Add(row);
                        }
                    }
                    rdrCommunication.Close();
                    if (connRN.State != ConnectionState.Closed) connRN.Close();

                    gvTaskList.Rows.Clear();
                    String strSqlQueryForTask = "select [dbo].[tbl_task].[id], [dbo].[tbl_task].[Subject], [dbo].[tbl_task].[Comment], [dbo].[tbl_task].[Solution] from [dbo].[tbl_task] " +
                                                "where [dbo].[tbl_task].[whoid] = @IndividualId " +
                                                "order by [dbo].[tbl_task].[CreateDate] desc";

                    SqlCommand cmdQueryForTask = new SqlCommand(strSqlQueryForTask, connRN);
                    cmdQueryForTask.CommandType = CommandType.Text;

                    cmdQueryForTask.Parameters.AddWithValue("@IndividualId", IndividualId);

                    if (connRN.State != ConnectionState.Closed)
                    {
                        connRN.Close();
                        connRN.Open();
                    }
                    else if (connRN.State == ConnectionState.Closed) connRN.Open();
                    SqlDataReader rdrTask = cmdQueryForTask.ExecuteReader();
                    if (rdrTask.HasRows)
                    {
                        while (rdrTask.Read())
                        {
                            DataGridViewRow row = new DataGridViewRow();
                            if (!rdrTask.IsDBNull(0)) row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrTask.GetInt32(0).ToString() });
                            else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                            if (!rdrTask.IsDBNull(1)) row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrTask.GetString(1) });
                            else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                            if (!rdrTask.IsDBNull(2)) row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrTask.GetString(2) });
                            else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                            if (!rdrTask.IsDBNull(3)) row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrTask.GetString(3) });
                            else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });

                            gvTaskList.Rows.Add(row);
                        }
                    }
                    rdrTask.Close();
                    if (connRN.State != ConnectionState.Closed) connRN.Close();
                }
            }
        }

        private void comboIllnessNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboCaseNo.SelectedItem != null)
            {
                if (comboCaseNo.SelectedItem.ToString() != "All")
                {
                    gvCommunicationList.Rows.Clear();
                    gvTaskList.Rows.Clear();

                    String SelectedCaseNo = comboCaseNo.SelectedItem.ToString();

                    String SelectedIllnessNo = String.Empty;
                    if (comboIllnessNo.SelectedItem != null)
                    {
                        if (comboIllnessNo.SelectedItem.ToString() != "All")
                        {
                            SelectedIllnessNo = comboIllnessNo.SelectedItem.ToString();

                            int? nSelectedIllnessId = null;

                            String strSqlQueryForIllnessId = "select [dbo].[tbl_illness].[Illness_Id] from [dbo].[tbl_illness] where [dbo].[tbl_illness].[IllnessNo] = @IllnessNo";

                            SqlCommand cmdQueryForIllnessId = new SqlCommand(strSqlQueryForIllnessId, connRN);
                            cmdQueryForIllnessId.CommandType = CommandType.Text;

                            cmdQueryForIllnessId.Parameters.AddWithValue("@IllnessNo", SelectedIllnessNo);

                            if (connRN.State != ConnectionState.Closed)
                            {
                                connRN.Close();
                                connRN.Open();
                            }
                            else if (connRN.State == ConnectionState.Closed) connRN.Open();
                            Object objIllnessId = cmdQueryForIllnessId.ExecuteScalar();
                            if (connRN.State != ConnectionState.Closed) connRN.Close();

                            if (objIllnessId != null) nSelectedIllnessId = Int32.Parse(objIllnessId.ToString());

                            comboIncidentNo.Items.Clear();

                            String strSqlQueryForIncidentNo = "select [dbo].[tbl_incident].[IncidentNo] from [dbo].[tbl_incident] " +
                                                                "where [dbo].[tbl_incident].[Individual_id] = @IndividualId and " +
                                                                "[dbo].[tbl_incident].[Case_id] = @CaseNo and " +
                                                                "[dbo].[tbl_incident].[Illness_id] = @IllnessId";

                            SqlCommand cmdQueryForIncidentNo = new SqlCommand(strSqlQueryForIncidentNo, connRN);
                            cmdQueryForIncidentNo.CommandType = CommandType.Text;

                            cmdQueryForIncidentNo.Parameters.AddWithValue("@IndividualId", IndividualId);
                            cmdQueryForIncidentNo.Parameters.AddWithValue("@CaseNo", SelectedCaseNo);
                            cmdQueryForIncidentNo.Parameters.AddWithValue("@IllnessId", nSelectedIllnessId.Value);

                            if (connRN.State != ConnectionState.Closed)
                            {
                                connRN.Close();
                                connRN.Open();
                            }
                            else if (connRN.State == ConnectionState.Closed) connRN.Open();
                            SqlDataReader rdrIncidentNo = cmdQueryForIncidentNo.ExecuteReader();
                            if (rdrIncidentNo.HasRows)
                            {
                                while (rdrIncidentNo.Read())
                                {
                                    if (!rdrIncidentNo.IsDBNull(0)) comboIncidentNo.Items.Add(rdrIncidentNo.GetString(0));
                                }
                            }
                            rdrIncidentNo.Close();
                            if (connRN.State != ConnectionState.Closed) connRN.Close();

                            if (comboIncidentNo.Items.Count > 0)
                            {
                                comboIncidentNo.Items.Insert(0, "All");
                                comboIncidentNo.Enabled = true;
                            }

                            comboIncidentNo.SelectedIndexChanged -= comboIncidentNo_SelectedIndexChanged;
                            comboIncidentNo.SelectedIndex = 0;
                            comboIncidentNo.SelectedIndexChanged += comboIncidentNo_SelectedIndexChanged;

                            

                            String strSqlQueryForCommunication = "select [dbo].[tbl_Communication].[CommunicationNo], [dbo].[tbl_Communication].[CommunicationType], " +
                                     "[dbo].[tbl_Communication].[Subject], [dbo].[tbl_Communication].[Body] " +
                                     "from [dbo].[tbl_Communication] " +
                                     "where [dbo].[tbl_Communication].[Individual_Id] = @IndividualId and " +
                                     "[dbo].[tbl_Communication].[CaseNo] = @SelectedCaseNo and " +
                                     "[dbo].[tbl_Communication].[IllnessNo] = @SelectedIllnessNo " +
                                     "order by [dbo].[tbl_Communication].[CreateDate] desc";

                            SqlCommand cmdQueryForCommunication = new SqlCommand(strSqlQueryForCommunication, connRN);
                            cmdQueryForCommunication.CommandType = CommandType.Text;

                            cmdQueryForCommunication.Parameters.AddWithValue("@IndividualId", IndividualId);
                            cmdQueryForCommunication.Parameters.AddWithValue("@SelectedCaseNo", SelectedCaseNo);
                            cmdQueryForCommunication.Parameters.AddWithValue("@SelectedIllnessNo", SelectedIllnessNo);

                            if (connRN.State != ConnectionState.Closed)
                            {
                                connRN.Close();
                                connRN.Open();
                            }
                            else if (connRN.State == ConnectionState.Closed) connRN.Open();
                            SqlDataReader rdrCommunication = cmdQueryForCommunication.ExecuteReader();
                            if (rdrCommunication.HasRows)
                            {
                                while (rdrCommunication.Read())
                                {
                                    DataGridViewRow row = new DataGridViewRow();
                                    if (!rdrCommunication.IsDBNull(0)) row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrCommunication.GetString(0) });
                                    else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                                    if (!rdrCommunication.IsDBNull(1))
                                    {
                                        switch ((CommunicationType)rdrCommunication.GetByte(1))
                                        {
                                            case CommunicationType.IncomingCall:
                                                row.Cells.Add(new DataGridViewTextBoxCell { Value = CommunicationType.IncomingCall.ToString() });
                                                break;
                                            case CommunicationType.OutgoingCall:
                                                row.Cells.Add(new DataGridViewTextBoxCell { Value = CommunicationType.OutgoingCall.ToString() });
                                                break;
                                            case CommunicationType.IncommingFax:
                                                row.Cells.Add(new DataGridViewTextBoxCell { Value = CommunicationType.IncommingFax.ToString() });
                                                break;
                                            case CommunicationType.OutgoingFax:
                                                row.Cells.Add(new DataGridViewTextBoxCell { Value = CommunicationType.OutgoingFax.ToString() });
                                                break;
                                            case CommunicationType.IncomingEFax:
                                                row.Cells.Add(new DataGridViewTextBoxCell { Value = CommunicationType.IncomingEFax.ToString() });
                                                break;
                                            case CommunicationType.OutgoingEFax:
                                                row.Cells.Add(new DataGridViewTextBoxCell { Value = CommunicationType.OutgoingEFax.ToString() });
                                                break;
                                            case CommunicationType.EmailReceived:
                                                row.Cells.Add(new DataGridViewTextBoxCell { Value = CommunicationType.EmailReceived.ToString() });
                                                break;
                                            case CommunicationType.EmailSent:
                                                row.Cells.Add(new DataGridViewTextBoxCell { Value = CommunicationType.EmailSent.ToString() });
                                                break;
                                            case CommunicationType.LetterReceived:
                                                row.Cells.Add(new DataGridViewTextBoxCell { Value = CommunicationType.LetterReceived.ToString() });
                                                break;
                                            case CommunicationType.LetterMailed:
                                                row.Cells.Add(new DataGridViewTextBoxCell { Value = CommunicationType.LetterMailed.ToString() });
                                                break;
                                            case CommunicationType.Other:
                                                row.Cells.Add(new DataGridViewTextBoxCell { Value = CommunicationType.Other.ToString() });
                                                break;
                                        }
                                    }
                                    else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                                    if (!rdrCommunication.IsDBNull(2)) row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrCommunication.GetString(2) });
                                    else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                                    if (!rdrCommunication.IsDBNull(3)) row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrCommunication.GetString(3) });
                                    else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });

                                    gvCommunicationList.Rows.Add(row);

                                }
                            }
                            rdrCommunication.Close();
                            if (connRN.State != ConnectionState.Closed) connRN.Close();

                            List<TaskInfo> lstTaskInfo = new List<TaskInfo>();
                            gvTaskList.Rows.Clear();

                            String strSqlQueryForTaskForIllnessNo = "select [dbo].[tbl_task].[id], [dbo].[tbl_task].[Subject], [dbo].[tbl_task].[Comment], [dbo].[tbl_task].[Solution], " +
                                                                    "[dbo].[tbl_task].[CreateDate] " +
                                                                    "from [dbo].[tbl_task] " +
                                                                    "where [dbo].[tbl_task].[whoid] = @IndividualId and [dbo].[tbl_task].[whatid] = @IllnessNo";

                            SqlCommand cmdQueryForTaskForIllnessNo = new SqlCommand(strSqlQueryForTaskForIllnessNo, connRN);
                            cmdQueryForTaskForIllnessNo.CommandType = CommandType.Text;

                            cmdQueryForTaskForIllnessNo.Parameters.AddWithValue("@IndividualId", IndividualId);
                            cmdQueryForTaskForIllnessNo.Parameters.AddWithValue("@IllnessNo", SelectedIllnessNo);

                            if (connRN.State != ConnectionState.Closed)
                            {
                                connRN.Close();
                                connRN.Open();
                            }
                            else if (connRN.State == ConnectionState.Closed) connRN.Open();
                            SqlDataReader rdrTaskForIllnessNo = cmdQueryForTaskForIllnessNo.ExecuteReader();
                            if (rdrTaskForIllnessNo.HasRows)
                            {
                                while (rdrTaskForIllnessNo.Read())
                                {
                                    TaskInfo info = new TaskInfo();

                                    if (!rdrTaskForIllnessNo.IsDBNull(0)) info.TaskId = rdrTaskForIllnessNo.GetInt32(0);
                                    else info.TaskId = null;
                                    if (!rdrTaskForIllnessNo.IsDBNull(1)) info.Subject = rdrTaskForIllnessNo.GetString(1);
                                    else info.Subject = String.Empty;
                                    if (!rdrTaskForIllnessNo.IsDBNull(2)) info.Comment = rdrTaskForIllnessNo.GetString(2);
                                    else info.Comment = String.Empty;
                                    if (!rdrTaskForIllnessNo.IsDBNull(3)) info.Solution = rdrTaskForIllnessNo.GetString(3);
                                    else info.Solution = String.Empty;
                                    if (!rdrTaskForIllnessNo.IsDBNull(4)) info.CreateDate = rdrTaskForIllnessNo.GetDateTime(4);
                                    else info.CreateDate = null;

                                    lstTaskInfo.Add(info);
                                }
                            }
                            rdrTaskForIllnessNo.Close();
                            if (connRN.State != ConnectionState.Closed) connRN.Close();

                            //List<String> lstIncidentNo = new List<string>();
                            List<IncidentIdNo> lstIncidentIdNo = new List<IncidentIdNo>();

                            String strSqlQueryForIncidentNoForIllnessId = "select [dbo].[tbl_incident].[IncidentNo], [dbo].[tbl_incident].[Incident_id] from [dbo].[tbl_incident] " +
                                                                          "where [dbo].[tbl_incident].[Individual_id] = @IndividualId and " +
                                                                          "[dbo].[tbl_incident].[Case_id] = @CaseNo and " +
                                                                          "[dbo].[tbl_incident].[Illness_id] = @IllnessId";

                            SqlCommand cmdQueryForIncidentNoForIllnessId = new SqlCommand(strSqlQueryForIncidentNoForIllnessId, connRN);
                            cmdQueryForIncidentNoForIllnessId.CommandType = CommandType.Text;

                            cmdQueryForIncidentNoForIllnessId.Parameters.AddWithValue("@IndividualId", IndividualId);
                            cmdQueryForIncidentNoForIllnessId.Parameters.AddWithValue("@CaseNo", SelectedCaseNo);
                            cmdQueryForIncidentNoForIllnessId.Parameters.AddWithValue("@IllnessId", nSelectedIllnessId.Value);

                            if (connRN.State != ConnectionState.Closed)
                            {
                                connRN.Close();
                                connRN.Open();
                            }
                            else if (connRN.State == ConnectionState.Closed) connRN.Open();
                            SqlDataReader rdrIncidentNoForIllnessId = cmdQueryForIncidentNoForIllnessId.ExecuteReader();
                            if (rdrIncidentNoForIllnessId.HasRows)
                            {
                                while (rdrIncidentNoForIllnessId.Read())
                                {
                                    IncidentIdNo incd = new IncidentIdNo();

                                    if (!rdrIncidentNoForIllnessId.IsDBNull(0)) incd.IncidentNo = rdrIncidentNoForIllnessId.GetString(0);
                                    else incd.IncidentNo = String.Empty;
                                    if (!rdrIncidentNoForIllnessId.IsDBNull(1)) incd.IncidentId = rdrIncidentNoForIllnessId.GetInt32(1);
                                    else incd.IncidentId = null;

                                    lstIncidentIdNo.Add(incd);
                                }
                            }
                            rdrIncidentNoForIllnessId.Close();
                            if (connRN.State != ConnectionState.Closed) connRN.Close();

                            if (lstIncidentIdNo.Count > 0)
                            {
                                foreach (IncidentIdNo incd_no in lstIncidentIdNo)
                                {
                                    String strSqlQueryForTaskForIncidentNo = "select [dbo].[tbl_task].[id], [dbo].[tbl_task].[Subject], [dbo].[tbl_task].[Comment], [dbo].[tbl_task].[Solution], " +
                                                                             "[dbo].[tbl_task].[CreateDate] " +
                                                                             "from [dbo].[tbl_task] " +
                                                                             "where [dbo].[tbl_task].[whoid] = @IndividualId and " +
                                                                             "[dbo].[tbl_task].[whatid] = @IncidentNo";

                                    SqlCommand cmdQueryForTaskForIncidentNo = new SqlCommand(strSqlQueryForTaskForIncidentNo, connRN);
                                    cmdQueryForTaskForIncidentNo.CommandType = CommandType.Text;

                                    cmdQueryForTaskForIncidentNo.Parameters.AddWithValue("@IndividualId", IndividualId);
                                    cmdQueryForTaskForIncidentNo.Parameters.AddWithValue("@IncidentNo", incd_no.IncidentNo);

                                    if (connRN.State != ConnectionState.Closed)
                                    {
                                        connRN.Close();
                                        connRN.Open();
                                    }
                                    else if (connRN.State == ConnectionState.Closed) connRN.Open();
                                    SqlDataReader rdrTaskForIncidentNo = cmdQueryForTaskForIncidentNo.ExecuteReader();
                                    if (rdrTaskForIncidentNo.HasRows)
                                    {
                                        while (rdrTaskForIncidentNo.Read())
                                        {
                                            TaskInfo info = new TaskInfo();

                                            if (!rdrTaskForIncidentNo.IsDBNull(0)) info.TaskId = rdrTaskForIncidentNo.GetInt32(0);
                                            else info.TaskId = null;
                                            if (!rdrTaskForIncidentNo.IsDBNull(1)) info.Subject = rdrTaskForIncidentNo.GetString(1);
                                            else info.Subject = String.Empty;
                                            if (!rdrTaskForIncidentNo.IsDBNull(2)) info.Comment = rdrTaskForIncidentNo.GetString(2);
                                            else info.Comment = String.Empty;
                                            if (!rdrTaskForIncidentNo.IsDBNull(3)) info.Solution = rdrTaskForIncidentNo.GetString(3);
                                            else info.Solution = String.Empty;
                                            if (!rdrTaskForIncidentNo.IsDBNull(4)) info.CreateDate = rdrTaskForIncidentNo.GetDateTime(4);
                                            else info.CreateDate = null;

                                            lstTaskInfo.Add(info);
                                        }
                                    }
                                    rdrTaskForIncidentNo.Close();
                                    if (connRN.State != ConnectionState.Closed) connRN.Close();
                                }
                            }
                            else
                            {
                                comboIncidentNo.Text = String.Empty;
                                comboIncidentNo.Items.Clear();
                                comboIncidentNo.Enabled = false;
                            }

                            List<String> lstMedBillNo = new List<string>();

                            foreach (IncidentIdNo incd in lstIncidentIdNo)
                            {
                                String strSqlQueryForMedBillNo = "select [dbo].[tbl_medbill].[BillNo] from [dbo].[tbl_medbill] " +
                                                                 "where [dbo].[tbl_medbill].[Individual_Id] = @IndividualId and " +
                                                                 "[dbo].[tbl_medbill].[Case_Id] = @CaseNo and " +
                                                                 "[dbo].[tbl_medbill].[Illness_Id] = @IllnessId and " +
                                                                 "[dbo].[tbl_medbill].[Incident_Id] = @IncidentId";

                                SqlCommand cmdQueryForMedBillNo = new SqlCommand(strSqlQueryForMedBillNo, connRN);
                                cmdQueryForMedBillNo.CommandType = CommandType.Text;

                                cmdQueryForMedBillNo.Parameters.AddWithValue("@IndividualId", IndividualId);
                                cmdQueryForMedBillNo.Parameters.AddWithValue("@CaseNo", SelectedCaseNo);
                                cmdQueryForMedBillNo.Parameters.AddWithValue("@IllnessId", nSelectedIllnessId.ToString());
                                cmdQueryForMedBillNo.Parameters.AddWithValue("@IncidentId", incd.IncidentId.ToString());

                                if (connRN.State != ConnectionState.Closed)
                                {
                                    connRN.Close();
                                    connRN.Open();
                                }
                                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                                SqlDataReader rdrMedBillNo = cmdQueryForMedBillNo.ExecuteReader();
                                if (rdrMedBillNo.HasRows)
                                {
                                    while (rdrMedBillNo.Read())
                                    {
                                        String MedBillNo = String.Empty;
                                        if (!rdrMedBillNo.IsDBNull(0)) MedBillNo = rdrMedBillNo.GetString(0);
                                        lstMedBillNo.Add(MedBillNo);
                                    }
                                }
                                rdrMedBillNo.Close();
                                if (connRN.State != ConnectionState.Closed) connRN.Close();
                            }

                            foreach (String medbill_no in lstMedBillNo)
                            {
                                String strSqlQueryForTaskInfoForMedBillNo = "select [dbo].[tbl_task].[id], [dbo].[tbl_task].[Subject], [dbo].[tbl_task].[Comment], [dbo].[tbl_task].[Solution], " +
                                                                            "[dbo].[tbl_task].[CreateDate] " +
                                                                            "from [dbo].[tbl_task] " +
                                                                            "where [dbo].[tbl_task].[whoid] = @IndividualId and " +
                                                                            "[dbo].[tbl_task].[whatid] = @MedBillNo";

                                SqlCommand cmdQueryForTaskInfoForMedBillNo = new SqlCommand(strSqlQueryForTaskInfoForMedBillNo, connRN);
                                cmdQueryForTaskInfoForMedBillNo.CommandType = CommandType.Text;

                                cmdQueryForTaskInfoForMedBillNo.Parameters.AddWithValue("@IndividualId", IndividualId);
                                cmdQueryForTaskInfoForMedBillNo.Parameters.AddWithValue("@MedBillNo", medbill_no);

                                if (connRN.State != ConnectionState.Closed)
                                {
                                    connRN.Close();
                                    connRN.Open();
                                }
                                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                                SqlDataReader rdrTaskInfoForMedBillNo = cmdQueryForTaskInfoForMedBillNo.ExecuteReader();
                                if (rdrTaskInfoForMedBillNo.HasRows)
                                {
                                    while (rdrTaskInfoForMedBillNo.Read())
                                    {
                                        TaskInfo info = new TaskInfo();
                                        if (!rdrTaskInfoForMedBillNo.IsDBNull(0)) info.TaskId = rdrTaskInfoForMedBillNo.GetInt32(0);
                                        else info.TaskId = null;
                                        if (!rdrTaskInfoForMedBillNo.IsDBNull(1)) info.Subject = rdrTaskInfoForMedBillNo.GetString(1);
                                        else info.Subject = String.Empty;
                                        if (!rdrTaskInfoForMedBillNo.IsDBNull(2)) info.Comment = rdrTaskInfoForMedBillNo.GetString(2);
                                        else info.Comment = String.Empty;
                                        if (!rdrTaskInfoForMedBillNo.IsDBNull(3)) info.Solution = rdrTaskInfoForMedBillNo.GetString(3);
                                        else info.Solution = String.Empty;
                                        if (!rdrTaskInfoForMedBillNo.IsDBNull(4)) info.CreateDate = rdrTaskInfoForMedBillNo.GetDateTime(4);
                                        else info.CreateDate = null;

                                        lstTaskInfo.Add(info);

                                    }
                                }
                                rdrTaskInfoForMedBillNo.Close();
                                if (connRN.State != ConnectionState.Closed) connRN.Close();
                            }

                            List<String> lstSettlementNo = new List<string>();

                            foreach (String medbill_no in lstMedBillNo)
                            {
                                String strSqlQueryForSettlementNo = "select [dbo].[tbl_settlement].[Name] from [dbo].[tbl_settlement] " +
                                                                    "where [dbo].[tbl_settlement].[MedicalBillID] = @MedBillNo";

                                SqlCommand cmdQueryForSettlementNo = new SqlCommand(strSqlQueryForSettlementNo, connRN);
                                cmdQueryForSettlementNo.CommandType = CommandType.Text;

                                cmdQueryForSettlementNo.Parameters.AddWithValue("@MedBillNo", medbill_no);

                                if (connRN.State != ConnectionState.Closed)
                                {
                                    connRN.Close();
                                    connRN.Open();
                                }
                                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                                SqlDataReader rdrSettlementNo = cmdQueryForSettlementNo.ExecuteReader();
                                if (rdrSettlementNo.HasRows)
                                {
                                    while (rdrSettlementNo.Read())
                                    {
                                        String SettlementNo = String.Empty;
                                        if (!rdrSettlementNo.IsDBNull(0)) SettlementNo = rdrSettlementNo.GetString(0);
                                        else SettlementNo = String.Empty;

                                        lstSettlementNo.Add(SettlementNo);
                                    }
                                }
                                if (connRN.State != ConnectionState.Closed) connRN.Close();
                            }

                            foreach (String settlement_no in lstSettlementNo)
                            {
                                String strSqlQueryForTaskInfo = "select [dbo].[tbl_task].[id], [dbo].[tbl_task].[Subject], [dbo].[tbl_task].[Comment], [dbo].[tbl_task].[Solution], " +
                                                                "[dbo].[tbl_task].[CreateDate] " +
                                                                "from [dbo].[tbl_task] " +
                                                                "where [dbo].[tbl_task].[whoid] = @IndividualId and " +
                                                                "[dbo].[tbl_task].[whatid] = @SettlementNo";

                                SqlCommand cmdQueryForTaskInfo = new SqlCommand(strSqlQueryForTaskInfo, connRN);
                                cmdQueryForTaskInfo.CommandType = CommandType.Text;

                                cmdQueryForTaskInfo.Parameters.AddWithValue("@IndividualId", IndividualId);
                                cmdQueryForTaskInfo.Parameters.AddWithValue("@SettlementNo", settlement_no);

                                if (connRN.State != ConnectionState.Closed)
                                {
                                    connRN.Close();
                                    connRN.Open();
                                }
                                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                                SqlDataReader rdrTaskInfo = cmdQueryForTaskInfo.ExecuteReader();
                                if (rdrTaskInfo.HasRows)
                                {
                                    while (rdrTaskInfo.Read())
                                    {
                                        TaskInfo info = new TaskInfo();
                                        if (!rdrTaskInfo.IsDBNull(0)) info.TaskId = rdrTaskInfo.GetInt32(0);
                                        else info.TaskId = null;
                                        if (!rdrTaskInfo.IsDBNull(1)) info.Subject = rdrTaskInfo.GetString(1);
                                        else info.Subject = String.Empty;
                                        if (!rdrTaskInfo.IsDBNull(2)) info.Comment = rdrTaskInfo.GetString(2);
                                        else info.Comment = String.Empty;
                                        if (!rdrTaskInfo.IsDBNull(3)) info.Solution = rdrTaskInfo.GetString(3);
                                        else info.Solution = String.Empty;
                                        if (!rdrTaskInfo.IsDBNull(4)) info.CreateDate = rdrTaskInfo.GetDateTime(4);
                                        else info.CreateDate = null;

                                        lstTaskInfo.Add(info);
                                    }
                                }
                                rdrTaskInfo.Close();
                                if (connRN.State != ConnectionState.Closed) connRN.Close();
                            }

                            lstTaskInfo.Sort(delegate (TaskInfo info1, TaskInfo info2)
                            {
                                if (info1.CreateDate == null && info2.CreateDate == null) return 0;
                                else if (info1.CreateDate == null) return -1;
                                else if (info2.CreateDate == null) return 1;
                                else return (-1) * info1.CreateDate.Value.CompareTo(info2.CreateDate.Value);
                            });

                            foreach (TaskInfo info in lstTaskInfo)
                            {
                                DataGridViewRow row = new DataGridViewRow();

                                if (info.TaskId != null) row.Cells.Add(new DataGridViewTextBoxCell { Value = info.TaskId });
                                else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                                if (info.Subject != String.Empty) row.Cells.Add(new DataGridViewTextBoxCell { Value = info.Subject });
                                else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                                if (info.Comment != String.Empty) row.Cells.Add(new DataGridViewTextBoxCell { Value = info.Comment });
                                else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                                if (info.Solution != String.Empty) row.Cells.Add(new DataGridViewTextBoxCell { Value = info.Solution });
                                else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });

                                gvTaskList.Rows.Add(row);
                            }
                        }
                        else
                        {
                            comboIncidentNo.Text = String.Empty;
                            comboIncidentNo.Items.Clear();
                            comboIncidentNo.Enabled = false;

                            String strSqlQueryForCommunication = "select [dbo].[tbl_Communication].[CommunicationNo], [dbo].[tbl_Communication].[CommunicationType], " +
                                                                 "[dbo].[tbl_Communication].[Subject], [dbo].[tbl_Communication].[Body] " +
                                                                 "from [dbo].[tbl_Communication] " +
                                                                 "where [dbo].[tbl_Communication].[Individual_id] = @IndividualId and " +
                                                                 "[dbo].[tbl_Communication].[CaseNo] = @CaseNo " +
                                                                 "order by [dbo].[tbl_Communication].[CreateDate] desc";

                            SqlCommand cmdQueryForCommunication = new SqlCommand(strSqlQueryForCommunication, connRN);
                            cmdQueryForCommunication.CommandType = CommandType.Text;


                            cmdQueryForCommunication.Parameters.AddWithValue("@IndividualId", IndividualId);
                            cmdQueryForCommunication.Parameters.AddWithValue("@CaseNo", SelectedCaseNo);

                            if (connRN.State != ConnectionState.Closed)
                            {
                                connRN.Close();
                                connRN.Open();
                            }
                            else if (connRN.State == ConnectionState.Closed) connRN.Open();
                            SqlDataReader rdrCommunication = cmdQueryForCommunication.ExecuteReader();
                            if (rdrCommunication.HasRows)
                            {
                                while (rdrCommunication.Read())
                                {
                                    DataGridViewRow row = new DataGridViewRow();

                                    if (!rdrCommunication.IsDBNull(0)) row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrCommunication.GetString(0) });
                                    else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                                    if (!rdrCommunication.IsDBNull(1))
                                    {
                                        CommunicationType type = (CommunicationType)rdrCommunication.GetByte(1);
                                        row.Cells.Add(new DataGridViewTextBoxCell { Value = type.ToString() });
                                    }
                                    else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                                    if (!rdrCommunication.IsDBNull(2)) row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrCommunication.GetString(2) });
                                    else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                                    if (!rdrCommunication.IsDBNull(3)) row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrCommunication.GetString(3) });
                                    else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });

                                    gvCommunicationList.Rows.Add(row);
                                }
                            }
                            rdrCommunication.Close();
                            if (connRN.State != ConnectionState.Closed) connRN.Close();

                            List<TaskInfo> lstTaskInfo = new List<TaskInfo>();
                            String strSqlQueryForTaskInfoForCaseNo = "select [dbo].[tbl_task].[id], [dbo].[tbl_task].[Subject], [dbo].[tbl_task].[Comment], [dbo].[tbl_task].[Solution], " +
                                                                    "[dbo].[tbl_task].[CreateDate] " +
                                                                    "from [dbo].[tbl_task] " +
                                                                    "where [dbo].[tbl_task].[whoid] = @IndividualId and " +
                                                                    "[dbo].[tbl_task].[whatid] = @CaseNo";
                                                            

                            SqlCommand cmdQueryForTaskInfoForCaseNo = new SqlCommand(strSqlQueryForTaskInfoForCaseNo, connRN);
                            cmdQueryForTaskInfoForCaseNo.CommandType = CommandType.Text;

                            cmdQueryForTaskInfoForCaseNo.Parameters.AddWithValue("@IndividualId", IndividualId);
                            cmdQueryForTaskInfoForCaseNo.Parameters.AddWithValue("@CaseNo", SelectedCaseNo);

                            if (connRN.State != ConnectionState.Closed)
                            {
                                connRN.Close();
                                connRN.Open();
                            }
                            else if (connRN.State == ConnectionState.Closed) connRN.Open();
                            SqlDataReader rdrTaskInfoForCaseNo = cmdQueryForTaskInfoForCaseNo.ExecuteReader();
                            if (rdrTaskInfoForCaseNo.HasRows)
                            {
                                while (rdrTaskInfoForCaseNo.Read())
                                {
                                    TaskInfo info = new TaskInfo();

                                    if (!rdrTaskInfoForCaseNo.IsDBNull(0)) info.TaskId = rdrTaskInfoForCaseNo.GetInt32(0);
                                    else info.TaskId = null;
                                    if (!rdrTaskInfoForCaseNo.IsDBNull(1)) info.Subject = rdrTaskInfoForCaseNo.GetString(1);
                                    else info.Subject = String.Empty;
                                    if (!rdrTaskInfoForCaseNo.IsDBNull(2)) info.Comment = rdrTaskInfoForCaseNo.GetString(2);
                                    else info.Comment = String.Empty;
                                    if (!rdrTaskInfoForCaseNo.IsDBNull(3)) info.Solution = rdrTaskInfoForCaseNo.GetString(3);
                                    else info.Solution = String.Empty;
                                    if (!rdrTaskInfoForCaseNo.IsDBNull(4)) info.CreateDate = rdrTaskInfoForCaseNo.GetDateTime(4);
                                    else info.CreateDate = null;

                                    lstTaskInfo.Add(info);
                                }
                            }
                            if (connRN.State != ConnectionState.Closed) connRN.Close();
                            ////////////////////////////////////////////////////////////////////////////////////////////
                            /// All Illness for the Case No
                            /// 

                            List<String> lstIllnessNo = new List<string>();

                            String strSqlQueryForIllnessNoForCaseNo = "select [dbo].[tbl_illness].[IllnessNo] from [dbo].[tbl_illness] " +
                                                                      "where [dbo].[tbl_illness].[Individual_Id] = @IndividualId and " +
                                                                      "[dbo].[tbl_illness].[Case_Id] = @CaseNo";

                            SqlCommand cmdQueryForForIllnessNoForCaseNo = new SqlCommand(strSqlQueryForIllnessNoForCaseNo, connRN);
                            cmdQueryForForIllnessNoForCaseNo.CommandType = CommandType.Text;

                            cmdQueryForForIllnessNoForCaseNo.Parameters.AddWithValue("@IndividualId", IndividualId);
                            cmdQueryForForIllnessNoForCaseNo.Parameters.AddWithValue("@CaseNo", SelectedCaseNo);

                            if (connRN.State != ConnectionState.Closed)
                            {
                                connRN.Close();
                                connRN.Open();
                            }
                            else if (connRN.State == ConnectionState.Closed) connRN.Open();
                            SqlDataReader rdrIllnessNoForCaseNo = cmdQueryForForIllnessNoForCaseNo.ExecuteReader();
                            if (rdrIllnessNoForCaseNo.HasRows)
                            {
                                while (rdrIllnessNoForCaseNo.Read())
                                {
                                    String IllnessNo = String.Empty;

                                    if (!rdrIllnessNoForCaseNo.IsDBNull(0)) IllnessNo = rdrIllnessNoForCaseNo.GetString(0);
                                    else IllnessNo = String.Empty;

                                    lstIllnessNo.Add(IllnessNo);
                                }
                            }
                            rdrIllnessNoForCaseNo.Close();
                            if (connRN.State != ConnectionState.Closed) connRN.Close();

                            foreach (String illness_no in lstIllnessNo)
                            {

                                String strSqlQueryForTaskInfoForIllnessNo = "select [dbo].[tbl_task].[id], [dbo].[tbl_task].[Subjet], [dbo].[tbl_task].[Comment], [dbo].[tbl_task].[Solution] " +
                                                                            "from [dbo].[tbl_task] " +
                                                                            "where [dbo].[tbl_task].[whoid] = @IndividualId and " +
                                                                            "[dbo].[tbl_task].[whatid] = @IllnessNo";

                                SqlCommand cmdQueryForTaskInfoForIllnessNo = new SqlCommand(strSqlQueryForTaskInfoForIllnessNo, connRN);
                                cmdQueryForTaskInfoForIllnessNo.CommandType = CommandType.Text;

                                cmdQueryForTaskInfoForIllnessNo.Parameters.AddWithValue("@IndiviualId", IndividualId);
                                cmdQueryForTaskInfoForIllnessNo.Parameters.AddWithValue("@IllnessNo", illness_no);

                                if (connRN.State != ConnectionState.Closed)
                                {
                                    connRN.Close();
                                    connRN.Open();
                                }
                                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                                SqlDataReader rdrTaskInfoForIllnessNo = cmdQueryForTaskInfoForIllnessNo.ExecuteReader();
                                if (rdrTaskInfoForIllnessNo.HasRows)
                                {
                                    while (rdrTaskInfoForIllnessNo.Read())
                                    {
                                        TaskInfo info = new TaskInfo();

                                        if (!rdrTaskInfoForIllnessNo.IsDBNull(0)) info.TaskId = rdrTaskInfoForIllnessNo.GetInt32(0);
                                        else info.TaskId = null;
                                        if (!rdrTaskInfoForIllnessNo.IsDBNull(1)) info.Subject = rdrTaskInfoForIllnessNo.GetString(1);
                                        else info.Subject = String.Empty;
                                        if (!rdrTaskInfoForIllnessNo.IsDBNull(2)) info.Comment = rdrTaskInfoForIllnessNo.GetString(2);
                                        else info.Comment = String.Empty;
                                        if (!rdrTaskInfoForIllnessNo.IsDBNull(3)) info.Solution = rdrTaskInfoForIllnessNo.GetString(3);
                                        else info.Solution = String.Empty;

                                        lstTaskInfo.Add(info);
                                    }
                                }
                                rdrTaskInfoForIllnessNo.Close();
                                if (connRN.State != ConnectionState.Closed) connRN.Close();
                            }

                            List<int?> lstIllnessId = new List<int?>();

                            foreach (String illness_no in lstIllnessNo)
                            {
                                String strSqlQueryForIllnessId = "select [dbo].[tbl_illness].[Illness_Id] from [dbo].[tbl_illness] where [dbo].[tbl_illness].[IllnessNo] = @IllnessNo";

                                // 08/12/19 begin here
                            }

                            ////////////////////////////////////////////////////////////////////////////////////////////
                            ///
                            lstTaskInfo.Sort(delegate (TaskInfo info1, TaskInfo info2)
                            {
                                if (info1.CreateDate == null && info2.CreateDate == null) return 0;
                                else if (info1.CreateDate == null) return -1;
                                else if (info2.CreateDate == null) return 1;
                                else return (-1) * info1.CreateDate.Value.CompareTo(info2.CreateDate.Value);
                            });

                            foreach (TaskInfo info in lstTaskInfo)
                            {
                                DataGridViewRow row = new DataGridViewRow();

                                if (info.TaskId != null) row.Cells.Add(new DataGridViewTextBoxCell { Value = info.TaskId });
                                else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                                if (info.Subject != String.Empty) row.Cells.Add(new DataGridViewTextBoxCell { Value = info.Subject });
                                else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                                if (info.Comment != String.Empty) row.Cells.Add(new DataGridViewTextBoxCell { Value = info.Comment });
                                else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                                if (info.Solution != String.Empty) row.Cells.Add(new DataGridViewTextBoxCell { Value = info.Solution });
                                else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });

                                gvTaskList.Rows.Add(row);
                            }
                        }
                    }
                    //String IllnessNo = String.Empty;

                    //if (comboIllnessNo.SelectedItem != null)
                    //{
                    //    if (comboIllnessNo.SelectedItem.ToString() != "All")
                    //    {
                    //        IllnessNo = comboIllnessNo.SelectedItem.ToString();

                    //        int? nSelectedIllnessId = null;

                    //        String strSqlQueryForIllnessId = "select [dbo].[tbl_illness].[Illness_Id] from [dbo].[tbl_illness] where [dbo].[tbl_illness].[IllnessNo] = @IllnessNo";

                    //        SqlCommand cmdQueryForIllnessId = new SqlCommand(strSqlQueryForIllnessId, connRN);
                    //        cmdQueryForIllnessId.CommandType = CommandType.Text;

                    //        cmdQueryForIllnessId.Parameters.AddWithValue("@IllnessNo", IllnessNo);

                    //        if (connRN.State != ConnectionState.Closed)
                    //        {
                    //            connRN.Close();
                    //            connRN.Open();
                    //        }
                    //        else if (connRN.State == ConnectionState.Closed) connRN.Open();
                    //        Object objIllnessId = cmdQueryForIllnessId.ExecuteScalar();
                    //        if (connRN.State != ConnectionState.Closed) connRN.Close();

                    //        if (objIllnessId != null) nSelectedIllnessId = Int32.Parse(objIllnessId.ToString());

                    //        //comboIncidentNo.Items.Remove("All");

                    //        comboIncidentNo.Items.Clear();
                    //        //comboIncidentNo.Items.Add("All INCD for " + IllnessNo);

                    //        comboIncidentNo.SelectedIndexChanged -= comboIncidentNo_SelectedIndexChanged;
                    //        comboIncidentNo.SelectedIndex = 0;
                    //        comboIncidentNo.SelectedIndexChanged += comboIncidentNo_SelectedIndexChanged;


                    //        String strSqlQueryForIncidentNo = "select [dbo].[tbl_incident].[IncidentNo] from [dbo].[tbl_incident] " +
                    //                                          "where [dbo].[tbl_incident].[Individual_id] = @IndividualId and " +
                    //                                          "[dbo].[tbl_incident].[Case_id] = @CaseNo and " +
                    //                                          "[dbo].[tbl_incident].[Illness_id] = @IllnessId";

                    //        SqlCommand cmdQueryForIncidentNo = new SqlCommand(strSqlQueryForIncidentNo, connRN);
                    //        cmdQueryForIncidentNo.CommandType = CommandType.Text;

                    //        cmdQueryForIncidentNo.Parameters.AddWithValue("@IndividualId", IndividualId);
                    //        cmdQueryForIncidentNo.Parameters.AddWithValue("@CaseNo", SelectedCaseNo);
                    //        cmdQueryForIncidentNo.Parameters.AddWithValue("@IllnessId", nSelectedIllnessId.Value);

                    //        if (connRN.State != ConnectionState.Closed)
                    //        {
                    //            connRN.Close();
                    //            connRN.Open();
                    //        }
                    //        else if (connRN.State == ConnectionState.Closed) connRN.Open();
                    //        SqlDataReader rdrIncidentNo = cmdQueryForIncidentNo.ExecuteReader();
                    //        if (rdrIncidentNo.HasRows)
                    //        {
                    //            while (rdrIncidentNo.Read())
                    //            {
                    //                if (!rdrIncidentNo.IsDBNull(0)) comboIncidentNo.Items.Add(rdrIncidentNo.GetString(0));
                    //            }
                    //        }
                    //        if (connRN.State != ConnectionState.Closed) connRN.Close();
                    //    }
                    //}
                }
                else
                {
                    comboIllnessNo.Text = String.Empty;
                    comboIllnessNo.Items.Clear();
                    comboIllnessNo.Enabled = false;
                }
            }
        }

        private void comboIncidentNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            String SelectedCaseNo = String.Empty;
            if (comboCaseNo.SelectedItem != null)
            {
                if (comboCaseNo.SelectedItem.ToString() != "All")
                {
                    SelectedCaseNo = comboCaseNo.SelectedItem.ToString();
                }
            }
            else
            {
                MessageBox.Show("You have not selected Case No.", "Alert");
                return;
            }

            String SelectedIllnessNo = String.Empty;
            if (comboIllnessNo.SelectedItem != null)
            {
                if (comboIllnessNo.SelectedItem.ToString() != "All")
                {
                    SelectedIllnessNo = comboIllnessNo.SelectedItem.ToString();
                }
            }
            else
            {
                MessageBox.Show("You have not selected Illness No.", "Alert");
                return;
            }

            int? nSelectedIllnessId = null;
            String strSqlQueryForIllnessId = "select [dbo].[tbl_illness].[Illness_Id] from [dbo].[tbl_illness] where [dbo].[tbl_illness].[IllnessNo] = @IllnessNo";

            SqlCommand cmdQueryForIllnessId = new SqlCommand(strSqlQueryForIllnessId, connRN);
            cmdQueryForIllnessId.CommandType = CommandType.Text;

            cmdQueryForIllnessId.Parameters.AddWithValue("@IllnessNo", SelectedIllnessNo);
            if (connRN.State != ConnectionState.Closed)
            {
                connRN.Close();
                connRN.Open();
            }
            else if (connRN.State == ConnectionState.Closed) connRN.Open();
            Object objIllnessId = cmdQueryForIllnessId.ExecuteScalar();
            if (connRN.State != ConnectionState.Closed) connRN.Close();

            if (objIllnessId != null) nSelectedIllnessId = Int32.Parse(objIllnessId.ToString());

            String SelectedIncidentNo = String.Empty;
            if (comboIncidentNo.SelectedItem != null)
            {
                if (comboIncidentNo.SelectedItem.ToString() != "All")
                {
                    SelectedIncidentNo = comboIncidentNo.SelectedItem.ToString();
                }
                else SelectedIncidentNo = "All";
            }
            else
            {
                MessageBox.Show("You have not selected Incident No.", "Alert");
                return;
            }

            if (SelectedIncidentNo != "All")
            {

                String strSqlQueryForIncidentId = "select [dbo].[tbl_incident].[Incident_id] from [dbo].[tbl_incident] where [dbo].[tbl_incident].[IncidentNo] = @IncidentNo";

                SqlCommand cmdQueryForIncidentId = new SqlCommand(strSqlQueryForIncidentId, connRN);
                cmdQueryForIncidentId.CommandType = CommandType.Text;

                cmdQueryForIncidentId.Parameters.AddWithValue("@IncidentNo", SelectedIncidentNo);

                if (connRN.State != ConnectionState.Closed)
                {
                    connRN.Close();
                    connRN.Open();
                }
                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                Object objIncidentId = cmdQueryForIncidentId.ExecuteScalar();
                if (connRN.State != ConnectionState.Closed) connRN.Close();

                int? nSelectedIncidentId = null;
                if (objIncidentId != null) nSelectedIncidentId = Int32.Parse(objIncidentId.ToString());

                gvCommunicationList.Rows.Clear();

                String strSqlQueryForCommunication = "select [dbo].[tbl_Communication].[CommunicationNo], [dbo].[tbl_Communication].[CommunicationType], " +
                                                     "[dbo].[tbl_Communication].[Subject], [dbo].[tbl_Communication].[Body] " +
                                                     "from [dbo].[tbl_Communication] " +
                                                     "where [dbo].[tbl_Communication].[Individual_Id] = @IndividualId and " +
                                                     "[dbo].[tbl_Communication].[CaseNo] = @SelectedCaseNo and " +
                                                     "[dbo].[tbl_Communication].[IllnessNo] = @SelectedIllnessNo and " +
                                                     "[dbo].[tbl_Communication].[IncidentNo] = @SelectedIncidentNo " +
                                                     "order by [dbo].[tbl_Communication].[CreateDate] desc";

                SqlCommand cmdQueryForCommunication = new SqlCommand(strSqlQueryForCommunication, connRN);
                cmdQueryForCommunication.CommandType = CommandType.Text;

                cmdQueryForCommunication.Parameters.AddWithValue("@IndividualId", IndividualId);
                cmdQueryForCommunication.Parameters.AddWithValue("@SelectedCaseNo", SelectedCaseNo);
                cmdQueryForCommunication.Parameters.AddWithValue("@SelectedIllnessNo", SelectedIllnessNo);
                cmdQueryForCommunication.Parameters.AddWithValue("@SelectedIncidentNo", SelectedIncidentNo);

                if (connRN.State != ConnectionState.Closed)
                {
                    connRN.Close();
                    connRN.Open();
                }
                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                SqlDataReader rdrCommunication = cmdQueryForCommunication.ExecuteReader();
                if (rdrCommunication.HasRows)
                {
                    while (rdrCommunication.Read())
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        if (!rdrCommunication.IsDBNull(0)) row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrCommunication.GetString(0) });
                        else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                        if (!rdrCommunication.IsDBNull(1))
                        {
                            switch ((CommunicationType)rdrCommunication.GetByte(1))
                            {
                                case CommunicationType.IncomingCall:
                                    row.Cells.Add(new DataGridViewTextBoxCell { Value = CommunicationType.IncomingCall.ToString() });
                                    break;
                                case CommunicationType.OutgoingCall:
                                    row.Cells.Add(new DataGridViewTextBoxCell { Value = CommunicationType.OutgoingCall.ToString() });
                                    break;
                                case CommunicationType.IncommingFax:
                                    row.Cells.Add(new DataGridViewTextBoxCell { Value = CommunicationType.IncommingFax.ToString() });
                                    break;
                                case CommunicationType.OutgoingFax:
                                    row.Cells.Add(new DataGridViewTextBoxCell { Value = CommunicationType.OutgoingFax.ToString() });
                                    break;
                                case CommunicationType.IncomingEFax:
                                    row.Cells.Add(new DataGridViewTextBoxCell { Value = CommunicationType.IncomingEFax.ToString() });
                                    break;
                                case CommunicationType.OutgoingEFax:
                                    row.Cells.Add(new DataGridViewTextBoxCell { Value = CommunicationType.OutgoingEFax.ToString() });
                                    break;
                                case CommunicationType.EmailReceived:
                                    row.Cells.Add(new DataGridViewTextBoxCell { Value = CommunicationType.EmailReceived.ToString() });
                                    break;
                                case CommunicationType.EmailSent:
                                    row.Cells.Add(new DataGridViewTextBoxCell { Value = CommunicationType.EmailSent.ToString() });
                                    break;
                                case CommunicationType.LetterReceived:
                                    row.Cells.Add(new DataGridViewTextBoxCell { Value = CommunicationType.LetterReceived.ToString() });
                                    break;
                                case CommunicationType.LetterMailed:
                                    row.Cells.Add(new DataGridViewTextBoxCell { Value = CommunicationType.LetterMailed.ToString() });
                                    break;
                                case CommunicationType.Other:
                                    row.Cells.Add(new DataGridViewTextBoxCell { Value = CommunicationType.Other.ToString() });
                                    break;
                            }
                        }
                        else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                        if (!rdrCommunication.IsDBNull(2)) row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrCommunication.GetString(2) });
                        else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                        if (!rdrCommunication.IsDBNull(3)) row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrCommunication.GetString(3) });
                        else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });

                        gvCommunicationList.Rows.Add(row);

                    }
                }
                rdrCommunication.Close();
                if (connRN.State != ConnectionState.Closed) connRN.Close();

                List<TaskInfo> lstTaskInfo = new List<TaskInfo>();

                String strSqlQueryForTaskInfoForIncidentNo = "select [dbo].[tbl_task].[id], [dbo].[tbl_task].[Subject], [dbo].[tbl_task].[Comment], [dbo].[tbl_task].[Solution], " +
                                                             "[dbo].[tbl_task].[CreateDate] " +
                                                             "from [dbo].[tbl_task] " +
                                                             "where [dbo].[tbl_task].[whoid] = @IndividualId and " +
                                                             "[dbo].[tbl_task].[whatid] = @IncidentNo";

                SqlCommand cmdQueryForTaskInfoForIncidentNo = new SqlCommand(strSqlQueryForTaskInfoForIncidentNo, connRN);
                cmdQueryForTaskInfoForIncidentNo.CommandType = CommandType.Text;

                cmdQueryForTaskInfoForIncidentNo.Parameters.AddWithValue("@IndividualId", IndividualId);
                cmdQueryForTaskInfoForIncidentNo.Parameters.AddWithValue("@IncidentNo", SelectedIncidentNo);

                if (connRN.State != ConnectionState.Closed)
                {
                    connRN.Close();
                    connRN.Open();
                }
                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                SqlDataReader rdrTaskInfoForIncidentNo = cmdQueryForTaskInfoForIncidentNo.ExecuteReader();
                if (rdrTaskInfoForIncidentNo.HasRows)
                {
                    while (rdrTaskInfoForIncidentNo.Read())
                    {
                        TaskInfo info = new TaskInfo();

                        if (!rdrTaskInfoForIncidentNo.IsDBNull(0)) info.TaskId = rdrTaskInfoForIncidentNo.GetInt32(0);
                        else info.TaskId = null;
                        if (!rdrTaskInfoForIncidentNo.IsDBNull(1)) info.Subject = rdrTaskInfoForIncidentNo.GetString(1);
                        else info.Subject = String.Empty;
                        if (!rdrTaskInfoForIncidentNo.IsDBNull(2)) info.Comment = rdrTaskInfoForIncidentNo.GetString(2);
                        else info.Comment = String.Empty;
                        if (!rdrTaskInfoForIncidentNo.IsDBNull(3)) info.Solution = rdrTaskInfoForIncidentNo.GetString(3);
                        else info.Solution = String.Empty;
                        if (!rdrTaskInfoForIncidentNo.IsDBNull(4)) info.CreateDate = rdrTaskInfoForIncidentNo.GetDateTime(4);
                        else info.CreateDate = null;

                        lstTaskInfo.Add(info);
                    }
                }
                rdrTaskInfoForIncidentNo.Close();
                if (connRN.State != ConnectionState.Closed) connRN.Close();

                List<String> lstMedBillNo = new List<string>();
                String strSqlQueryForMedBillNo = "select [dbo].[tbl_medbill].[BillNo] from [dbo].[tbl_medbill] " +
                                                 "where [dbo].[tbl_medbill].[Individual_Id] = @IndividualId and " +
                                                 "[dbo].[tbl_medbill].[Case_Id] = @CaseNo and " +
                                                 "[dbo].[tbl_medbill].[Illness_Id] = @IllnessId and " +
                                                 "[dbo].[tbl_medbill].[Incident_Id] = @IncidentId";

                SqlCommand cmdQueryForMedBillNo = new SqlCommand(strSqlQueryForMedBillNo, connRN);
                cmdQueryForMedBillNo.CommandType = CommandType.Text;

                cmdQueryForMedBillNo.Parameters.AddWithValue("@IndividualId", IndividualId);
                cmdQueryForMedBillNo.Parameters.AddWithValue("@CaseNo", SelectedCaseNo);
                cmdQueryForMedBillNo.Parameters.AddWithValue("@IllnessId", nSelectedIllnessId.Value.ToString());
                cmdQueryForMedBillNo.Parameters.AddWithValue("@IncidentId", nSelectedIncidentId.Value.ToString());

                if (connRN.State != ConnectionState.Closed)
                {
                    connRN.Close();
                    connRN.Open();
                }
                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                SqlDataReader rdrMedBillNo = cmdQueryForMedBillNo.ExecuteReader();
                if (rdrMedBillNo.HasRows)
                {
                    while (rdrMedBillNo.Read())
                    {
                        String MedBillNo = String.Empty;
                        if (!rdrMedBillNo.IsDBNull(0)) MedBillNo = rdrMedBillNo.GetString(0);
                        else MedBillNo = String.Empty;

                        lstMedBillNo.Add(MedBillNo);
                    }
                }
                if (connRN.State != ConnectionState.Closed) connRN.Close();

                foreach (String medbill_no in lstMedBillNo)
                {
                    String strSqlQueryForTaskInfoForMedBillNo = "select [dbo].[tbl_task].[id], [dbo].[tbl_task].[Subject], [dbo].[tbl_task].[Comment], [dbo].[tbl_task].[Solution], " +
                                                                "[dbo].[tbl_task].[CreateDate] " +
                                                                "from [dbo].[tbl_task] " +
                                                                "where [dbo].[tbl_task].[whoid] = @IndividualId and [dbo].[tbl_task].[whatid] = @MedBillNo";

                    SqlCommand cmdQueryForTaskInfoForMedBillNo = new SqlCommand(strSqlQueryForTaskInfoForMedBillNo, connRN);
                    cmdQueryForTaskInfoForMedBillNo.CommandType = CommandType.Text;

                    cmdQueryForTaskInfoForMedBillNo.Parameters.AddWithValue("@IndividualId", IndividualId);
                    cmdQueryForTaskInfoForMedBillNo.Parameters.AddWithValue("@MedBillNo", medbill_no);

                    if (connRN.State != ConnectionState.Closed)
                    {
                        connRN.Close();
                        connRN.Open();
                    }
                    else if (connRN.State == ConnectionState.Closed) connRN.Open();
                    SqlDataReader rdrTaskInfoForMedBillNo = cmdQueryForTaskInfoForMedBillNo.ExecuteReader();
                    if (rdrTaskInfoForMedBillNo.HasRows)
                    {
                        while (rdrTaskInfoForMedBillNo.Read())
                        {
                            TaskInfo info = new TaskInfo();

                            if (!rdrTaskInfoForMedBillNo.IsDBNull(0)) info.TaskId = rdrTaskInfoForMedBillNo.GetInt32(0);
                            else info.TaskId = null;
                            if (!rdrTaskInfoForMedBillNo.IsDBNull(1)) info.Subject = rdrTaskInfoForMedBillNo.GetString(1);
                            else info.Subject = String.Empty;
                            if (!rdrTaskInfoForMedBillNo.IsDBNull(2)) info.Comment = rdrTaskInfoForMedBillNo.GetString(2);
                            else info.Comment = String.Empty;
                            if (!rdrTaskInfoForMedBillNo.IsDBNull(3)) info.Solution = rdrTaskInfoForMedBillNo.GetString(3);
                            else info.Solution = String.Empty;
                            if (!rdrTaskInfoForMedBillNo.IsDBNull(4)) info.CreateDate = rdrTaskInfoForMedBillNo.GetDateTime(4);
                            else info.CreateDate = null;

                            lstTaskInfo.Add(info);
                        }
                    }
                    rdrTaskInfoForMedBillNo.Close();
                    if (connRN.State != ConnectionState.Closed) connRN.Close();
                }

                List<String> lstSettlements = new List<string>();

                foreach (String medbill_no in lstMedBillNo)
                {
                    String strSqlQueryForSettlementNo = "select [dbo].[tbl_settlement].[Name] from [dbo].[tbl_settlement] where [dbo].[tbl_settlement].[MedicalBillID] = @MedBillNo";

                    SqlCommand cmdQueryForSettlementNo = new SqlCommand(strSqlQueryForSettlementNo, connRN);
                    cmdQueryForSettlementNo.CommandType = CommandType.Text;

                    cmdQueryForSettlementNo.Parameters.AddWithValue("@MedBillNo", medbill_no);

                    if (connRN.State != ConnectionState.Closed)
                    {
                        connRN.Close();
                        connRN.Open();
                    }
                    else if (connRN.State == ConnectionState.Closed) connRN.Open();
                    SqlDataReader rdrSettlementNo = cmdQueryForSettlementNo.ExecuteReader();
                    if (rdrSettlementNo.HasRows)
                    {
                        while (rdrSettlementNo.Read())
                        {
                            String settlement_no = String.Empty;
                            if (!rdrSettlementNo.IsDBNull(0)) settlement_no = rdrSettlementNo.GetString(0);
                            else settlement_no = String.Empty;

                            lstSettlements.Add(settlement_no);
                        }
                    }
                    rdrSettlementNo.Close();
                    if (connRN.State != ConnectionState.Closed) connRN.Close();
                }

                foreach (String settlement_no in lstSettlements)
                {
                    String strSqlQueryForTaskInfoForSettlementNo = "select [dbo].[tbl_task].[id], [dbo].[tbl_task].[Subject], [dbo].[tbl_task].[Comment], [dbo].[tbl_task].[Solution], " +
                                                                   "[dbo].[tbl_task].[CreateDate] " +
                                                                   "from [dbo].[tbl_task] " +
                                                                   "where [dbo].[tbl_task].[whoid] = @IndividualId and " +
                                                                   "[dbo].[tbl_task].[whatid] = @SettlementNo";

                    SqlCommand cmdQueryForTaskInfoForSettlementNo = new SqlCommand(strSqlQueryForTaskInfoForSettlementNo, connRN);
                    cmdQueryForTaskInfoForSettlementNo.CommandType = CommandType.Text;

                    cmdQueryForTaskInfoForSettlementNo.Parameters.AddWithValue("@IndividualId", IndividualId);
                    cmdQueryForTaskInfoForSettlementNo.Parameters.AddWithValue("@SettlementNo", settlement_no);

                    if (connRN.State != ConnectionState.Closed)
                    {
                        connRN.Close();
                        connRN.Open();
                    }
                    else if (connRN.State == ConnectionState.Closed) connRN.Open();
                    SqlDataReader rdrTaskInfoForSettlementNo = cmdQueryForTaskInfoForSettlementNo.ExecuteReader();
                    if (rdrTaskInfoForSettlementNo.HasRows)
                    {
                        while (rdrTaskInfoForSettlementNo.Read())
                        {
                            TaskInfo info = new TaskInfo();

                            if (!rdrTaskInfoForSettlementNo.IsDBNull(0)) info.TaskId = rdrTaskInfoForSettlementNo.GetInt32(0);
                            else info.TaskId = null;
                            if (!rdrTaskInfoForSettlementNo.IsDBNull(1)) info.Subject = rdrTaskInfoForSettlementNo.GetString(1);
                            else info.Subject = String.Empty;
                            if (!rdrTaskInfoForSettlementNo.IsDBNull(2)) info.Comment = rdrTaskInfoForSettlementNo.GetString(2);
                            else info.Comment = String.Empty;
                            if (!rdrTaskInfoForSettlementNo.IsDBNull(3)) info.Solution = rdrTaskInfoForSettlementNo.GetString(3);
                            else info.Solution = String.Empty;
                            if (!rdrTaskInfoForSettlementNo.IsDBNull(4)) info.CreateDate = rdrTaskInfoForSettlementNo.GetDateTime(4);
                            else info.CreateDate = null;

                            lstTaskInfo.Add(info);
                        }
                    }

                    if (connRN.State != ConnectionState.Closed) connRN.Close();
                }

                lstTaskInfo.Sort(delegate (TaskInfo info1, TaskInfo info2)
                {
                    if (info1.CreateDate == null && info2.CreateDate == null) return 0;
                    else if (info1.CreateDate == null) return -1;
                    else if (info2.CreateDate == null) return 1;
                    else return (-1) * info1.CreateDate.Value.CompareTo(info2.CreateDate.Value);
                });

                foreach (TaskInfo info in lstTaskInfo)
                {
                    DataGridViewRow row = new DataGridViewRow();

                    if (info.TaskId != null) row.Cells.Add(new DataGridViewTextBoxCell { Value = info.TaskId });
                    else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                    if (info.Subject != String.Empty) row.Cells.Add(new DataGridViewTextBoxCell { Value = info.Subject });
                    else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                    if (info.Comment != String.Empty) row.Cells.Add(new DataGridViewTextBoxCell { Value = info.Comment });
                    else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                    if (info.Solution != String.Empty) row.Cells.Add(new DataGridViewTextBoxCell { Value = info.Solution });
                    else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                    if (info.CreateDate != null) row.Cells.Add(new DataGridViewTextBoxCell { Value = info.CreateDate });
                    else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });

                    gvTaskList.Rows.Add(row);
                }
            }
            else
            {
                List<CommunicationContentInfo> lstCommunicationInfo = new List<CommunicationContentInfo>();

                String strSqlQueryForCommunicationLog = "select [dbo].[tbl_Communication].[CommunicationNo], [dbo].[tbl_Communication].[CommunicationType], " +
                                                        "[dbo].[tbl_Communication].[Subject], [dbo].[tbl_Communication].[Body], [dbo].[tbl_Communication].[CreateDate] " +
                                                        "from [dbo].[tbl_Communication] " +
                                                        "where[dbo].[tbl_Communication].[Individual_Id] = @IndividualId and " +
                                                        "[dbo].[tbl_Communication].[CaseNo] = @SelectedCaseNo and " +
                                                        "[dbo].[tbl_Communication].[IllnessNo] = @SelectedIllnessNo";

                SqlCommand cmdQueryForCommunicationLog = new SqlCommand(strSqlQueryForCommunicationLog, connRN);
                cmdQueryForCommunicationLog.CommandType = CommandType.Text;

                cmdQueryForCommunicationLog.Parameters.AddWithValue("@IndividualId", IndividualId);
                cmdQueryForCommunicationLog.Parameters.AddWithValue("@SelectedCaseNo", SelectedCaseNo);
                cmdQueryForCommunicationLog.Parameters.AddWithValue("@SelectedIllnessNo", SelectedIllnessNo);

                if (connRN.State != ConnectionState.Closed)
                {
                    connRN.Close();
                    connRN.Open();
                }
                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                SqlDataReader rdrCommunicationLog = cmdQueryForCommunicationLog.ExecuteReader();
                if (rdrCommunicationLog.HasRows)
                {
                    while (rdrCommunicationLog.Read())
                    {
                        CommunicationContentInfo info = new CommunicationContentInfo();

                        if (!rdrCommunicationLog.IsDBNull(0)) info.CommunicationNo = rdrCommunicationLog.GetString(0);
                        else info.CommunicationNo = String.Empty;
                        if (!rdrCommunicationLog.IsDBNull(1)) info.CommType = (CommunicationType)rdrCommunicationLog.GetByte(1);
                        else info.CommType = null;
                        if (!rdrCommunicationLog.IsDBNull(2)) info.Subject = rdrCommunicationLog.GetString(2);
                        else info.Subject = String.Empty;
                        if (!rdrCommunicationLog.IsDBNull(3)) info.Body = rdrCommunicationLog.GetString(3);
                        else info.Body = String.Empty;
                        if (!rdrCommunicationLog.IsDBNull(4)) info.CreateDate = rdrCommunicationLog.GetDateTime(4);
                        else info.CreateDate = null;

                        lstCommunicationInfo.Add(info);
                    }
                }
                rdrCommunicationLog.Close();
                if (connRN.State != ConnectionState.Closed) connRN.Close();
                

                lstCommunicationInfo.Sort(delegate (CommunicationContentInfo info1, CommunicationContentInfo info2)
                {
                    if (info1.CreateDate == null && info2.CreateDate == null) return 0;
                    else if (info1.CreateDate == null) return -1;
                    else if (info2.CreateDate == null) return 1;
                    else return (-1) * info1.CreateDate.Value.CompareTo(info2.CreateDate.Value);
                });

                gvCommunicationList.Rows.Clear();

                foreach (CommunicationContentInfo info in lstCommunicationInfo)
                {
                    DataGridViewRow row = new DataGridViewRow();

                    if (info.CommunicationNo != String.Empty) row.Cells.Add(new DataGridViewTextBoxCell { Value = info.CommunicationNo });
                    else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                    if (info.CommType != null)
                    {
                        row.Cells.Add(new DataGridViewTextBoxCell { Value = info.CommType.Value.ToString() });
                    }
                    else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                    if (info.Subject != String.Empty) row.Cells.Add(new DataGridViewTextBoxCell { Value = info.Subject });
                    else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                    if (info.Body != String.Empty) row.Cells.Add(new DataGridViewTextBoxCell { Value = info.Body });
                    else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });

                    gvCommunicationList.Rows.Add(row);
                }
                // 08/09/19 - begin here to code task list for all incident case with no incident

                List<TaskInfo> lstTaskInfo = new List<TaskInfo>();
                String strSqlQueryForTaskInfoForIllnessNo = "select [dbo].[tbl_task].[id], [dbo].[tbl_task].[Subject], [dbo].[tbl_task].[Comment], [dbo].[tbl_task].[Solution], " +
                                                            "[dbo].[tbl_task].[CreateDate] " +
                                                            "from [dbo].[tbl_task] " +
                                                            "where [dbo].[tbl_task].[whoid] = @IndividualId and " +
                                                            "[dbo].[tbl_task].[whatid] = @IllnessNo";

                SqlCommand cmdQueryForTaskInfoForIllnessNo = new SqlCommand(strSqlQueryForTaskInfoForIllnessNo, connRN);
                cmdQueryForTaskInfoForIllnessNo.CommandType = CommandType.Text;

                cmdQueryForTaskInfoForIllnessNo.Parameters.AddWithValue("@IndividualId", IndividualId);
                cmdQueryForTaskInfoForIllnessNo.Parameters.AddWithValue("@IllnessNo", SelectedIllnessNo);

                if (connRN.State != ConnectionState.Closed)
                {
                    connRN.Close();
                    connRN.Open();
                }
                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                SqlDataReader rdrTaskInfoForIllnessNo = cmdQueryForTaskInfoForIllnessNo.ExecuteReader();
                if (rdrTaskInfoForIllnessNo.HasRows)
                {
                    while (rdrTaskInfoForIllnessNo.Read())
                    {
                        TaskInfo info = new TaskInfo();

                        if (!rdrTaskInfoForIllnessNo.IsDBNull(0)) info.TaskId = rdrTaskInfoForIllnessNo.GetInt32(0);
                        else info.TaskId = null;
                        if (!rdrTaskInfoForIllnessNo.IsDBNull(1)) info.Subject = rdrTaskInfoForIllnessNo.GetString(1);
                        else info.Subject = String.Empty;
                        if (!rdrTaskInfoForIllnessNo.IsDBNull(2)) info.Comment = rdrTaskInfoForIllnessNo.GetString(2);
                        else info.Comment = String.Empty;
                        if (!rdrTaskInfoForIllnessNo.IsDBNull(3)) info.Solution = rdrTaskInfoForIllnessNo.GetString(3);
                        else info.Solution = String.Empty;
                        if (!rdrTaskInfoForIllnessNo.IsDBNull(4)) info.CreateDate = rdrTaskInfoForIllnessNo.GetDateTime(4);
                        else info.CreateDate = null;

                        lstTaskInfo.Add(info);
                    }
                }
                rdrTaskInfoForIllnessNo.Close();
                if (connRN.State != ConnectionState.Closed) connRN.Close();

                List<String> lstIncidentNo = new List<string>();

                String strSqlQueryForIncidentNoForIllnessId = "select [dbo].[tbl_incident].[IncidentNo] from [dbo].[tbl_incident] " +
                                                              "where [dbo].[tbl_incident].[Individual_id] = @IndividualId and " +
                                                              "[dbo].[tbl_incident].[Case_id] = @CaseNo and " +
                                                              "[dbo].[tbl_incident].[Illness_id] = @IllnessId";

                SqlCommand cmdQueryForIncidentNoForIllnessId = new SqlCommand(strSqlQueryForIncidentNoForIllnessId, connRN);
                cmdQueryForIncidentNoForIllnessId.CommandType = CommandType.Text;

                cmdQueryForIncidentNoForIllnessId.Parameters.AddWithValue("@IndividualId", IndividualId);
                cmdQueryForIncidentNoForIllnessId.Parameters.AddWithValue("@CaseNo", SelectedCaseNo);
                cmdQueryForIncidentNoForIllnessId.Parameters.AddWithValue("@IllnessId", nSelectedIllnessId);

                if (connRN.State != ConnectionState.Closed)
                {
                    connRN.Close();
                    connRN.Open();
                }
                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                SqlDataReader rdrIncidentNo = cmdQueryForIncidentNoForIllnessId.ExecuteReader();
                if (rdrIncidentNo.HasRows)
                {
                    while (rdrIncidentNo.Read())
                    {
                        String IncidentNo = String.Empty;

                        if (!rdrIncidentNo.IsDBNull(0)) IncidentNo = rdrIncidentNo.GetString(0);
                        else IncidentNo = String.Empty;

                        lstIncidentNo.Add(IncidentNo);
                    }
                }
                if (connRN.State != ConnectionState.Closed) connRN.Close();

                foreach (String incd_no in lstIncidentNo)
                {
                    String strSqlQueryForTaskInfoForIncidentNo = "select [dbo].[tbl_task].[id], [dbo].[tbl_task].[Subject], [dbo].[tbl_task].[comment], [dbo].[tbl_task].[Solution], " +
                                                                 "[dbo].[tbl_task].[CreateDate] " +
                                                                 "from [dbo].[tbl_task] " +
                                                                 "where [dbo].[tbl_task].[whoid] = @IndividualId and " +
                                                                 "[dbo].[tbl_task].[whatid] = @IncidentNo";

                    SqlCommand cmdQueryForTaskInfoForIncidentNo = new SqlCommand(strSqlQueryForTaskInfoForIncidentNo, connRN);
                    cmdQueryForTaskInfoForIncidentNo.CommandType = CommandType.Text;

                    cmdQueryForTaskInfoForIncidentNo.Parameters.AddWithValue("@IndividualId", IndividualId);
                    cmdQueryForTaskInfoForIncidentNo.Parameters.AddWithValue("@IncidentNo", incd_no);

                    if (connRN.State != ConnectionState.Closed)
                    {
                        connRN.Close();
                        connRN.Open();
                    }
                    else if (connRN.State == ConnectionState.Closed) connRN.Open();
                    SqlDataReader rdrTaskInfoForIncidentNo = cmdQueryForTaskInfoForIncidentNo.ExecuteReader();
                    if (rdrTaskInfoForIncidentNo.HasRows)
                    {
                        while (rdrTaskInfoForIncidentNo.Read())
                        {
                            TaskInfo info = new TaskInfo();

                            if (!rdrTaskInfoForIncidentNo.IsDBNull(0)) info.TaskId = rdrTaskInfoForIncidentNo.GetInt32(0);
                            else info.TaskId = null;
                            if (!rdrTaskInfoForIncidentNo.IsDBNull(1)) info.Subject = rdrTaskInfoForIncidentNo.GetString(1);
                            else info.Subject = String.Empty;
                            if (!rdrTaskInfoForIncidentNo.IsDBNull(2)) info.Comment = rdrTaskInfoForIncidentNo.GetString(2);
                            else info.Comment = String.Empty;
                            if (!rdrTaskInfoForIncidentNo.IsDBNull(3)) info.Solution = rdrTaskInfoForIncidentNo.GetString(3);
                            else info.Solution = String.Empty;
                            if (!rdrTaskInfoForIncidentNo.IsDBNull(4)) info.CreateDate = rdrTaskInfoForIncidentNo.GetDateTime(4);
                            else info.CreateDate = null;

                            lstTaskInfo.Add(info);
                        }
                    }
                    rdrTaskInfoForIncidentNo.Close();
                    if (connRN.State != ConnectionState.Closed) connRN.Close();

                }

                List<IncidentIdNo> lstIncidentIdNo = new List<IncidentIdNo>();

                foreach (String incd_no in lstIncidentNo)
                {
                    String strSqlQueryForIncidentId = "select [dbo].[tbl_incident].[Incident_id] from [dbo].[tbl_incident] where [dbo].[tbl_incident].[IncidentNo] = @IncidentNo";

                    SqlCommand cmdQueryForIncidentId = new SqlCommand(strSqlQueryForIncidentId, connRN);
                    cmdQueryForIncidentId.CommandType = CommandType.Text;

                    cmdQueryForIncidentId.Parameters.AddWithValue("@IncidentNo", incd_no);

                    if (connRN.State != ConnectionState.Closed)
                    {
                        connRN.Close();
                        connRN.Open();
                    }
                    else if (connRN.State == ConnectionState.Closed) connRN.Open();
                    Object objIncidentId = cmdQueryForIncidentId.ExecuteScalar();
                    if (connRN.State != ConnectionState.Closed) connRN.Close();

                    int? nIncidentId = null;
                    if (objIncidentId != null) nIncidentId = Int32.Parse(objIncidentId.ToString());

                    lstIncidentIdNo.Add(new IncidentIdNo { IncidentId = nIncidentId, IncidentNo = incd_no });
                }

                List<String> lstMedBillNo = new List<string>();

                foreach (IncidentIdNo incd in lstIncidentIdNo)
                {
                    String strSqlQueryForMedBillNo = "select [dbo].[tbl_medbill].[BillNo] from [dbo].[tbl_medbill] " +
                                                     "where [dbo].[tbl_medbill].[Individual_Id] = @IndividualId and " +
                                                     "[dbo].[tbl_medbill].[Case_Id] = @CaseNo and " +
                                                     "[dbo].[tbl_medbill].[Illness_Id] = @IllnessId and " +
                                                     "[dbo].[tbl_medbill].[Incident_Id] = @IncidentId";

                    SqlCommand cmdQueryForMedBillNo = new SqlCommand(strSqlQueryForMedBillNo, connRN);
                    cmdQueryForMedBillNo.CommandType = CommandType.Text;

                    cmdQueryForMedBillNo.Parameters.AddWithValue("@IndividualId", IndividualId);
                    cmdQueryForMedBillNo.Parameters.AddWithValue("@CaseNo", SelectedCaseNo);
                    cmdQueryForMedBillNo.Parameters.AddWithValue("@IllnessId", nSelectedIllnessId);
                    cmdQueryForMedBillNo.Parameters.AddWithValue("@IncidentId", incd.IncidentId);

                    if (connRN.State != ConnectionState.Closed)
                    {
                        connRN.Close();
                        connRN.Open();
                    }
                    else if (connRN.State == ConnectionState.Closed) connRN.Open();
                    SqlDataReader rdrMedBillNo = cmdQueryForMedBillNo.ExecuteReader();
                    if (rdrMedBillNo.HasRows)
                    {
                        while (rdrMedBillNo.Read())
                        {
                            String MedBillNo = String.Empty;
                            if (!rdrMedBillNo.IsDBNull(0)) MedBillNo = rdrMedBillNo.GetString(0);
                            else MedBillNo = String.Empty;

                            lstMedBillNo.Add(MedBillNo);
                        }
                    }
                    rdrMedBillNo.Close();
                    if (connRN.State != ConnectionState.Closed) connRN.Close();
                }

                foreach (String medbill_no in lstMedBillNo)
                {
                    String strSqlQueryForTaskInfoForMedBillNo = "select [dbo].[tbl_task].[id], [dbo].[tbl_task].[Subject], [dbo].[tbl_task].[Comment], [dbo].[tbl_task].[Solution], " +
                                                                "[dbo].[tbl_task].[CreateDate] " +
                                                                "from [dbo].[tbl_task] " +
                                                                "where [dbo].[tbl_task].[whoid] = @IndividualId and " +
                                                                "[dbo].[tbl_task].[whatid] = @MedBillNo";

                    SqlCommand cmdQueryForTaskInfoForMedBillNo = new SqlCommand(strSqlQueryForTaskInfoForMedBillNo, connRN);
                    cmdQueryForTaskInfoForMedBillNo.CommandType = CommandType.Text;

                    cmdQueryForTaskInfoForMedBillNo.Parameters.AddWithValue("@IndividualId", IndividualId);
                    cmdQueryForTaskInfoForMedBillNo.Parameters.AddWithValue("@MedBillNo", medbill_no);

                    if (connRN.State != ConnectionState.Closed)
                    {
                        connRN.Close();
                        connRN.Open();
                    }
                    else if (connRN.State == ConnectionState.Closed) connRN.Open();
                    SqlDataReader rdrTaskInfoForMedBillNo = cmdQueryForTaskInfoForMedBillNo.ExecuteReader();
                    if (!rdrTaskInfoForMedBillNo.HasRows)
                    {
                        while (rdrTaskInfoForMedBillNo.Read())
                        {
                            TaskInfo info = new TaskInfo();
                            if (!rdrTaskInfoForMedBillNo.IsDBNull(0)) info.TaskId = rdrTaskInfoForMedBillNo.GetInt32(0);
                            else info.TaskId = null;
                            if (!rdrTaskInfoForMedBillNo.IsDBNull(1)) info.Subject = rdrTaskInfoForMedBillNo.GetString(1);
                            else info.Subject = String.Empty;
                            if (!rdrTaskInfoForMedBillNo.IsDBNull(2)) info.Comment = rdrTaskInfoForMedBillNo.GetString(2);
                            else info.Comment = String.Empty;
                            if (!rdrTaskInfoForMedBillNo.IsDBNull(3)) info.Solution = rdrTaskInfoForMedBillNo.GetString(3);
                            else info.Solution = String.Empty;
                            if (!rdrTaskInfoForMedBillNo.IsDBNull(4)) info.CreateDate = rdrTaskInfoForMedBillNo.GetDateTime(4);
                            else info.CreateDate = null;

                            lstTaskInfo.Add(info);
                        }
                    }
                    rdrTaskInfoForMedBillNo.Close();
                    if (connRN.State != ConnectionState.Closed) connRN.Close();
                }

                List<String> lstSettlementNo = new List<string>();

                foreach (String medbill_no in lstMedBillNo)
                {
                    String strSqlQueryForSettlementNo = "select [dbo].[tbl_settlement].[Name] from [dbo].[tbl_settlement] where [dbo].[tbl_settlement].[MedicalBillID] = @MedBillNo";

                    SqlCommand cmdQueryForSettlementNo = new SqlCommand(strSqlQueryForSettlementNo, connRN);
                    cmdQueryForSettlementNo.CommandType = CommandType.Text;

                    cmdQueryForSettlementNo.Parameters.AddWithValue("@MedBillNo", medbill_no);

                    if (connRN.State != ConnectionState.Closed)
                    {
                        connRN.Close();
                        connRN.Open();
                    }
                    else if (connRN.State == ConnectionState.Closed) connRN.Open();
                    SqlDataReader rdrSettlementNo = cmdQueryForSettlementNo.ExecuteReader();
                    if (rdrSettlementNo.HasRows)
                    {
                        while (rdrSettlementNo.Read())
                        {
                            String settlement_no = String.Empty;
                            if (!rdrSettlementNo.IsDBNull(0)) settlement_no = rdrSettlementNo.GetString(0);
                            else settlement_no = String.Empty;

                            lstSettlementNo.Add(settlement_no);
                        }
                    }
                    rdrSettlementNo.Close();
                    if (connRN.State != ConnectionState.Closed) connRN.Close();
                }

                foreach (String settlement_no in lstSettlementNo)
                {
                    String strSqlQueryForTaskInfoForSettlementNo = "select [dbo].[tbl_task].[id], [dbo].[tbl_task].[Subject], [dbo].[tbl_task].[Comment], [dbo].[tbl_task].[Solution], " +
                                                                   "[dbo].[tbl_task].[CreateDate] " +
                                                                   "from [dbo].[tbl_task] " +
                                                                   "where [dbo].[tbl_task].[whoid] = @IndividualId and " +
                                                                   "[dbo].[tbl_task].[whatid] = @SettlementNo";

                    SqlCommand cmdQueryForTaskInfoForSettlementNo = new SqlCommand(strSqlQueryForTaskInfoForSettlementNo, connRN);
                    cmdQueryForTaskInfoForSettlementNo.CommandType = CommandType.Text;

                    cmdQueryForTaskInfoForSettlementNo.Parameters.AddWithValue("@IndividualId", IndividualId);
                    cmdQueryForTaskInfoForSettlementNo.Parameters.AddWithValue("@SettlementNo", settlement_no);

                    if (connRN.State != ConnectionState.Closed)
                    {
                        connRN.Close();
                        connRN.Open();
                    }
                    else if (connRN.State == ConnectionState.Closed) connRN.Open();
                    SqlDataReader rdrTaskInfoForSettlementNo = cmdQueryForTaskInfoForSettlementNo.ExecuteReader();
                    if (rdrTaskInfoForSettlementNo.HasRows)
                    {
                        while (rdrTaskInfoForSettlementNo.Read())
                        {
                            TaskInfo info = new TaskInfo();

                            if (!rdrTaskInfoForSettlementNo.IsDBNull(0)) info.TaskId = rdrTaskInfoForSettlementNo.GetInt32(0);
                            else info.TaskId = null;
                            if (!rdrTaskInfoForSettlementNo.IsDBNull(1)) info.Subject = rdrTaskInfoForSettlementNo.GetString(1);
                            else info.Subject = String.Empty;
                            if (!rdrTaskInfoForSettlementNo.IsDBNull(2)) info.Comment = rdrTaskInfoForSettlementNo.GetString(2);
                            else info.Comment = String.Empty;
                            if (!rdrTaskInfoForSettlementNo.IsDBNull(3)) info.Solution = rdrTaskInfoForSettlementNo.GetString(3);
                            else info.Solution = String.Empty;
                            if (!rdrTaskInfoForSettlementNo.IsDBNull(4)) info.CreateDate = rdrTaskInfoForSettlementNo.GetDateTime(4);
                            else info.CreateDate = null;
                        }
                    }
                    rdrTaskInfoForSettlementNo.Close();
                    if (connRN.State != ConnectionState.Closed) connRN.Close();
                }

                lstTaskInfo.Sort(delegate (TaskInfo info1, TaskInfo info2)
                {
                    if (info1.CreateDate == null && info2.CreateDate == null) return 0;
                    else if (info1.CreateDate == null) return -1;
                    else if (info2.CreateDate == null) return 1;
                    else return (-1) * info1.CreateDate.Value.CompareTo(info2.CreateDate.Value);
                });

                foreach (TaskInfo info in lstTaskInfo)
                {
                    DataGridViewRow row = new DataGridViewRow();

                    if (info.TaskId != null) row.Cells.Add(new DataGridViewTextBoxCell { Value = info.TaskId.ToString() });
                    else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                    if (info.Subject != String.Empty) row.Cells.Add(new DataGridViewTextBoxCell { Value = info.Subject });
                    else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                    if (info.Comment != String.Empty) row.Cells.Add(new DataGridViewTextBoxCell { Value = info.Comment });
                    else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                    if (info.Solution != String.Empty) row.Cells.Add(new DataGridViewTextBoxCell { Value = info.Solution });
                    else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });

                    gvTaskList.Rows.Add(row);
                }

            }
        }
    }
}
