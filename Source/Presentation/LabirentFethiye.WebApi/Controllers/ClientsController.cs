using LabirentFethiye.Application.Abstracts.ProjectInterfaces;
using LabirentFethiye.Common.Dtos.ProjectDtos.ClientDtos.ClientRequestDtos;
using LabirentFethiye.Common.Dtos.ProjectDtos.ClientDtos.ClientResponseDtos;
using LabirentFethiye.Common.Requests;
using LabirentFethiye.Common.Responses;
using LabirentFethiye.Persistence.Concrete.ProjectConcretes;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LabirentFethiye.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService clientService;
        public ClientsController(IClientService clientService)
        {
            this.clientService = clientService;
        }

        #region Category
        [HttpGet]
        public async Task<IActionResult> GetAllCategory([FromQuery] ClientCategoryGetAllRequestDto model)
        {
            try
            {
                var categories = await clientService.GetAllCategory(model);
                return StatusCode(categories.StatusCode, categories);
            }
            catch (Exception ex)
            { return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500)); }
        }
        #endregion

        #region Villa

        [HttpGet]
        public async Task<IActionResult> GetAllVillaByCategorySlug([FromQuery] ClientVillaGetAllByCategorySlugRequestDto model)
        {
            try
            {
                var villas = await clientService.GetAllVillaByCategorySlug(model);
                return StatusCode(villas.StatusCode, villas);
            }
            catch (Exception ex)
            { return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500)); }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllVilla([FromQuery] ClientVillaGetAllRequestDto model)
        {
            try
            {
                var villas = await clientService.GetAllVilla(model);
                return StatusCode(villas.StatusCode, villas);
            }
            catch (Exception ex)
            { return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500)); }
        }

        [HttpGet]
        public async Task<IActionResult> GetVillaBySlug([FromQuery] ClientVillaGetBySlugRequestDto model)
        {
            try
            {
                var villa = await clientService.GetVillaBySlug(model);
                return StatusCode(villa.StatusCode, villa);
            }
            catch (Exception ex)
            { return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500)); }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDistanceRulerByVillaSlug([FromQuery] ClientDistanceRulerByVillaSlugRequestDto model)
        {
            try
            {
                var distanceRulers = await clientService.GetAllDistanceRulerByVillaSlug(model);
                return StatusCode(distanceRulers.StatusCode, distanceRulers);
            }
            catch (Exception ex)
            { return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500)); }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPriceTableByVillaSlug([FromQuery] ClientPriceTableGetAllByVillaSlugRequestDto model)
        {
            try
            {
                var priceTables = await clientService.GetAllPriceTableByVillaSlug(model);
                return StatusCode(priceTables.StatusCode, priceTables);
            }
            catch (Exception ex)
            { return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500)); }
        }

        [HttpGet]
        public async Task<IActionResult> GetReservationCalendarByVillaSlug([FromQuery] ClientReservationCalendarGetByVillaSlugRequestDto model)
        {
            try
            {
                var reservations = await clientService.GetReservationCalendarByVillaSlug(model);
                return StatusCode(reservations.StatusCode, reservations);
            }
            catch (Exception ex)
            { return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500)); }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCommentByVillaSlug([FromQuery] ClientCommentGetAllByVillaSlugRequestDto model)
        {
            try
            {
                var comments = await clientService.GetAllCommentByVillaSlug(model);
                return StatusCode(comments.StatusCode, comments);
            }
            catch (Exception ex)
            { return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500)); }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRecommendedVilla([FromQuery] ClientRecommendedVillaGetAllByVillaSlugRequestDto model)
        {
            try
            {
                var villas = await clientService.GetAllRecommendedVilla(model);
                return StatusCode(villas.StatusCode, villas);
            }
            catch (Exception ex)
            { return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500)); }
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllVillaSale([FromQuery] ClientVillaSaleGetAllRequestDto model)
        {
            try
            {
                var villas = await clientService.GetAllVillaSale(model);
                return StatusCode(villas.StatusCode, villas);
            }
            catch (Exception ex)
            { return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500)); }
        }
        #endregion

        #region Comment
        [HttpPost]
        public async Task<IActionResult> CreateComment([FromForm] ClientCommentCreateRequestDto model)
        {
            try
            {
                var comment = await clientService.CreateComment(model);
                return StatusCode(comment.StatusCode, comment);
            }
            catch (Exception ex)
            { return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500)); }
        }
        #endregion

        #region Town

        [HttpGet]
        public async Task<IActionResult> GetAllDistrict()
        {
            try
            {
                var districts = await clientService.GetAllDistrict();
                return StatusCode(districts.StatusCode, districts);
            }
            catch (Exception ex)
            { return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500)); }
        }

        #endregion

        #region Hotel Room
        [HttpGet]
        public async Task<IActionResult> GetAllHotel([FromQuery] ClientHotelGetAllRequestDto model)
        {
            try
            {
                var hotels = await clientService.GetAllHotel(model);
                return StatusCode(hotels.StatusCode, hotels);
            }
            catch (Exception ex)
            { return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500)); }
        }

        [HttpGet]
        public async Task<IActionResult> GetHotel([FromQuery] ClientHotelGetRequestDto model)
        {
            try
            {
                var hotel = await clientService.GetHotel(model);
                return StatusCode(hotel.StatusCode, hotel);
            }
            catch (Exception ex)
            { return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500)); }
        }

        [HttpGet]
        public async Task<IActionResult> GetRoom([FromQuery] ClientRoomGetRequestDto model)
        {
            try
            {
                var room = await clientService.GetRoom(model);
                return StatusCode(room.StatusCode, room);
            }
            catch (Exception ex)
            { return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500)); }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDistanceRulerByHotelSlug([FromQuery] ClientDistanceRulerByHotelSlugRequestDto model)
        {
            try
            {
                var distanceRulers = await clientService.GetAllDistanceRulerByHotelSlug(model);
                return StatusCode(distanceRulers.StatusCode, distanceRulers);
            }
            catch (Exception ex)
            { return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500)); }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPriceTableByRoomSlug([FromQuery] ClientPriceTableGetAllByRoomSlugRequestDto model)
        {
            try
            {
                var priceTables = await clientService.GetAllPriceTableByRoomSlug(model);
                return StatusCode(priceTables.StatusCode, priceTables);
            }
            catch (Exception ex)
            { return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500)); }
        }

        [HttpGet]
        public async Task<IActionResult> GetReservationCalendarByRoomSlug([FromQuery] ClientReservationCalendarGetByRoomSlugRequestDto model)
        {
            try
            {
                var reservations = await clientService.GetReservationCalendarByRoomSlug(model);
                return StatusCode(reservations.StatusCode, reservations);
            }
            catch (Exception ex)
            { return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500)); }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCommentByRoomSlug([FromQuery] ClientCommentGetAllByHotelSlugRequestDto model)
        {
            try
            {
                var comments = await clientService.GetAllCommentByHotelSlug(model);
                return StatusCode(comments.StatusCode, comments);
            }
            catch (Exception ex)
            { return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500)); }
        }

        #endregion

        #region Reservation
        [HttpGet]
        public async Task<IActionResult> GetReservation([FromQuery] ClientReservationGetRequestDto model)
        {
            try
            {
                var reservation = await clientService.GetReservation(model);
                return StatusCode(reservation.StatusCode, reservation);
            }
            catch (Exception ex)
            { return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500)); }
        }

        [HttpPost]
        public async Task<IActionResult> ReservationCreate([FromForm, FromBody] ClientReservationCreateRequestDto model)
        {
            try
            {
                var reservation = await clientService.ReservationCreate(model);
                return StatusCode(reservation.StatusCode, reservation);
            }
            catch (Exception ex)
            { return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500)); }
        }

        [HttpGet]
        public async Task<IActionResult> ReservationIsAvailible([FromQuery] ClientReservationIsAvailibleRequestDto model)
        {
            try
            {
                var reservationPrice = await clientService.ReservationIsAvailible(model);
                return StatusCode(200, reservationPrice);
            }
            catch (Exception ex)
            { return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500)); }
        }
        #endregion

        #region WebPages
        [HttpGet]
        public async Task<IActionResult> GetAllWebPage([FromQuery] ClientGetAllWebPageRequestDto model)
        {
            try
            {
                var districts = await clientService.GetAllWebPage(model);
                return StatusCode(districts.StatusCode, districts);
            }
            catch (Exception ex)
            { return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500)); }
        }

        [HttpGet]
        public async Task<IActionResult> GetWebPage([FromQuery] ClientWebPageGetRequestDto model)
        {
            try
            {
                var districts = await clientService.GetWebPage(model);
                return StatusCode(districts.StatusCode, districts);
            }
            catch (Exception ex)
            { return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500)); }
        }

        #endregion


        [HttpGet]
        public async Task<IActionResult> GetCurrencies()
        {
            try
            {
                var villas = await clientService.GetCurrency();
                return StatusCode(villas.StatusCode, villas);
            }
            catch (Exception ex)
            { return StatusCode((int)HttpStatusCode.BadRequest, ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500)); }
        }
    }

}
