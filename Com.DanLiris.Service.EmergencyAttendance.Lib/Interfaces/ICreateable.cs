using System.Threading.Tasks;

namespace Com.DanLiris.Service.EmergencyAttendance.Lib.Interfaces
{
    public interface ICreateable
    {
        Task<int> Create(object model);
    }
}
