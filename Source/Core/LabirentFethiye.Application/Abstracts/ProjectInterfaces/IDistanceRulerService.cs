using LabirentFethiye.Common.Dtos.ProjectDtos.DistanceRulerDtos.DistanceRulerRequestDtos;
using LabirentFethiye.Common.Dtos.ProjectDtos.DistanceRulerDtos.DistanceRulerResponseDtos;
using LabirentFethiye.Common.Responses;

namespace LabirentFethiye.Application.Abstracts.ProjectInterfaces
{
    public interface IDistanceRulerService
    {
        Task<ResponseDto<ICollection<DistanceRulerGetAllResponseDto>>> GetAll(Guid? VillaId, Guid? HotelId);
        Task<ResponseDto<DistanceRulerGetResponseDto>> Get(Guid Id);
        Task<ResponseDto<BaseResponseDto>> Create(DistanceRulerCreateRequestDto model, Guid userId);
        Task<ResponseDto<BaseResponseDto>> CreateDetail(DistanceRulerDetailCreateRequestDto model, Guid userId);
        Task<ResponseDto<BaseResponseDto>> Update(DistanceRulerUpdateRequestDto model, Guid userId);
        Task<ResponseDto<BaseResponseDto>> UpdateDetail(DistanceRulerDetailUpdateRequestDto model, Guid userId);
        Task<ResponseDto<BaseResponseDto>> DeleteHard(Guid Id);
    }
}
