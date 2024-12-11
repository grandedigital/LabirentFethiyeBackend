using LabirentFethiye.Common.Dtos.ProjectDtos.PaymentDtos.PaymentRequestDtos;
using LabirentFethiye.Common.Responses;

namespace LabirentFethiye.Application.Abstracts.ProjectInterfaces
{
    public interface IPaymentService
    {
        Task<ResponseDto<ICollection<PaymentGetAllResponseDto>>> GetAll(PaymentGetAllRequestDto model);
        Task<ResponseDto<PaymentGetResponseDto>> Get(Guid Id);
        Task<ResponseDto<BaseResponseDto>> Create(PaymentCreateRequestDto model, Guid userId);
        Task<ResponseDto<BaseResponseDto>> Update(PaymentUpdateRequestDto model, Guid userId);
    }
}
