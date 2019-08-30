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
    public partial class frmConfirmACHPayment : Form
    {

        public DateTime ACH_Date;
        public String ACH_Number;

        public frmConfirmACHPayment()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (dtpACHDate.Value != null) ACH_Date = dtpACHDate.Value;
            if (txtACHNo.Text != String.Empty) ACH_Number = txtACHNo.Text.Trim();
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void frmConfirmACHPayment_Load(object sender, EventArgs e)
        {

        }
    }
}
