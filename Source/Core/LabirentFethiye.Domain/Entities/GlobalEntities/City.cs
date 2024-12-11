using LabirentFethiye.Domain.Entities.GlobalEntities.BaseEntities;

namespace LabirentFethiye.Domain.Entities.GlobalEntities
{
    public class City : BaseEntity
    {
        public int CityNumber { get; set; }
        public string Name { get; set; }
        public ICollection<District> Districts { get; set; }
    }
}
