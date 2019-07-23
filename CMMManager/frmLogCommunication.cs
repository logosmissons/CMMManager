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
    public partial class frmLogCommunication : Form
    {
        public String IndividualId;
        private int nLoggedInUserId;
        private String strConnString;
        private SqlConnection connRN;

        public frmLogCommunication()
        {
            IndividualId = String.Empty;
            strConnString = @"Data Source = 12.230.174.166\cmm; Initial Catalog = RN_DB; Integrated Security = True; Max Pool Size = 200";
            connRN = new SqlConnection(strConnString);

            InitializeComponent();
        }

        public frmLogCommunication(String individual_id, int login_user_id)
        {
            InitializeComponent();

            IndividualId = individual_id;
            nLoggedInUserId = login_user_id;

            strConnString = @"Data Source = 12.230.174.166\cmm; Initial Catalog = RN_DB; Integrated Security = True; Max Pool Size = 200";
            connRN = new SqlConnection(strConnString);
        }

        private void frmLogCommunication_Load(object sender, EventArgs e)
        {
            String strIndividualId = IndividualId;
            txtCommunicationIndividualId.Text = strIndividualId;

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

            if (nNewCommunicationInserted == 1) MessageBox.Show("New communication has been inserted.", "Info");
            else if (nNewCommunicationInserted == 0) MessageBox.Show("New communication insertion has failed.", "Error"); 

            Close();
        }
    }
}
