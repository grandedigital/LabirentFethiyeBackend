using LabirentFethiye.Common.Dtos.ProjectDtos.ReservationDtos.ReservationRequestDtos;
using LabirentFethiye.Common.Dtos.ProjectDtos.ReservationDtos.ReservationResponseDtos;
using LabirentFethiye.Common.Requests;
using LabirentFethiye.Common.Responses;

namespace LabirentFethiye.Application.Abstracts.ProjectInterfaces
{
    public interface IReservationService
    {
        Task<ResponseDto<ICollection<ReservationGetAllResponseDto>>> GetAll( GetAllReservationRequestDto model);
        Task<ResponseDto<ICollection<ReservationGetAllResponseDto>>> GetAllForVilla(Pagination pagination, GetAllReservationRequestDto model);
        Task<ResponseDto<ICollection<ReservationGetAllResponseDto>>> GetAllForRoom(Pagination pagination, GetAllReservationRequestDto model);
        Task<ResponseDto<ReservationGetResponseDto>> Get(Guid Id);
        Task<ResponseDto<BaseResponseDto>> Create(ReservationCreateRequestDto model, Guid userId);
        Task<ResponseDto<BaseResponseDto>> Update(ReservationUpdateRequestDto model, Guid userId);
        Task<ResponseDto<BaseResponseDto>> ReservationStatusUpdate(ReservationStatusUpdateRequestDto model, Guid userId);
        Task<ResponseDto<BaseResponseDto>> Delete(Guid Id, Guid userId);
        Task<ResponseDto<GetReservationPriceResponseDto>> GetReservationPrice(GetReservationPriceRequestDto model);
        Task<bool> IsAvailible(ReservationIsAvailibleRequestDto model);
    }
}
