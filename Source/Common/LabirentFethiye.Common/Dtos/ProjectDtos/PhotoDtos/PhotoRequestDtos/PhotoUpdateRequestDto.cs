using LabirentFethiye.Common.Enums;

namespace LabirentFethiye.Common.Dtos.ProjectDtos.PhotoDtos.PhotoRequestDtos
{
    public class PhotoUpdateRequestDto
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? ImgAlt { get; set; }
        public string? ImgTitle { get; set; }
        public string? VideoLink { get; set; }
        public int Line { get; set; }
        public GeneralStatusType GeneralStatusType { get; set; }
    }
}
