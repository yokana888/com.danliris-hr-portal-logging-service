using Com.DanLiris.Service.EmergencyAttendance.Lib.Helpers;
using Com.DanLiris.Service.EmergencyAttendance.Lib.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.DanLiris.Service.EmergencyAttendance.Lib.ViewModels
{
    public class CheckOutViewModel : BaseViewModel
    {
        public int EmployeeId { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string ImageUri { get; set; }
        public string Username { get; set; }
        public DateTimeOffset CheckTime { get; set; }
        public string Type { get; private set; } = AttendanceType.CHECKOUT;
        public int? AttendanceId { get; set; }
    }
}
