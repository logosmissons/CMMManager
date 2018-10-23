using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMMManager
{
    public enum Gender { Male, Female };
    public enum HouseholdRole { HeadOfHousehold, Spouse, Child };
    public enum Plan { GoldMedi_I, GoldMedi_II, GoldPlus, Gold, Silver, Bronze };
    public enum MembershipStatus { Pending, Applied, Active, PastDue, Inactive, CancelledReq, CancelledByMember, TerminatedByCMM, Hold, Incomplete };

    public class IndividualInfo
    {
        public String strID;
        public String strAccountID;
        public String strLastName;
        public String strFirstName;
        public String strMiddleName;
        public String strSalutation;
        public String strEmail;
        public DateTime? dtBirthDate;
        public Gender IndividualGender;
        public String strSSN;
        public String strBillingStreetAddress;
        public String strBillingCity;
        public String strBillingState;
        public String strBillingZip;
        public String strShippingStreetAddress;
        public String strShippingCity;
        public String strShippingState;
        public String strShippingZip;
        public String HouseholdPrimaryContact;
        public HouseholdRole IndividualHouseholdRole;
        public String strChurch;
        public String strReferredBy;
        public Plan IndividualPlan;
        public String strMembershipID;
        public String strMembershipNo;
        public MembershipStatus membershipStatus;
        public DateTime? dtMembershipStartDate;
        public String strIndividualID;
        public String strLegacyIndividualID;
        public DateTime? dtMembershipCancelledDate;
        public DateTime? dtMembershipIndStartDate;
        public float X10K_Sharing_Monthly_Fee;

        public IndividualInfo()
        {
            strID = String.Empty;
            strAccountID = String.Empty;
            strLastName = String.Empty;
            strFirstName = String.Empty;
            strMiddleName = String.Empty;
            strSalutation = String.Empty;
            strEmail = String.Empty;
            dtBirthDate = null;
            HouseholdPrimaryContact = String.Empty;
            strBillingStreetAddress = String.Empty;
            strBillingCity = String.Empty;
            strBillingState = String.Empty;
            strBillingZip = String.Empty;
            strShippingStreetAddress = String.Empty;
            strShippingCity = String.Empty;
            strShippingState = String.Empty;
            strShippingZip = String.Empty;
            strChurch = String.Empty;
            strReferredBy = String.Empty;
            strMembershipID = String.Empty;
            strMembershipNo = String.Empty;
            dtMembershipStartDate = null;
            strSSN = String.Empty;
            strIndividualID = String.Empty;
            strLegacyIndividualID = String.Empty;
            dtMembershipCancelledDate = null;
            dtMembershipIndStartDate = null;
            X10K_Sharing_Monthly_Fee = 0;

        }

        public IndividualInfo(String id,
                              String acct_id,
                              String lastname,
                              String firstname,
                              String middlename,
                              String salutation,
                              String email,
                              DateTime birthdate,
                              Gender gender,
                              String primary_contact,
                              String billing_street,
                              String billing_city,
                              String billing_state,
                              String billing_zip,
                              String shipping_street,
                              String shipping_city,
                              String shipping_state,
                              String shipping_zip,
                              HouseholdRole role,
                              String church,
                              String referredby,
                              Plan plan,
                              String membership_id,
                              String membership_no,
                              MembershipStatus mem_status,
                              DateTime membership_start_date,
                              String ssn,
                              String individual_id,
                              String legacy_ind_id,
                              DateTime membership_cancel_date,
                              DateTime membership_ind_start_date,
                              float x10k_sharing_monthly_fee)
        {
            strID = id;
            strAccountID = acct_id;
            strLastName = lastname;
            strFirstName = firstname;
            strMiddleName = middlename;
            strSalutation = salutation;
            strEmail = email;
            dtBirthDate = birthdate;
            IndividualGender = gender;
            HouseholdPrimaryContact = primary_contact;
            IndividualHouseholdRole = role;
            strBillingStreetAddress = billing_street;
            strBillingCity = billing_city;
            strBillingState = billing_state;
            strBillingZip = billing_zip;
            strShippingStreetAddress = shipping_street;
            strShippingCity = shipping_city;
            strShippingState = shipping_state;
            strShippingZip = shipping_zip;
            strChurch = church;
            strReferredBy = referredby;
            IndividualPlan = plan;
            strMembershipID = membership_id;
            strMembershipNo = membership_no;
            membershipStatus = mem_status;
            dtMembershipStartDate = membership_start_date;
            strSSN = ssn;
            strIndividualID = individual_id;
            strLegacyIndividualID = legacy_ind_id;
            dtMembershipCancelledDate = membership_cancel_date;
            dtMembershipIndStartDate = membership_ind_start_date;
            X10K_Sharing_Monthly_Fee = x10k_sharing_monthly_fee;
        }

    }
}
