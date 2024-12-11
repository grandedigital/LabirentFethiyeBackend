using LabirentFethiye.Common.Enums;

namespace LabirentFethiye.Common.Dtos.ProjectDtos.ReservationDtos.ReservationRequestDtos
{
    public class ReservationStatusUpdateRequestDto
    {
        public Guid Id{ get; set; }
        public ReservationStatusType  ReservationStatusType{ get; set; }
    }
}
