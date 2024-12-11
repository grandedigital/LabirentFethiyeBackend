using LabirentFethiye.Common.Enums;
using LabirentFethiye.Domain.Entities.GlobalEntities;
using LabirentFethiye.Domain.Entities.GlobalEntities.BaseEntities;
using LabirentFethiye.Domain.Entities.GlobalEntities.IdentityEntities;

namespace LabirentFethiye.Domain.Entities.ProjectEntities
{
    public class Villa : WebSiteBaseEntity
    {
        public string VillaNumber { get; set; }
        public UInt16 Room { get; set; }
        public UInt16 Person { get; set; }
        public UInt16 Bath { get; set; }
        public string GoogleMap { get; set; }
        public PriceType PriceType { get; set; }

        public string WaterMaterNumber { get; set; }
        public string ElectricityMeterNumber { get; set; }
        public string InternetMeterNumber { get; set; }
        public string WifiPassword { get; set; }
        public string VillaOwnerName { get; set; }
        public string VillaOwnerPhone { get; set; }

        public bool OnlineReservation { get; set; }
        public bool IsRent { get; set; }
        public bool IsSale { get; set; }
        public int Line { get; set; }
        public int MinimumReservationNight { get; set; }

        public Guid TownId { get; set; }
        public Town Town { get; set; }

        public ICollection<VillaDetail> VillaDetails { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public ICollection<PriceTable> PriceTables { get; set; }
        public ICollection<DistanceRuler> DistanceRulers { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Payment> Payments { get; set; }
        public ICollection<PriceDate> PriceDates { get; set; }
        public ICollection<VillaFeature> VillaFeatures { get; set; }
        public ICollection<VillaCategory> VillaCategories { get; set; }
        public ICollection<Reservation> Reservations { get; set; }

        public Guid? PersonalId{ get; set; }
        public AppUser? Personal { get; set; }

    }
}
