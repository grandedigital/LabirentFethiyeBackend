using LabirentFethiye.Common.Requests;

namespace LabirentFethiye.Common.Dtos.ProjectDtos.HotelDtos.HotelRequestDtos
{
    public class HotelGetAllRequestDto
    {
        public HotelGetAllRequestDto()
        {
            Pagination = new Pagination()
            {
                Size = 10,
                Page = 0
            };
        }
        public Pagination? Pagination { get; set; }
        public bool? OrderByName { get; set; }        
        public string? SearchName { get; set; }
    }
}
