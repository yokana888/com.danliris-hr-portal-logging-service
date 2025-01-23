using AutoMapper;
using Com.Moonlay.NetCore.Lib.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using Com.DanLiris.Service.EmergencyAttendance.Lib.Services;
using Com.DanLiris.Service.EmergencyAttendance.WebApi.Helpers;
using Com.DanLiris.Service.EmergencyAttendance.Lib.Facades;
namespace Com.DanLiris.Service.EmergencyAttendance.WebApi.Controllers.v1.AttendanceController
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/emergency")]
    [Authorize]
    public class AttendanceController : Controller
    {
        private string ApiVersion = "1.0.0";
        private readonly IMapper mapper;
        private readonly IdentityService identityService;
        private readonly IServiceProvider _serviceProvider;
        private readonly IAttendanceFacade facade;

        public AttendanceController(IMapper mapper, IServiceProvider serviceProvider, IAttendanceFacade facade)
        {
            this.mapper = mapper;
            this.facade = facade;
            _serviceProvider = serviceProvider;
            identityService = (IdentityService)serviceProvider.GetService(typeof(IdentityService));
        }

        [HttpGet("list/{type}")]
        public async Task<IActionResult> GetCheckOut([FromRoute] string type, int page = 1, int size = 25, string order = "{}", string keyword = null, string filter = "{}")
        {
            try
            {
                identityService.Username = User.Claims.Single(p => p.Type.Equals("username")).Value;
                var result = await facade.Read(type, page, size, order, keyword, filter);

                return Ok(result);
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
