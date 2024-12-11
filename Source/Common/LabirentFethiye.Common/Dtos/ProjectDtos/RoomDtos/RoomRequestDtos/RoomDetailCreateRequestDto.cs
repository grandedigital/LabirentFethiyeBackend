namespace LabirentFethiye.Common.Dtos.ProjectDtos.RoomDtos.RoomRequestDtos
{
    public class RoomDetailCreateRequestDto
    {
        public string LanguageCode { get; set; }
        public string Name { get; set; }
        public string? DescriptionShort { get; set; }
        public string? DescriptionLong { get; set; }
        public string? FeatureTextBlue { get; set; }
        public string? FeatureTextRed { get; set; }
        public string? FeatureTextWhite { get; set; }
        public Guid RoomId { get; set; }
    }
}
