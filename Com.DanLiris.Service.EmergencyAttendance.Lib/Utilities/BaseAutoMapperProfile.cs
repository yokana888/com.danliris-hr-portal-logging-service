using AutoMapper;

namespace Com.DanLiris.Service.EmergencyAttendance.Lib.Utilities
{
    public class BaseAutoMapperProfile : Profile
    {
        public BaseAutoMapperProfile()
        {
            //RecognizePrefixes("_");
            ReplaceMemberName("_id", "Id");

            //var config = new MapperConfiguration(cfg => 
            //{
            //    cfg.re
            //})
        }
    }
}
