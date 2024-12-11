using LabirentFethiye.Common.Dtos.GlobalDtos.MailDtos.MailRequestDtos;
using LabirentFethiye.Common.Dtos.GlobalDtos.MailDtos.MailResponseDtos;
using LabirentFethiye.Common.Responses;

namespace LabirentFethiye.Infastructure.Abstract.Interfaces
{
    public interface IMailService
    {
        Task<ResponseDto<SendMailResponseDto>> SendMailAsync(SendMailRequestDto model);
        Task<ResponseDto<SendMailResponseDto>> SendMailAsync(MultiSendMailRequestDto model);
        Task<ResponseDto<SendMailResponseDto>> SendPasswordResetMailAsync(SendMailPasswordResetRequestDto model);

        Task<ResponseDto<SendMailResponseDto>> ReservationCreateSendMailAsync(ReservationCreateMailRequestDto model);
    }
}
