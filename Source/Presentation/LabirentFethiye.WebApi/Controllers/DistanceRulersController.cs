using LabirentFethiye.Application.Abstracts.ProjectInterfaces;
using LabirentFethiye.Common.Dtos.ProjectDtos.DistanceRulerDtos.DistanceRulerRequestDtos;
using LabirentFethiye.Common.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LabirentFethiye.WebApi.Controllers
{
    [Authorize(AuthenticationSchemes = "Admin")]
    public class DistanceRulersController : BaseController
    {
        private readonly IDistanceRulerService distanceRulerService;

        public DistanceRulersController(IDistanceRulerService distanceRulerService)
        {
            this.distanceRulerService = distanceRulerService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromRoute] Guid Id)
        {
            try
            {
                var distanceRuler = await distanceRulerService.Get(Id);
                return StatusCode(distanceRuler.StatusCode, distanceRuler);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500));
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] Guid? VillaId, [FromQuery] Guid? HotelId)
        {
            try
            {
                var distanceRulers = await distanceRulerService.GetAll(VillaId, HotelId);
                return StatusCode(distanceRulers.StatusCode, distanceRulers);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm, FromBody] DistanceRulerCreateRequestDto model)
        {
            try
            {
                var distanceRuler = await distanceRulerService.Create(model, UserId);
                return StatusCode(distanceRuler.StatusCode, distanceRuler);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500));
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateDetail([FromForm, FromBody] DistanceRulerDetailCreateRequestDto model)
        {
            try
            {
                var distanceRulerDetail = await distanceRulerService.CreateDetail(model, UserId);
                return StatusCode(distanceRulerDetail.StatusCode, distanceRulerDetail);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromForm, FromBody] DistanceRulerUpdateRequestDto model)
        {
            try
            {
                var distanceRuler = await distanceRulerService.Update(model, UserId);
                return StatusCode(distanceRuler.StatusCode, distanceRuler);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500));
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDetail([FromForm, FromBody] DistanceRulerDetailUpdateRequestDto model)
        {
            try
            {
                var distanceRulerDetail = await distanceRulerService.UpdateDetail(model, UserId);
                return StatusCode(distanceRulerDetail.StatusCode, distanceRulerDetail);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500));
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteHard([FromRoute] Guid Id)
        {
            try
            {
                var photos = await distanceRulerService.DeleteHard(Id);
                return StatusCode(photos.StatusCode, photos);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500));
            }
        }
    }
}
