using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System;
//using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
//using System.Text;
using System.Windows.Forms;

using OfficeOutlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;


//using System.Linq;

namespace CMMManager
{
    public class IndividualProgramRecord
    {
        public String IndividualId;
        public DateTime? CreateDate;
        public IndividualPlan? OldPlan;
        public IndividualPlan? NewPlan;

        public IndividualProgramRecord()
        {
            IndividualId = String.Empty;
            CreateDate = null;
            OldPlan = null;
            NewPlan = null; 
        }

        public IndividualProgramRecord(String individual_id,
                                       DateTime create_date,
                                       IndividualPlan old_plan,
                                       IndividualPlan new_plan)
        {
            IndividualId = individual_id;
            CreateDate = create_date;
            OldPlan = old_plan;
            NewPlan = new_plan;
        }
    }


    public class MedicalBillInfoForList
    {
        public String MedBillNo;
        public int MedBillTypeId;
        public String MedBillTypeName;
        public int MedBillStatusId;
        public String MedBillStatus;
        public Boolean bBillClosed;
        public DateTime? CreateDate;
        public String CreateStaffName;
        public DateTime? LastModifiDate;
        public String ModifiStaffName;
        public String CaseId;
        public int IllnessId;
        public String IllnessNo;
        public String ProgramName;
        public String ICD_10_Id;
        public int IncidentId;
        public String IncidentNo;
        public Boolean NPF_Form;
        public String NPFormFileNameMedBill;
        public String NPFSourceFilePathMedBill;
        public String NPFDestinationFilePathMedbill;
        public Boolean IB_Form;
        public String IBFileNameMedBill;
        public String IBSourceFilePathMedBill;
        public String IBDestinationFilePathMedBill;
        public Boolean PoP_Form;
        public String PoPFileNameMedBill;
        public String PoPSourceFilePathMedBill;
        public String PoPDestinationFilePathMedBill;
        public Boolean MedRec_Form;
        public String MedRecFileNameMedBill;
        public String MedRecSourceFilePathMedBill;
        public String MedRecDestiantionFilePathMedbill;
        public Boolean OtherDoc_Form;
        public String OtherDocFileNameMedBill;
        public String OtherDocSourceFilePathMedBill;
        public String OtherDocDestinationFilePathMedBill;
        public Decimal BillAmount;
        public Double WaitingPayBalance;
        public Decimal Balance;
        public Decimal SettlementTotal;
        public Decimal TotalSharedAmount;
        public DateTime? BillDate;
        public DateTime? ReceivedDate;
        public String Guarantor;
        public String MedicalProviderId;
        public String MedicalProviderName;
        public int nPatientTypeId;
        public String PatientTypeName;
        public int nPendingReasonIndex;
        public String PendingReason;
        public int nIneligibleReasonIndex;
        public String IneligibleReason;
        public String PrescriptionDrugName;
        public String PrescriptionNo;
        public String PrescriptionDescription;
        public String Note;
        public int TotalNumberOfPhysicalTherapy;
        public Decimal PersonalResponsibilityCredit;
        public Boolean WellBeingCare;
        public Decimal WellBeingCareTotal;
        public String ProviderPhoneNumber;
        public String AccountNoAtProvider;
        public String ProviderContactPerson;
        public DateTime? ProposalLetterSentDate;
        public DateTime? HIPPASentDate;
        public DateTime? MedicalRecordDate;

        public MedicalBillInfoForList()
        {
            MedBillNo = String.Empty;
            MedBillTypeId = -1;
            MedBillTypeName = String.Empty;
            MedBillStatusId = -1;
            MedBillStatus = String.Empty;
            bBillClosed = false;
            CreateDate = null;
            CreateStaffName = String.Empty;
            LastModifiDate = null;
            ModifiStaffName = String.Empty;
            CaseId = String.Empty;
            IllnessId = -1;
            IllnessNo = String.Empty;
            ProgramName = String.Empty;
            ICD_10_Id = String.Empty;
            IncidentId = -1;
            IncidentNo = String.Empty;
            NPF_Form = false;
            NPFormFileNameMedBill = String.Empty;
            NPFSourceFilePathMedBill = String.Empty;
            NPFDestinationFilePathMedbill = String.Empty;
            IB_Form = false;
            IBFileNameMedBill = String.Empty;
            IBSourceFilePathMedBill = String.Empty;
            IBDestinationFilePathMedBill = String.Empty;
            PoP_Form = false;
            PoPFileNameMedBill = String.Empty;
            PoPSourceFilePathMedBill = String.Empty;
            PoPDestinationFilePathMedBill = String.Empty;
            MedRec_Form = false;
            MedRecFileNameMedBill = String.Empty;
            MedRecSourceFilePathMedBill = String.Empty;
            MedRecDestiantionFilePathMedbill = String.Empty;
            OtherDoc_Form = false;
            OtherDocFileNameMedBill = String.Empty;
            OtherDocSourceFilePathMedBill = String.Empty;
            OtherDocDestinationFilePathMedBill = String.Empty;
            BillAmount = 0;
            WaitingPayBalance = 0;
            Balance = 0;
            SettlementTotal = 0;
            TotalSharedAmount = 0;
            BillDate = null;
            ReceivedDate = null;
            Guarantor = String.Empty;
            MedicalProviderId = String.Empty;
            MedicalProviderName = String.Empty;
            nPatientTypeId = -1;
            PatientTypeName = String.Empty;
            nPendingReasonIndex = -1;
            PendingReason = String.Empty;
            nIneligibleReasonIndex = -1;
            IneligibleReason = String.Empty;
            PrescriptionDrugName = String.Empty;
            PrescriptionNo = String.Empty;
            PrescriptionDescription = String.Empty;
            TotalNumberOfPhysicalTherapy = 0;
            Note = String.Empty;
            PersonalResponsibilityCredit = 0;
            WellBeingCare = false;
            WellBeingCareTotal = 0;
            ProviderPhoneNumber = String.Empty;
            AccountNoAtProvider = String.Empty;
            ProviderContactPerson = String.Empty;
            ProposalLetterSentDate = null;
            HIPPASentDate = null;
            MedicalRecordDate = null;
        }
    }

    public class MedBillInfoInCaseForm
    {
        public String MedicalBillType;
        public String MedicalBillNo;
        public DateTime? DateOfService;
        public String MedicalProviderId;
        public String MedicalProviderName;
        public Double? MedicalBillAmount;
        public Double? WaitingPayBalance;
        public Double? TotalSharedAmount;
        public Double? SettlementTotal;
        public Double? Balance;
        public String PendingReason;
        public String IneligibleReason;
        public String IncidentNo;
        public String IncidentNote;
        public DateTime? CreateDate;
        public String CreatedByStaffName;
        public DateTime? ModifiedDate;
        public String ModifiedByStaffName;

        public MedBillInfoInCaseForm()
        {
            MedicalBillType = String.Empty;
            MedicalBillNo = String.Empty;
            DateOfService = null;
            MedicalProviderId = String.Empty;
            MedicalProviderName = String.Empty;
            MedicalBillAmount = null;
            WaitingPayBalance = null;
            TotalSharedAmount = null;
            SettlementTotal = null;
            Balance = null;
            PendingReason = String.Empty;
            IneligibleReason = String.Empty;
            IncidentNo = String.Empty;
            IncidentNote = String.Empty;
            CreateDate = null;
            CreatedByStaffName = String.Empty;
            ModifiedDate = null;
            ModifiedByStaffName = String.Empty;
        }
    }

    public class MedBillStatusInfoCMMPendingPayment
    {
        public String MedBillNo;
        public int? MedBillStatus;

        public MedBillStatusInfoCMMPendingPayment()
        {
            MedBillNo = String.Empty;
            MedBillStatus = null;
        }
    }


    public class MedicalBillStatusCode
    {
        public int BillStatusCode;
        public int NewIndexCode;
        public String BillStatusValue;
        public String BillStatusDescription;

        public MedicalBillStatusCode()
        {
            BillStatusCode = -1;
            NewIndexCode = -1;
            BillStatusValue = String.Empty;
            BillStatusDescription = String.Empty;
        }

        public MedicalBillStatusCode(int status_code, int index_code, String status_value, String status_description)
        {
            BillStatusCode = status_code;
            NewIndexCode = index_code;
            BillStatusValue = status_value;
            BillStatusDescription = status_description;
        }
    }

    public class CaseDocInfoInMedBill
    {
        public String CaseDocNo;
        public String DocumentType;
        public DateTime? ReceivedDate;
        public String DestinationFilePath;

        public CaseDocInfoInMedBill()
        {
            CaseDocNo = null;
            DocumentType = null;
            ReceivedDate = null;
            DestinationFilePath = null;
        }
    }

    public class AnivDateInfo : IEquatable<AnivDateInfo>
    {
        public String IndividualId;
        public DateTime? AnivDate;
        public String Program;

        public AnivDateInfo()
        {
            IndividualId = String.Empty;
            AnivDate = null;
            Program = String.Empty;
        }

        public AnivDateInfo(String individual_id, DateTime aniv_date, String program)
        {
            IndividualId = individual_id;
            AnivDate = aniv_date;
            Program = program;
        }

        public bool Equals(AnivDateInfo otherInfo)
        {
            if (Object.ReferenceEquals(otherInfo, null)) return false;

            if (Object.ReferenceEquals(this, otherInfo)) return true;

            return IndividualId.Equals(otherInfo.IndividualId) && AnivDate.Equals(otherInfo.AnivDate) && Program.Equals(otherInfo.Program);
        }

