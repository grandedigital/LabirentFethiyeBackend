using LabirentFethiye.Common.Enums;

namespace LabirentFethiye.Common.Dtos.ProjectDtos.PaymentDtos.PaymentRequestDtos
{
    public class PaymentGetAllResponseDto
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public bool InOrOut { get; set; }
        public PriceType PriceType { get; set; }
        public DateTime CreatedAt { get; set; }

        public Guid PaymentTypeId { get; set; }
        public PaymentGetAllResponseDtoPaymentType PaymentType { get; set; }
    }

    public class PaymentGetAllResponseDtoPaymentType
    {
        public string Title { get; set; }
    }
}
