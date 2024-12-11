namespace LabirentFethiye.Common.Dtos.ProjectDtos.ClientDtos.ClientResponseDtos
{
    public class ClientWebPageGetResponseDto
    {
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public List<ClientWebPageGetResponseDtoWebPageDetail>? WebPageDetails { get; set; }
        public List<ClientWebPageGetResponseDtoWebPagePhoto>? Photos { get; set; }
    }
    public class ClientWebPageGetResponseDtoWebPageDetail
    {
        public string Title { get; set; }
        public string DescriptionShort { get; set; }
        public string DescriptionLong { get; set; }
    }

    public class ClientWebPageGetResponseDtoWebPagePhoto
    {
        public string Title { get; set; }
        public string ImgAlt { get; set; }
        public string ImgTitle { get; set; }
        public string Image { get; set; }
        public string VideoLink { get; set; }
    }
}
