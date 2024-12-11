using LabirentFethiye.Domain.Entities.GlobalEntities.BaseEntities;

namespace LabirentFethiye.Domain.Entities.ProjectEntities
{
    public class DistanceRulerDetail:BaseEntity
    {
        public string LanguageCode { get; set; }
        public string Name { get; set; }        

        public Guid DistanceRulerId { get; set; }
        public DistanceRuler DistanceRuler { get; set; }
    }
}
