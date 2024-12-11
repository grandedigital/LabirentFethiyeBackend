using Microsoft.AspNetCore.Http;

namespace LabirentFethiye.Common.Dtos.WebSiteDtos.WebPageDtos.WebPageRequestDto
{
    public class WebPhotoCreateRequestDto
    {
        public string? Title { get; set; }
        public string? ImgAlt { get; set; }
        public string? ImgTitle { get; set; }
        public string? Image { get; set; }
        public string? VideoLink { get; set; }
        public int Line { get; set; }
        public Guid WebPageId { get; set; }

        public IFormFile FormFile { get; set; }
    }

}
