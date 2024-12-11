namespace LabirentFethiye.Common.Dtos.ProjectDtos.DistanceRulerDtos.DistanceRulerRequestDtos
{
    public class DistanceRulerCreateRequestDto
    {
        public string? Icon { get; set; }
        public Guid? VillaId { get; set; }
        public Guid? HotelId { get; set; }

        public string LanguageCode { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
