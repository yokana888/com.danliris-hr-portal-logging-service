using Com.DanLiris.Service.EmergencyAttendance.Lib.Utilities;
using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EWorkplaceAbsensiService.Lib.Models
{
    public class EmployeeModel : StandardEntity<long>
    {
        [MaxLength(64)]
        public string EmployeeIdentity { get; set; }
        [MaxLength(16)]
        public string FingerprintId { get; set; }
        public string LeadBy { get; set; }
        public string LeadOf { get; set; }
        [MaxLength(128)]
        public string Firstname { get; set; }
        [MaxLength(128)]
        public string Lastname { get; set; }
        [MaxLength(64)]
        public string CitizenshipIdentity { get; set; }
        [MaxLength(64)]
        public string PhoneNumber { get; set; }
        [MaxLength(512)]
        public string Address { get; set; }
        [MaxLength(64)]
        public string BloodType { get; set; }
        [MaxLength(512)]
        public string City { get; set; }
        [MaxLength(512)]
        public string PlaceOfBirth { get; set; }
        public DateTimeOffset DoB { get; set; }
        public DateTimeOffset JoinDate { get; set; }
        public DateTimeOffset? RetirementDate { get; set; }
        public int EmployeeClassId { get; set; }
        public int EmployeeGrade { get; set; }
        public string EmploymentClass { get; set; }
        public int RoleEmployeeId { get; set; }
        public int UnitId { get; set; }
        // status kepegawaian
        [MaxLength(64)]
        public string EmploymentStatus { get; set; }
        // resign atau mangkir
        [MaxLength(256)]
        public string StatusEmployee { get; set; }
        [MaxLength(512)]
        public string EmployeeStatusRemark { get; set; }
        [MaxLength(512)]
        public string Education { get; set; }
        [MaxLength(512)]
        public string Specialization { get; set; }
        public int FamilyMember { get; set; }
        [MaxLength(512)]
        public string School { get; set; }
        [MaxLength(16)]
        public string Gender { get; set; }
        [MaxLength(128)]
        public string Religion { get; set; }
        [MaxLength(32)]
        public string MaritalStatus { get; set; }
        public int ChildNumber { get; set; }
        public DateTimeOffset? BeginContractDate { get; set; }
        public DateTimeOffset? EndContractDate { get; set; }
        public DateTimeOffset? BeginContractExtensionDate { get; set; }
        public DateTimeOffset? EndContractExtensionDate { get; set; }
        public int ContractNumber { get; set; }
        public DateTimeOffset? TrainingEndDate { get; set; }
        public DateTimeOffset? InactiveDate { get; set; }
        [MaxLength(512)]
        public string Trustee { get; set; }
        [MaxLength(128)]
        public string CompanyCode { get; set; }
        [MaxLength(128)]
        public string JPKNo { get; set; }
        public int SectionId { get; set; }
        public int GroupId { get; set; }
        public int LocationId { get; set; }
        [MaxLength(512)]
        public string Area { get; set; }
        public bool IsCanApproveRequestWfh { get; set; }
        public bool IsCanApproveRequestLeave { get; set; }
        public bool IsClaimed { get; set; }
        public string EmployeeLocation { get; set; }
        public DateTimeOffset? DateResign { get; set; }
        public DateTimeOffset? DateMutation { get; set; }
        public int WorkDays { get; set; }
        public double BaseSalary { get; set; }
        public bool IsWorkerUnion { get; set; }
        public double LeaderAllowance { get; set; }
        public double AchievementBonus { get; set; }
        public double GrossIncome { get; set; }
        public int BpjsKetenagakerjaanId { get; set; }
        public string BpjsKetenagakerjaan { get; set; }
        public bool BpjsKesehatan { get; set; }
        public string SocialMedia { get; set; }
        public int StatusPphId { get; set; }
        public double MealAllowance { get; set; }

        [MaxLength(1024)]
        public string ProfileImageUri { get; set; }
        [MaxLength(128)]
        public string AccessRole { get; set; }
        public bool HasAccessRight { get; set; }
        public DateTimeOffset? AssignmentDate { get; set; }
        public double BpjsKesehatanPercentage { get; set; }
        [MaxLength(128)]
        public string AccountNo { get; set; }
        [MaxLength(32)]
        public string NPWPNo { get; set; }
        public bool IsJoinSPRI { get; set; }
        public DateTimeOffset? JoinSPRIDate { get; set; }
        public bool CanAccessQRCode { get; set; }
        public bool CanAccessResetPassword { get; set; }

        //Notification
        public bool IsNotificationEnabled { get; set; }
        public string NotificationToken { get; set; }
        public string DigitalGenerateCode { get; set; }
        public int DigitalAutoIncrement { get; set; }
        public string DigitalId { get; set;  }
        [MaxLength(512)]
        public string DeleteReason { get; set; }
        public DateTimeOffset? EffectiveDatePph { get; set; }
        public string ProfileImageBase64 { get; set; }
    }
}
