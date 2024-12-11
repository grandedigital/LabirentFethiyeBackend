using LabirentFethiye.Domain.Entities.GlobalEntities.BaseEntities;

namespace LabirentFethiye.Domain.Entities.WebSiteEntities
{
    public class WebPhoto : BaseEntity
    {
        public string? Title { get; set; }
        public string? ImgAlt { get; set; }
        public string? ImgTitle { get; set; }
        public string Image { get; set; }
        public string? VideoLink { get; set; }
        public int Line { get; set; }

        public Guid WebPageId { get; set; }
        public virtual WebPage? WebPage { get; set; }
    }
}