        public override int GetHashCode()
        {
            int hashAnivInfoIndividualId = IndividualId == null ? 0 : IndividualId.GetHashCode();

            int hashAnivInfoProgram = Program == null ? 0 : Program.GetHashCode();

            int hashAnivInfoDate = AnivDate.GetHashCode();

            return hashAnivInfoIndividualId ^ hashAnivInfoProgram ^ hashAnivInfoDate;
        }
    }

    public class CommunicationTaskInfo
    {
        public Boolean? IsSelected;
        public String IndividualId;
        public String CaseNo;
        public String IllnessNo;
        public String IncidentNo;
        public String CommunicationNo;
        public CommunicationType? CommunicationType;
        public String Subject;
        public String Body;
        public String Solution;
        public DateTime? CreateDate;
        public String CreateStaff;
        public Boolean? IsCompleted;
        public int? nTaskId;
        public String TaskStatus;
        public String WhoId;
        public String WhatId;
        //public CommunicationType? commType;
        public String Comment;

        public CommunicationTaskInfo()
        {
            IsSelected = null;
            IndividualId = String.Empty;
            CaseNo = String.Empty;
            IllnessNo = String.Empty;
            IncidentNo = String.Empty;
            CommunicationNo = String.Empty;
            CommunicationType = null;
            Subject = String.Empty;
            Body = String.Empty;
            Solution = String.Empty;
            CreateDate = null;
            CreateStaff = String.Empty;
            IsCompleted = null;

            nTaskId = null;
            TaskStatus = String.Empty;
            WhoId = String.Empty;
            WhatId = String.Empty;
            //CommunicationType = null;
            Comment = String.Empty;
        }
    }

    public class CommunicationTypeInfo
    {
        public int? nSelectedIndex;
        public int? nCommunicationTypeId;
        public String CommunicationTypeValue;

        public CommunicationTypeInfo()
        {
            nSelectedIndex = null;
            nCommunicationTypeId = null;
            CommunicationTypeValue = null;
        }

        public CommunicationTypeInfo(int comm_id, String comm_type)
        {
            nCommunicationTypeId = comm_id;
            CommunicationTypeValue = comm_type;
        }

        public CommunicationTypeInfo(int selected_id, int comm_id, String comm_type)
        {
            nSelectedIndex = selected_id;
            nCommunicationTypeId = comm_id;
            CommunicationTypeValue = comm_type;
        }

    }

    public class TaskInfo
    {
        public int? TaskId;
        public DateTime? CreateDate;
        public String Subject;
        public String Comment;
        public String Solution;

        public TaskInfo()
        {
            TaskId = null;
            CreateDate = null;
            Subject = String.Empty;
            Comment = String.Empty;
            Solution = String.Empty;
        }

        public TaskInfo(int task_id, DateTime create_date, String subject, String comment, String solution)
        {
            TaskId = task_id;
            CreateDate = create_date;
            Subject = subject;
            Comment = comment;
            Solution = solution;
        }
    }

    public class IllnessInfo
    {
        public int? IllnessId;
        public String IllnessNo;

        public IllnessInfo()
        {
            IllnessId = null;
            IllnessNo = String.Empty;
        }

        public IllnessInfo(int illness_id, String illness_no)
        {
            IllnessId = illness_id;
            IllnessNo = illness_no;
        }
    }

    public class IncidentIdNo
    {
        public int? IncidentId;
        public String IncidentNo;

        public IncidentIdNo()
        {
            IncidentId = null;
            IncidentNo = String.Empty;
        }

        public IncidentIdNo(int incd_id, String incd_no)
        {
            IncidentId = incd_id;
            IncidentNo = incd_no;
        }
    }

    public class IllnessIdIncidentId
    {
        public int? IllnessId;
        public int? IncidentId;

        public IllnessIdIncidentId()
        {
            IllnessId = null;
            IncidentId = null;
        }

        public IllnessIdIncidentId(int illness_id, int incident_id)
        {
            IllnessId = illness_id;
            IncidentId = incident_id;
        }
    }

    public class BankInfo
    {
        public String BankName;
        public String BankRoutingNumber;
        public String AccountNumber;
        public String AccountHolder;

        public BankInfo()
        {
            BankName = String.Empty;
            BankRoutingNumber = String.Empty;
            AccountNumber = String.Empty;
            AccountHolder = String.Empty;
        }
    }

    public class SearchResultSettlementInfo
    {
        public String Name;
        public String MedicalBillNo;
        public String IndividualId;
        public String IndividualName;
        public String Type;
        public Double Amount;
        public String PaymentMethod;
        public String CheckNo;
        public DateTime? CheckDate;
        public String ACH_No;
        public DateTime? ACH_Date;
        public String CreditCardNo;
        public DateTime? CreditCardPaidDate;
        public Boolean Approved;

        public SearchResultSettlementInfo()
        {
            Name = null;
            MedicalBillNo = null;
            IndividualId = null;
            IndividualName = null;
            Type = null;
            Amount = 0;
            PaymentMethod = null;
            CheckNo = null;
            CheckDate = null;
            ACH_No = null;
            ACH_Date = null;
            CreditCardNo = null;
            CreditCardPaidDate = null;
            Approved = false;
            
        }
    }

    public class SettlementInfoForApproval
    {
        public String MedBillNo;
        public String IndividualId;
        public String IndividualName;
        public String SettlementNo;
        public String SettlementType;
        public Decimal SettlementAmount;
        public Boolean IsWellBeing;

        public SettlementInfoForApproval()
        {
            MedBillNo = String.Empty;
            IndividualId = String.Empty;
            IndividualName = String.Empty;
            SettlementNo = String.Empty;
            SettlementType = String.Empty;
            SettlementAmount = 0;
            IsWellBeing = false;
        }
    }

    public class ApprovedSettlementInfo
    {
        public String IncidentNo;
        public String FullName;
        public String FirstName;
        public String MiddleName;
        public String LastName;
        public String HouseholdRole;
        public String IndividualId;
        public String PrimaryName;
        public String CMMPaymentMethod;
        public String ProgramName;
        public String IncidentProgram;
        public DateTime MembershipStartDate;
        public String MembershipNo;
        public String ICD10Code;
        public DateTime IBReceivedDate;
        public DateTime ServiceDate;
        public String MedicalProviderId;
        public String MedicalProviderName;
        public String AccountNoAtMedProvider;
        public String AccountShippingStreet;
        public String AccountShippingCity;
        public String AccountShippingState;
        public String AccountShppingZip;
        public Decimal Amount;
        public String SettlementType;
        public String PaymentType;
        public String MedBillName;
        public String SettlementName;
        public Decimal WellBeingCareShared;
        public String ICD10CodeDescription;
        public String CreatedBy;
        public String LastModifiedBy;
        public String MembershipStatus;
        public Boolean Approved;

        public ApprovedSettlementInfo()
        {

        }
    }

    public class IncidentProgramInfo
    {
        public Boolean bIsDeleted;
        public Boolean bPersonalResponsibilityProgram;
        public int? IncidentProgramId;
        public String IncidentProgramName;
        public Decimal PersonalResponsibilityAmount;

        public IncidentProgramInfo()
        {
            bPersonalResponsibilityProgram = false;
            IncidentProgramId = null;
            IncidentProgramName = String.Empty;
        }

        public IncidentProgramInfo(int program_id, String program_name)
        {
            bIsDeleted = false;
            IncidentProgramId = program_id;
            IncidentProgramName = program_name;

            switch (program_id)
            {
                case 0:
                    PersonalResponsibilityAmount = 500;
                    break;
                case 1:
                    PersonalResponsibilityAmount = 500;
                    break;
                case 2:
                    PersonalResponsibilityAmount = 1000;
                    break;
                case 3:
                    PersonalResponsibilityAmount = 5000;
                    break;
                case 4:
                    PersonalResponsibilityAmount = 500;
                    break;
                case 5:
                    PersonalResponsibilityAmount = 500;
                    break;
            }
        }

        public IncidentProgramInfo(Boolean deleted, int program_id, String program_name)
        {
            bIsDeleted = deleted;
            IncidentProgramId = program_id;
            IncidentProgramName = program_name;

            switch (program_id)
            {
                case 0:
                    PersonalResponsibilityAmount = 500;
                    break;
                case 1:
                    PersonalResponsibilityAmount = 500;
                    break;
                case 2:
                    PersonalResponsibilityAmount = 1000;
                    break;
                case 3:
                    PersonalResponsibilityAmount = 5000;
                    break;
                case 4:
                    PersonalResponsibilityAmount = 500;
                    break;
                case 5:
                    PersonalResponsibilityAmount = 500;
                    break;
            }
        }
    }

    public class MedBillNoteTypeInfo
    {
        public int? MedBillNoteTypeId;
        public String MedBillNoteTypeValue;

        public MedBillNoteTypeInfo()
        {
            MedBillNoteTypeId = null;
            MedBillNoteTypeValue = String.Empty;
        }

        public MedBillNoteTypeInfo(int id, String value)
        {
            MedBillNoteTypeId = id;
            MedBillNoteTypeValue = value;
        }
    }

    public class IneligibleMedicalBillInfo
    {
        public String IncidentNo;
        public String MedicalBillNo;
        public Boolean bHasIneligibleSettlement;
        public Boolean bHasPaymentSettlement;
        public MedBillIneligibleType? IneligibleType;

        public IneligibleMedicalBillInfo()
        {
            IncidentNo = null;
            MedicalBillNo = null;
            bHasIneligibleSettlement = false;
            bHasPaymentSettlement = false;
            IneligibleType = null;
        }
    }

