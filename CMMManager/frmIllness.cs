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
    public enum SqlConnectionIllnessOpen { RNDBConn, RNDBConn2, RNDBConn3 };

    public partial class frmIllness : Form
    {

        public IllnessOption SelectedOption;

        public int nLoggedInUserId;
        public UserRole LoggedInUserRole;

        public String strCaseIdIllness = String.Empty;
        public String strIndividualId = String.Empty;

        public SqlConnection connRNDB;
        public SqlConnection connRNDB2;
        public SqlConnection connRNDB3;
        public String strRNDBConnString = String.Empty;
        public String strRNDBConnString2 = String.Empty;
        public String strRNDBConnString3 = String.Empty;

        public SqlConnectionIllnessOpen RNDB_ConnIllnessOpen;

        public String strSqlGetIllnessForCaseId = String.Empty;

        //public Boolean bIllnessSelected;
        public SelectedIllness IllnessSelected;

        public String IllnessNo;

        public String OldIllnessNo;
        public String NewIllnessNo;

        public DateTime MembershipStartDate;

        private String strICD10Code;

        private delegate void AddRowToIllness(DataGridViewRow row);
        private delegate void RemoveRowIllness(int nRow);
        private delegate void RemoveAllRowIllness();

        public String ICD10Code
        {
            get { return strICD10Code; }
            set { strICD10Code = value; }
        }

        public frmIllness()
        {
            InitializeComponent();
            strRNDBConnString = @"Data Source=CMM-2014U\CMM; Initial Catalog=RN_DB; Integrated Security=True;";
            strRNDBConnString2 = @"Data Source=CMM-2014U\CMM; Initial Catalog=RN_DB; Integrated Security=True;";
            strRNDBConnString3 = @"Data Source=CMM-2014U\CMM; Initial Catalog=RN_DB; Integrated Security=True;";


            connRNDB = new SqlConnection(strRNDBConnString);
            connRNDB2 = new SqlConnection(strRNDBConnString2);
            connRNDB3 = new SqlConnection(strRNDBConnString3);

            IllnessSelected = new SelectedIllness();

        }

        private void frmIllness_Load(object sender, EventArgs e)
        {

            strSqlGetIllnessForCaseId = "select [dbo].[tbl_illness].[IllnessNo], [dbo].[tbl_illness].[Individual_Id], [dbo].[tbl_illness].[ICD_10_Id], [dbo].[tbl_illness].[CreateDate], " +
                                        "[dbo].[tbl_illness].[Introduction], [dbo].[tbl_illness].[Illness_Id] from [dbo].[tbl_illness] " +
                                        "where [dbo].[tbl_illness].[Case_Id] = @CaseId and " +
                                        //"[dbo].[tbl_illness].[IllnessNo] = @IllnessNo and " +
                                        "[dbo].[tbl_illness].[IsDeleted] = 0";

            //SqlCommand cmdQueryForIllness = new SqlCommand(strSqlGetIllnessForCaseId, connRNDB);
            SqlCommand cmdQueryForIllness = new SqlCommand(strSqlGetIllnessForCaseId);

            cmdQueryForIllness.Parameters.AddWithValue("@CaseId", strCaseIdIllness);
            //cmdQueryForIllness.Parameters.AddWithValue("@IllnessNo", IllnessSelected.IllnessNo);

            cmdQueryForIllness.CommandType = CommandType.Text;
            cmdQueryForIllness.CommandText = strSqlGetIllnessForCaseId;

            if (connRNDB.State == ConnectionState.Open)
            {
                connRNDB.Close();
                if (connRNDB.State == ConnectionState.Open)
                {
                    cmdQueryForIllness.Connection = connRNDB2;
                    connRNDB2.Open();
                    RNDB_ConnIllnessOpen = SqlConnectionIllnessOpen.RNDBConn2;
                }
                else if (connRNDB.State == ConnectionState.Closed)
                {
                    cmdQueryForIllness.Connection = connRNDB;
                    connRNDB.Open();
                    RNDB_ConnIllnessOpen = SqlConnectionIllnessOpen.RNDBConn;
                }
            }
            else if (connRNDB.State == ConnectionState.Closed)
            {
                cmdQueryForIllness.Connection = connRNDB;
                connRNDB.Open();
                RNDB_ConnIllnessOpen = SqlConnectionIllnessOpen.RNDBConn;
            }

            SqlDependency dependencyIllness = new SqlDependency(cmdQueryForIllness);
            dependencyIllness.OnChange += new OnChangeEventHandler(OnIllnessListChange);

            SqlDataReader rdrIllnessForCaseId = cmdQueryForIllness.ExecuteReader();

            gvIllness.Rows.Clear();

            if (rdrIllnessForCaseId.HasRows)
            {
                while(rdrIllnessForCaseId.Read())
                {

                    DataGridViewRow row = new DataGridViewRow();

                    row.Cells.Add(new DataGridViewCheckBoxCell { Value = false });

                    if (!rdrIllnessForCaseId.IsDBNull(0))
                    {
                        row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrIllnessForCaseId.GetString(0) });
                        IllnessSelected.IllnessNo = rdrIllnessForCaseId.GetString(0);
                    }
                    else
                    {
                        row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                        IllnessSelected.IllnessNo = String.Empty;
                    }
                    if (!rdrIllnessForCaseId.IsDBNull(1)) row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrIllnessForCaseId.GetString(1) });
                    else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                    
                    if (!rdrIllnessForCaseId.IsDBNull(2)) row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrIllnessForCaseId.GetString(2) });
                    else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });

                    if (!rdrIllnessForCaseId.IsDBNull(3)) row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrIllnessForCaseId.GetDateTime(3).ToString("MM/dd/yyyy") });
                    else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });

                    if (!rdrIllnessForCaseId.IsDBNull(4)) row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrIllnessForCaseId.GetString(4) });
                    else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });

                    if (!rdrIllnessForCaseId.IsDBNull(5)) IllnessSelected.IllnessId = rdrIllnessForCaseId.GetInt32(5).ToString();
                    else IllnessSelected.IllnessId = String.Empty;

                    gvIllness.Rows.Add(row);

                }
            }

            //if (connRNDB.State == ConnectionState.Open) connRNDB.Close();
            if (RNDB_ConnIllnessOpen == SqlConnectionIllnessOpen.RNDBConn)
            {
                if (connRNDB.State != ConnectionState.Closed) connRNDB.Close();
            }
            else if (RNDB_ConnIllnessOpen == SqlConnectionIllnessOpen.RNDBConn2)
            {
                if (connRNDB2.State != ConnectionState.Closed) connRNDB2.Close();
            }

            if (IllnessNo != String.Empty)
            {
                for (int i = 0; i < gvIllness.RowCount; i++)
                {
                    if (IllnessNo.Trim() == gvIllness[1, i].Value.ToString().Trim()) gvIllness[0, i].Value = true;
                }
            }

            //if (LoggedInUserRole == UserRole.FDStaff ||
            //    LoggedInUserRole == UserRole.NPStaff ||
            //    LoggedInUserRole == UserRole.RNStaff)
            //{
            //    btnDelete.Enabled = false;
            //}
            //else btnDelete.Enabled = true;

        }

        private void OnIllnessListChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type == SqlNotificationType.Change)
            {
                SqlDependency dependency = sender as SqlDependency;
                dependency.OnChange -= OnIllnessListChange;

                UpdateGridViewIllnessList();
            }
        }

        private void AddNewRowToIllness(DataGridViewRow row)
        {
            gvIllness.Rows.Add(row);
        }

        private void RemoveRowFromIllness(int i)
        {
            gvIllness.Rows.RemoveAt(i);
        }

        private void RemoveAllRowsIllness()
        {
            gvIllness.Rows.Clear();
        }

        private void AddRowToIllnessSafely(DataGridViewRow row)
        {
            gvIllness.BeginInvoke(new AddRowToIllness(AddNewRowToIllness), row);
        }

        private void ClearIllnessSafely()
        {
            gvIllness.BeginInvoke(new RemoveAllRowIllness(RemoveAllRowsIllness));
        }

        private void UpdateGridViewIllnessList()
        {

            //String strQueryForIllness = "select [dbo].[tbl_illness].[Illness_Id], [dbo].[tbl_illness].[Individual_Id], [dbo].[tbl_illness].[ICD_10_Id], [dbo].[tbl_illness].[CreateDate], " +
            //                            "[dbo].[tbl_illness].[Introduction] from [dbo].[tbl_illness] " +
            //                            "where [dbo].[tbl_illness].[Case_Id] = @CaseId and " +
            //                            "[dbo].[tbl_illness].[IsDeleted] = 0";

            String strSqlQueryFprIllnessForCaseId = "select [dbo].[tbl_illness].[IllnessNo], [dbo].[tbl_illness].[Individual_Id], [dbo].[tbl_illness].[ICD_10_Id], [dbo].[tbl_illness].[CreateDate], " +
                            "[dbo].[tbl_illness].[Introduction], [dbo].[tbl_illness].[Illness_Id] from [dbo].[tbl_illness] " +
                            "where [dbo].[tbl_illness].[Case_Id] = @CaseId and " +
                            //"[dbo].[tbl_illness].[IllnessNo] = @IllnessNo and " +
                            "[dbo].[tbl_illness].[IsDeleted] = 0";

            SqlCommand cmdQueryForIllness = new SqlCommand(strSqlQueryFprIllnessForCaseId, connRNDB);
            cmdQueryForIllness.CommandType = CommandType.Text;
            //cmdQueryForIllness.CommandText = strQueryForIllness;

            cmdQueryForIllness.Parameters.AddWithValue("@CaseId", strCaseIdIllness);
            //cmdQueryForIllness.Parameters.AddWithValue("@IllnessNo", IllnessSelected.IllnessNo);

            if (RNDB_ConnIllnessOpen == SqlConnectionIllnessOpen.RNDBConn)
            {
                if (connRNDB.State != ConnectionState.Closed)
                {
                    connRNDB.Close();
                    cmdQueryForIllness.Connection = connRNDB;
                    connRNDB.Open();
                }
                else if (connRNDB.State == ConnectionState.Closed)
                {
                    cmdQueryForIllness.Connection = connRNDB;
                    connRNDB.Open();
                }
            }
            else if (RNDB_ConnIllnessOpen == SqlConnectionIllnessOpen.RNDBConn2)
            {
                if (connRNDB2.State != ConnectionState.Closed)
                {
                    connRNDB2.Close();
                    cmdQueryForIllness.Connection = connRNDB2;
                    connRNDB2.Open();
                }
                else if (connRNDB2.State != ConnectionState.Closed)
                {
                    cmdQueryForIllness.Connection = connRNDB2;
                    connRNDB2.Open();
                }
            }


            //if (connRNDB.State == ConnectionState.Open)
            //{
            //    connRNDB.Close();
            //    connRNDB.Open();
            //}
            else if (connRNDB.State == ConnectionState.Closed) connRNDB.Open();

            SqlDependency dependencyIllness = new SqlDependency(cmdQueryForIllness);
            dependencyIllness.OnChange += new OnChangeEventHandler(OnIllnessListChange);

            SqlDataReader reader = cmdQueryForIllness.ExecuteReader();

            //gvIllness.Rows.Clear();

            if (IsHandleCreated) ClearIllnessSafely();
            else gvIllness.Rows.Clear();

            if (reader.HasRows)
            {
                while(reader.Read())
                {
                    DataGridViewRow row = new DataGridViewRow();

                    row.Cells.Add(new DataGridViewCheckBoxCell { Value = false });
                    row.Cells.Add(new DataGridViewTextBoxCell { Value = reader.GetString(0) });
                    row.Cells.Add(new DataGridViewTextBoxCell { Value = reader.GetString(1) });
                    if (!reader.IsDBNull(2)) row.Cells.Add(new DataGridViewTextBoxCell { Value = reader.GetString(2) });
                    else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                    row.Cells.Add(new DataGridViewTextBoxCell { Value = reader.GetDateTime(3) });
                    if (!reader.IsDBNull(4)) row.Cells.Add(new DataGridViewTextBoxCell { Value = reader.GetString(4) });
                    else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });

                    if (IsHandleCreated) AddRowToIllnessSafely(row);
                    else gvIllness.Rows.Add(row);
                }
            }
            //if (connRNDB.State == ConnectionState.Open) connRNDB.Close();

            if (RNDB_ConnIllnessOpen == SqlConnectionIllnessOpen.RNDBConn)
            {
                if (connRNDB.State != ConnectionState.Closed) connRNDB.Close();
            }
            else if (RNDB_ConnIllnessOpen == SqlConnectionIllnessOpen.RNDBConn2)
            {
                if (connRNDB2.State != ConnectionState.Closed) connRNDB2.Close();
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (gvIllness.Rows.Count > 0)
            {
                Boolean bIllnessSelected = false;
                for (int i = 0; i < gvIllness.Rows.Count; i++)
                {
                    if ((Boolean)gvIllness[0, i].Value == true)
                    {
                        IllnessSelected.IllnessNo = gvIllness[1, i].Value.ToString();
                        NewIllnessNo = IllnessSelected.IllnessNo;
                        //int Result;
                        //if (Int32.TryParse(IllnessSelected.IllnessId, out Result)) NewIllnessId = Result;
                        IllnessSelected.ICD10Code = gvIllness[3, i].Value.ToString();

                        // 10/31/18 begin here to get Illness.IllnessId
                        if (NewIllnessNo.Trim() != String.Empty)
                        {
                            String IllnessNo = NewIllnessNo.Trim();

                            String strSqlQueryForIllnessId = "select [dbo].[tbl_illness].[Illness_Id] from [dbo].[tbl_illness] where [dbo].[tbl_illness].[IllnessNo] = @IllnessNo";

                            //SqlCommand cmdQueryForIllnessId = new SqlCommand(strSqlQueryForIllnessId, connRNDB);
                            SqlCommand cmdQueryForIllnessId = new SqlCommand(strSqlQueryForIllnessId);

                            cmdQueryForIllnessId.CommandType = CommandType.Text;

                            cmdQueryForIllnessId.Parameters.AddWithValue("@IllnessNo", IllnessNo);

                            //if (connRNDB.State == ConnectionState.Open)
                            //{
                            //    connRNDB.Close();
                            //    connRNDB.Open();
                            //}
                            //else if (connRNDB.State == ConnectionState.Closed) connRNDB.Open();

                            if (connRNDB.State == ConnectionState.Open)
                            {
                                connRNDB.Close();
                                if (connRNDB.State == ConnectionState.Open)
                                {
                                    cmdQueryForIllnessId.Connection = connRNDB2;
                                    connRNDB2.Open();
                                    RNDB_ConnIllnessOpen = SqlConnectionIllnessOpen.RNDBConn2;
                                }
                                else if (connRNDB.State == ConnectionState.Closed)
                                {
                                    cmdQueryForIllnessId.Connection = connRNDB;
                                    connRNDB.Open();
                                    RNDB_ConnIllnessOpen = SqlConnectionIllnessOpen.RNDBConn;
                                }
                            }
                            else if (connRNDB.State == ConnectionState.Closed)
                            {
                                cmdQueryForIllnessId.Connection = connRNDB;
                                connRNDB.Open();
                                RNDB_ConnIllnessOpen = SqlConnectionIllnessOpen.RNDBConn;
                            }

                            Object objIllnessId = cmdQueryForIllnessId.ExecuteScalar();
                            //if (connRNDB.State == ConnectionState.Open) connRNDB.Close();
                            if (RNDB_ConnIllnessOpen == SqlConnectionIllnessOpen.RNDBConn)
                            {
                                if (connRNDB.State != ConnectionState.Closed) connRNDB.Close();
                            }
                            else if (RNDB_ConnIllnessOpen == SqlConnectionIllnessOpen.RNDBConn2)
                            {
                                if (connRNDB2.State != ConnectionState.Closed) connRNDB2.Close();
                            }

                            if (objIllnessId != null) IllnessSelected.IllnessId = objIllnessId.ToString();
                            else
                            {
                                MessageBox.Show("No Illness Id for given Illness No", "Error");
                                return;
                            }
                        }


                        DialogResult = DialogResult.OK;
                        bIllnessSelected = true;
                        SelectedOption = IllnessOption.Select;
                        return;
                    }
                }
                if (bIllnessSelected == false)
                {
                    MessageBox.Show("Please select an Illness.", "Alert");
                    return;
                }
            }
         }

        private void gvIllness_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView gv = sender as DataGridView;

            int nClicked = e.RowIndex;

            if ((Boolean)gv[0, e.RowIndex].Value == true) gv[0, e.RowIndex].Value = false;
            else if ((Boolean)gv[0, e.RowIndex].Value == false) gv[0, e.RowIndex].Value = true;

            for (int i = 0; i < gv.RowCount; i++)
            {
                if (i != e.RowIndex) gv[0, i].Value = false;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            //DialogResult = DialogResult.None;
            //bIllnessSelected = false;
            SelectedOption = IllnessOption.Close;
            Close();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            String strSqlQueryForIllnessId = "select max([dbo].[tbl_illness].[Illness_Id]) from [dbo].[tbl_illness]";

            SqlCommand cmdQueryForIllnessId = new SqlCommand(strSqlQueryForIllnessId, connRNDB);
            cmdQueryForIllnessId.CommandType = CommandType.Text;

            //if (connRNDB.State == ConnectionState.Open)
            //{
            //    connRNDB.Close();
            //    connRNDB.Open();
            //}
            //else if (connRNDB.State == ConnectionState.Closed) connRNDB.Open();

            if (connRNDB.State == ConnectionState.Open)
            {
                connRNDB.Close();
                if (connRNDB.State == ConnectionState.Open)
                {
                    cmdQueryForIllnessId.Connection = connRNDB2;
                    connRNDB2.Open();
                    RNDB_ConnIllnessOpen = SqlConnectionIllnessOpen.RNDBConn2;
                }
                else if (connRNDB.State == ConnectionState.Closed)
                {
                    cmdQueryForIllnessId.Connection = connRNDB;
                    connRNDB.Open();
                    RNDB_ConnIllnessOpen = SqlConnectionIllnessOpen.RNDBConn;
                }
            }
            else if (connRNDB.State == ConnectionState.Closed)
            {
                cmdQueryForIllnessId.Connection = connRNDB;
                connRNDB.Open();
                RNDB_ConnIllnessOpen = SqlConnectionIllnessOpen.RNDBConn;
            }

            Object objIllnessId = cmdQueryForIllnessId.ExecuteScalar();
            //if (connRNDB.State == ConnectionState.Open) connRNDB.Close();

            if (RNDB_ConnIllnessOpen == SqlConnectionIllnessOpen.RNDBConn)
            {
                if (connRNDB.State != ConnectionState.Closed) connRNDB.Close();
            }
            else if (RNDB_ConnIllnessOpen == SqlConnectionIllnessOpen.RNDBConn2)
            {
                if (connRNDB2.State != ConnectionState.Closed) connRNDB2.Close();
            }

            String strLastIllnessId = objIllnessId.ToString();

            //String NewIllnessNo = String.Empty;
            String NewIllnessNo = "ILL-";

            if (strLastIllnessId != String.Empty)
            {
                String strSqlQueryForLastIllnessNo = "select [dbo].[tbl_illness].[IllnessNo] from [dbo].[tbl_illness] where [dbo].[tbl_illness].[Illness_Id] = @IllnessId";

                SqlCommand cmdQueryForLastIllnessNo = new SqlCommand(strSqlQueryForLastIllnessNo, connRNDB);
                cmdQueryForLastIllnessNo.CommandType = CommandType.Text;

                cmdQueryForLastIllnessNo.Parameters.AddWithValue("@IllnessId", Int32.Parse(objIllnessId.ToString()));

                //if (connRNDB.State == ConnectionState.Open)
                //{
                //    connRNDB.Close();
                //    connRNDB.Open();
                //}
                //else if (connRNDB.State == ConnectionState.Closed) connRNDB.Open();

                if (connRNDB.State == ConnectionState.Open)
                {
                    connRNDB.Close();
                    if (connRNDB.State == ConnectionState.Open)
                    {
                        cmdQueryForLastIllnessNo.Connection = connRNDB2;
                        connRNDB2.Open();
                        RNDB_ConnIllnessOpen = SqlConnectionIllnessOpen.RNDBConn2;
                    }
                    else if (connRNDB.State == ConnectionState.Closed)
                    {
                        cmdQueryForLastIllnessNo.Connection = connRNDB;
                        connRNDB.Open();
                        RNDB_ConnIllnessOpen = SqlConnectionIllnessOpen.RNDBConn;
                    }
                }
                else if (connRNDB.State == ConnectionState.Closed)
                {
                    cmdQueryForLastIllnessNo.Connection = connRNDB;
                    connRNDB.Open();
                    RNDB_ConnIllnessOpen = SqlConnectionIllnessOpen.RNDBConn;
                }

                Object objLastIllnessNo = cmdQueryForLastIllnessNo.ExecuteScalar();
                //if (connRNDB.State == ConnectionState.Open) connRNDB.Close();

                if (RNDB_ConnIllnessOpen == SqlConnectionIllnessOpen.RNDBConn)
                {
                    if (connRNDB.State != ConnectionState.Closed) connRNDB.Close();
                }
                else if (RNDB_ConnIllnessOpen == SqlConnectionIllnessOpen.RNDBConn2)
                {
                    if (connRNDB2.State != ConnectionState.Closed) connRNDB2.Close();
                }

                if (objLastIllnessNo != null)
                {
                    String strLastIllnessNo = objLastIllnessNo.ToString().Substring(4);
                    int LastIllnessNo = Int32.Parse(strLastIllnessNo);
                    LastIllnessNo++;

                    NewIllnessNo += LastIllnessNo.ToString();

                    frmIllnessCreationPage frmIllnessCreation = new frmIllnessCreationPage();

                    frmIllnessCreation.IllnessNo = NewIllnessNo;

                    frmIllnessCreation.nLoggedInUserId = nLoggedInUserId;
                    frmIllnessCreation.strCaseNo = strCaseIdIllness;
                    frmIllnessCreation.strIndividualNo = strIndividualId;
                    frmIllnessCreation.MembershipStartDate = MembershipStartDate;

                    frmIllnessCreation.ShowDialog(this);
                }
            }
            else
            {
                frmIllnessCreationPage frmIllnessCreation = new frmIllnessCreationPage();

                frmIllnessCreation.IllnessNo = "ILL-1";

                frmIllnessCreation.nLoggedInUserId = nLoggedInUserId;
                frmIllnessCreation.strCaseNo = strCaseIdIllness;
                frmIllnessCreation.strIndividualNo = strIndividualId;
                frmIllnessCreation.MembershipStartDate = MembershipStartDate;

                frmIllnessCreation.ShowDialog(this);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            String strIllnessNo = String.Empty;
            int nRowSelected = 0;
            if (gvIllness.Rows.Count > 0)
            {
                Boolean bIllnessSelected = false;
                for (int i = 0; i < gvIllness.Rows.Count; i++)
                {
                    if ((Boolean)gvIllness["Selected", i].Value == true)
                    {
                        IllnessSelected.IllnessNo = gvIllness["Illness_No", i]?.Value?.ToString();
                        strIllnessNo = IllnessSelected.IllnessNo;
                        bIllnessSelected = true;
                        nRowSelected = i;
                    }
                }
                if (bIllnessSelected == false)
                {
                    MessageBox.Show("Please select an Illness", "Alert");
                    return;
                }
            }
            else
            {
                MessageBox.Show("There is no Illnes.", "Alert");
                return;
            }

            if (strIllnessNo != String.Empty)
            {
                frmIllnessCreationPage frm = new frmIllnessCreationPage();
                frm.mode = IllnessMode.Edit;
                frm.nLoggedInUserId = nLoggedInUserId;
                frm.strIndividualNo = gvIllness["Individual_Id", nRowSelected].Value.ToString();
                frm.IllnessNo = strIllnessNo;
                //frm.nIllnessNo = nIllnessNoSelected;

                frm.ShowDialog();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (gvIllness.Rows.Count > 0)
            {
                String IllnessNo = String.Empty;

                for (int i = 0; i < gvIllness.Rows.Count; i++)
                {
                    if ((Boolean)gvIllness["Selected", i].Value == true)
                    {
                        IllnessNo = gvIllness["Illness_No", i]?.Value?.ToString();
                    }
                }

                if (IllnessNo != String.Empty)
                {
                    DialogResult dlgResultConfirm = MessageBox.Show("Are you sure to delete the illness?", "Warning", MessageBoxButtons.YesNo);

                    if (dlgResultConfirm == DialogResult.Yes)
                    {
                        Boolean bIncidentExists = false;

                        String strSqlQueryForIncidentNo = "select [dbo].[tbl_incident].[IncidentNo] from [dbo].[tbl_incident] " +
                                                            "inner join [dbo].[tbl_illness] on [dbo].[tbl_incident].[Illness_id] = [dbo].[tbl_illness].[Illness_Id] " +
                                                            "where [dbo].[tbl_illness].[IllnessNo] = @IllnessNo";

                        SqlCommand cmdQueryForIncidentNo = new SqlCommand(strSqlQueryForIncidentNo, connRNDB);

                        cmdQueryForIncidentNo.CommandType = CommandType.Text;
                        cmdQueryForIncidentNo.Parameters.AddWithValue("@IllnessNo", IllnessNo);

                        if (connRNDB.State == ConnectionState.Open)
                        {
                            connRNDB.Close();
                            if (connRNDB.State == ConnectionState.Open)
                            {
                                cmdQueryForIncidentNo.Connection = connRNDB2;
                                connRNDB2.Open();
                                RNDB_ConnIllnessOpen = SqlConnectionIllnessOpen.RNDBConn2;
                            }
                            else if (connRNDB.State == ConnectionState.Closed)
                            {
                                cmdQueryForIncidentNo.Connection = connRNDB;
                                connRNDB.Open();
                                RNDB_ConnIllnessOpen = SqlConnectionIllnessOpen.RNDBConn;
                            }
                        }
                        else if (connRNDB.State == ConnectionState.Closed)
                        {
                            cmdQueryForIncidentNo.Connection = connRNDB;
                            connRNDB.Open();
                            RNDB_ConnIllnessOpen = SqlConnectionIllnessOpen.RNDBConn;
                        }

                        SqlDataReader rdrIncidentNo = cmdQueryForIncidentNo.ExecuteReader();
                        if (rdrIncidentNo.HasRows)
                        {
                            bIncidentExists = true;
                        }

                        if (RNDB_ConnIllnessOpen == SqlConnectionIllnessOpen.RNDBConn)
                        {
                            if (connRNDB.State != ConnectionState.Closed) connRNDB.Close();
                        }
                        else if (RNDB_ConnIllnessOpen == SqlConnectionIllnessOpen.RNDBConn2)
                        {
                            if (connRNDB2.State != ConnectionState.Closed) connRNDB2.Close();
                        }

                        if (bIncidentExists == true)
                        {
                            MessageBox.Show("Cannot delete Illness. Incident on the Illness exists.", "Error");
                            return;
                        }

                        String strSqlDeleteIllness = "update [dbo].[tbl_illness] set [dbo].[tbl_illness].[IsDeleted] = 1 where [dbo].[tbl_illness].[IllnessNo] = @IllnessNo";

                        SqlCommand cmdDeleteIllness = new SqlCommand(strSqlDeleteIllness, connRNDB);

                        cmdDeleteIllness.CommandType = CommandType.Text;
                        cmdDeleteIllness.Parameters.AddWithValue("@IllnessNo", IllnessNo);

                        if (connRNDB.State == ConnectionState.Open)
                        {
                            connRNDB.Close();
                            if (connRNDB.State == ConnectionState.Open)
                            {
                                cmdDeleteIllness.Connection = connRNDB2;
                                connRNDB2.Open();
                                RNDB_ConnIllnessOpen = SqlConnectionIllnessOpen.RNDBConn2;
                            }
                            else if (connRNDB.State == ConnectionState.Closed)
                            {
                                cmdDeleteIllness.Connection = connRNDB;
                                connRNDB.Open();
                                RNDB_ConnIllnessOpen = SqlConnectionIllnessOpen.RNDBConn;
                            }
                        }
                        else if (connRNDB.State == ConnectionState.Closed)
                        {
                            cmdDeleteIllness.Connection = connRNDB;
                            connRNDB.Open();
                            RNDB_ConnIllnessOpen = SqlConnectionIllnessOpen.RNDBConn;
                        }

                        int nRowDeleted = cmdDeleteIllness.ExecuteNonQuery();

                        //if (connRNDB.State == ConnectionState.Open) connRNDB.Close();

                        if (RNDB_ConnIllnessOpen == SqlConnectionIllnessOpen.RNDBConn)
                        {
                            if (connRNDB.State != ConnectionState.Closed) connRNDB.Close();
                        }
                        else if (RNDB_ConnIllnessOpen == SqlConnectionIllnessOpen.RNDBConn2)
                        {
                            if (connRNDB2.State != ConnectionState.Closed) connRNDB2.Close();
                        }

                    }
                    else if (dlgResultConfirm == DialogResult.No)
                    {
                        return;
                    }
                }
                else if (IllnessNo == String.Empty)
                {
                    MessageBox.Show("Please select illness to delete.", "Alert");
                    return;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //DialogResult = DialogResult.Cancel;
            SelectedOption = IllnessOption.Cancel;
            Close();
        }
    }

    public class SelectedIllness
    {
        private String strIllnessId;
        private String strIllnessNo;
        private String strICD10Code;

        public String IllnessId
        {
            get { return strIllnessId; }
            set { strIllnessId = value; }
        }

        public String IllnessNo
        {
            get { return strIllnessNo; }
            set { strIllnessNo = value; }
        }

        public String ICD10Code
        {
            get { return strICD10Code; }
            set { strICD10Code = value; }
        }

        public SelectedIllness()
        {
            strIllnessId = String.Empty;
            strIllnessNo = String.Empty;
            strICD10Code = String.Empty;
        }

        public SelectedIllness (String illness_id, String illness_no, String icd10code)
        {
            strIllnessId = illness_id;
            strIllnessNo = illness_no;
            strICD10Code = icd10code;
        }
    }
}
