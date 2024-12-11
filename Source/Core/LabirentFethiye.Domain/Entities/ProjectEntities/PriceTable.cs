using LabirentFethiye.Domain.Entities.GlobalEntities.BaseEntities;

namespace LabirentFethiye.Domain.Entities.ProjectEntities
{
    public class PriceTable : BaseEntity
    {
        public decimal Price { get; set; }
        public string Icon { get; set; }
        public int Line { get; set; }

        public Guid? VillaId { get; set; }
        public Villa Villa { get; set; }
        public Guid? RoomId { get; set; }
        public Room Room { get; set; }
        public ICollection<PriceTableDetail> PriceTableDetails { get; set; }

    }
}
