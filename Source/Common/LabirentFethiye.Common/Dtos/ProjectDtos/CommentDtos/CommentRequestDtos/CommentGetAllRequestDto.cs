using LabirentFethiye.Common.Requests;

namespace LabirentFethiye.Common.Dtos.ProjectDtos.CommentDtos.CommentRequestDtos
{
    public class CommentGetAllRequestDto
    {
        public CommentGetAllRequestDto()
        {
            Pagination = new Pagination()
            {
                Size = 10,
                Page = 0
            };
        }
        public Pagination? Pagination { get; set; }
        public Guid? VillaId { get; set; }
        public Guid? HotelId { get; set; }
    }
}
