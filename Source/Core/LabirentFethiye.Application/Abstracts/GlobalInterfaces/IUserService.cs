using LabirentFethiye.Common.Dtos.GlobalDtos.UserDtos.UserRequestDtos;
using LabirentFethiye.Common.Dtos.GlobalDtos.UserDtos.UserResponseDtos;
using LabirentFethiye.Common.Responses;

namespace LabirentFethiye.Application.Abstracts.GlobalInterfaces
{
    public interface IUserService
    {
        Task<ResponseDto<ICollection<UserGetAllResponseDto>>> GetAll(UserGetAllRequestDto requestModel);
        Task<ResponseDto<BaseResponseDto>> Create(UserCreateRequestDto requestModel);
        Task<ResponseDto<BaseResponseDto>> UserRoleAsign(UserRoleAsignRequestDto requestModel);
    }
}
