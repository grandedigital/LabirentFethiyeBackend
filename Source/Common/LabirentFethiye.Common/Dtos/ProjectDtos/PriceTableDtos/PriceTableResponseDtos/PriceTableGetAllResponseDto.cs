namespace LabirentFethiye.Common.Dtos.ProjectDtos.PriceTableDtos.PriceTableResponseDtos
{
    public class PriceTableGetAllResponseDto
    {
        public Guid Id { get; set; }
        public string Icon { get; set; }
        public decimal Price { get; set; }
        public List<PriceTableGetAllResponseDtoPriceTableDetail>? PriceTableDetails { get; set; }

        public Guid? VillaId { get; set; }
        public Guid? RoomId { get; set; }
    }

    public class PriceTableGetAllResponseDtoPriceTableDetail
    {
        public Guid Id { get; set; }
        public string LanguageCode { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
