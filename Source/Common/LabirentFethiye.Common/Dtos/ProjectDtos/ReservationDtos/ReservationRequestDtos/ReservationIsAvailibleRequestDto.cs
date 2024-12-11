namespace LabirentFethiye.Common.Dtos.ProjectDtos.ReservationDtos.ReservationRequestDtos
{
    public class ReservationIsAvailibleRequestDto
    {
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public Guid? VillaId { get; set; }
        public Guid? RoomId { get; set; }
        public Guid? ReservationId { get; set; }
    }
}
