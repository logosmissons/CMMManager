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

    public partial class frmIncidentCreationPage : Form
    {

        public enum IncidentMode { AddNew, Edit };

        private String connStringSalesforce;
        private SqlConnection connSalesforce;
        private SqlCommand cmd_icd10;
        private String strSqlForICD10Codes;

        private String connStringRN;
        private SqlConnection connRNDB;

        private String strCaseNoConnection;
        private SqlConnection connCaseNo;

        private List<ICD10CodeInfo> lstICD10CodeInfo;

        private Dictionary<int, String> dicProgram;

        public String strCaseId = String.Empty;
        public String strIncidentId = String.Empty;
        public String strIncidentNo = String.Empty;
        public String strIndividualId = String.Empty;
        public String strIllnessId = String.Empty;
        public String strIllnessNo = String.Empty;

        //public IllnessMode mode;
        public IncidentMode mode;
        public int nLoggedInId;

        public void SetSqlSetting()
        {
            connStringSalesforce = @"Data Source=10.1.10.60\CMM; Initial Catalog=SalesForce;Integrated Security=True; Max Pool Size=200; MultipleActiveResultSets=True";
            //connStringSalesforce = @"Data Source=10.1.10.60\CMM; Initial Catalog=SalesForce;User ID=sa;Password=Yny00516; Max Pool Size=200; MultipleActiveResultSets=True";
            connSalesforce = new SqlConnection(connStringSalesforce);

            strSqlForICD10Codes = "select id, name, icd10_code__c from [ICD10 Code]";

            connStringRN = @"Data Source=10.1.10.60\CMM; Initial Catalog=RN_DB; Integrated Security=True; Max Pool Size=200; MultipleActiveResultSets=True";
            //connStringRN = @"Data Source=10.1.10.60\CMM; Initial Catalog=RN_DB; User ID=sa;Password=Yny00516; Max Pool Size=200; MultipleActiveResultSets=True";
            connRNDB = new SqlConnection(connStringRN);

            dicProgram = new Dictionary<int, string>();
        }


        public frmIncidentCreationPage()
        {
            InitializeComponent();
            strIndividualId = String.Empty;
            SetSqlSetting();
        }

        private void frmIncidentCreationPage_Load(object sender, EventArgs e)
        {
            txtCaseNo.Text = strCaseId.Trim();
            txtIllnessNo.Text = strIllnessNo.Trim();
         

            if (mode == IncidentMode.AddNew)
            {
                //String strSqlQueryForCountIncidentId = "select count(Incident_id) from [dbo].[tbl_incident]";

                //SqlCommand cmdQueryForCountIncidentId = new SqlCommand(strSqlQueryForCountIncidentId, connRNDB);
                //cmdQueryForCountIncidentId.CommandType = CommandType.Text;

                //if (connRNDB.State == ConnectionState.Open)
                //{
                //    connRNDB.Close();
                //    connRNDB.Open();
                //}
                //else if (connRNDB.State == ConnectionState.Closed) connRNDB.Open();
                ////int? nMaxIncidentId = int.Parse(cmdQueryForMaxIncidentId.ExecuteScalar().ToString());
                //int nCountIncidentId = (int)cmdQueryForCountIncidentId.ExecuteScalar();
                //if (connRNDB.State == ConnectionState.Open) connRNDB.Close();

                //int? nMaxIncidentId = null;
                //int nResultMaxIncidentId = 0;

                //String NewIncidentNo = "INCD-";

                //if (nCountIncidentId > 0)
                //{
                //    if (connRNDB.State != ConnectionState.Closed)
                //    {
                //        connRNDB.Close();
                //        connRNDB.Open();
                //    }
                //    else if (connRNDB.State == ConnectionState.Closed) connRNDB.Open();

                //    SqlCommand cmdLastIncidentNo = connRNDB.CreateCommand();
                //    SqlTransaction tranIncidentNo = connRNDB.BeginTransaction(IsolationLevel.Serializable);

                //    cmdLastIncidentNo.Connection = connRNDB;
                //    cmdLastIncidentNo.Transaction = tranIncidentNo;

                //    try
                //    {
                //        String strSqlQueryForLastIncidentNo = "select [dbo].[tbl_LastID].[IncidentId] from [dbo].[tbl_LastID] where [dbo].[tbl_LastID].[Id] = 1";

                //        cmdLastIncidentNo.CommandText = strSqlQueryForLastIncidentNo;
                //        cmdLastIncidentNo.CommandType = CommandType.Text;

                //        String strLastIncidentNo = String.Empty;
                //        Object objLastIncidentNo = cmdLastIncidentNo.ExecuteScalar();

                //        if (objLastIncidentNo != null) strLastIncidentNo = objLastIncidentNo.ToString();

                //        int nNewIncidentNo = Int32.Parse(strLastIncidentNo.Substring(5));
                //        nNewIncidentNo++;

                //        NewIncidentNo += nNewIncidentNo.ToString();

                //        String strSqlUpdateLastIncidentNo = "update [dbo].[tbl_LastID] set [dbo].[tbl_LastID].[IncidentId] = @NewIncidentNo where [dbo].[tbl_LastID].[Id] = 1";

                //        cmdLastIncidentNo.CommandText = strSqlUpdateLastIncidentNo;
                //        cmdLastIncidentNo.CommandType = CommandType.Text;

                //        cmdLastIncidentNo.Parameters.AddWithValue("@NewIncidentNo", NewIncidentNo);

                //        int nIncidentIdUpdated = cmdLastIncidentNo.ExecuteNonQuery();

                //        tranIncidentNo.Commit();
                //    }
                //    catch (Exception ex)
                //    {
                //        try
                //        {
                //            tranIncidentNo.Rollback();
                //            MessageBox.Show(ex.Message, "Error");
                //            return;
                //        }
                //        catch (SqlException se)
                //        {
                //            MessageBox.Show(se.Message, "Sql Error");
                //            return;
                //        }
                //    }
                //    if (connRNDB.State != ConnectionState.Closed) connRNDB.Close();
                //}
                //else
                //{
                //    NewIncidentNo += '1';

                //    if (connRNDB.State != ConnectionState.Closed)
                //    {
                //        connRNDB.Close();
                //        connRNDB.Open();
                //    }
                //    else if (connRNDB.State == ConnectionState.Closed) connRNDB.Open();

                //    SqlCommand cmdIncidentId = connRNDB.CreateCommand();
                //    SqlTransaction tranIncidentId = connRNDB.BeginTransaction(IsolationLevel.Serializable);

                //    cmdIncidentId.Connection = connRNDB;
                //    cmdIncidentId.Transaction = tranIncidentId;

                //    try
                //    {
                //        String strUpdateIncidentId = "update [dbo].[tbl_LastID] set [dbo].[tbl_LastID].[IncidentId] = @IncidentId where [dbo].[tbl_LastID].[Id] = 1";

                //        cmdIncidentId.CommandText = strUpdateIncidentId;
                //        cmdIncidentId.CommandType = CommandType.Text;

                //        cmdIncidentId.Parameters.AddWithValue("@IncidentId", NewIncidentNo);
                //        int nIncidentIdUpdated = cmdIncidentId.ExecuteNonQuery();

                //        tranIncidentId.Commit();
                //    }
                //    catch (Exception ex)
                //    {
                //        try
                //        {
                //            tranIncidentId.Rollback();
                //            MessageBox.Show(ex.Message, "Error");
                //            return;
                //        }
                //        catch (SqlException se)
                //        {
                //            MessageBox.Show(se.Message, "Sql Error");
                //            return;
                //        }
                //    }
                //    if (connRNDB.State != ConnectionState.Closed) connRNDB.Close();
                //}

                //txtIncidentNo.Text = NewIncidentNo;
                txtIncidentNo.Text = strIncidentNo;
                

                String strSqlQueryForIllnessIntro = "select [dbo].[tbl_illness].[Introduction] from [dbo].[tbl_illness] where [dbo].[tbl_illness].[IllnessNo] = @IllnessNo";

                SqlCommand cmdQueryForIllnessIntro = new SqlCommand(strSqlQueryForIllnessIntro, connRNDB);
                cmdQueryForIllnessIntro.CommandType = CommandType.Text;
                cmdQueryForIllnessIntro.CommandText = strSqlQueryForIllnessIntro;
                cmdQueryForIllnessIntro.Parameters.AddWithValue("@IllnessNo", strIllnessNo.Trim());

                if (connRNDB.State == ConnectionState.Open)
                {
                    connRNDB.Close();
                    connRNDB.Open();
                }
                else if (connRNDB.State == ConnectionState.Closed) connRNDB.Open();
                //String strIllnessIntro = cmdQueryForIllnessIntro.ExecuteScalar().ToString();
                Object objIllnessIntro = cmdQueryForIllnessIntro.ExecuteScalar();
                if (connRNDB.State == ConnectionState.Open) connRNDB.Close();

                String strIllnessIntro = String.Empty;
                if (objIllnessIntro != null) strIllnessIntro = objIllnessIntro.ToString();

                String strSqlQueryForICD10Code = "select [dbo].[tbl_illness].[ICD_10_Id] from [dbo].[tbl_illness] where [dbo].[tbl_illness].[IllnessNo] = @IllnessNo";

                SqlCommand cmdQueryForICD10Code = new SqlCommand(strSqlQueryForICD10Code, connRNDB);
                cmdQueryForICD10Code.CommandType = CommandType.Text;
                cmdQueryForICD10Code.Parameters.AddWithValue("@IllnessNo", strIllnessNo.Trim());

                if (connRNDB.State == ConnectionState.Open)
                {
                    connRNDB.Close();
                    connRNDB.Open();
                }
                else if (connRNDB.State == ConnectionState.Closed) connRNDB.Open();
                //String strICD10Code = cmdQueryForICD10Code.ExecuteScalar().ToString();
                Object objICD10Code = cmdQueryForICD10Code.ExecuteScalar();
                if (connRNDB.State == ConnectionState.Open) connRNDB.Close();

                String strICD10Code = String.Empty;
                if (objICD10Code != null)
                {
                    strICD10Code = objICD10Code.ToString();
                }
                //else
                //{
                //    MessageBox.Show("No ICD 10 Code", "Error", MessageBoxButtons.OK);
                //    return;
                //}

                if (strICD10Code != String.Empty) txtICD10Code.Text = strICD10Code;

                String strSqlQueryForDiseaseName = "select [dbo].[ICD10 Code].[Name] from [dbo].[ICD10 Code] where [dbo].[ICD10 Code].[ICD10_CODE__C] = @ICD10Code";

                SqlCommand cmdQueryForDiseaseName = new SqlCommand(strSqlQueryForDiseaseName, connSalesforce);

                cmdQueryForDiseaseName.CommandType = CommandType.Text;
                cmdQueryForDiseaseName.Parameters.AddWithValue("@ICD10Code", strICD10Code);

                if (connSalesforce.State == ConnectionState.Open)
                {
                    connSalesforce.Close();
                    connSalesforce.Open();
                }
                else if (connSalesforce.State == ConnectionState.Closed) connSalesforce.Open();
                //String strDiseaseName = cmdQueryForDiseaseName.ExecuteScalar().ToString();
                Object objDiseaseName = cmdQueryForDiseaseName.ExecuteScalar();
                if (connSalesforce.State == ConnectionState.Open) connSalesforce.Close();

                String strDiseaseName = String.Empty;

                if (objDiseaseName != null)
                {
                    strDiseaseName = objDiseaseName.ToString();
                }
                //else
                //{
                //    MessageBox.Show("No Disease Name", "Error", MessageBoxButtons.OK);
                //    return;
                //}

                if (strDiseaseName != String.Empty) txtICD10Name.Text = strDiseaseName.Trim();

                ////////////////////////////////////////////////////////////////////////////////////////////////////

                String strSqlQueryForPrograms = "select [dbo].[tbl_program].[Program_Id], [dbo].[tbl_program].[ProgramName] from [dbo].[tbl_program]";
                SqlCommand cmdQueryForPrograms = new SqlCommand(strSqlQueryForPrograms, connRNDB);
                cmdQueryForPrograms.CommandType = CommandType.Text;

                if (connRNDB.State == ConnectionState.Open)
                {
                    connRNDB.Close();
                    connRNDB.Open();
                }
                else if (connRNDB.State == ConnectionState.Closed) connRNDB.Open();
                SqlDataReader rdrPrograms = cmdQueryForPrograms.ExecuteReader();
                if (rdrPrograms.HasRows)
                {
                    while (rdrPrograms.Read())
                    {
                        if (!rdrPrograms.IsDBNull(0) && !rdrPrograms.IsDBNull(1)) dicProgram.Add(rdrPrograms.GetInt16(0), rdrPrograms.GetString(1));
                    }
                }
                rdrPrograms.Close();
                if (connRNDB.State == ConnectionState.Open) connRNDB.Close();

                for (int i = 0; i < dicProgram.Count; i++) comboProgram.Items.Add(dicProgram[i]);

                comboProgram.SelectedIndex = 0;

                dtpCreateDate.Enabled = false;
                dtpModifiedDate.Enabled = false;
            }
            else if (mode == IncidentMode.Edit)
            {
                // Populate the programs in comboBox
                String strSqlQueryForPrograms = "select [dbo].[tbl_program].[Program_Id], [dbo].[tbl_program].[ProgramName] from [dbo].[tbl_program]";
                SqlCommand cmdQueryForPrograms = new SqlCommand(strSqlQueryForPrograms, connRNDB);
                cmdQueryForPrograms.CommandType = CommandType.Text;

                if (connRNDB.State == ConnectionState.Open)
                {
                    connRNDB.Close();
                    connRNDB.Open();
                }
                else if (connRNDB.State == ConnectionState.Closed) connRNDB.Open();
                SqlDataReader rdrPrograms = cmdQueryForPrograms.ExecuteReader();
                if (rdrPrograms.HasRows)
                {
                    while (rdrPrograms.Read())
                    {
                        if (!rdrPrograms.IsDBNull(0) && !rdrPrograms.IsDBNull(1)) dicProgram.Add(rdrPrograms.GetInt16(0), rdrPrograms.GetString(1));
                    }
                }
                rdrPrograms.Close();
                if (connRNDB.State == ConnectionState.Open) connRNDB.Close();

                for (int i = 0; i < dicProgram.Count; i++) comboProgram.Items.Add(dicProgram[i]);

                // Populate main form
                //String strSqlQueryForIncident = "select [dbo].[tbl_incident].[Case_id], [dbo].[tbl_incident].[Illness_Id], [dbo].[tbl_incident].[Program_id], " +
                //                                "[dbo].[tbl_incident].[CreateDate], [dbo].[tbl_incident].[ModifiDate], [dbo].[tbl_incident].[IncidentNote] " +
                //                                "from [dbo].[tbl_incident] " +
                //                                "where [dbo].[tbl_incident].[Incident_id] = @IncidentId and [dbo].[tbl_incident].[Individual_id] = @IndividualId";

                String strSqlQueryForIncident = "select [dbo].[tbl_incident].[Case_id], [dbo].[tbl_illness].[IllnessNo], [dbo].[tbl_incident].[IncidentNo], " +
                                "[dbo].[tbl_incident].[Program_id], [dbo].[tbl_incident].[IsWellBeing], " +
                                "[dbo].[tbl_incident].[CreateDate], [dbo].[tbl_incident].[ModifiDate], [dbo].[tbl_incident].[IncidentNote], " +
                                "[dbo].[tbl_incident].[OccurrenceDate] " +
                                "from [dbo].[tbl_incident] " +
                                "inner join [dbo].[tbl_illness] on [dbo].[tbl_incident].[Illness_Id] = [dbo].[tbl_illness].[Illness_Id] " +
                                "where [dbo].[tbl_incident].[IncidentNo] = @IncidentNo and [dbo].[tbl_incident].[Individual_id] = @IndividualId";

                //"where [dbo].[tbl_incident].[Incident_id] = @IncidentId and [dbo].[tbl_incident].[Individual_id] = @IndividualId";

                SqlCommand cmdQueryForIncident = new SqlCommand(strSqlQueryForIncident, connRNDB);
                cmdQueryForIncident.CommandType = CommandType.Text;

                int result = 0;
                int nIncident = 0;
                //int nIndividualId = 0;
                //if (int.TryParse(strIncidentId, NumberStyles.Number, new CultureInfo("en-US"), out result))
                //{
                //    nIncident = result;
                //    cmdQueryForIncident.Parameters.AddWithValue("@IncidentId", nIncident);
                //}
                //txtIncidentId.Text = nIncident.ToString();
                cmdQueryForIncident.Parameters.AddWithValue("@IncidentNo", strIncidentNo);
                cmdQueryForIncident.Parameters.AddWithValue("@IndividualId", strIndividualId);

                //if (int.TryParse(strIndividualId, NumberStyles.Number, new CultureInfo("en-US"), out result))
                //{
                //    nIndividualId = result;
                //    cmdQueryForIncident.Parameters.AddWithValue("@IndividualId", nIndividualId);
                //}

                if (connRNDB.State == ConnectionState.Open)
                {
                    connRNDB.Close();
                    connRNDB.Open();
                }
                else if (connRNDB.State == ConnectionState.Closed) connRNDB.Open();
                SqlDataReader rdrIncident = cmdQueryForIncident.ExecuteReader();
                if (rdrIncident.HasRows)
                {
                    rdrIncident.Read();
                    if (!rdrIncident.IsDBNull(0)) txtCaseNo.Text = rdrIncident.GetString(0);
                    if (!rdrIncident.IsDBNull(1)) txtIllnessNo.Text = rdrIncident.GetString(1);
                    if (!rdrIncident.IsDBNull(2)) txtIncidentNo.Text = rdrIncident.GetString(2);
                    if (!rdrIncident.IsDBNull(3)) comboProgram.SelectedIndex = rdrIncident.GetInt16(3);
                    if (!rdrIncident.IsDBNull(4)) chkWellBeing.Checked = rdrIncident.GetBoolean(4);
                    if (!rdrIncident.IsDBNull(5)) dtpCreateDate.Text = rdrIncident.GetDateTime(5).ToString("MM/dd/yyyy");
                    if (!rdrIncident.IsDBNull(6)) dtpModifiedDate.Text = rdrIncident.GetDateTime(6).ToString("MM/dd/yyyy");
                    if (!rdrIncident.IsDBNull(7)) txtIncidentNote.Text = rdrIncident.GetString(7);
                    if (!rdrIncident.IsDBNull(8))
                    {
                        dtpIncdOccurrenceDate.Checked = true;
                        dtpIncdOccurrenceDate.Text = rdrIncident.GetDateTime(8).ToString("MM/dd/yyyy");
                    }
                }
                rdrIncident.Close();
                if (connRNDB.State == ConnectionState.Open) connRNDB.Close();

                strIllnessNo = txtIllnessNo.Text.Trim();
                String strSqlQueryForICD10Code = "select [dbo].[tbl_illness].[ICD_10_Id] from [dbo].[tbl_illness] " +
                                                 "where [dbo].[tbl_illness].[Illness_Id] = @IllnessId and [dbo].[tbl_illness].[Individual_Id] = @IndividualId";

                SqlCommand cmdQueryForICD10Code = new SqlCommand(strSqlQueryForICD10Code, connRNDB);
                cmdQueryForICD10Code.CommandType = CommandType.Text;
                //cmdQueryForICD10Code.Parameters.AddWithValue("@IllnessId", strIllnessNo.Trim());
                cmdQueryForICD10Code.Parameters.AddWithValue("@IllnessId", Int32.Parse(strIllnessId));
                cmdQueryForICD10Code.Parameters.AddWithValue("@IndividualId", strIndividualId.Trim());

                if (connRNDB.State == ConnectionState.Open)
                {
                    connRNDB.Close();
                    connRNDB.Open();
                }
                else if (connRNDB.State == ConnectionState.Closed) connRNDB.Open();
                //String strICD10Code = cmdQueryForICD10Code.ExecuteScalar().ToString();
                Object objICD10Code = cmdQueryForICD10Code.ExecuteScalar();
                if (connRNDB.State == ConnectionState.Open) connRNDB.Close();

                String strICD10Code = String.Empty;

                if (objICD10Code != null)
                {
                    strICD10Code = objICD10Code.ToString();
                }
                else
                {
                    MessageBox.Show("No ICD 10 Code for given Illness", "Error", MessageBoxButtons.OK);
                    return;
                }

                if (strICD10Code != String.Empty) txtICD10Code.Text = strICD10Code;

                String strSqlQueryForDiseaseName = "select [dbo].[ICD10 Code].[Name] from [dbo].[ICD10 Code] where [dbo].[ICD10 Code].[ICD10_CODE__C] = @ICD10Code";

                SqlCommand cmdQueryForDiseaseName = new SqlCommand(strSqlQueryForDiseaseName, connSalesforce);

                cmdQueryForDiseaseName.CommandType = CommandType.Text;
                cmdQueryForDiseaseName.Parameters.AddWithValue("@ICD10Code", strICD10Code);

                if (connSalesforce.State == ConnectionState.Open)
                {
                    connSalesforce.Close();
                    connSalesforce.Open();
                }
                else if (connSalesforce.State == ConnectionState.Closed) connSalesforce.Open();
                //String strDiseaseName = cmdQueryForDiseaseName.ExecuteScalar().ToString();
                Object objDiseaseName = cmdQueryForDiseaseName.ExecuteScalar();
                if (connSalesforce.State == ConnectionState.Open) connSalesforce.Close();

                String strDiseaseName = String.Empty;
                if (objDiseaseName != null)
                {
                    strDiseaseName = objDiseaseName.ToString();
                }
                //else
                //{
                //    MessageBox.Show("No Disease name", "Error", MessageBoxButtons.OK);
                //    return;
                //}

                if (strDiseaseName != String.Empty) txtICD10Name.Text = strDiseaseName.Trim();

                dtpCreateDate.Enabled = false;
                dtpModifiedDate.Enabled = false;

            }


            //comboProgram.SelectedIndex = 0;

            //String strQueryForDistinctCaseNoInIllness = "select distinct Case_Id from dbo.tbl_illness where Individual_Id = '" + strIndividualId + "'";

            //SqlCommand cmdQueryForDistinctCaseNoInIllness = connRNDB.CreateCommand();
            //cmdQueryForDistinctCaseNoInIllness.CommandType = CommandType.Text;
            //cmdQueryForDistinctCaseNoInIllness.CommandText = strQueryForDistinctCaseNoInIllness;

            //DataTable dtCaseId = new DataTable();

            //connRNDB.Open();
            //SqlDataReader rdrCaseIds = cmdQueryForDistinctCaseNoInIllness.ExecuteReader();
            //dtCaseId.Load(rdrCaseIds);
            //connRNDB.Close();

            //if (dtCaseId.Rows.Count > 0)
            //{
            //    cbDistinctCases.DataSource = dtCaseId;
            //    cbDistinctCases.DisplayMember = "Case_Id";
            //    cbDistinctCases.ValueMember = "Case_Id";
            //}

            //String strCaseIdSelected = cbDistinctCases.SelectedValue.ToString().Trim();
            //String strQueryForDistinctIllnessId = "select Illness_Id from dbo.tbl_illness where Case_id = '" + strCaseIdSelected + "' and " + 
            //                                      "Individual_Id = '" + strIndividualId.Trim() + "'";

            //SqlCommand cmdQueryForDistinctIllnessId = connRNDB.CreateCommand();
            //cmdQueryForDistinctIllnessId.CommandType = CommandType.Text;
            //cmdQueryForDistinctIllnessId.CommandText = strQueryForDistinctIllnessId;

            //DataTable dtIllnessId = new DataTable();

            //connRNDB.Open();
            //SqlDataReader rdrIllnessIds = cmdQueryForDistinctIllnessId.ExecuteReader();
            //dtIllnessId.Load(rdrIllnessIds);
            //connRNDB.Close();

            //if (dtIllnessId.Rows.Count > 0)
            //{
            //    cbIllnessId.DataSource = dtIllnessId;
            //    cbIllnessId.DisplayMember = "Illness_Id";
            //    cbIllnessId.ValueMember = "Illness_Id";
            //}

            //String strQueryForIllness = "select Introduction from dbo.tbl_illness where Illness_Id = '" + cbIllnessId.SelectedValue.ToString().Trim() + "'";

            //SqlCommand cmdQueryForIllness = connRNDB.CreateCommand();
            //cmdQueryForIllness.CommandType = CommandType.Text;
            //cmdQueryForIllness.CommandText = strQueryForIllness;

            //connRNDB.Open();
            //String strIllness = cmdQueryForIllness.ExecuteScalar().ToString();
            //connRNDB.Close();

            //txtIllness.Text = strIllness.Trim();

            //String strQueryForICD10Code = "select ICD_10_Id from dbo.tbl_illness where Illness_Id = '" + cbIllnessId.SelectedValue.ToString() + "'";

            //SqlCommand cmdQueryForICD10Code = connRNDB.CreateCommand();
            //cmdQueryForICD10Code.CommandType = CommandType.Text;
            //cmdQueryForICD10Code.CommandText = strQueryForICD10Code;

            //connRNDB.Open();
            //SqlDataReader rdrICD10Code = cmdQueryForICD10Code.ExecuteReader();
            //if (rdrICD10Code.HasRows)
            //{
            //    rdrICD10Code.Read();
            //    txtICD10Code.Text = rdrICD10Code.GetString(0);
            //}
            //connRNDB.Close();

            //String strQueryForICD10DiseaseName = "select Name from [ICD10 Code] where ICD10_CODE__C = '" + txtICD10Code.Text.Trim() + "'";

            //SqlCommand cmdQueryForICD10DiseaseName = connSalesforce.CreateCommand();
            //cmdQueryForICD10DiseaseName.CommandType = CommandType.Text;
            //cmdQueryForICD10DiseaseName.CommandText = strQueryForICD10DiseaseName;

            //connSalesforce.Open();
            //SqlDataReader rdrICD10Name = cmdQueryForICD10DiseaseName.ExecuteReader();
            //if (rdrICD10Name.HasRows)
            //{
            //    rdrICD10Name.Read();
            //    txtICD10Name.Text = rdrICD10Name.GetString(0);
            //}
            //connSalesforce.Close();




            //if (dtIllnessId.Rows.Count > 0)
            //{
            //    cbIllness.DataSource = dtIllnessId;
            //    cbIllness.DisplayMember = "Introduction";
            //    cbIllness.ValueMember = "Introduction";
            //}



        }

    //private void cbDistinctCases_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    ComboBox cbCase = sender as ComboBox;

    //    String strCaseSelected = cbCase.SelectedValue.ToString();

    //    String strQueryForIllnessId = "select Illness_Id from dbo.tbl_illness where Individual_Id = '" + strIndividualId + "' and Case_Id = '" + strCaseSelected + "'";

    //    SqlCommand cmdQueryForIllnessId = connRNDB.CreateCommand();
    //    cmdQueryForIllnessId.CommandType = CommandType.Text;
    //    cmdQueryForIllnessId.CommandText = strQueryForIllnessId;

    //    DataTable dtIllness = new DataTable();

    //    connRNDB.Open();
    //    SqlDataReader rdrIllnessId = cmdQueryForIllnessId.ExecuteReader();
    //    dtIllness.Load(rdrIllnessId);
    //    connRNDB.Close();

    //    if (dtIllness.Rows.Count > 0)
    //    {
    //        cbIllnessId.DataSource = dtIllness;
    //        cbIllnessId.DisplayMember = "Illness_Id";
    //        cbIllnessId.ValueMember = "Illness_Id";
    //    }
    //}

        private void btnCloseIncident_Click(object sender, EventArgs e)
        {
            Close();
        }

        //private void cbIllnessId_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    ComboBox cbIllnessId = sender as ComboBox;

        //    String strIllnessId = cbIllnessId.SelectedValue.ToString();

        //    String strQueryForIllness = "select ICD_10_Id, Introduction from dbo.tbl_illness where Illness_Id = '" + strIllnessId + "'";
        //    String strICD10Code = String.Empty;

        //    SqlCommand cmdQueryForIllness = connRNDB.CreateCommand();
        //    cmdQueryForIllness.CommandType = CommandType.Text;
        //    cmdQueryForIllness.CommandText = strQueryForIllness;

        //    connRNDB.Open();
        //    SqlDataReader rdrIllness = cmdQueryForIllness.ExecuteReader();
        //    if (rdrIllness.HasRows)
        //    {
        //        rdrIllness.Read();
        //        strICD10Code = rdrIllness.GetString(0).Trim();
        //        txtIllness.Text = rdrIllness.GetString(1).Trim();
        //    }
        //    connRNDB.Close();

        //    String strQueryForICD10Name = "select Name from [ICD10 Code] where ICD10_CODE__C = '" + strICD10Code + "'";

        //    SqlCommand cmdQueryForICD10Name = connSalesforce.CreateCommand();
        //    cmdQueryForICD10Name.CommandType = CommandType.Text;
        //    cmdQueryForICD10Name.CommandText = strQueryForICD10Name;

        //    connSalesforce.Open();
        //    SqlDataReader rdrICD10Name = cmdQueryForICD10Name.ExecuteReader();
        //    if (rdrICD10Name.HasRows)
        //    {
        //        rdrICD10Name.Read();
        //        txtICD10Name.Text = rdrICD10Name.GetString(0).ToString().Trim();
        //    }
        //    connSalesforce.Close();

        //}

        private void btnSaveIncident_Click(object sender, EventArgs e)
        {
            String IndividualId = strIndividualId.Trim();
            String CaseId = strCaseId.Trim();
            //int IllnessId = Int32.Parse(strIllnessNo.Trim());
            int IllnessId = Int32.Parse(strIllnessId.Trim());
            //String NewIncidentNo = "INCD-";
            String NewIncidentNo = txtIncidentNo.Text.Trim();


            if (mode == IncidentMode.AddNew)
            {

                String strSQlQueryForMembershipStartDate = "select [dbo].[contact].[Membership_IND_Start_date__c] from [dbo].[contact] where [dbo].[contact].[Individual_ID__c] = @IndividualId";

                SqlCommand cmdQueryForMembershipstartDate = new SqlCommand(strSQlQueryForMembershipStartDate, connSalesforce);
                cmdQueryForMembershipstartDate.CommandType = CommandType.Text;

                cmdQueryForMembershipstartDate.Parameters.AddWithValue("@IndividualId", IndividualId);

                if (connSalesforce.State != ConnectionState.Closed)
                {
                    connSalesforce.Close();
                    connSalesforce.Open();
                }
                else if (connSalesforce.State == ConnectionState.Closed) connSalesforce.Open();

                Object objMembershipStartDate = cmdQueryForMembershipstartDate.ExecuteScalar();

                DateTime? MembershipStartDate = null;
                if (objMembershipStartDate != null)
                {
                    MembershipStartDate = DateTime.Parse(objMembershipStartDate.ToString());
                }
                else
                {
                    MessageBox.Show("No membership start date for " + IndividualId, "Error");
                    return;
                }

                ////////////////////////////////////////////////////////////////////////////////////////////////////////

                //String strSqlQueryForMaxIncidentId = "select max([dbo].[tbl_incident].[incident_id]) from [dbo].[tbl_incident]";

                //SqlCommand cmdQueryForMaxIncidentId = new SqlCommand(strSqlQueryForMaxIncidentId, connRNDB);
                //cmdQueryForMaxIncidentId.CommandType = CommandType.Text;

                //if (connRNDB.State == ConnectionState.Open)
                //{
                //    connRNDB.Close();
                //    connRNDB.Open();
                //}
                //else if (connRNDB.State == ConnectionState.Closed) connRNDB.Open();
                //Object objMaxIncidentId = cmdQueryForMaxIncidentId.ExecuteScalar();
                //if (connRNDB.State == ConnectionState.Open) connRNDB.Close();

                //String MaxIncidentId = objMaxIncidentId.ToString();

                //if (MaxIncidentId == String.Empty)
                //{
                //    NewIncidentNo += '1';
                //}
                //else
                //{
                //    int nMaxIncidentId = Int32.Parse(objMaxIncidentId.ToString());

                //    String strSqlQueryForMaxIncidentNo = "select [dbo].[tbl_incident].[IncidentNo] from [dbo].[tbl_incident] where [dbo].[tbl_incident].[Incident_id] = @IncidentId";

                //    SqlCommand cmdQueryForMaxIncidentNo = new SqlCommand(strSqlQueryForMaxIncidentNo, connRNDB);
                //    cmdQueryForMaxIncidentNo.Parameters.AddWithValue("@IncidentId", nMaxIncidentId);

                //    if (connRNDB.State == ConnectionState.Open)
                //    {
                //        connRNDB.Close();
                //        connRNDB.Open();
                //    }
                //    else if (connRNDB.State == ConnectionState.Closed) connRNDB.Open();
                //    Object objMaxIncidentNo = cmdQueryForMaxIncidentNo.ExecuteScalar();
                //    if (connRNDB.State == ConnectionState.Open) connRNDB.Close();

                //    if (objMaxIncidentNo != null)
                //    {
                //        int nMaxIncidentNo = Int32.Parse(objMaxIncidentNo.ToString().Substring(5));
                //        nMaxIncidentNo++;
                //        NewIncidentNo += nMaxIncidentNo.ToString();
                //    }
                //    else
                //    {
                //        MessageBox.Show("No incident no", "Error");
                //        return;
                //    }
                //}

                String strInsertNewIncident = "insert into [dbo].[tbl_incident] " +
                        "([dbo].[tbl_incident].[IncidentNo], [dbo].[tbl_incident].[IsDeleted], " +
                        "[dbo].[tbl_incident].[Individual_id], [dbo].[tbl_incident].[Case_id], [dbo].[tbl_incident].[illness_id], " +
                        "[dbo].[tbl_incident].[OccurrenceDate], " +
                        "[dbo].[tbl_incident].[CreateDate], [dbo].[tbl_incident].[CreateStaff], " +
                        "[dbo].[tbl_incident].[ModifiDate], [dbo].[tbl_incident].[ModifiStaff]," +
                        "[dbo].[tbl_incident].[Incident_Status], " +
                        "[dbo].[tbl_incident].[Program_id], [dbo].[tbl_incident].[IsWellBeing], [dbo].[tbl_incident].[IncidentNote]) " +
                        "values (@IncidentNo, @IsDeleted, @IndividualId, @CaseId, @IllnessId, @OccurrenceDate, @CreateDate, @CreateStaff, @ModifiDate, @ModifiStaff, @IncidentStatus, " +
                        "@ProgramId, @WellBeing, @IncidentNote)";
                //"SELECT SCOPE_IDENTITY()";

                //int nUserId = 1;
                int nIncidentStatus = 0;
                int nProgramId = 0;

                SqlCommand cmdInsertIntoIncident = new SqlCommand(strInsertNewIncident, connRNDB);
                cmdInsertIntoIncident.CommandType = CommandType.Text;

                cmdInsertIntoIncident.Parameters.AddWithValue("@IncidentNo", NewIncidentNo);
                cmdInsertIntoIncident.Parameters.AddWithValue("@IsDeleted", 0);
                cmdInsertIntoIncident.Parameters.AddWithValue("@IndividualId", IndividualId);
                cmdInsertIntoIncident.Parameters.AddWithValue("@CaseId", CaseId);
                cmdInsertIntoIncident.Parameters.AddWithValue("@IllnessId", IllnessId);
                cmdInsertIntoIncident.Parameters.AddWithValue("@OccurrenceDate", dtpIncdOccurrenceDate.Value.ToString("MM/dd/yyyy"));
                cmdInsertIntoIncident.Parameters.AddWithValue("@CreateDate", DateTime.Now);
                cmdInsertIntoIncident.Parameters.AddWithValue("@CreateStaff", nLoggedInId);
                cmdInsertIntoIncident.Parameters.AddWithValue("@ModifiDate", DateTime.Now);
                cmdInsertIntoIncident.Parameters.AddWithValue("@ModifiStaff", nLoggedInId);
                cmdInsertIntoIncident.Parameters.AddWithValue("@IncidentStatus", nIncidentStatus);
                cmdInsertIntoIncident.Parameters.AddWithValue("@ProgramId", comboProgram.SelectedIndex);
                cmdInsertIntoIncident.Parameters.AddWithValue("@WellBeing", chkWellBeing.Checked);
                cmdInsertIntoIncident.Parameters.AddWithValue("@IncidentNote", txtIncidentNote.Text.Trim());

                if (connRNDB.State == ConnectionState.Open)
                {
                    connRNDB.Close();
                    connRNDB.Open();
                }
                else if (connRNDB.State == ConnectionState.Closed) connRNDB.Open();
                int nRowAffected = cmdInsertIntoIncident.ExecuteNonQuery();
                if (connRNDB.State == ConnectionState.Open) connRNDB.Close();

                if (nRowAffected == 2)
                {
                    MessageBox.Show("One incident is added successfully.");
                    DialogResult = DialogResult.OK;

                    Close();
                }

                //int nNewIncidentId = 0;
                //try
                //{
                //    connRNDB.Open();
                //    nNewIncidentId = Convert.ToInt32(cmdInsertIntoIncident.ExecuteNonQuery());
                //    connRNDB.Close();
                //}
                //catch (SqlException ex)
                //{
                //    MessageBox.Show(ex.Message, "Error");
                //    return;
                //}

                //String strInsertNewIncidentDataCapture = "insert into [dbo].[tbl_incident_capture] " +
                //                                         "([dbo].[tbl_incident_capture].[Incident_id], [dbo].[tbl_incident_capture].[Individual_id], " +
                //                                         "[dbo].[tbl_incident_capture].[Case_id], [dbo].[tbl_incident_capture].[Illness_id], " +
                //                                         "[dbo].[tbl_incident_capture].[CreateDate], [dbo].[tbl_incident_capture].[CreateStaff], " +
                //                                         "[dbo].[tbl_incident_capture].[ModifiDate], [dbo].[tbl_incident_capture].[ModifiStaff], " +
                //                                         "[dbo].[tbl_incident_capture].[Incident_Status], [dbo].[tbl_incident_capture].[Program_id], " +
                //                                         "[dbo].[tbl_incident_capture].[IncidentNote], [dbo].[tbl_incident_capture].[Operation], " +
                //                                         "[dbo].[tbl_incident_capture].[ModificationDateTime], [dbo].[tbl_incident_capture].[ModifyingStaff]) " +
                //                                         "values (@NewIncidentId, @IndividualId, @CaseId, @IllnessId, @CreateDate, @CreateStaff, @ModifiDate, @ModifiStaff, " +
                //                                         "@ProgramId, @IncidentNote, @Operation, @ModificationDateTime, @ModifyingStaff)";

                //int nOperation = 2;             // insert operation on data change capture
                //int nModifyingStaff = 8;        // inserted by Won Jik Jun

                //SqlCommand cmdInsertInfoIncidentDataCapture = new SqlCommand(strInsertNewIncidentDataCapture, connRNDB);
                //cmdInsertInfoIncidentDataCapture.CommandType = CommandType.Text;

                //cmdInsertInfoIncidentDataCapture.Parameters.AddWithValue("@NewIncidentId", nNewIncidentId);
                //cmdInsertInfoIncidentDataCapture.Parameters.AddWithValue("@IndividualId", IndividualId);
                //cmdInsertInfoIncidentDataCapture.Parameters.AddWithValue("@CaseId", CaseId);
                //cmdInsertInfoIncidentDataCapture.Parameters.AddWithValue("@Illness_id", IllnessId);
                //cmdInsertInfoIncidentDataCapture.Parameters.AddWithValue("@CreateDate", DateTime.Today.ToString("MM/dd/yyyy"));
                //cmdInsertInfoIncidentDataCapture.Parameters.AddWithValue("@CreateStaff", nUserId);
                //cmdInsertInfoIncidentDataCapture.Parameters.AddWithValue("@ModifiDate", DateTime.Today.ToString("MM/dd/yyyy"));
                //cmdInsertInfoIncidentDataCapture.Parameters.AddWithValue("@ModifiStaff", nUserId);
                //cmdInsertInfoIncidentDataCapture.Parameters.AddWithValue("@ProgramId", nProgramId);
                //cmdInsertInfoIncidentDataCapture.Parameters.AddWithValue("@IncidentNote", txtIncidentNote.Text.Trim());
                //cmdInsertInfoIncidentDataCapture.Parameters.AddWithValue("@Operation", nOperation);
                //cmdInsertInfoIncidentDataCapture.Parameters.AddWithValue("@ModificationDateTime", DateTime.Today.ToString("MM/dd/yyyy"));
                //cmdInsertInfoIncidentDataCapture.Parameters.AddWithValue("@ModifyingStaff", nUserId);

                //connRNDB.Open();
                //int nDataChangeCaptureRowAffected = cmdInsertInfoIncidentDataCapture.ExecuteNonQuery();
                //connRNDB.Close();

                //if (nDataChangeCaptureRowAffected == 1)
                //{
                //    DialogResult = DialogResult.OK;
                //    return;
                //}
                // 09/18/18 begin here

                


                //if (nRowAdded == 1)
                //{
                //    DialogResult = DialogResult.OK;
                //    //String strInsertDataChangeToIncident = ""

                //    //Close();
                //}
            }
            else if (mode == IncidentMode.Edit)
            {
                //String IncidentId = strIncidentId;
                String IncidentNo = txtIncidentNo.Text.Trim();

                String strSqlUpdateIncident = "update [dbo].[tbl_incident] set [dbo].[tbl_incident].[OccurrenceDate] = @OccurrenceDate, " +
                                              "[dbo].[tbl_incident].[ModifiDate] = @ModifiDate, [dbo].[tbl_incident].[ModifiStaff] = @ModifiStaff, " +
                                              "[dbo].[tbl_incident].[Program_id] = @ProgramId, [dbo].[tbl_incident].[IsWellBeing] = @WellBeing, " +
                                              "[dbo].[tbl_incident].[IncidentNote] = @IncidentNote " +
                                              "where [dbo].[tbl_incident].[IncidentNo] = @IncidentNo and [dbo].[tbl_incident].[Individual_id] = @IndividualId";

                SqlCommand cmdUpdateIncident = new SqlCommand(strSqlUpdateIncident, connRNDB);
                cmdUpdateIncident.CommandType = CommandType.Text;

                cmdUpdateIncident.Parameters.AddWithValue("@OccurrenceDate", dtpIncdOccurrenceDate.Value.ToString("MM/dd/yyyy"));

                //String ModifiDate = dtpModifiedDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
                //cmdUpdateIncident.Parameters.AddWithValue("@ModifiDate", dtpModifiedDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                cmdUpdateIncident.Parameters.AddWithValue("@ModifiDate", DateTime.Now);
                cmdUpdateIncident.Parameters.AddWithValue("@ModifiStaff", nLoggedInId);
                cmdUpdateIncident.Parameters.AddWithValue("@ProgramId", comboProgram.SelectedIndex);
                cmdUpdateIncident.Parameters.AddWithValue("@WellBeing", chkWellBeing.Checked);
                cmdUpdateIncident.Parameters.AddWithValue("@IncidentNote", txtIncidentNote.Text.Trim());
                //cmdUpdateIncident.Parameters.AddWithValue("@IncidentNo", strIncidentNo);
                cmdUpdateIncident.Parameters.AddWithValue("@IncidentNo", IncidentNo);
                cmdUpdateIncident.Parameters.AddWithValue("@IndividualId", strIndividualId);

                if (connRNDB.State == ConnectionState.Open)
                {
                    connRNDB.Close();
                    connRNDB.Open();
                }
                else if (connRNDB.State == ConnectionState.Closed) connRNDB.Open();
                int nRowUpdated = cmdUpdateIncident.ExecuteNonQuery(); // operation by is null on incident history table
                if (connRNDB.State == ConnectionState.Open) connRNDB.Close();

                if (nRowUpdated == 3)
                {
                    MessageBox.Show("The incident has been updated.");
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
        }

        private void chkWellBeing_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkbox = sender as CheckBox;

            if (chkbox.Checked)
            {
                //String ICD10Code = txtICD10Code.Text.Trim();

                //String strSqlQueryForICD10Code = "select [dbo].[tbl_illness].[ICD_10_Id] from [dbo].[tbl_illness] where [dbo].[tbl_illness].[Illness_Id] = @IllnessId";

                //SqlCommand cmdQueryForICD10Code = new SqlCommand(strSqlQueryForICD10Code, connRNDB);
                //cmdQueryForICD10Code.CommandType = CommandType.Text;

                //cmdQueryForICD10Code.Parameters.AddWithValue("@IllnessId", strIllnessId);

                //if (connRNDB.State != ConnectionState.Closed)
                //{
                //    connRNDB.Close();
                //    connRNDB.Open();
                //}
                //else if (connRNDB.State == ConnectionState.Closed) connRNDB.Open();
                //Object objICD10Code = cmdQueryForICD10Code.ExecuteScalar();
                //if (connRNDB.State != ConnectionState.Closed) connRNDB.Close();

                //String ICD10Code = String.Empty;
                //if (objICD10Code != null) ICD10Code = objICD10Code.ToString();

                //if (ICD10Code == "Z00.00" || ICD10Code == "Z00.012")
                //{
                String IncidentProgram = comboProgram.SelectedItem.ToString().Trim();

                if (IncidentProgram == "Gold Plus" || IncidentProgram == "Gold Medi-I" || IncidentProgram == "Gold Medi-II")
                {
                    String strSqlQueryForStartDate = "select [dbo].[contact].[Membership_IND_Start_date__c] from [dbo].[contact] " +
                                                        "where [dbo].[contact].[Individual_ID__c] = @IndividualId";

                    SqlCommand cmdQueryForStartDate = new SqlCommand(strSqlQueryForStartDate, connSalesforce);
                    cmdQueryForStartDate.CommandType = CommandType.Text;

                    cmdQueryForStartDate.Parameters.AddWithValue("@IndividualId", strIndividualId);

                    if (connSalesforce.State != ConnectionState.Closed)
                    {
                        connSalesforce.Close();
                        connSalesforce.Open();
                    }
                    else if (connSalesforce.State == ConnectionState.Closed) connSalesforce.Open();
                    Object objMembershipStartDate = cmdQueryForStartDate.ExecuteScalar();
                    if (connSalesforce.State != ConnectionState.Closed) connSalesforce.Close();

                    DateTime? MembershipStartDate = null;
                    if (objMembershipStartDate != null) MembershipStartDate = DateTime.Parse(objMembershipStartDate.ToString());

                    DateTime? WellBeingStartDate = MembershipStartDate.Value.AddMonths(6);

                    if (DateTime.Today < WellBeingStartDate.Value)
                    {
                        //MessageBox.Show("This incident is not eligible for Well Being Care.", "Alert");
                        DialogResult drWellBeing = MessageBox.Show("This incident is not eligible for Well Being Care. Do you want to contine with Well Being Care?", "Alert", MessageBoxButtons.YesNo);
                        if (drWellBeing == DialogResult.Yes) chkbox.Checked = true;
                        else chkbox.Checked = false;
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("The Incident Program is not eligible for Well Being Care.", "Alert");
                    chkbox.Checked = false;
                    return;
                }
                //}
                //else
                //{
                //    MessageBox.Show("The Illness is not eligible for Well Being Care.", "Alert");
                //    chkbox.Checked = false;
                //    return;
                //}
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
    }
}
