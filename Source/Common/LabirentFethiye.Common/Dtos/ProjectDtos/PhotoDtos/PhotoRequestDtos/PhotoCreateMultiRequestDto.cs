using Microsoft.AspNetCore.Http;

namespace LabirentFethiye.Common.Dtos.ProjectDtos.PhotoDtos.PhotoRequestDtos
{
    public class PhotoCreateMultiRequestDto
    {        
        public Guid? VillaId { get; set; }
        public Guid? HotelId { get; set; }
        public Guid? RoomId { get; set; }

        public List<IFormFile> FormFiles { get; set; }
    }
}
