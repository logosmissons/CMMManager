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
        private StringBuilder sbSqlQueryForCases;

        private List<String> lstCases;

        public String SelectedCaseName;

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
            SelectedCaseName = comboCases.SelectedItem.ToString();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