    public class MedBillStatusInfo
    {
        public Boolean IsDeleted;
        public int BillStatusCode;
        public String BillStatusValue;

        public MedBillStatusInfo()
        {
            IsDeleted = false;
            BillStatusCode = 0;
            BillStatusValue = String.Empty;
        }
    }

    public class MedicalProviderInfo
    {
        public String ID;
        public int? RN_ID;
        public String Phone;
        public String Name;
        public String Type;
        public int? RN_TypeId;

        public MedicalProviderInfo()
        {
            ID = String.Empty;
            RN_ID = null;
            Phone = null;
            Name = String.Empty;
            Type = String.Empty;
            RN_TypeId = null;
        }

        public MedicalProviderInfo(String id, String name, String type)
        {
            ID = id;
            Name = name;
            Type = type;
        }

        public MedicalProviderInfo(int rn_id, String phone_no, String name, int provider_type_id)
        {
            RN_ID = rn_id;
            Phone = phone_no;
            Name = name;
            RN_TypeId = provider_type_id;
        }
    }

    public class MedicalProviderTypeInfo
    {
        public int? SelectedId;
        public int? MedicalProviderTypeCode;
        public String MedicalProviderTypeName;

        public MedicalProviderTypeInfo()
        {
            SelectedId = null;
            MedicalProviderTypeCode = null;
            MedicalProviderTypeName = null;
        }

        public MedicalProviderTypeInfo(int selected_id, int provider_type_code, String medical_provider_type_name)
        {
            SelectedId = selected_id;
            MedicalProviderTypeCode = provider_type_code;
            MedicalProviderTypeName = medical_provider_type_name;
        }
    }

    public class ChurchInfo
    {
        public String ID;
        public String Name;

        public ChurchInfo()
        {
            ID = String.Empty;
            Name = String.Empty;
        }

        public ChurchInfo(String id, String name)
        {
            ID = id;
            Name = name;
        }
    }

    public class MedicalBillInfo
    {
        String BillNo;
        DateTime BillDate;
        int BillType;
        String CaseId;
        String Incident;
        String IllnessId;
        Double BillAmount;
        Double SettlementTotal;
        Double TotalSharedAmount;
        int BillStatus;

        public MedicalBillInfo()
        {
            BillNo = String.Empty;
            BillDate = DateTime.Today;
            BillType = -1;
            CaseId = String.Empty;
            Incident = String.Empty;
            IllnessId = String.Empty;
            BillAmount = 0;
            SettlementTotal = 0;
            TotalSharedAmount = 0;
            BillStatus = -1;
        }
    }

    public class SettlementTypeInfo
    {
        public int SettlementTypeCode;
        public String SettlementTypeValue;

        public SettlementTypeInfo()
        {
            SettlementTypeCode = 0;
            SettlementTypeValue = String.Empty;
        }

        public SettlementTypeInfo(int code, String value)
        {
            SettlementTypeCode = code;
            SettlementTypeValue = value;
        }
    }

    public class IndividualCityState
    {
        public String City;
        public String State;

        public IndividualCityState()
        {
            City = String.Empty;
            State = String.Empty;
        }
    }

    public class ConfirmChristianYesNo
    {
        public Boolean? ChristianYes;
        public Boolean? ChristianNo;

        public ConfirmChristianYesNo()
        {
            ChristianYes = null;
            ChristianNo = null;
        }
    }

    public class PersonalResponsiblityTypeInfo
    {
        public int PersonalResponsibilityTypeCode;
        public String PersonalResponsibilityTypeValue;

        public PersonalResponsiblityTypeInfo()
        {
            PersonalResponsibilityTypeCode = 0;
            PersonalResponsibilityTypeValue = String.Empty;
        }

        public PersonalResponsiblityTypeInfo(int code, String value)
        {
            PersonalResponsibilityTypeCode = code;
            PersonalResponsibilityTypeValue = value;
        }
    }

    public class StaffInfo
    {
        public int StaffId;
        public String StaffName;

        public StaffInfo()
        {
            StaffId = -1;
            StaffName = String.Empty;
        }
        public StaffInfo(int staff_id, String staff_name)
        {
            StaffId = staff_id;
            StaffName = staff_name;
        }
    }

    public class PaymentMethod
    {
        public int PaymentMethodId;
        public String PaymentMethodValue;

        public PaymentMethod()
        {
            PaymentMethodId = 0;
            PaymentMethodValue = String.Empty;
        }

        public PaymentMethod(int id, String value)
        {
            PaymentMethodId = id;
            PaymentMethodValue = value;
        }
    }

    public class CreditCardInfo
    {
        public int CreditCardId;
        public String CreditCardNo;

        public CreditCardInfo()
        {
            CreditCardId = 0;
            CreditCardNo = String.Empty;
        }

        public CreditCardInfo(int id, String card_no)
        {
            CreditCardId = id;
            CreditCardNo = card_no;
        }
    }
    //public class MedicalFormReceived
    //{
    //    public int NPF_Form;
    //    public int IB_Form;
    //    public int POP_Form;
    //    public int MedicalRecord_Form;
    //    public int Unknown_Form;

    //    public MedicalFormReceived()
    //    {
    //        NPF_Form = 0;
    //        IB_Form = 0;
    //        POP_Form = 0;
    //        MedicalRecord_Form = 0;
    //        Unknown_Form = 0;
    //    }

    //    public MedicalFormReceived(int npf_form, int ib_form, int pop_form, int medical_form, int unknown_form)
    //    {
    //        NPF_Form = npf_form;
    //        IB_Form = ib_form;
    //        POP_Form = pop_form;
    //        MedicalRecord_Form = medical_form;
    //        Unknown_Form = unknown_form;
    //    }
    //}

    public class CaseInfo
    {
        public String CaseName;
        public String IndividualId;

        public CaseInfo()
        {
            CaseName = String.Empty;
            IndividualId = String.Empty;
        }

        public CaseInfo(String casename, String individual_id)
        {
            CaseName = casename;
            IndividualId = individual_id;
        }
    }

    public class ReceivedCaseDocInfo
    {
        public String IndividualId;
        public String CaseNo;
        public String FullDocNo;
        public Boolean? bAddOn;
        public Boolean? bFullDocReceived;
        public Boolean? bFullDocAddOn;
        public DateTime? FullDocReceivedDate;
        public Boolean? bNPF_Received;
        public Boolean? bNPF_AddOn;
        public DateTime? NPF_ReceivedDate;
        public Boolean? bIB_Received;
        public Boolean? bIB_AddOn;
        public DateTime? IB_ReceivedDate;
        public Boolean? bPoP_Received;
        public Boolean? bPoP_AddOn;
        public DateTime? PoP_ReceivedDate;
        public Boolean? bMedRecReceived;
        public Boolean? bMedRecAddOn;
        public DateTime? MedRecReceivedDate;
        public Boolean? bOtherDocReceived;
        public Boolean? bOtherAddOn;
        public DateTime? OtherDocReceivedDate;
        public DateTime? CreateDate;
        public int? CreateStaffId;
        public String CreateStaffName;

        public ReceivedCaseDocInfo()
        {
            IndividualId = null;
            CaseNo = null;
            FullDocNo = null;
            bAddOn = false;

            bFullDocReceived = false;
            bFullDocAddOn = false;
            FullDocReceivedDate = null;
            bNPF_Received = false;
            bNPF_AddOn = false;
            NPF_ReceivedDate = null;
            bIB_Received = false;
            bIB_AddOn = false;
            IB_ReceivedDate = null;
            bPoP_Received = false;
            bPoP_AddOn = false;
            PoP_ReceivedDate = null;
            bMedRecReceived = false;
            bMedRecAddOn = false;
            MedRecReceivedDate = null;
            bOtherDocReceived = false;
            bOtherAddOn = false;
            OtherDocReceivedDate = null;

            CreateDate = null;
            CreateStaffId = null;
            CreateStaffName = null;
        }
    }

    public class NPFReceivedInfo
    {
        public Boolean? NPFReceived;
        public DateTime? NPFReceivedDate;

        public NPFReceivedInfo()
        {
            NPFReceived = null;
            NPFReceivedDate = null;
        }
    }

    public class IBReceivedInfo
    {
        public Boolean? IBReceived;
        public DateTime? IBReceivedDate;

        public IBReceivedInfo()
        {
            IBReceived = null;
            IBReceivedDate = null;
        }
    }

    public class PoPReceivedInfo
    {
        public Boolean? PoPReceived;
        public DateTime? PoPReceivedDate;

        public PoPReceivedInfo()
        {
            PoPReceived = null;
            PoPReceivedDate = null;
        }
    }

    public class MedRecReceivedInfo
    {
        public Boolean? MedRecReceived;
        public DateTime? MedRecReceivedDate;

        public MedRecReceivedInfo()
        {
            MedRecReceived = null;
            MedRecReceivedDate = null;
        }
    }

    public class OtherDocReceivedInfo
    {
        public Boolean? OtherDocReceived;
        public DateTime? OtherDocReceivedDate;

        public OtherDocReceivedInfo()
        {
            OtherDocReceived = null;
            OtherDocReceivedDate = null;
        }
    }

