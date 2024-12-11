using LabirentFethiye.Common.Enums;
using LabirentFethiye.Domain.Entities.GlobalEntities.BaseEntities;

namespace LabirentFethiye.Domain.Entities.ProjectEntities
{
    public class Room : WebSiteBaseEntity
    {
        public UInt16 Rooms { get; set; }
        public UInt16 Person { get; set; }
        public UInt16 Bath { get; set; }

        public bool OnlineReservation { get; set; }

        public string WaterMaterNumber { get; set; }
        public string ElectricityMeterNumber { get; set; }
        public string InternetMeterNumber { get; set; }
        public string WifiPassword { get; set; }

        public int Line { get; set; }
        public PriceType PriceType { get; set; }

        public ICollection<RoomDetail> RoomDetails { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public ICollection<PriceTable> PriceTables { get; set; }        
        public ICollection<Payment> Payments { get; set; }
        public ICollection<Feature> Features { get; set; }
        public ICollection<PriceDate> PriceDates { get; set; }
        public ICollection<Reservation> Reservations { get; set; }


        public Guid HotelId { get; set; }
        public Hotel Hotel { get; set; }
    }
}
