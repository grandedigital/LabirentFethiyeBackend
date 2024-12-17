using LabirentFethiye.Common.Requests;

namespace LabirentFethiye.Common.Dtos.ProjectDtos.ClientDtos.ClientRequestDtos
{
    public class ClientVillaSearchGetAllRequestDto
    {
        public ClientVillaSearchGetAllRequestDto()
        {
            Pagination = new Pagination()
            {
                Size = 10,
                Page = 0
            };
        }
        public Pagination Pagination { get; set; }
        public string Language { get; set; }
        public string? Name { get; set; }
        public int? Person { get; set; }
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
    }
}
