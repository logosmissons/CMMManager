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
    public partial class frmStartEndDate : Form
    {

        public DateTime? StartDate;
        public DateTime? EndDate;

        public frmStartEndDate()
        {
            InitializeComponent();

            StartDate = null;
            EndDate = null;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            StartDate = dtpStartDate.Value;
            EndDate = dtpEndDate.Value;

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
