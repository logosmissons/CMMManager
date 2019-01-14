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

    public enum UserRole { Administrator = 0, FDManager, RNManager, NPManager, FDStaff, RNStaff, NPStaff, SuperAdmin = 20 };
    public enum Department { NeedsProcessing = 1, ReviewAndNegotiation, Finance, IT };

    public partial class frmLogin : Form
    {
        private SqlConnection connRN;
        private String connStringRN;

        private SqlConnection connSalesforce;
        private String connStringSalesforce;

        public int nLoggedUserId;
        public String LoggedInUserName;
        public UserRole nLoggedUserRole;
        public Department nLoggedInUserDepartmentId;

        public frmLogin()
        {
            InitializeComponent();

            connStringRN = @"Data Source=CMM-2014U\CMM; Initial Catalog=RN_DB; Integrated Security=True";
            connRN = new SqlConnection(connStringRN);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            String UserEmail = txtEmail.Text.Trim();

            String strSqlQueryForUserInfo = "select [dbo].[tbl_user].[User_Id], [dbo].[tbl_user].[User_Name], [dbo].[tbl_user].[User_Role_Id], [dbo].[tbl_user].[Department_Id] " +
                                            "from [dbo].[tbl_user] " +
                                            "where [dbo].[tbl_user].[User_Email] = @UserEmail";

            SqlCommand cmdQueryForUserInfo = new SqlCommand(strSqlQueryForUserInfo, connRN);
            cmdQueryForUserInfo.CommandType = CommandType.Text;

            cmdQueryForUserInfo.Parameters.AddWithValue("@UserEmail", UserEmail);

            if (connRN.State == ConnectionState.Open)
            {
                connRN.Close();
                connRN.Open();
            }
            else if (connRN.State == ConnectionState.Closed) connRN.Open();

            //Object oUserId = cmdQueryForUserId.ExecuteScalar();
            SqlDataReader rdrUserInfo = cmdQueryForUserInfo.ExecuteReader();
            if (rdrUserInfo.HasRows)
            {
                rdrUserInfo.Read();
                if (!rdrUserInfo.IsDBNull(0)) nLoggedUserId = rdrUserInfo.GetInt16(0);
                if (!rdrUserInfo.IsDBNull(1)) LoggedInUserName = rdrUserInfo.GetString(1);
                if (!rdrUserInfo.IsDBNull(2)) nLoggedUserRole = (UserRole)rdrUserInfo.GetInt16(2);
                if (!rdrUserInfo.IsDBNull(3)) nLoggedInUserDepartmentId = (Department)rdrUserInfo.GetInt16(3);
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("You are not CMM staff. Check your email and try again.", "Alert");
                DialogResult = DialogResult.Retry;
            }
            rdrUserInfo.Close();
            if (connRN.State == ConnectionState.Open) connRN.Close();

            //if (oUserId != null)
            //{
            //    nLoggedUserId = Convert.ToInt16(oUserId);
            //    DialogResult = DialogResult.OK;
            //    return;
            //}
            //else
            //{
            //    MessageBox.Show("You are not CMM staff. Check your email and try again.", "Alert");
            //    DialogResult = DialogResult.Retry;
            //    return;
            //}
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            return;
        }
    }
}
