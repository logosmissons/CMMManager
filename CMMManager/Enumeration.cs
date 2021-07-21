using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMMManager
{
    public enum SqlConnectionOpen { RNConn, RNConn2, RNConn3 };
    public enum IllnessOption { Select, Close, Cancel };
    public enum IncidentOption { Select, Close, Cancel };
    public enum IncidentStatusCode { Under_Progress, Under_Review, Pending, Approved, Closed };
    public enum CaseStatus { OnGoing, Processing, Closed };
    public enum SettlementType { SelfPayDiscount = 1, ThirdPartyDiscount, MemberPayment, CMMProviderPayment, CMMDiscount, CMMMemberReimbursement, Ineligible, MedicalProviderRefund, PRReimbursement, MemberDiscount, ReverseTransaction };
    public enum PaymentMethodExport { Check = 1, CreditCard, ACH_Banking };
    public enum ProgramNameExport { GoldPlus = 0, Gold, Silver, Bronze, GoldMedi_I, GoldMedi_II };
    public enum IncidentProgramName { GoldPlus = 0, Gold, Silver, Bronze, GoldMedi_I, GoldMedi_II };
    public enum PaymentTypeExport { MemberReimbursement, PR_Reimbursement, ProviderPayment };

    public enum MemberStatus { Applied = 1, Hold = 2, Active = 3, PastDue = 4, Inactive = 5, Cancelled = 6, Imported = 7,
                               Deleted = 8, Pending = 9, TerminatedByCMM = 10, CancelledReq = 11, Incomplete = 12, CancelledCompleted = 13 }

    //public enum MemberStatus
    //{
    //    Applied = 1, Hold = 2, Active = 3, PastDue = 4, Inactive = 5, CancelledByMember = 6, Imported = 7,
    //    Deleted = 8, Pending = 9, TerminatedByCMM = 10, CancelledReq = 11, Incomplete = 12, CancelledByMember2 = 13
    //}

    //public enum MedicalBillType { MedicalBill = 1, Prescription = 2, PhysicalTherapy = 3, MedicalRecord = 4, LifePlan = 5, TestRequestByCMM = 6 };
    public enum MedicalBillType { MedicalBill = 1, Prescription = 2, NewBorn = 3, MedicalRecord = 4, LifePlan = 5, TestRequestByCMM = 6, PhysicalTherapy = 7 };
    public enum EnumPrescriptionType { MedicalBill = 0, Prescription = 1 };
    public enum BeneficiaryType { MedicalBill = 0, Beneficiary = 1 };
    public enum MedicalBillStatus { Pending = 0, CMMPendingPayment = 1, Closed = 2, Ineligible = 3, PartiallyIneligible = 4 };
    public enum IncidentType { All, WellBeing, Incident };

    public enum MedBillSortedInCaseTab { NotSorted, SortedAsc, SortedDesc };
    public enum MedBillSortedInMedBillViewTab { NotSorted, SortedAsc, SortedDesc };
    public enum PrescriptionSorted { NotSorted, SortedAsc, SortedDesc };
    public enum PhysicalTherapySorted { NotSorted, SortedAsc, SortedDesc };

    // enumeration for program change history
    public enum IndividualPlan { Bronze = 0, Silver, Gold, GoldPlus, GoldMedi_I, GoldMedi_II };
    public enum IllnessProgram { Bronze = 0, Silver, Gold, GoldPlus, GoldMedi_I, GoldMedi_II };
    public enum IncidentProgram { Bronze = 0, Silver, Gold, GoldPlus, GoldMedi_I, GoldMedi_II };

    public enum RelatedToTable { Membership, Case, Illness, Incident, MedicalBill, Settlement };
    public enum TaskStatus { NotStarted, InProgress, Completed, WaitingOnSomeoneElse, Deferred, Solved, Checked };
    public enum TaskPriority { High, Normal, Low };
    public enum TaskMode { AddNew, EditInRNManagerDashboard, EditInRNStaffDashboard, EditInNPManagerDashboard, EditInNPStaffDashboard, EditInMedBill, EditInCase, EditInIndividual, Reply,
                           EditInFDManagerDashboard, EditInFDStaffDashboard };

    public enum UserRole { Administrator = 0, FDManager, RNManager, NPManager, FDStaff, RNStaff, NPStaff, MSManager, MSStaff, Executive,
                                FDAssistantManager, RNAssistantManager, NPAssistantManager, MSAssistantManager, SuperAdmin = 20 };
    public enum TaskUserRole { Administrator = 0, FDManager, RNManager, NPManager, FDStaff, RNStaff, NPStaff, MSManager, MSStaff, Executive,
                               FDAssistantManager, RNAssistantManager, NPAssistantManager, MSAssistantManager, SuperAdmin = 20 };
                                
    public enum DepartmentManager { RNManager, NPMananger, FDManager, MSManager };

    public enum Department { MemberService = 0, NeedsProcessing, ReviewAndNegotiation, Finance, IT, Executive };

    // enumeration for case doc type
    public enum CaseDocType { NPF = 0, IB, PoP, MedRec, OtherDoc, FullDoc, IB_POP };

    // enumeration for task type
    public enum TaskType { Send = 0, Reply, SendAgain};

    // enumeration for communication type
    public enum CommunicationType { IncomingCall = 0, OutgoingCall, IncomingFax, OutgoingFax, EmailReceived = 6, EmailSent,
        LetterReceived, LetterMailed, Other, WalkIn, Task, CheckBlueSheet, ACH_BlueSheet, CreditCardBlueSheet, NoSharingOnlyBlueSheet,
        InterdepartmentalCommunication };
    // enumeration for Communication open mode
    public enum CommunicationOpenMode { AddNew = 0, ReadOnly, Update};

    //public enum ViewPrevToMedBill { CaseView, MedBillView };

    // enumeration for BlueSheet
    public enum EnumPaidTo { Member, MedicalProvider };
    public enum EnumSorted { NotSorted, SortedAsc, SortedDesc };
    public enum EnumBlueSheetType { Check = 1, ACH, CreditCard, NoSharingOnly };

    public enum Gender { Male, Female };
    public enum HouseholdRole { HeadOfHousehold, Spouse, Child };
    public enum Plan { GoldMedi_I, GoldMedi_II, GoldPlus, Gold, Silver, Bronze };
    public enum MembershipStatus { Pending, Applied, Active, PastDue, Inactive, CancelledReq, CancelledByMember, TerminatedByCMM, Hold, Incomplete };

    public enum MedBillIneligibleType { Ineligible, PartiallyIneligible };

}