    public class CasedInfoDetailed
    {
        public String CaseId;
        public String ParentCaseId;
        public String ContactId;
        public DateTime CreateDate;
        public DateTime ModificationDate;
        public int CreateStaff;
        public int ModifyingStaff;
        //public Boolean Status;
        public CaseStatus Status;
        public int NPF_Form;
        public int IB_Form;
        public int POP_Form;
        public int MedicalRecord_Form;
        public int Unknown_Form;
        //public Boolean NPF_Form;
        public String NPF_Form_File_Name;
        public String NPF_Form_Destination_File_Name;
        public DateTime? NPF_ReceivedDate;
        //public Boolean IB_Form;
        public String IB_Form_File_Name;
        public String IB_Form_Destination_File_Name;
        public DateTime? IB_ReceivedDate;
        //public Boolean POP_Form;
        public String POP_Form_File_Name;
        public String POP_Form_Destionation_File_Name;
        public DateTime? POP_ReceivedDate;
        //public Boolean MedRec_Form;
        public String MedRec_Form_File_Name;
        public String MedRec_Form_Destination_File_Name;
        public DateTime? MedRec_ReceivedDate;
        //public Boolean Unknown_Form;
        public String Unknown_Form_File_Name;
        public String Unknown_Form_Destination_File_Name;
        public DateTime? Unknown_ReceivedDate;
        public String Note;
        public String Log_Id;
        public Boolean AddBill_Form;
        public DateTime? AddBill_Received_Date;
        public String Remove_Log;
        public String Individual_Id;

        public CasedInfoDetailed()
        {
            CaseId = String.Empty;
            ParentCaseId = String.Empty;
            ContactId = String.Empty;
            CreateDate = DateTime.Today;
            ModificationDate = DateTime.Today;
            CreateStaff = 0;
            ModifyingStaff = 0;
            Status = CaseStatus.OnGoing;
            NPF_Form = 0;
            NPF_Form_File_Name = String.Empty;
            NPF_Form_Destination_File_Name = String.Empty;
            NPF_ReceivedDate = DateTime.Today;
            NPF_ReceivedDate = null;
            IB_Form_File_Name = String.Empty;
            IB_Form_Destination_File_Name = String.Empty;
            IB_ReceivedDate = null;
            //IB_ReceivedDate = DateTime.Today;
            POP_Form = 0;
            POP_Form_File_Name = String.Empty;
            POP_Form_Destionation_File_Name = String.Empty;
            POP_ReceivedDate = null;
            //POP_ReceivedDate = DateTime.Today;
            MedicalRecord_Form = 0;
            MedRec_Form_File_Name = String.Empty;
            MedRec_Form_Destination_File_Name = String.Empty;
            MedRec_ReceivedDate = null;
            //MedRec_ReceivedDate = DateTime.Today;
            Unknown_Form = 0;
            Unknown_Form_File_Name = String.Empty;
            Unknown_Form_Destination_File_Name = String.Empty;
            Unknown_ReceivedDate = null;
            //Unknown_ReceivedDate = DateTime.Today;
            Note = String.Empty;
            Log_Id = String.Empty;
            AddBill_Form = false;
            AddBill_Received_Date = null;
            //AddBill_Received_Date = DateTime.Today;
            Remove_Log = String.Empty;
            Individual_Id = String.Empty;
        }
    }

    public class MedicalBillDocType
    {
        public int? MedBillDocTypeCode;
        public String MedBillDocTypeValue;

        public MedicalBillDocType()
        {
            MedBillDocTypeCode = null;
            MedBillDocTypeValue = String.Empty;
        }
    }

    public class PendingReasonInfo
    {
        public int? SelectedId;
        public int? Id;
        public String PendingReason;

        public PendingReasonInfo()
        {
            SelectedId = null;
            Id = null;
            PendingReason = String.Empty;
        }

        public PendingReasonInfo(int id, string pending_reason)
        {
            Id = id;
            PendingReason = pending_reason;
        }

        public PendingReasonInfo(int selected_id, int id, string pending_reason)
        {
            SelectedId = selected_id;
            Id = id;
            PendingReason = pending_reason;
        }
    }

    public class IneligibleReasonInfo
    {
        public int? SelectedId;
        public int? Id;
        public String IneligibleReason;

        public IneligibleReasonInfo()
        {
            SelectedId = null;
            Id = null;
            IneligibleReason = String.Empty;
        }

        public IneligibleReasonInfo(int id, string ineligible_reason)
        {
            Id = id;
            IneligibleReason = ineligible_reason;
        }

        public IneligibleReasonInfo(int selected_id, int id, string ineligible_reason)
        {
            SelectedId = selected_id;
            Id = id;
            IneligibleReason = ineligible_reason;
        }
    }

    public class CheckPaymentCSVExportInfo
    {
        public String IndividualId;
        public String IndividualName;
        public String HouseholdRole;
        public String IncidentNo;
        public String IncidentProgram;
        public String SettlementNo;
        public DateTime TransactionDate;
        public String MembershipNo;
        public String PatientName;
        public String PatientFirstName;
        public String PatientMiddleName;
        public String PatientLastName;
        public String PrimaryName;
        public String PrimaryLastName;
        public String PrimaryFirstName;
        public String PrimaryMiddleName;
        public Decimal SettlementAmount;
        public DateTime ServiceDate;
        public String MedicalProvider;
        public String MedicalProviderId;
        public String AccountNoAtProvider;
        public String StreetAddress;
        public String City;
        public String State;
        public String Zip;

        public CheckPaymentCSVExportInfo()
        {
            IndividualId = String.Empty;
            HouseholdRole = String.Empty;
            IndividualName = String.Empty;
            IncidentNo = String.Empty;
            IncidentProgram = String.Empty;
            SettlementNo = String.Empty;
            PatientName = String.Empty;
            PatientFirstName = String.Empty;
            PatientMiddleName = String.Empty;
            PatientLastName = String.Empty;
            PrimaryName = String.Empty;
            MembershipNo = String.Empty;
            SettlementAmount = 0;
            MedicalProvider = String.Empty;
            MedicalProviderId = String.Empty;
            AccountNoAtProvider = String.Empty;
            StreetAddress = String.Empty;
            City = String.Empty;
            State = String.Empty;
            Zip = String.Empty;
        }
    }

    public class CheckPaymentInfo
    {
        public Boolean bIsPaid;
        public Boolean bIsExported;
        public Boolean bIsReviewed;
        public int? nExportedByID;
        public String IndividualName;
        public String IndividualId;
        public String HouseholdRole;
        public String PrimaryName;
        public String IncidentNo;
        public String ProgramName;
        public String IncidentProgram;
        public String MedicalBillNo;
        public Decimal MedicalBillAmount;
        public DateTime? ServiceDate;
        public String SettlementNo;
        public Decimal SettlementAmount;
        public String MedicalProviderId;
        public String MedicalProviderName;
        public String AccountNoAtMedProvider;
        public String ShippingStreet;
        public String ShippingCity;
        public String ShippingState;
        public String ShippingZip;
        public String SettlementType;
        public String CreatedBy;
        public String LastModifiedBy;
        public String MembershipStatus;
        public String MemebershipNo;

        public CheckPaymentInfo()
        {
            bIsPaid = false;
            bIsExported = false;
            bIsReviewed = false;
            nExportedByID = null;
            IndividualName = String.Empty;
            IndividualId = String.Empty;
            HouseholdRole = String.Empty;
            PrimaryName = String.Empty;
            IncidentNo = String.Empty;
            ProgramName = String.Empty;
            IncidentProgram = String.Empty;
            MedicalBillNo = String.Empty;
            MedicalBillAmount = 0;
            ServiceDate = null;
            SettlementNo = String.Empty;
            SettlementAmount = 0;
            MedicalProviderId = String.Empty;
            MedicalProviderName = String.Empty;
            AccountNoAtMedProvider = String.Empty;
            ShippingStreet = String.Empty;
            ShippingCity = String.Empty;
            ShippingState = String.Empty;
            ShippingZip = String.Empty;
            SettlementType = String.Empty;
            CreatedBy = String.Empty;
            LastModifiedBy = String.Empty;
            MembershipStatus = String.Empty;
            MemebershipNo = String.Empty;
        }
    }

    public class ACHPaymentInfo
    {
        public Boolean bIsPaid;
        public int nPaidBy;
        public String IndividualName;
        public String LastName;
        public String FirstName;
        public String MiddleName;
        public String IndividualId;
        public String MedicalBillNo;
        public String SettlementNo;
        public Decimal SettlementAmount;
        public String BankName;
        public String RoutingNumber;
        public String AccountNumber;
        public String AccountHolder;
        public String CreateStaffName;
        public String ModifiStaffName;
        public String MembershipStatus;
        public Boolean bIsExported;
        public int nExportedBy;
        public int nSettlementType;
        public Boolean IsReviewed;

        public ACHPaymentInfo()
        {
            bIsPaid = false;
            nPaidBy = 0;
            IndividualName = String.Empty;
            LastName = String.Empty;
            FirstName = String.Empty;
            MiddleName = String.Empty;
            IndividualId = String.Empty;
            MedicalBillNo = String.Empty;
            SettlementNo = String.Empty;
            SettlementAmount = 0;
            BankName = String.Empty;
            RoutingNumber = String.Empty;
            AccountNumber = String.Empty;
            AccountHolder = String.Empty;
            CreateStaffName = String.Empty;
            ModifiStaffName = String.Empty;
            MembershipStatus = String.Empty;
            bIsExported = false;
            nExportedBy = 0;
            nSettlementType = 0;
            IsReviewed = false;
        }
    }

