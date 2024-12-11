using LabirentFethiye.Application.Abstracts.ProjectInterfaces;
using LabirentFethiye.Common.Dtos.ProjectDtos.PriceTableDtos.PriceTableRequestDtos;
using LabirentFethiye.Common.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LabirentFethiye.WebApi.Controllers
{
    [Authorize(AuthenticationSchemes = "Admin")]
    public class PriceTablesController : BaseController
    {
        private readonly IPriceTableService priceTableService;

        public PriceTablesController(IPriceTableService priceTableService)
        {
            this.priceTableService = priceTableService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromRoute] Guid Id)
        {
            try
            {
                var priceTable = await priceTableService.Get(Id);
                return StatusCode(priceTable.StatusCode, priceTable);
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
                var priceTables = await priceTableService.GetAll(VillaId, RoomId);
                return StatusCode(priceTables.StatusCode, priceTables);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm, FromBody] PriceTableCreateRequestDto model)
        {
            try
            {
                var priceTable = await priceTableService.Create(model, UserId);
                return StatusCode(priceTable.StatusCode, priceTable);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500));
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateDetail([FromForm, FromBody] PriceTableDetailCreateRequestDto model)
        {
            try
            {
                var priceTableDetail = await priceTableService.CreateDetail(model, UserId);
                return StatusCode(priceTableDetail.StatusCode, priceTableDetail);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromForm, FromBody] PriceTableUpdateRequestDto model)
        {
            try
            {
                var priceTable = await priceTableService.Update(model, UserId);
                return StatusCode(priceTable.StatusCode, priceTable);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500));
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDetail([FromForm, FromBody] PriceTableDetailUpdateRequestDto model)
        {
            try
            {
                var priceTableDetail = await priceTableService.UpdateDetail(model, UserId);
                return StatusCode(priceTableDetail.StatusCode, priceTableDetail);
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
                var photos = await priceTableService.DeleteHard(Id);
                return StatusCode(photos.StatusCode, photos);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500));
            }
        }
    }
}
