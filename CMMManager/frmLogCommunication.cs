﻿using System;
using System.IO;
using System.Diagnostics;
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
    public partial class frmLogCommunication : Form
    {
        public String IndividualId;
        private int nLoggedInUserId;
        //private int nLoggedInUserRole;
        private String CommunicationNo;
        private String CaseNo;
        private String IllnessNo;
        private String IncidentNo;
        private CommunicationType CommType;
        private CommunicationOpenMode OpenMode;

        private String strConnString;
        private String strConnString2;
        private SqlConnection connRN;
        private SqlConnection connRN2;

        public String Subject = String.Empty;
        public String Body = String.Empty;
        public String Solution = String.Empty;
        public String CreatedByStaffName = String.Empty;
        public String CreatedDate = String.Empty;

        private const String CommunicationAttachmentDestinationPath = @"\\cmm-2019data\Sharefolder\CommunicationAttachments\";

        public frmLogCommunication()
        {
            IndividualId = String.Empty;
            strConnString = @"Data Source=cmm-2019data\CMM; Initial Catalog=RN_DB;Integrated Security=True; Max Pool Size=200; MultipleActiveResultSets=True; ";
            strConnString2 = @"Data Source=cmm-2019data\CMM; Initial Catalog=RN_DB;Integrated Security=True; Max Pool Size=200; MultipleActiveResultSets=True; ";
            //strConnString = @"Data Source=cmm-2019data\CMM; Initial Catalog=RN_DB;User ID=sa;Password=Yny00516; Max Pool Size=200; MultipleActiveResultSets=True";
            connRN = new SqlConnection(strConnString);
            connRN2 = new SqlConnection(strConnString2);

            InitializeComponent();
        }

        public frmLogCommunication(String individual_id, int login_user_id, CommunicationOpenMode mode)
        {
            InitializeComponent();

            IndividualId = individual_id;
            nLoggedInUserId = login_user_id;

            strConnString = @"Data Source=cmm-2019data\CMM; Initial Catalog=RN_DB; Integrated Security = True; Max Pool Size = 200";
            strConnString2 = @"Data Source=cmm-2019data\CMM; Initial Catalog=RN_DB;Integrated Security=True; Max Pool Size=200; MultipleActiveResultSets=True; ";

            //strConnString = @"Data Source=cmm-2019data\CMM; Initial Catalog=RN_DB; User ID=sa;Password=Yny00516; Max Pool Size = 200";
            connRN = new SqlConnection(strConnString);
            connRN2 = new SqlConnection(strConnString2);

            OpenMode = mode;
        }

        public frmLogCommunication(String individual_id, int login_user_id, String communication_no, CommunicationType type, String case_no, String illness_no, String incident_no, String subject, String body, String solution, String create_staff, String created_date, CommunicationOpenMode mode)
        {
            InitializeComponent();
            IndividualId = individual_id;
            nLoggedInUserId = login_user_id;

            CommunicationNo = communication_no;
            CaseNo = case_no;
            IllnessNo = illness_no;
            IncidentNo = incident_no;

            CommType = type;

            Subject = subject;
            Body = body;
            Solution = solution;

            CreatedByStaffName = create_staff;
            CreatedDate = created_date;

            OpenMode = mode;

            strConnString = @"Data Source=cmm-2019data\CMM; Initial Catalog=RN_DB; Integrated Security = True; Max Pool Size = 200";
            strConnString2 = @"Data Source=cmm-2019data\CMM; Initial Catalog=RN_DB;Integrated Security=True; Max Pool Size=200; MultipleActiveResultSets=True; ";

            //strConnString = @"Data Source=cmm-2019data\CMM; Initial Catalog=RN_DB; User ID=sa;Password=Yny00516; Max Pool Size = 200";
            connRN = new SqlConnection(strConnString);
            connRN2 = new SqlConnection(strConnString2);

        }

        private void frmLogCommunication_Load(object sender, EventArgs e)
        {
            String strIndividualId = IndividualId;
            txtCommunicationIndividualId.Text = strIndividualId;

            if (OpenMode == CommunicationOpenMode.AddNew)
            {
                if (Subject != String.Empty) txtCommunicationSubject.Text = Subject;
                if (Body != String.Empty) txtCommunicationBody.Text = Body;

                String strSqlQueryForLastCommunicationNo = "select [dbo].[tbl_LastID].[CommunicationNo] from [dbo].[tbl_LastID] where [dbo].[tbl_LastID].[Id] = 1";

                SqlCommand cmdQueryForLastCommunicationNo = new SqlCommand(strSqlQueryForLastCommunicationNo, connRN);
                cmdQueryForLastCommunicationNo.CommandType = CommandType.Text;

                if (connRN.State != ConnectionState.Closed)
                {
                    connRN.Close();
                    connRN.Open();
                }
                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                Object objLastCommunicationNo = cmdQueryForLastCommunicationNo.ExecuteScalar();
                if (connRN.State != ConnectionState.Closed) connRN.Close();

                String strLastCommunicationNo = String.Empty;
                if (objLastCommunicationNo != null) strLastCommunicationNo = objLastCommunicationNo.ToString();

                String strNewCommunicationNo = String.Empty;
                if (strLastCommunicationNo != String.Empty)
                {
                    String strNewCommunicationNoNumber = strLastCommunicationNo.Substring(5);
                    int nNewCommunicatoinNoNumber = Int32.Parse(strNewCommunicationNoNumber);
                    nNewCommunicatoinNoNumber++;

                    strNewCommunicationNo = "Comm-" + nNewCommunicatoinNoNumber.ToString();
                }

                String strSqlUpdateLastCommunicationNo = "update [dbo].[tbl_LastID] set [dbo].[tbl_LastID].[CommunicationNo] = @NewCommunicationNo";

                SqlCommand cmdUpdateLastCommunicationNo = new SqlCommand(strSqlUpdateLastCommunicationNo, connRN);
                cmdUpdateLastCommunicationNo.CommandType = CommandType.Text;

                cmdUpdateLastCommunicationNo.Parameters.AddWithValue("@NewCommunicationNo", strNewCommunicationNo);

                if (connRN.State != ConnectionState.Closed)
                {
                    connRN.Close();
                    connRN.Open();
                }
                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                int nCommunicationNoUpdated = cmdUpdateLastCommunicationNo.ExecuteNonQuery();
                if (connRN.State != ConnectionState.Closed) connRN.Close();

                txtCommunicationNo.Text = strNewCommunicationNo;

                String strSqlQueryForCasesForIndividual = "select [dbo].[tbl_case].[Case_Name] from [dbo].[tbl_case] " +
                                                          "where [dbo].[tbl_case].[individual_id] = @IndividualId and " +
                                                          "([dbo].[tbl_case].[IsDeleted] = 0 or [dbo].[tbl_case].[IsDeleted] IS NULL) " +
                                                          "order by [dbo].[tbl_case].[Case_Name] desc";

                SqlCommand cmdQueryForCasesForIndividual = new SqlCommand(strSqlQueryForCasesForIndividual, connRN);
                cmdQueryForCasesForIndividual.CommandType = CommandType.Text;

                cmdQueryForCasesForIndividual.Parameters.AddWithValue("@IndividualId", strIndividualId);

                if (connRN.State != ConnectionState.Closed)
                {
                    connRN.Close();
                    connRN.Open();
                }
                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                SqlDataReader rdrCasesForIndividual = cmdQueryForCasesForIndividual.ExecuteReader();
                comboCaseNo.Items.Add("None");
                if (rdrCasesForIndividual.HasRows)
                {
                    while (rdrCasesForIndividual.Read())
                    {
                        if (!rdrCasesForIndividual.IsDBNull(0)) comboCaseNo.Items.Add(rdrCasesForIndividual.GetString(0));
                    }
                }
                rdrCasesForIndividual.Close();
                if (connRN.State != ConnectionState.Closed) connRN.Close();
                comboCaseNo.SelectedIndex = 0;

                comboIllnessNo.Items.Clear();

                String strSqlQueryForIllnessForIndividual = "select [dbo].[tbl_illness].[IllnessNo] from [dbo].[tbl_illness] where [dbo].[tbl_illness].[Individual_Id] = @IndividualId " +
                                                            "order by [dbo].[tbl_illness].[IllnessNo] desc";

                SqlCommand cmdQueryForIllnessForIndividual = new SqlCommand(strSqlQueryForIllnessForIndividual, connRN);
                cmdQueryForIllnessForIndividual.CommandType = CommandType.Text;

                cmdQueryForIllnessForIndividual.Parameters.AddWithValue("@IndividualId", strIndividualId);

                if (connRN.State != ConnectionState.Closed)
                {
                    connRN.Close();
                    connRN.Open();
                }
                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                SqlDataReader rdrIllnessForIndividual = cmdQueryForIllnessForIndividual.ExecuteReader();
                comboIllnessNo.Items.Add("None");
                if (rdrIllnessForIndividual.HasRows)
                {
                    while (rdrIllnessForIndividual.Read())
                    {
                        if (!rdrIllnessForIndividual.IsDBNull(0)) comboIllnessNo.Items.Add(rdrIllnessForIndividual.GetString(0));
                    }
                }
                rdrIllnessForIndividual.Close();
                if (connRN.State != ConnectionState.Closed) connRN.Close();
                comboIllnessNo.SelectedIndex = 0;

                comboIncidentNo.Items.Clear();

                String strSqlQueryForIncidentForIndividual = "select [dbo].[tbl_incident].[IncidentNo] from [dbo].[tbl_incident] where [dbo].[tbl_incident].[Individual_id] = @IndividualId " +
                                                             "order by [dbo].[tbl_incident].[IncidentNo] desc";

                SqlCommand cmdQueryForIncidentForIndividual = new SqlCommand(strSqlQueryForIncidentForIndividual, connRN);
                cmdQueryForIncidentForIndividual.CommandType = CommandType.Text;

                cmdQueryForIncidentForIndividual.Parameters.AddWithValue("@IndividualId", strIndividualId);

                if (connRN.State != ConnectionState.Closed)
                {
                    connRN.Close();
                    connRN.Open();
                }
                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                SqlDataReader rdrIncidentForIndividual = cmdQueryForIncidentForIndividual.ExecuteReader();
                comboIncidentNo.Items.Add("None");
                if (rdrIncidentForIndividual.HasRows)
                {
                    while (rdrIncidentForIndividual.Read())
                    {
                        if (!rdrIncidentForIndividual.IsDBNull(0)) comboIncidentNo.Items.Add(rdrIncidentForIndividual.GetString(0));
                    }
                }
                rdrIncidentForIndividual.Close();
                if (connRN.State != ConnectionState.Closed) connRN.Close();

                comboIncidentNo.SelectedIndex = 0;

                List<CommunicationTypeInfo> lstCommunicationTypeInfo = new List<CommunicationTypeInfo>();

                String strSqlQueryForCommunicationTypes = "select [dbo].[tbl_communication_type_code].[CommunicationTypeId], [dbo].[tbl_communication_type_code].[CommunicationTypeValue] " +
                                                          "from [dbo].[tbl_communication_type_code]";

                SqlCommand cmdQueryForCommunicationsTypes = new SqlCommand(strSqlQueryForCommunicationTypes, connRN);
                cmdQueryForCommunicationsTypes.CommandType = CommandType.Text;

                if (connRN.State != ConnectionState.Closed)
                {
                    connRN.Close();
                    connRN.Open();
                }
                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                SqlDataReader rdrCommunicationTypes = cmdQueryForCommunicationsTypes.ExecuteReader();
                if (rdrCommunicationTypes.HasRows)
                {
                    while (rdrCommunicationTypes.Read())
                    {
                        CommunicationTypeInfo info = new CommunicationTypeInfo();

                        if (!rdrCommunicationTypes.IsDBNull(0)) info.nCommunicationTypeId = rdrCommunicationTypes.GetByte(0);
                        else info.nCommunicationTypeId = null;
                        if (!rdrCommunicationTypes.IsDBNull(1)) info.CommunicationTypeValue = rdrCommunicationTypes.GetString(1);
                        else info.CommunicationTypeValue = null;

                        lstCommunicationTypeInfo.Add(info);
                    }
                }
                rdrCommunicationTypes.Close();
                if (connRN.State != ConnectionState.Closed) connRN.Close();

                if (lstCommunicationTypeInfo.Count > 0)
                {
                    for (int i = 0; i < lstCommunicationTypeInfo.Count; i++)
                    {
                        if (lstCommunicationTypeInfo[i].nCommunicationTypeId != 12)
                        {
                            comboCommunicationType.Items.Add(lstCommunicationTypeInfo[i].CommunicationTypeValue);
                            lstCommunicationTypeInfo[i].nSelectedIndex = i;
                        }
                    }
                }

                //String strSqlQueryForCommunicationTypes = "select [dbo].[tbl_communication_type_code].[CommunicationTypeValue] from [dbo].[tbl_communication_type_code]";

                //SqlCommand cmdQueryForCommunicationsTypes = new SqlCommand(strSqlQueryForCommunicationTypes, connRN);
                //cmdQueryForCommunicationsTypes.CommandType = CommandType.Text;

                //if (connRN.State != ConnectionState.Closed)
                //{
                //    connRN.Close();
                //    connRN.Open();
                //}
                //else if (connRN.State == ConnectionState.Closed) connRN.Open();
                //SqlDataReader rdrCommunicationTypes = cmdQueryForCommunicationsTypes.ExecuteReader();
                //if (rdrCommunicationTypes.HasRows)
                //{
                //    while (rdrCommunicationTypes.Read())
                //    {
                //        if (!rdrCommunicationTypes.IsDBNull(0)) comboCommunicationType.Items.Add(rdrCommunicationTypes.GetString(0));
                //    }
                //}
                //rdrCommunicationTypes.Close();
                //if (connRN.State != ConnectionState.Closed) connRN.Close();
            }
            else if (OpenMode == CommunicationOpenMode.ReadOnly)
            {
                String strSqlQueryForCasesForIndividual = "select [dbo].[tbl_case].[Case_Name] from [dbo].[tbl_case] where [dbo].[tbl_case].[individual_id] = @IndividualId " +
                                          "order by [dbo].[tbl_case].[Case_Name] desc";

                SqlCommand cmdQueryForCasesForIndividual = new SqlCommand(strSqlQueryForCasesForIndividual, connRN);
                cmdQueryForCasesForIndividual.CommandType = CommandType.Text;

                cmdQueryForCasesForIndividual.Parameters.AddWithValue("@IndividualId", strIndividualId);

                if (connRN.State != ConnectionState.Closed)
                {
                    connRN.Close();
                    connRN.Open();
                }
                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                SqlDataReader rdrCasesForIndividual = cmdQueryForCasesForIndividual.ExecuteReader();
                //comboCaseNo.Items.Add(String.Empty);
                comboCaseNo.Items.Add("None");
                if (rdrCasesForIndividual.HasRows)
                {
                    while (rdrCasesForIndividual.Read())
                    {
                        if (!rdrCasesForIndividual.IsDBNull(0)) comboCaseNo.Items.Add(rdrCasesForIndividual.GetString(0));
                    }
                }
                rdrCasesForIndividual.Close();
                if (connRN.State != ConnectionState.Closed) connRN.Close();

                List<CommunicationTypeInfo> lstCommunicationTypeInfo = new List<CommunicationTypeInfo>();

                String strSqlQueryForCommunicationTypes = "select [dbo].[tbl_communication_type_code].[CommunicationTypeId], [dbo].[tbl_communication_type_code].[CommunicationTypeValue] " +
                                                          "from [dbo].[tbl_communication_type_code]";

                SqlCommand cmdQueryForCommunicationsTypes = new SqlCommand(strSqlQueryForCommunicationTypes, connRN);
                cmdQueryForCommunicationsTypes.CommandType = CommandType.Text;

                if (connRN.State != ConnectionState.Closed)
                {
                    connRN.Close();
                    connRN.Open();
                }
                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                SqlDataReader rdrCommunicationTypes = cmdQueryForCommunicationsTypes.ExecuteReader();
                if (rdrCommunicationTypes.HasRows)
                {
                    while (rdrCommunicationTypes.Read())
                    {
                        CommunicationTypeInfo info = new CommunicationTypeInfo();

                        if (!rdrCommunicationTypes.IsDBNull(0)) info.nCommunicationTypeId = rdrCommunicationTypes.GetByte(0);
                        else info.nCommunicationTypeId = null;
                        if (!rdrCommunicationTypes.IsDBNull(1)) info.CommunicationTypeValue = rdrCommunicationTypes.GetString(1);
                        else info.CommunicationTypeValue = null;

                        lstCommunicationTypeInfo.Add(info);
                    }
                }
                rdrCommunicationTypes.Close();
                if (connRN.State != ConnectionState.Closed) connRN.Close();

                if (lstCommunicationTypeInfo.Count > 0)
                {
                    for (int i = 0; i < lstCommunicationTypeInfo.Count; i++)
                    {
                        if (lstCommunicationTypeInfo[i].nCommunicationTypeId != 12)
                        {
                            comboCommunicationType.Items.Add(lstCommunicationTypeInfo[i].CommunicationTypeValue);
                            lstCommunicationTypeInfo[i].nSelectedIndex = i;
                        }
                    }
                }

                // 12-16-2019 begin here
                string strSqlQueryForCommunicationInfo = "select [dbo].[tbl_Communication].[Individual_Id], " +
                                                         "[dbo].[tbl_Communication].[CommunicationNo], [dbo].[tbl_Communication].[CommunicationType], " +
                                                         "[dbo].[tbl_Communication].[CaseNo], [dbo].[tbl_Communication].[IllnessNo], [dbo].[tbl_Communication].[IncidentNo], " +
                                                         "[dbo].[tbl_Communication].[Subject], [dbo].[tbl_Communication].[Body], [dbo].[tbl_Communication].[Solution], " +
                                                         "[dbo].[tbl_Communication].[CreateDate], [dbo].[tbl_CreateStaff].[Staff_Name], " +
                                                         "[dbo].[tbl_Communication].[ModifiDate], modifi_staff.[Staff_Name], " +
                                                         "[dbo].[tbl_Communication].[CommunicationSolvedDate], solved_staff.[Staff_Name], " +
                                                         "[dbo].[tbl_Communication].[IsComplete] " +
                                                         "from [dbo].[tbl_Communication] " +
                                                         "inner join [dbo].[tbl_CreateStaff] on [dbo].[tbl_Communication].[CreatedBy] = [dbo].[tbl_CreateStaff].[CreateStaff_Id] " +
                                                         "left join [dbo].[tbl_ModifiStaff] modifi_staff on [dbo].[tbl_Communication].[ModifiedBy] = modifi_staff.[ModifiStaff_Id] " +
                                                         "left join [dbo].[tbl_ModifiStaff] solved_staff on [dbo].[tbl_Communication].[CommunicationSolvedBy] = solved_staff.[ModifiStaff_Id] " +
                                                         "where [dbo].[tbl_Communication].[CommunicationNo] = @CommunicationNo";

                SqlCommand cmdQueryForCommunicationInfo = new SqlCommand(strSqlQueryForCommunicationInfo, connRN2);
                cmdQueryForCommunicationInfo.CommandType = CommandType.Text;
                

                cmdQueryForCommunicationInfo.Parameters.AddWithValue("@CommunicationNo", CommunicationNo);

                if (connRN2.State != ConnectionState.Closed)
                {
                    connRN2.Close();
                    connRN2.Open();
                }
                else if (connRN2.State == ConnectionState.Closed) connRN2.Open();
                SqlDataReader rdrCommunicationInfo = cmdQueryForCommunicationInfo.ExecuteReader();
                if (rdrCommunicationInfo.HasRows)
                {
                    if (rdrCommunicationInfo.Read())
                    {
                        if (!rdrCommunicationInfo.IsDBNull(0)) txtCommunicationIndividualId.Text = rdrCommunicationInfo.GetString(0);
                        else txtCommunicationIndividualId.Text = String.Empty;
                        if (!rdrCommunicationInfo.IsDBNull(1)) txtCommunicationNo.Text = rdrCommunicationInfo.GetString(1);
                        else txtCommunicationNo.Text = String.Empty;
                        if (!rdrCommunicationInfo.IsDBNull(2))
                        {
                            int CommunicationType = rdrCommunicationInfo.GetByte(2);
                            if (CommunicationType < 4) comboCommunicationType.SelectedIndex = CommunicationType;
                            else if (CommunicationType < 12) comboCommunicationType.SelectedIndex = CommunicationType - 2;
                            else if (CommunicationType >= 17) comboCommunicationType.SelectedIndex = CommunicationType - 6;
                        }
                        if (!rdrCommunicationInfo.IsDBNull(3))
                        {
                            String strCaseNo = rdrCommunicationInfo.GetString(3);
                            for (int i = 0; i < comboCaseNo.Items.Count; i++)
                            {
                                if (strCaseNo == comboCaseNo.Items[i].ToString()) comboCaseNo.SelectedIndex = i;
                            }
                        }
                        else comboCaseNo.SelectedIndex = 0;

                        if (!rdrCommunicationInfo.IsDBNull(4))
                        {
                            String strIllnessNo = rdrCommunicationInfo.GetString(4);
                            comboIllnessNo.Items.Add(strIllnessNo);
                            comboIllnessNo.SelectedIndex = 0;
                            comboIllnessNo.Enabled = false;
                        }
                        else comboIllnessNo.SelectedIndex = -1;
                        if (!rdrCommunicationInfo.IsDBNull(5))
                        {
                            String strIncidentNo = rdrCommunicationInfo.GetString(5);
                            comboIncidentNo.Items.Add(strIncidentNo);
                            comboIncidentNo.SelectedIndex = 0;
                            comboIncidentNo.Enabled = false;
                        }
                        else comboIncidentNo.SelectedIndex = -1;
                        if (!rdrCommunicationInfo.IsDBNull(6)) txtCommunicationSubject.Text = rdrCommunicationInfo.GetString(6);
                        else txtCommunicationSubject.Text = String.Empty;
                        if (!rdrCommunicationInfo.IsDBNull(7)) txtCommunicationBody.Text = rdrCommunicationInfo.GetString(7);
                        else txtCommunicationBody.Text = String.Empty;
                        if (!rdrCommunicationInfo.IsDBNull(8)) txtCommunicationSolution.Text = rdrCommunicationInfo.GetString(8);
                        else txtCommunicationSolution.Text = String.Empty;
                        if (!rdrCommunicationInfo.IsDBNull(9)) txtCreatedDate.Text = rdrCommunicationInfo.GetDateTime(9).ToString("MM/dd/yyyy");
                        else txtCreatedDate.Text = String.Empty;
                        if (!rdrCommunicationInfo.IsDBNull(10)) txtCreatedByName.Text = rdrCommunicationInfo.GetString(10);
                        else txtCreatedByName.Text = String.Empty;
                        if (!rdrCommunicationInfo.IsDBNull(13)) txtCommunicationSolvedDate.Text = rdrCommunicationInfo.GetDateTime(13).ToString("MM/dd/yyyy");
                        else txtCommunicationSolvedDate.Text = String.Empty;
                        if (!rdrCommunicationInfo.IsDBNull(14)) txtCommunicationSolvedBy.Text = rdrCommunicationInfo.GetString(14);
                        else txtCommunicationSolvedBy.Text = String.Empty;
                        if (!rdrCommunicationInfo.IsDBNull(15)) chkCommunnicationComplete.Checked = rdrCommunicationInfo.GetBoolean(15);
                        else chkCommunnicationComplete.Checked = false;
                    }
                }
                rdrCommunicationInfo.Close();
                if (connRN2.State != ConnectionState.Closed) connRN2.Close();

                //String strSqlQueryForCommComplete = "select [dbo].[tbl_Communication].[IsComplete] from [dbo].[tbl_Communication] " +
                //                                    "where [dbo].[tbl_Communication].[CommunicationNo] = @CommunicationNo";

                //SqlCommand cmdQueryForCommComplete = new SqlCommand(strSqlQueryForCommComplete, connRN);
                //cmdQueryForCommComplete.CommandType = CommandType.Text;

                //cmdQueryForCommComplete.Parameters.AddWithValue("@CommunicationNo", CommunicationNo);

                //if (connRN.State != ConnectionState.Closed)
                //{
                //    connRN.Close();
                //    connRN.Open();
                //}
                //else if (connRN.State == ConnectionState.Closed) connRN.Open();
                //Object objCommComplete = cmdQueryForCommComplete.ExecuteScalar();
                //if (connRN.State != ConnectionState.Closed) connRN.Close();

                //if (objCommComplete != null)
                //{
                //    Boolean? IsComplete = null;
                //    Boolean resultComplete;
                //    if (Boolean.TryParse(objCommComplete.ToString(), out resultComplete)) IsComplete = resultComplete;

                //    if (IsComplete != null) chkCommunnicationComplete.Checked = IsComplete.Value;
                //}
                

                //txtCommunicationIndividualId.Text = IndividualId;
                //txtCommunicationIndividualId.ReadOnly = true;
                //txtCommunicationNo.Text = CommunicationNo;
                //txtCommunicationNo.ReadOnly = true;

                //for (int i = 0; i < comboCaseNo.Items.Count; i++)
                //{
                //    if (CaseNo == comboCaseNo.Items[i].ToString()) comboCaseNo.SelectedIndex = i;
                //}
                //comboCaseNo.Enabled = false;

                //comboIllnessNo.Items.Add(IllnessNo);
                //comboIllnessNo.SelectedIndex = 0;
                //comboIllnessNo.Enabled = false;

                //comboIncidentNo.Items.Add(IncidentNo);
                //comboIncidentNo.SelectedIndex = 0;
                //comboIncidentNo.Enabled = false;

                ////comboCommunicationType.SelectedIndex = (int)CommType;
                //if ((int)CommType < 4) comboCommunicationType.SelectedIndex = (int)CommType;
                //else if ((int)CommType < 12) comboCommunicationType.SelectedIndex = (int)CommType - 2;

                //txtCreatedByName.Text = CreatedByStaffName.Trim();
                //txtCreatedDate.Text = CreatedDate.Trim();

                //txtCommunicationSubject.Text = Subject;
                //txtCommunicationSubject.ReadOnly = true;
                //txtCommunicationBody.Text = Body;
                //txtCommunicationBody.ReadOnly = true;

                //txtCommunicationSolution.Text = Solution;
                //txtCommunicationSolution.ReadOnly = true;

                String strSqlQueryForAttachments = "select [dbo].[tbl_CommunicationAttachments].[AttachmentNo], [dbo].[tbl_CommunicationAttachments].[AttachedFileName], " +
                                                   "[dbo].[tbl_CreateStaff].[Staff_Name], [dbo].[tbl_CommunicationAttachments].[CreateDate] " +
                                                   "from [dbo].[tbl_CommunicationAttachments] " +
                                                   "inner join [dbo].[tbl_CreateStaff] on [dbo].[tbl_CommunicationAttachments].[CreatedBy] = [dbo].[tbl_CreateStaff].[CreateStaff_Id] " +
                                                   "where [dbo].[tbl_CommunicationAttachments].[CommunicationNo] = @CommunicationNo and " +
                                                   "([dbo].[tbl_CommunicationAttachments].[IsDeleted] = 0 or [dbo].[tbl_CommunicationAttachments].[IsDeleted] IS NULL)";

                SqlCommand cmdQueryForAttachments = new SqlCommand(strSqlQueryForAttachments, connRN);
                cmdQueryForAttachments.CommandType = CommandType.Text;

                cmdQueryForAttachments.Parameters.AddWithValue("@CommunicationNo", CommunicationNo);
                if (connRN.State != ConnectionState.Closed)
                {
                    connRN.Close();
                    connRN.Open();
                }
                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                SqlDataReader rdrAttachments = cmdQueryForAttachments.ExecuteReader();
                if (rdrAttachments.HasRows)
                {
                    while (rdrAttachments.Read())
                    {
                        DataGridViewRow row = new DataGridViewRow();

                        row.Cells.Add(new DataGridViewCheckBoxCell { Value = false });
                        row.Cells[0].ReadOnly = true;
                        if (!rdrAttachments.IsDBNull(0)) row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrAttachments.GetString(0) });
                        else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                        row.Cells[1].ReadOnly = true;
                        row.Cells.Add(new DataGridViewButtonCell { Value = "Attach" });
                        row.Cells[2].ReadOnly = true;
                        if (!rdrAttachments.IsDBNull(1)) row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrAttachments.GetString(1) });
                        else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                        row.Cells[3].ReadOnly = true;
                        row.Cells.Add(new DataGridViewButtonCell { Value = "View" });
                        if (!rdrAttachments.IsDBNull(2)) row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrAttachments.GetString(2) });
                        else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                        row.Cells[4].ReadOnly = true;
                        if (!rdrAttachments.IsDBNull(3)) row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrAttachments.GetDateTime(3).ToString("MM/dd/yyyy HH:mm:ss") });
                        else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                        row.Cells[6].ReadOnly = true;

                        gvCommunicationAttachment.Rows.Add(row);
                    }
                }
                rdrAttachments.Close();
                if (connRN.State != ConnectionState.Closed) connRN.Close();

                //MakeCommunicationFormReadOnly();
                //MakeCommunicationSolutionUpdatable();

            }
            else if (OpenMode == CommunicationOpenMode.Update)
            {
                //comboCaseNo.Items.Add("None");
                String strSqlQueryForCasesForIndividual = "select [dbo].[tbl_case].[Case_Name] from [dbo].[tbl_case] where [dbo].[tbl_case].[individual_id] = @IndividualId " +
                          "order by [dbo].[tbl_case].[Case_Name] desc";

                SqlCommand cmdQueryForCasesForIndividual = new SqlCommand(strSqlQueryForCasesForIndividual, connRN);
                cmdQueryForCasesForIndividual.CommandType = CommandType.Text;

                cmdQueryForCasesForIndividual.Parameters.AddWithValue("@IndividualId", strIndividualId);

                if (connRN.State != ConnectionState.Closed)
                {
                    connRN.Close();
                    connRN.Open();
                }
                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                SqlDataReader rdrCasesForIndividual = cmdQueryForCasesForIndividual.ExecuteReader();
                comboCaseNo.Items.Add(String.Empty);
                if (rdrCasesForIndividual.HasRows)
                {
                    while (rdrCasesForIndividual.Read())
                    {
                        if (!rdrCasesForIndividual.IsDBNull(0)) comboCaseNo.Items.Add(rdrCasesForIndividual.GetString(0));
                    }
                }
                rdrCasesForIndividual.Close();
                if (connRN.State != ConnectionState.Closed) connRN.Close();

                String strSqlQueryForCommunicationTypes = "select [dbo].[tbl_communication_type_code].[CommunicationTypeValue] from [dbo].[tbl_communication_type_code]";

                SqlCommand cmdQueryForCommunicationsTypes = new SqlCommand(strSqlQueryForCommunicationTypes, connRN);
                cmdQueryForCommunicationsTypes.CommandType = CommandType.Text;

                if (connRN.State != ConnectionState.Closed)
                {
                    connRN.Close();
                    connRN.Open();
                }
                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                SqlDataReader rdrCommunicationTypes = cmdQueryForCommunicationsTypes.ExecuteReader();
                if (rdrCommunicationTypes.HasRows)
                {
                    while (rdrCommunicationTypes.Read())
                    {
                        if (!rdrCommunicationTypes.IsDBNull(0)) comboCommunicationType.Items.Add(rdrCommunicationTypes.GetString(0));
                    }
                }
                rdrCommunicationTypes.Close();
                if (connRN.State != ConnectionState.Closed) connRN.Close();

                string strSqlQueryForCommunicationInfo = "select [dbo].[tbl_Communication].[Individual_Id], " +
                                         "[dbo].[tbl_Communication].[CommunicationNo], [dbo].[tbl_Communication].[CommunicationType], " +
                                         "[dbo].[tbl_Communication].[CaseNo], [dbo].[tbl_Communication].[IllnessNo], [dbo].[tbl_Communication].[IncidentNo], " +
                                         "[dbo].[tbl_Communication].[Subject], [dbo].[tbl_Communication].[Body], [dbo].[tbl_Communication].[Solution], " +
                                         "[dbo].[tbl_Communication].[CreateDate], [dbo].[tbl_CreateStaff].[Staff_Name], " +
                                         "[dbo].[tbl_Communication].[ModifiDate], modifi_staff.[Staff_Name], " +
                                         "[dbo].[tbl_Communication].[CommunicationSolvedDate], solved_staff.[Staff_Name], " +
                                         "[dbo].[tbl_Communication].[IsComplete] " +
                                         "from [dbo].[tbl_Communication] " +
                                         "inner join [dbo].[tbl_CreateStaff] on [dbo].[tbl_Communication].[CreatedBy] = [dbo].[tbl_CreateStaff].[CreateStaff_Id] " +
                                         "left join [dbo].[tbl_ModifiStaff] modifi_staff on [dbo].[tbl_Communication].[ModifiedBy] = modifi_staff.[ModifiStaff_Id] " +
                                         "left join [dbo].[tbl_ModifiStaff] solved_staff on [dbo].[tbl_Communication].[CommunicationSolvedBy] = solved_staff.[ModifiStaff_Id] " +
                                         "where [dbo].[tbl_Communication].[CommunicationNo] = @CommunicationNo";

                SqlCommand cmdQueryForCommunicationInfo = new SqlCommand(strSqlQueryForCommunicationInfo, connRN2);
                cmdQueryForCommunicationInfo.CommandType = CommandType.Text;


                cmdQueryForCommunicationInfo.Parameters.AddWithValue("@CommunicationNo", CommunicationNo);

                if (connRN2.State != ConnectionState.Closed)
                {
                    connRN2.Close();
                    connRN2.Open();
                }
                else if (connRN2.State == ConnectionState.Closed) connRN2.Open();
                SqlDataReader rdrCommunicationInfo = cmdQueryForCommunicationInfo.ExecuteReader();
                if (rdrCommunicationInfo.HasRows)
                {
                    if (rdrCommunicationInfo.Read())
                    {
                        if (!rdrCommunicationInfo.IsDBNull(0)) txtCommunicationIndividualId.Text = rdrCommunicationInfo.GetString(0);
                        else txtCommunicationIndividualId.Text = String.Empty;
                        if (!rdrCommunicationInfo.IsDBNull(1)) txtCommunicationNo.Text = rdrCommunicationInfo.GetString(1);
                        else txtCommunicationNo.Text = String.Empty;
                        if (!rdrCommunicationInfo.IsDBNull(2))
                        {
                            int CommunicationType = rdrCommunicationInfo.GetByte(2);
                            if (CommunicationType < 4) comboCommunicationType.SelectedIndex = CommunicationType;
                            else if (CommunicationType < 12) comboCommunicationType.SelectedIndex = CommunicationType - 2;
                            else if (CommunicationType >= 17) comboCommunicationType.SelectedIndex = CommunicationType - 6;
                        }
                        if (!rdrCommunicationInfo.IsDBNull(3))
                        {
                            String strCaseNo = rdrCommunicationInfo.GetString(3);
                            for (int i = 0; i < comboCaseNo.Items.Count; i++)
                            {
                                if (strCaseNo == comboCaseNo.Items[i].ToString()) comboCaseNo.SelectedIndex = i;
                            }
                        }
                        else comboCaseNo.SelectedIndex = -1;

                        if (!rdrCommunicationInfo.IsDBNull(4))
                        {
                            String strIllnessNo = rdrCommunicationInfo.GetString(4);
                            comboIllnessNo.Items.Add(strIllnessNo);
                            comboIllnessNo.SelectedIndex = 0;
                            comboIllnessNo.Enabled = false;
                        }
                        else comboIllnessNo.SelectedIndex = -1;
                        if (!rdrCommunicationInfo.IsDBNull(5))
                        {
                            String strIncidentNo = rdrCommunicationInfo.GetString(5);
                            comboIncidentNo.Items.Add(strIncidentNo);
                            comboIncidentNo.SelectedIndex = 0;
                            comboIncidentNo.Enabled = false;
                        }
                        else comboIncidentNo.SelectedIndex = -1;
                        if (!rdrCommunicationInfo.IsDBNull(6)) txtCommunicationSubject.Text = rdrCommunicationInfo.GetString(6);
                        else txtCommunicationSubject.Text = String.Empty;
                        if (!rdrCommunicationInfo.IsDBNull(7)) txtCommunicationBody.Text = rdrCommunicationInfo.GetString(7);
                        else txtCommunicationBody.Text = String.Empty;
                        if (!rdrCommunicationInfo.IsDBNull(8)) txtCommunicationSolution.Text = rdrCommunicationInfo.GetString(8);
                        else txtCommunicationSolution.Text = String.Empty;
                        if (!rdrCommunicationInfo.IsDBNull(9)) txtCreatedDate.Text = rdrCommunicationInfo.GetDateTime(9).ToString("MM/dd/yyyy");
                        else txtCreatedDate.Text = String.Empty;
                        if (!rdrCommunicationInfo.IsDBNull(10)) txtCreatedByName.Text = rdrCommunicationInfo.GetString(10);
                        else txtCreatedByName.Text = String.Empty;
                        if (!rdrCommunicationInfo.IsDBNull(13)) txtCommunicationSolvedDate.Text = rdrCommunicationInfo.GetDateTime(13).ToString("MM/dd/yyyy");
                        else txtCommunicationSolvedDate.Text = String.Empty;
                        if (!rdrCommunicationInfo.IsDBNull(14)) txtCommunicationSolvedBy.Text = rdrCommunicationInfo.GetString(14);
                        else txtCommunicationSolvedBy.Text = String.Empty;
                        if (!rdrCommunicationInfo.IsDBNull(15)) chkCommunnicationComplete.Checked = rdrCommunicationInfo.GetBoolean(15);
                        else chkCommunnicationComplete.Checked = false;
                    }
                }
                rdrCommunicationInfo.Close();
                if (connRN2.State != ConnectionState.Closed) connRN2.Close();

                
                //txtCommunicationIndividualId.Text = IndividualId;
                //txtCommunicationIndividualId.ReadOnly = true;
                //txtCommunicationNo.Text = CommunicationNo;
                //txtCommunicationNo.ReadOnly = true;
                //txtCommunicationSolution.Text = Solution;

                //for (int i = 0; i < comboCaseNo.Items.Count; i++)
                //{
                //    if (CaseNo == comboCaseNo.Items[i].ToString()) comboCaseNo.SelectedIndex = i;
                //}
                //comboCaseNo.Enabled = false;

                //comboIllnessNo.Items.Add(IllnessNo);
                //comboIllnessNo.SelectedIndex = 0;
                //comboIllnessNo.Enabled = false;

                //comboIncidentNo.Items.Add(IncidentNo);
                //comboIncidentNo.SelectedIndex = 0;
                //comboIncidentNo.Enabled = false;

                ////comboCommunicationType.SelectedIndex = (int)CommType;
                //if ((int)CommType < 4) comboCommunicationType.SelectedIndex = (int)CommType;
                //else comboCommunicationType.SelectedIndex = (int)CommType - 2;

                //txtCreatedByName.Text = CreatedByStaffName.Trim();
                //txtCreatedDate.Text = CreatedDate.Trim();

                //txtCommunicationSubject.Text = Subject;
                ////txtCommunicationSubject.ReadOnly = true;
                //txtCommunicationBody.Text = Body;
                ////txtCommunicationBody.ReadOnly = true;

                String strSqlQueryForAttachments = "select [dbo].[tbl_CommunicationAttachments].[AttachmentNo], [dbo].[tbl_CommunicationAttachments].[AttachedFileName], " +
                                                   "[dbo].[tbl_CreateStaff].[Staff_Name], [dbo].[tbl_CommunicationAttachments].[CreateDate] " +
                                                   "from [dbo].[tbl_CommunicationAttachments] " +
                                                   "inner join [dbo].[tbl_CreateStaff] on [dbo].[tbl_CommunicationAttachments].[CreatedBy] = [dbo].[tbl_CreateStaff].[CreateStaff_Id] " +
                                                   "where [dbo].[tbl_CommunicationAttachments].[CommunicationNo] = @CommunicationNo and " +
                                                   "([dbo].[tbl_CommunicationAttachments].[IsDeleted] = 0 or [dbo].[tbl_CommunicationAttachments].[IsDeleted] IS NULL)";

                SqlCommand cmdQueryForAttachments = new SqlCommand(strSqlQueryForAttachments, connRN);
                cmdQueryForAttachments.CommandType = CommandType.Text;

                cmdQueryForAttachments.Parameters.AddWithValue("@CommunicationNo", CommunicationNo);
                if (connRN.State != ConnectionState.Closed)
                {
                    connRN.Close();
                    connRN.Open();
                }
                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                SqlDataReader rdrAttachments = cmdQueryForAttachments.ExecuteReader();
                if (rdrAttachments.HasRows)
                {
                    while (rdrAttachments.Read())
                    {
                        DataGridViewRow row = new DataGridViewRow();

                        row.Cells.Add(new DataGridViewCheckBoxCell { Value = false });
                        if (!rdrAttachments.IsDBNull(0)) row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrAttachments.GetString(0) });
                        else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                        row.Cells[1].ReadOnly = true;
                        row.Cells.Add(new DataGridViewButtonCell { Value = "Attach" });
                        row.Cells[2].ReadOnly = true;
                        if (!rdrAttachments.IsDBNull(1)) row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrAttachments.GetString(1) });
                        else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                        row.Cells[3].ReadOnly = true;
                        row.Cells.Add(new DataGridViewButtonCell { Value = "View" });
                        if (!rdrAttachments.IsDBNull(2)) row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrAttachments.GetString(2) });
                        else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                        row.Cells[4].ReadOnly = true;
                        if (!rdrAttachments.IsDBNull(3)) row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrAttachments.GetDateTime(3).ToString("MM/dd/yyyy HH:mm:ss") });
                        else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                        row.Cells[6].ReadOnly = true;

                        gvCommunicationAttachment.Rows.Add(row);
                    }
                }
                rdrAttachments.Close();
                if (connRN.State != ConnectionState.Closed) connRN.Close();

                String strSqlQueryForCommunicationLogCreateDate = "select [dbo].[tbl_Communication].[CreateDate] from [dbo].[tbl_Communication] " +
                                                                  "where [dbo].[tbl_Communication].[CommunicationNo] = @CommunicationNo";

                SqlCommand cmdQueryForCommCreateDate = new SqlCommand(strSqlQueryForCommunicationLogCreateDate, connRN);
                cmdQueryForCommCreateDate.CommandType = CommandType.Text;

                cmdQueryForCommCreateDate.Parameters.AddWithValue("@CommunicationNo", CommunicationNo);

                if (connRN.State != ConnectionState.Closed)
                {
                    connRN.Close();
                    connRN.Open();
                }
                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                Object objCommunicationLogCreateDate = cmdQueryForCommCreateDate.ExecuteScalar();
                if (connRN.State != ConnectionState.Closed) connRN.Close();

                DateTime? CommunicationLogCreateDate = null;
                if (objCommunicationLogCreateDate != null)
                {
                    DateTime resultCommLogCreateDate;
                    if (DateTime.TryParse(objCommunicationLogCreateDate.ToString(), out resultCommLogCreateDate)) CommunicationLogCreateDate = resultCommLogCreateDate;
                }

                String strSqlQueryForLoggedInUserRole = "select [dbo].[tbl_user].[User_Role_Id] from [dbo].[tbl_user] where [dbo].[tbl_user].[User_Id] = @LoggedInUserId";

                SqlCommand cmdQueryForLoggedInUserRole = new SqlCommand(strSqlQueryForLoggedInUserRole, connRN);
                cmdQueryForLoggedInUserRole.CommandType = CommandType.Text;

                cmdQueryForLoggedInUserRole.Parameters.AddWithValue("@LoggedInUserId", nLoggedInUserId);

                if (connRN.State != ConnectionState.Closed)
                {
                    connRN.Close();
                    connRN.Open();
                }
                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                Object objLoggedInUserRole = cmdQueryForLoggedInUserRole.ExecuteScalar();
                if (connRN.State != ConnectionState.Closed) connRN.Close();

                //Int16? nLoggedInUserRoleId = null;
                UserRole? LoggedInUserRoleId = null;
                if (objLoggedInUserRole != null)
                {
                    Int16 resultLoggedInUserRole;
                    if (Int16.TryParse(objLoggedInUserRole.ToString(), out resultLoggedInUserRole)) LoggedInUserRoleId = (UserRole)resultLoggedInUserRole;
                }

                if (CommunicationLogCreateDate != null)
                {
                    if (LoggedInUserRoleId == UserRole.RNStaff ||
                        LoggedInUserRoleId == UserRole.NPStaff ||
                        LoggedInUserRoleId == UserRole.MSStaff ||
                        LoggedInUserRoleId == UserRole.FDStaff)
                    {
                        if (CommunicationLogCreateDate.Value.AddDays(1) > DateTime.Now)
                            MakeCommunicationLogUpdatableBeforeOneDayPassed();
                        else
                            MakeCommunicationSolutionUpdatable();
                    }

                    if (LoggedInUserRoleId == UserRole.RNManager ||
                        LoggedInUserRoleId == UserRole.NPManager ||
                        LoggedInUserRoleId == UserRole.MSManager ||
                        LoggedInUserRoleId == UserRole.FDManager ||
                        LoggedInUserRoleId == UserRole.Administrator ||
                        LoggedInUserRoleId == UserRole.SuperAdmin ||
                        LoggedInUserRoleId == UserRole.Executive)
                    {
                        MakeCommunicationLogUpdatableBeforeOneDayPassed();
                    }                    
                }

                //txtCommunicationBody.Focus();
            }
        }

        public void MakeCommunicationSolutionUpdatable()
        {
            txtCommunicationSubject.ReadOnly = true;
            txtCommunicationBody.ReadOnly = true;

            comboCaseNo.Enabled = false;
            comboCommunicationType.Enabled = false;

            btnAddNewAttachment.Enabled = true;
            btnDeleteAttachment.Enabled = true;

            btnSaveCommunication.Enabled = true;
            btnCommunicationCancel.Text = "Close";
        }

        public void MakeCommunicationLogUpdatableBeforeOneDayPassed()
        {
            comboCommunicationType.Enabled = true;
            comboCaseNo.Enabled = true;
            comboIllnessNo.Enabled = true;
            comboIncidentNo.Enabled = true;
            txtCommunicationSubject.Enabled = true;
            txtCommunicationBody.Enabled = true;
            txtCommunicationSolution.Enabled = true;
            btnAddNewAttachment.Enabled = true;
            btnDeleteAttachment.Enabled = true;
            gvCommunicationAttachment.Enabled = true;
        }

        private void btnCommunicationCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSaveCommunication_Click(object sender, EventArgs e)
        {
            String strIndividualId = txtCommunicationIndividualId.Text.Trim();

            String strCommunicationNo = txtCommunicationNo.Text.Trim();
            String strCaseNo = String.Empty;
            if (comboCaseNo.SelectedItem != null) strCaseNo = comboCaseNo.SelectedItem.ToString();
            //else
            //{
            //    MessageBox.Show("Please select Case No.", "Alert");
            //    return;
            //}

            String strIllnessNo = String.Empty;
            if (comboIllnessNo.SelectedItem != null) strIllnessNo = comboIllnessNo.SelectedItem.ToString();

            String strIncidentNo = String.Empty;
            if (comboIncidentNo.SelectedItem != null) strIncidentNo = comboIncidentNo.SelectedItem.ToString();

            String strCommunicationType = String.Empty;
            if (comboCommunicationType.SelectedItem != null) strCommunicationType = comboCommunicationType.SelectedItem.ToString();
            else
            {
                MessageBox.Show("Please select Communication Type.", "Alert");
                return;
            }

            String strSubject = String.Empty;
            if (txtCommunicationSubject.Text.Trim() != String.Empty) strSubject = txtCommunicationSubject.Text.Trim();
            String strBody = String.Empty;
            if (txtCommunicationBody.Text.Trim() != String.Empty) strBody = txtCommunicationBody.Text.Trim();
            String strSolution = String.Empty;
            if (txtCommunicationSolution.Text.Trim() != String.Empty) strSolution = txtCommunicationSolution.Text.Trim();


            CommunicationType? communicationType = null;

            switch (strCommunicationType)
            {
                case "Incoming Call":
                    communicationType = CommunicationType.IncomingCall;
                    break;
                case "Outgoing Call":
                    communicationType = CommunicationType.OutgoingCall;
                    break;
                case "Incoming Fax":
                    communicationType = CommunicationType.IncomingFax;
                    break;
                case "Outgoing Fax":
                    communicationType = CommunicationType.OutgoingFax;
                    break;
                //case "Incoming E-Fax":
                //    communicationType = CommunicationType.IncomingEFax;
                //    break;
                //case "Outgoing E-Fax":
                //    communicationType = CommunicationType.OutgoingEFax;
                //    break;
                case "Email Received":
                    communicationType = CommunicationType.EmailReceived;
                    break;
                case "Email Sent":
                    communicationType = CommunicationType.EmailSent;
                    break;
                case "Letter Received":
                    communicationType = CommunicationType.LetterReceived;
                    break;
                case "Letter Mailed":
                    communicationType = CommunicationType.LetterMailed;
                    break;
                case "Other":
                    communicationType = CommunicationType.Other;
                    break;
                case "Walk-in":
                    communicationType = CommunicationType.WalkIn;
                    break;
                case "Interdepartmental Communication":
                    communicationType = CommunicationType.InterdepartmentalCommunication;
                    break;
            }

            if (communicationType == null)
            {
                MessageBox.Show("You have not selected Communication Type.", "Error");
                return;
            }

            String strSqlQueryForCommunicationNo = "select [dbo].[tbl_Communication].[CommunicationNo] from [dbo].[tbl_Communication] " +
                                                   "where [dbo].[tbl_Communication].[CommunicationNo] = @CommunicationNo";

            SqlCommand cmdQueryForCommunicationNo = new SqlCommand(strSqlQueryForCommunicationNo, connRN);
            cmdQueryForCommunicationNo.CommandType = CommandType.Text;

            cmdQueryForCommunicationNo.Parameters.AddWithValue("@CommunicationNo", strCommunicationNo);

            if (connRN.State != ConnectionState.Closed)
            {
                connRN.Close();
                connRN.Open();
            }
            else if (connRN.State == ConnectionState.Closed) connRN.Open();
            Object objCommunicationNo = cmdQueryForCommunicationNo.ExecuteScalar();
            if (connRN.State != ConnectionState.Closed) connRN.Close();

            String CommunicationNo = String.Empty;
            if (objCommunicationNo != null) CommunicationNo = objCommunicationNo.ToString();

            if (CommunicationNo == String.Empty)        // New communication
            {
                Boolean bCommunicationSaved = false;

                String strSqlInsertNewCommunication = "insert into [dbo].[tbl_Communication] ([dbo].[tbl_Communiation].[Individual_Id], [dbo].[tbl_Communication].[CaseNo], " +
                                                      "[dbo].[tbl_Communication].[IllnessNo], [dbo].[tbl_Communication].[IncidentNo], " +
                                                      "[dbo].[tbl_Communication].[CommunicationNo], [dbo].[tbl_Communication].[CommunicationType], " +
                                                      "[dbo].[tbl_Communication].[Subject], [dbo].[tbl_Communication].[Body], [dbo].[tbl_Communication].[Solution], " +
                                                      "[dbo].[tbl_Communication].[CreateDate], [dbo].[tbl_Communication].[CreatedBy], " +
                                                      "[dbo].[tbl_Communication].[IsComplete], [dbo].[tbl_Communication].[CommunicationSolvedDate], [dbo].[tbl_Communication].[CommunicationSolvedBy]) " +
                                                      "values (@IndividualId, @CaseNo, @IllnessNo, @IncidentNo, @CommunicationNo, @CommunicationType, " +
                                                      "@Subject, @Body, @Solution,  @CreateDate, @CreatedBy, @IsComplete, @CommunicationSolvedDate, @CommunicationSolvedBy)";

                SqlCommand cmdInsertNewCommunication = new SqlCommand(strSqlInsertNewCommunication, connRN);
                cmdInsertNewCommunication.CommandType = CommandType.Text;

                cmdInsertNewCommunication.Parameters.AddWithValue("@IndividualId", strIndividualId);
                if (strCaseNo != String.Empty) cmdInsertNewCommunication.Parameters.AddWithValue("@CaseNo", strCaseNo);
                else cmdInsertNewCommunication.Parameters.AddWithValue("@CaseNo", DBNull.Value);
                if (strIllnessNo != String.Empty) cmdInsertNewCommunication.Parameters.AddWithValue("@IllnessNo", strIllnessNo);
                else cmdInsertNewCommunication.Parameters.AddWithValue("@IllnessNo", DBNull.Value);
                if (strIncidentNo != String.Empty) cmdInsertNewCommunication.Parameters.AddWithValue("@IncidentNo", strIncidentNo);
                else cmdInsertNewCommunication.Parameters.AddWithValue("@IncidentNo", DBNull.Value);
                cmdInsertNewCommunication.Parameters.AddWithValue("@communicationNo", strCommunicationNo);
                cmdInsertNewCommunication.Parameters.AddWithValue("@CommunicationType", communicationType);
                if (strSubject != String.Empty) cmdInsertNewCommunication.Parameters.AddWithValue("@Subject", strSubject);
                else cmdInsertNewCommunication.Parameters.AddWithValue("@Subject", DBNull.Value);
                //if (chkCommunnicationComplete.Checked) strBody = "Solved";
                if (strBody != String.Empty) cmdInsertNewCommunication.Parameters.AddWithValue("@Body", strBody);
                else cmdInsertNewCommunication.Parameters.AddWithValue("@Body", DBNull.Value);
                if (chkCommunnicationComplete.Checked) strSolution += Environment.NewLine + "Solved";
                if (strSolution != String.Empty) cmdInsertNewCommunication.Parameters.AddWithValue("@Solution", strSolution);
                else cmdInsertNewCommunication.Parameters.AddWithValue("@Solution", DBNull.Value);
                cmdInsertNewCommunication.Parameters.AddWithValue("@CreateDate", DateTime.Now);
                cmdInsertNewCommunication.Parameters.AddWithValue("@CreatedBy", nLoggedInUserId);
                cmdInsertNewCommunication.Parameters.AddWithValue("@IsComplete", chkCommunnicationComplete.Checked);
                if (strSolution != String.Empty || chkCommunnicationComplete.Checked)
                {
                    cmdInsertNewCommunication.Parameters.AddWithValue("@CommunicationSolvedDate", DateTime.Now);
                    cmdInsertNewCommunication.Parameters.AddWithValue("@CommunicationSolvedBy", nLoggedInUserId);
                }
                else
                {
                    cmdInsertNewCommunication.Parameters.AddWithValue("@CommunicationSolvedDate", DBNull.Value);
                    cmdInsertNewCommunication.Parameters.AddWithValue("@CommunicationSolvedBy", DBNull.Value);
                }

                if (connRN.State != ConnectionState.Closed)
                {
                    connRN.Close();
                    connRN.Open();
                }
                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                int nNewCommunicationInserted = cmdInsertNewCommunication.ExecuteNonQuery();
                if (connRN.State != ConnectionState.Closed) connRN.Close();

                if (nNewCommunicationInserted == 1) bCommunicationSaved = true;   //MessageBox.Show("New communication has been inserted.", "Info");
                else if (nNewCommunicationInserted == 0) bCommunicationSaved = false; // MessageBox.Show("New communication insertion has failed.", "Error"); 

                Boolean bAttachmentSaved = false;

                for (int i = 0; i < gvCommunicationAttachment.Rows.Count; i++)
                {
                    String CommunicationAttachmentNo = gvCommunicationAttachment["CommunicationAttachmentNo", i]?.Value?.ToString();

                    String strSqlQueryForCommAttachmentNo = "select [dbo].[tbl_CommunicationAttachments].[AttachmentNo] from [dbo].[tbl_CommunicationAttachments] " +
                                                            "where [dbo].[tbl_CommunicationAttachments].[AttachmentNo] = @CommAttachmentNo";

                    SqlCommand cmdQueyrForCommAttachmentNo = new SqlCommand(strSqlQueryForCommAttachmentNo, connRN);
                    cmdQueyrForCommAttachmentNo.CommandType = CommandType.Text;

                    cmdQueyrForCommAttachmentNo.Parameters.AddWithValue("@CommAttachmentNo", CommunicationAttachmentNo);

                    if (connRN.State != ConnectionState.Closed)
                    {
                        connRN.Close();
                        connRN.Open();
                    }
                    else if (connRN.State == ConnectionState.Closed) connRN.Open();
                    Object objCommAttachmentNo = cmdQueyrForCommAttachmentNo.ExecuteScalar();
                    if (connRN.State != ConnectionState.Closed) connRN.Close();

                    String CommAttachmentNo = String.Empty;
                    if (objCommAttachmentNo != null) CommAttachmentNo = objCommAttachmentNo.ToString();

                    if (CommAttachmentNo == String.Empty)
                    {
                        String NewCommunicationAttachmentNo = gvCommunicationAttachment["CommunicationAttachmentNo", i]?.Value?.ToString();
                        String NewCommunicationAttachmentFileName = gvCommunicationAttachment["CommunicationAttachmentFileName", i]?.Value?.ToString();

                        if (NewCommunicationAttachmentFileName == String.Empty)
                        {
                            MessageBox.Show("Attachment file has not been uploaded.", "Error");
                            return;
                        }

                        String CommNo = txtCommunicationNo.Text.Trim();
                        String strAttachmentCreateDate = gvCommunicationAttachment["CreateDateCommunicationAttachment", i]?.Value?.ToString();
                        DateTime resultAttachmentCreateDate;
                        DateTime? AttachmentCreateDate = null;

                        if (DateTime.TryParse(strAttachmentCreateDate, out resultAttachmentCreateDate)) AttachmentCreateDate = resultAttachmentCreateDate;
                        else
                        {
                            MessageBox.Show("The Attachment Create Date is invalid.", "Error");
                            return;
                        }

                        String strSqlInsertNewAttachment = "insert into [dbo].[tbl_CommunicationAttachments] ([dbo].[tbl_CommunicationAttachments].[IsDeleted], " +
                                                           "[dbo].[tbl_CommunicationAttachments].[CommunicationNo], [dbo].[tbl_CommunicationAttachments].[AttachmentNo], " +
                                                           "[dbo].[tbl_CommunicationAttachments].[AttachedFileName], " +
                                                           "[dbo].[tbl_CommunicationAttachments].[CreatedBy], [dbo].[tbl_CommunicationAttachments].[CreateDate]) " +
                                                           "values (0, @CommunicationNo, @AttachmentNo, @AttachedFileName, @CreatedBy, @CreateDate)";

                        SqlCommand cmdInsertNewAttachment = new SqlCommand(strSqlInsertNewAttachment, connRN);
                        cmdInsertNewAttachment.CommandType = CommandType.Text;

                        cmdInsertNewAttachment.Parameters.AddWithValue("@CommunicationNo", CommNo);
                        cmdInsertNewAttachment.Parameters.AddWithValue("@AttachmentNo", NewCommunicationAttachmentNo);
                        cmdInsertNewAttachment.Parameters.AddWithValue("@AttachedFileName", NewCommunicationAttachmentFileName);
                        cmdInsertNewAttachment.Parameters.AddWithValue("@CreatedBy", nLoggedInUserId);
                        cmdInsertNewAttachment.Parameters.AddWithValue("@CreateDate", AttachmentCreateDate.Value);

                        if (connRN.State != ConnectionState.Closed)
                        {
                            connRN.Close();
                            connRN.Open();
                        }
                        else if (connRN.State == ConnectionState.Closed) connRN.Open();
                        int nNewAttachmentInserted = cmdInsertNewAttachment.ExecuteNonQuery();
                        if (connRN.State != ConnectionState.Closed) connRN.Close();

                        if (nNewAttachmentInserted == 1) bAttachmentSaved = true;
                        else if (nNewAttachmentInserted == 0) bAttachmentSaved = false;
                    }
                }

                if (bCommunicationSaved)
                {
                    if (gvCommunicationAttachment.Rows.Count == 0) MessageBox.Show("Communication has been saved", "Info");
                    if (gvCommunicationAttachment.Rows.Count > 0 && bAttachmentSaved) MessageBox.Show("The Communication and Attachments have been saved.");
                }
                else if (bCommunicationSaved && gvCommunicationAttachment.Rows.Count > 0 && !bAttachmentSaved) MessageBox.Show("Communication has been saved, but at least one of attachment has not been save.", "Error");
                else if (!bCommunicationSaved && gvCommunicationAttachment.Rows.Count > 0 && !bAttachmentSaved) MessageBox.Show("Communiation and Attachment have not been saved.", "Error");

                Close();
            }
            else
            {
                // 07/29/19 - begin here to update communication code
                String strSqlUpdateCommunication = "update [dbo].[tbl_Communication] set [dbo].[tbl_Communication].[CaseNo] = @NewCaseNo, " +
                                                   "[dbo].[tbl_Communication].[CommunicationType] = @NewCommunicationType, " +
                                                   "[dbo].[tbl_Communication].[Subject] = @NewSubject, " +
                                                   "[dbo].[tbl_Communication].[Body] = @NewBody, " +
                                                   "[dbo].[tbl_Communication].[Solution] = @NewSolution, " +
                                                   "[dbo].[tbl_Communication].[ModifiDate] = @ModifiDate, " +
                                                   "[dbo].[tbl_Communication].[ModifiedBy] = @ModifiedBy, " +
                                                   "[dbo].[tbl_Communication].[IsComplete] = @IsComplete, " +
                                                   "[dbo].[tbl_Communication].[CommunicationSolvedDate] = @CommunicationSolvedDate, " +
                                                   "[dbo].[tbl_Communication].[CommunicationSolvedBy] = @CommunicationSolvedBy " +
                                                   "where [dbo].[tbl_Communication].[Individual_Id] = @IndividualId and " +
                                                   "[dbo].[tbl_Communication].[CommunicationNo] = @CommunicationNo";

                SqlCommand cmdUpdateCommunication = new SqlCommand(strSqlUpdateCommunication, connRN);
                cmdUpdateCommunication.CommandType = CommandType.Text;

                cmdUpdateCommunication.Parameters.AddWithValue("@NewCaseNo", strCaseNo);
                cmdUpdateCommunication.Parameters.AddWithValue("@NewCommunicationType", (int)communicationType);
                cmdUpdateCommunication.Parameters.AddWithValue("@NewSubject", strSubject);
                cmdUpdateCommunication.Parameters.AddWithValue("@NewBody", strBody);
                if (chkCommunnicationComplete.Checked) strSolution += Environment.NewLine + "Solved";
                cmdUpdateCommunication.Parameters.AddWithValue("@NewSolution", strSolution);
                cmdUpdateCommunication.Parameters.AddWithValue("@ModifiDate", DateTime.Now);
                cmdUpdateCommunication.Parameters.AddWithValue("@ModifiedBy", nLoggedInUserId);
                cmdUpdateCommunication.Parameters.AddWithValue("@IndividualId", strIndividualId);
                cmdUpdateCommunication.Parameters.AddWithValue("@CommunicationNo", strCommunicationNo);
                cmdUpdateCommunication.Parameters.AddWithValue("@IsComplete", chkCommunnicationComplete.Checked);
                if (strSolution != String.Empty || chkCommunnicationComplete.Checked)
                {
                    cmdUpdateCommunication.Parameters.AddWithValue("@CommunicationSolvedDate", DateTime.Now);
                    cmdUpdateCommunication.Parameters.AddWithValue("@CommunicationSolvedBy", nLoggedInUserId);
                }
                else
                {
                    cmdUpdateCommunication.Parameters.AddWithValue("@CommunicationSolvedDate", DBNull.Value);
                    cmdUpdateCommunication.Parameters.AddWithValue("@CommunicationSolvedBy", DBNull.Value);
                }

                if (connRN.State != ConnectionState.Closed)
                {
                    connRN.Close();
                    connRN.Open();
                }
                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                int nCommunicationUpdated = cmdUpdateCommunication.ExecuteNonQuery();
                if (connRN.State != ConnectionState.Closed) connRN.Close();

                if (nCommunicationUpdated == 1)
                {
                    Boolean bAttachmentSaved = true;

                    for (int i = 0; i < gvCommunicationAttachment.Rows.Count; i++)
                    {
                        String CommunicationAttachmentNo = gvCommunicationAttachment["CommunicationAttachmentNo", i]?.Value?.ToString();

                        String strSqlQueryForCommAttachmentNo = "select [dbo].[tbl_CommunicationAttachments].[AttachmentNo] from [dbo].[tbl_CommunicationAttachments] " +
                                                                "where [dbo].[tbl_CommunicationAttachments].[AttachmentNo] = @CommAttachmentNo";

                        SqlCommand cmdQueryForCommunicationAttachmentNo = new SqlCommand(strSqlQueryForCommAttachmentNo, connRN);
                        cmdQueryForCommunicationAttachmentNo.CommandType = CommandType.Text;

                        cmdQueryForCommunicationAttachmentNo.Parameters.AddWithValue("@CommAttachmentNo", CommunicationAttachmentNo);

                        if (connRN.State != ConnectionState.Closed)
                        {
                            connRN.Close();
                            connRN.Open();
                        }
                        else if (connRN.State == ConnectionState.Closed) connRN.Open();
                        Object objCommunicationAttachmentNo = cmdQueryForCommunicationAttachmentNo.ExecuteScalar();
                        if (connRN.State != ConnectionState.Closed) connRN.Close();

                        String CommAttachmentNo = String.Empty;
                        if (objCommunicationAttachmentNo != null) CommAttachmentNo = objCommunicationAttachmentNo.ToString();

                        if (CommAttachmentNo == String.Empty)
                        {
                            String NewCommunicationAttachmentNo = gvCommunicationAttachment["CommunicationAttachmentNo", i]?.Value?.ToString();
                            String NewCommunicationAttachmentFileName = gvCommunicationAttachment["CommunicationAttachmentFileName", i]?.Value?.ToString();

                            if (NewCommunicationAttachmentFileName == String.Empty)
                            {
                                MessageBox.Show("Attachment file has not been uploaded.", "Error");
                                return;
                            }

                            String CommNo = txtCommunicationNo.Text.Trim();
                            String strAttachmentCreateDate = gvCommunicationAttachment["CreateDateCommunicationAttachment", i]?.Value?.ToString();
                            DateTime resultAttachmentCreateDate;
                            DateTime? AttachmentCreateDate = null;

                            if (DateTime.TryParse(strAttachmentCreateDate, out resultAttachmentCreateDate)) AttachmentCreateDate = resultAttachmentCreateDate;
                            else
                            {
                                MessageBox.Show("The Attachment Create Date is invalid.", "Error");
                                return;
                            }

                            String strSqlInsertNewAttachment = "insert into [dbo].[tbl_CommunicationAttachments] ([dbo].[tbl_CommunicationAttachments].[IsDeleted], " +
                                                               "[dbo].[tbl_CommunicationAttachments].[CommunicationNo], [dbo].[tbl_CommunicationAttachments].[AttachmentNo], " +
                                                               "[dbo].[tbl_CommunicationAttachments].[AttachedFileName], " +
                                                               "[dbo].[tbl_CommunicationAttachments].[CreatedBy], [dbo].[tbl_CommunicationAttachments].[CreateDate]) " +
                                                               "values (0, @CommunicationNo, @AttachmentNo, @AttachmentFileName, @CreatedBy, @CreateDate)";

                            SqlCommand cmdInsertNewAttachment = new SqlCommand(strSqlInsertNewAttachment, connRN);
                            cmdInsertNewAttachment.CommandType = CommandType.Text;

                            cmdInsertNewAttachment.Parameters.AddWithValue("@CommunicationNo", CommNo);
                            cmdInsertNewAttachment.Parameters.AddWithValue("@AttachmentNo", NewCommunicationAttachmentNo);
                            cmdInsertNewAttachment.Parameters.AddWithValue("@AttachmentFileName", NewCommunicationAttachmentFileName);
                            cmdInsertNewAttachment.Parameters.AddWithValue("@CreatedBy", nLoggedInUserId);
                            cmdInsertNewAttachment.Parameters.AddWithValue("@CreateDate", DateTime.Now);

                            if (connRN.State != ConnectionState.Closed)
                            {
                                connRN.Close();
                                connRN.Open();
                            }
                            else if (connRN.State == ConnectionState.Closed) connRN.Open();
                            int nAttachmentSaved = cmdInsertNewAttachment.ExecuteNonQuery();
                            if (connRN.State != ConnectionState.Closed) connRN.Close();

                            if (nAttachmentSaved != 1) bAttachmentSaved = false;
                        }
                    }

                    if (!bAttachmentSaved)
                    {
                        MessageBox.Show("At least one of new attachment has not been saved.", "Error");
                        return;
                    }
                    else
                    {
                        MessageBox.Show("The Communication has been saved.", "Info");
                        Close();
                    }

                }
            }
        }

        private void btnAddNewAttachment_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = new DataGridViewRow();

            DataGridViewCheckBoxCell chkSelectedCell = new DataGridViewCheckBoxCell();
            chkSelectedCell.Value = false;
            row.Cells.Add(chkSelectedCell);
            row.Cells[0].ReadOnly = false;

            String strQueryForLastAttachmentNo = "select [dbo].[tbl_LastID].[CommAttachmentNo] from [dbo].[tbl_LastID] where [dbo].[tbl_LastID].[Id] = 1";

            SqlCommand cmdQueryForLastAttachmentNo = new SqlCommand(strQueryForLastAttachmentNo, connRN);
            cmdQueryForLastAttachmentNo.CommandType = CommandType.Text;

            if (connRN.State != ConnectionState.Closed)
            {
                connRN.Close();
                connRN.Open();
            }
            else if (connRN.State == ConnectionState.Closed) connRN.Open();
            Object objLastCommAttachmentNo = cmdQueryForLastAttachmentNo.ExecuteScalar();
            if (connRN.State != ConnectionState.Closed) connRN.Close();

            String LastCommAttachmentNo = String.Empty;
            String NewCommAttachmentNo = String.Empty;
            if (objLastCommAttachmentNo != null) LastCommAttachmentNo = objLastCommAttachmentNo.ToString();

            if (LastCommAttachmentNo != String.Empty)
            {
                int nLastCommAttachmentNo = Int32.Parse(LastCommAttachmentNo.Substring(8));
                nLastCommAttachmentNo++;

                NewCommAttachmentNo = "CommAtt-" + nLastCommAttachmentNo.ToString();
            }
            else NewCommAttachmentNo = "CommAtt-1";

            String strUpdateNewCommAttachmentNo = "update [dbo].[tbl_LastID] set [dbo].[tbl_LastID].[CommAttachmentNo] = @NewCommAttachmentNo where [dbo].[tbl_LastID].[Id] = 1";

            SqlCommand cmdUpdateNewCommAttachmentNo = new SqlCommand(strUpdateNewCommAttachmentNo, connRN);
            cmdUpdateNewCommAttachmentNo.CommandType = CommandType.Text;

            cmdUpdateNewCommAttachmentNo.Parameters.AddWithValue("@NewCommAttachmentNo", NewCommAttachmentNo);

            if (connRN.State != ConnectionState.Closed)
            {
                connRN.Close();
                connRN.Open();
            }
            else if (connRN.State == ConnectionState.Closed) connRN.Open();
            int nCommAttachmentNoUpdated = cmdUpdateNewCommAttachmentNo.ExecuteNonQuery();
            if (connRN.State != ConnectionState.Closed) connRN.Close();

            DataGridViewTextBoxCell txtCommAttNo = new DataGridViewTextBoxCell();
            txtCommAttNo.Value = NewCommAttachmentNo;
            row.Cells.Add(txtCommAttNo);
            row.Cells[1].ReadOnly = true;

            DataGridViewButtonCell btnUploadCell = new DataGridViewButtonCell();
            btnUploadCell.Value = "Attach";
            row.Cells.Add(btnUploadCell);
            row.Cells[2].ReadOnly = false;

            DataGridViewTextBoxCell txtDestinationPathCell = new DataGridViewTextBoxCell();
            txtDestinationPathCell.Value = String.Empty;
            row.Cells.Add(txtDestinationPathCell);
            row.Cells[3].ReadOnly = true;

            DataGridViewButtonCell btnViewCell = new DataGridViewButtonCell();
            btnViewCell.Value = "View";
            row.Cells.Add(btnViewCell);
            row.Cells[4].ReadOnly = false;

            String strSqlQueryForStaffName = "select [dbo].[tbl_user].[User_Name] from [dbo].[tbl_user] where [dbo].[tbl_user].[User_Id] = @LoggedInUserId";

            SqlCommand cmdQueryForStaffName = new SqlCommand(strSqlQueryForStaffName, connRN);
            cmdQueryForStaffName.CommandType = CommandType.Text;

            cmdQueryForStaffName.Parameters.AddWithValue("@LoggedInUserId", nLoggedInUserId);

            if (connRN.State != ConnectionState.Closed)
            {
                connRN.Close();
                connRN.Open();
            }
            else if (connRN.State == ConnectionState.Closed) connRN.Open();
            Object objStaffName = cmdQueryForStaffName.ExecuteScalar();
            if (connRN.State != ConnectionState.Closed) connRN.Close();

            String StaffName = String.Empty;
            if (objStaffName != null) StaffName = objStaffName.ToString();

            if (StaffName != String.Empty)
            {
                DataGridViewTextBoxCell txtCreatingStaffName = new DataGridViewTextBoxCell();
                txtCreatingStaffName.Value = StaffName;
                row.Cells.Add(txtCreatingStaffName);
                row.Cells[5].ReadOnly = true;
            }

            DataGridViewTextBoxCell txtCreateDateCell = new DataGridViewTextBoxCell();
            txtCreateDateCell.Value = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            row.Cells.Add(txtCreateDateCell);
            row.Cells[6].ReadOnly = true;

            gvCommunicationAttachment.Rows.Add(row);
        }

        private void gvCommunicationAttachment_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            String CommAttachmentNo = String.Empty;

            if (gvCommunicationAttachment["CommunicationAttachmentNo", e.RowIndex]?.Value?.ToString() != String.Empty)
                CommAttachmentNo = gvCommunicationAttachment["CommunicationAttachmentNo", e.RowIndex]?.Value?.ToString();

            String strSqlQueryForCommunicationAttachmentNo = "select [dbo].[tbl_CommunicationAttachments].[AttachmentNo] from [dbo].[tbl_CommunicationAttachments] " +
                                                             "where [dbo].[tbl_CommunicationAttachments].[AttachmentNo] = @AttachmentNo";

            SqlCommand cmdQueryForCommunicationAttachmentNo = new SqlCommand(strSqlQueryForCommunicationAttachmentNo, connRN);
            cmdQueryForCommunicationAttachmentNo.CommandType = CommandType.Text;

            cmdQueryForCommunicationAttachmentNo.Parameters.AddWithValue("@AttachmentNo", CommAttachmentNo);

            if (connRN.State != ConnectionState.Closed)
            {
                connRN.Close();
                connRN.Open();
            }
            else if (connRN.State == ConnectionState.Closed) connRN.Open();
            Object objCommunicationAttachmentNo = cmdQueryForCommunicationAttachmentNo.ExecuteScalar();
            if (connRN.State != ConnectionState.Closed) connRN.Close();

            String AttachmentNo = String.Empty;
            if (objCommunicationAttachmentNo != null) AttachmentNo = objCommunicationAttachmentNo.ToString();

            if (AttachmentNo != String.Empty && e.ColumnIndex == 2)
            {
                MessageBox.Show("The attachment is read only.", "Alert");
                return;
            }

            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 2)
                {
                    OpenFileDialog dlgUpload = new OpenFileDialog();

                    if (dlgUpload.ShowDialog() == DialogResult.OK)
                    {

                        String strIndividualId = txtCommunicationIndividualId.Text.Trim();
                        String strCommunicationNo = txtCommunicationNo.Text.Trim();

                        String strAttachmentFileName = strIndividualId + "_" +
                                                       strCommunicationNo + "_" +
                                                       DateTime.Now.ToString("MM-dd-yyyy-HH-mm-ss") + "_" +
                                                       Path.GetFileName(dlgUpload.FileName);

                        String CommunicationAttachmentDestinationFileName = CommunicationAttachmentDestinationPath + strAttachmentFileName;
                                                                            
                        
                        try
                        {
                            File.Copy(dlgUpload.FileName, CommunicationAttachmentDestinationFileName, true);
                            //gvCommunicationAttachment[3, e.RowIndex].Value = Path.GetFileName(dlgUpload.FileName);
                            gvCommunicationAttachment[3, e.RowIndex].Value = strAttachmentFileName;
                            gvCommunicationAttachment[3, e.RowIndex].ReadOnly = true;

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    else return;
                }

                if (e.ColumnIndex == 4)
                {
                    String CommunicationAttachmentFileName = String.Empty;
                    CommunicationAttachmentFileName = gvCommunicationAttachment[3, e.RowIndex]?.Value?.ToString();

                    if (CommunicationAttachmentFileName != String.Empty)
                    {
                        ProcessStartInfo info = new ProcessStartInfo();
                        info.FileName = CommunicationAttachmentDestinationPath + CommunicationAttachmentFileName;

                        Process.Start(info);
                    }
                    else
                    {
                        MessageBox.Show("The destination path is empty.", "Error");
                        return;
                    }
                }
            }
        }

        private void btnDeleteAttachment_Click(object sender, EventArgs e)
        {
            List<String> lstCommunicationAttachmentNo = new List<string>();

            Boolean bAttachmentSelected = false;
            for (int i = 0; i < gvCommunicationAttachment.Rows.Count; i++)
            {
                if (Boolean.Parse(gvCommunicationAttachment["SelectedCommunicationAttachment", i]?.Value?.ToString()) == true)
                {
                    bAttachmentSelected = true;
                }
            }

            if (!bAttachmentSelected)
            {
                MessageBox.Show("Please selected Communication Attachment to delete.", "Alert");
                return;
            }

            DialogResult result = MessageBox.Show("Do you want to delete the selected attachments?", "Confirm", MessageBoxButtons.YesNo);

            if (result == DialogResult.No) return;

            for (int i = 0; i < gvCommunicationAttachment.Rows.Count; i++)
            {
                if (Boolean.Parse(gvCommunicationAttachment["SelectedCommunicationAttachment", i]?.Value?.ToString()) == true)
                {
                    String CommunicationAttachmentNo = gvCommunicationAttachment["CommunicationAttachmentNo", i]?.Value?.ToString();

                    if (CommunicationAttachmentNo != null)
                    {
                        String strSqlQueryForCreateStaffId = "select [dbo].[tbl_CommunicationAttachments].[CreatedBy] from [dbo].[tbl_CommunicationAttachments] " +
                                                             "where [dbo].[tbl_CommunicationAttachments].[AttachmentNo] = @CommAttachmentNo";

                        SqlCommand cmdQueryForCreateStaffId = new SqlCommand(strSqlQueryForCreateStaffId, connRN2);
                        cmdQueryForCreateStaffId.CommandType = CommandType.Text;

                        cmdQueryForCreateStaffId.Parameters.AddWithValue("@CommAttachmentNo", CommunicationAttachmentNo);

                        if (connRN2.State != ConnectionState.Closed)
                        {
                            connRN2.Close();
                            connRN2.Open();
                        }
                        else if (connRN2.State == ConnectionState.Closed) connRN2.Open();
                        Object objCommunicationAttachmentCreateStaffId = cmdQueryForCreateStaffId.ExecuteScalar();
                        if (connRN2.State != ConnectionState.Closed) connRN2.Close();

                        Int16? nCommunicationAttachmentCreateStaffId = null;
                        if (objCommunicationAttachmentCreateStaffId != null)
                        {
                            Int16 resultCommAttachmentStaffId;
                            if (Int16.TryParse(objCommunicationAttachmentCreateStaffId.ToString(), out resultCommAttachmentStaffId))
                                nCommunicationAttachmentCreateStaffId = resultCommAttachmentStaffId;
                        }

                        if (nCommunicationAttachmentCreateStaffId != null)
                        {
                            String strSqlQueryForCreateStaffDepartment = "select [dbo].[tbl_user].[Department_Id] from [dbo].[tbl_user] where [dbo].[tbl_user].[User_Id] = @CommunicationCreateStaffId";

                            SqlCommand cmdQueryForCreateStaffDepartment = new SqlCommand(strSqlQueryForCreateStaffDepartment, connRN2);
                            cmdQueryForCreateStaffDepartment.CommandType = CommandType.Text;

                            cmdQueryForCreateStaffDepartment.Parameters.AddWithValue("@CommunicationCreateStaffId", nCommunicationAttachmentCreateStaffId);

                            if (connRN2.State != ConnectionState.Closed)
                            {
                                connRN2.Close();
                                connRN2.Open();
                            }
                            else if (connRN2.State == ConnectionState.Closed) connRN2.Open();
                            Object objCreateStaffDepartment = cmdQueryForCreateStaffDepartment.ExecuteScalar();
                            if (connRN2.State != ConnectionState.Closed) connRN2.Close();

                            Int16? nCommunicationAttachmentCreateStaffDepartmentId = null;
                            if (objCreateStaffDepartment != null)
                            {
                                Int16 resultComAttachmentCreateStaffDepartmentId;
                                if (Int16.TryParse(objCreateStaffDepartment.ToString(), out resultComAttachmentCreateStaffDepartmentId))
                                    nCommunicationAttachmentCreateStaffDepartmentId = resultComAttachmentCreateStaffDepartmentId;
                            }

                            if (nCommunicationAttachmentCreateStaffId != nLoggedInUserId)
                            {
                                String strSqlQueryForLoggedInUserRoleId = "select [dbo].[tbl_user].[User_Role_Id] from [dbo].[tbl_user] where [dbo].[tbl_user].[User_Id] = @LoggedInUserId";

                                SqlCommand cmdQueryForLoggedInUserRoleId = new SqlCommand(strSqlQueryForLoggedInUserRoleId, connRN2);
                                cmdQueryForLoggedInUserRoleId.CommandType = CommandType.Text;

                                cmdQueryForLoggedInUserRoleId.Parameters.AddWithValue("@LoggedInUserId", nLoggedInUserId);

                                if (connRN2.State != ConnectionState.Closed)
                                {
                                    connRN2.Close();
                                    connRN2.Open();
                                }
                                else if (connRN2.State == ConnectionState.Closed) connRN2.Open();
                                Object objLoggedInUserRoleId = cmdQueryForLoggedInUserRoleId.ExecuteScalar();
                                if (connRN2.State != ConnectionState.Closed) connRN2.Close();

                                UserRole? LoggedInUserRole = null;
                                if (objLoggedInUserRoleId != null)
                                {
                                    Int16 resultLoggedInUserRole;
                                    if (Int16.TryParse(objLoggedInUserRoleId.ToString(), out resultLoggedInUserRole)) LoggedInUserRole = (UserRole)resultLoggedInUserRole;
                                }

                                if (LoggedInUserRole != UserRole.Executive &&
                                    LoggedInUserRole != UserRole.SuperAdmin &&
                                    LoggedInUserRole != UserRole.Administrator &&
                                    LoggedInUserRole != UserRole.FDManager &&
                                    LoggedInUserRole != UserRole.MSManager &&
                                    LoggedInUserRole != UserRole.NPManager &&
                                    LoggedInUserRole != UserRole.RNManager)
                                {
                                    MessageBox.Show("You cannot delete Communication Log Attachment you didn't create.", "Alert");
                                    return;
                                }

                                switch ((Department)nCommunicationAttachmentCreateStaffDepartmentId)
                                {
                                    case Department.Finance:
                                        if (LoggedInUserRole != UserRole.FDManager)
                                        {
                                            MessageBox.Show("You cannot delete Communication Log Attachment other department staff created.", "Alert");
                                            return;
                                        }
                                        break;
                                    case Department.MemberService:
                                        if (LoggedInUserRole != UserRole.MSManager)
                                        {
                                            MessageBox.Show("You cannot delete Communication Log Attachment other department staff created.", "Alert");
                                            return;
                                        }
                                        break;
                                    case Department.NeedsProcessing:
                                        if (LoggedInUserRole != UserRole.NPManager)
                                        {
                                            MessageBox.Show("You cannot delete Communication Log Attachment other department staff created.", "Alert");
                                            return;
                                        }
                                        break;
                                    case Department.ReviewAndNegotiation:
                                        if (LoggedInUserRole != UserRole.RNManager)
                                        {
                                            MessageBox.Show("You cannot delete Communication Log Attachment other department staff created.", "Alert");
                                            return;
                                        }
                                        break;
                                }
                            }
                        }
                    }
                }
            }

            Boolean bOneDayPassed = false;
            for (int i = 0; i < gvCommunicationAttachment.Rows.Count; i++)
            {
                if (Boolean.Parse(gvCommunicationAttachment["SelectedCommunicationAttachment", i]?.Value?.ToString()) == true)
                {
                    if (gvCommunicationAttachment["CreateDateCommunicationAttachment", i]?.Value != null &&
                        gvCommunicationAttachment["CreateDateCommunicationAttachment", i]?.Value?.ToString() != String.Empty)
                    {
                        String strCommunicationLogAttachmentCreateDate = gvCommunicationAttachment["CreateDateCommunicationAttachment", i]?.Value?.ToString();

                        DateTime? CommunicationLogAttachmentCreateDate = null;
                        DateTime resultCommunicationLogAttachmentCreateDate;

                        if (DateTime.TryParse(strCommunicationLogAttachmentCreateDate, out resultCommunicationLogAttachmentCreateDate))
                            CommunicationLogAttachmentCreateDate = resultCommunicationLogAttachmentCreateDate;
                        if (CommunicationLogAttachmentCreateDate.Value.AddDays(1) < DateTime.Now) bOneDayPassed = true;
                    }
                }
            }

            if (bOneDayPassed)
            {
                String strSqlQueryForLoggedInUserRoleId = "select [dbo].[tbl_user].[User_Role_Id] from [dbo].[tbl_user] where [dbo].[tbl_user].[User_Id] = @LoggedInUserId";

                SqlCommand cmdQueryForLoggedInUserRoleId = new SqlCommand(strSqlQueryForLoggedInUserRoleId, connRN2);
                cmdQueryForLoggedInUserRoleId.CommandType = CommandType.Text;

                cmdQueryForLoggedInUserRoleId.Parameters.AddWithValue("@LoggedInUserId", nLoggedInUserId);

                if (connRN2.State != ConnectionState.Closed)
                {
                    connRN2.Close();
                    connRN2.Open();
                }
                else if (connRN2.State == ConnectionState.Closed) connRN2.Open();
                Object objLoggedInUserRoleId = cmdQueryForLoggedInUserRoleId.ExecuteScalar();
                if (connRN2.State != ConnectionState.Closed) connRN2.Close();

                UserRole? LoggedInUserRole = null;
                if (objLoggedInUserRoleId != null)
                {
                    Int16 resultLoggedInUserRole;
                    if (Int16.TryParse(objLoggedInUserRoleId.ToString(), out resultLoggedInUserRole)) LoggedInUserRole = (UserRole)resultLoggedInUserRole;
                }

                if (LoggedInUserRole == UserRole.FDStaff ||
                    LoggedInUserRole == UserRole.MSStaff ||
                    LoggedInUserRole == UserRole.NPStaff ||
                    LoggedInUserRole == UserRole.RNStaff)
                {
                    MessageBox.Show("You don't have the right to delete Communication Log Attachment more than one day old.", "Alert");
                    return;
                }
            }

            Boolean bAttachmentDeleted = true;

            for (int i = 0; i < gvCommunicationAttachment.Rows.Count; i++)
            {

                if (Boolean.Parse(gvCommunicationAttachment["SelectedCommunicationAttachment", i]?.Value?.ToString()) == true)
                {
                    String SelectedAttachmentNo = gvCommunicationAttachment["CommunicationAttachmentNo", i]?.Value?.ToString();

                    String strSqlQueryForCommunicationAttachmentNo = "select [dbo].[tbl_CommunicationAttachments].[AttachmentNo] from [dbo].[tbl_CommunicationAttachments] " +
                                                 "where [dbo].[tbl_CommunicationAttachments].[AttachmentNo] = @AttachmentNo";

                    SqlCommand cmdQueryForCommunicationAttachmentNo = new SqlCommand(strSqlQueryForCommunicationAttachmentNo, connRN);
                    cmdQueryForCommunicationAttachmentNo.CommandType = CommandType.Text;

                    cmdQueryForCommunicationAttachmentNo.Parameters.AddWithValue("@AttachmentNo", SelectedAttachmentNo);

                    if (connRN.State != ConnectionState.Closed)
                    {
                        connRN.Close();
                        connRN.Open();
                    }
                    else if (connRN.State == ConnectionState.Closed) connRN.Open();
                    Object objAttachmentNo = cmdQueryForCommunicationAttachmentNo.ExecuteScalar();
                    if (connRN.State != ConnectionState.Closed) connRN.Close();

                    String AttachmentNo = String.Empty;
                    if (objAttachmentNo != null) AttachmentNo = objAttachmentNo.ToString();

                    if (AttachmentNo == String.Empty)
                    {
                        gvCommunicationAttachment.Rows.RemoveAt(i);
                        i--;
                    }
                    else
                    {
                        String strSqlDeleteCommunicationAttachment = "update [dbo].[tbl_CommunicationAttachments] set [dbo].[tbl_CommunicationAttachments].[IsDeleted] = 1 " +
                                                                     "where [dbo].[tbl_CommunicationAttachments].[AttachmentNo] = @AttachmentNo";

                        SqlCommand cmdDeleteCommunicationAttachment = new SqlCommand(strSqlDeleteCommunicationAttachment, connRN);
                        cmdDeleteCommunicationAttachment.CommandType = CommandType.Text;

                        cmdDeleteCommunicationAttachment.Parameters.AddWithValue("@AttachmentNo", AttachmentNo);

                        if (connRN.State != ConnectionState.Closed)
                        {
                            connRN.Close();
                            connRN.Open();
                        }
                        else if (connRN.State == ConnectionState.Closed) connRN.Open();
                        int nAttachmentDeleted = cmdDeleteCommunicationAttachment.ExecuteNonQuery();
                        if (connRN.State != ConnectionState.Closed) connRN.Close();

                        if (nAttachmentDeleted == 0) bAttachmentDeleted = false;
                        if (nAttachmentDeleted == 1)
                        {
                            gvCommunicationAttachment.Rows.RemoveAt(i);
                            i--;
                        }
                    }
                }
            }

            if (bAttachmentDeleted)
            {
                MessageBox.Show("The Communication Attachment have been deleted.", "Info");
                return;
            }
            else
            {
                MessageBox.Show("At least one of Communication Attachment has not been deleted.", "Error");
                return;
            }
        }

        private void comboCaseNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboCaseNo.SelectedItem != null)
            {
                if (comboCaseNo.SelectedItem.ToString() == "None")
                {
                    comboIllnessNo.Items.Clear();
                    comboIllnessNo.Items.Add("None");
                    comboIncidentNo.Items.Clear();
                    comboIncidentNo.Items.Add("None");

                    String strSqlQueryForIllnessNo = "select [dbo].[tbl_illness].[IllnessNo] from [dbo].[tbl_illness] where [dbo].[tbl_illness].[Individual_Id] = @IndividualId";

                    SqlCommand cmdQueryForIllnessNo = new SqlCommand(strSqlQueryForIllnessNo, connRN);
                    cmdQueryForIllnessNo.CommandType = CommandType.Text;

                    cmdQueryForIllnessNo.Parameters.AddWithValue("@IndividualId", IndividualId);

                    if (connRN.State != ConnectionState.Closed)
                    {
                        connRN.Close();
                        connRN.Open();
                    }
                    else if (connRN.State == ConnectionState.Closed) connRN.Open();
                    SqlDataReader rdrIllnessNo = cmdQueryForIllnessNo.ExecuteReader();
                    if (rdrIllnessNo.HasRows)
                    {
                        while(rdrIllnessNo.Read())
                        {
                            if (!rdrIllnessNo.IsDBNull(0)) comboIllnessNo.Items.Add(rdrIllnessNo.GetString(0));
                        }
                    }
                    rdrIllnessNo.Close();
                    if (connRN.State != ConnectionState.Closed) connRN.Close();

                    comboIllnessNo.SelectedIndex = 0;


                    String strSqlQueryForIncidentNo = "select [dbo].[tbl_incident].[IncidentNo] from [dbo].[tbl_incident] where [dbo].[tbl_incident].[Individual_id] = @IndividualId";

                    SqlCommand cmdQueryForIncidentNo = new SqlCommand(strSqlQueryForIncidentNo, connRN);
                    cmdQueryForIncidentNo.CommandType = CommandType.Text;

                    cmdQueryForIncidentNo.Parameters.AddWithValue("@IndividualId", IndividualId);

                    if (connRN.State != ConnectionState.Closed)
                    {
                        connRN.Close();
                        connRN.Open();
                    }
                    else if (connRN.State == ConnectionState.Closed) connRN.Open();
                    SqlDataReader rdrIncidentNo = cmdQueryForIncidentNo.ExecuteReader();
                    if (rdrIncidentNo.HasRows)
                    {
                        while (rdrIncidentNo.Read())
                        {
                            if (!rdrIncidentNo.IsDBNull(0)) comboIncidentNo.Items.Add(rdrIncidentNo.GetString(0));
                        }
                    }
                    rdrIncidentNo.Close();
                    if (connRN.State != ConnectionState.Closed) connRN.Close();

                    comboIncidentNo.SelectedIndex = 0;
                }
                else
                {
                    String CaseNoSelected = comboCaseNo.SelectedItem.ToString();

                    String strSqlQueryForIllnessNo = "select [dbo].[tbl_illness].[IllnessNo] from [dbo].[tbl_illness] " +
                                                     "where [dbo].[tbl_illness].[Case_Id] = @CaseNo and " +
                                                     "[dbo].[tbl_illness].[Individual_Id] = @IndividualId";

                    SqlCommand cmdQueryForIllnessNo = new SqlCommand(strSqlQueryForIllnessNo, connRN);
                    cmdQueryForIllnessNo.CommandType = CommandType.Text;

                    cmdQueryForIllnessNo.Parameters.AddWithValue("@CaseNo", CaseNoSelected);
                    cmdQueryForIllnessNo.Parameters.AddWithValue("@IndividualId", IndividualId);

                    if (connRN.State != ConnectionState.Closed)
                    {
                        connRN.Close();
                        connRN.Open();
                    }
                    else if (connRN.State == ConnectionState.Closed) connRN.Open();
                    SqlDataReader rdrIllnessNo = cmdQueryForIllnessNo.ExecuteReader();
                    comboIllnessNo.Items.Clear();
                    comboIllnessNo.Items.Add("None");
                    if (rdrIllnessNo.HasRows)
                    {
                        while (rdrIllnessNo.Read())
                        {
                            if (!rdrIllnessNo.IsDBNull(0)) comboIllnessNo.Items.Add(rdrIllnessNo.GetString(0));
                        }
                    }
                    rdrIllnessNo.Close();
                    if (connRN.State != ConnectionState.Closed) connRN.Close();
                    comboIllnessNo.SelectedIndex = 0;

                    String strSqlQueryForIncidentNo = "select [dbo].[tbl_incident].[IncidentNo] from [dbo].[tbl_incident] " +
                                                      "where [dbo].[tbl_incident].[Case_Id] = @CaseNo and " +
                                                      "[dbo].[tbl_incident].[Individual_id] = @IndividualId";

                    SqlCommand cmdQueryForIncidentNo = new SqlCommand(strSqlQueryForIncidentNo, connRN);
                    cmdQueryForIncidentNo.CommandType = CommandType.Text;

                    cmdQueryForIncidentNo.Parameters.AddWithValue("@CaseNo", CaseNoSelected);
                    cmdQueryForIncidentNo.Parameters.AddWithValue("@IndividualId", IndividualId);

                    if (connRN.State != ConnectionState.Closed)
                    {
                        connRN.Close();
                        connRN.Open();
                    }
                    else if (connRN.State == ConnectionState.Closed) connRN.Open();
                    SqlDataReader rdrIncidentNo = cmdQueryForIncidentNo.ExecuteReader();
                    comboIncidentNo.Items.Clear();
                    comboIncidentNo.Items.Add("None");
                    if (rdrIncidentNo.HasRows)
                    {
                        while (rdrIncidentNo.Read())
                        {
                            if (!rdrIncidentNo.IsDBNull(0)) comboIncidentNo.Items.Add(rdrIncidentNo.GetString(0));
                        }
                    }
                    rdrIncidentNo.Close();
                    if (connRN.State != ConnectionState.Closed) connRN.Close();
                    comboIncidentNo.SelectedIndex = 0;
                }
            }
        }

        private void comboIllnessNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboIllnessNo.SelectedItem == null)
            {
                comboIncidentNo.Items.Clear();
            }
            else
            {
                this.comboCaseNo.SelectedIndexChanged -= new System.EventHandler(this.comboCaseNo_SelectedIndexChanged);
                this.comboIllnessNo.SelectedIndexChanged -= new System.EventHandler(this.comboIllnessNo_SelectedIndexChanged);
                this.comboIncidentNo.SelectedIndexChanged -= new System.EventHandler(this.comboIncidentNo_SelectedIndexChanged);

                //String CaseNoSelected = comboCaseNo.SelectedItem.ToString();
                //String IllnessNo = comboIllnessNo.SelectedItem.ToString();

                String CaseNoSelected = "None";
                if (comboCaseNo.SelectedItem != null) CaseNoSelected = comboCaseNo.SelectedItem.ToString();
                String IllnessNo = "None";
                if (comboIllnessNo.SelectedItem != null) IllnessNo = comboIllnessNo.SelectedItem.ToString();

                //if (IllnessNo != String.Empty)
                if (IllnessNo != "None")
                {
                    String strSqlQueryForIllnessId = "select [dbo].[tbl_illness].[Illness_Id] from [dbo].[tbl_illness] where [dbo].[tbl_illness].[IllnessNo] = @IllnessNo";

                    SqlCommand cmdQueryForIllnessId = new SqlCommand(strSqlQueryForIllnessId, connRN);
                    cmdQueryForIllnessId.CommandType = CommandType.Text;

                    cmdQueryForIllnessId.Parameters.AddWithValue("@IllnessNo", IllnessNo);

                    if (connRN.State != ConnectionState.Closed)
                    {
                        connRN.Close();
                        connRN.Open();
                    }
                    else if (connRN.State == ConnectionState.Closed) connRN.Open();
                    Object objIllnessId = cmdQueryForIllnessId.ExecuteScalar();
                    if (connRN.State != ConnectionState.Closed) connRN.Close();

                    int? nIllnessId = null;

                    if (objIllnessId != null)
                    {
                        int nResultIllnessId;
                        if (Int32.TryParse(objIllnessId.ToString(), out nResultIllnessId)) nIllnessId = nResultIllnessId;
                        else
                        {
                            MessageBox.Show("Invalid Illness Id.", "Error");
                            return;
                        }
                    }

                    String strSqlQueryForCaseNo = "select [dbo].[tbl_illness].[Case_Id] from [dbo].[tbl_illness] " +
                                                  "where [dbo].[tbl_illness].[Illness_Id] = @IllnessId and " +
                                                  "[dbo].[tbl_illness].[Individual_Id] = @IndividualId";

                    SqlCommand cmdQueryForCaseNo = new SqlCommand(strSqlQueryForCaseNo, connRN);
                    cmdQueryForCaseNo.CommandType = CommandType.Text;

                    cmdQueryForCaseNo.Parameters.AddWithValue("@IllnessId", nIllnessId);
                    cmdQueryForCaseNo.Parameters.AddWithValue("@IndividualId", IndividualId);

                    if (connRN.State != ConnectionState.Closed)
                    {
                        connRN.Close();
                        connRN.Open();
                    }
                    else if (connRN.State == ConnectionState.Closed) connRN.Open();
                    Object objCaseNo = cmdQueryForCaseNo.ExecuteScalar();
                    if (connRN.State != ConnectionState.Closed) connRN.Close();

                    if (objCaseNo != null)
                    {
                        if (comboCaseNo.Items != null)
                        {
                            for (int i = 0; i < comboCaseNo.Items.Count; i++)
                            {
                                if (comboCaseNo.Items[i].ToString() == objCaseNo.ToString()) comboCaseNo.SelectedIndex = i;
                            }
                        }
                    }

                    //if (CaseNoSelected != String.Empty)
                    if (CaseNoSelected != "None")
                    {
                        String strSqlQueryForIncidentNo = "select [dbo].[tbl_incident].[IncidentNo] from [dbo].[tbl_incident] " +
                                                          "where [dbo].[tbl_incident].[Individual_Id] = @IndividualId and " +
                                                          "[dbo].[tbl_incident].[Case_id] = @CaseNo and " +
                                                          "[dbo].[tbl_incident].[Illness_id] = @IllnessId";

                        SqlCommand cmdQueryForIncidentNo = new SqlCommand(strSqlQueryForIncidentNo, connRN);
                        cmdQueryForIncidentNo.CommandType = CommandType.Text;

                        cmdQueryForIncidentNo.Parameters.AddWithValue("@IndividualId", IndividualId);
                        cmdQueryForIncidentNo.Parameters.AddWithValue("@CaseNo", CaseNoSelected);
                        cmdQueryForIncidentNo.Parameters.AddWithValue("@IllnessId", nIllnessId);

                        if (connRN.State != ConnectionState.Closed)
                        {
                            connRN.Close();
                            connRN.Open();
                        }
                        else if (connRN.State == ConnectionState.Closed) connRN.Open();
                        SqlDataReader rdrIncidentNo = cmdQueryForIncidentNo.ExecuteReader();
                        comboIncidentNo.Items.Clear();
                        comboIncidentNo.Items.Add("None");
                        if (rdrIncidentNo.HasRows)
                        {
                            while (rdrIncidentNo.Read())
                            {
                                if (!rdrIncidentNo.IsDBNull(0)) comboIncidentNo.Items.Add(rdrIncidentNo.GetString(0));
                            }
                        }
                        rdrIncidentNo.Close();
                        if (connRN.State != ConnectionState.Closed) connRN.Close();
                        comboIncidentNo.SelectedIndex = 0;
                    }
                }
                else
                {
                    //String CaseNoSelected = comboCaseNo.SelectedItem.ToString();
                    //String IllnessNo = comboIllnessNo.SelectedItem.ToString();

                    List<int?> lstIllnessId = new List<int?>();

                    String strSqlQueryForIllnessId = "select [dbo].[tbl_illness].[Illness_Id] from [dbo].[tbl_illness] " +
                                                     "where [dbo].[tbl_illness].[Individual_Id] = @IndividualId and " +
                                                     "[dbo].[tbl_illness].[Case_Id] = @CaseNo";

                    SqlCommand cmdQueryForIllnessId = new SqlCommand(strSqlQueryForIllnessId, connRN);
                    cmdQueryForIllnessId.CommandType = CommandType.Text;

                    cmdQueryForIllnessId.Parameters.AddWithValue("@IndividualId", IndividualId);
                    cmdQueryForIllnessId.Parameters.AddWithValue("@CaseNo", CaseNoSelected);

                    if (connRN.State != ConnectionState.Closed)
                    {
                        connRN.Close();
                        connRN.Open();
                    }
                    else if (connRN.State == ConnectionState.Closed) connRN.Open();
                    SqlDataReader rdrIllnessId = cmdQueryForIllnessId.ExecuteReader();
                    if (rdrIllnessId.HasRows)
                    {
                        while (rdrIllnessId.Read())
                        {
                            if (!rdrIllnessId.IsDBNull(0)) lstIllnessId.Add(rdrIllnessId.GetInt32(0));
                            else lstIllnessId.Add(null);
                        }
                    }
                    rdrIllnessId.Close();
                    if (connRN.State != ConnectionState.Closed) connRN.Close();

                    comboIncidentNo.Items.Clear();
                    comboIncidentNo.Items.Add("None");

                    foreach (int? illness_id in lstIllnessId)
                    {
                        String strSqlQueryForIncidentNo = "select [dbo].[tbl_incident].[IncidentNo] from [dbo].[tbl_incident] where [dbo].[tbl_incident].[Illness_id] = @IllnessId";

                        SqlCommand cmdQueryForIncidentNo = new SqlCommand(strSqlQueryForIncidentNo, connRN);
                        cmdQueryForIncidentNo.CommandType = CommandType.Text;

                        cmdQueryForIncidentNo.Parameters.AddWithValue("@IllnessId", illness_id);

                        if (connRN.State != ConnectionState.Closed)
                        {
                            connRN.Close();
                            connRN.Open();
                        }
                        else if (connRN.State == ConnectionState.Closed) connRN.Open();
                        SqlDataReader rdrIncidentNo = cmdQueryForIncidentNo.ExecuteReader();
                        if (rdrIncidentNo.HasRows)
                        {
                            while (rdrIncidentNo.Read())
                            {
                                if (!rdrIncidentNo.IsDBNull(0)) comboIncidentNo.Items.Add(rdrIncidentNo.GetString(0));
                            }
                        }
                        rdrIncidentNo.Close();
                        if (connRN.State != ConnectionState.Closed) connRN.Close();
                    }
                    comboIncidentNo.SelectedIndex = 0;
                }

                this.comboCaseNo.SelectedIndexChanged += new System.EventHandler(this.comboCaseNo_SelectedIndexChanged);
                this.comboIllnessNo.SelectedIndexChanged += new System.EventHandler(this.comboIllnessNo_SelectedIndexChanged);
                this.comboIncidentNo.SelectedIndexChanged += new System.EventHandler(this.comboIncidentNo_SelectedIndexChanged);
            }
        }

        private void comboIncidentNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            String IndividualId = txtCommunicationIndividualId.Text.Trim();

            ComboBox cbIncidentNo = sender as ComboBox;

            if (cbIncidentNo.SelectedItem != null)
            {
                comboCaseNo.SelectedIndexChanged -= comboCaseNo_SelectedIndexChanged;
                comboIllnessNo.SelectedIndexChanged -= comboIllnessNo_SelectedIndexChanged;
                comboIncidentNo.SelectedIndexChanged -= comboIncidentNo_SelectedIndexChanged;

                String IncidentNo = cbIncidentNo.SelectedItem.ToString();

                String CaseNoForIncidentNo = null;
                int? nIllnessIdForIncidentNo = null;

                String strSqlQueryForCaseNoAndIllnessId = "select [dbo].[tbl_incident].[Case_id], [dbo].[tbl_incident].[Illness_id] from [dbo].[tbl_incident] " +
                                                          "where [dbo].[tbl_incident].[Individual_id] = @IndividualId and " +
                                                          "[dbo].[tbl_incident].[IncidentNo] = @IncidentNo";

                SqlCommand cmdQueryForCaseNoAndIllnessId = new SqlCommand(strSqlQueryForCaseNoAndIllnessId, connRN);
                cmdQueryForCaseNoAndIllnessId.CommandType = CommandType.Text;

                cmdQueryForCaseNoAndIllnessId.Parameters.AddWithValue("@IndividualId", IndividualId);
                cmdQueryForCaseNoAndIllnessId.Parameters.AddWithValue("@IncidentNo", IncidentNo);

                if (connRN.State != ConnectionState.Closed)
                {
                    connRN.Close();
                    connRN.Open();
                }
                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                SqlDataReader rdrCaesNoAndIllnessId = cmdQueryForCaseNoAndIllnessId.ExecuteReader();
                if (rdrCaesNoAndIllnessId.HasRows)
                {
                    rdrCaesNoAndIllnessId.Read();
                    if (!rdrCaesNoAndIllnessId.IsDBNull(0)) CaseNoForIncidentNo = rdrCaesNoAndIllnessId.GetString(0);
                    else CaseNoForIncidentNo = null;
                    if (!rdrCaesNoAndIllnessId.IsDBNull(1)) nIllnessIdForIncidentNo = rdrCaesNoAndIllnessId.GetInt32(1);
                    else nIllnessIdForIncidentNo = null;
                }
                if (connRN.State != ConnectionState.Closed) connRN.Close();

                for (int i = 0; i < comboCaseNo.Items.Count; i++)
                {
                    if (comboCaseNo.Items[i] != null && CaseNoForIncidentNo != null)
                    {
                        if (comboCaseNo.Items[i].ToString() == CaseNoForIncidentNo) comboCaseNo.SelectedIndex = i;
                    }
                }

                if (nIllnessIdForIncidentNo != null)
                {
                    String strSqlQueryForIllnessNo = "select [dbo].[tbl_illness].[IllnessNo] from [dbo].[tbl_illness] " +
                                                     "where [dbo].[tbl_illness].[Illness_Id] = @IllnessId and " +
                                                     "[dbo].[tbl_illness].[Individual_Id] = @IndividualId";

                    SqlCommand cmdQueryForIllnessNo = new SqlCommand(strSqlQueryForIllnessNo, connRN);
                    cmdQueryForIllnessNo.CommandType = CommandType.Text;

                    cmdQueryForIllnessNo.Parameters.AddWithValue("@IllnessId", nIllnessIdForIncidentNo.Value);
                    cmdQueryForIllnessNo.Parameters.AddWithValue("@IndividualId", IndividualId);

                    if (connRN.State != ConnectionState.Closed)
                    {
                        connRN.Close();
                        connRN.Open();
                    }
                    else if (connRN.State == ConnectionState.Closed) connRN.Open();
                    Object objIllnessNo = cmdQueryForIllnessNo.ExecuteScalar();
                    if (connRN.State != ConnectionState.Closed) connRN.Close();

                    if (objIllnessNo != null)
                    {
                        for (int i = 0; i < comboIllnessNo.Items.Count; i++)
                        {
                            if (comboIllnessNo.Items[i] != null && objIllnessNo != null) comboIllnessNo.SelectedIndex = i;
                        }
                    }
                }

                comboCaseNo.SelectedIndexChanged += comboCaseNo_SelectedIndexChanged;
                comboIllnessNo.SelectedIndexChanged += comboIllnessNo_SelectedIndexChanged;
                comboIncidentNo.SelectedIndexChanged += comboIncidentNo_SelectedIndexChanged;
            }
        }
    }
}
