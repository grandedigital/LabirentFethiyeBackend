using LabirentFethiye.Common.Enums;

namespace LabirentFethiye.Common.Dtos.ProjectDtos.ClientDtos.ClientResponseDtos
{
    public class ClientHotelGetResponseDto
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public string DescriptionLong { get; set; }
        public UInt16 Room { get; set; }
        public UInt16 Person { get; set; }
        public UInt16 Bath { get; set; }
        public string VillaNumber { get; set; }
        public PriceType PriceType { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }

        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }

        public string Town { get; set; }
        public string District { get; set; }

        public List<ClientHotelGetResponseDtoComment> Comments { get; set; }
        public List<ClientHotelGetResponseDtoRoom> Rooms { get; set; }
        public ICollection<ClientHotelGetResponseDtoPhotos> Photos { get; set; }
    }
    public class ClientHotelGetResponseDtoComment
    {
        public string Title { get; set; }
        public string CommentText { get; set; }
        public decimal Rating { get; set; }
        public string Video { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
    }
    public class ClientHotelGetResponseDtoPhotos
    {
        public string Image { get; set; }
    }
    public class ClientHotelGetResponseDtoRoom
    {
        public UInt16 Rooms { get; set; }
        public UInt16 Person { get; set; }
        public UInt16 Bath { get; set; }
        public PriceType PriceType { get; set; }
        public bool OnlineReservation { get; set; }
        public string Slug { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public string Name { get; set; }
        public string FeatureTextWhite { get; set; }

        public List<ClientHotelGetResponseDtoRoomPhotos> Photos { get; set; }
    }
    public class ClientHotelGetResponseDtoRoomPhotos
    {
        public string Image { get; set; }
    }

}
