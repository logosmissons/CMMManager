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
    public partial class frmConfirmCCPaymentDlg : Form
    {

        private String IndividualId;
        private String IndividualName;
        private String MedicalProviderName;
        private String MedicalBillNo;
        private String SettlementNo;
        private String CreditCardNo;
        private Decimal MedBillAmount;
        private Decimal SettlementAmount;
        private Decimal PaymentAmount;
        private Boolean bNeedsReview;
        private String Comment;

        public String Note
        {
            get { return Comment; }
            set { Comment = value; }
        }

        public frmConfirmCCPaymentDlg()
        {
            InitializeComponent();
        }

        public frmConfirmCCPaymentDlg(String individual_id,
                            String individual_name,
                            String medical_provider,
                            String med_bill_no,
                            String settlement_no,
                            String credit_card_no,
                            Decimal med_bill_amount,
                            Decimal settlement_amount,
                            Decimal payment_amount,
                            String note)
        {
            InitializeComponent();
            IndividualId = individual_id;
            IndividualName = individual_name;
            MedicalProviderName = medical_provider;
            MedicalBillNo = med_bill_no;
            SettlementNo = settlement_no;
            CreditCardNo = credit_card_no;
            MedBillAmount = med_bill_amount;
            SettlementAmount = settlement_amount;
            PaymentAmount = payment_amount;
            Comment = note;
        }

        public frmConfirmCCPaymentDlg(String individual_id, 
                                    String individual_name, 
                                    String medical_provider,
                                    String med_bill_no, 
                                    String settlement_no, 
                                    String credit_card_no, 
                                    Decimal med_bill_amount,
                                    Decimal settlement_amount,
                                    Decimal payment_amount,
                                    Boolean needs_review)
        {
            InitializeComponent();
            IndividualId = individual_id;
            IndividualName = individual_name;
            MedicalProviderName = medical_provider;
            MedicalBillNo = med_bill_no;
            SettlementNo = settlement_no;
            CreditCardNo = credit_card_no;
            MedBillAmount = med_bill_amount;
            SettlementAmount = settlement_amount;
            PaymentAmount = payment_amount;
            bNeedsReview = needs_review;
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            Note = txtSettlementNote.Text.Trim();
            DialogResult = DialogResult.Yes;
            return;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            return;
        }

        private void frmConfirmPaymentDlg_Load(object sender, EventArgs e)
        {
            txtIndividualId.Text = IndividualId;
            txtIndividualName.Text = IndividualName;
            txtMedicalBillNo.Text = MedicalBillNo;
            txtSettlementNo.Text = SettlementNo;
            txtMedicalProviderName.Text = MedicalProviderName;
            txtCreditCardNo.Text = CreditCardNo;
            txtMedicalBillAmount.Text = MedBillAmount.ToString("C");
            txtSettlementAmount.Text = SettlementAmount.ToString("C");
            txtPaymentAmount.Text = PaymentAmount.ToString("C");
            txtSettlementNote.Text = Comment;
        }
    }
}
