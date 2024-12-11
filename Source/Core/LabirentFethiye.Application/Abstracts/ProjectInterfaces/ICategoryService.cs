using LabirentFethiye.Common.Dtos.ProjectDtos.CategoryDtos.CategoryRequestDtos;
using LabirentFethiye.Common.Dtos.ProjectDtos.CategoryDtos.CategoryResponseDtos;
using LabirentFethiye.Common.Responses;

namespace LabirentFethiye.Application.Abstracts.ProjectInterfaces
{
    public interface ICategoryService
    {
        Task<ResponseDto<ICollection<CategoryGetAllResponseDto>>> GetAll(CategoryGetAllRequestDto model);
        Task<ResponseDto<CategoryGetResponseDto>> Get(Guid Id);
        Task<ResponseDto<BaseResponseDto>> Create(CategoryCreateRequestDto model, Guid userId);
        Task<ResponseDto<BaseResponseDto>> CreateDetail(CategoryDetailCreateRequestDto model, Guid userId);
        Task<ResponseDto<BaseResponseDto>> Update(CategoryUpdateRequestDto model, Guid userId);
        Task<ResponseDto<BaseResponseDto>> UpdateDetail(CategoryDetailUpdateRequestDto model, Guid userId);
    }
}
