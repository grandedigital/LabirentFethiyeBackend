using LabirentFethiye.Domain.Entities.GlobalEntities.BaseEntities;

namespace LabirentFethiye.Domain.Entities.WebSiteEntities
{
    public class WebPageDetail : BaseEntity
    {
        public string LanguageCode { get; set; }
        public string Title { get; set; }
        public string? DescriptionShort { get; set; }
        public string? DescriptionLong { get; set; }

        public Guid WebPageId { get; set; }
        public virtual WebPage? WebPage { get; set; }
    }
}
