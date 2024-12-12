using LabirentFethiye.Common.Dtos.ProjectDtos.PriceDateDtos.PriceDateRequestDtos;
using LabirentFethiye.Common.Dtos.ProjectDtos.PriceDateDtos.PriceDateResponseDtos;
using LabirentFethiye.Common.Responses;

namespace LabirentFethiye.Application.Abstracts.ProjectInterfaces
{
    public interface IPriceDateService
    {
        Task<ResponseDto<ICollection<PriceDateGetAllResponseDto>>> GetAll(Guid? VillaId, Guid? RoomId);
        Task<ResponseDto<PriceDateGetResponseDto>> Get(Guid Id);
        Task<ResponseDto<BaseResponseDto>> Create(PriceDateCreateRequestDto model, Guid userId);
        Task<ResponseDto<BaseResponseDto>> Update(PriceDateUpdateRequestDto model, Guid userId);
        Task<ResponseDto<BaseResponseDto>> DeleteHard(Guid Id);
        Task<ResponseDto<ICollection<PriceDateGetForDateResponseDto>>> GetPriceForDate(PriceDateGetForDateRequestDto model);
        //Task<bool> GetPriceForDate(PriceDateGetForDateRequestDto model);
        Task<bool> PriceValidationControl(PriceDateGetForDateRequestDto model);

    }
}
