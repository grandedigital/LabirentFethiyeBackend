using LabirentFethiye.Common.Enums;

namespace LabirentFethiye.Common.Dtos.ProjectDtos.ClientDtos.ClientResponseDtos
{
    public class ClientRoomGetResponseDto
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public string DescriptionLong { get; set; }
        public UInt16 Rooms { get; set; }
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

        public ICollection<ClientRoomGetResponseDtoPhotos> Photos { get; set; }
    }
    public class ClientRoomGetResponseDtoPhotos
    {
        public string Image { get; set; }
    }
}
