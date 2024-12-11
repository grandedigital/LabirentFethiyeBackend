namespace LabirentFethiye.Common.Dtos.ProjectDtos.CategoryDtos.CategoryRequestDtos
{
    public class CategoryDetailCreateRequestDto
    {
        public Guid CategoryId { get; set; }
        public string LanguageCode { get; set; }
        public string Name { get; set; }
        public string? DescriptionShort { get; set; }
        public string? DescriptionLong { get; set; }
    }
}
