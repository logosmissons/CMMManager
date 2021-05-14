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
    public partial class frmShowIllnessReviewNote : Form
    {
        private String IndividualId;
        private String IllnessNo;

        private String connString;
        private SqlConnection connRN;

        public frmShowIllnessReviewNote()
        {
            InitializeComponent();
        }

        public frmShowIllnessReviewNote(String individual_id, String illness_no)
        {
            InitializeComponent();

            connString = @"Data Source=cmm-2019data\CMM; Initial Catalog=RN_DB;Integrated Security=True; Max Pool Size=200; MultipleActiveResultSets=True;";
            connRN = new SqlConnection(connString);

            IndividualId = individual_id;
            IllnessNo = illness_no;
        }

        private void frmShowIllnessReviewNote_Load(object sender, EventArgs e)
        {
            txtIndividualId.Text = IndividualId;
            txtIllnessNo.Text = IllnessNo;

            String strSqlQueryForIllnessReviewNote = "select [dbo].[tbl_illness].[Conclusion] from [dbo].[tbl_illness] " +
                                                     "where [dbo].[tbl_illness].[Individual_Id] = @IndividualId and " +
                                                     "[dbo].[tbl_illness].[IllnessNo] = @IllnessNo";

            SqlCommand cmdQueryForIllnessReviewNote = new SqlCommand(strSqlQueryForIllnessReviewNote, connRN);
            cmdQueryForIllnessReviewNote.CommandType = CommandType.Text;

            cmdQueryForIllnessReviewNote.Parameters.AddWithValue("@IndividualId", IndividualId);
            cmdQueryForIllnessReviewNote.Parameters.AddWithValue("@IllnessNo", IllnessNo);

            if (connRN.State != ConnectionState.Closed)
            {
                connRN.Close();
                connRN.Open();
            }
            else if (connRN.State == ConnectionState.Closed) connRN.Open();
            SqlDataReader rdrIllnessReviewNote = cmdQueryForIllnessReviewNote.ExecuteReader();
            if (rdrIllnessReviewNote.HasRows)
            {
                while (rdrIllnessReviewNote.Read())
                {
                    if (!rdrIllnessReviewNote.IsDBNull(0)) txtIllnessReviewNote.Text = rdrIllnessReviewNote.GetString(0);
                }
            }
            if (connRN.State != ConnectionState.Closed) connRN.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
