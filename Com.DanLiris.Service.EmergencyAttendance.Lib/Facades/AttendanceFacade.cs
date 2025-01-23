using Com.DanLiris.Service.EmergencyAttendance.Lib.Dto;
using Com.DanLiris.Service.EmergencyAttendance.Lib.Helpers;
using Com.DanLiris.Service.EmergencyAttendance.Lib.Models;
using Com.DanLiris.Service.EmergencyAttendance.Lib.ViewModels;
using Com.Moonlay.NetCore.Lib;
using EWorkplaceAbsensiService.Lib.Models;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace Com.DanLiris.Service.EmergencyAttendance.Lib.Facades
{
    public class AttendanceFacade : IAttendanceFacade
    {
        private readonly ApplicationDbContext dbContext;
        private readonly AttendanceDbContext attendanceDbContext;

        private readonly DbSet<CheckInModel> dbSetCheckIn;
        private readonly DbSet<CheckOutModel> dbSetCheckOut;

        public readonly IServiceProvider serviceProvider;

        private string USER_AGENT = "Facade";

        public AttendanceFacade(ApplicationDbContext dbContext, AttendanceDbContext attendanceDbContext, IServiceProvider serviceProvider)
        {
            this.dbContext = dbContext;
            this.attendanceDbContext = attendanceDbContext;

            dbSetCheckIn = dbContext.Set<CheckInModel>();
            dbSetCheckOut = dbContext.Set<CheckOutModel>();

            this.serviceProvider = serviceProvider;
        }

        public Tuple<List<CheckInViewModel>, int, Dictionary<string, string>> ReadCheckIn(int Page = 1, int Size = 25, string Order = "{}", string Keyword = null, string Filter = "{}")
        {
            throw new NotImplementedException();
        }

        public Tuple<List<CheckOutViewModel>, int, Dictionary<string, string>> ReadCheckOut(int Page = 1, int Size = 25, string Order = "{}", string Keyword = null, string Filter = "{}")
        {
            throw new NotImplementedException();
        }

        public async Task<CheckTimeIndex> Read(string type, int page = 1, int size = 25, string order = "{}", string keyword = null, string filter = "{}")
        {
            List<string> searchAttributes = new List<string>()
            {
                "CheckTime", "EmployeeId", "UnitName", "SectionName", "GroupName", "EmployeeName", "EmployeeIdentity"
            };

            var result = new List<CheckTimeDto>();
            var totalData = 0;

            if (type != "check-in") 
            {
                var queryIn = this.dbSetCheckIn.AsEnumerable();

                totalData = queryIn.Count();

                var queryInList = queryIn.OrderByDescending(entity => entity.CheckTime).Skip((page - 1) * size).Take(size).ToList();

                var query = from a in queryInList
                            join b in attendanceDbContext.Employees on a.EmployeeId equals b.Id
                            join c in attendanceDbContext.Units on b.UnitId equals c.Id
                            join d in attendanceDbContext.Sections on b.SectionId equals d.Id
                            join e in attendanceDbContext.Groups on b.GroupId equals e.Id
                            select new CheckTimeDto
                            {
                                Id = a.Id,
                                EmployeeId = a.EmployeeId,
                                EmployeeIdentity = b.EmployeeIdentity,
                                EmployeeName = b.Firstname + " " + b.Lastname,
                                UnitName = c.Name,
                                SectionName = d.Name,
                                GroupName = d.Name,
                                CheckTime = a.CheckTime,
                                AttendanceId = a.AttendanceId,
                                Username = a.CreatedBy,
                                Type = AttendanceType.CHECKIN,
                                IsPosted = a.IsPosted,
                            };

                result = query.ToList();
            }
            else
            {
                var queryOut = this.dbSetCheckOut.AsEnumerable();

                totalData = queryOut.Count();

                var queryOutList = queryOut.OrderByDescending(entity => entity.CheckTime).Skip((page - 1) * size).Take(size).ToList();

                var query = from a in queryOutList
                            join b in attendanceDbContext.Employees on a.EmployeeId equals b.Id
                            join c in attendanceDbContext.Units on b.UnitId equals c.Id
                            join d in attendanceDbContext.Sections on b.SectionId equals d.Id
                            join e in attendanceDbContext.Groups on b.GroupId equals e.Id
                            select new CheckTimeDto
                            {
                                Id = a.Id,
                                EmployeeId = a.EmployeeId,
                                EmployeeIdentity = b.EmployeeIdentity,
                                EmployeeName = b.Firstname + " " + b.Lastname,
                                UnitName = c.Name,
                                SectionName = d.Name,
                                GroupName = d.Name,
                                CheckTime = a.CheckTime,
                                AttendanceId = a.AttendanceId,
                                Username = a.CreatedBy,
                                Type = AttendanceType.CHECKIN,
                                IsPosted = a.IsPosted,
                            };
            }

            return new CheckTimeIndex(result, totalData, page, size);
        }
    }
}
