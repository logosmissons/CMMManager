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
    public partial class frmSettlementNote : Form
    {
        public String SettlementNo;
        public String SettlementNote;

        public frmSettlementNote()
        {
            InitializeComponent();
        }

        public frmSettlementNote(String settlement_no, String settlement_note)
        {
            InitializeComponent();
            SettlementNo = settlement_no;
            SettlementNote = settlement_note;
        }

        private void btnSaveSettlementNote_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            SettlementNo = txtSettlementNo.Text.Trim();
            SettlementNote = txtSettlementNote.Text.Trim();
            Close();
            return;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
            return;
        }

        private void frmSettlementNote_Load(object sender, EventArgs e)
        {
            txtSettlementNo.Text = SettlementNo;
            txtSettlementNote.Text = SettlementNote;
        }
    }
}
