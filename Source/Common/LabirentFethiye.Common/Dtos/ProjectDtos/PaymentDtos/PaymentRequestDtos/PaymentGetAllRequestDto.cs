namespace LabirentFethiye.Common.Dtos.ProjectDtos.PaymentDtos.PaymentRequestDtos
{
    public class PaymentGetAllRequestDto
    {
        public Guid? ReservationId { get; set; }
        public Guid? VillaId { get; set; }
        public Guid? HotelId { get; set; }
        public Guid? RoomId { get; set; }
    }
}
