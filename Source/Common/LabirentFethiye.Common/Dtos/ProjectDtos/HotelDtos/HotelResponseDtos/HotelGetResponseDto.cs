using LabirentFethiye.Common.Enums;

namespace LabirentFethiye.Common.Dtos.ProjectDtos.HotelDtos.HotelResponseDtos
{

    public class HotelGetResponseDto
    {
        public Guid Id { get; set; }

        public UInt16 Room { get; set; }
        public UInt16 Person { get; set; }
        public UInt16 Bath { get; set; }
        public string? GoogleMap { get; set; }

        public bool OnlineReservation { get; set; }

        public string WaterMaterNumber { get; set; }
        public string ElectricityMeterNumber { get; set; }
        public string InternetMeterNumber { get; set; }
        public string WifiPassword { get; set; }

        public PriceType PriceType { get; set; }

        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string Slug { get; set; }

        public List<HotelGetResponseDtoHotelDetail>? HotelDetails { get; set; }
        public List<HotelGetResponseDtoPhoto> Photos { get; set; }

        //public List<PriceTable>? PriceTables { get; set; }
        //public List<DistanceRuler>? DistanceRulers { get; set; }
        public List<HotelGetResponseDtoPayments> Payments { get; set; }

        public Guid? TownId { get; set; }
        // public Town? Town { get; set; }

        public GeneralStatusType GeneralStatusType { get; set; }
    }
    public class HotelGetResponseDtoPayments
    {
        public decimal Amount { get; set; }
        public bool InOrOut { get; set; }
    }
    public class HotelGetResponseDtoHotelDetail
    {
        public Guid Id { get; set; }
        public string LanguageCode { get; set; }
        public string Name { get; set; }
        public string DescriptionShort { get; set; }
        public string DescriptionLong { get; set; }
        public string FeatureTextBlue { get; set; }
        public string FeatureTextRed { get; set; }
        public string FeatureTextWhite { get; set; }
        public GeneralStatusType GeneralStatusType { get; set; }

    }

    public class HotelGetResponseDtoPhoto
    {
        public string Title { get; set; }
        public string ImgAlt { get; set; }
        public string ImgTitle { get; set; }
        public string Image { get; set; }
        public string VideoLink { get; set; }
        public int Line { get; set; }
    }
}
