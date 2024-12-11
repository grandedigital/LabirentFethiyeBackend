using LabirentFethiye.Domain.Entities.GlobalEntities.BaseEntities;

namespace LabirentFethiye.Domain.Entities.ProjectEntities
{
    public class ReservationItem : BaseEntity
    {
        public DateTime Day { get; set; }
        public decimal Price { get; set; }

        public Guid ReservationId { get; set; }
        public Reservation Reservation { get; set; }
    }
}
