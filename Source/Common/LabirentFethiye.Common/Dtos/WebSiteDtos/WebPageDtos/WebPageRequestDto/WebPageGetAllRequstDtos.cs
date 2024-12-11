using LabirentFethiye.Common.Requests;

namespace LabirentFethiye.Common.Dtos.WebSiteDtos.WebPageDtos.WebPageRequestDto
{
    public class WebPageGetAllRequstDtos
    {
        public WebPageGetAllRequstDtos()
        {
            Pagination = new Pagination()
            {
                Size = 10,
                Page = 0
            };
        }

        public Pagination? Pagination { get; set; }
        public string? slug { get; set; }
    }
}
