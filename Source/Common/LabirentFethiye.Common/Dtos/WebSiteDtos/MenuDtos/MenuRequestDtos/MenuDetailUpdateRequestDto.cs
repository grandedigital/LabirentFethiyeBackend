using LabirentFethiye.Common.Enums;

namespace LabirentFethiye.Common.Dtos.WebSiteDtos.MenuDtos.MenuRequestDtos
{
    public class MenuDetailUpdateRequestDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public GeneralStatusType GeneralStatusType { get; set; }
    }
}
