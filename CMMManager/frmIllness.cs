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

    public partial class frmIllness : Form
    {

        public IllnessOption SelectedOption;

        public int nLoggedInUserId;
        public UserRole LoggedInUserRole;

        public String strCaseIdIllness = String.Empty;
        public String strIndividualId = String.Empty;

        public SqlConnection connRNDB;
        public String strRNDBConnString = String.Empty;
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
            strRNDBConnString = @"Data Source=CMM-2014U\CMM; Initial Catalog=RN_DB;Integrated Security=True; Max Pool Size=200; MultipleActiveResultSets=True";
            //strRNDBConnString = @"Data Source=CMM-2014U\CMM; Initial Catalog=RN_DB;User ID=sa;Password=Yny00516; Max Pool Size=200; MultipleActiveResultSets=True";
            connRNDB = new SqlConnection(strRNDBConnString);

            SqlDependency.Start(strRNDBConnString);

            IllnessSelected = new SelectedIllness();

        }

        ~frmIllness()
        {
            SqlDependency.Stop(strRNDBConnString);
        }

        private void frmIllness_Load(object sender, EventArgs e)
        {

       

            strSqlGetIllnessForCaseId = "select [dbo].[tbl_illness].[IllnessNo], [dbo].[tbl_illness].[Individual_Id], [dbo].[tbl_illness].[ICD_10_Id], [dbo].[tbl_illness].[Introduction], " +
                                        "[dbo].[tbl_illness].[CreateDate], " +
                                        "[dbo].[tbl_illness].[Illness_Id] " +
                                        "from [dbo].[tbl_illness] " +
                                        "where [dbo].[tbl_illness].[Case_Id] = @CaseId and " +
                                        //"[dbo].[tbl_illness].[IllnessNo] = @IllnessNo and " +
                                        "[dbo].[tbl_illness].[IsDeleted] = 0";

            SqlCommand cmdQueryForIllness = new SqlCommand(strSqlGetIllnessForCaseId, connRNDB);
            cmdQueryForIllness.Parameters.AddWithValue("@CaseId", strCaseIdIllness);
            //cmdQueryForIllness.Parameters.AddWithValue("@IllnessNo", IllnessSelected.IllnessNo);

            cmdQueryForIllness.CommandType = CommandType.Text;
            cmdQueryForIllness.CommandText = strSqlGetIllnessForCaseId;

            SqlDependency dependencyIllness = new SqlDependency(cmdQueryForIllness);
            dependencyIllness.OnChange += new OnChangeEventHandler(OnIllnessListChange);

            if (connRNDB.State == ConnectionState.Open)
            {
                connRNDB.Close();
                connRNDB.Open();
            }
            else if (connRNDB.State == ConnectionState.Closed) connRNDB.Open();
            SqlDataReader rdrIllnessForCaseId = cmdQueryForIllness.ExecuteReader();

            if (rdrIllnessForCaseId.HasRows)
            {
                gvIllness.Rows.Clear();
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

                    if (!rdrIllnessForCaseId.IsDBNull(3)) row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrIllnessForCaseId.GetString(3) });
                    else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });

                    if (!rdrIllnessForCaseId.IsDBNull(4)) row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrIllnessForCaseId.GetDateTime(4).ToString("MM/dd/yyyy") });
                    else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });

                    if (!rdrIllnessForCaseId.IsDBNull(5)) IllnessSelected.IllnessId = rdrIllnessForCaseId.GetInt32(5).ToString();
                    else IllnessSelected.IllnessId = String.Empty;

                    gvIllness.Rows.Add(row);

                }
            }
            rdrIllnessForCaseId.Close();
            if (connRNDB.State == ConnectionState.Open) connRNDB.Close();

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

            SqlDependency dependencyIllness = new SqlDependency(cmdQueryForIllness);
            dependencyIllness.OnChange += new OnChangeEventHandler(OnIllnessListChange);

            if (connRNDB.State == ConnectionState.Open)
            {
                connRNDB.Close();
                connRNDB.Open();
            }
            else if (connRNDB.State == ConnectionState.Closed) connRNDB.Open();
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
            reader.Close();
            if (connRNDB.State == ConnectionState.Open) connRNDB.Close();
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

                            SqlCommand cmdQueryForIllnessId = new SqlCommand(strSqlQueryForIllnessId, connRNDB);
                            cmdQueryForIllnessId.CommandType = CommandType.Text;

                            cmdQueryForIllnessId.Parameters.AddWithValue("@IllnessNo", IllnessNo);

                            if (connRNDB.State == ConnectionState.Open)
                            {
                                connRNDB.Close();
                                connRNDB.Open();
                            }
                            else if (connRNDB.State == ConnectionState.Closed) connRNDB.Open();
                            Object objIllnessId = cmdQueryForIllnessId.ExecuteScalar();
                            if (connRNDB.State == ConnectionState.Open) connRNDB.Close();

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
            String strSqlQueryForIllnessId = "select count([dbo].[tbl_illness].[Illness_Id]) from [dbo].[tbl_illness]";

            SqlCommand cmdQueryForIllnessId = new SqlCommand(strSqlQueryForIllnessId, connRNDB);
            cmdQueryForIllnessId.CommandType = CommandType.Text;

            if (connRNDB.State == ConnectionState.Open)
            {
                connRNDB.Close();
                connRNDB.Open();
            }
            else if (connRNDB.State == ConnectionState.Closed) connRNDB.Open();
            int nIllnessId = (int)cmdQueryForIllnessId.ExecuteScalar();
            if (connRNDB.State == ConnectionState.Open) connRNDB.Close();

            String NewIllnessNo = "ILL-";

            if (nIllnessId > 0)
            {

                if (connRNDB.State != ConnectionState.Closed)
                {
                    connRNDB.Close();
                    connRNDB.Open();
                }
                else if (connRNDB.State == ConnectionState.Closed) connRNDB.Open();

                SqlCommand cmdLastIllnessNo = connRNDB.CreateCommand();
                SqlTransaction tranIllnessNo = connRNDB.BeginTransaction(IsolationLevel.Serializable);

                cmdLastIllnessNo.Connection = connRNDB;
                cmdLastIllnessNo.Transaction = tranIllnessNo;

                try
                {
                    String strSqlQueryForIllnessNo = "select [dbo].[tbl_LastID].[IllnessId] from [dbo].[tbl_LastID] where [dbo].[tbl_LastID].[Id] = 1";

                    cmdLastIllnessNo.CommandText = strSqlQueryForIllnessNo;
                    cmdLastIllnessNo.CommandType = CommandType.Text;

                    Object objLastIllnessNo = cmdLastIllnessNo.ExecuteScalar();

                    String strLastIllnessNo = String.Empty;
                    if (objLastIllnessNo != null)
                    {
                        strLastIllnessNo = objLastIllnessNo.ToString();
                    }

                    int nNewIllnessNo = Int32.Parse(strLastIllnessNo.Substring(4));
                    nNewIllnessNo++;
                    NewIllnessNo += nNewIllnessNo.ToString();

                    String strSqlUpdateLastIllnessNo = "update [dbo].[tbl_LastID] set [dbo].[tbl_LastID].[IllnessId] = @NewIllnessNo where [dbo].[tbl_LastID].[Id] = 1";

                    cmdLastIllnessNo.CommandText = strSqlUpdateLastIllnessNo;
                    cmdLastIllnessNo.CommandType = CommandType.Text;

                    cmdLastIllnessNo.Parameters.AddWithValue("@NewIllnessNo", NewIllnessNo);
                    int nIllnessNoUpdated = cmdLastIllnessNo.ExecuteNonQuery();

                    tranIllnessNo.Commit();
                }
                catch (Exception ex)
                {
                    try
                    {
                        tranIllnessNo.Rollback();
                        MessageBox.Show(ex.Message, "Error");
                        return;
                    }
                    catch (SqlException se)
                    {
                        MessageBox.Show(se.Message, "Sql Error");
                        return;
                    }
                }

                if (connRNDB.State != ConnectionState.Closed) connRNDB.Close();

                frmIllnessCreationPage frmIllnessCreation = new frmIllnessCreationPage();

                frmIllnessCreation.IllnessNo = NewIllnessNo;

                frmIllnessCreation.nLoggedInUserId = nLoggedInUserId;
                frmIllnessCreation.strCaseNo = strCaseIdIllness;
                frmIllnessCreation.strIndividualNo = strIndividualId;
                frmIllnessCreation.MembershipStartDate = MembershipStartDate;

                frmIllnessCreation.ShowDialog(this);


                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //String strSqlQueryForLastIllnessNo = "select [dbo].[tbl_illness].[IllnessNo] from [dbo].[tbl_illness] where [dbo].[tbl_illness].[Illness_Id] = @IllnessId";

                //SqlCommand cmdQueryForLastIllnessNo = new SqlCommand(strSqlQueryForLastIllnessNo, connRNDB);
                //cmdQueryForLastIllnessNo.CommandType = CommandType.Text;

                //cmdQueryForLastIllnessNo.Parameters.AddWithValue("@IllnessId", Int32.Parse(objIllnessId.ToString()));

                //if (connRNDB.State == ConnectionState.Open)
                //{
                //    connRNDB.Close();
                //    connRNDB.Open();
                //}
                //else if (connRNDB.State == ConnectionState.Closed) connRNDB.Open();
                //Object objLastIllnessNo = cmdQueryForLastIllnessNo.ExecuteScalar();
                //if (connRNDB.State == ConnectionState.Open) connRNDB.Close();

                //if (objLastIllnessNo != null)
                //{
                //    String strLastIllnessNo = objLastIllnessNo.ToString().Substring(4);
                //    int LastIllnessNo = Int32.Parse(strLastIllnessNo);
                //    LastIllnessNo++;

                //    NewIllnessNo += LastIllnessNo.ToString();

                //    frmIllnessCreationPage frmIllnessCreation = new frmIllnessCreationPage();

                //    frmIllnessCreation.IllnessNo = NewIllnessNo;

                //    frmIllnessCreation.nLoggedInUserId = nLoggedInUserId;
                //    frmIllnessCreation.strCaseNo = strCaseIdIllness;
                //    frmIllnessCreation.strIndividualNo = strIndividualId;
                //    frmIllnessCreation.MembershipStartDate = MembershipStartDate;

                //    frmIllnessCreation.ShowDialog(this);
                //}
            }
            else if (nIllnessId == 0)
            {

                String NewIllnessId = "ILL-1";

                if (connRNDB.State != ConnectionState.Closed)
                {
                    connRNDB.Close();
                    connRNDB.Open();
                }
                else if (connRNDB.State == ConnectionState.Closed) connRNDB.Open();

                SqlCommand cmdIllnessId = connRNDB.CreateCommand();
                SqlTransaction tranIllnessId = connRNDB.BeginTransaction(IsolationLevel.Serializable);

                cmdIllnessId.Connection = connRNDB;
                cmdIllnessId.Transaction = tranIllnessId;

                try
                {
                    String strSqlUpdateLastIllnessId = "update [dbo].[tbl_LastID] set [dbo].[tbl_LastID].[IllnessId] = @IllnessId where [dbo].[tbl_LastID].[Id] = 1";

                    cmdIllnessId.CommandText = strSqlUpdateLastIllnessId;
                    cmdIllnessId.CommandType = CommandType.Text;

                    cmdIllnessId.Parameters.AddWithValue("@IllnessId", NewIllnessId);

                    int nIllnessIdUpdated = cmdIllnessId.ExecuteNonQuery();

                    tranIllnessId.Commit();
                }
                catch (Exception ex)
                {
                    try
                    {
                        tranIllnessId.Rollback();
                        MessageBox.Show(ex.Message, "Error");
                        return;
                    }
                    catch (SqlException se)
                    {
                        MessageBox.Show(se.Message, "Sql Error");
                        return;
                    }
                }
                if (connRNDB.State != ConnectionState.Closed) connRNDB.Close();

                frmIllnessCreationPage frmIllnessCreation = new frmIllnessCreationPage();

                frmIllnessCreation.IllnessNo = NewIllnessId;

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
                MessageBox.Show("There is no Illness.", "Alert");
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

                        if (connRNDB.State == ConnectionState.Open)
                        {
                            connRNDB.Close();
                            connRNDB.Open();
                        }
                        else if (connRNDB.State == ConnectionState.Closed) connRNDB.Open();

                        String strSqlQueryForIncidentNo = "select [dbo].[tbl_incident].[IncidentNo] from [dbo].[tbl_incident] " +
                                                            "inner join [dbo].[tbl_illness] on [dbo].[tbl_incident].[Illness_id] = [dbo].[tbl_illness].[Illness_Id] " +
                                                            "where [dbo].[tbl_illness].[IllnessNo] = @IllnessNo and " +
                                                            "([dbo].[tbl_incident].[IsDeleted] = 0 or [dbo].[tbl_incident].[IsDeleted] IS NULL)";

                        SqlCommand cmdQueryForIncidentNo = new SqlCommand(strSqlQueryForIncidentNo, connRNDB);

                        cmdQueryForIncidentNo.CommandType = CommandType.Text;
                        cmdQueryForIncidentNo.Parameters.AddWithValue("@IllnessNo", IllnessNo);

                        SqlDataReader rdrIncidentNo = cmdQueryForIncidentNo.ExecuteReader();
                        if (rdrIncidentNo.HasRows)
                        {
                            bIncidentExists = true;
                        }
                        rdrIncidentNo.Close();
                        if (connRNDB.State == ConnectionState.Open) connRNDB.Close();

                        if (bIncidentExists == true)
                        {
                            MessageBox.Show("Cannot delete Illness. Incident on the Illness exists.", "Error");
                            return;
                        }

                        String strSqlDeleteIllness = "update [dbo].[tbl_illness] set [dbo].[tbl_illness].[IsDeleted] = 1 where [dbo].[tbl_illness].[IllnessNo] = @IllnessNo";

                        SqlCommand cmdDeleteIllness = new SqlCommand(strSqlDeleteIllness, connRNDB);

                        cmdDeleteIllness.CommandType = CommandType.Text;
                        cmdDeleteIllness.Parameters.AddWithValue("@IllnessNo", IllnessNo);

                        if (connRNDB.State != ConnectionState.Closed)
                        {
                            connRNDB.Close();
                            connRNDB.Open();
                        }
                        else if (connRNDB.State == ConnectionState.Closed) connRNDB.Open();
                        int nRowDeleted = cmdDeleteIllness.ExecuteNonQuery();
                        if (connRNDB.State != ConnectionState.Closed) connRNDB.Close();
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
