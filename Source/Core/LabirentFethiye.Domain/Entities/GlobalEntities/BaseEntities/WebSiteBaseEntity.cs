namespace LabirentFethiye.Domain.Entities.GlobalEntities.BaseEntities
{
    public class WebSiteBaseEntity : BaseEntity
    {
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string Slug { get; set; }
    }
}