    public class ACHPaymentCSVExportInfo
    {
        public String IndividualId;
        public String SettlementNo;
        public DateTime? TransactionDate;
        public String MembershipNo;
        public String IndividualName;
        public String LastName;
        public String FirstName;
        public String MiddleName;
        public String StreetAddress;
        public String City;
        public String State;
        public String Zip;
        public Decimal ExpenseAmount;
        public DateTime? ServiceDate;
        public String MedicalProviderId;
        public String MedicalProvider;
        public String AccountNoAtProvider;
        public String IncidentProgram;
        public String IncidentNo;
        
        public ACHPaymentCSVExportInfo()
        {
            IndividualId = String.Empty;
            TransactionDate = null;
            MembershipNo = String.Empty;
            IndividualName = String.Empty;
            LastName = String.Empty;
            FirstName = String.Empty;
            MiddleName = String.Empty;
            StreetAddress = String.Empty;
            City = String.Empty;
            State = String.Empty;
            Zip = String.Empty;
            ExpenseAmount = 0;
            ServiceDate = null;
            MedicalProviderId = String.Empty;
            MedicalProvider = String.Empty;
            AccountNoAtProvider = String.Empty;
            IncidentProgram = String.Empty;
            IncidentNo = String.Empty;
        }
    }

    public class ACHPaymentExportInfo
    {
        public String BankAccountHolderName;
        public String IndividualId;
        public String IndividualName;
        public String SettlementNumber;
        public Decimal Amount;
        public String RoutingNumber;
        public String AccountNumber;
        public String Description;
        public Boolean bMemberReimbursement;
        public String VendorId;

        public ACHPaymentExportInfo()
        {
            BankAccountHolderName = String.Empty;
            IndividualId = String.Empty;
            IndividualName = String.Empty;
            SettlementNumber = String.Empty;
            Amount = 0;
            RoutingNumber = String.Empty;
            AccountNumber = String.Empty;
            Description = "CMM NEEDS SHARED";
            bMemberReimbursement = false;
            VendorId = String.Empty;
        }
    }

    public class EmailContentInfo
    {
        public String EmailIncident;
        public String EmailSender;
        //public OfficeOutlook.Recipients EmailRecipients;
        //public OfficeOutlook.Recipient EmailRecipient;
        public String EmailRecipient;
        public String EmailBcc;
        public String EmailSubject;
        public String EmailBody;
        public List<String> lstEmailAttachmentFileNames;
        //public OfficeOutlook.Attachments EmailAttachments;
        //public OfficeOutlook.MailItem Email;

        public EmailContentInfo()
        {
            EmailIncident = String.Empty;
            EmailSender = String.Empty;
            EmailRecipient = String.Empty;
            EmailBcc = String.Empty;
            //EmailRecipients = null;
            //EmailRecipient = null;
            EmailSubject = String.Empty;
            EmailBody = String.Empty;
            lstEmailAttachmentFileNames = new List<string>();
        }
    }

    public class CommunicationContentInfo
    {
        public String IndividualId;
        public String CommunicationNo;
        public String CaseNo;
        public String IllnessNo;
        public String IncidentNo;
        public CommunicationType? CommType;
        public String CommunicationType;
        public String Subject;
        public String Body;
        public String Solution;
        public DateTime? CreateDate;
        public String CreatedBy;

        public CommunicationContentInfo()
        {
            IndividualId = String.Empty;
            CommunicationNo = String.Empty;
            CaseNo = String.Empty;
            IllnessNo = String.Empty;
            IncidentNo = String.Empty;
            CommType = null;
            CommunicationType = String.Empty;
            Subject = String.Empty;
            Body = String.Empty;
            Solution = String.Empty;
            CreateDate = null;
            CreatedBy = String.Empty;
        }
    }

    public class CreditCardPaymentInfo
    {
        public Boolean bIsPaid;
        public String IndividualName;
        public String MedicalProviderId;
        public String MedicalProviderName;
        public String MedicalProviderPhone;
        public String AccountNoAtProvider;
        public DateTime? IndividualBirthDate;
        public String SocialSecurityNumber;
        public String Sex;
        public String PrimaryName;
        public String MailingStreet;
        public String MailingCity;
        public String MailingState;
        public String MailingZip;
        public DateTime? ServiceDate;
        public String MedicalBillNo;
        public Decimal MedBillAmount;
        public String SettlementName;
        public Decimal SettlementAmount;
        public String MembershipStatus;
        public String SettlementNote;
        public String CreateStaffName;
        public String ModifyStaffName;
        public String IndividualID;
        public String MembershipNumber;

        public CreditCardPaymentInfo()
        {
            bIsPaid = false;
            IndividualName = String.Empty;
            MedicalProviderId = String.Empty;
            MedicalProviderName = String.Empty;
            MedicalProviderPhone = String.Empty;
            AccountNoAtProvider = String.Empty;
            SocialSecurityNumber = String.Empty;
            Sex = String.Empty;
            PrimaryName = String.Empty;
            MailingStreet = String.Empty;
            MailingCity = String.Empty;
            MailingState = String.Empty;
            MailingZip = String.Empty;
            MedicalBillNo = String.Empty;
            MedBillAmount = 0;
            SettlementName = String.Empty;
            SettlementAmount = 0;
            MembershipStatus = String.Empty;
            SettlementNote = String.Empty;
            CreateStaffName = String.Empty;
            ModifyStaffName = String.Empty;
            IndividualID = String.Empty;
            MembershipNumber = String.Empty;
        }
    }

    public class InactivePaymentInfo
    {
        public Boolean bIsPaid;
        public String IndividualId;
        public String IndividualName;
        public String SettlementNo;
        public String MembershipNo;
        public String MembershipStatus;
        public String State;
        public String Email;
        public String Phone;

        public InactivePaymentInfo()
        {
            bIsPaid = false;
            IndividualId = String.Empty;
            IndividualName = String.Empty;
            SettlementNo = String.Empty;
            MembershipNo = String.Empty;
            MembershipStatus = String.Empty;
            State = String.Empty;
            Email = String.Empty;
            Phone = String.Empty;
        }
    }

    /// <summary>
    ///
    /// </summary>

    public class UserInfo
    {
        public int? UserId;
        public String UserName;
        public String UserEmail;
        public UserRole UserRoleId;
        public DepartmentInfo departmentInfo;

        public UserInfo()
        {
            UserId = null;
            UserName = String.Empty;
            UserEmail = String.Empty;
            departmentInfo = new DepartmentInfo();
        }

        public UserInfo(int user_id, String user_name, String user_email, UserRole user_role_id, Department department_id)
        {
            UserId = user_id;
            UserName = user_name;
            UserEmail = user_email;
            UserRoleId = user_role_id;
            departmentInfo = new DepartmentInfo(department_id, String.Empty);
        }
    }

    public class DepartmentInfo
    {
        public Department DepartmentId;
        public String DepartmentName;

        public DepartmentInfo()
        {
            //DepartmentId = null;
            DepartmentName = String.Empty;
        }

        public DepartmentInfo(Department departmentId, String departmentName)
        {
            DepartmentId = departmentId;
            DepartmentName = departmentName;
        }
    }

    public class CaseDocTypeInfo
    {
        public int? CaseDocTypeCode;
        public String CaseDocTypeValue;

        public CaseDocTypeInfo()
        {
            CaseDocTypeCode = null;
            CaseDocTypeValue = String.Empty;
        }

        public CaseDocTypeInfo (int doc_type_code, String doc_type_value)
        {
            CaseDocTypeCode = doc_type_code;
            CaseDocTypeValue = doc_type_value;
        }
    }

    public class CaseDocInfo
    {
        public String CaseDocNo;
        public String CaseName;
        public int? DocumentTypeId;
        public DateTime? ReceivedDate;
        public String DestinationFilePath;
        public Boolean bAddOn;
        public String Note;
        public int? CreateStaffId;
        public DateTime? CreateDate;

        public CaseDocInfo()
        {
            CaseDocNo = String.Empty;
            CaseName = String.Empty;
            DocumentTypeId = null;
            ReceivedDate = null;
            DestinationFilePath = String.Empty;
            bAddOn = false;
            Note = String.Empty;
            CreateStaffId = null;
            CreateDate = null;
        }

        public CaseDocInfo(String case_doc_no,
                           String case_name, 
                           int doc_type_id, 
                           DateTime received_date, 
                           String destination_file_path,
                           Boolean add_on,
                           String note, 
                           int create_staff_id, 
                           DateTime create_date)
        {
            CaseDocNo = case_doc_no;
            CaseName = case_name;
            DocumentTypeId = doc_type_id;
            ReceivedDate = received_date;
            DestinationFilePath = destination_file_path;
            bAddOn = add_on;
            Note = note;
            CreateStaffId = create_staff_id;
            CreateDate = create_date;
        }
    }

    public class MissingCaseDocInfo
    {
        public String MissingCaseDocNo;
        public String CaseName;
        public DateTime? CaseCreateDate;
        public Boolean? bAddOn;
        public CaseDocType? DocumentTypeId;
        public Boolean? bMissingDocRequested;
        public DateTime? CreateDate;
        public int? CreateStaffId;
        public String Note;

        public MissingCaseDocInfo()
        {
            MissingCaseDocNo = null;
            CaseName = null;
            CaseCreateDate = null;
            bAddOn = null;
            DocumentTypeId = null;
            bMissingDocRequested = null;
            CreateDate = null;
            CreateStaffId = null;
            Note = null;
        }
        
