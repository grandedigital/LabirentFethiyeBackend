namespace LabirentFethiye.Common.Dtos.WebSiteDtos.WebPageDtos.WebPageResponseDto
{
    public class WebPageGetAllResponseDto
    {
        public Guid Id { get; set; }
        public List<WebPageGetAllResponseDtoWebPageDetail> WebPageDetails { get; set; }
        public List<WebPageGetAllResponseDtoWebPhoto> WebPhotos { get; set; }
        public WebPageGetAllResponseDtoMenu Menu { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
    }
    public class WebPageGetAllResponseDtoWebPageDetail
    {
        public Guid Id { get; set; }
        public string LanguageCode { get; set; }
        public string Title { get; set; }
        public string? DescriptionShort { get; set; }
        public string? DescriptionLong { get; set; }
    }

    public class WebPageGetAllResponseDtoMenu
    {
        public Guid Id { get; set; }
        public List<WebPageGetAllResponseDtoMenuDetail> MenuDetails { get; set; }
    }

    public class WebPageGetAllResponseDtoMenuDetail
    {
        public string Name { get; set; }
        public string LanguageCode { get; set; }
    }

    public class WebPageGetAllResponseDtoWebPhoto
    {
        public string Title { get; set; }
        public string ImgAlt { get; set; }
        public string ImgTitle { get; set; }
        public string Image { get; set; }
    }

}
