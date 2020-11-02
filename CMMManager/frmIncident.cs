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
using System.Globalization;

namespace CMMManager
{
    public partial class frmIncident : Form
    {
        public IncidentOption SelectedOption;

        public String IndividualId = String.Empty;
        public String CaseId = String.Empty;
        public String IllnessId = String.Empty;
        public String IllnessNo = String.Empty;
        //public String ICD10Code = String.Empty;

        public int nLoggedInId;
        public UserRole LoggedInUserRole;

        //public Dictionary<int, String> dicProgram;

        public SqlConnection connRNDB;
        public String strRNDBConnString = String.Empty;

        public SqlConnection connSalesforce;
        public String strSalesforceConnString;

        public SelectedIllness IllnessSelected;
        public SelectedIncident IncidentSelected;
        //public Boolean bIncidentSelected = false;
        public Boolean bIncidentCanceled = false;

        //private String strSqlQueryForIncident = String.Empty;
        //private String strSqlInsertNewIncident = String.Empty;

        private Dictionary<String, int> dicProgramId;

        private delegate void AddRowToIncidents(DataGridViewRow row);
        private delegate void RemoveRowIncidents(int nRow);
        private delegate void RemoveAllRowIncidents();


        public frmIncident()
        {
            InitializeComponent();

            strRNDBConnString = @"Data Source=CMM-2014U\CMM; Initial Catalog=RN_DB;Integrated Security=True; Max Pool Size=200; MultipleActiveResultSets=True";
            //strRNDBConnString = @"Data Source=CMM-2014U\CMM; Initial Catalog=RN_DB;User ID=sa;Password=Yny00516; Max Pool Size=200; MultipleActiveResultSets=True";
            connRNDB = new SqlConnection(strRNDBConnString);

            SqlDependency.Start(strRNDBConnString);

            strSalesforceConnString = @"Data Source=CMM-2014U\CMM; Initial Catalog=SalesForce; Integrated Security=True; MultipleActiveResultSets=True";
            connSalesforce = new SqlConnection(strSalesforceConnString);

            IncidentSelected = new SelectedIncident();
            IllnessSelected = new SelectedIllness();

            //dicProgram = new Dictionary<int, string>();
            dicProgramId = new Dictionary<string, int>();

        }

        ~frmIncident()
        {
            SqlDependency.Stop(strRNDBConnString);
        }