        public MissingCaseDocInfo(String missing_casedoc_no, 
                                  String case_name, 
                                  DateTime case_create_date, 
                                  Boolean add_on, 
                                  CaseDocType doc_type,
                                  Boolean missing_doc_requested, 
                                  DateTime create_date, 
                                  int create_staff_id, 
                                  String note)
        {
            MissingCaseDocNo = missing_casedoc_no;
            CaseName = case_name;
            CaseCreateDate = case_create_date;
            bAddOn = add_on;
            DocumentTypeId = doc_type;
            bMissingDocRequested = missing_doc_requested;
            CreateDate = create_date;
            CreateStaffId = create_staff_id;
            Note = note;
        }
    }

    public class CaseDocReceived
    {
        public String CaseNo;
        public String IndividualId;
        public Boolean? bFullDocReceived;
        public Boolean? bNPFReceived;
        public Boolean? bIBReceived;
        public Boolean? bPoPReceived;
        public Boolean? bMedRecReceived;
        public Boolean? bOtherDocReceived;

        public CaseDocReceived()
        {
            CaseNo = null;
            IndividualId = null;
            bFullDocReceived = false;
            bNPFReceived = false;
            bIBReceived = false;
            bPoPReceived = false;
            bMedRecReceived = false;
            bOtherDocReceived = false;
        }
    }

    public class MedicalDocInfo
    {
        public String MedBillDocNo;
        public String MedBillNo;
        public String FileName;
        public String DestinationFilePathName;
        public DateTime? CreateDate;
        public int? CreatedBy;

        public MedicalDocInfo()
        {
            MedBillDocNo = String.Empty;
            MedBillNo = String.Empty;
            FileName = String.Empty;
            DestinationFilePathName = String.Empty;
            CreateDate = null;
            CreatedBy = null;
        }
        public MedicalDocInfo(String medbill_doc_no,
                              String medbill_no, 
                              String source_file_name, 
                              String destination_path, 
                              DateTime create_date, 
                              int created_by)
        {
            MedBillDocNo = medbill_doc_no;
            MedBillNo = medbill_no;
            FileName = source_file_name;
            DestinationFilePathName = destination_path;
            CreateDate = create_date;
            CreatedBy = created_by;
        }
    }

    /// <summary>
    /// classes for BlueSheet
    /// </summary>
    /// 

    public class MedBillCheckInfo
    {
        public String MedicalBillNo;
        public String MedicalProviderName;
        public String CheckNo;
        public DateTime? CheckDate;
        public Double Amount;
        public String SettlementName;
        public String ContactPrimaryName;
        public String ContactMailingStreet;
        public String ContactMailingCity;
        public String ContactMailingState;
        public String ContactMailingZip;
        public String MembershipName;
        public String ContactName;
        public String ContactLastName;
        public String ContactMiddleName;
        public String ContactFirstName;
        public String IndividualId;

        public MedBillCheckInfo()
        {
            MedicalBillNo = String.Empty;
            MedicalProviderName = String.Empty;
            CheckNo = String.Empty;
            CheckDate = null;
            Amount = 0;
            SettlementName = String.Empty;
            ContactPrimaryName = String.Empty;
            ContactMailingStreet = String.Empty;
            ContactMailingCity = String.Empty;
            ContactMailingState = String.Empty;
            ContactMailingZip = String.Empty;
            MembershipName = String.Empty;
            ContactName = String.Empty;
            ContactLastName = String.Empty;
            ContactMiddleName = String.Empty;
            ContactFirstName = String.Empty;
            IndividualId = String.Empty;
        }
    }

    public class MedBillACHInfo
    {
        public String MedicalBillNo;
        public String MedicalProviderName;
        public String ACH_No;
        public DateTime? ACH_Date;
        public Double Amount;
        public String SettlementName;
        public String ContactPrimaryName;
        public String ContactMailingStreet;
        public String ContactMailingCity;
        public String ContactMailingState;
        public String ContactMailingZip;
        public String MembershipName;
        public String ContactName;
        public String ContactLastName;
        public String ContactMiddleName;
        public String ContactFirstName;
        public String IndividualId;

        public MedBillACHInfo()
        {
            MedicalBillNo = String.Empty;
            MedicalProviderName = String.Empty;
            ACH_No = String.Empty;
            ACH_Date = null;
            Amount = 0;
            SettlementName = String.Empty;
            ContactPrimaryName = String.Empty;
            ContactMailingStreet = String.Empty;
            ContactMailingCity = String.Empty;
            ContactMailingState = String.Empty;
            ContactMailingZip = String.Empty;
            MembershipName = String.Empty;
            ContactName = String.Empty;
            ContactLastName = String.Empty;
            ContactMiddleName = String.Empty;
            ContactFirstName = String.Empty;
            IndividualId = String.Empty;
        }
    }

    public class MedBillCreditCardPaymentInfo
    {
        public String MedicalBillNo;
        public String MedicalProviderName;
        public String CreditCard_No;
        public DateTime? Transaction_Date;
        public Double Amount;
        public String SettlementName;
        public String ContactPrimaryName;
        public String ContactMailingStreet;
        public String ContactMailingCity;
        public String ContactMailingState;
        public String ContactMailingZip;
        public String MembershipName;
        public String ContactName;
        public String ContactLastName;
        public String ContactMiddleName;
        public String ContactFirstName;
        public String IndividualId;

        public MedBillCreditCardPaymentInfo()
        {
            MedicalBillNo = String.Empty;
            MedicalProviderName = String.Empty;
            CreditCard_No = String.Empty;
            Transaction_Date = null;
            Amount = 0;
            SettlementName = String.Empty;
            ContactPrimaryName = String.Empty;
            ContactMailingStreet = String.Empty;
            ContactMailingCity = String.Empty;
            ContactMailingState = String.Empty;
            ContactMailingZip = String.Empty;
            MembershipName = String.Empty;
            ContactName = String.Empty;
            ContactLastName = String.Empty;
            ContactMiddleName = String.Empty;
            ContactFirstName = String.Empty;
            IndividualId = String.Empty;
        }
    }

    public class IncidentInfo
    {
        public String IncidentNo;
        public String SettlementType;
        public String MedicalProviderName;
        public String ContactName;

        public IncidentInfo()
        {
            IncidentNo = String.Empty;
            SettlementType = String.Empty;
            MedicalProviderName = String.Empty;
            ContactName = String.Empty;
        }
    }
        


    public class SortedFieldBlueSheet
    {

        public String Field = String.Empty;
        public EnumSorted Sorted = EnumSorted.NotSorted;

        public SortedFieldBlueSheet()
        {
            Field = String.Empty;
            Sorted = EnumSorted.NotSorted;
        }
    }

    public class MedicalExpenseBlueSheet
    {
        public double? BillAmount;
        public double? MemberDiscount;
        public double? CMMDiscount;
        public double? PersonalResponsibility;
        public double? CMMProviderPayment;
        public double? PastCMMProviderPayment;
        public double? PastReimbursement;
        public double? Reimbursement;
        public double? Balance;

        public MedicalExpenseBlueSheet()
        {
            BillAmount = 0;
            MemberDiscount = 0;
            CMMDiscount = 0;
            PersonalResponsibility = 0;
            CMMProviderPayment = 0;
            PastCMMProviderPayment = 0;
            PastReimbursement = 0;
            Reimbursement = 0;
            Balance = 0;
        }

        public MedicalExpenseBlueSheet(double? billAmount,
                              double? memberDiscount,
                              double? cmmDiscount,
                              double? personalResponsibility,
                              double? cmmProviderPayment,
                              double? pastCMMProviderPayment,
                              double? pastReimbursement,
                              double? reimbursement,
                              double? balance)
        {
            BillAmount = billAmount;
            MemberDiscount = memberDiscount;
            CMMDiscount = cmmDiscount;
            PersonalResponsibility = personalResponsibility;
            CMMProviderPayment = cmmProviderPayment;
            PastCMMProviderPayment = pastCMMProviderPayment;
            PastReimbursement = pastReimbursement;
            Reimbursement = reimbursement;
            Balance = balance;
        }
    }



    public class MedicalExpenseIneligibleBlueSheet
    {
        public double? BillAmount;
        public double? AmountIneligible;

        public MedicalExpenseIneligibleBlueSheet()
        {
            BillAmount = 0;
            AmountIneligible = 0;
        }

        public MedicalExpenseIneligibleBlueSheet(double? billAmount,
                                        double? amountIneligible)
        {
            BillAmount = billAmount;
            AmountIneligible = amountIneligible;
        }
    }

    public class MedicalExpensePartiallyIneligibleBlueSheet
    {
        public String INCD;
        public String PatientName;
        public String MedBill;
        public DateTime? ServiceDate;
        public DateTime? ReceiveDate;
        public String MedicalProvider;
        public Double? BillAmount;
        public Double? IneligibleAmount;
        public String IneligibleReason;

        public MedicalExpensePartiallyIneligibleBlueSheet()
        {
            INCD = String.Empty;
            PatientName = String.Empty;
            MedBill = String.Empty;
            ServiceDate = null;
            ReceiveDate = null;
            MedicalProvider = String.Empty;
            BillAmount = 0;
            IneligibleAmount = 0;
            IneligibleReason = String.Empty;
        }
    }

    public class CMMPendingPaymentBlueSheet
    {
        public double? BillAmount;
        public double? MemberDiscount;
        public double? CMMDiscount;
        public double? PersonalResponsibility;
        public double? SharedAmount;
        public double? AmountWillBeShared;

        public CMMPendingPaymentBlueSheet()
        {
            BillAmount = 0;
            MemberDiscount = 0;
            CMMDiscount = 0;
            PersonalResponsibility = 0;
            SharedAmount = 0;
            AmountWillBeShared = 0;
        }

