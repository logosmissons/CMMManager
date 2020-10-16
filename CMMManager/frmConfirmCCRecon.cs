using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CMMManager
{
    public partial class frmConfirmCCRecon : Form
    {
        private String SettlementNo;
        private Double SettlementAmount;

        public frmConfirmCCRecon()
        {
            InitializeComponent();

            SettlementNo = String.Empty;
            SettlementAmount = 0;
        }

        public frmConfirmCCRecon(String settlement_no, Double settlement_amount)
        {
            InitializeComponent();

            SettlementNo = settlement_no;
            SettlementAmount = settlement_amount;
        }

        private void frmConfirmCCRecon_Load(object sender, EventArgs e)
        {
            txtSettlementNo.Text = SettlementNo;
            txtSettlementAmount.Text = SettlementAmount.ToString("C");
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
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
