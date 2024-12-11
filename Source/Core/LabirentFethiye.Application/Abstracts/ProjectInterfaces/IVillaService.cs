using LabirentFethiye.Common.Dtos.ProjectDtos.VillaDtos.VillaRequestDtos;
using LabirentFethiye.Common.Dtos.ProjectDtos.VillaDtos.VillaResponseDtos;
using LabirentFethiye.Common.Responses;

namespace LabirentFethiye.Application.Abstracts.ProjectInterfaces
{
    public interface IVillaService
    {
        Task<ResponseDto<ICollection<VillaGetAllResponseDto>>> GetAll(VillaGetAllRequestDto model);
        Task<ResponseDto<VillaGetResponseDto>> Get(Guid Id);
        Task<ResponseDto<BaseResponseDto>> Create(VillaCreateRequestDto model, Guid userId);
        Task<ResponseDto<BaseResponseDto>> CreateDetail(VillaDetailCreateRequestDto model, Guid userId);
        Task<ResponseDto<BaseResponseDto>> Update(VillaUpdateRequestDto model, Guid userId);
        Task<ResponseDto<BaseResponseDto>> UpdateDetail(VillaDetailUpdateRequestDto model, Guid userId);
        Task<ResponseDto<BaseResponseDto>> VillaCategoryAsign(VillaCategoryAsignRequestDto model);
        Task<ResponseDto<ICollection<VillaGetAllAvailableDateResponseDto>>> GetVillaAvailableDates(Guid VillaId);
    }
}
