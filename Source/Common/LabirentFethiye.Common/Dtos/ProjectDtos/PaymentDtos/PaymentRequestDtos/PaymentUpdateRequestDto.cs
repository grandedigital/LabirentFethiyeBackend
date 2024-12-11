using LabirentFethiye.Common.Enums;

namespace LabirentFethiye.Common.Dtos.ProjectDtos.PaymentDtos.PaymentRequestDtos
{
    public class PaymentUpdateRequestDto
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        public bool InOrOut { get; set; }
        public PriceType PriceType { get; set; }
        public Guid PaymentTypeId { get; set; }
        public Guid? VillaId { get; set; }
        public Guid? HotelId { get; set; }
        public Guid? RoomId { get; set; }

        public GeneralStatusType GeneralStatusType { get; set; }
    }
}
