using System;
using System.ComponentModel.DataAnnotations.Schema;
using Com.Moonlay.Models;

namespace EWorkplaceAbsensiService.Lib.Models
{
    public class EmployeeUnitAccessItemModel: StandardEntity
    {
        public int EmployeeId { get; set; }
        public int UnitId { get; set; }
        public string? UnitName { get; set; }

        [ForeignKey("EmployeeId")]
        public virtual EmployeeModel Employee { get; set; }
    }
}
