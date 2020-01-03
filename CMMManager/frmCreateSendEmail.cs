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

        private List<String> lstAttachments;

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
            txtEmailBCC.Text = UserEmail;
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
                }

                message.Subject = txtEmailSubject.Text;

                //String[] EmailCC = txtEmailCC.Text.Trim().Split(';');

                InternetAddressList list = new InternetAddressList();
                String[] EmailBcc = txtEmailBCC.Text.Trim().Split(';');

                foreach (String email in EmailBcc)
                {
                    list.Add(new MailboxAddress(email.Trim()));
                }

                foreach (MailboxAddress emailBcc in list)
                {
                    message.Bcc.Add(emailBcc);
                }

                //list.Add(new MailboxAddress(txtEmailBCC.Text.Trim()));
                //message.Cc.Add(list[0]);
                

                BodyBuilder emailBuilderBody = new BodyBuilder();
                emailBuilderBody.TextBody = txtEmailBody.Text;
                //emailBuilderBody.Attachments.Add()

                foreach (String filename in lbAttachments.Items)
                {
                    emailBuilderBody.Attachments.Add(filename);
                }

                message.Body = emailBuilderBody.ToMessageBody();

                //message.Body = new TextPart("plain") { Text = txtEmailBody.Text };

                using (var client = new SmtpClient())
                {
                    client.SslProtocols = System.Security.Authentication.SslProtocols.Tls12;
                    client.Connect("mail.cmmlogos.org", 587, MailKit.Security.SecureSocketOptions.StartTls);

                    client.Authenticate("npd@cmmlogos.org", "Logos@5235");

                    client.Send(message);
                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            Close();
        }

        //private void chkOnGoing_CheckedChanged(object sender, EventArgs e)
        //{
        //    comboCase.Items.Clear();

        //    if (chkOnGoing.Checked)
        //    {
        //        String strSqlQueryForOngoingCases = "select [dbo].[tbl_case].[Case_Name] from [dbo].[tbl_case] " +
        //                                            "inner join [dbo].[tbl_case_status_code] on [dbo].[tbl_case].[Case_status] = [dbo].[tbl_case_status_code].[CaseStatusCode]" +
        //                                            "where [dbo].[tbl_case].[individual_id] = @IndividualId and " +
        //                                            "[dbo].[tbl_case_status_code].[CaseStatusValue] = 'On Going' and " +
        //                                            "([dbo].[tbl_case].[IsDeleted] = 0 or [dbo].[tbl_case].[IsDeleted] IS NULL)";

        //        SqlCommand cmdQueryForOnGoingCases = new SqlCommand(strSqlQueryForOngoingCases, connRN);
        //        cmdQueryForOnGoingCases.CommandType = CommandType.Text;

        //        cmdQueryForOnGoingCases.Parameters.AddWithValue("@IndividualId", IndividualId);

        //        if (connRN.State != ConnectionState.Closed)
        //        {
        //            connRN.Close();
        //            connRN.Open();
        //        }
        //        else if (connRN.State == ConnectionState.Closed) connRN.Open();
        //        SqlDataReader rdrOnGoingCase = cmdQueryForOnGoingCases.ExecuteReader();
        //        if (rdrOnGoingCase.HasRows)
        //        {
        //            comboCase.Items.Add(String.Empty);
        //            while (rdrOnGoingCase.Read())
        //            {
        //                if (!rdrOnGoingCase.IsDBNull(0)) comboCase.Items.Add(rdrOnGoingCase.GetString(0));
        //            }
        //        }
        //        rdrOnGoingCase.Close();
        //        if (connRN.State != ConnectionState.Closed) connRN.Close();
        //    }

        //    if (chkProcessing.Checked)
        //    {
        //        String strSqlQueryForProcessingCases = "select [dbo].[tbl_case].[Case_Name] from [dbo].[tbl_case] " +
        //                                                "inner join [dbo].[tbl_case_status_code] on [dbo].[tbl_case].[Case_status] = [dbo].[tbl_case_status_code].[CaseStatusCode]" +
        //                                                "where [dbo].[tbl_case].[individual_id] = @IndividualId and " +
        //                                                "[dbo].[tbl_case_status_code].[CaseStatusValue] = 'Processing' and " +
        //                                                "([dbo].[tbl_case].[IsDeleted] = 0 or [dbo].[tbl_case].[IsDeleted] IS NULL)";

        //        SqlCommand cmdQueryForProcessingCases = new SqlCommand(strSqlQueryForProcessingCases, connRN);
        //        cmdQueryForProcessingCases.CommandType = CommandType.Text;

        //        cmdQueryForProcessingCases.Parameters.AddWithValue("@IndividualId", IndividualId);

        //        if (connRN.State != ConnectionState.Closed)
        //        {
        //            connRN.Close();
        //            connRN.Open();
        //        }
        //        else if (connRN.State == ConnectionState.Closed) connRN.Open();
        //        SqlDataReader rdrProcessingCases = cmdQueryForProcessingCases.ExecuteReader();
        //        if (rdrProcessingCases.HasRows)
        //        {
        //            while (rdrProcessingCases.Read())
        //            {
        //                if (!rdrProcessingCases.IsDBNull(0)) comboCase.Items.Add(rdrProcessingCases.GetString(0));
        //            }
        //        }
        //        rdrProcessingCases.Close();
        //        if (connRN.State != ConnectionState.Closed) connRN.Close();
        //    }

        //    if (chkClosed.Checked)
        //    {
        //        String strSqlQueryForClosed = "select [dbo].[tbl_case].[Case_Name] from [dbo].[tbl_case] " +
        //                                      "inner join [dbo].[tbl_case_status_code] on [dbo].[tbl_case].[Case_status] = [dbo].[tbl_case_status_code].[CaseStatusCode]" +
        //                                      "where [dbo].[tbl_case].[individual_id] = @IndividualId and " +
        //                                      "[dbo].[tbl_case_status_code].[CaseStatusValue] = 'Closed' and " +
        //                                      "([dbo].[tbl_case].[IsDeleted] = 0 or [dbo].[tbl_case].[IsDeleted] IS NULL)";

        //        SqlCommand cmdQueryForClosed = new SqlCommand(strSqlQueryForClosed, connRN);
        //        cmdQueryForClosed.CommandType = CommandType.Text;

        //        cmdQueryForClosed.Parameters.AddWithValue("@IndividualId", IndividualId);

        //        if (connRN.State != ConnectionState.Closed)
        //        {
        //            connRN.Close();
        //            connRN.Open();
        //        }
        //        else if (connRN.State == ConnectionState.Closed) connRN.Open();
        //        SqlDataReader rdrCasesClosed = cmdQueryForClosed.ExecuteReader();
        //        if (rdrCasesClosed.HasRows)
        //        {
        //            while (rdrCasesClosed.Read())
        //            {
        //                if (!rdrCasesClosed.IsDBNull(0)) comboCase.Items.Add(rdrCasesClosed.GetString(0));
        //            }
        //        }
        //        if (connRN.State != ConnectionState.Closed) connRN.Close();
        //    }

        //    if (chkOnGoing.Checked || chkProcessing.Checked || chkClosed.Checked)
        //    {
        //        if (comboCase.Items[0].ToString() != String.Empty) comboCase.Items.Insert(0, String.Empty);
        //    }
        //}

        //private void chkProcessing_CheckedChanged(object sender, EventArgs e)
        //{
        //    comboCase.Items.Clear();

        //    if (chkOnGoing.Checked)
        //    {
        //        String strSqlQueryForOngoingCases = "select [dbo].[tbl_case].[Case_Name] from [dbo].[tbl_case] " +
        //                                            "inner join [dbo].[tbl_case_status_code] on [dbo].[tbl_case].[Case_status] = [dbo].[tbl_case_status_code].[CaseStatusCode]" +
        //                                            "where [dbo].[tbl_case].[individual_id] = @IndividualId and " +
        //                                            "[dbo].[tbl_case_status_code].[CaseStatusValue] = 'On Going' and " +
        //                                            "([dbo].[tbl_case].[IsDeleted] = 0 or [dbo].[tbl_case].[IsDeleted] IS NULL)";

        //        SqlCommand cmdQueryForOnGoingCases = new SqlCommand(strSqlQueryForOngoingCases, connRN);
        //        cmdQueryForOnGoingCases.CommandType = CommandType.Text;

        //        cmdQueryForOnGoingCases.Parameters.AddWithValue("@IndividualId", IndividualId);

        //        if (connRN.State != ConnectionState.Closed)
        //        {
        //            connRN.Close();
        //            connRN.Open();
        //        }
        //        else if (connRN.State == ConnectionState.Closed) connRN.Open();
        //        SqlDataReader rdrOnGoingCase = cmdQueryForOnGoingCases.ExecuteReader();
        //        if (rdrOnGoingCase.HasRows)
        //        {
        //            while (rdrOnGoingCase.Read())
        //            {
        //                if (!rdrOnGoingCase.IsDBNull(0)) comboCase.Items.Add(rdrOnGoingCase.GetString(0));
        //            }
        //        }
        //        rdrOnGoingCase.Close();
        //        if (connRN.State != ConnectionState.Closed) connRN.Close();
        //    }

        //    if (chkProcessing.Checked)
        //    {
        //        String strSqlQueryForProcessingCases = "select [dbo].[tbl_case].[Case_Name] from [dbo].[tbl_case] " +
        //                                                "inner join [dbo].[tbl_case_status_code] on [dbo].[tbl_case].[Case_status] = [dbo].[tbl_case_status_code].[CaseStatusCode]" +
        //                                                "where [dbo].[tbl_case].[individual_id] = @IndividualId and " +
        //                                                "[dbo].[tbl_case_status_code].[CaseStatusValue] = 'Processing' and " +
        //                                                "([dbo].[tbl_case].[IsDeleted] = 0 or [dbo].[tbl_case].[IsDeleted] IS NULL)";

        //        SqlCommand cmdQueryForProcessingCases = new SqlCommand(strSqlQueryForProcessingCases, connRN);
        //        cmdQueryForProcessingCases.CommandType = CommandType.Text;

        //        cmdQueryForProcessingCases.Parameters.AddWithValue("@IndividualId", IndividualId);

        //        if (connRN.State != ConnectionState.Closed)
        //        {
        //            connRN.Close();
        //            connRN.Open();
        //        }
        //        else if (connRN.State == ConnectionState.Closed) connRN.Open();
        //        SqlDataReader rdrProcessingCases = cmdQueryForProcessingCases.ExecuteReader();
        //        if (rdrProcessingCases.HasRows)
        //        {
        //            while (rdrProcessingCases.Read())
        //            {
        //                if (!rdrProcessingCases.IsDBNull(0)) comboCase.Items.Add(rdrProcessingCases.GetString(0));
        //            }
        //        }
        //        rdrProcessingCases.Close();
        //        if (connRN.State != ConnectionState.Closed) connRN.Close();
        //    }

        //    if (chkClosed.Checked)
        //    {
        //        String strSqlQueryForClosed = "select [dbo].[tbl_case].[Case_Name] from [dbo].[tbl_case] " +
        //                                      "inner join [dbo].[tbl_case_status_code] on [dbo].[tbl_case].[Case_status] = [dbo].[tbl_case_status_code].[CaseStatusCode]" +
        //                                      "where [dbo].[tbl_case].[individual_id] = @IndividualId and " +
        //                                      "[dbo].[tbl_case_status_code].[CaseStatusValue] = 'Closed' and " +
        //                                      "([dbo].[tbl_case].[IsDeleted] = 0 or [dbo].[tbl_case].[IsDeleted] IS NULL)";

        //        SqlCommand cmdQueryForClosed = new SqlCommand(strSqlQueryForClosed, connRN);
        //        cmdQueryForClosed.CommandType = CommandType.Text;

        //        cmdQueryForClosed.Parameters.AddWithValue("@IndividualId", IndividualId);

        //        if (connRN.State != ConnectionState.Closed)
        //        {
        //            connRN.Close();
        //            connRN.Open();
        //        }
        //        else if (connRN.State == ConnectionState.Closed) connRN.Open();
        //        SqlDataReader rdrCasesClosed = cmdQueryForClosed.ExecuteReader();
        //        if (rdrCasesClosed.HasRows)
        //        {
        //            while (rdrCasesClosed.Read())
        //            {
        //                if (!rdrCasesClosed.IsDBNull(0)) comboCase.Items.Add(rdrCasesClosed.GetString(0));
        //            }
        //        }
        //        if (connRN.State != ConnectionState.Closed) connRN.Close();
        //    }

        //    if (chkOnGoing.Checked || chkProcessing.Checked || chkClosed.Checked)
        //    {
        //        if (comboCase.Items[0].ToString() != String.Empty) comboCase.Items.Insert(0, String.Empty);
        //    }
        //}

        //private void chkClosed_CheckedChanged(object sender, EventArgs e)
        //{
        //    comboCase.Items.Clear();

        //    if (chkOnGoing.Checked)
        //    {
        //        String strSqlQueryForOngoingCases = "select [dbo].[tbl_case].[Case_Name] from [dbo].[tbl_case] " +
        //                                            "inner join [dbo].[tbl_case_status_code] on [dbo].[tbl_case].[Case_status] = [dbo].[tbl_case_status_code].[CaseStatusCode]" +
        //                                            "where [dbo].[tbl_case].[individual_id] = @IndividualId and " +
        //                                            "[dbo].[tbl_case_status_code].[CaseStatusValue] = 'On Going' and " +
        //                                            "([dbo].[tbl_case].[IsDeleted] = 0 or [dbo].[tbl_case].[IsDeleted] IS NULL)";

        //        SqlCommand cmdQueryForOnGoingCases = new SqlCommand(strSqlQueryForOngoingCases, connRN);
        //        cmdQueryForOnGoingCases.CommandType = CommandType.Text;

        //        cmdQueryForOnGoingCases.Parameters.AddWithValue("@IndividualId", IndividualId);

        //        if (connRN.State != ConnectionState.Closed)
        //        {
        //            connRN.Close();
        //            connRN.Open();
        //        }
        //        else if (connRN.State == ConnectionState.Closed) connRN.Open();
        //        SqlDataReader rdrOnGoingCase = cmdQueryForOnGoingCases.ExecuteReader();
        //        if (rdrOnGoingCase.HasRows)
        //        {
        //            while (rdrOnGoingCase.Read())
        //            {
        //                if (!rdrOnGoingCase.IsDBNull(0)) comboCase.Items.Add(rdrOnGoingCase.GetString(0));
        //            }
        //        }
        //        rdrOnGoingCase.Close();
        //        if (connRN.State != ConnectionState.Closed) connRN.Close();
        //    }

        //    if (chkProcessing.Checked)
        //    {
        //        String strSqlQueryForProcessingCases = "select [dbo].[tbl_case].[Case_Name] from [dbo].[tbl_case] " +
        //                                                "inner join [dbo].[tbl_case_status_code] on [dbo].[tbl_case].[Case_status] = [dbo].[tbl_case_status_code].[CaseStatusCode]" +
        //                                                "where [dbo].[tbl_case].[individual_id] = @IndividualId and " +
        //                                                "[dbo].[tbl_case_status_code].[CaseStatusValue] = 'Processing' and " +
        //                                                "([dbo].[tbl_case].[IsDeleted] = 0 or [dbo].[tbl_case].[IsDeleted] IS NULL)";

        //        SqlCommand cmdQueryForProcessingCases = new SqlCommand(strSqlQueryForProcessingCases, connRN);
        //        cmdQueryForProcessingCases.CommandType = CommandType.Text;

        //        cmdQueryForProcessingCases.Parameters.AddWithValue("@IndividualId", IndividualId);

        //        if (connRN.State != ConnectionState.Closed)
        //        {
        //            connRN.Close();
        //            connRN.Open();
        //        }
        //        else if (connRN.State == ConnectionState.Closed) connRN.Open();
        //        SqlDataReader rdrProcessingCases = cmdQueryForProcessingCases.ExecuteReader();
        //        if (rdrProcessingCases.HasRows)
        //        {
        //            while (rdrProcessingCases.Read())
        //            {
        //                if (!rdrProcessingCases.IsDBNull(0)) comboCase.Items.Add(rdrProcessingCases.GetString(0));
        //            }
        //        }
        //        rdrProcessingCases.Close();
        //        if (connRN.State != ConnectionState.Closed) connRN.Close();
        //    }

        //    if (chkClosed.Checked)
        //    {
        //        String strSqlQueryForClosed = "select [dbo].[tbl_case].[Case_Name] from [dbo].[tbl_case] " +
        //                                      "inner join [dbo].[tbl_case_status_code] on [dbo].[tbl_case].[Case_status] = [dbo].[tbl_case_status_code].[CaseStatusCode]" +
        //                                      "where [dbo].[tbl_case].[individual_id] = @IndividualId and " +
        //                                      "[dbo].[tbl_case_status_code].[CaseStatusValue] = 'Closed' and " +
        //                                      "([dbo].[tbl_case].[IsDeleted] = 0 or [dbo].[tbl_case].[IsDeleted] IS NULL)";

        //        SqlCommand cmdQueryForClosed = new SqlCommand(strSqlQueryForClosed, connRN);
        //        cmdQueryForClosed.CommandType = CommandType.Text;

        //        cmdQueryForClosed.Parameters.AddWithValue("@IndividualId", IndividualId);

        //        if (connRN.State != ConnectionState.Closed)
        //        {
        //            connRN.Close();
        //            connRN.Open();
        //        }
        //        else if (connRN.State == ConnectionState.Closed) connRN.Open();
        //        SqlDataReader rdrCasesClosed = cmdQueryForClosed.ExecuteReader();
        //        if (rdrCasesClosed.HasRows)
        //        {
        //            while (rdrCasesClosed.Read())
        //            {
        //                if (!rdrCasesClosed.IsDBNull(0)) comboCase.Items.Add(rdrCasesClosed.GetString(0));
        //            }
        //        }
        //        if (connRN.State != ConnectionState.Closed) connRN.Close();
        //    }

        //    if (chkOnGoing.Checked || chkProcessing.Checked || chkClosed.Checked)
        //    {
        //        if (comboCase.Items[0].ToString() != String.Empty) comboCase.Items.Insert(0, String.Empty);
        //    }
        //}

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
            frmAddEmailBcc emailBcc = new frmAddEmailBcc();

            if (emailBcc.ShowDialog() == DialogResult.OK)
            {

            }
        }
    }
}
