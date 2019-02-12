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
    public partial class frmConfirmCheckPayment : Form
    {
        public DateTime CheckDate;
        public int CheckNumber;

        public frmConfirmCheckPayment()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            CheckDate = dtpCheckDate.Value;
            int nCheckNumberResult = 0;

            if (Int32.TryParse(txtStartingCheckNo.Text.Trim(), out nCheckNumberResult))
            {
                CheckNumber = nCheckNumberResult;
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("The check number you entered is invalid.", "Error");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
