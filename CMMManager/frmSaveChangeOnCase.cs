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
    public partial class frmSaveChangeOnCase : Form
    {
        public frmSaveChangeOnCase()
        {
            InitializeComponent();
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;

            return;
        }

        //private void btnNo_Click(object sender, EventArgs e)
        //{
        //    DialogResult = DialogResult.No;

        //    return;
        //}

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;

            return;
        }
    }
}
