using LabirentFethiye.Common.Enums;
using LabirentFethiye.Domain.Entities.GlobalEntities.BaseEntities;

namespace LabirentFethiye.Domain.Entities.ProjectEntities
{
    public class Reservation : BaseEntity
    {
        public string ReservationNumber { get; set; }
        public string Note { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }

        public ReservationChannalType ReservationChannalType { get; set; }
        public ReservationStatusType ReservationStatusType { get; set; }

        public bool IsDepositPrice { get; set; }
        public bool IsCleaningPrice { get; set; }
        public bool HomeOwner { get; set; }

        public PriceType PriceType { get; set; }
        public decimal Amount { get; set; }
        public decimal ExtraPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }

        //-----
        public Guid? VillaId { get; set; }
        public Villa Villa { get; set; }

        public Guid? RoomId { get; set; }
        public Room Room { get; set; }

        public ICollection<ReservationInfo> ReservationInfos { get; set; }
        public ICollection<ReservationItem> ReservationItems { get; set; }
        public ICollection<Payment> Payments { get; set; }

    }
}
