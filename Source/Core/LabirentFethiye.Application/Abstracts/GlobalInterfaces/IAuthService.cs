using LabirentFethiye.Common.Dtos.GlobalDtos.AuthDtos.AuthRequestDtos;
using LabirentFethiye.Common.Dtos.GlobalDtos.AuthDtos.AuthResponseDtos;
using LabirentFethiye.Common.Responses;

namespace LabirentFethiye.Application.Abstracts.GlobalInterfaces
{
    public interface IAuthService
    {
        Task<ResponseDto<LoginResponseDto>> LoginAsync(LoginRequestDto request);

    }
}
