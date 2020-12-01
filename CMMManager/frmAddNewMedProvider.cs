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

namespace CMMManager
{
    public partial class frmAddNewMedProvider : Form
    {
        private String connStringSalesforce;
        private SqlConnection connSalesforce;
        private List<MedicalProviderTypeInfo> lstMedicalProviderTypes;

        public frmAddNewMedProvider()
        {
            InitializeComponent();
            connStringSalesforce = @"Data Source=cmm-2019data\CMM; Initial Catalog=SalesForce; Integrated Security=True; MultipleActiveResultSets=True";
            connSalesforce = new SqlConnection(connStringSalesforce);

            lstMedicalProviderTypes = new List<MedicalProviderTypeInfo>();
        }

        private void frmAddNewMedProvider_Load(object sender, EventArgs e)
        {
            lstMedicalProviderTypes.Clear();

            String strSqlQueryForMedicalProviderTypes = "select [dbo].[RN_MedicalProviderType_Code].[MedicalProviderTypeCode], [dbo].[RN_MedicalProviderType_Code].[MedicalProviderTypeValue] " +
                                                        "from [dbo].[RN_MedicalProviderType_Code] " +
                                                        "where [dbo].[RN_MedicalProviderType_Code].[IsDeleted] = 0 or [dbo].[RN_MedicalProviderType_Code].[IsDeleted] IS NULL";

            SqlCommand cmdQueryForMedicalProviderTypes = new SqlCommand(strSqlQueryForMedicalProviderTypes, connSalesforce);
            cmdQueryForMedicalProviderTypes.CommandType = CommandType.Text;

            if (connSalesforce.State != ConnectionState.Closed)
            {
                connSalesforce.Close();
                connSalesforce.Open();
            }
            else if (connSalesforce.State == ConnectionState.Closed) connSalesforce.Open();
            SqlDataReader rdrMedicalProviderType = cmdQueryForMedicalProviderTypes.ExecuteReader();
            if (rdrMedicalProviderType.HasRows)
            {
                while (rdrMedicalProviderType.Read())
                {
                    MedicalProviderTypeInfo info = new MedicalProviderTypeInfo();
                    if (!rdrMedicalProviderType.IsDBNull(0)) info.MedicalProviderTypeCode = rdrMedicalProviderType.GetInt32(0);
                    else info.MedicalProviderTypeCode = null;
                    if (!rdrMedicalProviderType.IsDBNull(1)) info.MedicalProviderTypeName = rdrMedicalProviderType.GetString(1);
                    else info.MedicalProviderTypeName = null;

                    lstMedicalProviderTypes.Add(info);
                }
            }
            rdrMedicalProviderType.Close();
            if (connSalesforce.State != ConnectionState.Closed) connSalesforce.Close();

            lstMedicalProviderTypes.Sort(delegate (MedicalProviderTypeInfo info1, MedicalProviderTypeInfo info2)
            {
                if (info1.MedicalProviderTypeName == null && info2.MedicalProviderTypeName == null) return 0;
                else if (info1.MedicalProviderTypeName == null) return -1;
                else if (info2.MedicalProviderTypeName == null) return 1;
                else return info1.MedicalProviderTypeName.CompareTo(info2.MedicalProviderTypeName);
            });

            for (int i = 0; i < lstMedicalProviderTypes.Count; i++)
            {
                lstMedicalProviderTypes[i].SelectedId = i;
            }

            foreach (MedicalProviderTypeInfo info in lstMedicalProviderTypes)
            {
                comboMedicalProviderType.Items.Add(info.MedicalProviderTypeName);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnSaveNewMedProvider_Click(object sender, EventArgs e)
        {
            if (txtMedicalProviderName.Text.Trim() == String.Empty)
            {
                MessageBox.Show("Medical Provider Name is empty.", "Error");
                return;
            }

            //if (comboMedicalProviderType.SelectedIndex == -1)
            if (comboMedicalProviderType.SelectedItem == null)
            {
                MessageBox.Show("Medical Provider Type is empty.", "Error");
                return;
            }

            if (txtCity.Text.Trim() == String.Empty)
            {
                MessageBox.Show("Medical Provider City is empty.", "Error");
                return;
            }

            if (txtCountry.Text.Trim() == String.Empty)
            {
                MessageBox.Show("Medical Provider Country is empty.", "Error");
                return;
            }

            if (txtPhone.Text.Trim() == String.Empty)
            {
                MessageBox.Show("Phone Number is empty.", "Error");
                return;
            }

            String strMedicalProviderName = txtMedicalProviderName.Text.Trim();
            String strProviderTypeName = comboMedicalProviderType.SelectedItem.ToString();
            int? nProviderType = null;

            for (int i = 0; i < lstMedicalProviderTypes.Count; i++)
            {
                if (lstMedicalProviderTypes[i].MedicalProviderTypeName == strProviderTypeName) nProviderType = lstMedicalProviderTypes[i].MedicalProviderTypeCode;
            }

            String strStreetAddress = txtStreet.Text.Trim();
            String strCity = txtCity.Text.Trim();
            String strState = txtState.Text.Trim();
            String strZip = txtZip.Text.Trim();
            String strCountry = txtCountry.Text.Trim();

            String strPhone = txtPhone.Text.Trim();
            String strFax = txtFax.Text.Trim();
            String strEmail = txtEmail.Text.Trim();
            String strWebSite = txtWebSite.Text.Trim();

            String strSqlQueryForMedicalProvider = "select [dbo].[RN_MedicalProvider].[Phone] from [dbo].[RN_MedicalProvider] " +
                                                   "where [dbo].[RN_MedicalProvider].[Phone] = @MedicalProviderPhone and " +
                                                   "([dbo].[RN_MedicalProvider].[IsDeleted] = 0 or [dbo].[RN_MedicalProvider].[IsDeleted] IS NULL)";

            SqlCommand cmdQueryForMedicalProviderPhone = new SqlCommand(strSqlQueryForMedicalProvider, connSalesforce);
            cmdQueryForMedicalProviderPhone.CommandType = CommandType.Text;

            cmdQueryForMedicalProviderPhone.Parameters.AddWithValue("@MedicalProviderPhone", strPhone);

            if (connSalesforce.State != ConnectionState.Closed)
            {
                connSalesforce.Close();
                connSalesforce.Open();
            }
            else if (connSalesforce.State == ConnectionState.Closed) connSalesforce.Open();
            Object objMedicalProviderPhone = cmdQueryForMedicalProviderPhone.ExecuteScalar();
            if (connSalesforce.State != ConnectionState.Closed) connSalesforce.Close();

            if (objMedicalProviderPhone == null)
            {
                String strSqlInsertNewMedicalProvider = "insert into [dbo].[RN_MedicalProvider] " +
                                                        "([dbo].[RN_MedicalProvider].[MedicalProviderName], [dbo].[RN_MedicalProvider].[MedicalProviderType], " +
                                                        "[dbo].[RN_MedicalProvider].[Street], [dbo].[RN_MedicalProvider].[City], [dbo].[RN_MedicalProvider].[State], " +
                                                        "[dbo].[RN_MedicalProvider].[Zip], [dbo].[RN_MedicalProvider].[Country], " +
                                                        "[dbo].[RN_MedicalProvider].[Phone], [dbo].[RN_MedicalProvider].[Fax], " +
                                                        "[dbo].[RN_MedicalProvider].[Email], [dbo].[RN_MedicalProvider].[WebSite])" +
                                                        "values (@MedicalProviderName, @MedicalProviderType, @Street, @City, @State, @Zip, @Country, @Phone, @Fax, @Email, @WebSite)";

                SqlCommand cmdInsertNewMedicalProvider = new SqlCommand(strSqlInsertNewMedicalProvider, connSalesforce);

                cmdInsertNewMedicalProvider.Parameters.AddWithValue("@MedicalProviderName", strMedicalProviderName);
                cmdInsertNewMedicalProvider.Parameters.AddWithValue("@MedicalProviderType", nProviderType);
                cmdInsertNewMedicalProvider.Parameters.AddWithValue("@Street", strStreetAddress);
                cmdInsertNewMedicalProvider.Parameters.AddWithValue("@City", strCity);
                cmdInsertNewMedicalProvider.Parameters.AddWithValue("@State", strState);
                cmdInsertNewMedicalProvider.Parameters.AddWithValue("@Zip", strZip);
                cmdInsertNewMedicalProvider.Parameters.AddWithValue("@Country", strCountry);
                cmdInsertNewMedicalProvider.Parameters.AddWithValue("@Phone", strPhone);
                cmdInsertNewMedicalProvider.Parameters.AddWithValue("@Fax", strFax);
                cmdInsertNewMedicalProvider.Parameters.AddWithValue("@Email", strEmail);
                cmdInsertNewMedicalProvider.Parameters.AddWithValue("@WebSite", strWebSite);

                if (connSalesforce.State != ConnectionState.Closed)
                {
                    connSalesforce.Close();
                    connSalesforce.Open();
                }
                else if (connSalesforce.State == ConnectionState.Closed) connSalesforce.Open();
                int nNewMedicalProviderInserted = cmdInsertNewMedicalProvider.ExecuteNonQuery();
                if (connSalesforce.State != ConnectionState.Closed) connSalesforce.Close();

                if (nNewMedicalProviderInserted == 1)
                {
                    MessageBox.Show("New Medical Provider has been added.", "Information");
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
            else
            {
                MessageBox.Show("The Medical Provider already exists. Please choose from the Medical Provider list.");
                return;
            }
        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