        public CMMPendingPaymentBlueSheet(double? billAmount,
                                 double? memberDiscount,
                                 double? cmmDiscount,
                                 double? personalResponsibility,
                                 double? sharedAmount,
                                 double? amountWillBeShared)
        {
            BillAmount = billAmount;
            MemberDiscount = memberDiscount;
            CMMDiscount = cmmDiscount;
            PersonalResponsibility = personalResponsibility;
            SharedAmount = sharedAmount;
            AmountWillBeShared = amountWillBeShared;
        }
    }

    public class PendingBlueSheet
    {
        public double? BillAmount;
        public double? Balance;
        public double? MemberDiscount;
        public double? CMMDiscount;
        public double? SharedAmount;
        public double? PendingAmount;

        public PendingBlueSheet()
        {
            BillAmount = 0;
            Balance = 0;
            MemberDiscount = 0;
            CMMDiscount = 0;
            SharedAmount = 0;
            PendingAmount = 0;
        }

        public PendingBlueSheet(double? billAmount,
                       double? balance,
                       double? memberDiscount,
                       double? cmmDiscount,
                       double? sharedAmount,
                       double? pendingAmount)
        {
            BillAmount = billAmount;
            Balance = balance;
            MemberDiscount = memberDiscount;
            CMMDiscount = cmmDiscount;
            SharedAmount = sharedAmount;
            PendingAmount = pendingAmount;
        }
    }

    public class PaidMedicalExpenseTableRowBlueSheet
    {
        //public String CheckNo;
        //public String Issue_Date;
        //public String INCD;
        public String PatientName;
        public String MED_BILL;
        public DateTime? Bill_Date;
        public String Medical_Provider;
        public String Bill_Amount;
        public String Member_Discount;
        public String CMM_Discount;
        public String Personal_Responsibility;
        public String CMM_Provider_Payment;
        public String PastCMM_Provider_Payment;
        public String PastReimbursement;
        public String Reimbursement;
        public String Balance;

        public PaidMedicalExpenseTableRowBlueSheet()
        {
            //CheckNo = String.Empty;
            //Issue_Date = String.Empty;
            PatientName = String.Empty;
            MED_BILL = String.Empty;
            Bill_Date = null;
            Medical_Provider = String.Empty;
            Bill_Amount = String.Empty;
            Member_Discount = String.Empty;
            CMM_Discount = String.Empty;
            Personal_Responsibility = String.Empty;
            CMM_Provider_Payment = String.Empty;
            PastCMM_Provider_Payment = String.Empty;
            PastReimbursement = String.Empty;
            Reimbursement = String.Empty;
            Balance = String.Empty;
        }
    }

    public class CMMPendingPaymentTableRowBlueSheet
    {
        //public String INCD;
        public String PatientName;
        public String MED_BILL;
        public String Bill_Date;
        public String Due_Date;
        public String Medical_Provider;
        public String Bill_Amount;
        public String Member_Discount;
        public String CMM_Discount;
        public String PersonalResponsibility;
        public String Shared_Amount;
        public String Balance;

        public CMMPendingPaymentTableRowBlueSheet()
        {
            PatientName = String.Empty;
            MED_BILL = String.Empty;
            Bill_Date = String.Empty;
            Due_Date = String.Empty;
            Medical_Provider = String.Empty;
            Bill_Amount = String.Empty;
            Member_Discount = String.Empty;
            CMM_Discount = String.Empty;
            PersonalResponsibility = String.Empty;
            Shared_Amount = String.Empty;
            Balance = String.Empty;
        }
    }

    public class PendingTableRowBlueSheet
    {
        //public String INCD;
        public String PatientName;
        public String MED_BILL;
        public String Bill_Date;
        public String Due_Date;
        public String Medical_Provider;
        public String Bill_Amount;
        public String Balance;
        public String Member_Discount;
        public String CMM_Discount;
        public String Shared_Amount;
        public String Pending_Reason;

        public PendingTableRowBlueSheet()
        {
            PatientName = String.Empty;
            MED_BILL = String.Empty;
            Bill_Date = String.Empty;
            Due_Date = String.Empty;
            Medical_Provider = String.Empty;
            Bill_Amount = String.Empty;
            Balance = String.Empty;
            Member_Discount = String.Empty;
            CMM_Discount = String.Empty;
            Shared_Amount = String.Empty;
            Pending_Reason = String.Empty;
        }
    }

    public class BillIneligibleTableRowBlueSheet
    {
        //public String INCD;
        public String PatientName;
        public String MED_BILL;
        public String Bill_Date;
        public String Received_Date;
        public String Medical_Provider;
        public String Bill_Amount;
        public String Amount_Ineligible;
        public String Ineligible_Reason;

        public BillIneligibleTableRowBlueSheet()
        {
            PatientName = String.Empty;
            MED_BILL = String.Empty;
            Bill_Date = String.Empty;
            Received_Date = String.Empty;
            Medical_Provider = String.Empty;
            Bill_Amount = String.Empty;
            Amount_Ineligible = String.Empty;
            Ineligible_Reason = String.Empty;
        }
    }

    public class BillIneligibleRowBlueSheet
    {
        public Double? Bill_Amount;
        public Double? Amount_Ineligible;

        public BillIneligibleRowBlueSheet()
        {
            Bill_Amount = 0;
            Amount_Ineligible = 0;
        }
    }

    public class CheckInfoBlueSheet
    {
        public String CheckNumber { get; set; }
        public DateTime dtCheckIssueDate { get; set; }
        public Double? CheckAmount { get; set; }
        public String PaidTo { get; set; }

        public CheckInfoBlueSheet()
        {
            CheckNumber = String.Empty;
            dtCheckIssueDate = DateTime.Today;
            CheckAmount = 0;
            PaidTo = String.Empty;

        }

        public CheckInfoBlueSheet(String checkno, DateTime issuedate, Double amount, String paidTo)
        {
            CheckNumber = checkno;
            dtCheckIssueDate = issuedate;
            CheckAmount = amount;
            PaidTo = paidTo;
        }
    }

    public class ACHInfoBlueSheet
    {
        public String ACHNumber { get; set; }
        public DateTime dtACHDate { get; set; }
        public Double? ACHAmount { get; set; }
        public String PaidTo { get; set; }

        public ACHInfoBlueSheet()
        {
            ACHNumber = String.Empty;
            dtACHDate = DateTime.Today;
            ACHAmount = 0;
            PaidTo = String.Empty;
        }

        public ACHInfoBlueSheet(String ach_no, DateTime ach_date, Double amount, String paidTo)
        {
            ACHNumber = ach_no;
            dtACHDate = ach_date;
            ACHAmount = amount;
            PaidTo = paidTo;
        }
    }

    public class CreditCardPaymentInfoBlueSheet
    {
        public DateTime dtPaymentDate { get; set; }
        public Double? CCPaymentAmount { get; set; }
        public String PaidTo { get; set; }

        public CreditCardPaymentInfoBlueSheet()
        {
            dtPaymentDate = DateTime.Today;
            CCPaymentAmount = 0;
            PaidTo = String.Empty;
        }

        public CreditCardPaymentInfoBlueSheet(DateTime cc_payment_date, Double amount, String paidTo)
        {
            dtPaymentDate = cc_payment_date;
            CCPaymentAmount = amount;
            PaidTo = paidTo;
        }
    }

    public class PersonalResponsibilityTotalInfoBlueSheet
    {
        public String IncidentNo;
        public DateTime? IncidentOccurrenceDate;
        public String ICD10CodeDescription;
        public Decimal PersonalResponsibilityTotal;

        public PersonalResponsibilityTotalInfoBlueSheet()
        {
            IncidentNo = String.Empty;
            IncidentOccurrenceDate = null;
            ICD10CodeDescription = String.Empty;
            PersonalResponsibilityTotal = 0;
        }

        public PersonalResponsibilityTotalInfoBlueSheet(String incident_no, DateTime incident_occurrence_date, Decimal personal_responsibility_total)
        {
            IncidentNo = incident_no;
            IncidentOccurrenceDate = incident_occurrence_date;
            PersonalResponsibilityTotal = personal_responsibility_total;
        }

        public PersonalResponsibilityTotalInfoBlueSheet(String incident_no, DateTime incident_occurrence_date, String icd10_description, Decimal personal_responsibility_total)
        {
            IncidentNo = incident_no;
            IncidentOccurrenceDate = incident_occurrence_date;
            ICD10CodeDescription = icd10_description;
            PersonalResponsibilityTotal = personal_responsibility_total;
        }
    }

    public class PersonalResponsibilityInfoBlueSheet
    {
        public String MedBillName;
        public DateTime? BillDate;
        public String MedicalProvider;
        public Double BillAmount;
        public String Type;
        public String PersonalResponsibilityType;
        public Double? MemberPayment;
        public Double? MemberDiscount;
        public Double? ThirdPartyDiscount;
        public Double PersonalResponsibilityTotal;

        public PersonalResponsibilityInfoBlueSheet()
        {
            MedBillName = String.Empty;
            BillDate = null;
            MedicalProvider = String.Empty;
            BillAmount = 0;
            Type = String.Empty;
            PersonalResponsibilityType = String.Empty;
            MemberPayment = null;
            MemberDiscount = null;
            ThirdPartyDiscount = null;
            PersonalResponsibilityTotal = 0;
        }

