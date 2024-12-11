using LabirentFethiye.Common.Dtos.ProjectDtos.PhotoDtos.PhotoRequestDtos;
using LabirentFethiye.Common.Dtos.ProjectDtos.PhotoDtos.PhotoResponseDtos;
using LabirentFethiye.Common.Responses;

namespace LabirentFethiye.Application.Abstracts.ProjectInterfaces
{
    public interface IPhotoService
    {
        Task<ResponseDto<ICollection<PhotoGetAllResponseDto>>> GetAll(PhotoGetAllRequestDto model);
        Task<ResponseDto<PhotoGetResponseDto>> Get(Guid Id);
        Task<ResponseDto<BaseResponseDto>> Create(PhotoCreateRequestDto model, Guid userId);
        Task<ResponseDto<BaseResponseDto>> CreateMultiPhoto(PhotoCreateMultiRequestDto model, Guid userId);
        Task<ResponseDto<BaseResponseDto>> Update(PhotoUpdateRequestDto model, Guid userId);
        Task<ResponseDto<BaseResponseDto>> UpdateLine(List<PhotoUpdateLineRequestDto> models, Guid userId);
        Task<ResponseDto<BaseResponseDto>> DeleteHard(Guid Id);
    }
}
