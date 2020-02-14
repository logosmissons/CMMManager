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
    public partial class frmMedBillsForCase : Form
    {

        private SqlConnection connRN;
        private String rn_cnn_str;

        public String CaseId;
        public String IndividualId;
        public String MedBillNo;

        public frmMedBillsForCase()
        {
            InitializeComponent();

            rn_cnn_str = @"Data Source=10.1.10.60\CMM; Initial Catalog=RN_DB;Integrated Security=True; Max Pool Size=200; MultipleActiveResultSets=True";
            //rn_cnn_str = @"Data Source=10.1.10.60\CMM; Initial Catalog=RN_DB;User ID=sa;Password=Yny00516; Max Pool Size=200; MultipleActiveResultSets=True";
            connRN = new SqlConnection(rn_cnn_str);

        }

        private void frmMedBillsForCase_Load(object sender, EventArgs e)
        {
            txtCaseNo.Text = CaseId;

            String strSqlQueryForMedBills = "select BillNo, BillDate, MedBillType_Id, Illness_Id, BillAmount, SettlementTotal, TotalSharedAmount from dbo.tbl_medbill " +
                                            "where dbo.tbl_medbill.Case_Id = @CaseID and dbo.tbl_medbill.Individual_Id = @IndividualId";

            SqlCommand cmdQueryForMedBills = new SqlCommand(strSqlQueryForMedBills, connRN);
            cmdQueryForMedBills.CommandType = CommandType.Text;

            cmdQueryForMedBills.Parameters.AddWithValue("@CaseId", CaseId);
            cmdQueryForMedBills.Parameters.AddWithValue("@IndividualId", IndividualId);

            connRN.Open();

            SqlDataReader rdrMedBillsForCase = cmdQueryForMedBills.ExecuteReader();
            if (rdrMedBillsForCase.HasRows)
            {
                while (rdrMedBillsForCase.Read())
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.Cells.Add(new DataGridViewCheckBoxCell { Value = false });
                    row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrMedBillsForCase.GetString(0) });
                    row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrMedBillsForCase.GetDateTime(1) });
                    row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrMedBillsForCase.GetSqlByte(2) });
                    row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrMedBillsForCase.GetString(3) });
                    row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrMedBillsForCase.GetDecimal(4).ToString("C") });
                    row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrMedBillsForCase.GetDecimal(5).ToString("C") });
                    row.Cells.Add(new DataGridViewTextBoxCell { Value = rdrMedBillsForCase.GetDecimal(6).ToString("C") });

                    gvMedBillsForCase.Rows.Add(row);
                }
            }
            rdrMedBillsForCase.Close();
            connRN.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            //DialogResult = DialogResult.Cancel;

            Close();
        }

        private void gvMedBillsForCase_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //String strMedBillNo;
            //String strIndividualId;

            DataGridView gvMedBills = sender as DataGridView;
            int nRowSelected = e.RowIndex;
            MedBillNo = gvMedBills["BillNo", nRowSelected].Value.ToString();
            DialogResult = DialogResult.OK;

            return;
        }
    }
}
