using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;

namespace CMMManager
{
    public partial class frmCreateSendEmail : Form
    {
        private String IndividualId;
        private int? nUserId = null;
        private int? nDepartmentId = null;
        private String UserName = null;
        private String UserEmail = null;
        private String IndividualEmail = null;
        private String IndividualName = null;

        private SqlConnection connRN;
        private String connStringRN;

        private SqlConnection connSalesForce;
        private String connStringSalesForce;

        private MimeMessage email_message;
        private EmailContentInfo EmailContent;

        private List<String> lstAttachments;

        // Shared folder for email attachments
        private String strPathForEmailAttachments = @"\\cmm-2014u\Sharefolder\EmailAttachments\";

        public frmCreateSendEmail()
        {
            InitializeComponent();
            IndividualId = null;
        }

        public frmCreateSendEmail(String individual_id, int user_id, int department_id, String user_name, String user_email, String individual_email, String individual_name)
        {
            InitializeComponent();

            connStringRN = @"Data Source=CMM-2014U\CMM; Initial Catalog=RN_DB;Integrated Security=True; Max Pool Size=200; MultipleActiveResultSets=True";
            connStringSalesForce = @"Data Source=CMM-2014U\CMM; Initial Catalog=SalesForce; Integrated Security=True; MultipleActiveResultSets=True";

            connRN = new SqlConnection(connStringRN);
            connSalesForce = new SqlConnection(connStringSalesForce);

            IndividualId = individual_id;
            nUserId = user_id;
            nDepartmentId = department_id;
            UserName = user_name;
            UserEmail = user_email;
            IndividualEmail = individual_email;
            IndividualName = individual_name;

            //email_message = message;

            EmailContent = new EmailContentInfo();
            //EmailContent.EmailSender = message.Sender.ToString();
            //EmailContent.EmailRecipient = message.To.ToString();
            //EmailContent.EmailSubject = message.Subject;
            //EmailContent.EmailBody = message.Body.ToString();
        }

