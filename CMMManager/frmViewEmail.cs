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
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;
using OfficeOutlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;
using System.Reflection;

namespace CMMManager
{
    public partial class frmViewEmail : Form
    {

        private String IndividualId;
        private String IndividualName;
        private String SenderEmail;
        private String RecipientEmail;
        private String CreatingStaff;
        private String CaseNo;
        private int? nLoggedInUserId;
        private DateTime? CreateDate;
        private String Subject;
        private String Body;

        // MailItem
        private OfficeOutlook.MailItem mailItem;
        private OfficeOutlook.Application application;

        // For task creation
        String LoggedInUserName = String.Empty;
        UserRole? LoggedInUserRoleId = null;
        Department? LoggedInUserDepartmentId = null;

        // Shared folder for email attachments
        private String strPathForEmailAttachments = @"\\cmm-2014u\Sharefolder\EmailAttachments\";

        private EmailContentInfo EmailContent;

        private String connStringRN = @"Data Source=CMM-2014U\CMM; Initial Catalog=RN_DB;Integrated Security=True; Max Pool Size=200; MultipleActiveResultSets=True";
        private SqlConnection connRN;

        public frmViewEmail()
        {
            InitializeComponent();
            IndividualId = String.Empty;
            SenderEmail = String.Empty;
            RecipientEmail = String.Empty;
            CaseNo = String.Empty;
            nLoggedInUserId = null;
            CreateDate = null;
            Subject = String.Empty;
            Body = String.Empty;

            LoggedInUserName = String.Empty;
            LoggedInUserRoleId = null;
            LoggedInUserDepartmentId = null;

            EmailContent = new EmailContentInfo();
            connRN = new SqlConnection(connStringRN);
        }

        public frmViewEmail(String individual_id, 
                            String individual_name, 
                            String sender, 
                            String recipient, 
                            String creating_staff, 
                            int login_user, 
                            DateTime create_date, 
                            String case_no, 
                            String subject, 
                            String body)
        {
            InitializeComponent();
            IndividualId = individual_id;
            IndividualName = individual_name;
            SenderEmail = sender;
            RecipientEmail = recipient;
            CreatingStaff = creating_staff;
            nLoggedInUserId = login_user;
            CreateDate = create_date;
            CaseNo = case_no;
            Subject = subject;
            Body = body;

            LoggedInUserName = String.Empty;
            LoggedInUserRoleId = null;
            LoggedInUserDepartmentId = null;

            EmailContent = new EmailContentInfo();
            connRN = new SqlConnection(connStringRN);
        }

