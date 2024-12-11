using LabirentFethiye.Domain.Entities.GlobalEntities.BaseEntities;

namespace LabirentFethiye.Domain.Entities.WebSiteEntities
{
    public class MenuDetail : BaseEntity
    {
        public string LanguageCode { get; set; }
        public string Name { get; set; }

        public Guid MenuId { get; set; }
        public virtual Menu Menu { get; set; }
    }
}
