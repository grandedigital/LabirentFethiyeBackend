using LabirentFethiye.Common.Enums;

namespace LabirentFethiye.Common.Dtos.ProjectDtos.PaymentTypeDtos.PaymentTypeRequestDtos
{
    public class PaymentTypeUpdateRequestDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public GeneralStatusType GeneralStatusType { get; set; }
    }
}
