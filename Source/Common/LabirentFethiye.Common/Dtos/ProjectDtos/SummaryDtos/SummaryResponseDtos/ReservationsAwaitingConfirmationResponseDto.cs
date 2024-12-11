namespace LabirentFethiye.Common.Dtos.ProjectDtos.SummaryDtos.SummaryResponseDtos
{
    public class ReservationsAwaitingConfirmationResponseDto
    {
        public Guid Id { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public string CustomerName { get; set; }
        public string ReservationNumber { get; set; }
        public string FacilityName { get; set; }
    }
}
