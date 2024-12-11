using LabirentFethiye.Common.Dtos.ProjectDtos.SummaryDtos.SummaryResponseDtos;
using LabirentFethiye.Common.Responses;

namespace LabirentFethiye.Application.Abstracts.ProjectInterfaces
{
    public interface ISummaryService
    {

        Task<ResponseDto<ICollection<ReservationsAwaitingConfirmationResponseDto>>> ReservationsAwaitingConfirmation();
        Task<ResponseDto<ICollection<ThreeDayCheckInCheckOutReservationResponseDto>>> ThreeDayCheckInCheckOutReservation();
        Task<ResponseDto<ICollection<CommentsAwaitingApprovalResponseDto>>> CommentsAwaitingApproval();
        Task<ResponseDto<ICollection<FiveDaysAvailableFacilitiesResponseDto>>> FiveDaysAvailableFacilities();
    }
}
