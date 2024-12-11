namespace LabirentFethiye.Domain.Entities.ProjectEntities
{
    public class VillaFeature
    {
        public Guid VillaId { get; set; }
        public Villa Villa { get; set; }
        public Guid FeatureId { get; set; }
        public Feature Feature { get; set; }
    }
}
