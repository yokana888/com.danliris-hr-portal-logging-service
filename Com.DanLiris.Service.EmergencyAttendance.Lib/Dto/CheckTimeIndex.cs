using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.DanLiris.Service.EmergencyAttendance.Lib.Dto
{
    public class CheckTimeIndex
    {
        public List<CheckTimeDto> data;
        public int total;
        public int page;
        public int size;

        public CheckTimeIndex(List<CheckTimeDto> data, int total, int page, int size)
        {
            this.data = data;
            this.total = total;
            this.page = page;
            this.size = size;
        }
    }
}
