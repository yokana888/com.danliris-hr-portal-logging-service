using Com.DanLiris.Service.EmergencyAttendance.Lib.Dto;
using Com.DanLiris.Service.EmergencyAttendance.Lib.Helpers;
using Com.DanLiris.Service.EmergencyAttendance.Lib.Models;
using Com.DanLiris.Service.EmergencyAttendance.Lib.Services;
using Com.DanLiris.Service.EmergencyAttendance.Lib.ViewModels;
using Com.Moonlay.Models;
using Com.Moonlay.NetCore.Lib;
using Com.Moonlay.NetCore.Lib.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace Com.DanLiris.Service.EmergencyAttendance.Lib.Facades
{
    public class AttendanceFacade : IAttendanceFacade
    {
        private readonly AppDbContext dbContext;
        //private readonly AttendanceDbContext attendanceDbContext;

        private readonly DbSet<CheckInModel> dbSetCheckIn;
        private readonly DbSet<CheckOutModel> dbSetCheckOut;
        private readonly IdentityService identityService;
        public readonly IServiceProvider serviceProvider;

        private string USER_AGENT = "Facade";

        public AttendanceFacade(AppDbContext dbContext, /*AttendanceDbContext attendanceDbContext,*/ IServiceProvider serviceProvider)
        {
            this.dbContext = dbContext;
            //this.attendanceDbContext = attendanceDbContext;

            dbSetCheckIn = dbContext.Set<CheckInModel>();
            dbSetCheckOut = dbContext.Set<CheckOutModel>();

            this.serviceProvider = serviceProvider;
            identityService = (IdentityService)serviceProvider.GetService(typeof(IdentityService));
        }

        //public async Task<CheckTimeIndex> Read(string type, int page = 1, int size = 25, string order = "{}", string keyword = null, string filter = "{}")
        //{
        //    var adminEmployeeId = 0;
        //    var accessRole = "User Biasa";
        //    var startDate = DateTimeOffset.Now.AddDays(-7);
        //    var endDate = DateTimeOffset.Now;
        //    var result = new List<CheckTimeDto>();
        //    var totalData = 0;

        //    var employeeQuery = 
        //        this.attendanceDbContext.Employees.AsNoTracking()
        //            .Select(x => 
        //                new EmployeeModel 
        //                { 
        //                    Id = x.Id, 
        //                    EmployeeIdentity = x.EmployeeIdentity,
        //                    Firstname = x.Firstname,
        //                    Lastname = x.Lastname,
        //                    UnitId = x.UnitId,
        //                    SectionId = x.SectionId,
        //                    GroupId = x.GroupId
        //                }).AsQueryable();

        //    Dictionary<string, string> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(filter);
        //    Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
        //    List<string> searchAttributes = new List<string>()
        //    {
        //        "CheckTime", "EmployeeId", "UnitName", "SectionName", "GroupName", "EmployeeName", "EmployeeIdentity"
        //    };

        //    if (FilterDictionary.ContainsKey("adminEmployeeId"))
        //    {
        //        adminEmployeeId = Convert.ToInt16(FilterDictionary["adminEmployeeId"]);
        //        FilterDictionary.Remove("adminEmployeeId");
        //    }

        //    if (FilterDictionary.ContainsKey("accessRole"))
        //    {
        //        accessRole = FilterDictionary["accessRole"];
        //        FilterDictionary.Remove("accessRole");
        //    }

        //    if (FilterDictionary.ContainsKey("startDate"))
        //    {
        //        if (DateTime.TryParse(FilterDictionary["startDate"], out DateTime resultStartDate))
        //        {
        //            startDate = resultStartDate;
        //        }

        //        FilterDictionary.Remove("startDate");
        //    }

        //    if (FilterDictionary.ContainsKey("endDate"))
        //    {
        //        if (DateTime.TryParse(FilterDictionary["endDate"], out DateTime resultEndDate))
        //        {
        //            endDate = resultEndDate;
        //        }

        //        FilterDictionary.Remove("endDate");
        //    }

        //    if (OrderDictionary.Count <= 0)
        //    {
        //        OrderDictionary.Add("CheckTime", "desc");
        //    }

        //    if (accessRole != "Personalia Pusat" && accessRole != "HRD")
        //    {
        //        var unitIds = attendanceDbContext.EmployeeUnitAccessItems.AsNoTracking().Where(entity => entity.EmployeeId == adminEmployeeId).Select(entity => entity.UnitId).ToList();

        //        if (unitIds.Count > 0)
        //        {
        //            employeeQuery = employeeQuery.Where(s => unitIds.Contains(s.UnitId));
        //        }
        //        else
        //        {
        //            return new CheckTimeIndex(result, totalData, page, size);
        //        }
        //    }

        //    employeeQuery = QueryHelper<EmployeeModel>.ConfigureFilter(employeeQuery, FilterDictionary);

        //    var employees = from a in employeeQuery
        //                    join b in attendanceDbContext.Units on a.UnitId equals b.Id
        //                    join c in attendanceDbContext.Sections on a.SectionId equals c.Id
        //                    join d in attendanceDbContext.Groups on a.GroupId equals d.Id
        //                    select new
        //                    {
        //                        EmployeeId = a.Id,
        //                        EmployeeName = a.Firstname + " " + a.Lastname,
        //                        EmployeeIdentity = a.EmployeeIdentity,
        //                        UnitId = b.Id,
        //                        UnitName = b.Name,
        //                        SectionId = c.Id,
        //                        SectionName = c.Name,
        //                        GroupId = d.Id,
        //                        GroupName = d.Name,
        //                    }; 

        //    if (type == "check-in")
        //    {
        //        var queryIn = this.dbSetCheckIn.AsNoTracking().Where(x => x.CheckTime.Date >= startDate.Date && x.CheckTime.Date <= endDate.Date).AsEnumerable();

        //        var query = from a in employees
        //                    join b in queryIn
        //                    on a.EmployeeId equals b.Id
        //                    select new CheckTimeDto
        //                    {
        //                        Id = b.Id,
        //                        EmployeeId = b.EmployeeId,
        //                        EmployeeIdentity = a.EmployeeIdentity,
        //                        EmployeeName = a.EmployeeName,
        //                        UnitName = a.UnitName,
        //                        SectionName = a.UnitName,
        //                        GroupName = a.UnitName,
        //                        CheckTime = b.CheckTime,
        //                        AttendanceId = b.AttendanceId,
        //                        Username = b.CreatedBy,
        //                        Type = AttendanceType.CHECKIN,
        //                        IsPosted = b.IsPosted,
        //                    };

        //        query = QueryHelper<CheckTimeDto>.ConfigureSearch(query, searchAttributes, keyword);
        //        query = QueryHelper<CheckTimeDto>.ConfigureOrder(query, OrderDictionary);

        //        Pageable<CheckTimeDto> pageable = new Pageable<CheckTimeDto>(query, page - 1, size);
        //        result = pageable.Data.ToList<CheckTimeDto>();
        //        totalData = pageable.TotalCount;
        //    }
        //    else
        //    {
        //        var queryOut = this.dbSetCheckOut.AsNoTracking().Where(x => x.CheckTime.Date >= startDate.Date && x.CheckTime.Date <= endDate.Date).AsEnumerable();

        //        var query = from a in employees
        //                    join b in queryOut
        //                    on a.EmployeeId equals b.Id
        //                    select new CheckTimeDto
        //                    {
        //                        Id = b.Id,
        //                        EmployeeId = b.EmployeeId,
        //                        EmployeeIdentity = a.EmployeeIdentity,
        //                        EmployeeName = a.EmployeeName,
        //                        UnitName = a.UnitName,
        //                        SectionName = a.UnitName,
        //                        GroupName = a.UnitName,
        //                        CheckTime = b.CheckTime,
        //                        AttendanceId = b.AttendanceId,
        //                        Username = b.CreatedBy,
        //                        Type = AttendanceType.CHECKOUT,
        //                        IsPosted = b.IsPosted,
        //                    };

        //        query = QueryHelper<CheckTimeDto>.ConfigureSearch(query, searchAttributes, keyword);
        //        query = QueryHelper<CheckTimeDto>.ConfigureOrder(query, OrderDictionary);

        //        Pageable<CheckTimeDto> pageable = new Pageable<CheckTimeDto>(query, page - 1, size);
        //        result = pageable.Data.ToList<CheckTimeDto>();
        //        totalData = pageable.TotalCount;
        //    }

        //    return new CheckTimeIndex(result, totalData, page, size);
        //}

        //public async Task<CheckTimeIndex> Read(string type, int page = 1, int size = 25)
        //{
        //    var result = new List<CheckTimeDto>();
        //    var totalData = 0;

        //    if (type == "check-in")
        //    { 
        //        totalData = await this.dbSetCheckIn.CountAsync();

        //        var queryInList = this.dbSetCheckIn.AsNoTracking().OrderByDescending(entity => entity.CheckTime).Skip((page - 1) * size).Take(size).ToList();

        //        var query = from a in queryInList
        //                    join b in attendanceDbContext.Employees on a.EmployeeId equals b.Id
        //                    join c in attendanceDbContext.Units on b.UnitId equals c.Id
        //                    join d in attendanceDbContext.Sections on b.SectionId equals d.Id
        //                    join e in attendanceDbContext.Groups on b.GroupId equals e.Id
        //                    select new CheckTimeDto
        //                    {
        //                        Id = a.Id,
        //                        EmployeeId = a.EmployeeId,
        //                        EmployeeIdentity = b.EmployeeIdentity,
        //                        EmployeeName = b.Firstname + " " + b.Lastname,
        //                        UnitName = c.Name,
        //                        SectionName = d.Name,
        //                        GroupName = d.Name,
        //                        CheckTime = a.CheckTime,
        //                        AttendanceId = a.AttendanceId,
        //                        Username = a.CreatedBy,
        //                        Type = AttendanceType.CHECKIN,
        //                        IsPosted = a.IsPosted,
        //                    };

        //        result = query.ToList();
        //    }
        //    else
        //    {
        //        var queryOut = this.dbSetCheckOut.AsEnumerable();

        //        totalData = queryOut.Count();

        //        var queryOutList = queryOut.OrderByDescending(entity => entity.CheckTime).Skip((page - 1) * size).Take(size).ToList();

        //        var query = from a in queryOutList
        //                    join b in attendanceDbContext.Employees on a.EmployeeId equals b.Id
        //                    join c in attendanceDbContext.Units on b.UnitId equals c.Id
        //                    join d in attendanceDbContext.Sections on b.SectionId equals d.Id
        //                    join e in attendanceDbContext.Groups on b.GroupId equals e.Id
        //                    select new CheckTimeDto
        //                    {
        //                        Id = a.Id,
        //                        EmployeeId = a.EmployeeId,
        //                        EmployeeIdentity = b.EmployeeIdentity,
        //                        EmployeeName = b.Firstname + " " + b.Lastname,
        //                        UnitName = c.Name,
        //                        SectionName = d.Name,
        //                        GroupName = d.Name,
        //                        CheckTime = a.CheckTime,
        //                        AttendanceId = a.AttendanceId,
        //                        Username = a.CreatedBy,
        //                        Type = AttendanceType.CHECKIN,
        //                        IsPosted = a.IsPosted,
        //                    };
        //    }

        //    return new CheckTimeIndex(result, totalData, page, size);
        //}

        public async Task<int> CheckIn(CheckInViewModel viewModel)
        {
            int Created = 0;

            var existingData = 
                await dbContext.CheckIns
                    .FirstOrDefaultAsync(entity => 
                        entity.EmployeeId == viewModel.EmployeeId &&
                        entity.CheckTime.AddHours(identityService.TimezoneOffset).Date == viewModel.CheckTime.AddHours(identityService.TimezoneOffset).Date
                    );

            if (existingData == null)
            {
                using (var transaction = await this.dbContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        var attendance = new Models.CheckInModel()
                        {
                            CheckTime = viewModel.CheckTime,
                            EmployeeId = viewModel.EmployeeId,
                            Latitude = viewModel.Latitude,
                            Longitude = viewModel.Longitude,
                            ImageUri = viewModel.ImageUri
                        };

                        EntityExtension.FlagForCreate(attendance, viewModel.Username, USER_AGENT);
                        dbContext.CheckIns.Add(attendance);

                        Created = await dbContext.SaveChangesAsync();
                        await transaction.CommitAsync();
                    }
                    catch (Exception)
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }
                }
            }
            else
            {
                var errorResult = new List<ValidationResult>()
                {
                    new ValidationResult("Anda Sudah Melakukan Check-in", new List<string> { "CheckIn" })
                };

                var validationContext = new ValidationContext(viewModel, serviceProvider, null);
                throw new ServiceValidationExeption(validationContext, errorResult);
            }


            return Created;
        }

        public async Task<int> CheckOut(CheckOutViewModel viewModel)
        {
            int Created = 0;

            var existingData =
                await dbContext.CheckOuts
                    .FirstOrDefaultAsync(entity =>
                        entity.EmployeeId == viewModel.EmployeeId &&
                        entity.CheckTime.AddHours(identityService.TimezoneOffset).Date == viewModel.CheckTime.AddHours(identityService.TimezoneOffset).Date
                    );

            if (existingData == null)
            {
                using (var transaction = await this.dbContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        var attendance = new Models.CheckOutModel()
                        {
                            CheckTime = viewModel.CheckTime,
                            EmployeeId = viewModel.EmployeeId,
                            Latitude = viewModel.Latitude,
                            Longitude = viewModel.Longitude,
                            ImageUri = viewModel.ImageUri
                        };

                        EntityExtension.FlagForCreate(attendance, viewModel.Username, USER_AGENT);
                        dbContext.CheckOuts.Add(attendance);

                        Created = await dbContext.SaveChangesAsync();
                        await transaction.CommitAsync();
                    }
                    catch (Exception)
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }
                }
            }
            else
            {
                var errorResult = new List<ValidationResult>()
                {
                    new ValidationResult("Anda Sudah Melakukan Check-Out", new List<string> { "CheckOut" })
                };

                var validationContext = new ValidationContext(viewModel, serviceProvider, null);
                throw new ServiceValidationExeption(validationContext, errorResult);
            }


            return Created;
        }

        public async Task<bool> GetLatestAttend(int employeeId, string type, DateTimeOffset date)
        {
            var result = false;

            if (type == AttendanceType.CHECKIN)
            {
                result = 
                    await dbContext.CheckIns
                    .AnyAsync(entity =>
                        entity.EmployeeId == employeeId &&
                        entity.CheckTime.AddHours(identityService.TimezoneOffset).Date == date.AddHours(identityService.TimezoneOffset).Date
                    );
            }
            else
            {
                result =
                   await dbContext.CheckOuts
                   .AnyAsync(entity =>
                       entity.EmployeeId == employeeId &&
                       entity.CheckTime.AddHours(identityService.TimezoneOffset).Date == date.AddHours(identityService.TimezoneOffset).Date
                   );
            }

            return result;
        }
    }
}
