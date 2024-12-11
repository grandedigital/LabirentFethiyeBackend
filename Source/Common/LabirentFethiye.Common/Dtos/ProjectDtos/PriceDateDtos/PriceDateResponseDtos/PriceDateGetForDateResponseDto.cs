using LabirentFethiye.Common.Enums;

namespace LabirentFethiye.Common.Dtos.ProjectDtos.PriceDateDtos.PriceDateResponseDtos
{
    public class PriceDateGetForDateResponseDto
    {
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
        public PriceType PriceType { get; set; }
    }
}
