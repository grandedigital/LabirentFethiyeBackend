namespace LabirentFethiye.Common.Dtos.WebSiteDtos.WebPageDtos.WebPageRequestDto
{
    public class WebPageDetailCreateRequestDto
    {
        public string LanguageCode { get; set; }
        public string Title { get; set; }
        public string? DescriptionShort { get; set; }
        public string? DescriptionLong { get; set; }

        public Guid WebPageId { get; set; }
    }
}
