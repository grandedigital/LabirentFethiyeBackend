using LabirentFethiye.Common.Dtos.ProjectDtos.PriceTableDtos.PriceTableRequestDtos;
using LabirentFethiye.Common.Dtos.ProjectDtos.PriceTableDtos.PriceTableResponseDtos;
using LabirentFethiye.Common.Responses;

namespace LabirentFethiye.Application.Abstracts.ProjectInterfaces
{
    public interface IPriceTableService
    {
        Task<ResponseDto<ICollection<PriceTableGetAllResponseDto>>> GetAll(Guid? VillaId, Guid? RoomId);
        Task<ResponseDto<PriceTableGetResponseDto>> Get(Guid Id);
        Task<ResponseDto<BaseResponseDto>> Create(PriceTableCreateRequestDto model, Guid userId);
        Task<ResponseDto<BaseResponseDto>> CreateDetail(PriceTableDetailCreateRequestDto model, Guid userId);
        Task<ResponseDto<BaseResponseDto>> Update(PriceTableUpdateRequestDto model, Guid userId);
        Task<ResponseDto<BaseResponseDto>> UpdateDetail(PriceTableDetailUpdateRequestDto model, Guid userId);
        Task<ResponseDto<BaseResponseDto>> DeleteHard(Guid Id);
    }
}
