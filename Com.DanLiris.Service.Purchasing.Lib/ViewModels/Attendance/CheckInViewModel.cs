using Com.DanLiris.Service.Logging.Lib.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.DanLiris.Service.Logging.Lib.ViewModels.AttendanceViewModel
{
    public class CheckInViewModel : BaseViewModel
    {
        public int? EmployeeId { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string ImageUri { get; set; }
        public string Username { get; set; }
        public DateTimeOffset CheckTime { get; set; }
        public int? AttendanceId { get; set; }
    }
}
