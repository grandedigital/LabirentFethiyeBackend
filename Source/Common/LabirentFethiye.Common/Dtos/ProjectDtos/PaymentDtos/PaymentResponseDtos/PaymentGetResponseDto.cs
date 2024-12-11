using LabirentFethiye.Common.Enums;

namespace LabirentFethiye.Common.Dtos.ProjectDtos.PaymentDtos.PaymentRequestDtos
{
    public class PaymentGetResponseDto
    {
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public bool InOrOut { get; set; }
        public PriceType PriceType { get; set; }
        public Guid PaymentTypeId { get; set; }
        public PaymentGetAllResponseDtoPaymentType PaymentType { get; set; }
    }

    public class PaymentGetResponseDtoPaymentType
    {
        public string Title { get; set; }
    }
}
