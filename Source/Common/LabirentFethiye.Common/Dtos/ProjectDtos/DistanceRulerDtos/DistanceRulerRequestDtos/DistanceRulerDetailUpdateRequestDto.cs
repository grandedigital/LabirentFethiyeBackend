using LabirentFethiye.Common.Enums;

namespace LabirentFethiye.Common.Dtos.ProjectDtos.DistanceRulerDtos.DistanceRulerRequestDtos
{
    public class DistanceRulerDetailUpdateRequestDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public GeneralStatusType GeneralStatusType { get; set; }
    }
}
