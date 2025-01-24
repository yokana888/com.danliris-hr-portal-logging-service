using AutoMapper;
using Com.Moonlay.NetCore.Lib.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using Com.DanLiris.Service.EmergencyAttendance.Lib.Services;
using Com.DanLiris.Service.EmergencyAttendance.WebApi.Helpers;
using Com.DanLiris.Service.EmergencyAttendance.Lib.Facades;
using Com.DanLiris.Service.EmergencyAttendance.Lib.ViewModels;
using Humanizer;
using Com.DanLiris.Service.EmergencyAttendance.Lib.Dto;
using Com.DanLiris.Service.EmergencyAttendance.Lib.Services.BlobStorage;
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
        private readonly IBlobStorage _blobStorage;
        private readonly IAttendanceFacade facade;

        public AttendanceController(IMapper mapper, IServiceProvider serviceProvider, IAttendanceFacade facade)
        {
            this.mapper = mapper;
            this.facade = facade;
            _serviceProvider = serviceProvider;
            _blobStorage = serviceProvider.GetService<IBlobStorage>();
            identityService = (IdentityService)serviceProvider.GetService(typeof(IdentityService));
        }

        private void VerifyUser()
        {
            identityService.Username = User.Claims.ToArray().SingleOrDefault(p => p.Type.Equals("username")).Value;
            identityService.Token = Request.Headers["Authorization"].FirstOrDefault().Replace("Bearer ", "");
            identityService.TimezoneOffset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
        }

        //[HttpGet("list/{type}")]
        //public async Task<IActionResult> Get([FromRoute] string type, int page = 1, int size = 25)
        //{
        //    try
        //    {
        //        identityService.Username = User.Claims.Single(p => p.Type.Equals("username")).Value;
        //        var result = await facade.Read(type, page, size);

        //        return Ok(result);
        //    }
        //    catch (Exception e)
        //    {
        //        Dictionary<string, object> Result =
        //            new ResultFormatter(ApiVersion, General.INTERNAL_ERROR_STATUS_CODE, e.Message)
        //            .Fail();
        //        return StatusCode(General.INTERNAL_ERROR_STATUS_CODE, Result);
        //    }
        //}

        [HttpPost("check-in")]
        public async Task<IActionResult> CheckIn([FromBody] CheckInViewModel viewModel)
        {
            try
            {
                VerifyUser();

                viewModel.CheckTime = DateTimeOffset.Now.ToUniversalTime();
                viewModel.Username = identityService.Username;

                var result = await facade.CheckIn(viewModel);

                return Created("", result);
            }
            catch (ServiceValidationExeption e)
            {
                var result = new ResultFormatter(ApiVersion, General.BAD_REQUEST_STATUS_CODE, General.BAD_REQUEST_MESSAGE)
                    .Fail(e);
                return BadRequest(result);
            }
            catch (Exception e)
            {
                var result = new ResultFormatter(ApiVersion, General.INTERNAL_ERROR_STATUS_CODE, e.Message)
                    .Fail();
                return StatusCode(General.INTERNAL_ERROR_STATUS_CODE, result);
            }
        }

        [HttpPost("check-out")]
        public async Task<IActionResult> CheckOut([FromBody] CheckOutViewModel viewModel)
        {
            try
            {
                VerifyUser();

                viewModel.CheckTime = DateTimeOffset.Now.ToUniversalTime();
                viewModel.Username = identityService.Username;

                var result = await facade.CheckOut(viewModel);

                return Created("", result);
            }
            catch (ServiceValidationExeption e)
            {
                var result = new ResultFormatter(ApiVersion, General.BAD_REQUEST_STATUS_CODE, General.BAD_REQUEST_MESSAGE)
                    .Fail(e);
                return BadRequest(result);
            }
            catch (Exception e)
            {
                var result = new ResultFormatter(ApiVersion, General.INTERNAL_ERROR_STATUS_CODE, e.Message)
                    .Fail();
                return StatusCode(General.INTERNAL_ERROR_STATUS_CODE, result);
            }
        }

        [HttpPost("upload-attendance-image/{employeeIdentity}")]
        public async Task<IActionResult> UploadAttendancemImage([FromRoute] string employeeIdentity, IFormFile formFile)
        {
            try
            {
                VerifyUser();

                if (formFile != null && IsImage(formFile))
                {
                    if (formFile.Length > 0)
                    {
                        var filename = employeeIdentity + formFile.FileName;
                        var result = await _blobStorage.Upload(formFile.OpenReadStream(), filename);

                        return Created("", new { result });
                    }
                }

                return BadRequest("File is not a valid image");
            }
            catch (Exception e)
            {
                var result = new ResultFormatter(ApiVersion, General.INTERNAL_ERROR_STATUS_CODE, e.Message)
                    .Fail();
                return StatusCode(General.INTERNAL_ERROR_STATUS_CODE, result);
            }
        }

        [HttpPost("upload-attendance-image/base64/{employeeIdentity}")]
        public async Task<IActionResult> UploadAttendancemImageBase64([FromRoute] string employeeIdentity, [FromBody] UploadBase64 uploadBase64)
        {
            try
            {
                VerifyUser();

                if (!string.IsNullOrWhiteSpace(uploadBase64.Base64String) && !string.IsNullOrWhiteSpace(uploadBase64.FileName))
                {

                    var bytes = Convert.FromBase64String(uploadBase64.Base64String);
                    var contents = new MemoryStream(bytes);

                    var filename = employeeIdentity + uploadBase64.FileName;
                    var result = await _blobStorage.Upload(contents, filename);

                    return Created("", new { result });
                }

                return BadRequest("File is not a valid image");
            }
            catch (Exception e)
            {
                var result = new ResultFormatter(ApiVersion, General.INTERNAL_ERROR_STATUS_CODE, e.Message)
                    .Fail();
                return StatusCode(General.INTERNAL_ERROR_STATUS_CODE, result);
            }
        }

        private bool IsImage(IFormFile formFile)
        {
            if (formFile.ContentType.Contains("image"))
            {
                return true;
            }

            string[] formats = new string[] { ".jpg", ".png", ".gif", ".jpeg", ".heic" };

            return formats.Any(item => formFile.FileName.EndsWith(item, StringComparison.OrdinalIgnoreCase));
        }
    }
}