        private void frmIncident_Load(object sender, EventArgs e)
        {
            List<IncidentListInfo> lstIncidentListInfo = new List<IncidentListInfo>();

            String strSqlQueryForIncident = "select [dbo].[tbl_incident].[incident_id], [dbo].[tbl_incident].[IncidentNo], [dbo].[tbl_incident].[individual_id], " +
                                            "[dbo].[tbl_incident].[Case_id], [dbo].[tbl_incident].[Illness_id], [dbo].[tbl_illness].[IllnessNo], " +
                                            "[dbo].[tbl_incident].[CreateDate], [dbo].[tbl_program].[ProgramName], [dbo].[tbl_incident].[IncidentNote], " +
                                            "[dbo].[tbl_illness].[ICD_10_Id], [dbo].[tbl_incident].[OccurrenceDate] " +
                                            "from [dbo].[tbl_incident] " +
                                            "inner join [dbo].[tbl_illness] on [dbo].[tbl_incident].[Illness_id] = [dbo].[tbl_illness].[Illness_Id] " +
                                            "inner join [dbo].[tbl_program] on [dbo].[tbl_incident].[Program_id] = [dbo].[tbl_program].[Program_Id] " +
                                            "where [dbo].[tbl_incident].[individual_id] = @IndividualId and " +
                                            "([dbo].[tbl_incident].[IsDeleted] = 0 or [dbo].[tbl_incident].[IsDeleted] IS NULL) " +
                                            "order by [dbo].[tbl_incident].[incident_id]";

            SqlCommand cmdQueryForIncident = new SqlCommand(strSqlQueryForIncident, connRNDB);
            cmdQueryForIncident.CommandType = CommandType.Text;
            cmdQueryForIncident.CommandText = strSqlQueryForIncident;

            cmdQueryForIncident.Parameters.AddWithValue("@IndividualId", IndividualId);

            SqlDependency dependencyIncident = new SqlDependency(cmdQueryForIncident);
            dependencyIncident.OnChange += new OnChangeEventHandler(OnIncidentListChange);

            if (connRNDB.State == ConnectionState.Open)
            {
                connRNDB.Close();
                connRNDB.Open();
            }
            else if (connRNDB.State == ConnectionState.Closed) connRNDB.Open();

            SqlDataReader rdrIncidents = cmdQueryForIncident.ExecuteReader();

            if (rdrIncidents.HasRows)
            {
                while (rdrIncidents.Read())
                {
                    IncidentListInfo info = new IncidentListInfo();

                    if (!rdrIncidents.IsDBNull(0)) info.IncidentId = rdrIncidents.GetInt32(0);
                    else info.IncidentId = null;
                    if (!rdrIncidents.IsDBNull(1)) info.IncidentNo = rdrIncidents.GetString(1);
                    else info.IncidentNo = null;
                    if (!rdrIncidents.IsDBNull(2)) info.IndividualId = rdrIncidents.GetString(2);
                    else info.IndividualId = null;
                    if (!rdrIncidents.IsDBNull(3)) info.CaseNo = rdrIncidents.GetString(3);
                    else info.CaseNo = null;
                    if (!rdrIncidents.IsDBNull(4))
                    {
                        info.IllnessId = rdrIncidents.GetInt32(4);
                        IllnessSelected.IllnessId = info.IllnessId.ToString();
                        IncidentSelected.IllnessId = info.IllnessId.ToString();
                    }
                    else info.IllnessId = null;
                    if (!rdrIncidents.IsDBNull(5)) info.IllnessNo = rdrIncidents.GetString(5);
                    else info.IllnessNo = null;
                    if (!rdrIncidents.IsDBNull(6)) info.CreateDate = rdrIncidents.GetDateTime(6);
                    else info.CreateDate = null;
                    if (!rdrIncidents.IsDBNull(7)) info.ProgramName = rdrIncidents.GetString(7);
                    else info.ProgramName = null;
                    if (!rdrIncidents.IsDBNull(8)) info.IncidentNote = rdrIncidents.GetString(8);
                    else info.IncidentNote = null;
                    if (!rdrIncidents.IsDBNull(8)) info.ICD10Code = rdrIncidents.GetString(9);
                    else info.ICD10Code = null;
                    if (!rdrIncidents.IsDBNull(9)) info.OccurrenceDate = rdrIncidents.GetDateTime(10);
                    else info.OccurrenceDate = null;

                    lstIncidentListInfo.Add(info);

                }
                //gvIncidents.Rows.Clear();

                //while (rdrIncidents.Read())
                //{
                //    DataGridViewRow row = new DataGridViewRow();

                //    row.Cells.Add(new DataGridViewCheckBoxCell { Value = false });
                //    if (!rdrIncidents.IsDBNull(0)) IncidentSelected.IncidentId = rdrIncidents.GetInt32(0).ToString();
                //    if (!rdrIncidents.IsDBNull(1)) row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrIncidents.GetString(1) });
                //    else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                //    if (!rdrIncidents.IsDBNull(4))
                //    {
                //        IllnessSelected.IllnessId = rdrIncidents.GetInt32(4).ToString();
                //        IncidentSelected.IllnessId = rdrIncidents.GetInt32(4).ToString();
                //    }
                //    if (!rdrIncidents.IsDBNull(5)) row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrIncidents.GetString(5) });
                //    else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                //    if (!rdrIncidents.IsDBNull(6)) row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrIncidents.GetDateTime(6).ToString("MM/dd/yyyy") });
                //    else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                //    if (!rdrIncidents.IsDBNull(7)) row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrIncidents.GetString(7) });
                //    else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                //    if (!rdrIncidents.IsDBNull(8)) row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrIncidents.GetString(8) });
                //    else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });

                //    gvIncidents.Rows.Add(row);
                //}
            }
            rdrIncidents.Close();
            if (connRNDB.State == ConnectionState.Open) connRNDB.Close();

            foreach (IncidentListInfo info in lstIncidentListInfo)
            {
                String strSqlQueryForDiseaseNameForICD10Code = "select [dbo].[ICD10 Code].[Name] from [dbo].[ICD10 Code] where [dbo].[ICD10 Code].[ICD10_Code__c] = @ICD10Code";

                SqlCommand cmdQueryForDiseaseName = new SqlCommand(strSqlQueryForDiseaseNameForICD10Code, connSalesforce);
                cmdQueryForDiseaseName.CommandType = CommandType.Text;

                cmdQueryForDiseaseName.Parameters.AddWithValue("@ICD10Code", info.ICD10Code);

                if (connSalesforce.State != ConnectionState.Closed)
                {
                    connSalesforce.Close();
                    connSalesforce.Open();
                }
                else if (connSalesforce.State == ConnectionState.Closed) connSalesforce.Open();
                Object objDiseaseDiscription = cmdQueryForDiseaseName.ExecuteScalar();
                if (connSalesforce.State != ConnectionState.Closed) connSalesforce.Close();

                String strDiseaseDescription = objDiseaseDiscription?.ToString();

                if (strDiseaseDescription != null)
                {
                    info.DiseaseDescription = strDiseaseDescription;
                }
            }

            gvIncidents.Rows.Clear();

            foreach (IncidentListInfo info in lstIncidentListInfo)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.Cells.Add(new DataGridViewCheckBoxCell { Value = false });
                //if (info.IncidentId != null) row.Cells.Add(new DataGridViewTextBoxCell { Value = info.IncidentId });
                //else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                if (info.IncidentNo != null) row.Cells.Add(new DataGridViewTextBoxCell { Value = info.IncidentNo });
                else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                if (info.IllnessNo != null) row.Cells.Add(new DataGridViewTextBoxCell { Value = info.IllnessNo });
                else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                if (info.DiseaseDescription != null) row.Cells.Add(new DataGridViewTextBoxCell { Value = info.DiseaseDescription });
                else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                if (info.OccurrenceDate != null) row.Cells.Add(new DataGridViewTextBoxCell { Value = info.OccurrenceDate.Value.ToString("MM/dd/yyyy") });
                else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                if (info.CreateDate != null) row.Cells.Add(new DataGridViewTextBoxCell { Value = info.CreateDate.Value.ToString("MM/dd/yyyy") });
                else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                if (info.ProgramName != null) row.Cells.Add(new DataGridViewTextBoxCell { Value = info.ProgramName });
                else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                if (info.IncidentNote != null) row.Cells.Add(new DataGridViewTextBoxCell { Value = info.IncidentNote });
                else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });

                gvIncidents.Rows.Add(row);
            }

            //if (IncidentSelected.IncidentId != String.Empty)
            if (IncidentSelected.IncidentNo != null)
            {
                for (int i = 0; i < gvIncidents.RowCount; i++)
                {
                    //if (IncidentSelected.IncidentId == gvIncidents[1, i].Value.ToString()) gvIncidents[0, i].Value = true;
                    if (IncidentSelected.IncidentNo == gvIncidents[1, i].Value.ToString()) gvIncidents[0, i].Value = true;

                }
            }

            String strSqlQueryForProgramId = "select [dbo].[tbl_program].[ProgramName], [dbo].[tbl_program].[Program_Id] from [dbo].[tbl_program]";

            SqlCommand cmdQueryForProgramId = new SqlCommand(strSqlQueryForProgramId, connRNDB);
            cmdQueryForProgramId.CommandType = CommandType.Text;

            if (connRNDB.State == ConnectionState.Open)
            {
                connRNDB.Close();
                connRNDB.Open();
            }
            else if (connRNDB.State == ConnectionState.Closed) connRNDB.Open();
            SqlDataReader rdrProgramId = cmdQueryForProgramId.ExecuteReader();
            if (rdrProgramId.HasRows)
            {
                while(rdrProgramId.Read())
                {
                    if (!rdrProgramId.IsDBNull(0) && !rdrProgramId.IsDBNull(1)) dicProgramId.Add(rdrProgramId.GetString(0).Trim(), rdrProgramId.GetInt16(1));
                }
            }
            rdrProgramId.Close();
            if (connRNDB.State == ConnectionState.Open) connRNDB.Close();

            if (IncidentSelected.IncidentNo != String.Empty)
            {
                for (int i = 0; i < gvIncidents.Rows.Count; i++)
                {
                    if (IncidentSelected.IncidentNo.Trim() == gvIncidents["Incident_No", i]?.Value?.ToString().Trim())
                    {
                        gvIncidents["Selected", i].Value = true;
                    }
                }
            }
        }

        private void AddRowToIncidentsSafely(DataGridViewRow row)
        {
            gvIncidents.BeginInvoke(new AddRowToIncidents(AddNewRowToIncidents), row);
        }

        private void ClearIncidentsSafely()
        {
            gvIncidents.BeginInvoke(new RemoveAllRowIncidents(RemoveAllRowsIncidents));
        }

        private void AddNewRowToIncidents(DataGridViewRow row)
        {
            gvIncidents.Rows.Add(row);
        }

        private void RemoveRowFromIncidents(int i)
        {
            gvIncidents.Rows.RemoveAt(i);
        }

        private void RemoveAllRowsIncidents()
        {
            gvIncidents.Rows.Clear();
        }

        private void OnIncidentListChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type == SqlNotificationType.Change)
            {
                SqlDependency dependency = sender as SqlDependency;
                dependency.OnChange -= OnIncidentListChange;

                UpdateGridViewIncidentList();
            }
        }

        private void UpdateGridViewIncidentList()
        {
            //String strSqlQueryForIncident = "select [dbo].[tbl_incident].[incident_id], [dbo].[tbl_incident].[individual_id], [dbo].[tbl_incident].[Case_id], [dbo].[tbl_incident].[Illness_id], " +
            //                                "[dbo].[tbl_incident].[CreateDate], [dbo].[tbl_program].[ProgramName], [dbo].[tbl_incident].[IncidentNote] " +
            //                                "from ([dbo].[tbl_incident] inner join [dbo].[tbl_illness] on [dbo].[tbl_incident].[Illness_id] = [dbo].[tbl_illness].[Illness_Id]) " +
            //                                "inner join [dbo].[tbl_program] on [dbo].[tbl_incident].[Program_id] = [dbo].[tbl_program].[Program_Id] " +
            //                                "where [dbo].[tbl_incident].[individual_id] = @IndividualId and " +
            //                                "[dbo].[tbl_incident].[Case_id] = @CaseId and " +
            //                                "[dbo].[tbl_incident].[IsDeleted] = 0 " +
            //                                "order by [dbo].[tbl_incident].[incident_id]";

            List<IncidentListInfo> lstIncidentListInfo = new List<IncidentListInfo>();

            String strSqlQueryForIncident = "select [dbo].[tbl_incident].[incident_id], [dbo].[tbl_incident].[IncidentNo], [dbo].[tbl_incident].[individual_id], " +
                                "[dbo].[tbl_incident].[Case_id], [dbo].[tbl_incident].[Illness_id], [dbo].[tbl_illness].[IllnessNo], " +
                                "[dbo].[tbl_incident].[CreateDate], [dbo].[tbl_program].[ProgramName], [dbo].[tbl_incident].[IncidentNote], " +
                                "[dbo].[tbl_illness].[ICD_10_Id], [dbo].[tbl_incident].[OccurrenceDate] " +
                                "from ([dbo].[tbl_incident] " +
                                "inner join [dbo].[tbl_illness] on [dbo].[tbl_incident].[Illness_id] = [dbo].[tbl_illness].[Illness_Id]) " +
                                "inner join [dbo].[tbl_program] on [dbo].[tbl_incident].[Program_id] = [dbo].[tbl_program].[Program_Id] " +
                                "where [dbo].[tbl_incident].[individual_id] = @IndividualId and " +
                                //"[dbo].[tbl_incident].[Case_id] = @CaseId and " +
                                //"[dbo].[tbl_illness].[IllnessNo] = @IllnessNo and " +
                                "([dbo].[tbl_incident].[IsDeleted] = 0 or [dbo].[tbl_incident].[IsDeleted] IS NULL) " +
                                "order by [dbo].[tbl_incident].[incident_id]";

            SqlCommand cmdQueryForIncident = new SqlCommand(strSqlQueryForIncident, connRNDB);
            cmdQueryForIncident.CommandType = CommandType.Text;
            cmdQueryForIncident.CommandText = strSqlQueryForIncident;

            cmdQueryForIncident.Parameters.AddWithValue("@IndividualId", IndividualId);
            //cmdQueryForIncident.Parameters.AddWithValue("@CaseId", CaseId);
            //cmdQueryForIncident.Parameters.AddWithValue("@IllnessNo", IllnessNo);

            SqlDependency dependencyIncident = new SqlDependency(cmdQueryForIncident);
            dependencyIncident.OnChange += new OnChangeEventHandler(OnIncidentListChange);

            if (connRNDB.State != ConnectionState.Closed)
            {
                connRNDB.Close();
                connRNDB.Open();
            }
            else if (connRNDB.State == ConnectionState.Closed) connRNDB.Open();

            SqlDataReader rdrIncidents = cmdQueryForIncident.ExecuteReader();

            //if (IsHandleCreated) ClearIncidentsSafely();
            //else gvIncidents.Rows.Clear();

            if (rdrIncidents.HasRows)
            {
                while (rdrIncidents.Read())
                {

                    IncidentListInfo info = new IncidentListInfo();

                    if (!rdrIncidents.IsDBNull(0)) info.IncidentId = rdrIncidents.GetInt32(0);
                    else info.IncidentId = null;
                    if (!rdrIncidents.IsDBNull(1)) info.IncidentNo = rdrIncidents.GetString(1);
                    else info.IncidentNo = null;
                    if (!rdrIncidents.IsDBNull(2)) info.IndividualId = rdrIncidents.GetString(2);
                    else info.IndividualId = null;
                    if (!rdrIncidents.IsDBNull(3)) info.CaseNo = rdrIncidents.GetString(3);
                    else info.CaseNo = null;
                    if (!rdrIncidents.IsDBNull(4))
                    {
                        info.IllnessId = rdrIncidents.GetInt32(4);
                        IllnessSelected.IllnessId = info.IllnessId.ToString();
                        IncidentSelected.IllnessId = info.IllnessId.ToString();
                    }
                    else info.IllnessId = null;
                    if (!rdrIncidents.IsDBNull(5)) info.IllnessNo = rdrIncidents.GetString(5);
                    else info.IllnessNo = null;
                    if (!rdrIncidents.IsDBNull(6)) info.CreateDate = rdrIncidents.GetDateTime(6);
                    else info.CreateDate = null;
                    if (!rdrIncidents.IsDBNull(7)) info.ProgramName = rdrIncidents.GetString(7);
                    else info.ProgramName = null;
                    if (!rdrIncidents.IsDBNull(8)) info.IncidentNote = rdrIncidents.GetString(8);
                    else info.IncidentNote = null;
                    if (!rdrIncidents.IsDBNull(8)) info.ICD10Code = rdrIncidents.GetString(9);
                    else info.ICD10Code = null;
                    if (!rdrIncidents.IsDBNull(9)) info.OccurrenceDate = rdrIncidents.GetDateTime(10);
                    else info.OccurrenceDate = null;

                    lstIncidentListInfo.Add(info);
                    //DataGridViewRow row = new DataGridViewRow();

                    //row.Cells.Add(new DataGridViewCheckBoxCell { Value = false });
                    //if (!rdrIncidents.IsDBNull(0)) IncidentSelected.IncidentId = rdrIncidents.GetInt32(0).ToString();
                    //if (!rdrIncidents.IsDBNull(1)) row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrIncidents.GetString(1) });
                    //else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                    //if (!rdrIncidents.IsDBNull(4))
                    //{
                    //    IllnessSelected.IllnessId = rdrIncidents.GetInt32(4).ToString();
                    //    IncidentSelected.IllnessId = rdrIncidents.GetInt32(4).ToString();
                    //}
                    //if (!rdrIncidents.IsDBNull(5)) row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrIncidents.GetString(5) });
                    //else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                    //if (!rdrIncidents.IsDBNull(6)) row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrIncidents.GetDateTime(6).ToString("MM/dd/yyyy") });
                    //else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                    //if (!rdrIncidents.IsDBNull(7)) row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrIncidents.GetString(7) });
                    //else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                    //if (!rdrIncidents.IsDBNull(8)) row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrIncidents.GetString(8) });
                    //else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });

                    //if (IsHandleCreated) AddRowToIncidentsSafely(row);
                    //else gvIncidents.Rows.Add(row);
                }
            }
            rdrIncidents.Close();
            if (connRNDB.State == ConnectionState.Open) connRNDB.Close();
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///
            foreach (IncidentListInfo info in lstIncidentListInfo)
            {
                String strSqlQueryForDiseaseNameForICD10Code = "select [dbo].[ICD10 Code].[Name] from [dbo].[ICD10 Code] where [dbo].[ICD10 Code].[ICD10_Code__c] = @ICD10Code";

                SqlCommand cmdQueryForDiseaseName = new SqlCommand(strSqlQueryForDiseaseNameForICD10Code, connSalesforce);
                cmdQueryForDiseaseName.CommandType = CommandType.Text;

                cmdQueryForDiseaseName.Parameters.AddWithValue("@ICD10Code", info.ICD10Code);

                if (connSalesforce.State != ConnectionState.Closed)
                {
                    connSalesforce.Close();
                    connSalesforce.Open();
                }
                else if (connSalesforce.State == ConnectionState.Closed) connSalesforce.Open();
                Object objDiseaseDiscription = cmdQueryForDiseaseName.ExecuteScalar();
                if (connSalesforce.State != ConnectionState.Closed) connSalesforce.Close();

                String strDiseaseDescription = objDiseaseDiscription?.ToString();

                if (strDiseaseDescription != null)
                {
                    info.DiseaseDescription = strDiseaseDescription;
                }
            }

            if (IsHandleCreated) ClearIncidentsSafely();
            else gvIncidents.Rows.Clear();

            foreach (IncidentListInfo info in lstIncidentListInfo)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.Cells.Add(new DataGridViewCheckBoxCell { Value = false });
                //if (info.IncidentId != null) row.Cells.Add(new DataGridViewTextBoxCell { Value = info.IncidentId });
                //else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                if (info.IncidentNo != null) row.Cells.Add(new DataGridViewTextBoxCell { Value = info.IncidentNo });
                else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                if (info.IllnessNo != null) row.Cells.Add(new DataGridViewTextBoxCell { Value = info.IllnessNo });
                else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                if (info.DiseaseDescription != null) row.Cells.Add(new DataGridViewTextBoxCell { Value = info.DiseaseDescription });
                else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                if (info.OccurrenceDate != null) row.Cells.Add(new DataGridViewTextBoxCell { Value = info.OccurrenceDate.Value.ToString("MM/dd/yyyy") });
                else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                if (info.CreateDate != null) row.Cells.Add(new DataGridViewTextBoxCell { Value = info.CreateDate.Value.ToString("MM/dd/yyyy") });
                else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                if (info.ProgramName != null) row.Cells.Add(new DataGridViewTextBoxCell { Value = info.ProgramName });
                else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                if (info.IncidentNote != null) row.Cells.Add(new DataGridViewTextBoxCell { Value = info.IncidentNote });
                else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });

                if (IsHandleCreated) AddRowToIncidentsSafely(row);
                else gvIncidents.Rows.Add(row);

            }



            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            //SqlCommand cmdQueryForIncident = new SqlCommand(strSqlQueryForIncident, connRNDB);
            //cmdQueryForIncident.CommandType = CommandType.Text;
            //cmdQueryForIncident.CommandText = strSqlQueryForIncident;
            //cmdQueryForIncident.Parameters.AddWithValue("@IndividualId", IndividualId);
            //cmdQueryForIncident.Parameters.AddWithValue("@CaseId", CaseId);
            ////cmdQueryForIncident.Parameters.AddWithValue("@ICD10Code", ICD10Code);

            //SqlDependency dependencyIncident = new SqlDependency(cmdQueryForIncident);
            //dependencyIncident.OnChange += new OnChangeEventHandler(OnIncidentListChange);

            //if (connRNDB.State == ConnectionState.Open)
            //{
            //    connRNDB.Close();
            //    connRNDB.Open();
            //}
            //else if (connRNDB.State == ConnectionState.Closed) connRNDB.Open();
            //SqlDataReader rdrIncidents = cmdQueryForIncident.ExecuteReader();

            //if (IsHandleCreated) ClearIncidentsSafely();
            //else gvIncidents.Rows.Clear();

            //if (rdrIncidents.HasRows)
            //{
            //    while (rdrIncidents.Read())
            //    {
            //        DataGridViewRow row = new DataGridViewRow();

            //        row.Cells.Add(new DataGridViewCheckBoxCell { Value = false });
            //        row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrIncidents.GetInt32(0) });
            //        row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrIncidents.GetInt32(3) });
            //        row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrIncidents.GetDateTime(4).ToString("MM/dd/yyyy") });
            //        row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrIncidents.GetString(5) });
            //        row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrIncidents.GetString(6) });

            //        if (IsHandleCreated) AddRowToIncidentsSafely(row);
            //        else gvIncidents.Rows.Add(row);
            //    }
            //}

            //if (connRNDB.State == ConnectionState.Open) connRNDB.Close();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            String strSqlQueryForCountIncidentId = "select count(Incident_id) from [dbo].[tbl_incident]";

            SqlCommand cmdQueryForCountIncidentId = new SqlCommand(strSqlQueryForCountIncidentId, connRNDB);
            cmdQueryForCountIncidentId.CommandType = CommandType.Text;

            if (connRNDB.State == ConnectionState.Open)
            {
                connRNDB.Close();
                connRNDB.Open();
            }
            else if (connRNDB.State == ConnectionState.Closed) connRNDB.Open();
            //int? nMaxIncidentId = int.Parse(cmdQueryForMaxIncidentId.ExecuteScalar().ToString());
            int nCountIncidentId = (int)cmdQueryForCountIncidentId.ExecuteScalar();
            if (connRNDB.State == ConnectionState.Open) connRNDB.Close();

            int? nMaxIncidentId = null;
            int nResultMaxIncidentId = 0;

            String NewIncidentNo = "RNINCD-";

            if (nCountIncidentId > 0)
            {
                if (connRNDB.State != ConnectionState.Closed)
                {
                    connRNDB.Close();
                    connRNDB.Open();
                }
                else if (connRNDB.State == ConnectionState.Closed) connRNDB.Open();

                SqlCommand cmdLastIncidentNo = connRNDB.CreateCommand();
                SqlTransaction tranIncidentNo = connRNDB.BeginTransaction(IsolationLevel.Serializable);

                cmdLastIncidentNo.Connection = connRNDB;
                cmdLastIncidentNo.Transaction = tranIncidentNo;

                try
                {
                    String strSqlQueryForLastIncidentNo = "select [dbo].[tbl_LastID].[IncidentId] from [dbo].[tbl_LastID] where [dbo].[tbl_LastID].[Id] = 1";

                    cmdLastIncidentNo.CommandText = strSqlQueryForLastIncidentNo;
                    cmdLastIncidentNo.CommandType = CommandType.Text;

                    String strLastIncidentNo = String.Empty;
                    Object objLastIncidentNo = cmdLastIncidentNo.ExecuteScalar();

                    if (objLastIncidentNo != null) strLastIncidentNo = objLastIncidentNo.ToString();

                    int nNewIncidentNo = Int32.Parse(strLastIncidentNo.Substring(7));
                    nNewIncidentNo++;

                    NewIncidentNo += nNewIncidentNo.ToString();

                    String strSqlUpdateLastIncidentNo = "update [dbo].[tbl_LastID] set [dbo].[tbl_LastID].[IncidentId] = @NewIncidentNo where [dbo].[tbl_LastID].[Id] = 1";

                    cmdLastIncidentNo.CommandText = strSqlUpdateLastIncidentNo;
                    cmdLastIncidentNo.CommandType = CommandType.Text;

                    cmdLastIncidentNo.Parameters.AddWithValue("@NewIncidentNo", NewIncidentNo);

                    int nIncidentIdUpdated = cmdLastIncidentNo.ExecuteNonQuery();

                    tranIncidentNo.Commit();
                }
                catch (Exception ex)
                {
                    try
                    {
                        tranIncidentNo.Rollback();
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
            }
            else
            {
                NewIncidentNo += '1';

                if (connRNDB.State != ConnectionState.Closed)
                {
                    connRNDB.Close();
                    connRNDB.Open();
                }
                else if (connRNDB.State == ConnectionState.Closed) connRNDB.Open();

                SqlCommand cmdIncidentId = connRNDB.CreateCommand();
                SqlTransaction tranIncidentId = connRNDB.BeginTransaction(IsolationLevel.Serializable);

                cmdIncidentId.Connection = connRNDB;
                cmdIncidentId.Transaction = tranIncidentId;

                try
                {
                    String strUpdateIncidentId = "update [dbo].[tbl_LastID] set [dbo].[tbl_LastID].[IncidentId] = @IncidentId where [dbo].[tbl_LastID].[Id] = 1";

                    cmdIncidentId.CommandText = strUpdateIncidentId;
                    cmdIncidentId.CommandType = CommandType.Text;

                    cmdIncidentId.Parameters.AddWithValue("@IncidentId", NewIncidentNo);
                    int nIncidentIdUpdated = cmdIncidentId.ExecuteNonQuery();

                    tranIncidentId.Commit();
                }
                catch (Exception ex)
                {
                    try
                    {
                        tranIncidentId.Rollback();
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
            }

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            frmIncidentCreationPage frmIncidentCreation = new frmIncidentCreationPage();

            frmIncidentCreation.strIndividualId = IndividualId;
            frmIncidentCreation.strCaseId = CaseId;
            frmIncidentCreation.strIllnessId = IllnessId;
            frmIncidentCreation.strIllnessNo = IllnessNo;
            frmIncidentCreation.nLoggedInId = nLoggedInId;
            frmIncidentCreation.strIncidentNo = NewIncidentNo;
            // Add new incident no to frm

            frmIncidentCreation.mode = frmIncidentCreationPage.IncidentMode.AddNew;



            if (frmIncidentCreation.ShowDialog(this) == DialogResult.OK)
            {
                List<IncidentListInfo> lstIncidentListInfo = new List<IncidentListInfo>();

                String strSqlQueryForIncident = "select [dbo].[tbl_incident].[incident_id], [dbo].[tbl_incident].[IncidentNo], [dbo].[tbl_incident].[individual_id], " +
                                                "[dbo].[tbl_incident].[Case_id], [dbo].[tbl_incident].[Illness_id], [dbo].[tbl_illness].[IllnessNo], " +
                                                "[dbo].[tbl_incident].[CreateDate], [dbo].[tbl_program].[ProgramName], [dbo].[tbl_incident].[IncidentNote], " +
                                                "[dbo].[tbl_incident].[IncidentNote], [dbo].[tbl_incident].[OccurrenceDate] " +
                                                "from ([dbo].[tbl_incident] " +
                                                "inner join [dbo].[tbl_illness] on [dbo].[tbl_incident].[Illness_id] = [dbo].[tbl_illness].[Illness_Id]) " +
                                                "inner join [dbo].[tbl_program] on [dbo].[tbl_incident].[Program_id] = [dbo].[tbl_program].[Program_Id] " +
                                                "where [dbo].[tbl_incident].[individual_id] = @IndividualId and " +
                                                "[dbo].[tbl_incident].[Case_id] = @CaseId and " +
                                                "[dbo].[tbl_illness].[IllnessNo] = @IllnessNo and " +
                                                "[dbo].[tbl_incident].[IsDeleted] = 0 " +
                                                "order by [dbo].[tbl_incident].[incident_id]";

                SqlCommand cmdQueryForIncident = new SqlCommand(strSqlQueryForIncident, connRNDB);
                cmdQueryForIncident.CommandType = CommandType.Text;
                cmdQueryForIncident.CommandText = strSqlQueryForIncident;

                cmdQueryForIncident.Parameters.AddWithValue("@IndividualId", IndividualId);
                cmdQueryForIncident.Parameters.AddWithValue("@CaseId", CaseId);
                cmdQueryForIncident.Parameters.AddWithValue("@IllnessNo", IllnessNo);

                SqlDependency dependencyIncident = new SqlDependency(cmdQueryForIncident);
                dependencyIncident.OnChange += new OnChangeEventHandler(OnIncidentListChange);

                if (connRNDB.State == ConnectionState.Open)
                {
                    connRNDB.Close();
                    connRNDB.Open();
                }
                else if (connRNDB.State == ConnectionState.Closed) connRNDB.Open();

                gvIncidents.Rows.Clear();

                SqlDataReader rdrIncidents = cmdQueryForIncident.ExecuteReader();
                if (rdrIncidents.HasRows)
                {
                    while (rdrIncidents.Read())
                    {

                        IncidentListInfo info = new IncidentListInfo();

                        if (!rdrIncidents.IsDBNull(0)) info.IncidentId = rdrIncidents.GetInt32(0);
                        else info.IncidentId = null;
                        if (!rdrIncidents.IsDBNull(1)) info.IncidentNo = rdrIncidents.GetString(1);
                        else info.IncidentNo = null;
                        if (!rdrIncidents.IsDBNull(2)) info.IndividualId = rdrIncidents.GetString(2);
                        else info.IndividualId = null;
                        if (!rdrIncidents.IsDBNull(3)) info.CaseNo = rdrIncidents.GetString(3);
                        else info.CaseNo = null;
                        if (!rdrIncidents.IsDBNull(4))
                        {
                            info.IllnessId = rdrIncidents.GetInt32(4);
                            IllnessSelected.IllnessId = info.IllnessId.ToString();
                            IncidentSelected.IllnessId = info.IllnessId.ToString();
                        }
                        else info.IllnessId = null;
                        if (!rdrIncidents.IsDBNull(5)) info.IllnessNo = rdrIncidents.GetString(5);
                        else info.IllnessNo = null;
                        if (!rdrIncidents.IsDBNull(6)) info.CreateDate = rdrIncidents.GetDateTime(6);
                        else info.CreateDate = null;
                        if (!rdrIncidents.IsDBNull(7)) info.ProgramName = rdrIncidents.GetString(7);
                        else info.ProgramName = null;
                        if (!rdrIncidents.IsDBNull(8)) info.IncidentNote = rdrIncidents.GetString(8);
                        else info.IncidentNote = null;
                        if (!rdrIncidents.IsDBNull(8)) info.ICD10Code = rdrIncidents.GetString(9);
                        else info.ICD10Code = null;
                        if (!rdrIncidents.IsDBNull(9)) info.OccurrenceDate = rdrIncidents.GetDateTime(10);
                        else info.OccurrenceDate = null;

                        lstIncidentListInfo.Add(info);
                        //DataGridViewRow row = new DataGridViewRow();

                        //row.Cells.Add(new DataGridViewCheckBoxCell { Value = false });
                        //if (!rdrIncidents.IsDBNull(0)) IncidentSelected.IncidentId = rdrIncidents.GetInt32(0).ToString();
                        //if (!rdrIncidents.IsDBNull(1)) row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrIncidents.GetString(1) });
                        //else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                        //if (!rdrIncidents.IsDBNull(4))
                        //{
                        //    IllnessSelected.IllnessId = rdrIncidents.GetInt32(4).ToString();
                        //    IncidentSelected.IllnessId = rdrIncidents.GetInt32(4).ToString();
                        //}
                        //if (!rdrIncidents.IsDBNull(5)) row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrIncidents.GetString(5) });
                        //else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                        //if (!rdrIncidents.IsDBNull(6)) row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrIncidents.GetDateTime(6).ToString("MM/dd/yyyy") });
                        //else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                        //if (!rdrIncidents.IsDBNull(7)) row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrIncidents.GetString(7) });
                        //else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                        //if (!rdrIncidents.IsDBNull(8)) row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrIncidents.GetString(8) });
                        //else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });

                        //gvIncidents.Rows.Add(row);
                    }
                }
                rdrIncidents.Close();
                if (connRNDB.State == ConnectionState.Open) connRNDB.Close();

                foreach (IncidentListInfo info in lstIncidentListInfo)
                {
                    String strSqlQueryForDiseaseNameForICD10Code = "select [dbo].[ICD10 Code].[Name] from [dbo].[ICD10 Code] where [dbo].[ICD10 Code].[ICD10_Code__c] = @ICD10Code";

                    SqlCommand cmdQueryForDiseaseName = new SqlCommand(strSqlQueryForDiseaseNameForICD10Code, connSalesforce);
                    cmdQueryForDiseaseName.CommandType = CommandType.Text;

                    cmdQueryForDiseaseName.Parameters.AddWithValue("@ICD10Code", info.ICD10Code);

                    if (connSalesforce.State != ConnectionState.Closed)
                    {
                        connSalesforce.Close();
                        connSalesforce.Open();
                    }
                    else if (connSalesforce.State == ConnectionState.Closed) connSalesforce.Open();
                    Object objDiseaseDiscription = cmdQueryForDiseaseName.ExecuteScalar();
                    if (connSalesforce.State != ConnectionState.Closed) connSalesforce.Close();

                    String strDiseaseDescription = objDiseaseDiscription?.ToString();

                    if (strDiseaseDescription != null)
                    {
                        info.DiseaseDescription = strDiseaseDescription;
                    }
                }

                //if (IsHandleCreated) ClearIncidentsSafely();
                gvIncidents.Rows.Clear();

                foreach (IncidentListInfo info in lstIncidentListInfo)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.Cells.Add(new DataGridViewCheckBoxCell { Value = false });
                    //if (info.IncidentId != null) row.Cells.Add(new DataGridViewTextBoxCell { Value = info.IncidentId });
                    //else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                    if (info.IncidentNo != null) row.Cells.Add(new DataGridViewTextBoxCell { Value = info.IncidentNo });
                    else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                    if (info.IllnessNo != null) row.Cells.Add(new DataGridViewTextBoxCell { Value = info.IllnessNo });
                    else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                    if (info.DiseaseDescription != null) row.Cells.Add(new DataGridViewTextBoxCell { Value = info.DiseaseDescription });
                    else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                    if (info.OccurrenceDate != null) row.Cells.Add(new DataGridViewTextBoxCell { Value = info.OccurrenceDate.Value.ToString("MM/dd/yyyy") });
                    else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                    if (info.CreateDate != null) row.Cells.Add(new DataGridViewTextBoxCell { Value = info.CreateDate.Value.ToString("MM/dd/yyyy") });
                    else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                    if (info.ProgramName != null) row.Cells.Add(new DataGridViewTextBoxCell { Value = info.ProgramName });
                    else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                    if (info.IncidentNote != null) row.Cells.Add(new DataGridViewTextBoxCell { Value = info.IncidentNote });
                    else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });

                    //if (IsHandleCreated) AddRowToIncidentsSafely(row);
                    gvIncidents.Rows.Add(row);

                }

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            //DialogResult = DialogResult.None;
            //bIncidentSelected = false;
            SelectedOption = IncidentOption.Close;
            Close();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (gvIncidents.Rows.Count > 0)
            {
                Boolean bIncidentSelected = false;
                for (int i = 0; i < gvIncidents.Rows.Count; i++)
                {
                    if ((bool)gvIncidents[0, i].Value == true)
                    {
                        IncidentSelected.IncidentNo = gvIncidents[1, i].Value.ToString();
                        IncidentSelected.IllnessNo = gvIncidents[2, i].Value.ToString();
                        String strSqlQueryForIncidentId = "select [dbo].[tbl_incident].[Incident_id] from [dbo].[tbl_incident] where [dbo].[tbl_incident].[IncidentNo] = @IncidentNo";

                        SqlCommand cmdQueryForIncidentId = new SqlCommand(strSqlQueryForIncidentId, connRNDB);
                        cmdQueryForIncidentId.CommandType = CommandType.Text;

                        cmdQueryForIncidentId.Parameters.AddWithValue("@IncidentNo", IncidentSelected.IncidentNo);

                        if (connRNDB.State == ConnectionState.Open)
                        {
                            connRNDB.Close();
                            connRNDB.Open();
                        }
                        else if (connRNDB.State == ConnectionState.Closed) connRNDB.Open();

                        Object objIncidentId = cmdQueryForIncidentId.ExecuteScalar();
                        if (connRNDB.State == ConnectionState.Open) connRNDB.Close();

                        //int? nIncidentId = null;
                        if (objIncidentId != null)
                        {
                            //nIncidentId = Int32.Parse(objIncidentId.ToString());
                            IncidentSelected.IncidentId = objIncidentId.ToString();
                        }

                        //IncidentSelected.ProgramId = int.Parse(gvIncidents[4, i].Value.ToString());

                        String strProgramName = gvIncidents[6, i].Value.ToString().Trim();
                        //IncidentSelected.ProgramId = dicProgramId[strProgramName];
                        IncidentSelected.ProgramId = dicProgramId[gvIncidents[6, i].Value.ToString().Trim()];
                        IncidentSelected.Note = gvIncidents[7, i].Value.ToString();

                        bIncidentSelected = true;
                        DialogResult = DialogResult.OK;
                        SelectedOption = IncidentOption.Select;

                        return;
                    }
                }
                if (bIncidentSelected == false)
                {
                    MessageBox.Show("Please select in Incident.", "Alert");
                    return;
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            String strIllnessId = String.Empty;
            String strIncidentId = String.Empty;

            String strIllnessNo = String.Empty;
            String strIncidentNo = String.Empty;

            if (gvIncidents.Rows.Count > 0)
            {
                Boolean bIncidentSelected = false;
                for (int i = 0; i < gvIncidents.Rows.Count; i++)
                {
                    if ((Boolean)gvIncidents["Selected", i].Value == true)
                    {
                        IncidentSelected.IncidentNo = gvIncidents["Incident_No", i]?.Value?.ToString();
                        strIncidentNo = IncidentSelected.IncidentNo;
                        IllnessSelected.IllnessNo = gvIncidents["Illness_No", i]?.Value?.ToString();
                        bIncidentSelected = true;
                    }
                }
                if (bIncidentSelected == false)
                {
                    MessageBox.Show("Please select an Incident.", "Alert");
                    return;
                }
            }
            else
            {
                MessageBox.Show("There is no incident for the Illness.", "Alert");
                return;
            }

            strIllnessNo = IllnessSelected.IllnessNo;
            strIncidentNo = IncidentSelected.IncidentNo;

            if (strIncidentNo != String.Empty)
            {

                frmIncidentCreationPage frmIncd = new frmIncidentCreationPage();
                frmIncd.strIllnessId = IllnessSelected.IllnessId;
                frmIncd.strIllnessNo = IllnessSelected.IllnessNo;
                frmIncd.strIncidentId = IncidentSelected.IncidentId;
                frmIncd.strIncidentNo = IncidentSelected.IncidentNo;
                frmIncd.strCaseId = CaseId;
                frmIncd.strIndividualId = IndividualId;
                frmIncd.nLoggedInId = nLoggedInId;
                frmIncd.mode = frmIncidentCreationPage.IncidentMode.Edit;

                frmIncd.ShowDialog();

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (gvIncidents.Rows.Count > 0)
            {
                //int nTotalIncidentSelected = 0;
                List<String> lstIncidentsToDelete = new List<String>();

                for(int i = 0; i < gvIncidents.Rows.Count; i++)
                {
                    if ((Boolean)gvIncidents["Selected", i].Value == true)
                    {
                        //nTotalIncidentSelected++;
                        lstIncidentsToDelete.Add(gvIncidents["Incident_No", i]?.Value?.ToString());
                    }
                }
                //if (nTotalIncidentSelected > 0)

                if (lstIncidentsToDelete.Count > 0)
                {
                    try
                    {
                        DialogResult dlgResultConfirm = MessageBox.Show("Are you sure to delete these incidents?", "Waring", MessageBoxButtons.YesNo);

                        if (dlgResultConfirm == DialogResult.Yes)
                        {
                            Boolean bErrorFlag = false;

                            if (connRNDB.State == ConnectionState.Open)
                            {
                                connRNDB.Close();
                                connRNDB.Open();
                            }
                            else if (connRNDB.State == ConnectionState.Closed) connRNDB.Open();

                            // Begin transaction here
                            SqlTransaction transDelete = connRNDB.BeginTransaction();

                            for (int i = 0; i < lstIncidentsToDelete.Count; i++)
                            {
                                String strSqlDeleteIncident = "update [dbo].[tbl_incident] set [dbo].[tbl_incident].[IsDeleted] = 1 where [dbo].[tbl_incident].[IncidentNo] = @IncidentNo";

                                SqlCommand cmdDeleteIncident = new SqlCommand(strSqlDeleteIncident, connRNDB, transDelete);;
                                cmdDeleteIncident.CommandType = CommandType.Text;

                                cmdDeleteIncident.Parameters.AddWithValue("@IncidentNo", lstIncidentsToDelete[i]);

                                int nRowDeleted = cmdDeleteIncident.ExecuteNonQuery();
                                //if (nRowDeleted == 0) bErrorFlag = true;
                            }

                            transDelete.Commit();

                            //if (bErrorFlag)
                            //{
                            //    MessageBox.Show("Some of incident have not been deleted.", "Error");
                            //    return;
                            //}
                        }
                        if (dlgResultConfirm == DialogResult.No)
                        {
                            return;
                        }
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
                else if (lstIncidentsToDelete.Count == 0)
                {
                    MessageBox.Show("Please select incidents to delete");
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            SelectedOption = IncidentOption.Cancel;
            Close();
        }

        private void gvIncidents_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
    }

    public class SelectedIncident
    {
        private String strIncidentId;
        private String strIncidentNo;
        private String strCaseId;
        private String strIllnessId;
        private String strIllnessNo;
        private int nIncidentStatus;
        private int nProgramId;
        private String strNote;

        public String IncidentId
        {
            get { return strIncidentId; }
            set { strIncidentId = value; }
        }

        public String IncidentNo
        {
            get { return strIncidentNo; }
            set { strIncidentNo = value; }
        }

        public String CaseId
        {
            get { return strCaseId; }
            set { strCaseId = value; }
        }

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

        public int IncidentStatus
        {
            get { return nIncidentStatus; }
            set { nIncidentStatus = value; }
        }

        public int ProgramId
        {
            get { return nProgramId; }
            set { nProgramId = value; }
        }

        public String Note
        {
            get { return strNote; }
            set { strNote = value; }
        }

        public SelectedIncident()
        {
            strIncidentId = String.Empty;
            strCaseId = String.Empty;
            strIllnessNo = String.Empty;
            nIncidentStatus = 2;
            nProgramId = 0;
            Note = String.Empty;
        }

        public SelectedIncident(String incident_id, String case_id, String illness_no, int incident_status, int program_id, String note)
        {
            strIncidentId = incident_id;
            strCaseId = case_id;
            strIllnessNo = illness_no;
            nIncidentStatus = incident_status;
            nProgramId = program_id;
            Note = note;
        }
    }
}
