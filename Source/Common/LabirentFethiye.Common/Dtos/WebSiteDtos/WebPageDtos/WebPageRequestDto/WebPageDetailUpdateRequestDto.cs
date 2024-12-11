using LabirentFethiye.Common.Enums;

namespace LabirentFethiye.Common.Dtos.WebSiteDtos.WebPageDtos.WebPageRequestDto
{
    public class WebPageDetailUpdateRequestDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? DescriptionShort { get; set; }
        public string? DescriptionLong { get; set; }
        public GeneralStatusType GeneralStatusType { get; set; }
    }
}
