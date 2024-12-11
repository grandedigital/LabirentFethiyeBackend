using LabirentFethiye.Common.Enums;

namespace LabirentFethiye.Common.Dtos.ProjectDtos.VillaDtos.VillaRequestDtos
{
    public class VillaCreateRequestDto
    {
        public UInt16 Room { get; set; }
        public UInt16 Person { get; set; }
        public UInt16 Bath { get; set; }
        public bool OnlineReservation { get; set; }
        public string? GoogleMap { get; set; }
        public Guid? TownId { get; set; }
        public Guid? CompanyId { get; set; }
        public string? MetaTitle { get; set; }
        public string? MetaDescription { get; set; }

        public string? WaterMaterNumber { get; set; }
        public string? ElectricityMeterNumber { get; set; }
        public string? InternetMeterNumber { get; set; }
        public string? WifiPassword { get; set; }

        public string? VillaOwnerName { get; set; }
        public string? VillaOwnerPhone { get; set; }
        public string? VillaNumber { get; set; }

        public string? FeatureTextBlue { get; set; }
        public string? FeatureTextRed { get; set; }
        public string? FeatureTextWhite { get; set; }

        public bool IsRent { get; set; }
        public bool IsSale { get; set; }


        public string LanguageCode { get; set; }
        public string Name { get; set; }
        public string? DescriptionShort { get; set; }
        public string? DescriptionLong { get; set; }
        public Guid? PersonalId { get; set; }

        public PriceType PriceType { get; set; }

    }
}
