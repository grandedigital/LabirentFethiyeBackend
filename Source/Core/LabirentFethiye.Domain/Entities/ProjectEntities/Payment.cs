using LabirentFethiye.Common.Enums;
using LabirentFethiye.Domain.Entities.GlobalEntities.BaseEntities;

namespace LabirentFethiye.Domain.Entities.ProjectEntities
{
    public class Payment : BaseEntity
    {
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public bool InOrOut { get; set; }
        public PriceType PriceType { get; set; }

        public Guid PaymentTypeId { get; set; }
        public PaymentType PaymentType { get; set; }

        public Guid? VillaId { get; set; }
        public Villa Villa { get; set; }

        public Guid? HotelId { get; set; }
        public Hotel Hotel { get; set; }

        public Guid? RoomId { get; set; }
        public Room Room { get; set; }

        public Guid? ReservationId { get; set; }
        public Reservation? Reservation { get; set; }

    }
}
