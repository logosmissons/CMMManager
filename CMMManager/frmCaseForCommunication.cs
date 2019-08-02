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
    public partial class frmCaseForCommunication : Form
    {
        public Boolean bOnGoing;
        public Boolean bProcessing;
        public Boolean bClosed;

        public String SqlRNConnString;
        public SqlConnection connRN;

        public String IndividualId;
        public String SelectedCaseNo;
        public int SelectedIllnessId;
        public String SelectedIncidentNo;

        private StringBuilder sbSqlQueryForCases;

        private List<String> lstCases;

        public String SelectedCaseName;
        public String SelectedIllnessName;
        public String SelectedIncidentName;

        public frmCaseForCommunication()
        {
            InitializeComponent();

            bOnGoing = false;
            bProcessing = false;
            bClosed = false;

            IndividualId = String.Empty;

            SqlRNConnString = @"Data Source=12.230.174.166\cmm; Initial Catalog=RN_DB; Integrated Security=True; Max Pool Size=200";
            connRN = new SqlConnection(SqlRNConnString);

            sbSqlQueryForCases = new StringBuilder();
            lstCases = new List<string>();

            SelectedCaseName = String.Empty;
            SelectedCaseNo = String.Empty;
            SelectedIncidentNo = String.Empty;

        }

        public frmCaseForCommunication(Boolean ongoing, Boolean processing, Boolean closed, String individual_id)
        {
            InitializeComponent();

            bOnGoing = ongoing;
            bProcessing = processing;
            bClosed = closed;

            IndividualId = individual_id;

            SqlRNConnString = @"Data Source=12.230.174.166\cmm; Initial Catalog=RN_DB; Integrated Security=True; Max Pool Size=200";
            connRN = new SqlConnection(SqlRNConnString);

            chkOnGoing.Checked = bOnGoing;
            chkProcessing.Checked = bProcessing;
            chkClosed.Checked = bClosed;

            sbSqlQueryForCases = new StringBuilder();
            lstCases = new List<string>();

            SelectedCaseName = String.Empty;
            SelectedCaseNo = String.Empty;
            SelectedIncidentNo = String.Empty;

        }

        private void frmCaseForCommunication_Load(object sender, EventArgs e)
        {
            comboCases.Items.Clear();
            String strSqlQueryForCase = "select [dbo].[tbl_case].[Case_Name] from [dbo].[tbl_case] where [dbo].[tbl_case].[individual_id] = @IndividualId";
            sbSqlQueryForCases.Append(strSqlQueryForCase);

            //comboCases.Items.Add("None");

            if (bOnGoing)
            {
                sbSqlQueryForCases.Append(" and [dbo].[tbl_case].[Case_status] = 0");

                SqlCommand cmdQueryForCasesOngoingForIndividual = new SqlCommand(sbSqlQueryForCases.ToString(), connRN);
                cmdQueryForCasesOngoingForIndividual.Parameters.AddWithValue("@IndividualId", IndividualId);

                if (connRN.State != ConnectionState.Closed)
                {
                    connRN.Close();
                    connRN.Open();
                }
                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                SqlDataReader rdrCasesOngoingForIndividual = cmdQueryForCasesOngoingForIndividual.ExecuteReader();
                if (rdrCasesOngoingForIndividual.HasRows)
                {
                    while (rdrCasesOngoingForIndividual.Read())
                    {
                        lstCases.Add(rdrCasesOngoingForIndividual.GetString(0));
                    }
                }
                rdrCasesOngoingForIndividual.Close();
                if (connRN.State != ConnectionState.Closed) connRN.Close();
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////

            sbSqlQueryForCases.Clear();
            sbSqlQueryForCases.Append(strSqlQueryForCase);

            if (bProcessing)
            {
                sbSqlQueryForCases.Append(" and [dbo].[tbl_case].[Case_status] = 1");

                SqlCommand cmdQueryForCasesProcessingForIndividual = new SqlCommand(sbSqlQueryForCases.ToString(), connRN);
                cmdQueryForCasesProcessingForIndividual.Parameters.AddWithValue("@IndividualId", IndividualId);

                if (connRN.State != ConnectionState.Closed)
                {
                    connRN.Close();
                    connRN.Open();
                }
                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                SqlDataReader rdrCasesForProcessingIndividual = cmdQueryForCasesProcessingForIndividual.ExecuteReader();
                if (rdrCasesForProcessingIndividual.HasRows)
                {
                    while (rdrCasesForProcessingIndividual.Read())
                    {
                        lstCases.Add(rdrCasesForProcessingIndividual.GetString(0));
                    }
                }
                rdrCasesForProcessingIndividual.Close();
                if (connRN.State != ConnectionState.Closed) connRN.Close();
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////

            sbSqlQueryForCases.Clear();
            sbSqlQueryForCases.Append(strSqlQueryForCase);

            if (bClosed)
            {
                sbSqlQueryForCases.Append(" and [dbo].[tbl_case].[Case_status] = 2");

                SqlCommand cmdQueryForCasesClosedForIndividual = new SqlCommand(sbSqlQueryForCases.ToString(), connRN);
                cmdQueryForCasesClosedForIndividual.Parameters.AddWithValue("@IndividualId", IndividualId);

                if (connRN.State != ConnectionState.Closed)
                {
                    connRN.Close();
                    connRN.Open();
                }
                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                SqlDataReader rdrCasesClosedForProcessingIndividual = cmdQueryForCasesClosedForIndividual.ExecuteReader();
                if (rdrCasesClosedForProcessingIndividual.HasRows)
                {
                    while (rdrCasesClosedForProcessingIndividual.Read())
                    {
                        lstCases.Add(rdrCasesClosedForProcessingIndividual.GetString(0));
                    }
                }
                rdrCasesClosedForProcessingIndividual.Close();
                if (connRN.State != ConnectionState.Closed) connRN.Close();
            }

            lstCases.Sort();

            comboCases.Items.Add("None");
            foreach (String Case_Name in lstCases)
            {
                comboCases.Items.Add(Case_Name);
            }

            comboCases.SelectedIndex = 0;
        }

        private void chkOnGoing_CheckedChanged(object sender, EventArgs e)
        {
            comboCases.Items.Clear();

            sbSqlQueryForCases = new StringBuilder();
            lstCases = new List<string>();

            String strSqlQueryForCase = "select [dbo].[tbl_case].[Case_Name] from [dbo].[tbl_case] where [dbo].[tbl_case].[individual_id] = @IndividualId";
            sbSqlQueryForCases.Append(strSqlQueryForCase);

            if (chkOnGoing.Checked)
            {
                sbSqlQueryForCases.Append(" and [dbo].[tbl_case].[Case_status] = 0");

                SqlCommand cmdQueryForCasesOngoingForIndividual = new SqlCommand(sbSqlQueryForCases.ToString(), connRN);
                cmdQueryForCasesOngoingForIndividual.Parameters.AddWithValue("@IndividualId", IndividualId);

                if (connRN.State != ConnectionState.Closed)
                {
                    connRN.Close();
                    connRN.Open();
                }
                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                SqlDataReader rdrCasesOngoingForIndividual = cmdQueryForCasesOngoingForIndividual.ExecuteReader();
                if (rdrCasesOngoingForIndividual.HasRows)
                {
                    while (rdrCasesOngoingForIndividual.Read())
                    {
                        lstCases.Add(rdrCasesOngoingForIndividual.GetString(0));
                    }
                }
                rdrCasesOngoingForIndividual.Close();
                if (connRN.State != ConnectionState.Closed) connRN.Close();
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////

            sbSqlQueryForCases.Clear();
            sbSqlQueryForCases.Append(strSqlQueryForCase);

            if (chkProcessing.Checked)
            {
                sbSqlQueryForCases.Append(" and [dbo].[tbl_case].[Case_status] = 1");

                SqlCommand cmdQueryForCasesProcessingForIndividual = new SqlCommand(sbSqlQueryForCases.ToString(), connRN);
                cmdQueryForCasesProcessingForIndividual.Parameters.AddWithValue("@IndividualId", IndividualId);

                if (connRN.State != ConnectionState.Closed)
                {
                    connRN.Close();
                    connRN.Open();
                }
                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                SqlDataReader rdrCasesForProcessingIndividual = cmdQueryForCasesProcessingForIndividual.ExecuteReader();
                if (rdrCasesForProcessingIndividual.HasRows)
                {
                    while (rdrCasesForProcessingIndividual.Read())
                    {
                        lstCases.Add(rdrCasesForProcessingIndividual.GetString(0));
                    }
                }
                rdrCasesForProcessingIndividual.Close();
                if (connRN.State != ConnectionState.Closed) connRN.Close();
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////

            sbSqlQueryForCases.Clear();
            sbSqlQueryForCases.Append(strSqlQueryForCase);

            if (chkClosed.Checked)
            {
                sbSqlQueryForCases.Append(" and [dbo].[tbl_case].[Case_status] = 2");

                SqlCommand cmdQueryForCasesClosedForIndividual = new SqlCommand(sbSqlQueryForCases.ToString(), connRN);
                cmdQueryForCasesClosedForIndividual.Parameters.AddWithValue("@IndividualId", IndividualId);

                if (connRN.State != ConnectionState.Closed)
                {
                    connRN.Close();
                    connRN.Open();
                }
                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                SqlDataReader rdrCasesClosedForProcessingIndividual = cmdQueryForCasesClosedForIndividual.ExecuteReader();
                if (rdrCasesClosedForProcessingIndividual.HasRows)
                {
                    while (rdrCasesClosedForProcessingIndividual.Read())
                    {
                        lstCases.Add(rdrCasesClosedForProcessingIndividual.GetString(0));
                    }
                }
                rdrCasesClosedForProcessingIndividual.Close();
                if (connRN.State != ConnectionState.Closed) connRN.Close();
            }

            lstCases.Sort();

            comboCases.Items.Add("None");
            foreach (String Case_Name in lstCases)
            {
                comboCases.Items.Add(Case_Name);
            }
            comboCases.SelectedItem = "None";
        }

        private void chkProcessing_CheckedChanged(object sender, EventArgs e)
        {
            comboCases.Items.Clear();
            sbSqlQueryForCases = new StringBuilder();
            lstCases = new List<string>();

            String strSqlQueryForCase = "select [dbo].[tbl_case].[Case_Name] from [dbo].[tbl_case] where [dbo].[tbl_case].[individual_id] = @IndividualId";
            sbSqlQueryForCases.Append(strSqlQueryForCase);

            if (chkOnGoing.Checked)
            {
                sbSqlQueryForCases.Append(" and [dbo].[tbl_case].[Case_status] = 0");

                SqlCommand cmdQueryForCasesOngoingForIndividual = new SqlCommand(sbSqlQueryForCases.ToString(), connRN);
                cmdQueryForCasesOngoingForIndividual.Parameters.AddWithValue("@IndividualId", IndividualId);

                if (connRN.State != ConnectionState.Closed)
                {
                    connRN.Close();
                    connRN.Open();
                }
                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                SqlDataReader rdrCasesOngoingForIndividual = cmdQueryForCasesOngoingForIndividual.ExecuteReader();
                if (rdrCasesOngoingForIndividual.HasRows)
                {
                    while (rdrCasesOngoingForIndividual.Read())
                    {
                        lstCases.Add(rdrCasesOngoingForIndividual.GetString(0));
                    }
                }
                rdrCasesOngoingForIndividual.Close();
                if (connRN.State != ConnectionState.Closed) connRN.Close();
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////

            sbSqlQueryForCases.Clear();
            sbSqlQueryForCases.Append(strSqlQueryForCase);

            if (chkProcessing.Checked)
            {
                sbSqlQueryForCases.Append(" and [dbo].[tbl_case].[Case_status] = 1");

                SqlCommand cmdQueryForCasesProcessingForIndividual = new SqlCommand(sbSqlQueryForCases.ToString(), connRN);
                cmdQueryForCasesProcessingForIndividual.Parameters.AddWithValue("@IndividualId", IndividualId);

                if (connRN.State != ConnectionState.Closed)
                {
                    connRN.Close();
                    connRN.Open();
                }
                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                SqlDataReader rdrCasesForProcessingIndividual = cmdQueryForCasesProcessingForIndividual.ExecuteReader();
                if (rdrCasesForProcessingIndividual.HasRows)
                {
                    while (rdrCasesForProcessingIndividual.Read())
                    {
                        lstCases.Add(rdrCasesForProcessingIndividual.GetString(0));
                    }
                }
                rdrCasesForProcessingIndividual.Close();
                if (connRN.State != ConnectionState.Closed) connRN.Close();
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////

            sbSqlQueryForCases.Clear();
            sbSqlQueryForCases.Append(strSqlQueryForCase);

            if (chkClosed.Checked)
            {
                sbSqlQueryForCases.Append(" and [dbo].[tbl_case].[Case_status] = 2");

                SqlCommand cmdQueryForCasesClosedForIndividual = new SqlCommand(sbSqlQueryForCases.ToString(), connRN);
                cmdQueryForCasesClosedForIndividual.Parameters.AddWithValue("@IndividualId", IndividualId);

                if (connRN.State != ConnectionState.Closed)
                {
                    connRN.Close();
                    connRN.Open();
                }
                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                SqlDataReader rdrCasesClosedForProcessingIndividual = cmdQueryForCasesClosedForIndividual.ExecuteReader();
                if (rdrCasesClosedForProcessingIndividual.HasRows)
                {
                    while (rdrCasesClosedForProcessingIndividual.Read())
                    {
                        lstCases.Add(rdrCasesClosedForProcessingIndividual.GetString(0));
                    }
                }
                rdrCasesClosedForProcessingIndividual.Close();
                if (connRN.State != ConnectionState.Closed) connRN.Close();
            }

            lstCases.Sort();

            comboCases.Items.Add("None");
            foreach (String Case_Name in lstCases)
            {
                comboCases.Items.Add(Case_Name);
            }
        }

        private void chkClosed_CheckedChanged(object sender, EventArgs e)
        {
            comboCases.Items.Clear();
            sbSqlQueryForCases = new StringBuilder();
            lstCases = new List<string>();

            String strSqlQueryForCase = "select [dbo].[tbl_case].[Case_Name] from [dbo].[tbl_case] where [dbo].[tbl_case].[individual_id] = @IndividualId";
            sbSqlQueryForCases.Append(strSqlQueryForCase);

            if (chkOnGoing.Checked)
            {
                sbSqlQueryForCases.Append(" and [dbo].[tbl_case].[Case_status] = 0");

                SqlCommand cmdQueryForCasesOngoingForIndividual = new SqlCommand(sbSqlQueryForCases.ToString(), connRN);
                cmdQueryForCasesOngoingForIndividual.Parameters.AddWithValue("@IndividualId", IndividualId);

                if (connRN.State != ConnectionState.Closed)
                {
                    connRN.Close();
                    connRN.Open();
                }
                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                SqlDataReader rdrCasesOngoingForIndividual = cmdQueryForCasesOngoingForIndividual.ExecuteReader();
                if (rdrCasesOngoingForIndividual.HasRows)
                {
                    while (rdrCasesOngoingForIndividual.Read())
                    {
                        lstCases.Add(rdrCasesOngoingForIndividual.GetString(0));
                    }
                }
                rdrCasesOngoingForIndividual.Close();
                if (connRN.State != ConnectionState.Closed) connRN.Close();
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////

            sbSqlQueryForCases.Clear();
            sbSqlQueryForCases.Append(strSqlQueryForCase);

            if (chkProcessing.Checked)
            {
                sbSqlQueryForCases.Append(" and [dbo].[tbl_case].[Case_status] = 1");

                SqlCommand cmdQueryForCasesProcessingForIndividual = new SqlCommand(sbSqlQueryForCases.ToString(), connRN);
                cmdQueryForCasesProcessingForIndividual.Parameters.AddWithValue("@IndividualId", IndividualId);

                if (connRN.State != ConnectionState.Closed)
                {
                    connRN.Close();
                    connRN.Open();
                }
                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                SqlDataReader rdrCasesForProcessingIndividual = cmdQueryForCasesProcessingForIndividual.ExecuteReader();
                if (rdrCasesForProcessingIndividual.HasRows)
                {
                    while (rdrCasesForProcessingIndividual.Read())
                    {
                        lstCases.Add(rdrCasesForProcessingIndividual.GetString(0));
                    }
                }
                rdrCasesForProcessingIndividual.Close();
                if (connRN.State != ConnectionState.Closed) connRN.Close();
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////

            sbSqlQueryForCases.Clear();
            sbSqlQueryForCases.Append(strSqlQueryForCase);

            if (chkClosed.Checked)
            {
                sbSqlQueryForCases.Append(" and [dbo].[tbl_case].[Case_status] = 2");

                SqlCommand cmdQueryForCasesClosedForIndividual = new SqlCommand(sbSqlQueryForCases.ToString(), connRN);
                cmdQueryForCasesClosedForIndividual.Parameters.AddWithValue("@IndividualId", IndividualId);

                if (connRN.State != ConnectionState.Closed)
                {
                    connRN.Close();
                    connRN.Open();
                }
                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                SqlDataReader rdrCasesClosedForProcessingIndividual = cmdQueryForCasesClosedForIndividual.ExecuteReader();
                if (rdrCasesClosedForProcessingIndividual.HasRows)
                {
                    while (rdrCasesClosedForProcessingIndividual.Read())
                    {
                        lstCases.Add(rdrCasesClosedForProcessingIndividual.GetString(0));
                    }
                }
                rdrCasesClosedForProcessingIndividual.Close();
                if (connRN.State != ConnectionState.Closed) connRN.Close();
            }

            lstCases.Sort();

            comboCases.Items.Add("None");
            foreach (String Case_Name in lstCases)
            {
                comboCases.Items.Add(Case_Name);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (comboCases.SelectedItem != null) SelectedCaseName = comboCases.SelectedItem.ToString();
            else SelectedCaseName = String.Empty;
            if (comboIllnessNo.SelectedItem != null) SelectedIllnessName = comboIllnessNo.SelectedItem.ToString();
            else SelectedIllnessName = String.Empty;
            if (comboIncidentNo.SelectedItem != null) SelectedIncidentName = comboIncidentNo.SelectedItem.ToString();
            else SelectedIncidentName = String.Empty;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void comboCases_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboCases.SelectedItem != null)
            {
                if (comboCases.SelectedItem.ToString() != "None")
                {

                    comboIllnessNo.Items.Add("None");

                    SelectedCaseNo = comboCases.SelectedItem.ToString();

                    String strSqlQueryForIllnessNo = "select [dbo].[tbl_illness].[IllnessNo] from [dbo].[tbl_illness] " +
                                                   "where [dbo].[tbl_illness].[Individual_Id] = @IndividualId and [dbo].[tbl_illness].[Case_Id] = @CaseNo";

                    SqlCommand cmdQueryForIllness = new SqlCommand(strSqlQueryForIllnessNo, connRN);
                    cmdQueryForIllness.CommandType = CommandType.Text;

                    cmdQueryForIllness.Parameters.AddWithValue("@IndividualId", IndividualId);
                    cmdQueryForIllness.Parameters.AddWithValue("@CaseNo", SelectedCaseNo);

                    if (connRN.State != ConnectionState.Closed)
                    {
                        connRN.Close();
                        connRN.Open();
                    }
                    else if (connRN.State == ConnectionState.Closed) connRN.Open();
                    SqlDataReader rdrIllnessNo = cmdQueryForIllness.ExecuteReader();
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
                }
            }
        }

        private void comboIllnessNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboIllnessNo.SelectedItem != null)
            {
                if (comboIllnessNo.SelectedItem.ToString() != "None")
                {
                    comboIncidentNo.Items.Add("None");

                    String SelectedIllnessNo = comboIllnessNo.SelectedItem.ToString();

                    String strSqlQueryForIllnessId = "select [dbo].[tbl_illness].[Illness_Id] from [dbo].[tbl_illness] where [dbo].[tbl_illness].[IllnessNo] = @IllnessNo";

                    SqlCommand cmdQueryForIllnessId = new SqlCommand(strSqlQueryForIllnessId, connRN);
                    cmdQueryForIllnessId.CommandType = CommandType.Text;

                    cmdQueryForIllnessId.Parameters.AddWithValue("@IllnessNo", SelectedIllnessNo);

                    if (connRN.State != ConnectionState.Closed)
                    {
                        connRN.Close();
                        connRN.Open();
                    }
                    else if (connRN.State == ConnectionState.Closed) connRN.Open();
                    Object objIllnessId = cmdQueryForIllnessId.ExecuteScalar();
                    if (connRN.State != ConnectionState.Closed) connRN.Close();

                    int? nIllnessId = null;
                    if (objIllnessId != null) nIllnessId = Int32.Parse(objIllnessId.ToString());

                    String strSqlQueryForIncidentNo = "select [dbo].[tbl_incident].[IncidentNo] from [dbo].[tbl_incident] " +
                                                        "where [dbo].[tbl_incident].[Individual_id] = @IndividualId and " +
                                                        "[dbo].[tbl_incident].[Case_id] = @CaseNo and " +
                                                        "[dbo].[tbl_incident].[Illness_id] = @IllnessId";

                    SqlCommand cmdQueryForIncidentNo = new SqlCommand(strSqlQueryForIncidentNo, connRN);
                    cmdQueryForIncidentNo.CommandType = CommandType.Text;

                    cmdQueryForIncidentNo.Parameters.AddWithValue("@IndividualId", IndividualId);
                    cmdQueryForIncidentNo.Parameters.AddWithValue("@CaseNo", SelectedCaseNo);
                    cmdQueryForIncidentNo.Parameters.AddWithValue("@IllnessId", nIllnessId.Value);

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
                            else comboIncidentNo.Items.Add(String.Empty);
                        }
                    }
                    rdrIncidentNo.Close();
                    if (connRN.State != ConnectionState.Closed) connRN.Close();

                    comboIncidentNo.SelectedIndex = 0;
                }
            }
        }
    }
}
