using LabirentFethiye.Domain.Entities.GlobalEntities.BaseEntities;
using LabirentFethiye.Domain.Entities.ProjectEntities;

namespace LabirentFethiye.Domain.Entities.GlobalEntities
{
    public class Town : BaseEntity
    {
        public string Name { get; set; }
        public Guid DistrictId { get; set; }
        public District District { get; set; }
        public ICollection<Villa> Villas { get; set; }
        public ICollection<Hotel> Hotels { get; set; }


    }
}
