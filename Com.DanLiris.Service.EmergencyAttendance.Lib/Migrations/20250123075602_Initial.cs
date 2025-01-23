using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Com.DanLiris.Service.EmergencyAttendance.Lib.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CheckIns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    Longitude = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Latitude = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    CheckTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ImageUri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPosted = table.Column<bool>(type: "bit", nullable: false),
                    AttendanceId = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedAgent = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LastModifiedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LastModifiedAgent = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DeletedAgent = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
               },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckIns", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CheckOuts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    Longitude = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Latitude = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    CheckTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ImageUri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPosted = table.Column<bool>(type: "bit", nullable: false),
                    AttendanceId = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedAgent = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LastModifiedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LastModifiedAgent = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DeletedAgent = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckOuts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CheckIns_EmployeeId_CheckTime",
                table: "CheckIns",
                columns: new[] { "EmployeeId", "CheckTime" });

            migrationBuilder.CreateIndex(
                name: "IX_CheckOuts_EmployeeId_CheckTime",
                table: "CheckOuts",
                columns: new[] { "EmployeeId", "CheckTime" });

            #region Comment
            //migrationBuilder.CreateTable(
            //    name: "AttendanceModel",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        EmployeeId = table.Column<int>(type: "int", nullable: false),
            //        State = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
            //        Location = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
            //        Longitude = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
            //        Latitude = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
            //        CheckOutLongitude = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
            //        CheckOutLatitude = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
            //        CheckedInShiftId = table.Column<int>(type: "int", nullable: false),
            //        CheckIn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
            //        CheckOut = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
            //        IsApproved = table.Column<bool>(type: "bit", nullable: false),
            //        CheckOutDifference = table.Column<int>(type: "int", nullable: false),
            //        CheckInDifference = table.Column<int>(type: "int", nullable: false),
            //        CheckInImageUri = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        IsFailedRecognizeCheckInImage = table.Column<bool>(type: "bit", nullable: false),
            //        CheckOutImageUri = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        IsFailedRecognizeCheckOutImage = table.Column<bool>(type: "bit", nullable: false),
            //        Active = table.Column<bool>(type: "bit", nullable: false),
            //        CreatedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
            //        CreatedAgent = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
            //        LastModifiedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        LastModifiedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
            //        LastModifiedAgent = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
            //        IsDeleted = table.Column<bool>(type: "bit", nullable: false),
            //        DeletedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        DeletedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
            //        DeletedAgent = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_AttendanceModel", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "EmployeeModel",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        EmployeeIdentity = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
            //        FingerprintId = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
            //        LeadBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        LeadOf = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Firstname = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
            //        Lastname = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
            //        CitizenshipIdentity = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
            //        PhoneNumber = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
            //        Address = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
            //        BloodType = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
            //        City = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
            //        PlaceOfBirth = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
            //        DoB = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
            //        JoinDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
            //        RetirementDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
            //        EmployeeClassId = table.Column<int>(type: "int", nullable: false),
            //        EmployeeGrade = table.Column<int>(type: "int", nullable: false),
            //        EmploymentClass = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        RoleEmployeeId = table.Column<int>(type: "int", nullable: false),
            //        UnitId = table.Column<int>(type: "int", nullable: false),
            //        EmploymentStatus = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
            //        StatusEmployee = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
            //        EmployeeStatusRemark = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
            //        Education = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
            //        Specialization = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
            //        FamilyMember = table.Column<int>(type: "int", nullable: false),
            //        School = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
            //        Gender = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
            //        Religion = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
            //        MaritalStatus = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
            //        ChildNumber = table.Column<int>(type: "int", nullable: false),
            //        BeginContractDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
            //        EndContractDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
            //        BeginContractExtensionDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
            //        EndContractExtensionDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
            //        ContractNumber = table.Column<int>(type: "int", nullable: false),
            //        TrainingEndDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
            //        InactiveDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
            //        Trustee = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
            //        CompanyCode = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
            //        JPKNo = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
            //        SectionId = table.Column<int>(type: "int", nullable: false),
            //        GroupId = table.Column<int>(type: "int", nullable: false),
            //        LocationId = table.Column<int>(type: "int", nullable: false),
            //        Area = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
            //        IsCanApproveRequestWfh = table.Column<bool>(type: "bit", nullable: false),
            //        IsCanApproveRequestLeave = table.Column<bool>(type: "bit", nullable: false),
            //        IsClaimed = table.Column<bool>(type: "bit", nullable: false),
            //        EmployeeLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        DateResign = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
            //        DateMutation = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
            //        WorkDays = table.Column<int>(type: "int", nullable: false),
            //        BaseSalary = table.Column<double>(type: "float", nullable: false),
            //        IsWorkerUnion = table.Column<bool>(type: "bit", nullable: false),
            //        LeaderAllowance = table.Column<double>(type: "float", nullable: false),
            //        AchievementBonus = table.Column<double>(type: "float", nullable: false),
            //        GrossIncome = table.Column<double>(type: "float", nullable: false),
            //        BpjsKetenagakerjaanId = table.Column<int>(type: "int", nullable: false),
            //        BpjsKetenagakerjaan = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        BpjsKesehatan = table.Column<bool>(type: "bit", nullable: false),
            //        SocialMedia = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        StatusPphId = table.Column<int>(type: "int", nullable: false),
            //        MealAllowance = table.Column<double>(type: "float", nullable: false),
            //        ProfileImageUri = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
            //        AccessRole = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
            //        HasAccessRight = table.Column<bool>(type: "bit", nullable: false),
            //        AssignmentDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
            //        BpjsKesehatanPercentage = table.Column<double>(type: "float", nullable: false),
            //        AccountNo = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
            //        NPWPNo = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
            //        IsJoinSPRI = table.Column<bool>(type: "bit", nullable: false),
            //        JoinSPRIDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
            //        CanAccessQRCode = table.Column<bool>(type: "bit", nullable: false),
            //        CanAccessResetPassword = table.Column<bool>(type: "bit", nullable: false),
            //        IsNotificationEnabled = table.Column<bool>(type: "bit", nullable: false),
            //        NotificationToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        DigitalGenerateCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        DigitalAutoIncrement = table.Column<int>(type: "int", nullable: false),
            //        DigitalId = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        DeleteReason = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
            //        EffectiveDatePph = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
            //        ProfileImageBase64 = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Active = table.Column<bool>(type: "bit", nullable: false),
            //        CreatedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
            //        CreatedAgent = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
            //        LastModifiedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        LastModifiedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
            //        LastModifiedAgent = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
            //        IsDeleted = table.Column<bool>(type: "bit", nullable: false),
            //        DeletedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        DeletedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
            //        DeletedAgent = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_EmployeeModel", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "GroupModel",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
            //        UnitId = table.Column<int>(type: "int", nullable: false),
            //        SectionId = table.Column<int>(type: "int", nullable: false),
            //        Active = table.Column<bool>(type: "bit", nullable: false),
            //        CreatedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
            //        CreatedAgent = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
            //        LastModifiedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        LastModifiedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
            //        LastModifiedAgent = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
            //        IsDeleted = table.Column<bool>(type: "bit", nullable: false),
            //        DeletedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        DeletedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
            //        DeletedAgent = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_GroupModel", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "SectionModel",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
            //        UnitId = table.Column<int>(type: "int", nullable: false),
            //        Active = table.Column<bool>(type: "bit", nullable: false),
            //        CreatedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
            //        CreatedAgent = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
            //        LastModifiedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        LastModifiedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
            //        LastModifiedAgent = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
            //        IsDeleted = table.Column<bool>(type: "bit", nullable: false),
            //        DeletedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        DeletedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
            //        DeletedAgent = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_SectionModel", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "UnitModel",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
            //        Code = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
            //        EmployeeIdentityReferenceCode = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
            //        Active = table.Column<bool>(type: "bit", nullable: false),
            //        CreatedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
            //        CreatedAgent = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
            //        LastModifiedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        LastModifiedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
            //        LastModifiedAgent = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
            //        IsDeleted = table.Column<bool>(type: "bit", nullable: false),
            //        DeletedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        DeletedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
            //        DeletedAgent = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_UnitModel", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "EmployeeUnitAccessItemModel",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        EmployeeId = table.Column<int>(type: "int", nullable: false),
            //        UnitId = table.Column<int>(type: "int", nullable: false),
            //        UnitName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Active = table.Column<bool>(type: "bit", nullable: false),
            //        CreatedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
            //        CreatedAgent = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
            //        LastModifiedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        LastModifiedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
            //        LastModifiedAgent = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
            //        IsDeleted = table.Column<bool>(type: "bit", nullable: false),
            //        DeletedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        DeletedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
            //        DeletedAgent = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_EmployeeUnitAccessItemModel", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_EmployeeUnitAccessItemModel_EmployeeModel_EmployeeId",
            //            column: x => x.EmployeeId,
            //            principalTable: "EmployeeModel",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_EmployeeUnitAccessItemModel_EmployeeId",
            //    table: "EmployeeUnitAccessItemModel",
            //    column: "EmployeeId");
            #endregion
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CheckIns");

            migrationBuilder.DropTable(
                name: "CheckOuts");

            #region Comment
            //migrationBuilder.DropTable(
            //    name: "AttendanceModel");

            //migrationBuilder.DropTable(
            //    name: "EmployeeUnitAccessItemModel");

            //migrationBuilder.DropTable(
            //    name: "GroupModel");

            //migrationBuilder.DropTable(
            //    name: "SectionModel");

            //migrationBuilder.DropTable(
            //    name: "UnitModel");

            //migrationBuilder.DropTable(
            //    name: "EmployeeModel");
            #endregion
        }
    }
}
