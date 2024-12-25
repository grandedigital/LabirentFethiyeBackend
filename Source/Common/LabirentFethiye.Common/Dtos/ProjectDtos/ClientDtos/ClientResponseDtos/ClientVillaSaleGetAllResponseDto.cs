using LabirentFethiye.Common.Enums;

namespace LabirentFethiye.Common.Dtos.ProjectDtos.ClientDtos.ClientResponseDtos
{
    public class ClientVillaSaleGetAllResponseDto
    {
        public string Name { get; set; }
        public string FeatureTextWhite { get; set; }
        public string DescriptionShort { get; set; }
        public string Slug { get; set; }
        public UInt16 Room { get; set; }
        public UInt16 Person { get; set; }
        public UInt16 Bath { get; set; }
        public string VillaNumber { get; set; }
        public string Town { get; set; }
        public string District { get; set; }
        public ICollection<ClientVillaSaleGetAllResponseDtoPhotos> Photos { get; set; }
    }

    public class ClientVillaSaleGetAllResponseDtoPhotos
    {
        public string Image { get; set; }
    }
}
