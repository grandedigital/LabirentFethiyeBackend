using LabirentFethiye.Common.Dtos.WebSiteDtos.WebPageDtos.WebPageRequestDto;
using LabirentFethiye.Common.Dtos.WebSiteDtos.WebPageDtos.WebPageResponseDto;
using LabirentFethiye.Common.Responses;

namespace LabirentFethiye.Application.Abstracts.WebSiteInterfaces
{
    public interface IWebPageService
    {
        Task<ResponseDto<BaseResponseDto>> Create(WebPageCreateRequestDto model, Guid userId);
        Task<ResponseDto<ICollection<WebPageGetAllResponseDto>>> GetAll(WebPageGetAllRequstDtos model);
        Task<ResponseDto<WebPageGetResponseDto>> Get(Guid Id);
        Task<ResponseDto<BaseResponseDto>> CreateDetail(WebPageDetailCreateRequestDto model, Guid userId);
        Task<ResponseDto<BaseResponseDto>> Update(WebPageUpdateRequestDto model, Guid userId);
        Task<ResponseDto<BaseResponseDto>> UpdateDetail(WebPageDetailUpdateRequestDto model, Guid userId);
        Task<ResponseDto<BaseResponseDto>> DeleteHard(Guid Id);

        Task<ResponseDto<BaseResponseDto>> CreatePhoto(WebPhotoCreateRequestDto model, Guid userId);
        Task<ResponseDto<ICollection<WebPhotoGetAllResponseDto>>> GetAllWebPhoto(Guid WebPageId);
        Task<ResponseDto<BaseResponseDto>> DeleteHardWebPhoto(Guid Id);
        Task<ResponseDto<BaseResponseDto>> UpdateLine(List<WebPhotoUpdateLineRequestDto> models, Guid userId);


    }


}
