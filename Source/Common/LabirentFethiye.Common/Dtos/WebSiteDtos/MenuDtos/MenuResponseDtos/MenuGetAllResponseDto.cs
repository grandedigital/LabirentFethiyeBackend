using LabirentFethiye.Common.Enums;

namespace LabirentFethiye.Common.Dtos.WebSiteDtos.MenuDtos.MenuResponseDtos
{
    public class MenuGetAllResponseDto
    {
        public Guid Id { get; set; }
        public string Slug { get; set; }
        public PageType PageType { get; set; }
        public virtual List<MenuGetAllResponseDtoMenuDetail> MenuDetails { get; set; }

    }

    public class MenuGetAllResponseDtoMenuDetail
    {
        public string LanguageCode { get; set; }
        public string Name { get; set; }
    }
}
