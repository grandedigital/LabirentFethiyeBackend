using Microsoft.AspNetCore.Http;

namespace LabirentFethiye.Common.Dtos.ProjectDtos.PhotoDtos.PhotoRequestDtos
{
    public class PhotoCreateRequestDto
    {
        public string? Title { get; set; }
        public string? ImgAlt { get; set; }
        public string? ImgTitle { get; set; }
        public string? Image { get; set; }
        public string? VideoLink { get; set; }
        public int Line { get; set; }
        public Guid? VillaId { get; set; }
        public Guid? HotelId { get; set; }
        public Guid? RoomId { get; set; }

        public IFormFile FormFile { get; set; }
    }
}
