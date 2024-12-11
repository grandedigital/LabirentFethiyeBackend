namespace LabirentFethiye.Common.Dtos.ProjectDtos.PriceTableDtos.PriceTableRequestDtos
{
    public class PriceTableDetailCreateRequestDto
    {
        public string LanguageCode { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid PriceTableId { get; set; }
    }
}