        private void frmCreateSendEmail_Load(object sender, EventArgs e)
        {
            if (IndividualId == null)
            {
                MessageBox.Show("Individual Id is null.", "Error");
                return;
            }
        
            if (nUserId == null)
            {
                MessageBox.Show("User ID is null", "Error");
                return;
            }

            if (nDepartmentId == null)
            {
                MessageBox.Show("Department ID is null", "Error");
                return;
            }

            if (UserName == null)
            {
                MessageBox.Show("User Name is null", "Error");
                return;
            }

            if (UserEmail == null)
            {
                MessageBox.Show("User Email is null", "Error");
                return;
            }

            String strSqlQueryForCases = "select [dbo].[tbl_case].[Case_Name] from [dbo].[tbl_case] " +
                                                "inner join [dbo].[tbl_case_status_code] on [dbo].[tbl_case].[Case_status] = [dbo].[tbl_case_status_code].[CaseStatusCode]" +
                                                "where [dbo].[tbl_case].[individual_id] = @IndividualId and " +
                                                "([dbo].[tbl_case].[IsDeleted] = 0 or [dbo].[tbl_case].[IsDeleted] IS NULL)";

            SqlCommand cmdQueryForCases = new SqlCommand(strSqlQueryForCases, connRN);
            cmdQueryForCases.CommandType = CommandType.Text;

            cmdQueryForCases.Parameters.AddWithValue("@IndividualId", IndividualId);

            if (connRN.State != ConnectionState.Closed)
            {
                connRN.Close();
                connRN.Open();
            }
            else if (connRN.State == ConnectionState.Closed) connRN.Open();
            SqlDataReader rdrCase = cmdQueryForCases.ExecuteReader();
            if (rdrCase.HasRows)
            {
                comboCase.Items.Add(String.Empty);
                while (rdrCase.Read())
                {
                    if (!rdrCase.IsDBNull(0)) comboCase.Items.Add(rdrCase.GetString(0));
                }
            }
            rdrCase.Close();
            if (connRN.State != ConnectionState.Closed) connRN.Close();

            comboEmailFrom.Items.Add("npd@cmmlogos.org");
            comboEmailFrom.Items.Add(UserEmail);

            comboEmailFrom.SelectedIndex = 0;
            txtEmailTo.Text = IndividualEmail;
            txtEmailBcc.Text = UserEmail;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSendEmail_Click(object sender, EventArgs e)
        {
            try
            {
                MimeMessage message = new MimeMessage();

                if (comboEmailFrom.SelectedIndex == 0)
                {
                    message.From.Add(new MailboxAddress("Needs Processing Department", comboEmailFrom.SelectedItem.ToString()));
                }
                else if (comboEmailFrom.SelectedIndex > 0)
                {
                    message.From.Add(new MailboxAddress(UserName, comboEmailFrom.SelectedItem.ToString()));
                }

                if (txtEmailTo.Text.Trim() != String.Empty && IndividualName != null)
                {
                    message.To.Add(new MailboxAddress(IndividualName, txtEmailTo.Text.Trim()));
                    //message.To.Add(new MailboxAddress("Hyung Park", "harrispark09@gmail.com"));
                }

                message.Subject = txtEmailSubject.Text;

                String[] EmailBcc = txtEmailBcc.Text.Trim().Split(';');
                for (int i = 0; i < EmailBcc.Length; i++)
                {
                    if (EmailBcc[i].Trim() != String.Empty) message.Bcc.Add(new MailboxAddress("CMM Staff", EmailBcc[i].Trim()));
                }

                BodyBuilder emailBuilderBody = new BodyBuilder();
                emailBuilderBody.TextBody = txtEmailBody.Text;

                foreach (String filename in lbAttachments.Items)
                {
                    emailBuilderBody.Attachments.Add(filename);
                }

                message.Body = emailBuilderBody.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    client.SslProtocols = System.Security.Authentication.SslProtocols.Tls12;
                    client.Connect("mail.cmmlogos.org", 587, MailKit.Security.SecureSocketOptions.StartTls);

                    client.Authenticate("npd@cmmlogos.org", "Logos@5235");

                    client.Send(message);
                    client.Disconnect(true);
                }

                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ///
                if (comboEmailFrom.SelectedItem != null) EmailContent.EmailSender = comboEmailFrom.SelectedItem.ToString().Trim();
                EmailContent.EmailRecipient = txtEmailTo.Text.Trim();
                EmailContent.EmailSubject = txtEmailSubject.Text;
                EmailContent.EmailBody = txtEmailBody.Text;

                SaveEmailContent(message);
                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //return;
            }
            finally
            {
                Close();
            }
        }

        private void SaveEmailContent(MimeMessage message)
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

            String CaseNameForEmail = String.Empty;
            String IllnessNameForEmail = String.Empty;
            String IncidentNameForEmail = String.Empty;

            if (comboCase.SelectedItem != null) CaseNameForEmail = comboCase.SelectedItem.ToString();
            if (comboIllness.SelectedItem != null) IllnessNameForEmail = comboIllness.SelectedItem.ToString();
            if (comboIncident.SelectedItem != null) IncidentNameForEmail = comboIncident.SelectedItem.ToString();

            String strSqlInsertNewCommunication = "insert into [dbo].[tbl_Communication] ([dbo].[tbl_Communication].[Individual_Id], " +
                                                  "[dbo].[tbl_Communication].[CommunicationNo], [dbo].[tbl_Communication].[CommunicationType], " +
                                                  "[dbo].[tbl_Communication].[CaseNo], [dbo].[tbl_Communication].[IllnessNo], [dbo].[tbl_Communication].[IncidentNo], " +
                                                  "[dbo].[tbl_Communication].[EmailSender], [dbo].[tbl_Communication].[EmailRecipient], " +
                                                  "[dbo].[tbl_Communication].[Subject], [dbo].[tbl_Communication].[Body], " +
                                                  "[dbo].[tbl_Communication].[CreateDate], [dbo].[tbl_Communication].[CreatedBy]) " +
                                                  "values (@IndividualId, @CommunicationNo, @CommunicationType, @CaseNo, @IllnessNo, @IncidentNo, @EmailSender, @EmailRecipient, @Subject, @Body, @CreateDate, @CreatedBy)";

