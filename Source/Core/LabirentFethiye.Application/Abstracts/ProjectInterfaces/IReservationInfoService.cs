using LabirentFethiye.Common.Dtos.ProjectDtos.ReservationInfoDtos.ReservationInfoRequestDtos;
using LabirentFethiye.Common.Dtos.ProjectDtos.ReservationInfoDtos.ReservationInfoResponseDtos;
using LabirentFethiye.Common.Responses;

namespace LabirentFethiye.Application.Abstracts.ProjectInterfaces
{
    public interface IReservationInfoService
    {
        Task<ResponseDto<ICollection<ReservationInfoGetAllResponseDto>>> GetAll( Guid ReservationId);
        Task<ResponseDto<ReservationInfoGetResponseDto>> Get(Guid Id);
        Task<ResponseDto<BaseResponseDto>> Create(ReservationInfoCreateRequestDto model, Guid userId);
        Task<ResponseDto<BaseResponseDto>> Update(ReservationInfoUpdateRequestDto model, Guid userId);
        Task<ResponseDto<BaseResponseDto>> DeleteHard(Guid Id, Guid userId);
    }
}
