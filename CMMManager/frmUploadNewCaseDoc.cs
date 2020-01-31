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
using System.IO;
using System.Diagnostics;

namespace CMMManager
{
    public partial class frmUploadNewCaseDoc : Form
    {
        private String IndividualId;
        private String IndividualName;
        private String CaseNo;
        private CaseDocType CaseDocType;
        private Boolean bAddOn;
        private String FullDocNo;
        private DateTime FullDocReceivedDate;
        private DateTime CaseDocReceivedDate;
        private String CaseDocNote;
        private String CaseDocDestinationPath;
        private String CaseDocDestinationFolder;

        // Logged In User Id
        private int nLoggedInUserId;

        // Variables for RN database connection
        private String connStringRN = @"Data Source=CMM-2014U\CMM; Initial Catalog=RN_DB;Integrated Security=True; Max Pool Size=200; MultipleActiveResultSets=True";
        private SqlConnection connRN;

        // Case Document Destination folder
        private String strCaseDocDestinationPath = @"\\cmm-2014u\Sharefolder\FormsInCase\";

        public frmUploadNewCaseDoc()
        {
            InitializeComponent();
        }

        public frmUploadNewCaseDoc(String individual_id, 
                                   String individual_name, 
                                   String case_no, 
                                   Boolean add_on, 
                                   CaseDocType doc_type,
                                   int login_user,
                                   String full_doc_no,
                                   DateTime full_doc_received_date)
        {
            InitializeComponent();

            IndividualId = individual_id;
            IndividualName = individual_name;
            CaseNo = case_no;
            bAddOn = add_on;
            CaseDocType = doc_type;
            nLoggedInUserId = login_user;
            FullDocNo = full_doc_no;
            FullDocReceivedDate = full_doc_received_date;

            connRN = new SqlConnection(connStringRN);
        }

