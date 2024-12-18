using LabirentFethiye.Common.Requests;

namespace LabirentFethiye.Common.Dtos.ProjectDtos.ClientDtos.ClientRequestDtos
{
    public class ClientGetAllWebPageRequestDto
    {
        public ClientGetAllWebPageRequestDto()
        {
            Pagination = new Pagination()
            {
                Size = 10,
                Page = 0
            };
        }
        public Pagination Pagination { get; set; }
        public string Language { get; set; }
        public string Slug { get; set; }
    }
}