            SqlCommand cmdInsertNewCommunication = new SqlCommand(strSqlInsertNewCommunication, connRN);
            cmdInsertNewCommunication.CommandType = CommandType.Text;

            cmdInsertNewCommunication.Parameters.AddWithValue("@IndividualId", IndividualId);
            cmdInsertNewCommunication.Parameters.AddWithValue("@CommunicationNo", NewCommunicationNo);
            cmdInsertNewCommunication.Parameters.AddWithValue("@CommunicationType", CommunicationType.EmailSent);
            if (CaseNameForEmail != String.Empty) cmdInsertNewCommunication.Parameters.AddWithValue("@CaseNo", CaseNameForEmail);
            else cmdInsertNewCommunication.Parameters.AddWithValue("@CaseNo", DBNull.Value);
            if (IllnessNameForEmail != String.Empty) cmdInsertNewCommunication.Parameters.AddWithValue("@IllnessNo", IllnessNameForEmail);
            else cmdInsertNewCommunication.Parameters.AddWithValue("@IllnessNo", DBNull.Value);
            if (IncidentNameForEmail != String.Empty) cmdInsertNewCommunication.Parameters.AddWithValue("@IncidentNo", IncidentNameForEmail);
            else cmdInsertNewCommunication.Parameters.AddWithValue("@IncidentNo", DBNull.Value);
            cmdInsertNewCommunication.Parameters.AddWithValue("@EmailSender", EmailContent.EmailSender);
            cmdInsertNewCommunication.Parameters.AddWithValue("@EmailRecipient", EmailContent.EmailRecipient);
            cmdInsertNewCommunication.Parameters.AddWithValue("@Subject", EmailContent.EmailSubject);
            cmdInsertNewCommunication.Parameters.AddWithValue("@Body", EmailContent.EmailBody);

            cmdInsertNewCommunication.Parameters.AddWithValue("@CreateDate", DateTime.Now);
            cmdInsertNewCommunication.Parameters.AddWithValue("@CreatedBy", nUserId);

            if (connRN.State != ConnectionState.Closed)
            {
                connRN.Close();
                connRN.Open();
            }
            else if (connRN.State == ConnectionState.Closed) connRN.Open();
            int nCommunicationInserted = cmdInsertNewCommunication.ExecuteNonQuery();
            if (connRN.State != ConnectionState.Closed) connRN.Close();

            List<String> lstAttachmentFiles = new List<string>();

            if (nCommunicationInserted == 1)
            {
                foreach (String file_path in lbAttachments.Items)
                {
                    lstAttachmentFiles.Add(Path.GetFileName(file_path));
                }

                foreach (String filename in lstAttachmentFiles)
                {
                    //EmailContent.lstEmailAttachmentFileNames.Add(strPathForEmailAttachments + DateTime.Now.ToString("MM-dd-yyyy HH-mm-ss") + "_" + filename);
                    EmailContent.lstEmailAttachmentFileNames.Add(strPathForEmailAttachments + filename);
                }

                for (int i = 0; i < EmailContent.lstEmailAttachmentFileNames.Count; i++)
                {
                    String strSqlInsertEmailAttachment = "insert into [dbo].[tbl_EmailAttachment] " +
                                                            "([dbo].[tbl_EmailAttachment].[CommunicationNo], [dbo].[tbl_EmailAttachment].[EmailAttachmentFilePath]) " +
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

                //for (int i = 0; i < EmailContent.lstEmailAttachmentFileNames.Count; i++)
                //{
                //    File.Copy(lbAttachments.Items[i].ToString(), EmailContent.lstEmailAttachmentFileNames[i], true);
                //}

                foreach (String filename in lbAttachments.Items)
                {
                    File.Copy(filename, strPathForEmailAttachments + Path.GetFileName(filename), true);
                }
            }
        }