        private void frmViewEmail_Load(object sender, EventArgs e)
        {
            txtSender.Text = SenderEmail;
            txtRecipient.Text = RecipientEmail;
            txtCreatedBy.Text = CreatingStaff;
            txtCaseNo.Text = CaseNo;
            txtCreateDate.Text = CreateDate.Value.ToString("MM/dd/yyyy HH:mm:ss");
            txtSubject.Text = Subject;
            txtBody.Text = Body;

            if (nLoggedInUserId != null)
            {
                String strSqlQueryForLoggedInUserInfo = "select [dbo].[tbl_user].[User_Name], [dbo].[tbl_user].[User_Role_Id], [dbo].[tbl_user].[Department_Id] from [dbo].[tbl_user] " +
                                                        "where [dbo].[tbl_user].[User_Id] = @LoggedInUserId";

                SqlCommand cmdQueryForLoggedInUserInfo = new SqlCommand(strSqlQueryForLoggedInUserInfo, connRN);
                cmdQueryForLoggedInUserInfo.CommandType = CommandType.Text;

                cmdQueryForLoggedInUserInfo.Parameters.AddWithValue("@LoggedInUserId", nLoggedInUserId);

                if (connRN.State != ConnectionState.Closed)
                {
                    connRN.Close();
                    connRN.Open();
                }
                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                SqlDataReader rdrLoggedInUserInfo = cmdQueryForLoggedInUserInfo.ExecuteReader();
                if (rdrLoggedInUserInfo.HasRows)
                {
                    rdrLoggedInUserInfo.Read();
                    if (!rdrLoggedInUserInfo.IsDBNull(0)) LoggedInUserName = rdrLoggedInUserInfo.GetString(0);
                    if (!rdrLoggedInUserInfo.IsDBNull(1)) LoggedInUserRoleId = (UserRole)rdrLoggedInUserInfo.GetInt16(1);
                    if (!rdrLoggedInUserInfo.IsDBNull(2)) LoggedInUserDepartmentId = (Department)rdrLoggedInUserInfo.GetInt16(2);
                }
                rdrLoggedInUserInfo.Close();
                if (connRN.State != ConnectionState.Closed) connRN.Close();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnReply_Click(object sender, EventArgs e)
        {
            String strMemberEmail = txtSender.Text.Trim();

            strMemberEmail = "harrispark009@gmail.com";

            application = new OfficeOutlook.Application();
            mailItem = application.CreateItem(OfficeOutlook.OlItemType.olMailItem);

            ((OfficeOutlook.ItemEvents_10_Event)mailItem).Send += new OfficeOutlook.ItemEvents_10_SendEventHandler(SaveEmailContent);

            mailItem.To = strMemberEmail;
            OfficeOutlook.NameSpace outlookNamespace = application.GetNamespace("MAPI");

            mailItem.Display(true);

            try
            {
                outlookNamespace.SendAndReceive(false);
                if (outlookNamespace != null) Marshal.ReleaseComObject(outlookNamespace);
            }
            catch (Exception ex)
            {

            }

        }

        private void SaveEmailContent(ref bool Cancel)
        {

            String strSqlQueryForLastCommNo = "select [dbo].[tbl_LastID].[CommunicationNo] from [dbo].[tbl_LastID] where [dbo].[tbl_LastID].[Id] = 1";

            SqlCommand cmdQueryForLastCommNo = new SqlCommand(strSqlQueryForLastCommNo, connRN);
            cmdQueryForLastCommNo.CommandType = CommandType.Text;

            if (connRN.State != ConnectionState.Closed)
            {
                connRN.Close();
                connRN.Open();
            }
            else if (connRN.State == ConnectionState.Closed) connRN.Open();
            Object objCommunicationNo = cmdQueryForLastCommNo.ExecuteScalar();
            if (connRN.State != ConnectionState.Closed) connRN.Close();

            String strCommunicationNo = String.Empty;
            if (objCommunicationNo != null) strCommunicationNo = objCommunicationNo.ToString();

            String NewCommunicationNo = String.Empty;
            if (strCommunicationNo == String.Empty) NewCommunicationNo = "Comm-1";
            else
            {
                String strCommNoNumber = strCommunicationNo.Substring(5);
                int nCommNo = Int32.Parse(strCommNoNumber);
                nCommNo++;

                NewCommunicationNo = "Comm-" + nCommNo.ToString();
            }

            String strSqlUpdateLastCommNo = "update [dbo].[tbl_LastID] set [dbo].[tbl_LastID].[CommunicationNo] = @LastCommNo where [dbo].[tbl_LastID].[Id] = 1";

            SqlCommand cmdUpdateLastCommNo = new SqlCommand(strSqlUpdateLastCommNo, connRN);
            cmdUpdateLastCommNo.CommandType = CommandType.Text;

            cmdUpdateLastCommNo.Parameters.AddWithValue("@LastCommNo", NewCommunicationNo);

            if (connRN.State != ConnectionState.Closed)
            {
                connRN.Close();
                connRN.Open();
            }
            else if (connRN.State == ConnectionState.Closed) connRN.Open();
            int nLastCommNoUpdated = cmdUpdateLastCommNo.ExecuteNonQuery();
            if (connRN.State != ConnectionState.Closed) connRN.Close();

            //String IndividualId = txtIndividualID.Text.Trim();

            EmailContent.EmailSubject = mailItem.Subject;
            EmailContent.EmailBody = mailItem.Body;

            String strSqlInsertNewCommunication = "insert into [dbo].[tbl_Communication] ([dbo].[tbl_Communication].[Individual_Id], " +
                                                  "[dbo].[tbl_Communication].[CommunicationNo], [dbo].[tbl_Communication].[CommunicationType], " +
                                                  "[dbo].[tbl_Communication].[CaseNo], [dbo].[tbl_Communication].[Subject], [dbo].[tbl_Communication].[Body], " +
                                                  "[dbo].[tbl_Communication].[CreateDate], [dbo].[tbl_Communication].[CreatedBy]) " +
                                                  "values (@IndividualId, @CommunicationNo, @CommunicationType, @CaseNo, @Subject, @Body, @CreateDate, @CreatedBy)";

            SqlCommand cmdInsertNewCommunication = new SqlCommand(strSqlInsertNewCommunication, connRN);
            cmdInsertNewCommunication.CommandType = CommandType.Text;

            cmdInsertNewCommunication.Parameters.AddWithValue("@IndividualId", IndividualId);
            cmdInsertNewCommunication.Parameters.AddWithValue("@CommunicationNo", NewCommunicationNo);
            cmdInsertNewCommunication.Parameters.AddWithValue("@CommunicationType", CommunicationType.EmailSent);
            if (CaseNo != "None") cmdInsertNewCommunication.Parameters.AddWithValue("@CaseNo", CaseNo);
            else cmdInsertNewCommunication.Parameters.AddWithValue("@CaseNo", DBNull.Value);
            cmdInsertNewCommunication.Parameters.AddWithValue("@Subject", EmailContent.EmailSubject);
            cmdInsertNewCommunication.Parameters.AddWithValue("@Body", EmailContent.EmailBody);

            cmdInsertNewCommunication.Parameters.AddWithValue("@CreateDate", DateTime.Now);
            cmdInsertNewCommunication.Parameters.AddWithValue("@CreatedBy", nLoggedInUserId);

            if (connRN.State != ConnectionState.Closed)
            {
                connRN.Close();
                connRN.Open();
            }
            else if (connRN.State == ConnectionState.Closed) connRN.Open();
            int nCommunicationInserted = cmdInsertNewCommunication.ExecuteNonQuery();
            if (connRN.State != ConnectionState.Closed) connRN.Close();

            if (nCommunicationInserted == 1)
            {
                for (int i = 1; i <= mailItem.Attachments.Count; i++) EmailContent.lstEmailAttachmentFileNames.Add(strPathForEmailAttachments + mailItem.Attachments[i].FileName);

                for (int i = 0; i < EmailContent.lstEmailAttachmentFileNames.Count; i++)
                {

                    String strSqlInsertEmailAttachment = "insert into [dbo].[tbl_EmailAttachment] ([dbo].[tbl_EmailAttachment].[CommunicationNo], [dbo].[tbl_EmailAttachment].[CommFilePath]) " +
                                                            "values (@CommunicationNo, @CommunicationFilePath)";

                    SqlCommand cmdInsertEmailAttachment = new SqlCommand(strSqlInsertEmailAttachment, connRN);
                    cmdInsertEmailAttachment.CommandType = CommandType.Text;

                    cmdInsertEmailAttachment.Parameters.AddWithValue("@CommunicationNo", NewCommunicationNo);
                    cmdInsertEmailAttachment.Parameters.AddWithValue("@CommunicationFilePath", EmailContent.lstEmailAttachmentFileNames[i]);

                    if (connRN.State != ConnectionState.Closed)
                    {
                        connRN.Close();
                        connRN.Open();
                    }
                    else if (connRN.State == ConnectionState.Closed) connRN.Open();
                    int nEmailAttachmentSaved = cmdInsertEmailAttachment.ExecuteNonQuery();
                    if (connRN.State != ConnectionState.Closed) connRN.Close();
                }
            }

            EmailContent.EmailSubject = mailItem.Subject;
            EmailContent.EmailBody = mailItem.Body;

            for (int i = 1; i <= mailItem.Attachments.Count; i++)
            {
                mailItem.Attachments[i].SaveAsFile(strPathForEmailAttachments + mailItem.Attachments[i].FileName);
            }
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            application = new OfficeOutlook.Application();
            mailItem = application.CreateItem(OfficeOutlook.OlItemType.olMailItem);

            OfficeOutlook.NameSpace outlookNamespace = application.GetNamespace("MAPI");

            mailItem.Display(true);

            try
            {
                outlookNamespace.SendAndReceive(false);
                if (outlookNamespace != null) Marshal.ReleaseComObject(outlookNamespace);
            }
            catch (Exception ex)
            {

            }
        }

        private void btnCreateTask_Click(object sender, EventArgs e)
        {
            String email_case = txtCaseNo.Text.Trim();
            String email_subject = txtSubject.Text.Trim();
            String email_body = txtBody.Text.Trim();

            frmTaskCreationPage frmTaskCreation = new frmTaskCreationPage(IndividualId,
                                                                          IndividualName,
                                                                          (int)nLoggedInUserId,
                                                                          LoggedInUserName,
                                                                          (UserRole)LoggedInUserRoleId,
                                                                          (Department)LoggedInUserDepartmentId,
                                                                          TaskMode.AddNew,
                                                                          email_case,
                                                                          email_subject,
                                                                          email_body);

            frmTaskCreation.Show();
          
        }
    }
}
