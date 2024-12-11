using LabirentFethiye.Common.Enums;

namespace LabirentFethiye.Common.Dtos.ProjectDtos.PriceTableDtos.PriceTableRequestDtos
{
    public class PriceTableUpdateRequestDto
    {
        public Guid Id { get; set; }
        public string? Icon { get; set; }
        public decimal Price { get; set; }
        public PriceType PriceType { get; set; }
        public GeneralStatusType GeneralStatusType { get; set; }
    }
}
