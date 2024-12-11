using LabirentFethiye.Common.Enums;

namespace LabirentFethiye.Common.Dtos.ProjectDtos.HotelDtos.HotelRequestDtos
{
    public class HotelUpdateRequestDto
    {
        public Guid Id { get; set; }
        public UInt16 Room { get; set; }
        public UInt16 Person { get; set; }
        public UInt16 Bath { get; set; }
        public bool OnlineReservation { get; set; }
        public string? GoogleMap { get; set; }
        public Guid? TownId { get; set; }
        public string? MetaTitle { get; set; }
        public string? MetaDescription { get; set; }
        public string? Slug { get; set; }
        public string? WaterMaterNumber { get; set; }
        public string? ElectricityMeterNumber { get; set; }
        public string? InternetMeterNumber { get; set; }
        public string? WifiPassword { get; set; }

        public int Line { get; set; }

        public GeneralStatusType GeneralStatusType { get; set; }
        public PriceType PriceType { get; set; }

    }
}
