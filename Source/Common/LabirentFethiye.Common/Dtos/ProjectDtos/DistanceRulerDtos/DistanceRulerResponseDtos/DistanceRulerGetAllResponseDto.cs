namespace LabirentFethiye.Common.Dtos.ProjectDtos.DistanceRulerDtos.DistanceRulerResponseDtos
{
    public class DistanceRulerGetAllResponseDto
    {
        public Guid Id { get; set; }
        public string Icon { get; set; }
        public string Value { get; set; }

        public virtual List<DistanceRulerGetAllResponseDtoDistanceRulerDetail>? DistanceRulerDetails { get; set; }
    }

    public class DistanceRulerGetAllResponseDtoDistanceRulerDetail
    {
        public Guid Id { get; set; }
        public string LanguageCode { get; set; }
        public string Name { get; set; }
    }
}
