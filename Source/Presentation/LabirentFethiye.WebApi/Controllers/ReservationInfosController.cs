using LabirentFethiye.Application.Abstracts.ProjectInterfaces;
using LabirentFethiye.Common.Dtos.ProjectDtos.ReservationInfoDtos.ReservationInfoRequestDtos;
using LabirentFethiye.Common.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LabirentFethiye.WebApi.Controllers
{
    [Authorize(AuthenticationSchemes = "Admin")]
    public class ReservationInfosController : BaseController
    {
        private readonly IReservationInfoService reservationInfoService;

        public ReservationInfosController(IReservationInfoService reservationInfoService)
        {
            this.reservationInfoService = reservationInfoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromRoute] Guid Id)
        {
            try
            {
                var reservation = await reservationInfoService.Get(Id);
                return StatusCode(reservation.StatusCode, reservation);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500));
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] Guid ReservationId)
        {
            try
            {
                var GetAllReservation = await reservationInfoService.GetAll(ReservationId);
                return StatusCode(GetAllReservation.StatusCode, GetAllReservation);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm, FromBody] ReservationInfoCreateRequestDto model)
        {
            try
            {
                var reservation = await reservationInfoService.Create(model, UserId);
                return StatusCode(reservation.StatusCode, reservation);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromForm, FromBody] ReservationInfoUpdateRequestDto model)
        {
            try
            {
                var reservation = await reservationInfoService.Update(model, UserId);
                return StatusCode(reservation.StatusCode, reservation);
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
                var reservation = await reservationInfoService.DeleteHard(Id, UserId);
                return StatusCode(reservation.StatusCode, reservation);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500));
            }
        }
    }
}
