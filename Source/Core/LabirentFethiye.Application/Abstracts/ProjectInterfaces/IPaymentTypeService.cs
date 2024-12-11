using LabirentFethiye.Common.Dtos.ProjectDtos.PaymentTypeDtos.PaymentTypeRequestDtos;
using LabirentFethiye.Common.Dtos.ProjectDtos.PaymentTypeDtos.PaymentTypeResponseDtos;
using LabirentFethiye.Common.Responses;

namespace LabirentFethiye.Application.Abstracts.ProjectInterfaces
{
    public interface IPaymentTypeService
    {
        Task<ResponseDto<ICollection<PaymentTypeGetAllResponseDto>>> GetAll();
        Task<ResponseDto<PaymentTypeGetResponseDto>> Get(Guid Id);
        Task<ResponseDto<BaseResponseDto>> Create(PaymentTypeCreateRequestDto model, Guid userId);
        Task<ResponseDto<BaseResponseDto>> Update(PaymentTypeUpdateRequestDto model, Guid userId);
        Task<ResponseDto<BaseResponseDto>> Delete(Guid Id, Guid userId);
    }
}
