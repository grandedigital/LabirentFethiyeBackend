using LabirentFethiye.Common.Enums;

namespace LabirentFethiye.Common.Dtos.ProjectDtos.ClientDtos.ClientResponseDtos
{
    public class ClientHotelGetAllResponseDto
    {
        public string Name { get; set; }
        public string FeatureTextWhite { get; set; }
        public string Slug { get; set; }
        public UInt16 Room { get; set; }
        public UInt16 Person { get; set; }
        public UInt16 Bath { get; set; }
        public PriceType PriceType { get; set; }

        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }

        public string Town { get; set; }
        public string District { get; set; }

        public ICollection<ClientHotelGetAllResponseDtoPhotos> Photos { get; set; }
    }
    public class ClientHotelGetAllResponseDtoPhotos
    {
        public string Image { get; set; }
    }
}
