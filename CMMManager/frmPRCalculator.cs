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
    public partial class frmPRCalculator : Form
    {
        private String IndividualId;

        private String connStringRN;
        private String connStringSalesforce;

        private SqlConnection connRN;
        private SqlConnection connSalesforce;

        public frmPRCalculator()
        {
            InitializeComponent();
        }

        public frmPRCalculator(String individual_id)
        {
            InitializeComponent();

            IndividualId = individual_id;

            connStringRN = @"Data Source=CMM-2014U\CMM; Initial Catalog=RN_DB;Integrated Security=True; Max Pool Size=200; MultipleActiveResultSets=True;";
            connStringSalesforce = @"Data Source=CMM-2014U\CMM; Initial Catalog=SalesForce; Integrated Security=True; MultipleActiveResultSets=True";

            connRN = new SqlConnection(connStringRN);
            connSalesforce = new SqlConnection(connStringSalesforce);
        }

        private void frmPRCalculator_Load(object sender, EventArgs e)
        {
            txtIndividualId.Text = IndividualId;

            String strSqlQueryForIndividualInfo = "select [dbo].[Contact].[Name], [dbo].[Contact].[Membership_IND_Start_date__c] from [dbo].[Contact] " +
                                                  "where [dbo].[Contact].[Individual_ID__c] = @IndividualId";

            SqlCommand cmdQueryForIndividualInfo = new SqlCommand(strSqlQueryForIndividualInfo, connSalesforce);
            cmdQueryForIndividualInfo.CommandType = CommandType.Text;

            cmdQueryForIndividualInfo.Parameters.AddWithValue("@IndividualId", IndividualId);

            if (connSalesforce.State != ConnectionState.Closed)
            {
                connSalesforce.Close();
                connSalesforce.Open();
            }
            else if (connSalesforce.State == ConnectionState.Closed) connSalesforce.Open();
            SqlDataReader rdrIndividualInfo = cmdQueryForIndividualInfo.ExecuteReader();
            if (rdrIndividualInfo.HasRows)
            {
                rdrIndividualInfo.Read();
                if (!rdrIndividualInfo.IsDBNull(0)) txtIndividualName.Text = rdrIndividualInfo.GetString(0);
                if (!rdrIndividualInfo.IsDBNull(1)) txtIndividualStartDate.Text = rdrIndividualInfo.GetDateTime(1).ToString("MM/dd/yyyy");
            }
            rdrIndividualInfo.Close();
            if (connSalesforce.State != ConnectionState.Closed) connSalesforce.Close();

            String strSqlQueryForProgramChange = "select [dbo].[ContactHistory].[CreatedDate], [dbo].[ContactHistory].[OldValue], [dbo].[ContactHistory].[NewValue] " +
                                                 "from [dbo].[ContactHistory] " +
                                                 "inner join [dbo].[Contact] on [dbo].[ContactHistory].[ContactId] = [dbo].[Contact].[Id] " +
                                                 "where [dbo].[Contact].[Individual_ID__c] = @IndividualId";

            SqlCommand cmdQueryForProgramChange = new SqlCommand(strSqlQueryForProgramChange, connSalesforce);
            cmdQueryForProgramChange.CommandType = CommandType.Text;

            cmdQueryForProgramChange.Parameters.AddWithValue("@IndividualId", IndividualId);

            if (connSalesforce.State != ConnectionState.Closed)
            {
                connSalesforce.Close();
                connSalesforce.Open();
            }
            else if (connSalesforce.State == ConnectionState.Closed) connSalesforce.Open();
            SqlDataReader rdrProgramChange = cmdQueryForProgramChange.ExecuteReader();
            if (rdrProgramChange.HasRows)
            {
                while (rdrProgramChange.Read())
                {
                    DataGridViewRow row = new DataGridViewRow();
                    if (!rdrProgramChange.IsDBNull(0)) row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrProgramChange.GetDateTime(0).ToString("MM/dd/yyyy") });
                    else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                    if (!rdrProgramChange.IsDBNull(1)) row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrProgramChange.GetString(1) });
                    else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });
                    if (!rdrProgramChange.IsDBNull(2)) row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrProgramChange.GetString(2) });
                    else row.Cells.Add(new DataGridViewTextBoxCell { Value = String.Empty });

                    gvProgramChangeHistory.Rows.Add(row);
                }
            }
            rdrProgramChange.Close();
            if (connSalesforce.State != ConnectionState.Closed) connSalesforce.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnRunReport_Click(object sender, EventArgs e)
        {

            DateTime dtAnivStartDate = dtpAnivEndDate.Value;
            DateTime dtAnivEndDate = dtpAnivEndDate.Value;

            List<PersonalResponsibilityTotalInfo> lstPersonalResponsibilityInfo = new List<PersonalResponsibilityTotalInfo>();

            String strSqlQueryForPRInfoGoldPlus = "select [dbo].[tbl_case].[Case_Name], [dbo].[tbl_illness].[IllnessNo], [dbo].[tbl_incident].[IncidentNo], " +
                                                   "[dbo].[tbl_medbill].[BillNo], [dbo].[tbl_settlement].[Name], " +
                                                   "[dbo].[tbl_settlement].[PersonalResponsibilityCredit], " +
                                                   "[dbo].[tbl_settlement_type_code].[SettlementTypeValue] " +
                                                   "from [dbo].[tbl_medbill] " +
                                                   "inner join [dbo].[tbl_case] on [dbo].[tbl_medbill].[Case_Id] = [dbo].[tbl_case].[Case_Name] " +
                                                   "inner join [dbo].[tbl_illness] on [dbo].[tbl_medbill].[Illness_Id] = [dbo].[tbl_illness].[Illness_Id] " +
                                                   "inner join [dbo].[tbl_incident] on [dbo].[tbl_medbill].[Incident_Id] = [dbo].[tbl_incident].[Incident_id] " +
                                                   "inner join [dbo].[tbl_program] on [dbo].[tbl_incident].[Program_id] = [dbo].[tbl_program].[Program_Id] " +
                                                   "inner join [dbo].[tbl_settlement] on [dbo].[tbl_medbill].[BillNo] = [dbo].[tbl_settlement].[MedicalBillID] " +
                                                   "inner join [dbo].[tbl_settlement_type_code] on [dbo].[tbl_settlement].[SettlementType] = [dbo].[tbl_settlement_type_code].[SettlementTypeCode] " +
                                                   "where ([dbo].[tbl_illness].[Date_of_Diagnosis] >= @AnivStartDate and [dbo].[tbl_illness].[Date_of_Diagnosis] <= @AnivEndDate) and " +
                                                   "[dbo].[tbl_program].[ProgramName] = 'Gold Plus' and [dbo].[tbl_medbill].[Individual_Id] = @IndividualId and " +
                                                   "([dbo].[tbl_incident].[IsWellBeing] = 0 or [dbo].[tbl_incident].[IsWellBeing] IS NULL) " +
                                                   "order by [dbo].[tbl_illness].[Date_of_Diagnosis]";

            SqlCommand cmdQueryForPRInfoGoldPlus = new SqlCommand(strSqlQueryForPRInfoGoldPlus, connRN);
            cmdQueryForPRInfoGoldPlus.CommandType = CommandType.Text;

            cmdQueryForPRInfoGoldPlus.Parameters.AddWithValue("@AnivStartDate", dtAnivStartDate);
            cmdQueryForPRInfoGoldPlus.Parameters.AddWithValue("@AnivEndDate", dtAnivEndDate);
            cmdQueryForPRInfoGoldPlus.Parameters.AddWithValue("@IndividualId", IndividualId);

            if (connRN.State != ConnectionState.Closed)
            {
                connRN.Close();
                connRN.Open();
            }
            else if (connRN.State == ConnectionState.Closed) connRN.Open();
            SqlDataReader rdrPRInfoGoldPlus = cmdQueryForPRInfoGoldPlus.ExecuteReader();
            if (rdrPRInfoGoldPlus.HasRows)
            {
                while (rdrPRInfoGoldPlus.Read())
                {
                    PersonalResponsibilityTotalInfo info = new PersonalResponsibilityTotalInfo();
                    if (!rdrPRInfoGoldPlus.IsDBNull(0)) info.CaseName = rdrPRInfoGoldPlus.GetString(0);
                    else info.CaseName = null;
                    if (!rdrPRInfoGoldPlus.IsDBNull(1)) info.IllnessNo = rdrPRInfoGoldPlus.GetString(1);
                    else info.IllnessNo = null;
                    if (!rdrPRInfoGoldPlus.IsDBNull(2)) info.IncidentNo = rdrPRInfoGoldPlus.GetString(2);
                    else info.IncidentNo = null;
                    if (!rdrPRInfoGoldPlus.IsDBNull(3)) info.MedBillNo = rdrPRInfoGoldPlus.GetString(3);
                    else info.MedBillNo = null;
                    if (!rdrPRInfoGoldPlus.IsDBNull(4)) info.SettlementNo = rdrPRInfoGoldPlus.GetString(4);
                    else info.SettlementNo = null;
                    if (!rdrPRInfoGoldPlus.IsDBNull(5)) info.PersonalResponsibilityAmount = rdrPRInfoGoldPlus.GetDouble(5);
                    else info.PersonalResponsibilityAmount = 0;
                    if (!rdrPRInfoGoldPlus.IsDBNull(6))
                    {
                        switch (rdrPRInfoGoldPlus.GetString(6).Trim())
                        {
                            case "Third-Party Discount":
                                info.PR_Type = PersonalResponsibilityType.ThirdPartyDiscount;
                                break;
                            case "Member Payment":
                                info.PR_Type = PersonalResponsibilityType.MemberPayment;
                                break;
                            case "Member Discount":
                                info.PR_Type = PersonalResponsibilityType.MemberDiscount;
                                break;
                        }
                    }

                    lstPersonalResponsibilityInfo.Add(info);
                }
            }
            rdrPRInfoGoldPlus.Close();
            if (connRN.State != ConnectionState.Closed) connRN.Close();

            foreach (PersonalResponsibilityTotalInfo info in lstPersonalResponsibilityInfo)
            {
                DataGridViewRow row = new DataGridViewRow();

                row.Cells.Add(new DataGridViewTextBoxCell { Value = info.CaseName });
                row.Cells.Add(new DataGridViewTextBoxCell { Value = info.IllnessNo });
                row.Cells.Add(new DataGridViewTextBoxCell { Value = info.IncidentNo });
                row.Cells.Add(new DataGridViewTextBoxCell { Value = info.MedBillNo });
                row.Cells.Add(new DataGridViewTextBoxCell { Value = info.SettlementNo });
                row.Cells.Add(new DataGridViewTextBoxCell { Value = info.PersonalResponsibilityAmount.ToString("C") });
                row.Cells.Add(new DataGridViewTextBoxCell { Value = info.PR_Type.ToString() });

                gvPersonalResponsibilityReport.Rows.Add(row);
            }
        }
    }
}