        public PersonalResponsibilityInfoBlueSheet(String medbill_name,
                                          DateTime bill_date,
                                          String medical_provider,
                                          Double bill_amount,
                                          String type,
                                          String personal_responsibility_type,
                                          Double member_payment,
                                          Double member_discount,
                                          Double third_party_discount,
                                          Double personal_responsibility_total)
        {
            MedBillName = medbill_name;
            BillDate = bill_date;
            MedicalProvider = medical_provider;
            BillAmount = bill_amount;
            Type = type;
            PersonalResponsibilityType = personal_responsibility_type;
            MemberPayment = member_payment;
            MemberDiscount = member_discount;
            ThirdPartyDiscount = third_party_discount;
            PersonalResponsibilityTotal = personal_responsibility_total;
        }
    }

    public class SettlementIneligibleInfoBlueSheet
    {
        public String MedBillName;
        public DateTime? BillDate;
        public String MedicalProvider;
        public Double BillAmount;
        public String Type;
        public Double? IneligibleAmount;
        public String IneligibleReason;

        public SettlementIneligibleInfoBlueSheet()
        {
            MedBillName = String.Empty;
            BillDate = null;
            MedicalProvider = String.Empty;
            BillAmount = 0;
            Type = String.Empty;
            IneligibleAmount = 0;
            IneligibleReason = String.Empty;
        }

        public SettlementIneligibleInfoBlueSheet(String medbill_name,
                                        DateTime bill_date,
                                        String medical_provider,
                                        Double bill_amount,
                                        String type,
                                        Double ineligible_amount,
                                        String ineligible_reason)
        {
            MedBillName = medbill_name;
            BillDate = bill_date;
            MedicalProvider = medical_provider;
            BillAmount = bill_amount;
            Type = type;
            IneligibleAmount = ineligible_amount;
            IneligibleReason = ineligible_reason;
        }
    }

    public class PersonalResponsibilityExpenseBlueSheet
    {
        public Double BillAmount;
        //public Double SettlementAmount;
        public Double MemberPayment;
        public Double MemberDiscount;
        public Double ThirdPartyDiscount;
        public Double PersonalResponsiblityTotal;

        public PersonalResponsibilityExpenseBlueSheet()
        {
            BillAmount = 0;
            //SettlementAmount = 0;
            MemberPayment = 0;
            MemberDiscount = 0;
            ThirdPartyDiscount = 0;
            PersonalResponsiblityTotal = 0;
        }

        public PersonalResponsibilityExpenseBlueSheet(Double bill_amount, Double member_payment, Double member_discount, Double third_party_discount, Double personal_responsibility_total)
        {
            BillAmount = bill_amount;
            //SettlementAmount = settlement_amount;
            MemberPayment = member_payment;
            MemberDiscount = member_discount;
            ThirdPartyDiscount = third_party_discount;
            PersonalResponsiblityTotal = personal_responsibility_total;
        }
    }

    public class IncidentBlueSheet : IEquatable<IncidentBlueSheet>, IComparable<IncidentBlueSheet>
    {
        public String Name { get; set; }
        public String PatientName { get; set; }
        public String ICD10_Code { get; set; }

        public IncidentBlueSheet()
        {
            Name = String.Empty;
            PatientName = String.Empty;
            ICD10_Code = String.Empty;
        }
        public IncidentBlueSheet(String name, String patientName, String icd10_code)
        {
            Name = name;
            PatientName = patientName;
            ICD10_Code = icd10_code;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            IncidentBlueSheet objIncd = obj as IncidentBlueSheet;
            if (objIncd == null) return false;
            else return Equals(objIncd);
        }

        public bool Equals(IncidentBlueSheet incd)
        {
            if (incd == null) return false;
            return (this.Name.Equals(incd.Name));
        }

        public int SortByNameAscending(String Name1, String Name2)
        {
            return Name1.CompareTo(Name2);
        }

        public int CompareTo(IncidentBlueSheet compareIncd)
        {
            if (compareIncd == null)
                return 1;
            else
                return this.Name.CompareTo(compareIncd.Name);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    ///////////////////////////////////////////////////////////////////////////
    // DateTimePickerColumn for DataGridView

    public class CalendarColumn : DataGridViewColumn
    {
        public CalendarColumn() : base(new CalendarCell())
        {
        }

        public override DataGridViewCell CellTemplate
        {
            get
            {
                return base.CellTemplate;
            }
            set
            {
                // Ensure that the cell used for the template is a CalendarCell.
                if (value != null &&
                    !value.GetType().IsAssignableFrom(typeof(CalendarCell)))
                {
                    throw new InvalidCastException("Must be a CalendarCell");
                }
                base.CellTemplate = value;
            }
        }
    }

    public class CalendarCell : DataGridViewTextBoxCell
    {

        public CalendarCell()
            : base()
        {
            // Use the short date format.
            this.Style.Format = "d";
        }

        public override void InitializeEditingControl(int rowIndex, object
            initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            // Set the value of the editing control to the current cell value.
            base.InitializeEditingControl(rowIndex, initialFormattedValue,
                dataGridViewCellStyle);
            CalendarEditingControl ctl =
                DataGridView.EditingControl as CalendarEditingControl;
            // Use the default row value when Value property is null.
            if (this.Value == null)
            {
                ctl.Value = (DateTime)this.DefaultNewRowValue;
            }
            //else  : original code
            else if (this.Value.ToString() != String.Empty)
            {
                //ctl.Value = (DateTime)this.Value;
                ctl.Value = DateTime.Parse(this.Value.ToString());
            }
        }

        public override Type EditType
        {
            get
            {
                // Return the type of the editing control that CalendarCell uses.
                return typeof(CalendarEditingControl);
            }
        }

        public override Type ValueType
        {
            get
            {
                // Return the type of the value that CalendarCell contains.

                return typeof(DateTime);
            }
        }

        public override object DefaultNewRowValue
        {
            get
            {
                // Use the current date and time as the default value.
                return DateTime.Now;
            }
        }
    }

    class CalendarEditingControl : DateTimePicker, IDataGridViewEditingControl
    {
        DataGridView dataGridView;
        private bool valueChanged = false;
        int rowIndex;

        public CalendarEditingControl()
        {
            this.Format = DateTimePickerFormat.Short;
        }

        // Implements the IDataGridViewEditingControl.EditingControlFormattedValue 
        // property.
        public object EditingControlFormattedValue
        {
            get
            {
                return this.Value.ToShortDateString();
            }
            set
            {
                if (value is String)
                {
                    try
                    {
                        // This will throw an exception of the string is 
                        // null, empty, or not in the format of a date.
                        this.Value = DateTime.Parse((String)value);
                    }
                    catch
                    {
                        // In the case of an exception, just use the 
                        // default value so we're not left with a null
                        // value.
                        this.Value = DateTime.Now;
                    }
                }
            }
        }

        // Implements the 
        // IDataGridViewEditingControl.GetEditingControlFormattedValue method.
        public object GetEditingControlFormattedValue(
            DataGridViewDataErrorContexts context)
        {
            return EditingControlFormattedValue;
        }

        // Implements the 
        // IDataGridViewEditingControl.ApplyCellStyleToEditingControl method.
        public void ApplyCellStyleToEditingControl(
            DataGridViewCellStyle dataGridViewCellStyle)
        {
            this.Font = dataGridViewCellStyle.Font;
            this.CalendarForeColor = dataGridViewCellStyle.ForeColor;
            this.CalendarMonthBackground = dataGridViewCellStyle.BackColor;
        }

        // Implements the IDataGridViewEditingControl.EditingControlRowIndex 
        // property.
        public int EditingControlRowIndex
        {
            get
            {
                return rowIndex;
            }
            set
            {
                rowIndex = value;
            }
        }

        // Implements the IDataGridViewEditingControl.EditingControlWantsInputKey 
        // method.
        public bool EditingControlWantsInputKey(
            Keys key, bool dataGridViewWantsInputKey)
        {
            // Let the DateTimePicker handle the keys listed.
            switch (key & Keys.KeyCode)
            {
                case Keys.Left:
                case Keys.Up:
                case Keys.Down:
                case Keys.Right:
                case Keys.Home:
                case Keys.End:
                case Keys.PageDown:
                case Keys.PageUp:
                    return true;
                default:
                    return !dataGridViewWantsInputKey;
            }
        }

        // Implements the IDataGridViewEditingControl.PrepareEditingControlForEdit 
        // method.
        public void PrepareEditingControlForEdit(bool selectAll)
        {
            // No preparation needs to be done.
        }

        // Implements the IDataGridViewEditingControl
        // .RepositionEditingControlOnValueChange property.
        public bool RepositionEditingControlOnValueChange
        {
            get
            {
                return false;
            }
        }

        // Implements the IDataGridViewEditingControl
        // .EditingControlDataGridView property.
        public DataGridView EditingControlDataGridView
        {
            get
            {
                return dataGridView;
            }
            set
            {
                dataGridView = value;
            }
        }

        // Implements the IDataGridViewEditingControl
        // .EditingControlValueChanged property.
        public bool EditingControlValueChanged
        {
            get
            {
                return valueChanged;
            }
            set
            {
                valueChanged = value;
            }
        }

        // Implements the IDataGridViewEditingControl
        // .EditingPanelCursor property.
        public Cursor EditingPanelCursor
        {
            get
            {
                return base.Cursor;
            }
        }

        protected override void OnValueChanged(EventArgs eventargs)
        {
            // Notify the DataGridView that the contents of the cell
            // have changed.
            valueChanged = true;
            this.EditingControlDataGridView.NotifyCurrentCellDirty(true);
            base.OnValueChanged(eventargs);
        }
    }
}
