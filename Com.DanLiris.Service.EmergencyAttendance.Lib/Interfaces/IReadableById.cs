using System.Threading.Tasks;

namespace Com.DanLiris.Service.EmergencyAttendances.Lib.Interfaces
{
    public interface IReadByIdable<TModel>
    {
        Task<TModel> ReadById(int id);
    }
}
