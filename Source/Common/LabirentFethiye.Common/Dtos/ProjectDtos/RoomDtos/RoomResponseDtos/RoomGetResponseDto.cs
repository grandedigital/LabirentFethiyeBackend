using LabirentFethiye.Common.Enums;

namespace LabirentFethiye.Common.Dtos.ProjectDtos.RoomDtos.RoomResponseDtos
{

    public class RoomGetResponseDto
    {
        public Guid Id { get; set; }
        public UInt16 Rooms { get; set; }
        public UInt16 Person { get; set; }
        public UInt16 Bath { get; set; }

        public bool OnlineReservation { get; set; }

        public string WaterMaterNumber { get; set; }
        public string ElectricityMeterNumber { get; set; }
        public string InternetMeterNumber { get; set; }
        public string WifiPassword { get; set; }

        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string Slug { get; set; }
        public PriceType PriceType { get; set; }

        public List<RoomGetResponseDtoRoomDetail>? RoomDetails { get; set; }
        public virtual List<RoomGetResponseDtoPhotos> Photos { get; set; }
        //public virtual List<PriceTable>? PriceTables { get; set; }
        //public virtual List<DistanceRuler>? DistanceRulers { get; set; }
        public List<RoomGetResponseDtoPayments> Payments { get; set; }

        public RoomGetResponseDtoHotel Hotel { get; set; }

        public GeneralStatusType GeneralStatusType { get; set; }
    }
    public class RoomGetResponseDtoPayments
    {
        public decimal Amount { get; set; }
        public bool InOrOut { get; set; }
    }
    public class RoomGetResponseDtoPhotos
    {
        public string Title { get; set; }
        public string ImgAlt { get; set; }
        public string ImgTitle { get; set; }
        public string Image { get; set; }
        public string VideoLink { get; set; }
        public int Line { get; set; }
    }
    public class RoomGetResponseDtoRoomDetail
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

    public class RoomGetResponseDtoHotel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public PriceType PriceType { get; set; }
        public List<RoomGetResponseDtoHotelDetail>? HotelDetails { get; set; }

    }

    public class RoomGetResponseDtoHotelDetail
    {
        public Guid Id { get; set; }
        public string LanguageCode { get; set; }
        public string Name { get; set; }
        public string DescriptionShort { get; set; }
        public string DescriptionLong { get; set; }
        public GeneralStatusType GeneralStatusType { get; set; }
    }
}
