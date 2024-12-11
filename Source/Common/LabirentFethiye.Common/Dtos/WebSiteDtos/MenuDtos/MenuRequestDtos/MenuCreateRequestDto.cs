using LabirentFethiye.Common.Enums;

namespace LabirentFethiye.Common.Dtos.WebSiteDtos.MenuDtos.MenuRequestDtos
{
    public class MenuCreateRequestDto
    {
        public PageType PageType { get; set; }
        public string LanguageCode { get; set; }
        public string Name { get; set; }
        public string? MetaTitle { get; set; }
        public string? MetaDescription { get; set; }
    }
}
