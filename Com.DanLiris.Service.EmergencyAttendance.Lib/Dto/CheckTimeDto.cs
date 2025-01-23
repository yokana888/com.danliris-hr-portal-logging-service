using Com.DanLiris.Service.EmergencyAttendance.Lib.Helpers;
using Com.DanLiris.Service.EmergencyAttendance.Lib.Models;
using Com.DanLiris.Service.EmergencyAttendance.Lib.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Com.DanLiris.Service.EmergencyAttendance.Lib.Dto
{
    public class CheckTimeDto : BaseViewModel
    {
        public int? EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeIdentity { get; set; }
        public string UnitName { get; set; }
        public string SectionName { get; set; }
        public string GroupName { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string ImageUri { get; set; }
        public string Username { get; set; }
        public DateTimeOffset CheckTime { get; set; }
        public string Type { get; set; }
        public int? AttendanceId { get; set; }
        public bool IsPosted { get; set; }
    }
}
