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

        public String strCaseIdIllness = String.Empty;
        public String strIndividualId = String.Empty;

        public SqlConnection connRNDB;
        public String strRNDBConnString = String.Empty;
        public String strSqlGetIllnessForCaseId = String.Empty;

        public Boolean bIllnessSelected;
        public SelectedIllness IllnessSelected;

        public int OldIllnessId;
        public int NewIllnessId;

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
            strRNDBConnString = @"Data Source=CMM-2014U\CMM; Initial Catalog=RN_DB; Integrated Security=True";
            connRNDB = new SqlConnection(strRNDBConnString);

            IllnessSelected = new SelectedIllness();

        }

        private void frmIllness_Load(object sender, EventArgs e)
        {
            strSqlGetIllnessForCaseId = "select [dbo].[tbl_illness].[Illness_Id], [dbo].[tbl_illness].[Individual_Id], [dbo].[tbl_illness].[ICD_10_Id], [dbo].[tbl_illness].[CreateDate], " +
                                        "[dbo].[tbl_illness].[Introduction] from [dbo].[tbl_illness] " +
                                        "where [dbo].[tbl_illness].[Case_Id] = @CaseId and " +
                                        "[dbo].[tbl_illness].[IsDeleted] = 0";

            SqlCommand cmdQueryForIllness = new SqlCommand(strSqlGetIllnessForCaseId, connRNDB);
            cmdQueryForIllness.Parameters.AddWithValue("@CaseId", strCaseIdIllness); ;
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

                    row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrIllnessForCaseId.GetInt32(0).ToString() });
                    row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrIllnessForCaseId.GetString(1) });
                    row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrIllnessForCaseId.GetString(2) });
                    row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrIllnessForCaseId.GetDateTime(3).ToString("MM/dd/yyyy") });
                    row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrIllnessForCaseId.GetString(4) });

                    gvIllness.Rows.Add(row);

                }
            }

            if (connRNDB.State == ConnectionState.Open) connRNDB.Close();

            if (IllnessSelected.IllnessId != String.Empty)
            {
                for (int i = 0; i < gvIllness.RowCount; i++)
                {
                    int Result = 0;

                    if (IllnessSelected.IllnessId == gvIllness[1, i].Value.ToString())
                    {
                        if (Int32.TryParse(IllnessSelected.IllnessId, out Result)) OldIllnessId = Result;
                        gvIllness[0, i].Value = true;
                    }
                }
            }
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

            String strQueryForIllness = "select [dbo].[tbl_illness].[Illness_Id], [dbo].[tbl_illness].[Individual_Id], [dbo].[tbl_illness].[ICD_10_Id], [dbo].[tbl_illness].[CreateDate], " +
                                        "[dbo].[tbl_illness].[Introduction] from [dbo].[tbl_illness] " +
                                        "where [dbo].[tbl_illness].[Case_Id] = @CaseId and " +
                                        "[dbo].[tbl_illness].[IsDeleted] = 0";

            SqlCommand cmdQueryForIllness = new SqlCommand(strQueryForIllness, connRNDB);
            cmdQueryForIllness.CommandType = CommandType.Text;
            cmdQueryForIllness.CommandText = strQueryForIllness;

            cmdQueryForIllness.Parameters.AddWithValue("@CaseId", strCaseIdIllness);

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
                    row.Cells.Add(new DataGridViewTextBoxCell { Value = reader.GetInt32(0).ToString() });
                    row.Cells.Add(new DataGridViewTextBoxCell { Value = reader.GetString(1) });
                    row.Cells.Add(new DataGridViewTextBoxCell { Value = reader.GetString(2) });
                    row.Cells.Add(new DataGridViewTextBoxCell { Value = reader.GetDateTime(3) });
                    row.Cells.Add(new DataGridViewTextBoxCell { Value = reader.GetString(4) });

                    if (IsHandleCreated) AddRowToIllnessSafely(row);
                    else gvIllness.Rows.Add(row);
                }
            }
            if (connRNDB.State == ConnectionState.Open) connRNDB.Close();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (gvIllness.Rows.Count > 0)
            {
                for (int i = 0; i < gvIllness.Rows.Count; i++)
                {
                    if ((Boolean)gvIllness[0, i].Value == true)
                    {
                        IllnessSelected.IllnessId = gvIllness[1, i].Value.ToString();
                        int Result;
                        if (Int32.TryParse(IllnessSelected.IllnessId, out Result)) NewIllnessId = Result;
                        IllnessSelected.ICD10Code = gvIllness[3, i].Value.ToString();
                        DialogResult = DialogResult.OK;

                        SelectedOption = IllnessOption.Select;
                        return;
                    }
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

            if (connRNDB.State == ConnectionState.Open)
            {
                connRNDB.Close();
                connRNDB.Open();
            }
            else if (connRNDB.State == ConnectionState.Closed) connRNDB.Open();
            Object objIllnessId = cmdQueryForIllnessId.ExecuteScalar();
            if (connRNDB.State == ConnectionState.Open) connRNDB.Close();

            if (objIllnessId == null)
            {
                frmIllnessCreationPage frmIllnessCreation = new frmIllnessCreationPage();

                frmIllnessCreation.nIllnessId = 1;
                frmIllnessCreation.nLoggedInUserId = nLoggedInUserId;
                frmIllnessCreation.strCaseNo = strCaseIdIllness;
                frmIllnessCreation.strIndividualNo = strIndividualId;
                frmIllnessCreation.MembershipStartDate = MembershipStartDate;

                frmIllnessCreation.ShowDialog(this);
            }
            else
            {
                frmIllnessCreationPage frmIllnessCreation = new frmIllnessCreationPage();

                int nNewIllnessId = Int32.Parse(objIllnessId.ToString());
                nNewIllnessId++;

                frmIllnessCreation.nIllnessId = nNewIllnessId;
                frmIllnessCreation.nLoggedInUserId = nLoggedInUserId;
                frmIllnessCreation.strCaseNo = strCaseIdIllness;
                frmIllnessCreation.strIndividualNo = strIndividualId;
                frmIllnessCreation.MembershipStartDate = MembershipStartDate;

                frmIllnessCreation.ShowDialog(this);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int? nIllnessIdSelected = null;
            int nNumberOfRowSelected = 0;
            int nRowSelected = 0;

            //for (int i = 0; i < gvIllness.Rows.Count; i++)
            //{
            //    if (gvIllness["Selected", i].Selected)
            //    {
            //        nNumberOfRowSelected++;
            //        nIllnessIdSelected = Int32.Parse(gvIllness["Illness_Id", i].Value.ToString());
            //        nRowSelected = i;
            //    }
            //}

            foreach (DataGridViewRow row in gvIllness.Rows)
            {
                DataGridViewCheckBoxCell chkSelectedCell = row.Cells["Selected"] as DataGridViewCheckBoxCell;

                if (Boolean.Parse(chkSelectedCell.Value.ToString()) == true)
                {
                    nNumberOfRowSelected++;
                    nIllnessIdSelected = Int32.Parse(row.Cells["Illness_Id"].Value.ToString());
                    nRowSelected = row.Index;
                }
            }

            if (nNumberOfRowSelected == 1)
            {
                frmIllnessCreationPage frm = new frmIllnessCreationPage();
                frm.mode = IllnessMode.Edit;
                frm.nLoggedInUserId = nLoggedInUserId;
                frm.strIndividualNo = gvIllness["Individual_Id", nRowSelected].Value.ToString();
                frm.nIllnessId = nIllnessIdSelected;

                frm.ShowDialog();
            }
            else if (nNumberOfRowSelected > 1)
            {
                MessageBox.Show("You have selected more than one Illness.", "Alert");
                return;
            }
            else if (nNumberOfRowSelected == 0)
            {
                MessageBox.Show("Please selected one of Illness.", "Alert");
                return;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (gvIllness.Rows.Count > 0)
            {
                //int nTotalIllnessSelected = 0;
                List<int> lstIllnessToDelete = new List<int>();

                for (int i = 0; i < gvIllness.Rows.Count; i++)
                {
                    if ((Boolean)gvIllness["Selected", i].Value == true)
                    {
                        //nTotalIllnessSelected++;
                        lstIllnessToDelete.Add(Int32.Parse(gvIllness["Illness_Id", i]?.Value?.ToString()));
                    }
                }

                //if (nTotalIllnessSelected > 0)
                if (lstIllnessToDelete.Count > 0)
                {
                    DialogResult dlgResultConfirm = MessageBox.Show("Are you sure to delete these illness?", "Warning", MessageBoxButtons.YesNo);

                    if (dlgResultConfirm == DialogResult.Yes)
                    {
                        try
                        {
                            //Boolean bErrorFlag = false;

                            if (connRNDB.State == ConnectionState.Open)
                            {
                                connRNDB.Close();
                                connRNDB.Open();
                            }
                            else if (connRNDB.State == ConnectionState.Closed) connRNDB.Open();

                            SqlTransaction transDeleteIllness = connRNDB.BeginTransaction();

                            for (int i = 0; i < lstIllnessToDelete.Count; i++)
                            {
                                String strSqlDeleteIllness = "update [dbo].[tbl_illness] set [dbo].[tbl_illness].[IsDeleted] = 1 where [dbo].[tbl_illness].[Illness_Id] = @IllnessId";

                                SqlCommand cmdDeleteIllness = new SqlCommand(strSqlDeleteIllness, connRNDB, transDeleteIllness);

                                cmdDeleteIllness.CommandType = CommandType.Text;
                                cmdDeleteIllness.Parameters.AddWithValue("@IllnessId", lstIllnessToDelete[i]);

                                int nRowDeleted = cmdDeleteIllness.ExecuteNonQuery();

                                //if (nRowDeleted == 0) bErrorFlag = true;
                            }
                            transDeleteIllness.Commit();
                            //if (bErrorFlag)
                            //{
                            //    MessageBox.Show("Some of illness have not been deleted.", "Error");
                            //    return;
                            //}
                        }
                        catch (SqlException ex)
                        {
                            MessageBox.Show(ex.Message, "Error");
                        }
                        finally
                        {
                            if (connRNDB.State == ConnectionState.Open) connRNDB.Close();
                        }
                    }
                    else if (dlgResultConfirm == DialogResult.No)
                    {
                        return;
                    }
                }
                else if (lstIllnessToDelete.Count == 0)
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
        private String strICD10Code;

        public String IllnessId
        {
            get { return strIllnessId; }
            set { strIllnessId = value; }
        }

        public String ICD10Code
        {
            get { return strICD10Code; }
            set { strICD10Code = value; }
        }

        public SelectedIllness()
        {
            strIllnessId = String.Empty;
            strICD10Code = String.Empty;
        }

        public SelectedIllness (String illness_id, String icd10code)
        {
            strIllnessId = illness_id;
            strICD10Code = icd10code;
        }
    }
}
