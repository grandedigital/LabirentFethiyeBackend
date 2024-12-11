using LabirentFethiye.Domain.Entities.GlobalEntities.BaseEntities;

namespace LabirentFethiye.Domain.Entities.ProjectEntities
{
    public class DistanceRuler:BaseEntity
    {
        public string Icon { get; set; }
        public int Line { get; set; }
        public string Value { get; set; }

        public Guid? VillaId { get; set; }
        public Villa Villa { get; set; }
        public Guid? HotelId { get; set; }
        public Hotel Hotel { get; set; }
        public ICollection<DistanceRulerDetail> DistanceRulerDetails { get; set; }      
    }
}
