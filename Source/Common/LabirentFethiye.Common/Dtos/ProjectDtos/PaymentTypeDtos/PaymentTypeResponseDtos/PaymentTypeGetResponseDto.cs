namespace LabirentFethiye.Common.Dtos.ProjectDtos.PaymentTypeDtos.PaymentTypeResponseDtos
{
    public class PaymentTypeGetResponseDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<PaymentTypeGetResponseDtoPayment> Payments { get; set; }

    }
    public class PaymentTypeGetResponseDtoPayment
    {
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public bool InOrOut { get; set; }
    }
}
