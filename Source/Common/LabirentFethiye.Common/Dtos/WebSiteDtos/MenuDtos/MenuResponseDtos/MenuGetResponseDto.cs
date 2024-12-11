using LabirentFethiye.Common.Enums;

namespace LabirentFethiye.Common.Dtos.WebSiteDtos.MenuDtos.MenuResponseDtos
{
    public class MenuGetResponseDto
    {
        public Guid Id { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string Slug { get; set; }
        public PageType PageType { get; set; }
        public virtual List<MenuGetResponseDtoMenuDetail> MenuDetails { get; set; }

    }
    public class MenuGetResponseDtoMenuDetail
    {
        public Guid Id { get; set; }
        public string LanguageCode { get; set; }
        public string Name { get; set; }
    }
}
