using LabirentFethiye.Application.Abstracts.ProjectInterfaces;
using LabirentFethiye.Common.Dtos.ProjectDtos.VillaDtos.VillaRequestDtos;
using LabirentFethiye.Common.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LabirentFethiye.WebApi.Controllers
{
    [Authorize(AuthenticationSchemes = "Admin")]
    public class VillasController : BaseController
    {
        private readonly IVillaService villaService;
        public VillasController(IVillaService villaService)
        {
            this.villaService = villaService;
        }

        #region Get Methods
        [HttpGet]
        public async Task<IActionResult> Get([FromRoute] Guid Id)
        {
            try
            {
                var villa = await villaService.Get(Id);
                return StatusCode(villa.StatusCode, villa);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500));
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] VillaGetAllRequestDto model)
        {
            try
            {
                var villas = await villaService.GetAll(model);
                return StatusCode(villas.StatusCode, villas);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500));
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetVillaAvailableDates([FromQuery] Guid VillaId)
        {
            try
            {
                var villas = await villaService.GetVillaAvailableDates(VillaId);
                return StatusCode(villas.StatusCode, villas);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500));
            }
        }
        #endregion

        #region Post Methods
        [HttpPost]
        public async Task<IActionResult> Create([FromForm, FromBody] VillaCreateRequestDto model)
        {
            try
            {
                var villa = await villaService.Create(model, UserId);
                return StatusCode(villa.StatusCode, villa);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500));
            }
        }

        [HttpPost]
        public async Task<IActionResult> VillaCategoryAsign([FromForm, FromBody] VillaCategoryAsignRequestDto model)
        {
            try
            {
                var villa = await villaService.VillaCategoryAsign(model);
                return StatusCode(villa.StatusCode, villa);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500));
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateDetail([FromForm, FromBody] VillaDetailCreateRequestDto model)
        {
            try
            {
                var villaDetail = await villaService.CreateDetail(model, UserId);
                return StatusCode(villaDetail.StatusCode, villaDetail);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromForm, FromBody] VillaUpdateRequestDto model)
        {
            try
            {
                var villa = await villaService.Update(model, UserId);
                return StatusCode(villa.StatusCode, villa);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500));
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDetail([FromForm, FromBody] VillaDetailUpdateRequestDto model)
        {
            try
            {
                var villaDetail = await villaService.UpdateDetail(model, UserId);
                return StatusCode(villaDetail.StatusCode, villaDetail);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500));
            }
        } 
        #endregion
    }
}
