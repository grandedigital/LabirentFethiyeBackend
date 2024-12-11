using LabirentFethiye.Common.Dtos.WebSiteDtos.MenuDtos.MenuRequestDtos;
using LabirentFethiye.Common.Dtos.WebSiteDtos.MenuDtos.MenuResponseDtos;
using LabirentFethiye.Common.Responses;

namespace LabirentFethiye.Application.Abstracts.WebSiteInterfaces
{
    public interface IMenuService
    {
        Task<ResponseDto<ICollection<MenuGetAllResponseDto>>> GetAll();
        Task<ResponseDto<MenuGetResponseDto>> Get(Guid Id);
        Task<ResponseDto<MenuGetResponseDto>> GetMenuBySlug(string slug);
        Task<ResponseDto<BaseResponseDto>> Create(MenuCreateRequestDto model, Guid userId);
        Task<ResponseDto<BaseResponseDto>> CreateDetail(MenuDetailCreateRequestDto model, Guid userId);
        Task<ResponseDto<BaseResponseDto>> Update(MenuUpdateRequestDto model, Guid userId);
        Task<ResponseDto<BaseResponseDto>> UpdateDetail(MenuDetailUpdateRequestDto model, Guid userId);
        Task<ResponseDto<BaseResponseDto>> DeleteHard(Guid Id);
    }
}
