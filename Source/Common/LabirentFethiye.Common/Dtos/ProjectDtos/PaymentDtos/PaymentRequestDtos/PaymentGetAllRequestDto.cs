using LabirentFethiye.Common.Requests;

namespace LabirentFethiye.Common.Dtos.ProjectDtos.PaymentDtos.PaymentRequestDtos
{
    public class PaymentGetAllRequestDto
    {
        public PaymentGetAllRequestDto()
        {
            Pagination = new Pagination()
            {
                Size = 10,
                Page = 0
            };
        }
        public Pagination? Pagination { get; set; }
        public Guid? ReservationId { get; set; }
        public Guid? VillaId { get; set; }
        public Guid? HotelId { get; set; }
        public Guid? RoomId { get; set; }
    }
}
