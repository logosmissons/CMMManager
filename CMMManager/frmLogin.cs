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
    public partial class frmLogin : Form
    {

        private SqlConnection connRN;
        private String connStringRN;

        private SqlConnection connSalesforce;
        private String connStringSalesforce;

        public int nLoggedUserId;

        public frmLogin()
        {
            InitializeComponent();

            connStringRN = @"Data Source=CMM-2014U\CMM; Initial Catalog=RN_DB; Integrated Security=True";
            connRN = new SqlConnection(connStringRN);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            String UserEmail = txtEmail.Text.Trim();

            String strSqlQueryForUserId = "select [dbo].[tbl_user].[User_Id] from [dbo].[tbl_user] where [dbo].[tbl_user].[User_Email] = @UserEmail";

            SqlCommand cmdQueryForUserId = new SqlCommand(strSqlQueryForUserId, connRN);
            cmdQueryForUserId.CommandType = CommandType.Text;

            cmdQueryForUserId.Parameters.AddWithValue("@UserEmail", UserEmail);

            connRN.Open();
            Object oUserId = cmdQueryForUserId.ExecuteScalar();
            connRN.Close();

            if (oUserId != null)
            {
                nLoggedUserId = Convert.ToInt16(oUserId);
                DialogResult = DialogResult.OK;
                return;
            }
            else
            {
                MessageBox.Show("You are not CMM staff. Check your email and try again.", "Alert");
                DialogResult = DialogResult.Retry;
                return;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            return;
        }
    }
}
