using AutoMapper;

namespace Com.DanLiris.Service.Logging.Lib.Utilities
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
