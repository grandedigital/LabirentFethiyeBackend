using LabirentFethiye.Common.Dtos.GlobalDtos.DistrictDtos.DistrictResponseDtos;

namespace LabirentFethiye.Common.Dtos.TownDtos.TownResponseDtos
{
    public class TownGetResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid DistrictId { get; set; }
        public DistrictGetResponseDto District { get; set; }
    }

}

