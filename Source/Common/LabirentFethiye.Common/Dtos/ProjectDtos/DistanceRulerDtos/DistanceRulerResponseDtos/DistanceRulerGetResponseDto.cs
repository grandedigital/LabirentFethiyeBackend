namespace LabirentFethiye.Common.Dtos.ProjectDtos.DistanceRulerDtos.DistanceRulerResponseDtos
{
    public class DistanceRulerGetResponseDto
    {
        public Guid Id { get; set; }
        public string Icon { get; set; }
        public string Value { get; set; }

        public List<DistanceRulerGetResponseDtoDistanceRulerDetail>? DistanceRulerDetails { get; set; }      
    }

    public class DistanceRulerGetResponseDtoDistanceRulerDetail
    {
        public Guid Id { get; set; }
        public string LanguageCode { get; set; }
        public string Name { get; set; }
    }
}
