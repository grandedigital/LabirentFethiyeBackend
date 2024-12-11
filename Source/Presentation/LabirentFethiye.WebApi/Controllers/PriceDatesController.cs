using LabirentFethiye.Application.Abstracts.ProjectInterfaces;
using LabirentFethiye.Common.Dtos.ProjectDtos.PriceDateDtos.PriceDateRequestDtos;
using LabirentFethiye.Common.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LabirentFethiye.WebApi.Controllers
{
    [Authorize(AuthenticationSchemes = "Admin")]
    public class PriceDatesController : BaseController
    {
        private readonly IPriceDateService priceDateService;

        public PriceDatesController(IPriceDateService priceDateService)
        {
            this.priceDateService = priceDateService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromRoute] Guid Id)
        {
            try
            {
                var priceDate = await priceDateService.Get(Id);
                return StatusCode(priceDate.StatusCode, priceDate);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500));
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] Guid? VillaId, [FromQuery] Guid? RoomId)
        {
            try
            {
                var priceDates = await priceDateService.GetAll(VillaId, RoomId);
                return StatusCode(priceDates.StatusCode, priceDates);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm, FromBody] PriceDateCreateRequestDto model)
        {
            try
            {
                var priceDate = await priceDateService.Create(model, UserId);
                return StatusCode(priceDate.StatusCode, priceDate);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromForm, FromBody] PriceDateUpdateRequestDto model)
        {
            try
            {
                var priceDate = await priceDateService.Update(model, UserId);
                return StatusCode(priceDate.StatusCode, priceDate);
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
                var priceDate = await priceDateService.DeleteHard(Id);
                return StatusCode(priceDate.StatusCode, priceDate);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500));
            }
        }
    }
}