        private void frmUploadNewCaseDoc_Load(object sender, EventArgs e)
        {
            txtIndividualId.Text = IndividualId;
            txtIndividualName.Text = IndividualName;
            txtCaseNo.Text = CaseNo;
            chkAddOn.Checked = bAddOn;
            //comboDocumentType.Items.Add(CaseDocType.NPF.ToString());
            //comboDocumentType.SelectedIndex = 0;
            comboDocumentType.Items.Add(CaseDocType.NPF.ToString());
            comboDocumentType.Items.Add(CaseDocType.IB.ToString());
            comboDocumentType.Items.Add(CaseDocType.PoP.ToString());
            comboDocumentType.Items.Add(CaseDocType.MedRec.ToString());
            comboDocumentType.Items.Add(CaseDocType.OtherDoc.ToString());
            comboDocumentType.Items.Add(CaseDocType.FullDoc.ToString());
            comboDocumentType.Items.Add(CaseDocType.IB_POP.ToString());

            comboDocumentType.SelectedIndex = (int)CaseDocType;
            comboDocumentType.Enabled = false;

            String NewCaseDocNo = String.Empty;

            String strSqlQueryForLastCaseDocId = "select [dbo].[tbl_LastID].[CaseDocId] from [dbo].[tbl_LastID] where [dbo].[tbl_LastID].[Id] = 1";

            SqlCommand cmdQueryForLastCaseDocId = new SqlCommand(strSqlQueryForLastCaseDocId, connRN);
            cmdQueryForLastCaseDocId.CommandType = CommandType.Text;

            if (connRN.State != ConnectionState.Closed)
            {
                connRN.Close();
                connRN.Open();
            }
            else if (connRN.State == ConnectionState.Closed) connRN.Open();
            Object objCaseDocId = cmdQueryForLastCaseDocId.ExecuteScalar();
            if (connRN.State != ConnectionState.Closed) connRN.Close();

            if (objCaseDocId != null) NewCaseDocNo = objCaseDocId.ToString();

            if (NewCaseDocNo == String.Empty)
            {
                NewCaseDocNo = "CASEDOC-1";

                String strSqlUpdateFirstCaseDocId = "update [dbo].[tbl_LastID] set [dbo].[tbl_LastID].[CaseDocId] = @CaseDocId where [dbo].[tbl_LastID].[Id] = 1";

                SqlCommand cmdUpdateFirstCaseDocId = new SqlCommand(strSqlUpdateFirstCaseDocId, connRN);
                cmdUpdateFirstCaseDocId.CommandType = CommandType.Text;

                cmdUpdateFirstCaseDocId.Parameters.AddWithValue("@CaseDocId", NewCaseDocNo);

                if (connRN.State != ConnectionState.Closed)
                {
                    connRN.Close();
                    connRN.Open();
                }
                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                int nFirstDocIdUpdated = cmdUpdateFirstCaseDocId.ExecuteNonQuery();
                if (connRN.State != ConnectionState.Closed) connRN.Close();
            }
            else
            {
                String LastCaseDocNo = NewCaseDocNo;
                String LastCaseDocNumberPart = LastCaseDocNo.Substring(8);
                int nLastCaseDocNo = Int32.Parse(LastCaseDocNumberPart);
                nLastCaseDocNo++;
                NewCaseDocNo = "CASEDOC-" + nLastCaseDocNo.ToString();

                String strSqlUpdateLastCaseDocId = "update [dbo].[tbl_LastID] set [dbo].[tbl_LastID].[CaseDocId] = @NewCaseDocId where [dbo].[tbl_LastID].[Id] = 1";

                SqlCommand cmdUpdateLastCaseDocId = new SqlCommand(strSqlUpdateLastCaseDocId, connRN);
                cmdUpdateLastCaseDocId.CommandType = CommandType.Text;

                cmdUpdateLastCaseDocId.Parameters.AddWithValue("@NewCaseDocId", NewCaseDocNo);

                if (connRN.State != ConnectionState.Closed)
                {
                    connRN.Close();
                    connRN.Open();
                }
                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                int nCaseDocIdUpdated = cmdUpdateLastCaseDocId.ExecuteNonQuery();
                if (connRN.State != ConnectionState.Closed) connRN.Close();
            }

            txtCaseDocNo.Text = NewCaseDocNo.Trim();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            Boolean bCaseDocInserted = true;

            if ((FullDocReceivedDate.ToString("MM/dd/yyyy") != dtpCaseDocReceivedDate.Value.ToString("MM/dd/yyyy")) && (txtCaseDocDestinationPath.Text.Trim() == String.Empty))
            {
                MessageBox.Show("If you do not want to upload any document, the Received Date has to be same as Full Doc Received Date.", "Alert");
                return;
            }

            String strSqlQueryForCaseDocNo = "select [dbo].[tbl_case_doc].[CaseDocNo] from [dbo].[tbl_case_doc] where [dbo].[tbl_case_doc].[CaseDocNo] = @NewCaseDocNo";

            SqlCommand cmdQueryForCaseDocNo = new SqlCommand(strSqlQueryForCaseDocNo, connRN);
            cmdQueryForCaseDocNo.CommandType = CommandType.Text;

            cmdQueryForCaseDocNo.Parameters.AddWithValue("@NewCaseDocNo", txtCaseDocNo.Text.Trim());

            if (connRN.State != ConnectionState.Closed)
            {
                connRN.Close();
                connRN.Open();
            }
            else if (connRN.State == ConnectionState.Closed) connRN.Open();
            Object objCaseDocNo = cmdQueryForCaseDocNo.ExecuteScalar();
            if (connRN.State != ConnectionState.Closed) connRN.Close();

            if (objCaseDocNo == null) // save new case document
            {
                String strSqlInsertNewCaseDocument = "insert into [dbo].[tbl_case_doc] ([dbo].[tbl_case_doc].[IsDeleted], " +
                                                        "[dbo].[tbl_case_doc].[CaseDocNo], [dbo].[tbl_case_doc].[Case_Name], [dbo].[tbl_case_doc].[DocumentTypeId], " +
                                                        "[dbo].[tbl_case_doc].[ReceivedDate], [dbo].[tbl_case_doc].[DestinationFilePath], [dbo].[tbl_case_doc].[IsAddOn], " +
                                                        "[dbo].[tbl_case_doc].[Note], " +
                                                        "[dbo].[tbl_case_doc].[CreatedById], [dbo].[tbl_case_doc].[CreateDate], [dbo].[tbl_case_doc].[FullDocNo]) " +
                                                        "values (0, " +
                                                        "@NewCaseDocNo, @Case_Name, @CaseDocType, " +
                                                        "@ReceivedDate, @DestinationFilePath, @AddOn, @Note, " +
                                                        "@CreatedById, @CreateDate, @FullDocNo)";

                SqlCommand cmdInsertNewCaseDocument = new SqlCommand(strSqlInsertNewCaseDocument, connRN);
                cmdInsertNewCaseDocument.CommandType = CommandType.Text;

                cmdInsertNewCaseDocument.Parameters.AddWithValue("@NewCaseDocNo", txtCaseDocNo.Text.Trim());
                cmdInsertNewCaseDocument.Parameters.AddWithValue("@Case_Name", txtCaseNo.Text.Trim());
                cmdInsertNewCaseDocument.Parameters.AddWithValue("@CaseDocType", comboDocumentType.SelectedIndex);

                if (dtpCaseDocReceivedDate.Value != null) cmdInsertNewCaseDocument.Parameters.AddWithValue("@ReceivedDate", dtpCaseDocReceivedDate.Value);
                else cmdInsertNewCaseDocument.Parameters.AddWithValue("@ReceivedDate", DBNull.Value);
                if (txtCaseDocDestinationPath.Text.Trim() != String.Empty) cmdInsertNewCaseDocument.Parameters.AddWithValue("@DestinationFilePath", txtCaseDocDestinationPath.Text.Trim());
                else cmdInsertNewCaseDocument.Parameters.AddWithValue("@DestinationFilePath", DBNull.Value);
                cmdInsertNewCaseDocument.Parameters.AddWithValue("@AddOn", chkAddOn.Checked);
                cmdInsertNewCaseDocument.Parameters.AddWithValue("@Note", txtCaseDocNote.Text.Trim());
                cmdInsertNewCaseDocument.Parameters.AddWithValue("@CreatedById", nLoggedInUserId);
                cmdInsertNewCaseDocument.Parameters.AddWithValue("@CreateDate", DateTime.Now);
                cmdInsertNewCaseDocument.Parameters.AddWithValue("@FullDocNo", FullDocNo);                  

                if (connRN.State != ConnectionState.Closed)
                {
                    connRN.Close();
                    connRN.Open();
                }
                else if (connRN.State == ConnectionState.Closed) connRN.Open();
                int nCaseDocInserted = cmdInsertNewCaseDocument.ExecuteNonQuery();
                if (connRN.State != ConnectionState.Closed) connRN.Close();

                if (nCaseDocInserted != 1) bCaseDocInserted = false;
                else
                {
                    bCaseDocInserted = true;
                    DialogResult = DialogResult.OK;
                }
            }
        
        //DialogResult = DialogResult.OK;
        Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlgUploadCaseDoc = new OpenFileDialog();
            dlgUploadCaseDoc.Filter = "pdf files (*.pdf)|*.pdf|All files(*.*)|*.*";
            dlgUploadCaseDoc.RestoreDirectory = true;

            if (dlgUploadCaseDoc.ShowDialog() == DialogResult.OK)
            {
                String strCaseDocSourcePathName = String.Empty;
                String strCaseDocDestinationPathName = String.Empty;

                strCaseDocSourcePathName = dlgUploadCaseDoc.FileName;
                String strCaseDocSourceFileName = Path.GetFileName(strCaseDocSourcePathName);
                String strCaseName = txtCaseNo.Text.Trim();
                strCaseDocDestinationPathName = strCaseDocDestinationPath + "_" + strCaseName + "_" + DateTime.Now.ToString("MM-dd-yyyy_HH-mm-ss") + "_Doc_" + strCaseDocSourceFileName;

                try
                {
                    File.Copy(strCaseDocSourcePathName, strCaseDocDestinationPathName, true);
                    txtCaseDocDestinationPath.Text = strCaseDocDestinationPathName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
            else return;
        }

        private void btnViewCaseDoc_Click(object sender, EventArgs e)
        {

            if (txtCaseDocDestinationPath.Text.Trim() != String.Empty)
            {
                ProcessStartInfo info = new ProcessStartInfo();
                info.FileName = txtCaseDocDestinationPath.Text.Trim();
                Process.Start(info);
            }
            else
            {
                MessageBox.Show("The Case Doc Destination Path is empty.", "Error");
                return;
            }
        }
    }
}
