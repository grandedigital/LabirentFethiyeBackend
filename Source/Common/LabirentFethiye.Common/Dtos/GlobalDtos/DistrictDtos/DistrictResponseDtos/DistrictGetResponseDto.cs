using LabirentFethiye.Common.Dtos.GlobalDtos.CityDtos.CityResponseDtos;

namespace LabirentFethiye.Common.Dtos.GlobalDtos.DistrictDtos.DistrictResponseDtos
{
    public class DistrictGetResponseDto
    {
        public string Name { get; set; }
        public Guid CityId { get; set; }
        public CityGetResponseDto City { get; set; }
    }
}
