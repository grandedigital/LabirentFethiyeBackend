namespace LabirentFethiye.Common.Dtos.ProjectDtos.ReservationDtos.ReservationRequestDtos
{
    public class ReservationUpdateRequestDto
    {
        public Guid Id { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public decimal Discount { get; set; }
        public string? Note { get; set; }
        public bool HomeOwner { get; set; }

    }
}
