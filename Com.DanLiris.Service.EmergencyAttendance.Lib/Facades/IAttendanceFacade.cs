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
        Task<CheckTimeIndex> Read(string type, int page = 1, int size = 25);
        Task<CheckTimeIndex> Read(string type, int page = 1, int size = 25, string order = "{}", string keyword = null, string filter = "{}");
        Task<int> CheckIn(CheckInViewModel viewModel);
        Task<int> CheckOut(CheckOutViewModel viewModel);
        Task<CheckTimeDto> GetLatestAttend(int employeeId);
    }
}
