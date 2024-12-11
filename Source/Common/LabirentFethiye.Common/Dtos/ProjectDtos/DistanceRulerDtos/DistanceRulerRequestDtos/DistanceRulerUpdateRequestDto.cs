using LabirentFethiye.Common.Enums;

namespace LabirentFethiye.Common.Dtos.ProjectDtos.DistanceRulerDtos.DistanceRulerRequestDtos
{
    public class DistanceRulerUpdateRequestDto
    {
        public Guid Id { get; set; }
        public string? Icon { get; set; }
        public string? Value { get; set; }

        public GeneralStatusType GeneralStatusType { get; set; }
    }
}
