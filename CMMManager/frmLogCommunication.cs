using System;
using System.IO;
using System.Diagnostics;
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
    public partial class frmLogCommunication : Form
    {
        public String IndividualId;
        private int nLoggedInUserId;
        private String CommunicationNo;
        private String CaseNo;
        private CommunicationType CommType;
        private CommunicationOpenMode OpenMode;

        private String strConnString;
        private SqlConnection connRN;

        public String Subject = String.Empty;
        public String Body = String.Empty;

        private const String CommunicationAttachmentDestinationPath = @"\\cmm-2014u\Sharefolder\CommunicationAttachments\";

        public frmLogCommunication()
        {
            IndividualId = String.Empty;
            strConnString = @"Data Source = 12.230.174.166\cmm; Initial Catalog = RN_DB; Integrated Security = True; Max Pool Size = 200";
            connRN = new SqlConnection(strConnString);

            InitializeComponent();
        }

        public frmLogCommunication(String individual_id, int login_user_id, CommunicationOpenMode mode)
        {
            InitializeComponent();

            IndividualId = individual_id;
            nLoggedInUserId = login_user_id;

            strConnString = @"Data Source = 12.230.174.166\cmm; Initial Catalog = RN_DB; Integrated Security = True; Max Pool Size = 200";
            connRN = new SqlConnection(strConnString);

            OpenMode = mode;
        }

        public frmLogCommunication(String individual_id, int login_user_id, String communication_no, CommunicationType type, String case_no, String subject, String body, CommunicationOpenMode mode)
        {
            InitializeComponent();
            IndividualId = individual_id;
            nLoggedInUserId = login_user_id;

            CommunicationNo = communication_no;
            CaseNo = case_no;

            CommType = type;

            Subject = subject;
            Body = body;

            OpenMode = mode;

            strConnString = @"Data Source = 12.230.174.166\cmm; Initial Catalog = RN_DB; Integrated Security = True; Max Pool Size = 200";
            connRN = new SqlConnection(strConnString);
        }

        private void frmLogCommunication_Load(object sender, EventArgs e)
        {
            String strIndividualId = IndividualId;
            txtCommunicationIndividualId.Text = strIndividualId;

            if (OpenMode == CommunicationOpenMode.AddNew)
            {
                if (Subject != String.Empty) txtCommunicationSubject.Text = Subject;
                if (Body != String.Empty) txtCommunicationBody.Text = Body;

                String strSqlQueryForLastCommunicationNo = "select [dbo].[tbl_LastID].[CommunicationNo] from [dbo].[tbl_LastID] where [dbo].[tbl_LastID].[Id] = 1";

                SqlCommand cmdQueryForLastCommunicationNo = new SqlCommand(strSqlQueryForLastCommunicationNo, connRN);
                cmdQueryForLastCommunicationNo.CommandType = CommandType.Text;

                if (connRN.State != ConnectionState.Closed)
                {
                    connRN.Close();
                    connRN.Open();
                }
                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                Object objLastCommunicationNo = cmdQueryForLastCommunicationNo.ExecuteScalar();
                if (connRN.State != ConnectionState.Closed) connRN.Close();

                String strLastCommunicationNo = String.Empty;
                if (objLastCommunicationNo != null) strLastCommunicationNo = objLastCommunicationNo.ToString();

                String strNewCommunicationNo = String.Empty;
                if (strLastCommunicationNo != String.Empty)
                {
                    String strNewCommunicationNoNumber = strLastCommunicationNo.Substring(5);
                    int nNewCommunicatoinNoNumber = Int32.Parse(strNewCommunicationNoNumber);
                    nNewCommunicatoinNoNumber++;

                    strNewCommunicationNo = "Comm-" + nNewCommunicatoinNoNumber.ToString();
                }

                String strSqlUpdateLastCommunicationNo = "update [dbo].[tbl_LastID] set [dbo].[tbl_LastID].[CommunicationNo] = @NewCommunicationNo";

                SqlCommand cmdUpdateLastCommunicationNo = new SqlCommand(strSqlUpdateLastCommunicationNo, connRN);
                cmdUpdateLastCommunicationNo.CommandType = CommandType.Text;

                cmdUpdateLastCommunicationNo.Parameters.AddWithValue("@NewCommunicationNo", strNewCommunicationNo);

                if (connRN.State != ConnectionState.Closed)
                {
                    connRN.Close();
                    connRN.Open();
                }
                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                int nCommunicationNoUpdated = cmdUpdateLastCommunicationNo.ExecuteNonQuery();
                if (connRN.State != ConnectionState.Closed) connRN.Close();

                txtCommunicationNo.Text = strNewCommunicationNo;

                String strSqlQueryForCasesForIndividual = "select [dbo].[tbl_case].[Case_Name] from [dbo].[tbl_case] where [dbo].[tbl_case].[individual_id] = @IndividualId " +
                                                          "order by [dbo].[tbl_case].[Case_Name] desc";

                SqlCommand cmdQueryForCasesForIndividual = new SqlCommand(strSqlQueryForCasesForIndividual, connRN);
                cmdQueryForCasesForIndividual.CommandType = CommandType.Text;

                cmdQueryForCasesForIndividual.Parameters.AddWithValue("@IndividualId", strIndividualId);

                if (connRN.State != ConnectionState.Closed)
                {
                    connRN.Close();
                    connRN.Open();
                }
                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                SqlDataReader rdrCasesForIndividual = cmdQueryForCasesForIndividual.ExecuteReader();
                comboCaseNo.Items.Add(String.Empty);
                if (rdrCasesForIndividual.HasRows)
                {
                    while (rdrCasesForIndividual.Read())
                    {
                        if (!rdrCasesForIndividual.IsDBNull(0)) comboCaseNo.Items.Add(rdrCasesForIndividual.GetString(0));
                    }
                }
                rdrCasesForIndividual.Close();
                if (connRN.State != ConnectionState.Closed) connRN.Close();

                String strSqlQueryForCommunicationTypes = "select [dbo].[tbl_communication_type_code].[CommunicationTypeValue] from [dbo].[tbl_communication_type_code]";

                SqlCommand cmdQueryForCommunicationsTypes = new SqlCommand(strSqlQueryForCommunicationTypes, connRN);
                cmdQueryForCommunicationsTypes.CommandType = CommandType.Text;

                if (connRN.State != ConnectionState.Closed)
                {
                    connRN.Close();
                    connRN.Open();
                }
                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                SqlDataReader rdrCommunicationTypes = cmdQueryForCommunicationsTypes.ExecuteReader();
                if (rdrCommunicationTypes.HasRows)
                {
                    while (rdrCommunicationTypes.Read())
                    {
                        if (!rdrCommunicationTypes.IsDBNull(0)) comboCommunicationType.Items.Add(rdrCommunicationTypes.GetString(0));
                    }
                }
                rdrCommunicationTypes.Close();
                if (connRN.State != ConnectionState.Closed) connRN.Close();
            }
            else if (OpenMode == CommunicationOpenMode.ReadOnly)
            {

                String strSqlQueryForCasesForIndividual = "select [dbo].[tbl_case].[Case_Name] from [dbo].[tbl_case] where [dbo].[tbl_case].[individual_id] = @IndividualId " +
                                          "order by [dbo].[tbl_case].[Case_Name] desc";

                SqlCommand cmdQueryForCasesForIndividual = new SqlCommand(strSqlQueryForCasesForIndividual, connRN);
                cmdQueryForCasesForIndividual.CommandType = CommandType.Text;

                cmdQueryForCasesForIndividual.Parameters.AddWithValue("@IndividualId", strIndividualId);

                if (connRN.State != ConnectionState.Closed)
                {
                    connRN.Close();
                    connRN.Open();
                }
                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                SqlDataReader rdrCasesForIndividual = cmdQueryForCasesForIndividual.ExecuteReader();
                comboCaseNo.Items.Add(String.Empty);
                if (rdrCasesForIndividual.HasRows)
                {
                    while (rdrCasesForIndividual.Read())
                    {
                        if (!rdrCasesForIndividual.IsDBNull(0)) comboCaseNo.Items.Add(rdrCasesForIndividual.GetString(0));
                    }
                }
                rdrCasesForIndividual.Close();
                if (connRN.State != ConnectionState.Closed) connRN.Close();

                String strSqlQueryForCommunicationTypes = "select [dbo].[tbl_communication_type_code].[CommunicationTypeValue] from [dbo].[tbl_communication_type_code]";

                SqlCommand cmdQueryForCommunicationsTypes = new SqlCommand(strSqlQueryForCommunicationTypes, connRN);
                cmdQueryForCommunicationsTypes.CommandType = CommandType.Text;

                if (connRN.State != ConnectionState.Closed)
                {
                    connRN.Close();
                    connRN.Open();
                }
                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                SqlDataReader rdrCommunicationTypes = cmdQueryForCommunicationsTypes.ExecuteReader();
                if (rdrCommunicationTypes.HasRows)
                {
                    while (rdrCommunicationTypes.Read())
                    {
                        if (!rdrCommunicationTypes.IsDBNull(0)) comboCommunicationType.Items.Add(rdrCommunicationTypes.GetString(0));
                    }
                }
                rdrCommunicationTypes.Close();
                if (connRN.State != ConnectionState.Closed) connRN.Close();

                txtCommunicationIndividualId.Text = IndividualId;
                txtCommunicationIndividualId.ReadOnly = true;
                txtCommunicationNo.Text = CommunicationNo;
                txtCommunicationNo.ReadOnly = true;

                for (int i = 0; i < comboCaseNo.Items.Count; i++)
                {
                    if (CaseNo == comboCaseNo.Items[i].ToString()) comboCaseNo.SelectedIndex = i;
                }
                comboCaseNo.Enabled = false;

                comboCommunicationType.SelectedIndex = (int)CommType;

                txtCommunicationSubject.Text = Subject;
                txtCommunicationSubject.ReadOnly = true;
                txtCommunicationBody.Text = Body;
                txtCommunicationBody.ReadOnly = true;

                String strSqlQueryForAttachments = "select [dbo].[tbl_CommunicationAttachments].[AttachmentNo], [dbo].[tbl_CommunicationAttachments].[AttachedFileName], " +
                                                   "[dbo].[tbl_CreateStaff].[Staff_Name], [dbo].[tbl_CommunicationAttachments].[CreateDate] " +
                                                   "from [dbo].[tbl_CommunicationAttachments] " +
                                                   "inner join [dbo].[tbl_CreateStaff] on [dbo].[tbl_CommunicationAttachments].[CreatedBy] = [dbo].[tbl_CreateStaff].[CreateStaff_Id] " +
                                                   "where [dbo].[tbl_CommunicationAttachments].[CommunicationNo] = @CommunicationNo";

                SqlCommand cmdQueryForAttachments = new SqlCommand(strSqlQueryForAttachments, connRN);
                cmdQueryForAttachments.CommandType = CommandType.Text;

                cmdQueryForAttachments.Parameters.AddWithValue("@CommunicationNo", CommunicationNo);
                if (connRN.State != ConnectionState.Closed)
                {
                    connRN.Close();
                    connRN.Open();
                }
                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                SqlDataReader rdrAttachments = cmdQueryForAttachments.ExecuteReader();
                if (rdrAttachments.HasRows)
                {
                    while (rdrAttachments.Read())
                    {
                        DataGridViewRow row = new DataGridViewRow();

                        row.Cells.Add(new DataGridViewCheckBoxCell { Value = false });
                        row.Cells[0].ReadOnly = true;
                        if (!rdrAttachments.IsDBNull(0)) row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrAttachments.GetString(0) });
                        else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                        row.Cells[1].ReadOnly = true;
                        row.Cells.Add(new DataGridViewButtonCell { Value = "Attach" });
                        row.Cells[2].ReadOnly = true;
                        if (!rdrAttachments.IsDBNull(1)) row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrAttachments.GetString(1) });
                        else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                        row.Cells[3].ReadOnly = true;
                        row.Cells.Add(new DataGridViewButtonCell { Value = "View" });
                        if (!rdrAttachments.IsDBNull(2)) row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrAttachments.GetString(2) });
                        else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                        row.Cells[4].ReadOnly = true;
                        if (!rdrAttachments.IsDBNull(3)) row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrAttachments.GetDateTime(3).ToString("MM/dd/yyyy HH:mm:ss") });
                        else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                        row.Cells[6].ReadOnly = true;

                        gvCommunicationAttachment.Rows.Add(row);
                    }
                }
                rdrAttachments.Close();
                if (connRN.State != ConnectionState.Closed) connRN.Close();

                MakeCommunicationFormReadOnly();
            }
            else if (OpenMode == CommunicationOpenMode.Update)
            {
                String strSqlQueryForCasesForIndividual = "select [dbo].[tbl_case].[Case_Name] from [dbo].[tbl_case] where [dbo].[tbl_case].[individual_id] = @IndividualId " +
                          "order by [dbo].[tbl_case].[Case_Name] desc";

                SqlCommand cmdQueryForCasesForIndividual = new SqlCommand(strSqlQueryForCasesForIndividual, connRN);
                cmdQueryForCasesForIndividual.CommandType = CommandType.Text;

                cmdQueryForCasesForIndividual.Parameters.AddWithValue("@IndividualId", strIndividualId);

                if (connRN.State != ConnectionState.Closed)
                {
                    connRN.Close();
                    connRN.Open();
                }
                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                SqlDataReader rdrCasesForIndividual = cmdQueryForCasesForIndividual.ExecuteReader();
                comboCaseNo.Items.Add(String.Empty);
                if (rdrCasesForIndividual.HasRows)
                {
                    while (rdrCasesForIndividual.Read())
                    {
                        if (!rdrCasesForIndividual.IsDBNull(0)) comboCaseNo.Items.Add(rdrCasesForIndividual.GetString(0));
                    }
                }
                rdrCasesForIndividual.Close();
                if (connRN.State != ConnectionState.Closed) connRN.Close();

                String strSqlQueryForCommunicationTypes = "select [dbo].[tbl_communication_type_code].[CommunicationTypeValue] from [dbo].[tbl_communication_type_code]";

                SqlCommand cmdQueryForCommunicationsTypes = new SqlCommand(strSqlQueryForCommunicationTypes, connRN);
                cmdQueryForCommunicationsTypes.CommandType = CommandType.Text;

                if (connRN.State != ConnectionState.Closed)
                {
                    connRN.Close();
                    connRN.Open();
                }
                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                SqlDataReader rdrCommunicationTypes = cmdQueryForCommunicationsTypes.ExecuteReader();
                if (rdrCommunicationTypes.HasRows)
                {
                    while (rdrCommunicationTypes.Read())
                    {
                        if (!rdrCommunicationTypes.IsDBNull(0)) comboCommunicationType.Items.Add(rdrCommunicationTypes.GetString(0));
                    }
                }
                rdrCommunicationTypes.Close();
                if (connRN.State != ConnectionState.Closed) connRN.Close();

                txtCommunicationIndividualId.Text = IndividualId;
                txtCommunicationIndividualId.ReadOnly = true;
                txtCommunicationNo.Text = CommunicationNo;
                txtCommunicationNo.ReadOnly = true;

                for (int i = 0; i < comboCaseNo.Items.Count; i++)
                {
                    if (CaseNo == comboCaseNo.Items[i].ToString()) comboCaseNo.SelectedIndex = i;
                }
                comboCaseNo.Enabled = false;

                comboCommunicationType.SelectedIndex = (int)CommType;

                txtCommunicationSubject.Text = Subject;
                //txtCommunicationSubject.ReadOnly = true;
                txtCommunicationBody.Text = Body;
                //txtCommunicationBody.ReadOnly = true;

                String strSqlQueryForAttachments = "select [dbo].[tbl_CommunicationAttachments].[AttachmentNo], [dbo].[tbl_CommunicationAttachments].[AttachedFileName], " +
                                                   "[dbo].[tbl_CreateStaff].[Staff_Name], [dbo].[tbl_CommunicationAttachments].[CreateDate] " +
                                                   "from [dbo].[tbl_CommunicationAttachments] " +
                                                   "inner join [dbo].[tbl_CreateStaff] on [dbo].[tbl_CommunicationAttachments].[CreatedBy] = [dbo].[tbl_CreateStaff].[CreateStaff_Id] " +
                                                   "where [dbo].[tbl_CommunicationAttachments].[CommunicationNo] = @CommunicationNo";

                SqlCommand cmdQueryForAttachments = new SqlCommand(strSqlQueryForAttachments, connRN);
                cmdQueryForAttachments.CommandType = CommandType.Text;

                cmdQueryForAttachments.Parameters.AddWithValue("@CommunicationNo", CommunicationNo);
                if (connRN.State != ConnectionState.Closed)
                {
                    connRN.Close();
                    connRN.Open();
                }
                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                SqlDataReader rdrAttachments = cmdQueryForAttachments.ExecuteReader();
                if (rdrAttachments.HasRows)
                {
                    while (rdrAttachments.Read())
                    {
                        DataGridViewRow row = new DataGridViewRow();

                        row.Cells.Add(new DataGridViewCheckBoxCell { Value = false });
                        row.Cells[0].ReadOnly = true;
                        if (!rdrAttachments.IsDBNull(0)) row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrAttachments.GetString(0) });
                        else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                        row.Cells[1].ReadOnly = true;
                        row.Cells.Add(new DataGridViewButtonCell { Value = "Attach" });
                        row.Cells[2].ReadOnly = true;
                        if (!rdrAttachments.IsDBNull(1)) row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrAttachments.GetString(1) });
                        else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                        row.Cells[3].ReadOnly = true;
                        row.Cells.Add(new DataGridViewButtonCell { Value = "View" });
                        if (!rdrAttachments.IsDBNull(2)) row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrAttachments.GetString(2) });
                        else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                        row.Cells[4].ReadOnly = true;
                        if (!rdrAttachments.IsDBNull(3)) row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrAttachments.GetDateTime(3).ToString("MM/dd/yyyy HH:mm:ss") });
                        else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                        row.Cells[6].ReadOnly = true;

                        gvCommunicationAttachment.Rows.Add(row);
                    }
                }
                rdrAttachments.Close();
                if (connRN.State != ConnectionState.Closed) connRN.Close();
            }
        }

        private void MakeCommunicationFormReadOnly()
        {
            txtCommunicationSubject.ReadOnly = true;
            txtCommunicationBody.ReadOnly = true;

            comboCaseNo.Enabled = false;
            comboCommunicationType.Enabled = false;

            btnAddNewAttachment.Enabled = false;
            btnDeleteAttachment.Enabled = false;

            btnSaveCommunication.Enabled = false;
            btnCommunicationCancel.Text = "Close";
        }

        private void btnCommunicationCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSaveCommunication_Click(object sender, EventArgs e)
        {
            String strIndividualId = txtCommunicationIndividualId.Text.Trim();
            String strCaseNo = comboCaseNo.SelectedItem.ToString();
            String strCommunicationNo = txtCommunicationNo.Text.Trim();
            String strCommunicationType = comboCommunicationType.SelectedItem.ToString();

            String strSubject = String.Empty;
            if (txtCommunicationSubject.Text.Trim() != String.Empty) strSubject = txtCommunicationSubject.Text.Trim();
            String strBody = String.Empty;
            if (txtCommunicationBody.Text.Trim() != String.Empty) strBody = txtCommunicationBody.Text.Trim();

            CommunicationType communicationType = CommunicationType.EmailReceived;

            switch (strCommunicationType)
            {
                case "Incoming Call":
                    communicationType = CommunicationType.IncomingCall;
                    break;
                case "Outgoing Call":
                    communicationType = CommunicationType.OutgoingCall;
                    break;
                case "Incoming Fax":
                    communicationType = CommunicationType.IncommingFax;
                    break;
                case "Outgoing Fax":
                    communicationType = CommunicationType.OutgoingFax;
                    break;
                case "Incoming E-Fax":
                    communicationType = CommunicationType.IncomingEFax;
                    break;
                case "Outgoing E-Fax":
                    communicationType = CommunicationType.OutgoingEFax;
                    break;
                case "Email Received":
                    communicationType = CommunicationType.EmailReceived;
                    break;
                case "Email Sent":
                    communicationType = CommunicationType.EmailSent;
                    break;
                case "Letter Received":
                    communicationType = CommunicationType.LetterReceived;
                    break;
                case "Letter Mailed":
                    communicationType = CommunicationType.LetterMailed;
                    break;
                case "Other":
                    communicationType = CommunicationType.Other;
                    break;
            }

            String strSqlQueryForCommunicationNo = "select [dbo].[tbl_Communication].[CommunicationNo] from [dbo].[tbl_Communication] " +
                                                   "where [dbo].[tbl_Communication].[CommunicationNo] = @CommunicationNo";

            SqlCommand cmdQueryForCommunicationNo = new SqlCommand(strSqlQueryForCommunicationNo, connRN);
            cmdQueryForCommunicationNo.CommandType = CommandType.Text;

            cmdQueryForCommunicationNo.Parameters.AddWithValue("@CommunicationNo", strCommunicationNo);

            if (connRN.State != ConnectionState.Closed)
            {
                connRN.Close();
                connRN.Open();
            }
            else if (connRN.State == ConnectionState.Closed) connRN.Open();
            Object objCommunicationNo = cmdQueryForCommunicationNo.ExecuteScalar();
            if (connRN.State != ConnectionState.Closed) connRN.Close();

            String CommunicationNo = String.Empty;
            if (objCommunicationNo != null) CommunicationNo = objCommunicationNo.ToString();

            if (CommunicationNo == String.Empty)
            {
                Boolean bCommunicationSaved = false;

                String strSqlInsertNewCommunication = "insert into [dbo].[tbl_Communication] ([dbo].[tbl_Communiation].[Individual_Id], [dbo].[tbl_Communication].[CaseNo], " +
                                                      "[dbo].[tbl_Communication].[CommunicationNo], [dbo].[tbl_Communication].[CommunicationType], " +
                                                      "[dbo].[tbl_Communication].[Subject], [dbo].[tbl_Communication].[Body], " +
                                                      "[dbo].[tbl_Communication].[CreateDate], [dbo].[tbl_Communication].[CreatedBy]) " +
                                                      "values (@IndividualId, @CaseNo, @CommunicationNo, @CommunicationType, @Subject, @Body, @CreateDate, @CreatedBy)";

                SqlCommand cmdInsertNewCommunication = new SqlCommand(strSqlInsertNewCommunication, connRN);
                cmdInsertNewCommunication.CommandType = CommandType.Text;

                cmdInsertNewCommunication.Parameters.AddWithValue("@IndividualId", strIndividualId);
                cmdInsertNewCommunication.Parameters.AddWithValue("@CaseNo", strCaseNo);
                cmdInsertNewCommunication.Parameters.AddWithValue("@communicationNo", strCommunicationNo);
                cmdInsertNewCommunication.Parameters.AddWithValue("@CommunicationType", communicationType);
                if (strSubject != String.Empty) cmdInsertNewCommunication.Parameters.AddWithValue("@Subject", strSubject);
                else cmdInsertNewCommunication.Parameters.AddWithValue("@Subject", DBNull.Value);
                if (strBody != String.Empty) cmdInsertNewCommunication.Parameters.AddWithValue("@Body", strBody);
                else cmdInsertNewCommunication.Parameters.AddWithValue("@Body", DBNull.Value);
                cmdInsertNewCommunication.Parameters.AddWithValue("@CreateDate", DateTime.Now);
                cmdInsertNewCommunication.Parameters.AddWithValue("@CreatedBy", nLoggedInUserId);

                if (connRN.State != ConnectionState.Closed)
                {
                    connRN.Close();
                    connRN.Open();
                }
                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                int nNewCommunicationInserted = cmdInsertNewCommunication.ExecuteNonQuery();
                if (connRN.State != ConnectionState.Closed) connRN.Close();

                if (nNewCommunicationInserted == 1) bCommunicationSaved = true;   //MessageBox.Show("New communication has been inserted.", "Info");
                else if (nNewCommunicationInserted == 0) bCommunicationSaved = false; // MessageBox.Show("New communication insertion has failed.", "Error"); 

                Boolean bAttachmentSaved = false;

                for (int i = 0; i < gvCommunicationAttachment.Rows.Count; i++)
                {
                    String CommunicationAttachmentNo = gvCommunicationAttachment["CommunicationAttachmentNo", i]?.Value?.ToString();

                    String strSqlQueryForCommAttachmentNo = "select [dbo].[tbl_CommunicationAttachments].[AttachmentNo] from [dbo].[tbl_CommunicationAttachments] " +
                                                            "where [dbo].[tbl_CommunicationAttachments].[AttachmentNo] = @CommAttachmentNo";

                    SqlCommand cmdQueyrForCommAttachmentNo = new SqlCommand(strSqlQueryForCommAttachmentNo, connRN);
                    cmdQueyrForCommAttachmentNo.CommandType = CommandType.Text;

                    cmdQueyrForCommAttachmentNo.Parameters.AddWithValue("@CommAttachmentNo", CommunicationAttachmentNo);

                    if (connRN.State != ConnectionState.Closed)
                    {
                        connRN.Close();
                        connRN.Open();
                    }
                    else if (connRN.State == ConnectionState.Closed) connRN.Open();
                    Object objCommAttachmentNo = cmdQueyrForCommAttachmentNo.ExecuteScalar();
                    if (connRN.State != ConnectionState.Closed) connRN.Close();

                    String CommAttachmentNo = String.Empty;
                    if (objCommAttachmentNo != null) CommAttachmentNo = objCommAttachmentNo.ToString();

                    if (CommAttachmentNo == String.Empty)
                    {
                        String NewCommunicationAttachmentNo = gvCommunicationAttachment["CommunicationAttachmentNo", i]?.Value?.ToString();
                        String NewCommunicationAttachmentFileName = gvCommunicationAttachment["CommunicationAttachmentFileName", i]?.Value?.ToString();

                        if (NewCommunicationAttachmentFileName == String.Empty)
                        {
                            MessageBox.Show("Attachment file has not been uploaded.", "Error");
                            return;
                        }

                        String CommNo = txtCommunicationNo.Text.Trim();
                        String strAttachmentCreateDate = gvCommunicationAttachment["CreateDateCommunicationAttachment", i]?.Value?.ToString();
                        DateTime resultAttachmentCreateDate;
                        DateTime? AttachmentCreateDate = null;

                        if (DateTime.TryParse(strAttachmentCreateDate, out resultAttachmentCreateDate)) AttachmentCreateDate = resultAttachmentCreateDate;
                        else
                        {
                            MessageBox.Show("The Attachment Create Date is invalid.", "Error");
                            return;
                        }

                        String strSqlInsertNewAttachment = "insert into [dbo].[tbl_CommunicationAttachments] ([dbo].[tbl_CommunicationAttachments].[IsDeleted], " +
                                                           "[dbo].[tbl_CommunicationAttachments].[CommunicationNo], [dbo].[tbl_CommunicationAttachments].[AttachmentNo], " +
                                                           "[dbo].[tbl_CommunicationAttachments].[AttachedFileName], " +
                                                           "[dbo].[tbl_CommunicationAttachments].[CreatedBy], [dbo].[tbl_CommunicationAttachments].[CreateDate]) " +
                                                           "values (0, @CommunicationNo, @AttachmentNo, @AttachedFileName, @CreatedBy, @CreateDate)";

                        SqlCommand cmdInsertNewAttachment = new SqlCommand(strSqlInsertNewAttachment, connRN);
                        cmdInsertNewAttachment.CommandType = CommandType.Text;

                        cmdInsertNewAttachment.Parameters.AddWithValue("@CommunicationNo", CommNo);
                        cmdInsertNewAttachment.Parameters.AddWithValue("@AttachmentNo", NewCommunicationAttachmentNo);
                        cmdInsertNewAttachment.Parameters.AddWithValue("@AttachedFileName", NewCommunicationAttachmentFileName);
                        cmdInsertNewAttachment.Parameters.AddWithValue("@CreatedBy", nLoggedInUserId);
                        cmdInsertNewAttachment.Parameters.AddWithValue("@CreateDate", AttachmentCreateDate.Value);

                        if (connRN.State != ConnectionState.Closed)
                        {
                            connRN.Close();
                            connRN.Open();
                        }
                        else if (connRN.State == ConnectionState.Closed) connRN.Open();
                        int nNewAttachmentInserted = cmdInsertNewAttachment.ExecuteNonQuery();
                        if (connRN.State != ConnectionState.Closed) connRN.Close();

                        if (nNewAttachmentInserted == 1) bAttachmentSaved = true;
                        else if (nNewAttachmentInserted == 0) bAttachmentSaved = false;
                    }
                }

                if (bCommunicationSaved && bAttachmentSaved) MessageBox.Show("Communication and Attachment have been saved", "Info");
                else if (bCommunicationSaved && !bAttachmentSaved) MessageBox.Show("Communication has been saved, but at least one of attachment has not been save.", "Error");
                else if (!bCommunicationSaved && !bAttachmentSaved) MessageBox.Show("Communiation and Attachment have not been saved.", "Error");

                Close();
            }
            else
            {
                // 07/29/19 - begin here to update communication code
            }
        }

        private void btnAddNewAttachment_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = new DataGridViewRow();

            DataGridViewCheckBoxCell chkSelectedCell = new DataGridViewCheckBoxCell();
            chkSelectedCell.Value = false;
            row.Cells.Add(chkSelectedCell);
            row.Cells[0].ReadOnly = false;

            String strQueryForLastAttachmentNo = "select [dbo].[tbl_LastID].[CommAttachmentNo] from [dbo].[tbl_LastID] where [dbo].[tbl_LastID].[Id] = 1";

            SqlCommand cmdQueryForLastAttachmentNo = new SqlCommand(strQueryForLastAttachmentNo, connRN);
            cmdQueryForLastAttachmentNo.CommandType = CommandType.Text;

            if (connRN.State != ConnectionState.Closed)
            {
                connRN.Close();
                connRN.Open();
            }
            else if (connRN.State == ConnectionState.Closed) connRN.Open();
            Object objLastCommAttachmentNo = cmdQueryForLastAttachmentNo.ExecuteScalar();
            if (connRN.State != ConnectionState.Closed) connRN.Close();

            String LastCommAttachmentNo = String.Empty;
            String NewCommAttachmentNo = String.Empty;
            if (objLastCommAttachmentNo != null) LastCommAttachmentNo = objLastCommAttachmentNo.ToString();

            if (LastCommAttachmentNo != String.Empty)
            {
                int nLastCommAttachmentNo = Int32.Parse(LastCommAttachmentNo.Substring(8));
                nLastCommAttachmentNo++;

                NewCommAttachmentNo = "CommAtt-" + nLastCommAttachmentNo.ToString();
            }
            else NewCommAttachmentNo = "CommAtt-1";

            String strUpdateNewCommAttachmentNo = "update [dbo].[tbl_LastID] set [dbo].[tbl_LastID].[CommAttachmentNo] = @NewCommAttachmentNo where [dbo].[tbl_LastID].[Id] = 1";

            SqlCommand cmdUpdateNewCommAttachmentNo = new SqlCommand(strUpdateNewCommAttachmentNo, connRN);
            cmdUpdateNewCommAttachmentNo.CommandType = CommandType.Text;

            cmdUpdateNewCommAttachmentNo.Parameters.AddWithValue("@NewCommAttachmentNo", NewCommAttachmentNo);

            if (connRN.State != ConnectionState.Closed)
            {
                connRN.Close();
                connRN.Open();
            }
            else if (connRN.State == ConnectionState.Closed) connRN.Open();
            int nCommAttachmentNoUpdated = cmdUpdateNewCommAttachmentNo.ExecuteNonQuery();
            if (connRN.State != ConnectionState.Closed) connRN.Close();

            DataGridViewTextBoxCell txtCommAttNo = new DataGridViewTextBoxCell();
            txtCommAttNo.Value = NewCommAttachmentNo;
            row.Cells.Add(txtCommAttNo);
            row.Cells[1].ReadOnly = true;

            DataGridViewButtonCell btnUploadCell = new DataGridViewButtonCell();
            btnUploadCell.Value = "Attach";
            row.Cells.Add(btnUploadCell);
            row.Cells[2].ReadOnly = false;

            DataGridViewTextBoxCell txtDestinationPathCell = new DataGridViewTextBoxCell();
            txtDestinationPathCell.Value = String.Empty;
            row.Cells.Add(txtDestinationPathCell);
            row.Cells[3].ReadOnly = true;

            DataGridViewButtonCell btnViewCell = new DataGridViewButtonCell();
            btnViewCell.Value = "View";
            row.Cells.Add(btnViewCell);
            row.Cells[4].ReadOnly = false;

            String strSqlQueryForStaffName = "select [dbo].[tbl_user].[User_Name] from [dbo].[tbl_user] where [dbo].[tbl_user].[User_Id] = @LoggedInUserId";

            SqlCommand cmdQueryForStaffName = new SqlCommand(strSqlQueryForStaffName, connRN);
            cmdQueryForStaffName.CommandType = CommandType.Text;

            cmdQueryForStaffName.Parameters.AddWithValue("@LoggedInUserId", nLoggedInUserId);

            if (connRN.State != ConnectionState.Closed)
            {
                connRN.Close();
                connRN.Open();
            }
            else if (connRN.State == ConnectionState.Closed) connRN.Open();
            Object objStaffName = cmdQueryForStaffName.ExecuteScalar();
            if (connRN.State != ConnectionState.Closed) connRN.Close();

            String StaffName = String.Empty;
            if (objStaffName != null) StaffName = objStaffName.ToString();

            if (StaffName != String.Empty)
            {
                DataGridViewTextBoxCell txtCreatingStaffName = new DataGridViewTextBoxCell();
                txtCreatingStaffName.Value = StaffName;
                row.Cells.Add(txtCreatingStaffName);
                row.Cells[5].ReadOnly = true;
            }

            DataGridViewTextBoxCell txtCreateDateCell = new DataGridViewTextBoxCell();
            txtCreateDateCell.Value = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            row.Cells.Add(txtCreateDateCell);
            row.Cells[6].ReadOnly = true;

            gvCommunicationAttachment.Rows.Add(row);
        }

        private void gvCommunicationAttachment_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (OpenMode == CommunicationOpenMode.ReadOnly && e.ColumnIndex == 2) return;

            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 2)
                {
                    OpenFileDialog dlgUpload = new OpenFileDialog();

                    if (dlgUpload.ShowDialog() == DialogResult.OK)
                    {
                        String CommunicationAttachmentDestinationFileName = CommunicationAttachmentDestinationPath + Path.GetFileName(dlgUpload.FileName);
                        try
                        {
                            File.Copy(dlgUpload.FileName, CommunicationAttachmentDestinationFileName, true);
                            gvCommunicationAttachment[3, e.RowIndex].Value = Path.GetFileName(dlgUpload.FileName);
                            gvCommunicationAttachment[3, e.RowIndex].ReadOnly = true;

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    else return;
                }

                if (e.ColumnIndex == 4)
                {
                    String CommunicationAttachmentFileName = String.Empty;
                    CommunicationAttachmentFileName = gvCommunicationAttachment[3, e.RowIndex]?.Value?.ToString();

                    if (CommunicationAttachmentFileName != String.Empty)
                    {
                        ProcessStartInfo info = new ProcessStartInfo();
                        info.FileName = CommunicationAttachmentDestinationPath + CommunicationAttachmentFileName;

                        Process.Start(info);
                    }
                    else
                    {
                        MessageBox.Show("The destination path is empty.", "Error");
                        return;
                    }
                }
            }
        }
    }
}
