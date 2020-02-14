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
using System.IO;

namespace CMMManager
{
    public partial class frmLogin : Form
    {
        private SqlConnection connRN;
        private String connStringRN;

        private SqlConnection connSalesforce;
        private String connStringSalesforce;

        public int nLoggedUserId;
        public String LoggedInUserEmail;
        public String LoggedInUserName;
        public UserRole nLoggedUserRole;
        public Department nLoggedInUserDepartmentId;

        public frmLogin()
        {
            InitializeComponent();

            connStringRN = @"Data Source=10.1.10.60\CMM; Initial Catalog=RN_DB; Integrated Security=True; Max Pool Size=200; MultipleActiveResultSets=True";
            //connStringRN = @"Data Source=10.1.10.60\CMM;Initial Catalog=RN_DB;User ID=sa;Password=Yny00516";
            connRN = new SqlConnection(connStringRN);

            String strUserLoginInfoPath = @"C:\RNManagerUserLoginInfo\UserLoginInfo.txt";

            if (File.Exists(strUserLoginInfoPath))
            {
                FileStream fs = File.OpenRead(strUserLoginInfoPath);

                StreamReader sr = new StreamReader(fs);

                txtEmail.Text = sr.ReadLine();

                sr.Close();
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            String UserEmail = txtEmail.Text.Trim();

            String strSqlQueryForUserInfo = "select [dbo].[tbl_user].[User_Id], [dbo].[tbl_user].[User_Email], [dbo].[tbl_user].[User_Name], " +
                                            "[dbo].[tbl_user].[User_Role_Id], [dbo].[tbl_user].[Department_Id] " +
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
                if (!rdrUserInfo.IsDBNull(1)) LoggedInUserEmail = rdrUserInfo.GetString(1);
                if (!rdrUserInfo.IsDBNull(2)) LoggedInUserName = rdrUserInfo.GetString(2);
                if (!rdrUserInfo.IsDBNull(3)) nLoggedUserRole = (UserRole)rdrUserInfo.GetInt16(3);
                if (!rdrUserInfo.IsDBNull(4)) nLoggedInUserDepartmentId = (Department)rdrUserInfo.GetInt16(4);

                String strUserLoginIdFolderHidden = @"C:\RNManagerUserLoginInfo";
      
                if (!Directory.Exists(strUserLoginIdFolderHidden))
                {
                    DirectoryInfo di = Directory.CreateDirectory(strUserLoginIdFolderHidden);
                    di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
                }

                String strUserLoginInfoPath = @"C:\RNManagerUserLoginInfo\UserLoginInfo.txt";

                FileStream fs = File.Open(strUserLoginInfoPath, FileMode.Create, FileAccess.Write, FileShare.None);
                StreamWriter sw = new StreamWriter(fs);

                sw.WriteLine(LoggedInUserEmail);
                sw.Close();
                //fs.Close();

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

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
