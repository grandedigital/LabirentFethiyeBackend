namespace LabirentFethiye.Common.Dtos.WebSiteDtos.WebPageDtos.WebPageResponseDto
{
    public class WebPageGetResponseDto
    {
        public Guid Id { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public List<WebPageGetResponseDtoWebPageDetail> WebPageDetails { get; set; }
        public List<WebPageGetResponseDtoWebPhoto> Photos { get; set; }

    }
    public class WebPageGetResponseDtoWebPageDetail
    {
        public string LanguageCode { get; set; }
        public string Title { get; set; }
        public string? DescriptionShort { get; set; }
        public string? DescriptionLong { get; set; }
    }
    public class WebPageGetResponseDtoWebPhoto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string ImgAlt { get; set; }
        public string ImgTitle { get; set; }
        public string Image { get; set; }
        public string VideoLink { get; set; }
        public int Line { get; set; }
    }
}
