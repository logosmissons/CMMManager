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
    public partial class frmSearchResult : Form
    {
        DataGridView gvIndividual;
        DataGridView gvCase;
        DataGridView gvIllness;
        DataGridView gvMedBill;

        String strSalesforceConnString;
        SqlConnection connSalesforce;
        SqlCommand cmdSalesforce;
        //SqlDataAdapter daContact;
        //DataTable dtContact;

        public IndividualInfo IndividualSelected;


        public frmSearchResult()
        {
            InitializeComponent();

            strSalesforceConnString = @"Data Source = CMM-2014U\CMM; Initial Catalog=SalesForce; Integrated Security=True";
            connSalesforce = new SqlConnection(strSalesforceConnString);
            
            gvIndividual = new DataGridView();

        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            return;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (gvIndividual.Rows.Count > 0) gvIndividual.DataSource = null;

            String strSqlSearchContact = "select contact.ID, contact.AccountID, contact.LastName, contact.FirstName, contact.MiddleName, contact.Salutation, " +
                                         "contact.Email, contact.BirthDate, contact.cmm_Gender__C, contact.Household_Role__C, contact.c4g_Plan__C, " +
                                         "membership.Name as Membership, contact.c4g_Membership_Status__C, contact.Social_Security_Number__C, " +
                                         "contact.Individual_ID__C, contact.Legacy_Database_Individual_ID__C, " +
                                         "contact.Membership_Ind_Start_Date__C As MembershipStartDate, contact.Membership_Cancelled_Date__C As MembershipCancelDate, " +
                                         "contact.c4g_Membership_Status__C As MembershipStatus, " +
                                         "account.BillingStreet, account.BillingCity, account.BillingState, account.BillingPostalCode, " +
                                         "account.ShippingStreet, account.ShippingCity, account.ShippingState, account.ShippingPostalCode, " +
                                         "Church.Name As ChurchName, " +
                                         "program.Name as ProgramName " +
                                         "from contact " + 
                                         "inner join membership on contact.c4g_Membership__C = membership.ID " +
                                         "inner join account on contact.AccountID = account.ID " +
                                         "inner join program on contact.c4g_plan__c = program.ID " +
                                         "inner join Church on contact.c4g_Church__C = Church.ID " +
                                         "where contact.LastName like '%' + @LastName + '%' or " +
                                         "contact.FirstName like '%' + @FirstName + '%' or " +
                                         "contact.Household_Role__C like '%' + @HouseholdRole + '%' or " +
                                         "contact.c4g_Membership__C like '%' + @MembershipID + '%' or " +
                                         "contact.c4g_Membership_Status__C like '%' + @MembershipStatus + '%' or " +
                                         "contact.Social_Security_Number__C like '%' + @SSN + '%' or " +
                                         "contact.Individual_ID__C like '%' + @IndividualID + '%' or " +
                                         "contact.Legacy_Database_Individual_ID__C like '%' + @LagacyIndividualID + '%'";

            cmdSalesforce = connSalesforce.CreateCommand();
            cmdSalesforce.CommandType = CommandType.Text;
            cmdSalesforce.CommandText = strSqlSearchContact;

            cmdSalesforce.Parameters.AddWithValue("@LastName", txtSearch.Text.Trim());
            cmdSalesforce.Parameters.AddWithValue("@FirstName", txtSearch.Text.Trim());
            cmdSalesforce.Parameters.AddWithValue("@HouseholdRole", txtSearch.Text.Trim());
            cmdSalesforce.Parameters.AddWithValue("@MembershipID", txtSearch.Text.Trim());
            cmdSalesforce.Parameters.AddWithValue("@MembershipStatus", txtSearch.Text.Trim());
            cmdSalesforce.Parameters.AddWithValue("@SSN", txtSearch.Text.Trim());
            cmdSalesforce.Parameters.AddWithValue("@IndividualID", txtSearch.Text.Trim());
            cmdSalesforce.Parameters.AddWithValue("@LagacyIndividualID", txtSearch.Text.Trim());

            DataTable dtContact = new DataTable();

            SqlDataAdapter daContact = new SqlDataAdapter(cmdSalesforce);
            daContact.Fill(dtContact);

            gvIndividual.DataSource = dtContact;

            if (dtContact.Rows.Count > 0) gvIndividual.Height = 25 * (dtContact.Rows.Count + 1);
            gvIndividual.Width = 3000;
            gvIndividual.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gvIndividual.MultiSelect = false;
            gvIndividual.AllowUserToAddRows = false;

            gvIndividual.CellDoubleClick += gvIndividual_CellDoubleClicked;

            IndividualSelected = new IndividualInfo();

            pnlSearchResult.Controls.Add(gvIndividual);
        }

        private void gvIndividual_CellDoubleClicked(object sender, EventArgs e)
        {
            int nRowSelected = gvIndividual.CurrentCell.RowIndex;

            IndividualSelected.strMembershipID = gvIndividual["Membership", nRowSelected].Value.ToString();
            IndividualSelected.strID = gvIndividual["ID", nRowSelected].Value.ToString();
            IndividualSelected.strAccountID = gvIndividual["AccountID", nRowSelected].Value.ToString();
            IndividualSelected.strLastName = gvIndividual["LASTNAME", nRowSelected].Value.ToString();
            IndividualSelected.strFirstName = gvIndividual["FIRSTNAME", nRowSelected].Value.ToString();
            IndividualSelected.strSalutation = gvIndividual["SALUTATION", nRowSelected].Value.ToString();
            IndividualSelected.dtBirthDate = DateTime.Parse(gvIndividual["BIRTHDATE", nRowSelected].Value.ToString());
            if (gvIndividual["cmm_GENDER__C", nRowSelected].Value.ToString() == "Male") IndividualSelected.IndividualGender = Gender.Male;
            else if (gvIndividual["cmm_GENDER__C", nRowSelected].Value.ToString() == "Female") IndividualSelected.IndividualGender = Gender.Female;
            if (gvIndividual["HOUSEHOLD_ROLE__C", nRowSelected].Value.ToString() == "Head of Household")
                IndividualSelected.IndividualHouseholdRole = HouseholdRole.HeadOfHousehold;
            else if (gvIndividual["HOUSEHOLD_ROLE__C", nRowSelected].Value.ToString() == "Spouse") IndividualSelected.IndividualHouseholdRole = HouseholdRole.Spouse;
            else if (gvIndividual["HOUSEHOLD_ROLE__C", nRowSelected].Value.ToString() == "Child") IndividualSelected.IndividualHouseholdRole = HouseholdRole.Child;

            IndividualSelected.strSSN = gvIndividual["SOCIAL_SECURITY_NUMBER__C", nRowSelected].Value.ToString();
            IndividualSelected.strIndividualID = gvIndividual["INDIVIDUAL_ID__C", nRowSelected].Value.ToString();

            IndividualSelected.strBillingStreetAddress = gvIndividual["BillingStreet", nRowSelected].Value.ToString();
            IndividualSelected.strBillingCity = gvIndividual["BillingCity", nRowSelected].Value.ToString();
            IndividualSelected.strBillingState = gvIndividual["BillingState", nRowSelected].Value.ToString();
            IndividualSelected.strBillingZip = gvIndividual["BillingPostalCode", nRowSelected].Value.ToString();
            IndividualSelected.strShippingStreetAddress = gvIndividual["ShippingStreet", nRowSelected].Value.ToString();
            IndividualSelected.strShippingCity = gvIndividual["ShippingCity", nRowSelected].Value.ToString();
            IndividualSelected.strShippingState = gvIndividual["ShippingState", nRowSelected].Value.ToString();
            IndividualSelected.strShippingZip = gvIndividual["ShippingPostalCode", nRowSelected].Value.ToString();

            IndividualSelected.strEmail = gvIndividual["Email", nRowSelected].Value.ToString();

            if (gvIndividual["ProgramName", nRowSelected].Value.ToString() == "Gold Medi-I") IndividualSelected.IndividualPlan = Plan.GoldMedi_I;
            else if (gvIndividual["ProgramName", nRowSelected].Value.ToString() == "Gold Medi-II") IndividualSelected.IndividualPlan = Plan.GoldMedi_II;
            else if (gvIndividual["ProgramName", nRowSelected].Value.ToString() == "Gold Plus") IndividualSelected.IndividualPlan = Plan.GoldPlus;
            else if (gvIndividual["ProgramName", nRowSelected].Value.ToString() == "Gold") IndividualSelected.IndividualPlan = Plan.Gold;
            else if (gvIndividual["ProgramName", nRowSelected].Value.ToString() == "Silver") IndividualSelected.IndividualPlan = Plan.Silver;
            else if (gvIndividual["ProgramName", nRowSelected].Value.ToString() == "Bronze") IndividualSelected.IndividualPlan = Plan.Bronze;

            IndividualSelected.dtMembershipIndStartDate = DateTime.Parse(gvIndividual["MembershipStartDate", nRowSelected].Value.ToString());
            if (gvIndividual["MembershipCancelDate", nRowSelected].Value.ToString() != String.Empty)
            {
                IndividualSelected.dtMembershipCancelledDate = DateTime.Parse(gvIndividual["MembershipCancelDate", nRowSelected].Value.ToString());
            }
            else IndividualSelected.dtMembershipCancelledDate = null;

            IndividualSelected.strChurch = gvIndividual["ChurchName", nRowSelected].Value.ToString();

            switch(gvIndividual["MembershipStatus", nRowSelected].Value.ToString())
            {
                case "Pending":
                    IndividualSelected.membershipStatus = MembershipStatus.Pending;
                    break;
                case "Applied":
                    IndividualSelected.membershipStatus = MembershipStatus.Applied;
                    break;
                case "Active":
                    IndividualSelected.membershipStatus = MembershipStatus.Active;
                    break;
                case "Past Due":
                    IndividualSelected.membershipStatus = MembershipStatus.PastDue;
                    break;
                case "Inactive":
                    IndividualSelected.membershipStatus = MembershipStatus.Inactive;
                    break;
                case "Cancelled Req.":
                    IndividualSelected.membershipStatus = MembershipStatus.CancelledReq;
                    break;
                case "Cancelled by Member":
                    IndividualSelected.membershipStatus = MembershipStatus.CancelledByMember;
                    break;
                case "Terminated by CMM":
                    IndividualSelected.membershipStatus = MembershipStatus.TerminatedByCMM;
                    break;
                case "Hold":
                    IndividualSelected.membershipStatus = MembershipStatus.Hold;
                    break;
                case "Incomplete":
                    IndividualSelected.membershipStatus = MembershipStatus.Incomplete;
                    break;
                default:
                    break;
            }

            DialogResult = DialogResult.OK;
            return;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
