using LabirentFethiye.Common.Dtos.ProjectDtos.HotelDtos.HotelRequestDtos;
using LabirentFethiye.Common.Dtos.ProjectDtos.HotelDtos.HotelResponseDtos;
using LabirentFethiye.Common.Responses;

namespace LabirentFethiye.Application.Abstracts.ProjectInterfaces
{
    public interface IHotelService
    {
        Task<ResponseDto<ICollection<HotelGetAllResponseDto>>> GetAll(HotelGetAllRequestDto model);
        Task<ResponseDto<HotelGetResponseDto>> Get(Guid Id);
        Task<ResponseDto<BaseResponseDto>> Create(HotelCreateRequestDto model, Guid userId);
        Task<ResponseDto<BaseResponseDto>> CreateDetail(HotelDetailCreateRequestDto model, Guid userId);
        Task<ResponseDto<BaseResponseDto>> Update(HotelUpdateRequestDto model, Guid userId);
        Task<ResponseDto<BaseResponseDto>> UpdateDetail(HotelDetailUpdateRequestDto model, Guid userId);
    }
}
