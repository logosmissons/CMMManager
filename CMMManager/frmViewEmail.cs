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
    public partial class frmViewEmail : Form
    {
        private String Recipient;
        private String Subject;
        private String Body;

        public frmViewEmail()
        {
            InitializeComponent();
            Recipient = String.Empty;
            Subject = String.Empty;
            Body = String.Empty;
        }

        public frmViewEmail(String recipient, String subject, String body)
        {
            InitializeComponent();
            Recipient = recipient;
            Subject = subject;
            Body = body;
        }

        private void frmViewEmail_Load(object sender, EventArgs e)
        {
            txtRecipient.Text = Recipient;
            txtSubject.Text = Subject;
            txtBody.Text = Body;
        }
    }
}
