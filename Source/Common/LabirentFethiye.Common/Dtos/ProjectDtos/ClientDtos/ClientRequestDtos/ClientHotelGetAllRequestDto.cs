using LabirentFethiye.Common.Requests;

namespace LabirentFethiye.Common.Dtos.ProjectDtos.ClientDtos.ClientRequestDtos
{
    public class ClientHotelGetAllRequestDto
    {
        public ClientHotelGetAllRequestDto()
        {
            Pagination = new Pagination()
            {
                Size = 10,
                Page = 0
            };
        }
        public Pagination? Pagination { get; set; }
        public string Language { get; set; }
    }
}
