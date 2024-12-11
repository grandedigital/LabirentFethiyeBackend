namespace LabirentFethiye.Common.Dtos.WebSiteDtos.WebPageDtos.WebPageRequestDto
{
    public class WebPageCreateRequestDto
    {
        public Guid MenuId { get; set; }
        public string? MetaTitle { get; set; }
        public string? MetaDescription { get; set; }
        public string LanguageCode { get; set; }
        public string Title { get; set; }
        public string? DescriptionShort { get; set; }
        public string? DescriptionLong { get; set; }
    }
}
