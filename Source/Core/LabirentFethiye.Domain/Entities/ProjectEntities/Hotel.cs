using LabirentFethiye.Common.Enums;
using LabirentFethiye.Domain.Entities.GlobalEntities;
using LabirentFethiye.Domain.Entities.GlobalEntities.BaseEntities;

namespace LabirentFethiye.Domain.Entities.ProjectEntities
{
    public class Hotel:WebSiteBaseEntity
    {
        public UInt16 Room { get; set; }
        public UInt16 Person { get; set; }
        public UInt16 Bath { get; set; }

        public string GoogleMap { get; set; }
        public string WaterMaterNumber { get; set; }
        public string ElectricityMeterNumber { get; set; }
        public string InternetMeterNumber { get; set; }
        public string WifiPassword { get; set; }

        public PriceType PriceType { get; set; }
        public int Line { get; set; }

        public Guid TownId { get; set; }
        public Town Town { get; set; }

        public ICollection<Room> Rooms { get; set; }
        public ICollection<HotelDetail> HotelDetails { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<DistanceRuler> DistanceRulers { get; set; }
        public ICollection<Payment> Payments { get; set; }
        public ICollection<Feature> Features { get; set; }

    }
}
