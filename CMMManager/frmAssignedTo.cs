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
    public partial class frmAssignedTo : Form
    {
        private SqlConnection connRNDB;
        private String strConnString;
        private List<UserInfo> lstStaffInfo;
        private List<UserInfo> lstStaffInfoSorted;
        public String AssignedToList;

        public frmAssignedTo()
        {
            InitializeComponent();

            strConnString = @"Data Source=10.1.10.60\CMM; Initial Catalog=RN_DB;Integrated Security=True; Max Pool Size=200; MultipleActiveResultSets=True";
            //strConnString = @"Data Source=10.1.10.60\CMM; Initial Catalog=RN_DB;User ID=sa;Password=Yny00516; Max Pool Size=200; MultipleActiveResultSets=True";
            connRNDB = new SqlConnection(strConnString);
            lstStaffInfo = new List<UserInfo>();
            lstStaffInfoSorted = new List<UserInfo>();
        }

        private void frmAssignedTo_Load(object sender, EventArgs e)
        {
            String strSqlQueryForStaffInfo = "select [dbo].[tbl_user].[User_Id], [dbo].[tbl_user].[User_Name], [dbo].[tbl_user].[User_Email], " +
                                             "[dbo].[tbl_user].[Department_Id], [dbo].[tbl_department].[DepartmentName], [dbo].[tbl_user].[User_Role_Id] " +
                                             "from [dbo].[tbl_user] " +
                                             "inner join [dbo].[tbl_department] on [dbo].[tbl_user].[Department_Id] = [dbo].[tbl_department].[Department_Id]";

            SqlCommand cmdQueryForStaffInfo = new SqlCommand(strSqlQueryForStaffInfo, connRNDB);
            cmdQueryForStaffInfo.CommandType = CommandType.Text;

            if (connRNDB.State != ConnectionState.Closed)
            {
                connRNDB.Close();
                connRNDB.Open();
            }
            else if (connRNDB.State == ConnectionState.Closed) connRNDB.Open();

            lstStaffInfo.Clear();
            lstStaffInfoSorted.Clear();

            SqlDataReader rdrStaffInfo = cmdQueryForStaffInfo.ExecuteReader();
            if (rdrStaffInfo.HasRows)
            {
                while (rdrStaffInfo.Read())
                {
                    UserInfo info = new UserInfo();
                    if (!rdrStaffInfo.IsDBNull(0)) info.UserId = rdrStaffInfo.GetInt16(0);
                    if (!rdrStaffInfo.IsDBNull(1)) info.UserName = rdrStaffInfo.GetString(1);
                    if (!rdrStaffInfo.IsDBNull(2)) info.UserEmail = rdrStaffInfo.GetString(2);
                    if (!rdrStaffInfo.IsDBNull(3)) info.departmentInfo.DepartmentId = (Department)rdrStaffInfo.GetInt16(3);
                    if (!rdrStaffInfo.IsDBNull(4)) info.departmentInfo.DepartmentName = rdrStaffInfo.GetString(4);
                    if (!rdrStaffInfo.IsDBNull(5)) info.UserRoleId = (UserRole)rdrStaffInfo.GetInt16(5);
                    lstStaffInfo.Add(info);
                }
            }
            rdrStaffInfo.Close();
            if (connRNDB.State != ConnectionState.Closed) connRNDB.Close();

            //lstStaffInfoSorted = lstStaffInfo.OrderBy(info => info.departmentInfo.DepartmentName).ToList();
            List<String> lstDepartment = new List<String>();
            foreach (UserInfo info in lstStaffInfo)
            {
                lstDepartment.Add(info.departmentInfo.DepartmentName);
            }

            List<String> lstDepartmentDistinct = new List<String>();
            foreach (String department in lstDepartment.Distinct())
            {
                lstDepartmentDistinct.Add(department);
            }

            foreach (String department in lstDepartmentDistinct)
            {
                tvStaffNames.Nodes.Add(department);
            }

            lstStaffInfoSorted = lstStaffInfo.OrderBy(info => info.departmentInfo.DepartmentName).OrderBy(info => info.UserName).ToList();


            foreach (UserInfo info in lstStaffInfoSorted)
            {
                for (int i = 0; i < tvStaffNames.Nodes.Count; i++)
                {
                    if (tvStaffNames.Nodes[i].Text == info.departmentInfo.DepartmentName) tvStaffNames.Nodes[i].Nodes.Add(info.UserName);
                }
            }

            tvStaffNames.ExpandAll();
            tvStaffNames.Nodes[0].EnsureVisible();
          
        }

        private void tvStaffNames_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (!e.Node.Text.Contains("Department")) txtAssignedTo.Text += e.Node.Text.Trim() + ';';
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            AssignedToList = txtAssignedTo.Text.Trim();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
