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

        private String connStringRN;
        private String connStringSalesForce;
        private SqlConnection connRN;
        private SqlConnection connSalesForce;

        public frmAddEmailBcc()
        {
            InitializeComponent();

            connStringRN = @"Data Source=CMM-2014U\CMM; Initial Catalog=RN_DB;Integrated Security=True; Max Pool Size=200; MultipleActiveResultSets=True";
            connStringSalesForce = @"Data Source=CMM-2014U\CMM; Initial Catalog=SalesForce; Integrated Security=True; MultipleActiveResultSets=True";

            connRN = new SqlConnection(connStringRN);
            connSalesForce = new SqlConnection(connStringSalesForce);
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

        }
    }
}