        private void comboCase_SelectedIndexChanged(object sender, EventArgs e)
        {
            String CaseSelected = null;

            if (comboCase.SelectedIndex != -1)
            {
                CaseSelected = comboCase.SelectedItem.ToString();
            }
            else return;

            if (comboCase.SelectedIndex == 0)
            {
                comboIllness.SelectedIndex = -1;
                comboIllness.Items.Clear();

                comboIncident.SelectedIndex = -1;
                comboIncident.Items.Clear();
            }

            comboIllness.Items.Clear();

            if (CaseSelected != null)
            {
                String strSqlQueryForIllnessForCase = "select [dbo].[tbl_illness].[IllnessNo] from [dbo].[tbl_illness] " +
                                                      "where [dbo].[tbl_illness].[Case_Id] = @CaseSelected and " +
                                                      "[dbo].[tbl_illness].[Individual_Id] = @IndividualId and " +
                                                      "([dbo].[tbl_illness].[IsDeleted] = 0 or [dbo].[tbl_illness].[IsDeleted] IS NULL)";

                SqlCommand cmdQueryForIllnessForCase = new SqlCommand(strSqlQueryForIllnessForCase, connRN);
                cmdQueryForIllnessForCase.CommandType = CommandType.Text;

                cmdQueryForIllnessForCase.Parameters.AddWithValue("@CaseSelected", CaseSelected);
                cmdQueryForIllnessForCase.Parameters.AddWithValue("@IndividualId", IndividualId);

                if (connRN.State != ConnectionState.Closed)
                {
                    connRN.Close();
                    connRN.Open();
                }
                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                SqlDataReader rdrIllness = cmdQueryForIllnessForCase.ExecuteReader();
                if (rdrIllness.HasRows)
                {
                    comboIllness.Items.Add(String.Empty);
                    while (rdrIllness.Read())
                    {
                        if (!rdrIllness.IsDBNull(0)) comboIllness.Items.Add(rdrIllness.GetString(0));
                    }
                }
                rdrIllness.Close();
                if (connRN.State != ConnectionState.Closed) connRN.Close();
            }
        }

