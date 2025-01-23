using Com.DanLiris.Service.EmergencyAttendance.Lib.Utilities;
using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.DanLiris.Service.EmergencyAttendance.Lib.Models
{
    public class CheckOutModel : StandardEntity<long>
    {
        public int EmployeeId { get; set; }
        [MaxLength(128)]
        public string Longitude { get; set; }
        [MaxLength(128)]
        public string Latitude { get; set; }
        public DateTimeOffset CheckTime { get; set; }
        public string? ImageUri { get; set; }
        public bool IsPosted { get; set; }
        public int AttendanceId { get; set; }
    }
}
