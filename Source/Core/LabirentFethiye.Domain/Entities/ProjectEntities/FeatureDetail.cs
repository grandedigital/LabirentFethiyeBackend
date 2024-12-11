using LabirentFethiye.Domain.Entities.GlobalEntities.BaseEntities;

namespace LabirentFethiye.Domain.Entities.ProjectEntities
{
    public class FeatureDetail : BaseEntity
    {
        public string LanguageCode { get; set; }
        public string Name { get; set; }

        public Guid FeatureId { get; set; }
        public Feature Feature { get; set; }
    }
}