        private void comboIllness_SelectedIndexChanged(object sender, EventArgs e)
        {
            String IllnessNoSelected = null;

            if (comboIllness.SelectedIndex != -1)
            {
                IllnessNoSelected = comboIllness.SelectedItem.ToString();
            }
            else return;

            if (comboIllness.SelectedIndex == 0)
            {
                comboIncident.SelectedIndex = -1;
                comboIncident.Items.Clear();
            }

            comboIncident.Items.Clear();

            if (IllnessNoSelected != null)
            {
                String strSqlQueryForIllnessId = "select [dbo].[tbl_illness].[Illness_Id] from [dbo].[tbl_illness] " +
                                                 "where [dbo].[tbl_illness].[IllnessNo] = @IllnessNo and " +
                                                 "[dbo].[tbl_illness].[Case_Id] = @CaseNo and " +
                                                 "[dbo].[tbl_illness].[Individual_Id] = @IndividualId and " +
                                                 "([dbo].[tbl_illness].[IsDeleted] = 0 or [dbo].[tbl_illness].[IsDeleted] IS NULL)";

                SqlCommand cmdQueryForIllnessId = new SqlCommand(strSqlQueryForIllnessId, connRN);
                cmdQueryForIllnessId.CommandType = CommandType.Text;

                cmdQueryForIllnessId.Parameters.AddWithValue("@IllnessNo", IllnessNoSelected);
                cmdQueryForIllnessId.Parameters.AddWithValue("@CaseNo", comboCase.SelectedItem.ToString());
                cmdQueryForIllnessId.Parameters.AddWithValue("@IndividualId", IndividualId);

                if (connRN.State != ConnectionState.Closed)
                {
                    connRN.Close();
                    connRN.Open();
                }
                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                Object objIllnessId = cmdQueryForIllnessId.ExecuteScalar();
                if (connRN.State != ConnectionState.Closed) connRN.Close();

                int? nIllnessId = null;

                if (objIllnessId != null) nIllnessId = Int32.Parse(objIllnessId.ToString());

                if (nIllnessId != null)
                {
                    String strSqlQueryForIncidentNo = "select [dbo].[tbl_incident].[IncidentNo] from [dbo].[tbl_incident] " +
                                                      "where [dbo].[tbl_incident].[Illness_id] = @IllnessId and " +
                                                      "[dbo].[tbl_incident].[Case_Id] = @CaseNo and " +
                                                      "[dbo].[tbl_incident].[Individual_Id] = @IndividualId and " +
                                                      "([dbo].[tbl_incident].[IsDeleted] = 0 or [dbo].[tbl_incident].[IsDeleted] IS NULL)";

                    SqlCommand cmdQueryForIncidentNo = new SqlCommand(strSqlQueryForIncidentNo, connRN);
                    cmdQueryForIncidentNo.CommandType = CommandType.Text;

                    cmdQueryForIncidentNo.Parameters.AddWithValue("@IllnessId", nIllnessId.Value);
                    cmdQueryForIncidentNo.Parameters.AddWithValue("@CaseNo", comboCase.SelectedItem.ToString());
                    cmdQueryForIncidentNo.Parameters.AddWithValue("@IndividualId", IndividualId);

                    if (connRN.State != ConnectionState.Closed)
                    {
                        connRN.Close();
                        connRN.Open();
                    }
                    else if (connRN.State == ConnectionState.Closed) connRN.Open();
                    SqlDataReader rdrIncidentNo = cmdQueryForIncidentNo.ExecuteReader();
                    if (rdrIncidentNo.HasRows)
                    {
                        comboIncident.Items.Add(String.Empty);
                        while (rdrIncidentNo.Read())
                        {
                            if (!rdrIncidentNo.IsDBNull(0)) comboIncident.Items.Add(rdrIncidentNo.GetString(0));
                        }
                    }
                    rdrIncidentNo.Close();
                    if (connRN.State != ConnectionState.Closed) connRN.Close();
                }
            }
        }

        private void btnAttachFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDlg = new OpenFileDialog();

            openFileDlg.Filter = "PDF Files (*.pdf)|*.pdf|All files (*.*)|*.*";
            openFileDlg.FilterIndex = 1;
            openFileDlg.RestoreDirectory = true;

            if (openFileDlg.ShowDialog() == DialogResult.OK)
            {
                String attachmentFilePath = openFileDlg.FileName;
                lbAttachments.Items.Add(attachmentFilePath);
            }
        }

        private void btnDeleteAttachment_Click(object sender, EventArgs e)
        {
            if (lbAttachments.SelectedIndex != -1)
            {
                lbAttachments.Items.RemoveAt(lbAttachments.SelectedIndex);
            }
        }

        private void btnEmailBcc_Click(object sender, EventArgs e)
        {
            frmAddEmailBcc emailBcc = new frmAddEmailBcc(UserEmail, txtEmailBcc.Text);

            if (emailBcc.ShowDialog() == DialogResult.OK)
            {
                //txtEmailBcc.Text += emailBcc.EmailBcc;
                txtEmailBcc.Text = UserEmail + "; " + emailBcc.EmailBcc;
            }
        }

        private void lbAttachments_DoubleClick(object sender, EventArgs e)
        {
            if (lbAttachments.SelectedItem != null)
            {
                System.Diagnostics.ProcessStartInfo processInfo = new System.Diagnostics.ProcessStartInfo();
                processInfo.FileName = lbAttachments.SelectedItem.ToString();

                System.Diagnostics.Process.Start(processInfo);
            }
        }
    }
}
