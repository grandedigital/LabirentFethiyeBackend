using LabirentFethiye.Domain.Entities.GlobalEntities.BaseEntities;

namespace LabirentFethiye.Domain.Entities.ProjectEntities
{
    public class Feature : BaseEntity
    {
        public string Icon { get; set; }
        public int Line { get; set; }
        public Guid ParentId { get; set; }
        public Feature Parent { get; set; }
        public ICollection<Feature> Children { get; set; }
        public ICollection<FeatureDetail> FeatureDetails { get; set; }
        public ICollection<VillaFeature> VillaFeatures { get; set; }
    }
}
