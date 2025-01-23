using Com.DanLiris.Service.EmergencyAttendance.Lib.Dto;
using Com.DanLiris.Service.EmergencyAttendance.Lib.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.DanLiris.Service.EmergencyAttendance.Lib.Facades
{
    public interface IAttendanceFacade
    {
        Task<CheckTimeIndex> Read(string type, int page = 1, int size = 25, string order = "{}", string keyword = null, string filter = "{}");
        Tuple<List<CheckInViewModel>, int, Dictionary<string, string>> ReadCheckIn(int page = 1, int size = 25, string order = "{}", string keyword = null, string filter = "{}");
        Tuple<List<CheckOutViewModel>, int, Dictionary<string, string>> ReadCheckOut(int page = 1, int size = 25, string order = "{}", string keyword = null, string filter = "{}");
    }
}
