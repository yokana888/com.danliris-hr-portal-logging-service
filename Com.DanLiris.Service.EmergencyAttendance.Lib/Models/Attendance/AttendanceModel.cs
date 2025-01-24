//using Com.DanLiris.Service.EmergencyAttendance.Lib.Utilities;
//using Com.Moonlay.Models;
//using Microsoft.Extensions.DependencyInjection;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;

//namespace EWorkplaceAbsensiService.Lib.Models
//{
//    public class AttendanceModel : StandardEntity
//    {
//        public int EmployeeId { get; set; }
//        [MaxLength(8)]
//        public string? State { get; set; }
//        [MaxLength(128)]
//        public string? Location { get; set; }
//        [MaxLength(128)]
//        public string? Longitude { get; set; }
//        [MaxLength(128)]
//        public string? Latitude { get; set; }
//        [MaxLength(128)]
//        public string? CheckOutLongitude { get; set; }
//        [MaxLength(128)]
//        public string? CheckOutLatitude { get; set; }
//        public int CheckedInShiftId { get; set; }
//        public DateTimeOffset CheckIn { get; set; }
//        public DateTimeOffset CheckOut { get; set; }
//        public bool IsApproved { get; set; }
//        public int CheckOutDifference { get; set; }
//        public int CheckInDifference { get; set; }

//        public string? CheckInImageUri { get; set; }
//        public bool IsFailedRecognizeCheckInImage { get; set; }
//        public string? CheckOutImageUri { get; set; }
//        public bool IsFailedRecognizeCheckOutImage { get; set; }
//    }
//}
