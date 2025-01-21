using AutoMapper;
using Com.Moonlay.NetCore.Lib.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using Com.DanLiris.Service.Logging.Lib.Services;
using Com.DanLiris.Service.Logging.Lib.Facades;
using Com.DanLiris.Service.Logging.Lib.Interfaces;
using Com.DanLiris.Service.Logging.Lib.Models.DeliveryOrderModel;
using Com.DanLiris.Service.Logging.Lib.ViewModels.DeliveryOrderViewModel;
using Com.DanLiris.Service.Logging.WebApi.Helpers;
namespace Com.DanLiris.Service.Logging.WebApi.Controllers.v1.AttendanceController
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/sos")]
    [Authorize]
    public class AttendanceController : Controller
    {
        private string ApiVersion = "1.0.0";
        private readonly IMapper mapper;
        private readonly IdentityService identityService;
        private readonly IServiceProvider _serviceProvider;

        public AttendanceController(IMapper mapper, IServiceProvider serviceProvider)
        {
            this.mapper = mapper;
            _serviceProvider = serviceProvider;
            identityService = (IdentityService)serviceProvider.GetService(typeof(IdentityService));
        }

        [HttpGet]
        public IActionResult Get(int page = 1, int size = 25, string order = "{}", string keyword = null, string filter = "{}")
        {
            try
            {
                //var Data = facade.Read(page, size, order, keyword, filter);
                var newData = mapper.Map<List<DeliveryOrderViewModel>>(Data.Item1);

                List<object> listData = new List<object>();
                listData.AddRange(newData.AsQueryable().Select(s => new
                {
                    s._id,
                    s.no,
                    s.supplierDoDate,
                    s.supplier,
                    s.LastModifiedUtc,
                    items = s.items.Select(i => new { i.purchaseOrderExternal, i.fulfillments })
                }));

                return Ok(new
                {
                    apiVersion = ApiVersion,
                    statusCode = General.OK_STATUS_CODE,
                    message = General.OK_MESSAGE,
                    data = listData,
                    info = new Dictionary<string, object>
                    {
                        { "count", listData.Count },
                        { "total", Data.Item2 },
                        { "order", Data.Item3 },
                        { "page", page },
                        { "size", size }
                    },
                });
            }
            catch (Exception e)
            {
                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, General.INTERNAL_ERROR_STATUS_CODE, e.Message)
                    .Fail();
                return StatusCode(General.INTERNAL_ERROR_STATUS_CODE, Result);
            }
        }
    }
}
