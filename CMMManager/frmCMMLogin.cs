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
    public partial class frmCMMLogin : Form
    {
        public frmCMMLogin()
        {
            InitializeComponent();
        }

        private void btnCMMLogin_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            return;
        }
    }
}
