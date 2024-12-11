using LabirentFethiye.Domain.Entities.GlobalEntities.BaseEntities;

namespace LabirentFethiye.Domain.Entities.ProjectEntities
{
    public class PriceDate : BaseEntity
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }

        public Guid? VillaId { get; set; }
        public Villa Villa { get; set; }
        public Guid? RoomId { get; set; }
        public Room Room { get; set; }
    }
}
