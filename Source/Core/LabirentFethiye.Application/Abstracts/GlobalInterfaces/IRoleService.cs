using LabirentFethiye.Common.Dtos.GlobalDtos.RoleDtos.RoleRequestDtos;
using LabirentFethiye.Common.Dtos.GlobalDtos.RoleDtos.RoleResponseDtos;
using LabirentFethiye.Common.Responses;

namespace LabirentFethiye.Application.Abstracts.GlobalInterfaces
{
    public interface IRoleService
    {
        Task<ResponseDto<ICollection<RoleGetAllResponseDto>>> GetAll();
        Task<ResponseDto<BaseResponseDto>> Create(RoleCreateRequestDto model);
    }
}
