using LabirentFethiye.Common.Dtos.ProjectDtos.ClientDtos.ClientRequestDtos;
using LabirentFethiye.Common.Dtos.ProjectDtos.ClientDtos.ClientResponseDtos;
using LabirentFethiye.Common.Requests;
using LabirentFethiye.Common.Responses;

namespace LabirentFethiye.Application.Abstracts.ProjectInterfaces
{
    public interface IClientService
    {
        #region Category
        Task<ResponseDto<ICollection<ClientCategoryGetAllResponseDto>>> GetAllCategory(ClientCategoryGetAllRequestDto model);
        #endregion


        #region Villa
        Task<ResponseDto<ICollection<ClientVillaGetAllByCategorySlugResponseDto>>> GetAllVillaByCategorySlug(ClientVillaGetAllByCategorySlugRequestDto model);
        Task<ResponseDto<ClientVillaGetBySlugResponseDto>> GetVillaBySlug(ClientVillaGetBySlugRequestDto model);
        Task<ResponseDto<ICollection<ClientVillaGetAllResponseDto>>> GetAllVilla(ClientVillaGetAllRequestDto model);
        Task<ResponseDto<ClientVillaGetResponseDto>> GetVilla(ClientVillaGetRequestDto model);
        Task<ResponseDto<ICollection<ClientDistanceRulerByVillaSlugResponseDto>>> GetAllDistanceRulerByVillaSlug(ClientDistanceRulerByVillaSlugRequestDto model);
        Task<ResponseDto<ICollection<ClientPriceTableGetAllByVillaSlugResponseDto>>> GetAllPriceTableByVillaSlug(ClientPriceTableGetAllByVillaSlugRequestDto model);
        Task<ResponseDto<ICollection<ClientReservationCalendarGetByVillaSlugResponseDto>>> GetReservationCalendarByVillaSlug(ClientReservationCalendarGetByVillaSlugRequestDto model);
        Task<ResponseDto<ICollection<ClientCommentGetAllByVillaSlugResponseDto>>> GetAllCommentByVillaSlug(ClientCommentGetAllByVillaSlugRequestDto model);
        Task<ResponseDto<ICollection<ClientRecommendedVillaGetAllByVillaSlugResponseDto>>> GetAllRecommendedVilla(ClientRecommendedVillaGetAllByVillaSlugRequestDto model);
        Task<ResponseDto<BaseResponseDto>> CreateComment(ClientCommentCreateRequestDto model);
        Task<ResponseDto<ICollection<ClientVillaSaleGetAllResponseDto>>> GetAllVillaSale(ClientVillaSaleGetAllRequestDto model);

        #endregion

        #region Hotel
        Task<ResponseDto<ClientHotelGetResponseDto>> GetHotel(ClientHotelGetRequestDto model);
        Task<ResponseDto<ClientRoomGetResponseDto>> GetRoom(ClientRoomGetRequestDto model);
        Task<ResponseDto<ICollection<ClientHotelGetAllResponseDto>>> GetAllHotel(ClientHotelGetAllRequestDto model);

        Task<ResponseDto<ICollection<ClientDistanceRulerByHotelSlugResponseDto>>> GetAllDistanceRulerByHotelSlug(ClientDistanceRulerByHotelSlugRequestDto model);
        Task<ResponseDto<ICollection<ClientPriceTableGetAllByRoomSlugResponseDto>>> GetAllPriceTableByRoomSlug(ClientPriceTableGetAllByRoomSlugRequestDto model);
        Task<ResponseDto<ICollection<ClientReservationCalendarGetByRoomSlugResponseDto>>> GetReservationCalendarByRoomSlug(ClientReservationCalendarGetByRoomSlugRequestDto model);
        Task<ResponseDto<ICollection<ClientCommentGetAllByHotelSlugResponseDto>>> GetAllCommentByHotelSlug(ClientCommentGetAllByHotelSlugRequestDto model);

        #endregion

        #region WebPages
        Task<ResponseDto<ICollection<ClienWebPageGetAllResponseDto>>> GetAllWebPage(ClientGetAllWebPageRequestDto model);
        Task<ResponseDto<ClientWebPageGetResponseDto>> GetWebPage(ClientWebPageGetRequestDto model);

        #endregion

        #region Reservation
        Task<ResponseDto<ClientReservationGetResponseDto>> GetReservation(ClientReservationGetRequestDto model);
        Task<ResponseDto<ClientReservationCreateResponseDto>> ReservationCreate(ClientReservationCreateRequestDto model);
        Task<ResponseDto<ClientReservationIsAvailibleResponseDto>> ReservationIsAvailible(ClientReservationIsAvailibleRequestDto model);

        #endregion

        Task<ResponseDto<ClientCurrencyGetResponseDto>> GetCurrency();
        Task<ResponseDto<ICollection<ClientDistrictGetAllResponseDto>>> GetAllDistrict();


    }
}
