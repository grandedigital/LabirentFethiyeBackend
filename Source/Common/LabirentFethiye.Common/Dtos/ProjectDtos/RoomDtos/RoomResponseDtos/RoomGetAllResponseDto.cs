using LabirentFethiye.Common.Enums;

namespace LabirentFethiye.Common.Dtos.ProjectDtos.RoomDtos.RoomResponseDtos
{

    public class RoomGetAllResponseDto
    {
        public Guid Id { get; set; }
        public UInt16 Rooms { get; set; }
        public UInt16 Person { get; set; }
        public UInt16 Bath { get; set; }

        public bool OnlineReservation { get; set; }

        public List<RoomGetAllResponseDtoRoomDetail>? RoomDetails { get; set; }
        //public virtual List<Photo>? Photos { get; set; }
        //public virtual List<PriceTable>? PriceTables { get; set; }
        //public virtual List<DistanceRuler>? DistanceRulers { get; set; }

        public RoomGetAllResponseDtoHotel Hotel { get; set; }

        public GeneralStatusType GeneralStatusType { get; set; }
    }

    public class RoomGetAllResponseDtoRoomDetail
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

    public class RoomGetAllResponseDtoHotel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<RoomGetAllResponseDtoHotelDetail>? HotelDetails { get; set; }

    }

    public class RoomGetAllResponseDtoHotelDetail
    {
        public Guid Id { get; set; }
        public string LanguageCode { get; set; }
        public string Name { get; set; }
        public string DescriptionShort { get; set; }
        public string DescriptionLong { get; set; }
        public GeneralStatusType GeneralStatusType { get; set; }
    }
}
