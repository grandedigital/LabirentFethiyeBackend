using LabirentFethiye.Common.Enums;

namespace LabirentFethiye.Common.Dtos.WebSiteDtos.MenuDtos.MenuRequestDtos
{
    public class MenuUpdateRequestDto
    {
        public Guid Id { get; set; }
        public PageType PageType { get; set; }
        public string? MetaTitle { get; set; }
        public string? Slug { get; set; }
        public string? MetaDescription { get; set; }
        public GeneralStatusType GeneralStatusType { get; set; }
    }
}
