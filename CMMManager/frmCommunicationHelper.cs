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


            comboCaseNo.Items.Add("None");
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
            comboCaseNo.SelectedIndex = 0;
            
            String strSqlQueryForCommunication = "select [dbo].[tbl_Communication].[CommunicationNo], [dbo].[tbl_Communication].[CommunicationType], " +
                                                 "[dbo].[tbl_Communication].[Subject], [dbo].[tbl_Communication].[Body] " +
                                                 "from [dbo].[tbl_Communication] " +
                                                 "where [dbo].[tbl_Communication].[Individual_Id] = @IndividualId";

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
                                        "where [dbo].[tbl_task].[whoid] = @IndividualId";

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

        private void InitializeCommunicationDetail()
        {
            txtCommunicationType.Text = String.Empty;
            txtCommunicationCreatedBy.Text = String.Empty;
            txtCommunicationCreateDate.Text = String.Empty;
            txtCommunicationSubject.Text = String.Empty;
            txtCommunicationBody.Text = String.Empty;
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

        private void InitializeTaskDetail()
        {
            txtTaskCreatedBy.Text = String.Empty;
            txtTaskCreateDate.Text = String.Empty;
            txtTaskAssignedTo.Text = String.Empty;
            txtTaskDueDate.Text = String.Empty;
            txtTaskSubject.Text = String.Empty;
            txtTaskComment.Text = String.Empty;
            txtTaskSolution.Text = String.Empty;
            txtTaskStatus.Text = String.Empty;
        }

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
                if (cbCaseNo.SelectedItem.ToString() != "None")
                {
                    String SelectedCaseNo = cbCaseNo.SelectedItem.ToString();

                    comboIllnessNo.Items.Clear();
                    comboIllnessNo.Items.Add("None");
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

                    comboIllnessNo.SelectedIndex = 0;

                    gvCommunicationList.Rows.Clear();
                    InitializeCommunicationDetail();

                    String strSqlQueryForCommunication = "select [dbo].[tbl_Communication].[CommunicationNo], [dbo].[tbl_Communication].[CommunicationType], " +
                                                         "[dbo].[tbl_Communication].[Subject], [dbo].[tbl_Communication].[Body] " +
                                                         "from [dbo].[tbl_Communication] " +
                                                         "where [dbo].[tbl_Communication].[Individual_Id] = @IndividualId and [dbo].[tbl_Communication].[CaseNo] = @SelectedCaseNo";

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


                    gvTaskList.Rows.Clear();

                    String strSqlQueryForTaskInCase = "select [dbo].[tbl_task].[id], [dbo].[tbl_task].[Subject], [dbo].[tbl_task].[Comment], [dbo].[tbl_task].[Solution] " +
                                                      "from [dbo].[tbl_task] " +
                                                      "where [dbo].[tbl_task].[whatid] = @CaseNoSelected";

                    SqlCommand cmdQueryForTaskInCase = new SqlCommand(strSqlQueryForTaskInCase, connRN);
                    cmdQueryForTaskInCase.CommandType = CommandType.Text;

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
                            DataGridViewRow row = new DataGridViewRow();
                            if (!rdrTaskInCase.IsDBNull(0)) row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrTaskInCase.GetInt32(0).ToString() });
                            else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                            if (!rdrTaskInCase.IsDBNull(1)) row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrTaskInCase.GetString(1) });
                            else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                            if (!rdrTaskInCase.IsDBNull(2)) row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrTaskInCase.GetString(2) });
                            else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                            if (!rdrTaskInCase.IsDBNull(3)) row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrTaskInCase.GetString(3) });
                            else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });

                            gvTaskList.Rows.Add(row);
                        }
                    }
                    rdrTaskInCase.Close();
                    if (connRN.State != ConnectionState.Closed) connRN.Close();
                }
                else
                {
                    gvCommunicationList.Rows.Clear();

                    String strSqlQueryForCommunication = "select [dbo].[tbl_Communication].[CommunicationNo], [dbo].[tbl_Communication].[CommunicationType], " +
                                                         "[dbo].[tbl_Communication].[Subject], [dbo].[tbl_Communication].[Body] " +
                                                         "from [dbo].[tbl_Communication] " +
                                                         "where [dbo].[tbl_Communication].[Individual_Id] = @IndividualId";

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
                                                "where [dbo].[tbl_task].[whoid] = @IndividualId";

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
                if (comboCaseNo.SelectedItem.ToString() != "None")
                {
                    gvCommunicationList.Rows.Clear();
                    gvTaskList.Rows.Clear();

                    String SelectedCaseNo = comboCaseNo.SelectedItem.ToString();

                    String SelectedIllnessNo = String.Empty;
                    if (comboIllnessNo.SelectedItem != null)
                    {
                        if (comboIllnessNo.SelectedItem.ToString() != "None") SelectedIllnessNo = comboIllnessNo.SelectedItem.ToString();
                    }

                    String strSqlQueryForCommunication = "select [dbo].[tbl_Communication].[CommunicationNo], [dbo].[tbl_Communication].[CommunicationType], " +
                                                         "[dbo].[tbl_Communication].[Subject], [dbo].[tbl_Communication].[Body] " +
                                                         "from [dbo].[tbl_Communication] " +
                                                         "where [dbo].[tbl_Communication].[Individual_Id] = @IndividualId and " +
                                                         "[dbo].[tbl_Communication].[CaseNo] = @SelectedCaseNo and " +
                                                         "[dbo].[tbl_Communication].[IllnessNo] = @SelectedIllnessNo";

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

                    //String IllnessNo = String.Empty;

                    //if (comboIllnessNo.SelectedItem != null)
                    //{
                    //    if (comboIllnessNo.SelectedItem.ToString() != "None")
                    //    {
                    //        IllnessNo = comboIllnessNo.SelectedItem.ToString();

                    //        int? nIllnessId = null;

                    //    }
                    //}
                }
            }
        }

        private void comboIncidentNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            String SelectedCaseNo = String.Empty;
            if (comboCaseNo.SelectedItem != null)
            {
                if (comboCaseNo.SelectedItem.ToString() != "None")
                {
                    SelectedCaseNo = comboCaseNo.SelectedItem.ToString();
                }
            }

            String SelectedIllnessNo = String.Empty;
            if (comboIllnessNo.SelectedItem != null)
            {
                if (comboIllnessNo.SelectedItem.ToString() != "None")
                {
                    SelectedIllnessNo = comboIllnessNo.SelectedItem.ToString();
                }
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
                if (comboIncidentNo.SelectedItem.ToString() != "None")
                {
                    SelectedIncidentNo = comboIncidentNo.SelectedItem.ToString();
                }
            }

            String strSqlQueryForCommunication = "select [dbo].[tbl_Communication].[CommunicationNo], [dbo].[tbl_Communication].[CommunicationType], " +
                                                 "[dbo].[tbl_Communication].[Subject], [dbo].[tbl_Communication].[Body] " +
                                                 "from [dbo].[tbl_Communication] " +
                                                 "where [dbo].[tbl_Communication].[CaseNo] = @SelectedCaseNo and " +
                                                 "[dbo].[tbl_Communication].[IllnessNo] = @SelectedIllnessNo and " +
                                                 "[dbo].[tbl_Communication].[IncidentNo] = @SelectedIncidentNo";

            SqlCommand cmdQueryForCommunication = new SqlCommand(strSqlQueryForCommunication, connRN);
            cmdQueryForCommunication.CommandType = CommandType.Text;

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
        }
    }
}
