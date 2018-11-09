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

    public enum IllnessMode { AddNew, Edit };

    public partial class frmIllnessCreationPage : Form
    {
        private String connStringIcd10;
        private SqlConnection conn_icd10codes;
        private String connStringSalesforce;
        private SqlConnection connSalesforce;
        private SqlCommand cmd_icd10;
        private String strSqlForICD10Codes;

        private String strRNConnection;
        private SqlConnection connRNDB;

        private String strCaseNoConnection;
        private SqlConnection connCaseNo;

        public int nIllnessId;
        public String IllnessNo = String.Empty;
        public String strIndividualNo = String.Empty;
        public String strCaseNo = String.Empty;

        public int nInsertedId;

        public int nLoggedInUserId;

        public DateTime MembershipStartDate;


        List<ICD10CodeInfo> lstICD10CodeInfo;

        Dictionary<int, String> dicLimitedSharingOptions;
        Dictionary<int, Decimal> dicLimitedSharing1;
        Dictionary<int, Decimal> dicLimitedSharing2;

        //public String strCaseId;

        public IllnessMode mode;

        public void SetSqlSetting()
        {
            connStringIcd10 = @"Data Source=CMM-2014U\CMM; Initial Catalog=SalesForce;Integrated Security=True";

            connStringSalesforce = @"Data Source=CMM-2014U\CMM; Initial Catalog=SalesForce;Integrated Security=True";
            connSalesforce = new SqlConnection(connStringSalesforce);

            strSqlForICD10Codes = "select id, name, icd10_code__c from [ICD10 Code]";

            strRNConnection = @"Data Source=CMM-2014U\CMM; Initial Catalog=RN_DB; Integrated Security=True";
            connRNDB = new SqlConnection(strRNConnection);

            nInsertedId = 0;
          
            //SqlDependency.Start(strRNConnection);

        }

        public frmIllnessCreationPage()
        {
            InitializeComponent();
            SetSqlSetting();
            lstICD10CodeInfo = new List<ICD10CodeInfo>();
            dicLimitedSharingOptions = new Dictionary<int, string>();
            dicLimitedSharing1 = new Dictionary<int, Decimal>();
            dicLimitedSharing2 = new Dictionary<int, Decimal>();
            //strCaseId = null;

            String strSqlQueryForLimitedSharingInfo = "select [dbo].[tbl_limited_sharing_info].[LimitedSharingId], [dbo].[tbl_limited_sharing_info].[LimitedSharingName] " +
                                          "from [dbo].[tbl_limited_sharing_info]";

            SqlCommand cmdQueryForLimitedSharingInfo = new SqlCommand(strSqlQueryForLimitedSharingInfo, connRNDB);
            cmdQueryForLimitedSharingInfo.CommandType = CommandType.Text;

            if (connRNDB.State == ConnectionState.Open)
            {
                connRNDB.Close();
                connRNDB.Open();
            }
            else if (connRNDB.State == ConnectionState.Closed) connRNDB.Open();
            SqlDataReader rdrLimitedSharingInfo = cmdQueryForLimitedSharingInfo.ExecuteReader();

            if (rdrLimitedSharingInfo.HasRows)
            {
                while (rdrLimitedSharingInfo.Read())
                {
                    dicLimitedSharingOptions.Add(rdrLimitedSharingInfo.GetInt16(0), rdrLimitedSharingInfo.GetString(1));
                }
            }
            if (connRNDB.State == ConnectionState.Open) connRNDB.Close();



            String strSqlQueryForLimitedSharing1 = "select [dbo].[tbl_limited_sharing_1].[YearNo], [dbo].[tbl_limited_sharing_1].[YearlyLimit] from [dbo].[tbl_limited_sharing_1]";

            SqlCommand cmdQueryForLimitedSharing1 = new SqlCommand(strSqlQueryForLimitedSharing1, connRNDB);
            cmdQueryForLimitedSharing1.CommandType = CommandType.Text;

            if (connRNDB.State == ConnectionState.Open)
            {
                connRNDB.Close();
                connRNDB.Open();
            }
            else if (connRNDB.State == ConnectionState.Closed) connRNDB.Open();
            SqlDataReader rdrLimitedSharing1 = cmdQueryForLimitedSharing1.ExecuteReader();

            if (rdrLimitedSharing1.HasRows)
            {
                while (rdrLimitedSharing1.Read())
                {
                    dicLimitedSharing1.Add(rdrLimitedSharing1.GetInt16(0), rdrLimitedSharing1.GetDecimal(1));
                }
            }
            if (connRNDB.State == ConnectionState.Open) connRNDB.Close();

            String strSqlQueryForLimitedSharing2 = "select [dbo].[tbl_limited_sharing_2].[YearNo], [dbo].[tbl_limited_sharing_2].[YearlyLimit] from [dbo].[tbl_limited_sharing_2]";

            SqlCommand cmdQueryForLimitedSharing2 = new SqlCommand(strSqlQueryForLimitedSharing2, connRNDB);
            cmdQueryForLimitedSharing2.CommandType = CommandType.Text;

            if (connRNDB.State == ConnectionState.Open)
            {
                connRNDB.Close();
                connRNDB.Open();
            }
            else if (connRNDB.State == ConnectionState.Closed) connRNDB.Open();
            SqlDataReader rdrLimitedSharing2 = cmdQueryForLimitedSharing2.ExecuteReader();

            if (rdrLimitedSharing2.HasRows)
            {
                while (rdrLimitedSharing2.Read())
                {
                    dicLimitedSharing2.Add(rdrLimitedSharing2.GetInt16(0), rdrLimitedSharing2.GetDecimal(1));
                }
            }
            else if (connRNDB.State == ConnectionState.Open) connRNDB.Close();
        }

        ~frmIllnessCreationPage()
        {
            //SqlDependency.Stop(strRNConnection);
        }

        private void frmIllnessCreationPage_Load(object sender, EventArgs e)
        {
            //txtIllnessNo.Text = nIllnessNo.ToString();
            txtIllnessNo.Text = IllnessNo;
            txtIndividualNo.Text = strIndividualNo;
            txtCaseNo.Text = strCaseNo;

            conn_icd10codes = new SqlConnection(connStringIcd10);
            cmd_icd10 = conn_icd10codes.CreateCommand();

            cmd_icd10.CommandType = CommandType.Text;
            cmd_icd10.CommandText = strSqlForICD10Codes;
            lstICD10CodeInfo.Clear();

            conn_icd10codes.Open();
            SqlDataReader rdr_icd10codes = cmd_icd10.ExecuteReader();

            if (rdr_icd10codes.HasRows)
            {
                while (rdr_icd10codes.Read())
                {
                    lstICD10CodeInfo.Add(new ICD10CodeInfo { Id = rdr_icd10codes.GetString(0), Name = rdr_icd10codes.GetString(1), ICD10Code = rdr_icd10codes.GetString(2) });
                }
            }
            conn_icd10codes.Close();

            var srcICD10Codes = new AutoCompleteStringCollection();

            for(int i = 0; i < lstICD10CodeInfo.Count; i++)
            {
                srcICD10Codes.Add(lstICD10CodeInfo[i].ICD10Code);
            }

            txtICD10Code.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtICD10Code.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtICD10Code.AutoCompleteCustomSource = srcICD10Codes;

            dtpDateOfDiagnosis.Format = DateTimePickerFormat.Custom;
            dtpDateOfDiagnosis.CustomFormat = " ";

            //if (mode == IllnessMode.AddNew)
            //{
            //    btnSaveIllness.Enabled = true;
            //    //btnAddNew.Enabled = true;
            //    btnIllnessUpdate.Enabled = false;                
            //}
            //else if (mode == IllnessMode.Edit)
            //{
            //    btnSaveIllness.Enabled = false;
            //    //btnAddNew.Enabled = false;
            //    btnIllnessUpdate.Enabled = true;
            //}

            for (int i = 0; i < dicLimitedSharingOptions.Count; i++)
            {
                comboLimitedSharing.Items.Add(dicLimitedSharingOptions[i]);
            }
            comboLimitedSharing.SelectedIndex = 0;

            if (mode == IllnessMode.AddNew)
            {

            }
            else if (mode == IllnessMode.Edit)
            {
                //int IllnessId = nIllnessNo.Value;

                String strSqlQueryForIllness = "select [dbo].[tbl_illness].[Illness_Id], [dbo].[tbl_illness].[ICD_10_Id], [dbo].[tbl_illness].[CreateDate], [dbo].[tbl_illness].[ModifiDate], " +
                                               "[dbo].[tbl_illness].[Individual_Id], [dbo].[tbl_illness].[Case_Id], [dbo].[tbl_illness].[Date_of_Diagnosis], " +
                                               "[dbo].[tbl_illness].[LimitedSharingId], " +
                                               "[dbo].[tbl_illness].[Introduction], [dbo].[tbl_illness].[Body], [dbo].[tbl_illness].[Conclusion] " +
                                               "from [dbo].[tbl_illness] " +
                                               "where [dbo].[tbl_illness].[IllnessNo] = @IllnessNo";
                                               //"where [dbo].[tbl_illness].[Illness_Id] = @IllnessId";

                SqlCommand cmdQueryForIllness = new SqlCommand(strSqlQueryForIllness, connRNDB);
                cmdQueryForIllness.Parameters.AddWithValue("@IllnessNo", IllnessNo);

                if (connRNDB.State == ConnectionState.Open)
                {
                    connRNDB.Close();
                    connRNDB.Open();
                }
                else if (connRNDB.State == ConnectionState.Closed) connRNDB.Open();
                SqlDataReader rdrIllness = cmdQueryForIllness.ExecuteReader();
                if (rdrIllness.HasRows)
                {
                    rdrIllness.Read();
                    nIllnessId = rdrIllness.GetInt32(0);
                    if (!rdrIllness.IsDBNull(1)) txtICD10Code.Text = rdrIllness.GetString(1);
                    if (!rdrIllness.IsDBNull(2))
                    {
                        dtpCreateDate.Value = rdrIllness.GetDateTime(2);
                        dtpCreateDate.Enabled = false;
                    }
                    if (!rdrIllness.IsDBNull(4)) txtIndividualNo.Text = rdrIllness.GetString(4);
                    if (!rdrIllness.IsDBNull(5)) txtCaseNo.Text = rdrIllness.GetString(5);
                    if (!rdrIllness.IsDBNull(6))
                    {
                        dtpDateOfDiagnosis.Value = rdrIllness.GetDateTime(6);
                        dtpDateOfDiagnosis.Format = DateTimePickerFormat.Short;
                    }
                    else
                    {
                        dtpDateOfDiagnosis.Format = DateTimePickerFormat.Custom;
                        dtpDateOfDiagnosis.CustomFormat = " ";
                    }
                    if (!rdrIllness.IsDBNull(7)) comboLimitedSharing.SelectedIndex = rdrIllness.GetInt16(7);
                    if (!rdrIllness.IsDBNull(8)) txtIntroduction.Text = rdrIllness.GetString(8);
                    if (!rdrIllness.IsDBNull(9)) txtIllnessNote.Text = rdrIllness.GetString(9);
                    if (!rdrIllness.IsDBNull(10)) txtConclusion.Text = rdrIllness.GetString(10);
                }

                if (connRNDB.State == ConnectionState.Open) connRNDB.Close();
            }


            //String strSqlQueryForIllness = "select [dbo].[tbl_illness].[Illness_Id], [dbo].[tbl_illness].["

            //String strSqlQueryForLimitedSharingInfo = "select [dbo].[tbl_limited_sharing_info].[LimitedSharingId], [dbo].[tbl_limited_sharing_info].[LimitedSharingName] " +
            //                                          "from [dbo].[tbl_limited_sharing_info]";

            //SqlCommand cmdQueryForLimitedSharingInfo = new SqlCommand(strSqlQueryForLimitedSharingInfo, connRNDB);
            //cmdQueryForLimitedSharingInfo.CommandType = CommandType.Text;

            //connRNDB.Open();
            //SqlDataReader rdrLimitedSharingInfo = cmdQueryForLimitedSharingInfo.ExecuteReader();

            //if (rdrLimitedSharingInfo.HasRows)
            //{
            //    while (rdrLimitedSharingInfo.Read())
            //    {
            //        dicLimitedSharingOptions.Add(rdrLimitedSharingInfo.GetInt16(0), rdrLimitedSharingInfo.GetString(1));
            //    }
            //}
            //connRNDB.Close();

            //for (int i = 0; i < dicLimitedSharingOptions.Count; i++)
            //{
            //    comboLimitedSharing.Items.Add(dicLimitedSharingOptions[i]);
            //}
            //comboLimitedSharing.SelectedIndex = 0;

            //String strSqlQueryForLimitedSharing1 = "select [dbo].[tbl_limited_sharing_1].[YearNo], [dbo].[tbl_limited_sharing_1].[YearlyLimit] from [dbo].[tbl_limited_sharing_1]";

            //SqlCommand cmdQueryForLimitedSharing1 = new SqlCommand(strSqlQueryForLimitedSharing1, connRNDB);
            //cmdQueryForLimitedSharing1.CommandType = CommandType.Text;

            //connRNDB.Open();
            //SqlDataReader rdrLimitedSharing1 = cmdQueryForLimitedSharing1.ExecuteReader();

            //if (rdrLimitedSharing1.HasRows)
            //{
            //    while (rdrLimitedSharing1.Read())
            //    {
            //        dicLimitedSharing1.Add(rdrLimitedSharing1.GetInt16(0), rdrLimitedSharing1.GetDecimal(1));
            //    }
            //}
            //connRNDB.Close();

            //String strSqlQueryForLimitedSharing2 = "select [dbo].[tbl_limited_sharing_2].[YearNo], [dbo].[tbl_limited_sharing_2].[YearlyLimit] from [dbo].[tbl_limited_sharing_2]";

            //SqlCommand cmdQueryForLimitedSharing2 = new SqlCommand(strSqlQueryForLimitedSharing2, connRNDB);
            //cmdQueryForLimitedSharing2.CommandType = CommandType.Text;

            //connRNDB.Open();
            //SqlDataReader rdrLimitedSharing2 = cmdQueryForLimitedSharing2.ExecuteReader();

            //if (rdrLimitedSharing2.HasRows)
            //{
            //    while (rdrLimitedSharing2.Read())
            //    {
            //        dicLimitedSharing2.Add(rdrLimitedSharing2.GetInt16(0), rdrLimitedSharing2.GetDecimal(1));
            //    }
            //}
            //connRNDB.Close();
        }

        private void btnSaveIllness_Click(object sender, EventArgs e)
        {
            //int IllnessId = nIllnessNo.Value;

            
            String strICD10Code = String.Empty;

            //String strIllnessId = String.Empty;
            String strIndividualId = String.Empty;
            String strIntroduction = String.Empty;
            String strIllnessNote = String.Empty;
            String strConclusion = String.Empty;

            //if (txtIllnessId.Text.Trim() != String.Empty) strIllnessId = txtIllnessId.Text.Trim();
            if (txtICD10Code.Text.Trim() != String.Empty) strICD10Code = txtICD10Code.Text.Trim();
            if (txtIndividualNo.Text.Trim() != String.Empty) strIndividualId = txtIndividualNo.Text.Trim();
            if (txtIntroduction.Text.Trim() != String.Empty) strIntroduction = txtIntroduction.Text.Trim();
            if (txtIllnessNote.Text.Trim() != String.Empty) strIllnessNote = txtIllnessNote.Text.Trim();
            if (txtConclusion.Text.Trim() != String.Empty) strConclusion = txtConclusion.Text.Trim();

            //String strSqlQueryForIllnessId = "select [dbo].[tbl_illness].[Illness_Id] from [dbo].[tbl_illness] " +
            //                                 "where [dbo].[tbl_illness].[Illness_Id] = @IllnessId and " +
            //                                 "[dbo].[tbl_illness].[Case_Id] = @CaseNo and " +
            //                                 "[dbo].[tbl_illness].[Individual_Id] = @IndividualId";

            String strSqlQueryForIllnessId = "select [dbo].[tbl_illness].[IllnessNo] from [dbo].[tbl_illness] " +
                                             "where [dbo].[tbl_illness].[IllnessNo] = @IllnessNo and [dbo].[tbl_illness].[Individual_Id] = @IndividualId";

            SqlCommand cmdQueryForIllnessId = new SqlCommand(strSqlQueryForIllnessId, connRNDB);
            cmdQueryForIllnessId.CommandType = CommandType.Text;

            cmdQueryForIllnessId.Parameters.AddWithValue("@IllnessNo", IllnessNo);
            cmdQueryForIllnessId.Parameters.AddWithValue("@CaseNo", strCaseNo);
            cmdQueryForIllnessId.Parameters.AddWithValue("@IndividualId", strIndividualId);

            if (connRNDB.State == ConnectionState.Open)
            {
                connRNDB.Close();
                connRNDB.Open();
            }
            else if (connRNDB.State == ConnectionState.Closed) connRNDB.Open();
            Object objIllnessNo = cmdQueryForIllnessId.ExecuteScalar();
            if (connRNDB.State == ConnectionState.Open) connRNDB.Close();

            //String strIllnessNo = objIllnessId.ToString();

            if (objIllnessNo == null)
            {

                //String strSqlCreateIllness = "insert into tbl_illness (ICD_10_Id, CreateDate, ModifiDate, CreateStaff, ModifiStaff, Individual_Id, Case_Id, " +
                //                 "Date_of_Diagnosis, Remove_log, Introduction, Body, Conclusion, IsRemoved) " +
                //                 "values ('" + strICD10Code + "', '" + DateTime.Today.ToString("MM/dd/yyyy") + "', '" + DateTime.Today.ToString("MM/dd/yyyy") + "', " +
                //                 nCreateStaff + ", " + nModifiStaff + ", '" + strIndividualId + "', '" + txtCaseNo.Text.Trim() + "', '" + 
                //                 dtpCreateDate.Value.ToString("MM/dd/yyyy") + "', " + "null, '" + 
                //                 strIntroduction + "', '" + strIllnessNote + "', '" + strConclusion + "', 0); select Scope_Identity()";

                String strSqlCreateIllness = "insert into [dbo].[tbl_illness] ([dbo].[tbl_illness].[IllnessNo], [dbo].[tbl_illness].[IsDeleted], " +
                                             "[dbo].[tbl_illness].[ICD_10_Id], [dbo].[tbl_illness].[CreateDate], [dbo].[tbl_illness].[ModifiDate], " +
                                             "[dbo].[tbl_illness].[CreateStaff], [dbo].[tbl_illness].[ModifiStaff], [dbo].[tbl_illness].[Individual_Id], [dbo].[tbl_illness].[Case_Id], " +
                                             "[dbo].[tbl_illness].[LimitedSharingId], [dbo].[tbl_illness].[Date_of_Diagnosis], [dbo].[tbl_illness].[Remove_log], " +
                                             "[dbo].[tbl_illness].[Introduction], [dbo].[tbl_illness].[Body], [dbo].[tbl_illness].[Conclusion], [dbo].[tbl_illness].[IsRemoved]) " +
                                             "values (@IllnessNo, @IsDeleted, @ICD10Code, @CreateDate, @ModifiDate, " +
                                             "@CreateStaff, @ModifiStaff, @IndividualId, @CaseId, " +
                                             "@LimitedSharingId, @DateOfDiagnosis, @RemoveLog, " +
                                             "@Introduction, @Body, @Conclusion, 0)";

                //String strRNConnection = @"Data Source=CMM-2014U\CMM; Initial Catalog=RN_DB; Integrated Security=True";
                //SqlConnection connRNDB = new SqlConnection(strRNConnection);

                SqlCommand cmdCreateIllness = new SqlCommand(strSqlCreateIllness, connRNDB);
                cmdCreateIllness.CommandType = CommandType.Text;

                cmdCreateIllness.Parameters.AddWithValue("@IllnessNo", IllnessNo);
                cmdCreateIllness.Parameters.AddWithValue("@IsDeleted", 0);

                if (strICD10Code != String.Empty) cmdCreateIllness.Parameters.AddWithValue("@ICD10Code", strICD10Code);
                else cmdCreateIllness.Parameters.AddWithValue("@ICD10Code", DBNull.Value);

                cmdCreateIllness.Parameters.AddWithValue("@CreateDate", DateTime.Today);
                cmdCreateIllness.Parameters.AddWithValue("@ModifiDate", DateTime.Today);
                cmdCreateIllness.Parameters.AddWithValue("@CreateStaff", nLoggedInUserId);
                cmdCreateIllness.Parameters.AddWithValue("@ModifiStaff", nLoggedInUserId);
                cmdCreateIllness.Parameters.AddWithValue("@IndividualId", strIndividualId);
                cmdCreateIllness.Parameters.AddWithValue("@CaseId", txtCaseNo.Text.Trim());
                cmdCreateIllness.Parameters.AddWithValue("@LimitedSharingId", comboLimitedSharing.SelectedIndex);
                if (dtpDateOfDiagnosis.Text.Trim() != String.Empty) cmdCreateIllness.Parameters.AddWithValue("@DateOfDiagnosis", dtpDateOfDiagnosis.Value);
                else cmdCreateIllness.Parameters.AddWithValue("@DateOfDiagnosis", DBNull.Value);
                cmdCreateIllness.Parameters.AddWithValue("@RemoveLog", DBNull.Value);
                if (strIntroduction != String.Empty) cmdCreateIllness.Parameters.AddWithValue("@Introduction", strIntroduction);
                else cmdCreateIllness.Parameters.AddWithValue("@Introduction", DBNull.Value);
                if (strIntroduction != String.Empty) cmdCreateIllness.Parameters.AddWithValue("@Body", strIllnessNote);
                else cmdCreateIllness.Parameters.AddWithValue("@Body", DBNull.Value);
                if (strConclusion != String.Empty) cmdCreateIllness.Parameters.AddWithValue("@Conclusion", strConclusion);
                else cmdCreateIllness.Parameters.AddWithValue("@Conclusion", DBNull.Value);

                if (connRNDB.State == ConnectionState.Open)
                {
                    connRNDB.Close();
                    connRNDB.Open();
                }
                else if (connRNDB.State == ConnectionState.Closed) connRNDB.Open();
                int nRowInserted = cmdCreateIllness.ExecuteNonQuery();
                //int nNewInsertedId = (int)cmdCreateIllness.ExecuteScalar();
                if (connRNDB.State == ConnectionState.Open) connRNDB.Close();

                if (nRowInserted == 1)
                {
                    MessageBox.Show("New Illness has been created.", "Information");
                    DialogResult = DialogResult.OK;
                    return;
                }
                else if (nRowInserted == 0)
                {
                    MessageBox.Show("New Illness has not been created.", "Error");
                    return;
                }
            }
            else
            {
                //String strICD10Code = String.Empty;
                //String strIndividualId = String.Empty;
                //String strCaseNo = String.Empty;
                //String strIntroduction = String.Empty;
                //String strIllnessNote = String.Empty;
                //String strConclusion = String.Empty;

                //int nIllnessNo = Int32.Parse(objIllnessId.ToString());

                if (txtICD10Code.Text.Trim() != String.Empty) strICD10Code = txtICD10Code.Text.Trim();
                if (txtIndividualNo.Text.Trim() != String.Empty) strIndividualId = txtIndividualNo.Text.Trim();
                if (txtCaseNo.Text.Trim() != String.Empty) strCaseNo = txtCaseNo.Text.Trim();
                if (txtIntroduction.Text.Trim() != String.Empty) strIntroduction = txtIntroduction.Text.Trim();
                if (txtIllnessNote.Text.Trim() != String.Empty) strIllnessNote = txtIllnessNote.Text.Trim();
                if (txtConclusion.Text.Trim() != String.Empty) strConclusion = txtConclusion.Text.Trim();

                String strSqlUpdateIllness = "update [dbo].[tbl_illness] set [dbo].[tbl_illness].[icd_10_Id] = @ICD10Code, [dbo].[tbl_illness].[ModifiDate] = @ModifiDate, " +
                                             "[dbo].[tbl_illness].[ModifiStaff] = @ModifiStaff, [dbo].[tbl_illness].[Date_of_Diagnosis] = @DiagnosisDate, " +
                                             "[dbo].[tbl_illness].[LimitedSharingId] = @LimitedSharingId, " +
                                             "[dbo].[tbl_illness].[Introduction] = @Introduction, [dbo].[tbl_illness].[Body] = @Body, [dbo].[tbl_illness].[Conclusion] = @Conclusion " +
                                             "where [dbo].[tbl_illness].[Individual_Id] = @IndividualId and [dbo].[tbl_illness].[Case_Id] = @CaseId and " +
                                             "[dbo].[tbl_illness].[IllnessNo] = @IllnessNo";

                SqlCommand cmdUpdateIllness = new SqlCommand(strSqlUpdateIllness, connRNDB);
                cmdUpdateIllness.CommandType = CommandType.Text;

                if (strICD10Code != String.Empty) cmdUpdateIllness.Parameters.AddWithValue("@ICD10Code", strICD10Code);
                else cmdUpdateIllness.Parameters.AddWithValue("@ICD10Code", DBNull.Value);
                cmdUpdateIllness.Parameters.AddWithValue("@ModifiDate", DateTime.Today);
                cmdUpdateIllness.Parameters.AddWithValue("@ModifiStaff", nLoggedInUserId);
                cmdUpdateIllness.Parameters.AddWithValue("@DiagnosisDate", dtpDateOfDiagnosis.Value);
                cmdUpdateIllness.Parameters.AddWithValue("@LimitedSharingId", comboLimitedSharing.SelectedIndex);
                cmdUpdateIllness.Parameters.AddWithValue("@Introduction", strIntroduction);
                cmdUpdateIllness.Parameters.AddWithValue("@Body", strIllnessNote);
                cmdUpdateIllness.Parameters.AddWithValue("@Conclusion", strConclusion);
                cmdUpdateIllness.Parameters.AddWithValue("@IndividualId", strIndividualId);
                cmdUpdateIllness.Parameters.AddWithValue("@CaseId", strCaseNo);
                cmdUpdateIllness.Parameters.AddWithValue("@IllnessNo", IllnessNo);

                if (connRNDB.State == ConnectionState.Open)
                {
                    connRNDB.Close();
                    connRNDB.Open();
                }
                else if (connRNDB.State == ConnectionState.Closed) connRNDB.Open();
                int nUpdated = cmdUpdateIllness.ExecuteNonQuery();
                if (connRNDB.State == ConnectionState.Open) connRNDB.Close();

                if (nUpdated == 1)
                {
                    MessageBox.Show("The Illness has been updated.", "Information");
                    DialogResult = DialogResult.OK;
                    return;
                }
                else if (nUpdated == 0)
                {
                    MessageBox.Show("The Illness has not been updated.", "Error");
                    return;
                }
            }
            //Close();
        }

        private void txtICD10Code_TextChanged(object sender, EventArgs e)
        {
            String strICD10Code = txtICD10Code.Text;

            for (int i = 0; i < lstICD10CodeInfo.Count; i++)
            {
                if (strICD10Code.ToUpper() == lstICD10CodeInfo[i].ICD10Code) txtDiseaseName.Text = lstICD10CodeInfo[i].Name;
                else if (txtICD10Code.Text.Trim() == String.Empty) txtDiseaseName.Text = String.Empty;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        //private void btnIllnessUpdate_Click(object sender, EventArgs e)
        //{
        //    String strICD10Code = String.Empty;
        //    String strIndividualId = String.Empty;
        //    String strCaseNo = String.Empty;
        //    String strIntroduction = String.Empty;
        //    String strIllnessNote = String.Empty;
        //    String strConclusion = String.Empty;

        //    if (txtICD10Code.Text.Trim() != String.Empty) strICD10Code = txtICD10Code.Text.Trim();
        //    if (txtIndividualNo.Text.Trim() != String.Empty) strIndividualId = txtIndividualNo.Text.Trim();
        //    if (txtCaseNo.Text.Trim() != String.Empty) strCaseNo = txtCaseNo.Text.Trim();
        //    if (txtIntroduction.Text.Trim() != String.Empty) strIntroduction = txtIntroduction.Text.Trim();
        //    if (txtIllnessNote.Text.Trim() != String.Empty) strIllnessNote = txtIllnessNote.Text.Trim();
        //    if (txtConclusion.Text.Trim() != String.Empty) strConclusion = txtConclusion.Text.Trim();

        //    String strSqlUpdateIllness = "update [dbo].[tbl_illness] set [dbo].[tbl_illness].[icd_10_Id] = @ICD10Code, [dbo].[tbl_illness].[ModifiDate] = @ModifiDate, " +
        //                                 "[dbo].[tbl_illness].[ModifiStaff] = @ModifiStaff, [dbo].[tbl_illness].[Date_of_Diagnosis] = @DiagnosisDate, " +
        //                                 "[dbo].[tbl_illness].[Introduction] = @Introduction, [dbo].[tbl_illness].[Body] = @Body, [dbo].[tbl_illness].[Conclusion] = @Conclusion " +
        //                                 "where [dbo].[tbl_illness].[Individual_Id] = @IndividualId and [dbo].[tbl_illness].[Case_Id] = @CaseId";

        //    SqlCommand cmdUpdateIllness = new SqlCommand(strSqlUpdateIllness, connRNDB);
        //    cmdUpdateIllness.CommandType = CommandType.Text;

        //    if (strICD10Code != String.Empty) cmdUpdateIllness.Parameters.AddWithValue("@ICD10Code", strICD10Code);
        //    else cmdUpdateIllness.Parameters.AddWithValue("@ICD10Code", DBNull.Value);
        //    cmdUpdateIllness.Parameters.AddWithValue("@ModifiDate", DateTime.Today.ToString("yyyy-MM-dd"));
        //    cmdUpdateIllness.Parameters.AddWithValue("@ModifiStaff", nLoggedInUserId);
        //    cmdUpdateIllness.Parameters.AddWithValue("@DiagnosisDate", dtpDateOfDiagnosis.Value.ToString("MM/dd/yyyy"));
        //    cmdUpdateIllness.Parameters.AddWithValue("@Introduction", strIntroduction);
        //    cmdUpdateIllness.Parameters.AddWithValue("@Body", strIllnessNote);
        //    cmdUpdateIllness.Parameters.AddWithValue("@Conclusion", strConclusion);
        //    cmdUpdateIllness.Parameters.AddWithValue("@IndividualId", strIndividualId);
        //    cmdUpdateIllness.Parameters.AddWithValue("@CaseId", strCaseNo);

        //    connRNDB.Open();
        //    cmdUpdateIllness.ExecuteNonQuery();
        //    connRNDB.Close();


        //    Close();

        //}

        private void comboLimitedSharing_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 09/04/18 begin here
            // calculate the number of years of CMM membership

            // Use strIndividualNo

            String strSqlQueryForMembershipStartDate = "select [dbo].[membership].[c4g_start_date__c] from [dbo].[membership] " +
                                                       "inner join [dbo].[contact] on [dbo].[membership].[name] = [dbo].[contact].[membership_number__c] " +
                                                       "where [dbo].[contact].[individual_id__c] = @IndividualId";

            SqlCommand cmdQueryForMembershipStartDate = new SqlCommand(strSqlQueryForMembershipStartDate, connSalesforce);
            cmdQueryForMembershipStartDate.CommandType = CommandType.Text;

            cmdQueryForMembershipStartDate.Parameters.AddWithValue("@IndividualId", strIndividualNo);

            if (connSalesforce.State == ConnectionState.Open)
            {
                connSalesforce.Close();
                connSalesforce.Open();
            }
            else if (connSalesforce.State == ConnectionState.Closed) connSalesforce.Open();
            //DateTime dtMembershipStartDate = DateTime.Parse(cmdQueryForMembershipStartDate.ExecuteScalar().ToString());
            Object objMembershipStartDate = cmdQueryForMembershipStartDate.ExecuteScalar();
            if (connSalesforce.State == ConnectionState.Open) connSalesforce.Close();

            DateTime dtResultMembershipStartDate;
            DateTime? dtMembershipStartDate = null;

            if (objMembershipStartDate != null)
            {
                if (DateTime.TryParse(objMembershipStartDate.ToString(), out dtResultMembershipStartDate)) dtMembershipStartDate = dtResultMembershipStartDate;
            }
            else
            {
                MessageBox.Show("Invalid datetime value", "Error", MessageBoxButtons.OK);
                return;
            }


            int NumberOfYears = DateTime.Today.Year - dtMembershipStartDate.Value.Year;
            if (dtMembershipStartDate.Value.AddYears(NumberOfYears) > DateTime.Today) NumberOfYears--;

            int LimitedSharingYear = NumberOfYears;
            if (LimitedSharingYear > 4) LimitedSharingYear = 4;

            ComboBox combo = (ComboBox)sender;
            switch (combo.SelectedIndex)
            {
                case 1:
                    txtLimitedSharingYearlyLimit.Text = dicLimitedSharing1[LimitedSharingYear].ToString("C");
                    break;
                case 2:
                    txtLimitedSharingYearlyLimit.Text = dicLimitedSharing2[LimitedSharingYear].ToString("C");
                    break;
                default:
                    Decimal Zero = 0;
                    txtLimitedSharingYearlyLimit.Text = Zero.ToString("C");
                    break;
            }
        }

        private void dtpDateOfDiagnosis_ValueChanged(object sender, EventArgs e)
        {
            dtpDateOfDiagnosis.Format = DateTimePickerFormat.Short;
            //dtpDateOfDiagnosis.CustomFormat = null;
        }

        private void dtpDateOfDiagnosis_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                dtpDateOfDiagnosis.Format = DateTimePickerFormat.Custom;
                dtpDateOfDiagnosis.CustomFormat = " ";
            }
        }
    }

    public class ICD10CodeInfo
    {
        public String Id;
        public String Name;
        public String ICD10Code;

        public ICD10CodeInfo()
        {
            Id = String.Empty;
            Name = String.Empty;
            ICD10Code = String.Empty;
        }

        public ICD10CodeInfo(String strId, String strName, String strICD10Code)
        {
            Id = strId;
            Name = strName;
            ICD10Code = strICD10Code;
        }
    }
}
