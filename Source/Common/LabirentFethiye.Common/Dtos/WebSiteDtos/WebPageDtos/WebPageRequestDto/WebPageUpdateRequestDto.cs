using LabirentFethiye.Common.Enums;

namespace LabirentFethiye.Common.Dtos.WebSiteDtos.WebPageDtos.WebPageRequestDto
{
    public class WebPageUpdateRequestDto
    {
        public Guid Id { get; set; }
        public string? MetaTitle { get; set; }
        public string? MetaDescription { get; set; }
        public GeneralStatusType GeneralStatusType { get; set; }
    }
}
