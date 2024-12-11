using LabirentFethiye.Common.Dtos.GlobalDtos.TownDtos.TownResponseDtos;
using LabirentFethiye.Common.Responses;

namespace LabirentFethiye.Application.Abstracts.ProjectInterfaces
{
    public interface ITownService
    {
        Task<ResponseDto<ICollection<TownGetAllResponseDto>>> GetAll();
        Task<ResponseDto<ICollection<TownGetAllByDistricyIdResponseDto>>> GetAllByDistrictId(Guid DistrictId);
    }
}
