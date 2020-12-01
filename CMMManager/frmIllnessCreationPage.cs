using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CMMManager
{

    public enum IllnessMode { AddNew, Edit };

    public partial class frmIllnessCreationPage : Form
    {
        private String connStringIcd10;
        //private String connStringIcd10_2;
        private SqlConnection conn_icd10codes;
        private String connStringSalesforce;
        private SqlConnection connSalesforce;
        private SqlCommand cmd_icd10;
        private String strSqlForICD10Codes;

        private String strRNConnection;
        private String strRNConnection2;

        private SqlConnection connRNDB;
        private SqlConnection connRNDB2;

        private String strCaseNoConnection;
        private SqlConnection connCaseNo;

        public int nIllnessId;
        public String IllnessNo = String.Empty;
        public String strIndividualNo = String.Empty;
        public String strCaseNo = String.Empty;

        public int nInsertedId;

        public int nLoggedInUserId;

        public DateTime MembershipStartDate;

        List<IllnessProgramInfo> lstIllnessProgramInfo;

        List<ICD10CodeInfo> lstICD10CodeInfo;

        Dictionary<int, String> dicLimitedSharingOptions;
        Dictionary<int, Decimal> dicLimitedSharing1;
        Dictionary<int, Decimal> dicLimitedSharing2;

        //public String strCaseId;

        public IllnessMode mode;
        public Boolean bOpenFromIllnessView;

        public void SetSqlSetting()
        {
            //connStringIcd10 = @"Data Source=cmm-2019data\CMM; Initial Catalog=SalesForce;Integrated Security=True; MultipleActiveResultSets=True";
            connStringIcd10 = @"Data Source=cmm-2019data\CMM; Initial Catalog=RN_DB;Integrated Security=True; Max Pool Size=200; MultipleActiveResultSets=True";
            //connStringIcd10_2 = @"Data Source=cmm-2019data\CMM; Initial Catalog=SalesForce;Integrated Security=True; MultipleActiveResultSets=True";
            //connStringIcd10 = @"Data Source=cmm-2019data\CMM; Initial Catalog=SalesForce;User ID=sa;Password=Yny00516; MultipleActiveResultSets=True";

            connStringSalesforce = @"Data Source=cmm-2019data\CMM; Initial Catalog=SalesForce;Integrated Security=True; MultipleActiveResultSets=True";
            //connStringSalesforce = @"Data Source=cmm-2019data\CMM; Initial Catalog=SalesForce;User ID=sa;Password=Yny00516; MultipleActiveResultSets=True";

            connSalesforce = new SqlConnection(connStringSalesforce);

            strSqlForICD10Codes = "select id, name, icd10_code__c from [dbo].[tbl_ICD10]";

            strRNConnection = @"Data Source=cmm-2019data\CMM; Initial Catalog=RN_DB;Integrated Security=True; Max Pool Size=200; MultipleActiveResultSets=True";
            strRNConnection2 = @"Data Source=cmm-2019data\CMM; Initial Catalog=RN_DB;Integrated Security=True; Max Pool Size=200; MultipleActiveResultSets=True";
            //strRNConnection = @"Data Source=cmm-2019data\CMM; Initial Catalog=RN_DB;User ID=sa;Password=Yny00516; Max Pool Size=200; MultipleActiveResultSets=True";
            connRNDB = new SqlConnection(strRNConnection);
            connRNDB2 = new SqlConnection(strRNConnection2);

            nInsertedId = 0;
          
            //SqlDependency.Start(strRNConnection);

        }

        public frmIllnessCreationPage()
        {
            InitializeComponent();
            SetSqlSetting();

            lstICD10CodeInfo = new List<ICD10CodeInfo>();
            lstIllnessProgramInfo = new List<IllnessProgramInfo>();

            String strSqlQueryForIllnessProgramInfo = "select [dbo].[tbl_program].[Program_Id], [dbo].[tbl_program].[ProgramName] from [dbo].[tbl_program] " +
                                                      "where [dbo].[tbl_program].[IsDeleted] = 0 or [dbo].[tbl_program].[IsDeleted] IS NULL";

            SqlCommand cmdQueryForIllnessProgramInfo = new SqlCommand(strSqlQueryForIllnessProgramInfo, connRNDB);
            cmdQueryForIllnessProgramInfo.CommandType = CommandType.Text;

            if (connRNDB.State != ConnectionState.Closed)
            {
                connRNDB.Close();
                connRNDB.Open();
            }
            else if (connRNDB.State == ConnectionState.Closed) connRNDB.Open();
            SqlDataReader rdrIllnessProgramInfo = cmdQueryForIllnessProgramInfo.ExecuteReader();
            if (rdrIllnessProgramInfo.HasRows)
            {
                while (rdrIllnessProgramInfo.Read())
                {
                    IllnessProgramInfo info = new IllnessProgramInfo();
                    if (!rdrIllnessProgramInfo.IsDBNull(0)) info.IllnessProgramId = rdrIllnessProgramInfo.GetInt16(0);
                    else info.IllnessProgramId = null;
                    if (!rdrIllnessProgramInfo.IsDBNull(1)) info.IllnessProgramName = rdrIllnessProgramInfo.GetString(1);
                    else info.IllnessProgramName = null;
                    lstIllnessProgramInfo.Add(info);
                }
            }
            rdrIllnessProgramInfo.Close();
            if (connRNDB.State != ConnectionState.Closed) connRNDB.Close();

            foreach (IllnessProgramInfo info in lstIllnessProgramInfo)
            {
                comboIllnessProgram.Items.Add(info.IllnessProgramName);
            }


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
            rdrLimitedSharingInfo.Close();
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
            rdrLimitedSharing1.Close();
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
            rdrLimitedSharing2.Close();
            if (connRNDB.State == ConnectionState.Open) connRNDB.Close();
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
            //txtCaseNo.Text = strCaseNo;

            List<CaseReceivedDate> lstCaseReceivedDate = new List<CaseReceivedDate>();

            string strSqlQueryForCaseForIndividual = "select [dbo].[tbl_case].[Case_Name] from [dbo].[tbl_case] " +
                                                     "where [dbo].[tbl_case].[Contact_ID] = @IndividualId and " +
                                                     "([dbo].[tbl_case].[IsDeleted] = 0 or [dbo].[tbl_case].[IsDeleted] IS NULL)";

            SqlCommand cmdQueyrForCaseForIndividual = new SqlCommand(strSqlQueryForCaseForIndividual, connRNDB);
            cmdQueyrForCaseForIndividual.CommandType = CommandType.Text;

            cmdQueyrForCaseForIndividual.Parameters.AddWithValue("@IndividualId", strIndividualNo);

            if (connRNDB.State != ConnectionState.Closed)
            {
                connRNDB.Close();
                connRNDB.Open();
            }
            else if (connRNDB.State == ConnectionState.Closed) connRNDB.Open();
            SqlDataReader rdrCase = cmdQueyrForCaseForIndividual.ExecuteReader();
            if (rdrCase.HasRows)
            {
                while (rdrCase.Read())
                {
                    //if (!rdrCase.IsDBNull(0)) comboCaseNoIllness.Items.Add(rdrCase.GetString(0));
                    CaseReceivedDate caseInfo = new CaseReceivedDate();
                    if (!rdrCase.IsDBNull(0)) caseInfo.CaseNo = rdrCase.GetString(0);
                    lstCaseReceivedDate.Add(caseInfo);
                }
            }
            rdrCase.Close();
            if (connRNDB.State != ConnectionState.Closed) connRNDB.Close();

            foreach (CaseReceivedDate info in lstCaseReceivedDate)
            {
                String strSqlQueryForCaseReceivedDate = "select top(1) [dbo].[tbl_case_doc].[ReceivedDate] from [dbo].[tbl_case_doc] " +
                                                        "where [dbo].[tbl_case_doc].[Case_Name] = @CaseNo and " +
                                                        "([dbo].[tbl_case_doc].[IsDeleted] = 0 or [dbo].[tbl_case_doc].[IsDeleted] IS NULL) " +
                                                        "order by [dbo].[tbl_case_doc].[ReceivedDate]";

                SqlCommand cmdQueryForCaseReceivedDate = new SqlCommand(strSqlQueryForCaseReceivedDate, connRNDB);
                cmdQueryForCaseReceivedDate.CommandType = CommandType.Text;

                cmdQueryForCaseReceivedDate.Parameters.AddWithValue("@CaseNo", info.CaseNo);

                if (connRNDB.State != ConnectionState.Closed)
                {
                    connRNDB.Close();
                    connRNDB.Open();
                }
                else if (connRNDB.State == ConnectionState.Closed) connRNDB.Open();
                SqlDataReader rdrReceivedDate = cmdQueryForCaseReceivedDate.ExecuteReader();
                if (rdrReceivedDate.HasRows)
                {
                    rdrReceivedDate.Read();
                    if (!rdrReceivedDate.IsDBNull(0)) info.dtReceivedDate = rdrReceivedDate.GetDateTime(0);
                }
                rdrReceivedDate.Close();
                if (connRNDB.State != ConnectionState.Closed) connRNDB.Close();
            }

            foreach (CaseReceivedDate info in lstCaseReceivedDate)
            {
                if (info.CaseNo != null && info.dtReceivedDate != null) comboCaseNoIllness.Items.Add(info.CaseNo + ": " + info.dtReceivedDate.Value.ToString("MM/dd/yyyy"));                
            }

            //comboCaseNoIllness.SelectedItem = strCaseNo;
            //comboCaseNoIllness.Enabled = false;
            if (comboCaseNoIllness.Items.Count > 0)
            {
                comboCaseNoIllness.SelectedIndex = 0;
                comboCaseNoIllness.Enabled = false;
            }

            conn_icd10codes = new SqlConnection(connStringIcd10);
            cmd_icd10 = conn_icd10codes.CreateCommand();

            cmd_icd10.CommandType = CommandType.Text;
            cmd_icd10.CommandText = strSqlForICD10Codes;
            lstICD10CodeInfo.Clear();

            if (conn_icd10codes.State != ConnectionState.Closed)
            {
                conn_icd10codes.Close();
                conn_icd10codes.Open();
            }
            else if (conn_icd10codes.State == ConnectionState.Closed) conn_icd10codes.Open();
            SqlDataReader rdr_icd10codes = cmd_icd10.ExecuteReader();

            if (rdr_icd10codes.HasRows)
            {
                while (rdr_icd10codes.Read())
                {
                    //lstICD10CodeInfo.Add(new ICD10CodeInfo { Id = rdr_icd10codes.GetString(0), Name = rdr_icd10codes.GetString(1), ICD10Code = rdr_icd10codes.GetString(2) });
                    lstICD10CodeInfo.Add(new ICD10CodeInfo { Id = rdr_icd10codes.GetInt32(0), Name = rdr_icd10codes.GetString(1), ICD10Code = rdr_icd10codes.GetString(2) });
                }
            }
            rdr_icd10codes.Close();
            if (conn_icd10codes.State == ConnectionState.Open) conn_icd10codes.Close();

            var srcICD10Codes = new AutoCompleteStringCollection();

            for (int i = 0; i < lstICD10CodeInfo.Count; i++)
            {
                srcICD10Codes.Add(lstICD10CodeInfo[i].ICD10Code);
            }

            txtICD10Code.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtICD10Code.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtICD10Code.AutoCompleteCustomSource = srcICD10Codes;

            //dtpDateOfDiagnosis.Format = DateTimePickerFormat.Custom;
            //dtpDateOfDiagnosis.CustomFormat = " ";

            dtpDateOfDiagnosis.Value = DateTime.Today;

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

                //if (bOpenFromIllnessView) comboCaseNoIllness.Enabled = true;
                //else comboCaseNoIllness.Enabled = false;
                if (comboCaseNoIllness.Items.Count > 1) comboCaseNoIllness.Enabled = true;
                //List<IllnessProgramHistory> lstIllnessProgramHistory = new List<IllnessProgramHistory>();

                //String strSqlQueryForMemberProgramHistory = "select [dbo].[ContactHistory].[CreateDate], [dbo].[ContactHistory].[OldValue], [dbo].[ContactHistory].[NewValue] " +
                //                                            "from [dbo].[ContactHistory] " +
                //                                            "inner join [dbo].[Contact] on [dbo].[ContactHistory].[ContactId] = [dbo].[Contact].[Id] " +
                //                                            "where [dbo].[Contact].[Individual_ID__c] = @IndividualId and " +
                //                                            "cast([dbo].[ContactHistory].[Field] as nvarchar(max)) = 'c4g_Plan__c'";

                //SqlCommand cmdQueryForMemberProgramHistory = new SqlCommand(strSqlQueryForMemberProgramHistory, connSalesforce);
                //cmdQueryForMemberProgramHistory.CommandType = CommandType.Text;

                //cmdQueryForMemberProgramHistory.Parameters.AddWithValue("@IndividualId", strIndividualNo);

                //if (connSalesforce.State != ConnectionState.Closed)
                //{
                //    connSalesforce.Close();
                //    connSalesforce.Open();
                //}
                //else if (connSalesforce.State == ConnectionState.Closed) connSalesforce.Open();
                //SqlDataReader rdrIllnessProgramHistory = cmdQueryForMemberProgramHistory.ExecuteReader();
                //if (rdrIllnessProgramHistory.HasRows)
                //{
                //    while (rdrIllnessProgramHistory.Read())
                //    {
                //        IllnessProgramHistory program = new IllnessProgramHistory();
                //        if (!rdrIllnessProgramHistory.IsDBNull(0)) program.CreateDate = rdrIllnessProgramHistory.GetDateTime(0);
                //        if (!rdrIllnessProgramHistory.IsDBNull(1))
                //        {
                //            switch (rdrIllnessProgramHistory.GetString(1).Trim())
                //            {
                //                case "GoldPlus":
                //                    program.OldProgram = IllnessProgram.GoldPlus;
                //                    break;
                //                case "Gold":
                //                    program.OldProgram = IllnessProgram.Gold;
                //                    break;
                //                case "Silver":
                //                    program.OldProgram = IllnessProgram.Silver;
                //                    break;
                //                case "Bronze":
                //                    program.OldProgram = IllnessProgram.Bronze;
                //                    break;
                //                case "Gold Medi-I":
                //                    program.OldProgram = IllnessProgram.GoldMedi_I;
                //                    break;
                //                case "Gold Medi-II":
                //                    program.OldProgram = IllnessProgram.GoldMedi_II;
                //                    break;
                //            }
                //        }
                //        if (!rdrIllnessProgramHistory.IsDBNull(2))
                //        {
                //            switch (rdrIllnessProgramHistory.GetString(2).Trim())
                //            {
                //                case "GoldPlus":
                //                    program.NewProgram = IllnessProgram.GoldPlus;
                //                    break;
                //                case "Gold":
                //                    program.NewProgram = IllnessProgram.Gold;
                //                    break;
                //                case "Silver":
                //                    program.NewProgram = IllnessProgram.Silver;
                //                    break;
                //                case "Bronze":
                //                    program.NewProgram = IllnessProgram.Bronze;
                //                    break;
                //                case "Gold Medi-I":
                //                    program.NewProgram = IllnessProgram.GoldMedi_I;
                //                    break;
                //                case "Gold Medi-II":
                //                    program.NewProgram = IllnessProgram.GoldMedi_II;
                //                    break;
                //            }
                //        }

                //        lstIllnessProgramHistory.Add(program);                        
                //    }
                //}
                //rdrIllnessProgramHistory.Close();
                //if (connSalesforce.State != ConnectionState.Closed) connSalesforce.Close();

                //if (lstIllnessProgramHistory.Count > 0)
                //{
                    
                //}


                //String strSqlQueryForMemberProgram = "select [dbo].[Program].[Name] from [dbo].[Contact] " +
                //                                     "inner join [dbo].[Program] on [dbo].[Contact].[c4g_Plan__c] = [dbo].[Program].[ID] " +
                //                                     "where [dbo].[Contact].[Individual_ID__c] = @IndividualId";

                //SqlCommand cmdQueryForMemberProgram = new SqlCommand(strSqlQueryForMemberProgram, connSalesforce);
                //cmdQueryForMemberProgram.CommandType = CommandType.Text;

                //cmdQueryForMemberProgram.Parameters.AddWithValue("@IndividualId", strIndividualNo);

                //if (connSalesforce.State != ConnectionState.Closed)
                //{
                //    connSalesforce.Close();
                //    connSalesforce.Open();
                //}
                //else if (connSalesforce.State == ConnectionState.Closed) connSalesforce.Open();
                //Object objMemberProgram = cmdQueryForMemberProgram.ExecuteScalar();
                //if (connSalesforce.State != ConnectionState.Closed) connSalesforce.Close();

                //String strMemberProgram = objMemberProgram?.ToString();

                //switch (strMemberProgram.Trim())
                //{
                //    case "Gold Plus":
                //        comboIllnessProgram.SelectedIndex = 0;
                //        break;
                //    case "Gold":
                //        comboIllnessProgram.SelectedIndex = 1;
                //        break;
                //    case "Silver":
                //        comboIllnessProgram.SelectedIndex = 2;
                //        break;
                //    case "Bronze":
                //        comboIllnessProgram.SelectedIndex = 3;
                //        break;
                //    case "Gold Medi-I":
                //        comboIllnessProgram.SelectedIndex = 4;
                //        break;
                //    case "Gold Medi-II":
                //        comboIllnessProgram.SelectedIndex = 5;
                //        break;
                //}
            }
            else if (mode == IllnessMode.Edit)
            {
                //int IllnessId = nIllnessNo.Value;

                Int32? LimitedSharingId = null;

                if (bOpenFromIllnessView) comboCaseNoIllness.Enabled = true;
                else comboCaseNoIllness.Enabled = false;

                String strSqlQueryForIllness = "select [dbo].[tbl_illness].[Illness_Id], [dbo].[tbl_illness].[ICD_10_Id], [dbo].[tbl_illness].[CreateDate], [dbo].[tbl_illness].[ModifiDate], " +
                                               "[dbo].[tbl_illness].[Individual_Id], [dbo].[tbl_illness].[Case_Id], [dbo].[tbl_illness].[Date_of_Diagnosis], " +
                                               "[dbo].[tbl_illness].[LimitedSharingId], " +
                                               "[dbo].[tbl_illness].[Introduction], [dbo].[tbl_illness].[Body], [dbo].[tbl_illness].[Conclusion], " +
                                               "[dbo].[tbl_illness].[Program_Id] " +
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
                    if (!rdrIllness.IsDBNull(1))
                    {
                        //txtICD10Code.Text = rdrIllness.GetString(1);
                        String strICD10Code = rdrIllness.GetString(1);
                        txtICD10Code.Text = strICD10Code;
                        SetIllnessEligibility(strICD10Code);
                    }
                    if (!rdrIllness.IsDBNull(2))
                    {
                        dtpCreateDate.Value = rdrIllness.GetDateTime(2);
                        dtpCreateDate.Enabled = false;
                    }
                    if (!rdrIllness.IsDBNull(4)) txtIndividualNo.Text = rdrIllness.GetString(4);
                    //if (!rdrIllness.IsDBNull(5)) txtCaseNo.Text = rdrIllness.GetString(5);
                    if (!rdrIllness.IsDBNull(5))
                    {
                        //comboCaseNoIllness.SelectedItem = rdrIllness.GetString(5);
                        for (int i = 0; i < comboCaseNoIllness.Items.Count; i++)
                        {
                            if (comboCaseNoIllness.Items[i].ToString().Contains(rdrIllness.GetString(5))) comboCaseNoIllness.SelectedIndex = i;
                        }
                        comboCaseNoIllness.Enabled = false;
                    }
                        
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
                    
                    if (!rdrIllness.IsDBNull(7))
                    {
                        //comboLimitedSharing.SelectedIndex = rdrIllness.GetInt16(7);
                        LimitedSharingId = (Int32)rdrIllness.GetInt16(7);
                        //comboLimitedSharing.SelectedIndex = LimitedSharingId;
                    }
                    if (!rdrIllness.IsDBNull(8)) txtIllnessNote.Text = rdrIllness.GetString(8) + Environment.NewLine;
                    if (!rdrIllness.IsDBNull(9)) txtIllnessNote.Text += rdrIllness.GetString(9);
                    if (!rdrIllness.IsDBNull(10)) txtConclusion.Text = rdrIllness.GetString(10);
                    if (!rdrIllness.IsDBNull(11)) comboIllnessProgram.SelectedIndex = rdrIllness.GetByte(11);
                    else comboIllnessProgram.SelectedIndex = -1;
                                        
                }
                rdrIllness.Close();
                if (connRNDB.State == ConnectionState.Open) connRNDB.Close();

                if (LimitedSharingId != null) comboLimitedSharing.SelectedIndex = LimitedSharingId.Value;


                String strSqlQueryForIncidentInfoForIllness = "select [dbo].[tbl_incident].[IncidentNo], [dbo].[tbl_program].[ProgramName], " +
                                                           "[dbo].[tbl_incident].[IsWellBeing], [dbo].[tbl_incident].[OccurrenceDate], " +
                                                           "[dbo].[tbl_incident].[TotalSharedAmount], [dbo].[tbl_incident].[IncidentNote] " +
                                                           "from [dbo].[tbl_incident] " +
                                                           "inner join [dbo].[tbl_illness] on [dbo].[tbl_incident].[Illness_id] = [dbo].[tbl_illness].[Illness_Id] " +
                                                           "inner join [dbo].[tbl_program] on [dbo].[tbl_incident].[Program_id] = [dbo].[tbl_program].[Program_Id] " +
                                                           "where [dbo].[tbl_illness].[IllnessNo] = @IllnessNo";

                SqlCommand cmdQueryForIncidentInfoForIllness = new SqlCommand(strSqlQueryForIncidentInfoForIllness, connRNDB);
                cmdQueryForIncidentInfoForIllness.CommandType = CommandType.Text;

                cmdQueryForIncidentInfoForIllness.Parameters.AddWithValue("@IllnessNo", IllnessNo);

                if (connRNDB.State != ConnectionState.Closed)
                {
                    connRNDB.Close();
                    connRNDB.Open();
                }
                else if (connRNDB.State == ConnectionState.Closed) connRNDB.Open();
                SqlDataReader rdrIncidentInfo = cmdQueryForIncidentInfoForIllness.ExecuteReader();
                if (rdrIncidentInfo.HasRows)
                {
                    while (rdrIncidentInfo.Read())
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        if (!rdrIncidentInfo.IsDBNull(0)) row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrIncidentInfo.GetString(0) });
                        else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                        if (!rdrIncidentInfo.IsDBNull(1)) row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrIncidentInfo.GetString(1).Trim() });
                        else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                        if (!rdrIncidentInfo.IsDBNull(2))
                        {
                            if (rdrIncidentInfo.GetBoolean(2)) row.Cells.Add(new DataGridViewTextBoxCell { Value = "Well Being" });
                            else row.Cells.Add(new DataGridViewTextBoxCell { Value = "Incident" });
                        }
                        else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                        if (!rdrIncidentInfo.IsDBNull(3)) row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrIncidentInfo.GetDateTime(3).ToString("MM/dd/yyyy") });
                        else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                        if (!rdrIncidentInfo.IsDBNull(4)) row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrIncidentInfo.GetDecimal(4).ToString("C") });
                        else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                        if (!rdrIncidentInfo.IsDBNull(5)) row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrIncidentInfo.GetString(5) });
                        else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });

                        gvRelatedIncidentInfo.Rows.Add(row);
                    }
                }
                rdrIncidentInfo.Close();
                if (connRNDB.State != ConnectionState.Closed) connRNDB.Close();

            }

            if (comboIllnessProgram.SelectedIndex == -1)
            {
                String strSqlQueryForMemberProgram = "select [dbo].[Program].[Name] from [dbo].[Contact] " +
                                     "inner join [dbo].[Program] on [dbo].[Contact].[c4g_Plan__c] = [dbo].[Program].[ID] " +
                                     "where [dbo].[Contact].[Individual_ID__c] = @IndividualId";

                SqlCommand cmdQueryForMemberProgram = new SqlCommand(strSqlQueryForMemberProgram, connSalesforce);
                cmdQueryForMemberProgram.CommandType = CommandType.Text;

                cmdQueryForMemberProgram.Parameters.AddWithValue("@IndividualId", strIndividualNo);

                if (connSalesforce.State != ConnectionState.Closed)
                {
                    connSalesforce.Close();
                    connSalesforce.Open();
                }
                else if (connSalesforce.State == ConnectionState.Closed) connSalesforce.Open();
                Object objMemberProgram = cmdQueryForMemberProgram.ExecuteScalar();
                if (connSalesforce.State != ConnectionState.Closed) connSalesforce.Close();

                String strMemberProgram = objMemberProgram?.ToString();

                switch (strMemberProgram.Trim())
                {
                    case "Gold Plus":
                        comboIllnessProgram.SelectedIndex = 0;
                        break;
                    case "Gold":
                        comboIllnessProgram.SelectedIndex = 1;
                        break;
                    case "Silver":
                        comboIllnessProgram.SelectedIndex = 2;
                        break;
                    case "Bronze":
                        comboIllnessProgram.SelectedIndex = 3;
                        break;
                    case "Gold Medi-I":
                        comboIllnessProgram.SelectedIndex = 4;
                        break;
                    case "Gold Medi-II":
                        comboIllnessProgram.SelectedIndex = 5;
                        break;
                }
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

        private void SetIllnessEligibility(String icd10code)
        {
            String ICD10CodeUpperCase = icd10code.ToUpper();

            if (ICD10CodeUpperCase.Contains("F17") ||
                ICD10CodeUpperCase.Contains("Z33.2") ||
                ICD10CodeUpperCase.Contains("Z71.51") ||
                ICD10CodeUpperCase.Contains("X78.9XXA") ||
                ICD10CodeUpperCase.Contains("B20") ||
                ICD10CodeUpperCase.Contains("F41.9") ||
                ICD10CodeUpperCase.Contains("F90.0") ||
                ICD10CodeUpperCase.Contains("G47.00") ||
                ICD10CodeUpperCase.Contains("F39"))
            {
                rbEligible.Checked = false;
                rbIneligible.Checked = true;
            }
        }

        private void btnSaveIllness_Click(object sender, EventArgs e)
        {
            //int IllnessId = nIllnessNo.Value;

            
            String strICD10Code = String.Empty;

            //String strIllnessId = String.Empty;
            String strIndividualId = String.Empty;
            //String strIntroduction = String.Empty;
            String strIllnessNote = String.Empty;
            String strConclusion = String.Empty;

            //if (txtIllnessId.Text.Trim() != String.Empty) strIllnessId = txtIllnessId.Text.Trim();
            if (txtICD10Code.Text.Trim() != String.Empty) strICD10Code = txtICD10Code.Text.Trim();
            if (txtIndividualNo.Text.Trim() != String.Empty) strIndividualId = txtIndividualNo.Text.Trim();
            //if (txtIntroduction.Text.Trim() != String.Empty) strIntroduction = txtIntroduction.Text.Trim();
            if (txtIllnessNote.Text.Trim() != String.Empty) strIllnessNote = txtIllnessNote.Text.Trim();
            if (txtConclusion.Text.Trim() != String.Empty) strConclusion = txtConclusion.Text.Trim();

            // This section of code (if block) needs further discussion in RN project meeting
            //if (strICD10Code.ToUpper() == "Z00.00" || strICD10Code.ToUpper() == "Z00.012")
            //{
            //    String strSqlQueryForGoldPlusPlan = "select [dbo].[contact].[Individual_ID__c], [dbo].[program].[Name], [dbo].[contact].[Membership_IND_Start_date__c] " +
            //                                        "from [dbo].[contact] " +
            //                                        "inner join [dbo].[program] on [dbo].[contact].[c4g_Plan__c] = [dbo].[program].[ID] " +
            //                                        "where [dbo].[contact].[Individual_ID__c] = @IndividualId";

            //    SqlCommand cmdQueryForGoldPlusPlan = new SqlCommand(strSqlQueryForGoldPlusPlan, connSalesforce);
            //    cmdQueryForGoldPlusPlan.CommandType = CommandType.Text;

            //    cmdQueryForGoldPlusPlan.Parameters.AddWithValue("@IndividualId", strIndividualId);

            //    if (connSalesforce.State != ConnectionState.Closed)
            //    {
            //        connSalesforce.Close();
            //        connSalesforce.Open();
            //    }
            //    else if (connSalesforce.State == ConnectionState.Closed) connSalesforce.Open();

            //    String IndividualPlan = String.Empty;
            //    String IndividualId = String.Empty;
            //    DateTime? MembershipStartDate = new DateTime();
            //    Boolean bIndividualId = true;
            //    Boolean bIndividualPlan = true;
            //    Boolean bMembershipStartDate = true;

            //    SqlDataReader rdrGoldPlusPlan = cmdQueryForGoldPlusPlan.ExecuteReader();
            //    if (rdrGoldPlusPlan.HasRows)
            //    {
            //        rdrGoldPlusPlan.Read();
            //        if (!rdrGoldPlusPlan.IsDBNull(0)) IndividualId = rdrGoldPlusPlan.GetString(0);
            //        else bIndividualId = false;
            //        if (!rdrGoldPlusPlan.IsDBNull(1)) IndividualPlan = rdrGoldPlusPlan.GetString(1);
            //        else bIndividualPlan = false;
            //        if (!rdrGoldPlusPlan.IsDBNull(2)) MembershipStartDate = rdrGoldPlusPlan.GetDateTime(2);
            //        else bMembershipStartDate = false;
                    
            //    }
            //    if (connSalesforce.State == ConnectionState.Open) connSalesforce.Close();

            //    if (bIndividualId == false)
            //    {
            //        MessageBox.Show("No such Individual ID", "Error");
            //        return;
            //    }

            //    if (bIndividualPlan == false)
            //    {
            //        MessageBox.Show("The individual has no membership plan.", "Error");
            //        return;
            //    }

            //    if (bMembershipStartDate == false)
            //    {
            //        MessageBox.Show("The individual has no start date.", "Error");
            //        return;
            //    }

            //    if (IndividualPlan != "Gold Plus" &&
            //        IndividualPlan != "Gold Medi-I" &&
            //        IndividualPlan != "Gold Medi-II")
            //    {
            //        MessageBox.Show("Individual Plan does not qualify for Well Being Care.", "Alert");
            //        return;
            //    }

            //    DateTime WellBeingCareBeginDate = MembershipStartDate.Value.AddMonths(6);

            //}

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
                                             "[dbo].[tbl_illness].[Body], [dbo].[tbl_illness].[Conclusion], [dbo].[tbl_illness].[Program_Id], " +
                                             "[dbo].[tbl_illness].[IsRemoved]) " +
                                             "values (@IllnessNo, @IsDeleted, @ICD10Code, @CreateDate, @ModifiDate, " +
                                             "@CreateStaff, @ModifiStaff, @IndividualId, @CaseId, " +
                                             "@LimitedSharingId, @DateOfDiagnosis, @RemoveLog, " +
                                             "@Body, @Conclusion, @ProgramId, 0)";

                //String strRNConnection = @"Data Source=cmm-2019data\CMM; Initial Catalog=RN_DB; Integrated Security=True";
                //SqlConnection connRNDB = new SqlConnection(strRNConnection);

                SqlCommand cmdCreateIllness = new SqlCommand(strSqlCreateIllness, connRNDB);
                cmdCreateIllness.CommandType = CommandType.Text;

                cmdCreateIllness.Parameters.AddWithValue("@IllnessNo", IllnessNo);
                cmdCreateIllness.Parameters.AddWithValue("@IsDeleted", 0);

                if (strICD10Code != String.Empty) cmdCreateIllness.Parameters.AddWithValue("@ICD10Code", strICD10Code);
                else cmdCreateIllness.Parameters.AddWithValue("@ICD10Code", DBNull.Value);

                cmdCreateIllness.Parameters.AddWithValue("@CreateDate", DateTime.Now);
                cmdCreateIllness.Parameters.AddWithValue("@ModifiDate", DateTime.Now);
                cmdCreateIllness.Parameters.AddWithValue("@CreateStaff", nLoggedInUserId);
                cmdCreateIllness.Parameters.AddWithValue("@ModifiStaff", nLoggedInUserId);
                cmdCreateIllness.Parameters.AddWithValue("@IndividualId", strIndividualId);
                //cmdCreateIllness.Parameters.AddWithValue("@CaseId", txtCaseNo.Text.Trim());                
                String CaseNoReceivedDate = comboCaseNoIllness.SelectedItem.ToString();
                //String CaseNo = CaseNoReceivedDate.Substring(0, CaseNoReceivedDate.IndexOf(':'));
                cmdCreateIllness.Parameters.AddWithValue("@CaseId", CaseNoReceivedDate.Substring(0, CaseNoReceivedDate.IndexOf(':')));
                //cmdCreateIllness.Parameters.AddWithValue("@CaseId", comboCaseNoIllness.SelectedItem.ToString());
                cmdCreateIllness.Parameters.AddWithValue("@LimitedSharingId", comboLimitedSharing.SelectedIndex);
                if (dtpDateOfDiagnosis.Text.Trim() != String.Empty) cmdCreateIllness.Parameters.AddWithValue("@DateOfDiagnosis", dtpDateOfDiagnosis.Value);
                else cmdCreateIllness.Parameters.AddWithValue("@DateOfDiagnosis", DBNull.Value);
                cmdCreateIllness.Parameters.AddWithValue("@RemoveLog", DBNull.Value);
                //if (strIntroduction != String.Empty) cmdCreateIllness.Parameters.AddWithValue("@Introduction", strIntroduction);
                //else cmdCreateIllness.Parameters.AddWithValue("@Introduction", DBNull.Value);
                if (strIllnessNote != String.Empty) cmdCreateIllness.Parameters.AddWithValue("@Body", strIllnessNote);
                else cmdCreateIllness.Parameters.AddWithValue("@Body", DBNull.Value);
                if (strConclusion != String.Empty) cmdCreateIllness.Parameters.AddWithValue("@Conclusion", strConclusion);
                else cmdCreateIllness.Parameters.AddWithValue("@Conclusion", DBNull.Value);
                if (comboIllnessProgram.SelectedIndex != -1) cmdCreateIllness.Parameters.AddWithValue("@ProgramId", comboIllnessProgram.SelectedIndex);
                else cmdCreateIllness.Parameters.AddWithValue("@ProgramId", DBNull.Value);

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
                //else if (nRowInserted == 0)
                else
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
                //if (txtCaseNo.Text.Trim() != String.Empty) strCaseNo = txtCaseNo.Text.Trim();
                if (comboCaseNoIllness.SelectedItem != null) strCaseNo = comboCaseNoIllness.SelectedItem.ToString().Substring(0, comboCaseNoIllness.SelectedItem.ToString().IndexOf(':'));
                //if (txtIntroduction.Text.Trim() != String.Empty) strIntroduction = txtIntroduction.Text.Trim();

                if (txtIllnessNote.Text.Trim() != String.Empty) strIllnessNote = txtIllnessNote.Text.Trim();
                if (txtConclusion.Text.Trim() != String.Empty) strConclusion = txtConclusion.Text.Trim();

                String strSqlUpdateIllness = "update [dbo].[tbl_illness] set [dbo].[tbl_illness].[icd_10_Id] = @ICD10Code, [dbo].[tbl_illness].[ModifiDate] = @ModifiDate, " +
                                             "[dbo].[tbl_illness].[ModifiStaff] = @ModifiStaff, [dbo].[tbl_illness].[Date_of_Diagnosis] = @DiagnosisDate, " +
                                             "[dbo].[tbl_illness].[LimitedSharingId] = @LimitedSharingId, " +
                                             "[dbo].[tbl_illness].[Body] = @Body, [dbo].[tbl_illness].[Conclusion] = @Conclusion, " +
                                             "[dbo].[tbl_illness].[Program_Id] = @ProgramId " +
                                             "where [dbo].[tbl_illness].[Individual_Id] = @IndividualId and [dbo].[tbl_illness].[Case_Id] = @CaseId and " +
                                             "[dbo].[tbl_illness].[IllnessNo] = @IllnessNo";

                SqlCommand cmdUpdateIllness = new SqlCommand(strSqlUpdateIllness, connRNDB);
                cmdUpdateIllness.CommandType = CommandType.Text;

                if (strICD10Code != String.Empty) cmdUpdateIllness.Parameters.AddWithValue("@ICD10Code", strICD10Code);
                else cmdUpdateIllness.Parameters.AddWithValue("@ICD10Code", DBNull.Value);
                cmdUpdateIllness.Parameters.AddWithValue("@ModifiDate", DateTime.Now);
                cmdUpdateIllness.Parameters.AddWithValue("@ModifiStaff", nLoggedInUserId);
                cmdUpdateIllness.Parameters.AddWithValue("@DiagnosisDate", dtpDateOfDiagnosis.Value);
                cmdUpdateIllness.Parameters.AddWithValue("@LimitedSharingId", comboLimitedSharing.SelectedIndex);
                //cmdUpdateIllness.Parameters.AddWithValue("@Introduction", strIntroduction);
                cmdUpdateIllness.Parameters.AddWithValue("@Body", strIllnessNote);
                cmdUpdateIllness.Parameters.AddWithValue("@Conclusion", strConclusion);
                if (comboIllnessProgram.SelectedIndex != -1) cmdUpdateIllness.Parameters.AddWithValue("@ProgramId", comboIllnessProgram.SelectedIndex);
                else cmdUpdateIllness.Parameters.AddWithValue("@ProgramId", DBNull.Value);
                cmdUpdateIllness.Parameters.AddWithValue("@IndividualId", strIndividualId);

                String CaseNoReceivedDate = comboCaseNoIllness.SelectedItem.ToString();
                String CaseNo = CaseNoReceivedDate.Substring(0, CaseNoReceivedDate.IndexOf(':'));

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
            rbEligible.Checked = false;
            rbIneligible.Checked = false;

            String strICD10Code = txtICD10Code.Text;

            String strSqlQueryForDiseaseName = "select [dbo].[tbl_ICD10].[Name] from [dbo].[tbl_ICD10] where [dbo].[tbl_ICD10].[ICD10_Code__c] = @ICD10Code";

            SqlCommand cmdQueryForDiseaseName = new SqlCommand(strSqlQueryForDiseaseName, connRNDB2);
            cmdQueryForDiseaseName.CommandType = CommandType.Text;

            cmdQueryForDiseaseName.Parameters.AddWithValue("@ICD10Code", strICD10Code);

            if (connRNDB2.State != ConnectionState.Closed)
            {
                connRNDB2.Close();
                connRNDB2.Open();
            }
            else if (connRNDB2.State == ConnectionState.Closed) connRNDB2.Open();
            Object objDiseaseName = cmdQueryForDiseaseName.ExecuteScalar();

            if (connRNDB2.State != ConnectionState.Closed) connRNDB2.Close();

            //String strSqlQueryForDiseaseName = "select [dbo].[ICD10 Code].[Name] from [dbo].[ICD10 Code] where [dbo].[ICD10 Code].[ICD10_Code__c] = @ICD10Code";

            //SqlCommand cmdQueryForDiseaseName = new SqlCommand(strSqlQueryForDiseaseName, connSalesforce);
            //cmdQueryForDiseaseName.CommandType = CommandType.Text;

            //cmdQueryForDiseaseName.Parameters.AddWithValue("@ICD10Code", strICD10Code);

            //if (connSalesforce.State != ConnectionState.Closed)
            //{
            //    connSalesforce.Close();
            //    connSalesforce.Open();
            //}
            //else if (connSalesforce.State == ConnectionState.Closed) connSalesforce.Open();
            //Object objDiseaseName = cmdQueryForDiseaseName.ExecuteScalar();
            //if (connSalesforce.State != ConnectionState.Closed) connSalesforce.Close();

            String strDiseaseName = objDiseaseName?.ToString();
            //if (objDiseaseName != null) strDiseaseName = objDiseaseName.ToString();

            txtDiseaseName.Text = strDiseaseName?.Trim();

            //String strICD10CodeUpperCase = strICD10Code;
            SetIllnessEligibility(strICD10Code);
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

            String strSqlQueryForIndividualStartDate = "select [dbo].[contact].[Membership_IND_Start_date__c] from [dbo].[contact] " +
                                                       "where [dbo].[contact].[individual_id__c] = @IndividualId";

            SqlCommand cmdQueryForIndividualStartDate = new SqlCommand(strSqlQueryForIndividualStartDate, connSalesforce);
            cmdQueryForIndividualStartDate.CommandType = CommandType.Text;

            cmdQueryForIndividualStartDate.Parameters.AddWithValue("@IndividualId", strIndividualNo);

            if (connSalesforce.State != ConnectionState.Closed)
            {
                connSalesforce.Close();
                connSalesforce.Open();
            }
            else if (connSalesforce.State == ConnectionState.Closed) connSalesforce.Open();
            Object objIndividualStartDate = cmdQueryForIndividualStartDate.ExecuteScalar();
            if (connSalesforce.State == ConnectionState.Open) connSalesforce.Close();

            DateTime dtResultIndividualStartDate;
            DateTime? dtIndividualStartDate = null;

            if (objIndividualStartDate != null)
            {
                if (DateTime.TryParse(objIndividualStartDate.ToString(), out dtResultIndividualStartDate)) dtIndividualStartDate = dtResultIndividualStartDate;
            }
            else
            {
                MessageBox.Show("Invalid datetime value", "Error", MessageBoxButtons.OK);
                return;
            }

            int NumberOfYears = DateTime.Today.Year - dtIndividualStartDate.Value.Year;
            if (dtIndividualStartDate.Value.AddYears(NumberOfYears) > DateTime.Today) NumberOfYears--;

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
                    txtYearlyLimitBalance.Text = String.Empty;
                    break;
            }

            Double LimitedSharingYearlyLimit = 0;

            String strLimitedSharingYearlyLimit = txtLimitedSharingYearlyLimit.Text.Trim();

            Double resultLimitedSharingYearlyLimit = 0;

            if (Double.TryParse(strLimitedSharingYearlyLimit, NumberStyles.Currency, new CultureInfo("en-US"), out resultLimitedSharingYearlyLimit))
            {
                LimitedSharingYearlyLimit = resultLimitedSharingYearlyLimit;

                if (LimitedSharingYearlyLimit == 0) return;
            }

            String IllnessNo = txtIllnessNo.Text?.Trim();

            Double IllnessTotalSharedAmount = 0;

            if (IllnessNo != null && IllnessNo != String.Empty)
            {
                String strSqlQueryForTotalSharedAmount = "select [dbo].[tbl_illness].[TotalSharedAmount] from [dbo].[tbl_illness] " +
                                                         "where [dbo].[tbl_illness].[IllnessNo] = @IllnessNo";

                SqlCommand cmdQueryForTotalSharedAmount = new SqlCommand(strSqlQueryForTotalSharedAmount, connRNDB);
                cmdQueryForTotalSharedAmount.CommandType = CommandType.Text;

                cmdQueryForTotalSharedAmount.Parameters.AddWithValue("@IllnessNo", IllnessNo);

                if (connRNDB.State != ConnectionState.Closed)
                {
                    connRNDB.Close();
                    connRNDB.Open();
                }
                else if (connRNDB.State == ConnectionState.Closed) connRNDB.Open();
                Object objIllnessTotalSharedAmount = cmdQueryForTotalSharedAmount.ExecuteScalar();
                if (connRNDB.State != ConnectionState.Closed) connRNDB.Close();

                String strIllnessTotalSharedAmount = objIllnessTotalSharedAmount?.ToString();

                if (strIllnessTotalSharedAmount != null)
                {
                    Double resultIllnessTotalSharedAmount = 0;
                    if (Double.TryParse(strIllnessTotalSharedAmount, out resultIllnessTotalSharedAmount)) IllnessTotalSharedAmount = resultIllnessTotalSharedAmount;
                }
            }

            LimitedSharingYearlyLimit -= IllnessTotalSharedAmount;

            txtYearlyLimitBalance.Text = LimitedSharingYearlyLimit.ToString("C");
        }

        private void dtpDateOfDiagnosis_ValueChanged(object sender, EventArgs e)
        {
            dtpDateOfDiagnosis.Format = DateTimePickerFormat.Short;

            List<IllnessProgramHistory> lstIllnessProgramHistory = new List<IllnessProgramHistory>();

            String strSqlQueryForMemberProgramHistory = "select [dbo].[ContactHistory].[CreatedDate], [dbo].[ContactHistory].[OldValue], [dbo].[ContactHistory].[NewValue] " +
                                                        "from [dbo].[ContactHistory] " +
                                                        "inner join [dbo].[Contact] on [dbo].[ContactHistory].[ContactId] = [dbo].[Contact].[Id] " +
                                                        "where [dbo].[Contact].[Individual_ID__c] = @IndividualId and " +
                                                        "cast([dbo].[ContactHistory].[Field] as nvarchar(max)) = 'c4g_Plan__c' " +
                                                        "order by [dbo].[ContactHistory].[CreatedDate]";

            SqlCommand cmdQueryForMemberProgramHistory = new SqlCommand(strSqlQueryForMemberProgramHistory, connSalesforce);
            cmdQueryForMemberProgramHistory.CommandType = CommandType.Text;

            cmdQueryForMemberProgramHistory.Parameters.AddWithValue("@IndividualId", strIndividualNo);

            if (connSalesforce.State != ConnectionState.Closed)
            {
                connSalesforce.Close();
                connSalesforce.Open();
            }
            else if (connSalesforce.State == ConnectionState.Closed) connSalesforce.Open();
            SqlDataReader rdrIllnessProgramHistory = cmdQueryForMemberProgramHistory.ExecuteReader();
            if (rdrIllnessProgramHistory.HasRows)
            {
                while (rdrIllnessProgramHistory.Read())
                {
                    IllnessProgramHistory program = new IllnessProgramHistory();
                    if (!rdrIllnessProgramHistory.IsDBNull(0)) program.CreateDate = rdrIllnessProgramHistory.GetDateTime(0);
                    if (!rdrIllnessProgramHistory.IsDBNull(1))
                    {
                        switch (rdrIllnessProgramHistory.GetString(1).Trim())
                        {
                            case "Gold Plus":
                                program.OldProgram = IllnessProgram.GoldPlus;
                                break;
                            case "Gold":
                                program.OldProgram = IllnessProgram.Gold;
                                break;
                            case "Silver":
                                program.OldProgram = IllnessProgram.Silver;
                                break;
                            case "Bronze":
                                program.OldProgram = IllnessProgram.Bronze;
                                break;
                            case "Gold Medi-I":
                                program.OldProgram = IllnessProgram.GoldMedi_I;
                                break;
                            case "Gold Medi-II":
                                program.OldProgram = IllnessProgram.GoldMedi_II;
                                break;
                            default:
                                program.OldProgram = null;
                                break;
                        }
                    }
                    if (!rdrIllnessProgramHistory.IsDBNull(2))
                    {
                        switch (rdrIllnessProgramHistory.GetString(2).Trim())
                        {
                            case "Gold Plus":
                                program.NewProgram = IllnessProgram.GoldPlus;
                                break;
                            case "Gold":
                                program.NewProgram = IllnessProgram.Gold;
                                break;
                            case "Silver":
                                program.NewProgram = IllnessProgram.Silver;
                                break;
                            case "Bronze":
                                program.NewProgram = IllnessProgram.Bronze;
                                break;
                            case "Gold Medi-I":
                                program.NewProgram = IllnessProgram.GoldMedi_I;
                                break;
                            case "Gold Medi-II":
                                program.NewProgram = IllnessProgram.GoldMedi_II;
                                break;
                            default:
                                program.NewProgram = null;
                                break;
                        }
                    }

                    program.IndividualId = strIndividualNo;

                    lstIllnessProgramHistory.Add(program);
                }
            }
            rdrIllnessProgramHistory.Close();
            if (connSalesforce.State != ConnectionState.Closed) connSalesforce.Close();

            for (int i = 0; i < lstIllnessProgramHistory.Count; i++)
            {
                if (lstIllnessProgramHistory[i].OldProgram == null && lstIllnessProgramHistory[i].NewProgram == null)
                {
                    lstIllnessProgramHistory.RemoveAt(i);
                    i--;
                }
            }
            
            if (lstIllnessProgramHistory.Count > 1)
            {
                for (int i = 0; i < lstIllnessProgramHistory.Count - 1; i++)
                {
                    if (lstIllnessProgramHistory[i].CreateDate.Value == lstIllnessProgramHistory[i + 1].CreateDate.Value)
                    {
                        lstIllnessProgramHistory.RemoveAt(i);
                        i--;
                    }
                }

                for (int i = 0; i < lstIllnessProgramHistory.Count - 1; i++)
                {
                    if (lstIllnessProgramHistory[i].CreateDate.Value.Year == lstIllnessProgramHistory[i + 1].CreateDate.Value.Year &&
                        lstIllnessProgramHistory[i].CreateDate.Value.Month == lstIllnessProgramHistory[i + 1].CreateDate.Value.Month)
                    {
                        lstIllnessProgramHistory.RemoveAt(i);
                        i--;
                    }
                }
            }
            
            List<IllnessProgramHistory> lstIllnessProgramHistoryAnnivDate = new List<IllnessProgramHistory>();

            foreach (IllnessProgramHistory program in lstIllnessProgramHistory)
            {
                IllnessProgramHistory program_history = new IllnessProgramHistory();
                program_history.CreateDate = new DateTime(program.CreateDate.Value.Year, program.CreateDate.Value.Month, 1);
                program_history.IndividualId = program.IndividualId;
                program_history.OldProgram = program.OldProgram;
                program_history.NewProgram = program.NewProgram;

                lstIllnessProgramHistoryAnnivDate.Add(program_history);
            }

            DateTime DiagnosisDate = dtpDateOfDiagnosis.Value;

            if (lstIllnessProgramHistoryAnnivDate.Count > 0)
            {
                if (DiagnosisDate >= lstIllnessProgramHistoryAnnivDate[lstIllnessProgramHistoryAnnivDate.Count - 1].CreateDate)
                {
                    SetIllnessProgram(lstIllnessProgramHistoryAnnivDate[lstIllnessProgramHistoryAnnivDate.Count - 1].NewProgram.Value);
                }
                else if (lstIllnessProgramHistoryAnnivDate[0].CreateDate.Value <= DiagnosisDate)
                {
                    for (int i = 0; i < lstIllnessProgramHistoryAnnivDate.Count - 1; i++)
                    {
                        if (DiagnosisDate >= lstIllnessProgramHistoryAnnivDate[i].CreateDate.Value && 
                            DiagnosisDate < lstIllnessProgramHistoryAnnivDate[i + 1].CreateDate.Value)
                        {
                            SetIllnessProgram(lstIllnessProgramHistoryAnnivDate[i].NewProgram.Value);
                        }
                    }
                }
                else if (lstIllnessProgramHistoryAnnivDate[0].CreateDate.Value > DiagnosisDate)
                {
                    SetIllnessProgram(lstIllnessProgramHistoryAnnivDate[0].OldProgram.Value);
                }
            }

            //dtpDateOfDiagnosis.CustomFormat = null;
        }

        private void SetIllnessProgram(IllnessProgram program)
        {
            switch (program)
            {
                case IllnessProgram.GoldPlus:
                    comboIllnessProgram.SelectedIndex = 0;
                    break;
                case IllnessProgram.Gold:
                    comboIllnessProgram.SelectedIndex = 1;
                    break;
                case IllnessProgram.Silver:
                    comboIllnessProgram.SelectedIndex = 2;
                    break;
                case IllnessProgram.Bronze:
                    comboIllnessProgram.SelectedIndex = 3;
                    break;
                case IllnessProgram.GoldMedi_I:
                    comboIllnessProgram.SelectedIndex = 4;
                    break;
                case IllnessProgram.GoldMedi_II:
                    comboIllnessProgram.SelectedIndex = 5;
                    break;
            }
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
        //public String Id;
        public int? Id;
        public String Name;
        public String ICD10Code;

        public ICD10CodeInfo()
        {
            //Id = String.Empty;
            Id = null;
            Name = String.Empty;
            ICD10Code = String.Empty;
        }

        public ICD10CodeInfo(int id, String strName, String strICD10Code)
        {
            Id = id;
            Name = strName;
            ICD10Code = strICD10Code;
        }
    }
}
