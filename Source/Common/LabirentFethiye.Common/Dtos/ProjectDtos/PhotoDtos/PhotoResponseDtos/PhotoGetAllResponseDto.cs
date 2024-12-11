namespace LabirentFethiye.Common.Dtos.ProjectDtos.PhotoDtos.PhotoResponseDtos
{
    public class PhotoGetAllResponseDto
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
