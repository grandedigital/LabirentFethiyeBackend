using LabirentFethiye.Common.Enums;

namespace LabirentFethiye.Common.Dtos.ProjectDtos.PriceTableDtos.PriceTableRequestDtos
{
    public class PriceTableCreateRequestDto
    {
        public string? Icon { get; set; }
        public decimal Price { get; set; }
        public PriceType PriceType { get; set; }
        public Guid? VillaId { get; set; }
        public Guid? RoomId { get; set; }

        public string LanguageCode { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
