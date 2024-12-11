using LabirentFethiye.Domain.Entities.GlobalEntities.BaseEntities;

namespace LabirentFethiye.Domain.Entities.GlobalEntities
{
    public class District : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Town> Towns { get; set; }
        public Guid CityId { get; set; }
        public City City { get; set; }

    }
}
