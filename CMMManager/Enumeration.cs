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
    public enum SettlementType { SelfPayDiscount = 1, ThirdPartyDiscount, MemberPayment, CMMProviderPayment, CMMDiscount, CMMMemberReimbursement, Ineligible, MedicalProviderRefund, PRReimbursement, MemberDiscount };
    public enum PaymentMethodExport { Check = 1, CreditCard, ACH_Banking };
    public enum ProgramNameExport { GoldPlus = 0, Gold, Silver, Bronze, GoldMedi_I, GoldMedi_II };
    public enum IncidentProgramName { GoldPlus = 0, Gold, Silver, Bronze, GoldMedi_I, GoldMedi_II };
    public enum PaymentTypeExport { MemberReimbursement, PR_Reimbursement, ProviderPayment };

    public enum MedicalBillType { MedicalBill = 1, Prescription = 2, PhysicalTherapy = 3, MedicalRecord = 4 };

    // enumeration for program change history
    public enum IndividualPlan { Bronze = 0, Silver, Gold, GoldPlus, GoldMedi_I, GoldMedi_II };
    public enum IllnessProgram { Bronze = 0, Silver, Gold, GoldPlus, GoldMedi_I, GoldMedi_II };
    public enum IncidentProgram { Bronze = 0, Silver, Gold, GoldPlus, GoldMedi_I, GoldMedi_II };

    public enum RelatedToTable { Membership, Case, Illness, Incident, MedicalBill, Settlement };
    public enum TaskStatus { NotStarted, InProgress, Completed, WaitingOnSomeoneElse, Deferred, Solved };
    public enum TaskPriority { High, Normal, Low };
    public enum TaskMode { AddNew, EditInRNManagerDashboard, EditInRNStaffDashboard, EditInNPManagerDashboard, EditInNPStaffDashboard, EditInMedBill, EditInCase, EditInIndividual }

    public enum UserRole { Administrator = 0, FDManager, RNManager, NPManager, FDStaff, RNStaff, NPStaff, MSManager, MSStaff, Executive, SuperAdmin = 20 };
    public enum Department { MemberService = 0, NeedsProcessing, ReviewAndNegotiation, Finance, IT, Executive };

    // enumeration for case doc type
    public enum CaseDocType { NPF = 0, IB, PoP, MedRec, OtherDoc, FullDoc, IB_POP };

    // enumeration for task type
    public enum TaskType { Send = 0, Reply, SendAgain};

    // enumeration for communication type
    public enum CommunicationType { IncomingCall = 0, OutgoingCall, IncomingFax, OutgoingFax, EmailReceived = 6, EmailSent, LetterReceived, LetterMailed, Other, WalkIn, Task, CheckBlueSheet, ACH_BlueSheet, CreditCardBlueSheet };
    // enumeration for Communication open mode
    public enum CommunicationOpenMode { AddNew = 0, ReadOnly, Update};

    //public enum ViewPrevToMedBill { CaseView, MedBillView };

    // enumeration for BlueSheet
    public enum EnumPaidTo { Member, MedicalProvider };
    public enum EnumSorted { NotSorted, SortedAsc, SortedDesc };
    public enum EnumBlueSheetType { Check = 1, ACH, CreditCard };

    public enum Gender { Male, Female };
    public enum HouseholdRole { HeadOfHousehold, Spouse, Child };
    public enum Plan { GoldMedi_I, GoldMedi_II, GoldPlus, Gold, Silver, Bronze };
    public enum MembershipStatus { Pending, Applied, Active, PastDue, Inactive, CancelledReq, CancelledByMember, TerminatedByCMM, Hold, Incomplete };

    public enum MedBillIneligibleType { Ineligible, PartiallyIneligible };

}
