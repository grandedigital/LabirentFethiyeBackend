using LabirentFethiye.Common.Enums;
using LabirentFethiye.Domain.Entities.GlobalEntities.BaseEntities;

namespace LabirentFethiye.Domain.Entities.WebSiteEntities
{
    public class Menu : WebSiteBaseEntity
    {
        public PageType PageType { get; set; }
        public ICollection<WebPage>? WebPageDetails { get; set; }
        public ICollection<MenuDetail>? MenuDetails { get; set; }
    }
}
