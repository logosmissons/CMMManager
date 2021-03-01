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
    public partial class frmAddEmailCc : Form
    {
        public String IndividualId;

        public String EmailCc;

        private String connStringRN;
        private String connStringSalesForce;

        private SqlConnection connRN;
        private SqlConnection connSalesForce;
        

        public frmAddEmailCc()
        {
            InitializeComponent();

            connStringRN = @"Data Source=cmm-2019data\CMM; Initial Catalog=RN_DB;Integrated Security=True; Max Pool Size=200; MultipleActiveResultSets=True";
            connStringSalesForce = @"Data Source=cmm-2019data\CMM; Initial Catalog=SalesForce; Integrated Security=True; MultipleActiveResultSets=True";

            connRN = new SqlConnection(connStringRN);
            connSalesForce = new SqlConnection(connStringSalesForce);
        }

        public frmAddEmailCc(String individual_id)
        {
            InitializeComponent();

            connStringRN = @"Data Source=cmm-2019data\CMM; Initial Catalog=RN_DB;Integrated Security=True; Max Pool Size=200; MultipleActiveResultSets=True";
            connStringSalesForce = @"Data Source=cmm-2019data\CMM; Initial Catalog=SalesForce; Integrated Security=True; MultipleActiveResultSets=True";

            connRN = new SqlConnection(connStringRN);
            connSalesForce = new SqlConnection(connStringSalesForce);

            IndividualId = individual_id;
        }

        private void btnOkCc_Click(object sender, EventArgs e)
        {
            EmailCc = String.Empty;

            if (lbEmailCc.Items.Count > 0)
            {
                foreach (String email in lbEmailCc.Items)
                {
                    EmailCc += email + "; ";
                }
            }

            DialogResult = DialogResult.OK;
        }

        private void frmAddEmailCc_Load(object sender, EventArgs e)
        {
            String strSqlQueryForAccountNoForIndividualId = "select [dbo].[Contact].[AccountId] from [dbo].[Contact] " +
                                                            "where [dbo].[Contact].[Individual_ID__c] = @IndividualId";

            SqlCommand cmdQueryForAccountNoIndividualId = new SqlCommand(strSqlQueryForAccountNoForIndividualId, connSalesForce);
            cmdQueryForAccountNoIndividualId.CommandType = CommandType.Text;

            cmdQueryForAccountNoIndividualId.Parameters.AddWithValue("@IndividualId", IndividualId);

            if (connSalesForce.State != ConnectionState.Closed)
            {
                connSalesForce.Close();
                connSalesForce.Open();
            }
            else if (connSalesForce.State == ConnectionState.Closed) connSalesForce.Open();
            Object objAccountNoForIndividualId = cmdQueryForAccountNoIndividualId.ExecuteScalar();
            if (connSalesForce.State != ConnectionState.Closed) connSalesForce.Close();

            if (objAccountNoForIndividualId != null)
            {
                String strSqlQueryForFamilyEmailListForAccountNo = "select [dbo].[Contact].[Email] from [dbo].[Contact] " +
                                                                   "where [dbo].[Contact].[AccountId] = @AccountNo";

                SqlCommand cmdQueryForFamilyEmailListForAccountNo = new SqlCommand(strSqlQueryForFamilyEmailListForAccountNo, connSalesForce);
                cmdQueryForFamilyEmailListForAccountNo.CommandType = CommandType.Text;

                cmdQueryForFamilyEmailListForAccountNo.Parameters.AddWithValue("@AccountNo", objAccountNoForIndividualId.ToString());

                if (connSalesForce.State != ConnectionState.Closed)
                {
                    connSalesForce.Close();
                    connSalesForce.Open();
                }
                else if (connSalesForce.State == ConnectionState.Closed) connSalesForce.Open();
                SqlDataReader rdrFamilyEmailList = cmdQueryForFamilyEmailListForAccountNo.ExecuteReader();
                if (rdrFamilyEmailList.HasRows)
                {
                    tvFamilyEmail.Nodes.Add("Member's Family Email");
                    while (rdrFamilyEmailList.Read())
                    {
                        if (!rdrFamilyEmailList.IsDBNull(0)) tvFamilyEmail.Nodes[0].Nodes.Add(rdrFamilyEmailList.GetString(0));
                    }
                }
                rdrFamilyEmailList.Close();
                if (connSalesForce.State != ConnectionState.Closed) connSalesForce.Close();
            }
        }

        private void tvFamilyEmail_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            lbEmailCc.Items.Add(e.Node.Text.Trim());
        }

        private void btnAddEmailToCc_Click(object sender, EventArgs e)
        {
            if (tvFamilyEmail.SelectedNode != null) lbEmailCc.Items.Add(tvFamilyEmail.SelectedNode.Text.Trim());
        }

        private void btnRemoveEmailFromCc_Click(object sender, EventArgs e)
        {
            if (lbEmailCc.SelectedIndex != -1) lbEmailCc.Items.RemoveAt(lbEmailCc.SelectedIndex);
        }

        private void btnCancelCc_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
