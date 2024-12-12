using LabirentFethiye.Application.Abstracts.ProjectInterfaces;
using LabirentFethiye.Common.Dtos.ProjectDtos.ReservationDtos.ReservationRequestDtos;
using LabirentFethiye.Common.Requests;
using LabirentFethiye.Common.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LabirentFethiye.WebApi.Controllers
{
    [Authorize(AuthenticationSchemes = "Admin")]
    public class ReservationsController : BaseController
    {
        private readonly IReservationService reservationService;

        public ReservationsController(IReservationService reservationService)
        {
            this.reservationService = reservationService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromRoute] Guid Id)
        {
            try
            {
                var reservation = await reservationService.Get(Id);
                return StatusCode(reservation.StatusCode, reservation);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500));
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllReservationRequestDto model)
        {
            try
            {
                var GetAllReservation = await reservationService.GetAll( model);
                return StatusCode(GetAllReservation.StatusCode, GetAllReservation);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500));
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllForVilla([FromQuery] Pagination pagination, [FromQuery] GetAllReservationRequestDto model)
        {
            try
            {
                var GetAllReservation = await reservationService.GetAllForVilla(pagination, model);
                return StatusCode(GetAllReservation.StatusCode, GetAllReservation);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500));
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllForRoom([FromQuery] Pagination pagination, [FromQuery] GetAllReservationRequestDto model)
        {
            try
            {
                var GetAllReservation = await reservationService.GetAllForRoom(pagination, model);
                return StatusCode(GetAllReservation.StatusCode, GetAllReservation);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm, FromBody] ReservationCreateRequestDto model)
        {
            try
            {
                var reservation = await reservationService.Create(model, UserId);
                return StatusCode(reservation.StatusCode, reservation);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromForm, FromBody] ReservationUpdateRequestDto model)
        {
            try
            {
                var reservation = await reservationService.Update(model, UserId);
                return StatusCode(reservation.StatusCode, reservation);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500));
            }
        }

        [HttpPost]
        public async Task<IActionResult> ReservationStatusUpdate([FromForm, FromBody] ReservationStatusUpdateRequestDto model)
        {
            try
            {
                var reservation = await reservationService.ReservationStatusUpdate(model, UserId);
                return StatusCode(reservation.StatusCode, reservation);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete([FromRoute] Guid Id)
        {
            try
            {
                var reservation = await reservationService.Delete(Id, UserId);
                return StatusCode(reservation.StatusCode, reservation);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500));
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetReservationPrice([FromQuery] GetReservationPriceRequestDto model)
        {
            try
            {
                var reservationPrice = await reservationService.GetReservationPrice(model);
                return StatusCode(reservationPrice.StatusCode, reservationPrice);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500));
            }
        }

        [HttpGet]
        public async Task<IActionResult> IsAvailible([FromQuery] ReservationIsAvailibleRequestDto model)
        {
            try
            {
                var reservationPrice = await reservationService.IsAvailible(model);
                return StatusCode(200, reservationPrice);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500));
            }
        }
    }
}
