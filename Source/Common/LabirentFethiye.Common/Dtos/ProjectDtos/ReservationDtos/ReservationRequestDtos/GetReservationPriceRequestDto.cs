namespace LabirentFethiye.Common.Dtos.ProjectDtos.ReservationDtos.ReservationRequestDtos
{
    public class GetReservationPriceRequestDto
    {
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public Guid? VillaId { get; set; }
        public Guid? RoomId { get; set; }
    }
}
