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
    public partial class frmAddEmailBcc : Form
    {
        public String EmailBcc = null;
        private String EmailBccDefault = null;
        private String EmailBccLoaded = null;

        private String connStringRN;
        private String connStringSalesForce;
        private SqlConnection connRN;
        private SqlConnection connSalesForce;

        public frmAddEmailBcc()
        {
            InitializeComponent();

            connStringRN = @"Data Source=10.1.10.60\CMM; Initial Catalog=RN_DB;Integrated Security=True; Max Pool Size=200; MultipleActiveResultSets=True";
            connStringSalesForce = @"Data Source=10.1.10.60\CMM; Initial Catalog=SalesForce; Integrated Security=True; MultipleActiveResultSets=True";

            connRN = new SqlConnection(connStringRN);
            connSalesForce = new SqlConnection(connStringSalesForce);
        }

        public frmAddEmailBcc(String email_bcc_default, String email_bcc)
        {
            InitializeComponent();

            connStringRN = @"Data Source=10.1.10.60\CMM; Initial Catalog=RN_DB;Integrated Security=True; Max Pool Size=200; MultipleActiveResultSets=True";
            connStringSalesForce = @"Data Source=10.1.10.60\CMM; Initial Catalog=SalesForce; Integrated Security=True; MultipleActiveResultSets=True";

            connRN = new SqlConnection(connStringRN);
            connSalesForce = new SqlConnection(connStringSalesForce);

            EmailBccDefault = email_bcc_default;
            EmailBccLoaded = email_bcc;
        }

        private void frmAddEmailBcc_Load(object sender, EventArgs e)
        {
            String strSqlQueryForMSStaff = "select [dbo].[tbl_user].[User_Email] from [dbo].[tbl_user] " +
                                           "inner join [dbo].[tbl_department] on [dbo].[tbl_user].[Department_Id] = [dbo].[tbl_department].[Department_Id] " +
                                           "where [dbo].[tbl_department].[DepartmentName] = 'Member Service Department'";

            SqlCommand cmdQueryForMSStaff = new SqlCommand(strSqlQueryForMSStaff, connRN);
            cmdQueryForMSStaff.CommandType = CommandType.Text;

            if (connRN.State != ConnectionState.Closed)
            {
                connRN.Close();
                connRN.Open();
            }
            else if (connRN.State == ConnectionState.Closed) connRN.Open();
            SqlDataReader rdrMSStaff = cmdQueryForMSStaff.ExecuteReader();
            if (rdrMSStaff.HasRows)
            {
                tvStaffEmail.Nodes.Add("Member Service Department");
                while (rdrMSStaff.Read())
                {
                    if (!rdrMSStaff.IsDBNull(0)) tvStaffEmail.Nodes[0].Nodes.Add(rdrMSStaff.GetString(0));
                }
            }
            rdrMSStaff.Close();
            if (connRN.State != ConnectionState.Closed) connRN.Close();

            String strSqlQueryForNPStaff = "select [dbo].[tbl_user].[User_Email] from [dbo].[tbl_user] " +
                                           "inner join [dbo].[tbl_department] on [dbo].[tbl_user].[Department_Id] = [dbo].[tbl_department].[Department_Id] " +
                                           "where [dbo].[tbl_department].[DepartmentName] = 'Needs Processing Department'";

            SqlCommand cmdQueryForNPStaff = new SqlCommand(strSqlQueryForNPStaff, connRN);
            cmdQueryForNPStaff.CommandType = CommandType.Text;

            if (connRN.State != ConnectionState.Closed)
            {
                connRN.Close();
                connRN.Open();
            }
            else if (connRN.State == ConnectionState.Closed) connRN.Open();
            SqlDataReader rdrNPStaff = cmdQueryForNPStaff.ExecuteReader();
            if (rdrNPStaff.HasRows)
            {
                tvStaffEmail.Nodes.Add("Needs Processing Department");
                while (rdrNPStaff.Read())
                {
                    if (!rdrNPStaff.IsDBNull(0)) tvStaffEmail.Nodes[1].Nodes.Add(rdrNPStaff.GetString(0));
                }
            }
            rdrNPStaff.Close();
            if (connRN.State != ConnectionState.Closed) connRN.Close();

            String strSqlQueryForRNStaff = "select [dbo].[tbl_user].[User_Email] from [dbo].[tbl_user] " +
                                           "inner join [dbo].[tbl_department] on [dbo].[tbl_user].[Department_Id] = [dbo].[tbl_department].[Department_Id] " +
                                           "where [dbo].[tbl_department].[DepartmentName] = 'Review and Negotiation Department'";

            SqlCommand cmdQueryForRNStaff = new SqlCommand(strSqlQueryForRNStaff, connRN);
            cmdQueryForRNStaff.CommandType = CommandType.Text;

            if (connRN.State != ConnectionState.Closed)
            {
                connRN.Close();
                connRN.Open();
            }
            else if (connRN.State == ConnectionState.Closed) connRN.Open();
            SqlDataReader rdrRNStaff = cmdQueryForRNStaff.ExecuteReader();
            if (rdrRNStaff.HasRows)
            {
                tvStaffEmail.Nodes.Add("Review and Negotiation Department");
                while (rdrRNStaff.Read())
                {
                    if (!rdrRNStaff.IsDBNull(0)) tvStaffEmail.Nodes[2].Nodes.Add(rdrRNStaff.GetString(0));
                }
            }
            rdrRNStaff.Close();
            if (connRN.State != ConnectionState.Closed) connRN.Close();

            String strSqlQueryForFDStaff = "select [dbo].[tbl_user].[User_Email] from [dbo].[tbl_user] " +
                               "inner join [dbo].[tbl_department] on [dbo].[tbl_user].[Department_Id] = [dbo].[tbl_department].[Department_Id] " +
                               "where [dbo].[tbl_department].[DepartmentName] = 'Finance Department'";

            SqlCommand cmdQueryForFDStaff = new SqlCommand(strSqlQueryForFDStaff, connRN);
            cmdQueryForFDStaff.CommandType = CommandType.Text;

            if (connRN.State != ConnectionState.Closed)
            {
                connRN.Close();
                connRN.Open();
            }
            else if (connRN.State == ConnectionState.Closed) connRN.Open();
            SqlDataReader rdrFDStaff = cmdQueryForFDStaff.ExecuteReader();
            if (rdrFDStaff.HasRows)
            {
                tvStaffEmail.Nodes.Add("Finance Department");
                while (rdrFDStaff.Read())
                {
                    if (!rdrFDStaff.IsDBNull(0)) tvStaffEmail.Nodes[3].Nodes.Add(rdrFDStaff.GetString(0));
                }
            }
            rdrFDStaff.Close();
            if (connRN.State != ConnectionState.Closed) connRN.Close();

            String strSqlQueryForITStaff = "select [dbo].[tbl_user].[User_Email] from [dbo].[tbl_user] " +
                               "inner join [dbo].[tbl_department] on [dbo].[tbl_user].[Department_Id] = [dbo].[tbl_department].[Department_Id] " +
                               "where [dbo].[tbl_department].[DepartmentName] = 'IT Department'";

            SqlCommand cmdQueryForITStaff = new SqlCommand(strSqlQueryForITStaff, connRN);
            cmdQueryForITStaff.CommandType = CommandType.Text;

            if (connRN.State != ConnectionState.Closed)
            {
                connRN.Close();
                connRN.Open();
            }
            else if (connRN.State == ConnectionState.Closed) connRN.Open();
            SqlDataReader rdrITStaff = cmdQueryForITStaff.ExecuteReader();
            if (rdrITStaff.HasRows)
            {
                tvStaffEmail.Nodes.Add("IT Department");
                while (rdrITStaff.Read())
                {
                    if (!rdrITStaff.IsDBNull(0)) tvStaffEmail.Nodes[4].Nodes.Add(rdrITStaff.GetString(0));
                }
            }
            if (connRN.State != ConnectionState.Closed) connRN.Close();

            String strSqlQueryForExecutive = "select [dbo].[tbl_user].[User_Email] from [dbo].[tbl_user] " +
                               "inner join [dbo].[tbl_department] on [dbo].[tbl_user].[Department_Id] = [dbo].[tbl_department].[Department_Id] " +
                               "where [dbo].[tbl_department].[DepartmentName] = 'Executive'";

            SqlCommand cmdQueryForExecutive = new SqlCommand(strSqlQueryForExecutive, connRN);
            cmdQueryForExecutive.CommandType = CommandType.Text;

            if (connRN.State != ConnectionState.Closed)
            {
                connRN.Close();
                connRN.Open();
            }
            else if (connRN.State == ConnectionState.Closed) connRN.Open();
            SqlDataReader rdrExecutive = cmdQueryForExecutive.ExecuteReader();
            if (rdrExecutive.HasRows)
            {
                tvStaffEmail.Nodes.Add("Executive");
                while (rdrExecutive.Read())
                {
                    if (!rdrExecutive.IsDBNull(0)) tvStaffEmail.Nodes[5].Nodes.Add(rdrExecutive.GetString(0));
                }
            }
            rdrExecutive.Close();
            if (connRN.State != ConnectionState.Closed) connRN.Close();

            String[] email_bcc = EmailBccLoaded.Split(';');

            for (int i = 0; i < email_bcc.Length; i++)
            {
                if (email_bcc[i].Trim() != String.Empty && email_bcc[i].Trim() != EmailBccDefault.Trim()) lbEmailBcc.Items.Add(email_bcc[i].Trim());
            }

        }

        private void btnCancelBcc_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tvStaffEmail_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (!e.Node.Text.Contains("Department")) lbEmailBcc.Items.Add(e.Node.Text.Trim());
        }

        private void btnAddEmailToBcc_Click(object sender, EventArgs e)
        {
            if (tvStaffEmail.SelectedNode != null && !tvStaffEmail.SelectedNode.Text.Contains("Department")) lbEmailBcc.Items.Add(tvStaffEmail.SelectedNode.Text);
        }

        private void btnRemoveEmailFromBcc_Click(object sender, EventArgs e)
        {
            if (lbEmailBcc.SelectedIndex != -1) lbEmailBcc.Items.RemoveAt(lbEmailBcc.SelectedIndex);
        }

        private void btnOkBcc_Click(object sender, EventArgs e)
        {
            EmailBcc = String.Empty;

            if (lbEmailBcc.Items.Count > 0)
            {
                foreach (String email in lbEmailBcc.Items)
                {
                    EmailBcc += email + "; ";
                }
            }

            DialogResult = DialogResult.OK;
        }
    }
}
