namespace LabirentFethiye.Common.Dtos.ProjectDtos.ClientDtos.ClientResponseDtos
{
    public class ClienWebPageGetAllResponseDto
    {
        public Guid Id { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string Slug { get; set; }
        public List<ClienWebPageGetAllResponseDtoWebPageDetail> WebPageDetails { get; set; }
        public List<ClienWebPageGetAllResponseDtoWebPagePhoto> Photos { get; set; }
    }

    public class ClienWebPageGetAllResponseDtoWebPageDetail
    {
        public string Title { get; set; }
        public string DescriptionShort { get; set; }
    }

    public class ClienWebPageGetAllResponseDtoWebPagePhoto
    {
        public string Title { get; set; }
        public string ImgAlt { get; set; }
        public string ImgTitle { get; set; }
        public string Image { get; set; }
    }
}
