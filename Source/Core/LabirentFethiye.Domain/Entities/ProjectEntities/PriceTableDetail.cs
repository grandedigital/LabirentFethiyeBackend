using LabirentFethiye.Domain.Entities.GlobalEntities.BaseEntities;

namespace LabirentFethiye.Domain.Entities.ProjectEntities
{
    public class PriceTableDetail:BaseEntity
    {
        public string LanguageCode { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public Guid PriceTableId { get; set; }
        public PriceTable PriceTable { get; set; }
    }
}
