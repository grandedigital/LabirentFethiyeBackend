using LabirentFethiye.Domain.Entities.GlobalEntities.BaseEntities;

namespace LabirentFethiye.Domain.Entities.WebSiteEntities
{
    public class WebPage : WebSiteBaseEntity
    {
        public virtual List<WebPageDetail>? WebPageDetails { get; set; }
        public virtual List<WebPhoto>? Photos { get; set; }


        public Guid MenuId { get; set; }
        public virtual Menu Menu { get; set; }
    }
}
