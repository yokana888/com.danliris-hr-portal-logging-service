using Com.DanLiris.Service.EmergencyAttendance.Lib.Utilities;
using Com.Moonlay.Models;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EWorkplaceAbsensiService.Lib.Models
{
    public class UnitModel : StandardEntity
    {
        [MaxLength(512)]
        public string? Name { get; set; }
        [MaxLength(64)]
        public string? Code { get; set; }
        [MaxLength(16)]
        public string? EmployeeIdentityReferenceCode { get; set; }
    }
}
