namespace LabirentFethiye.Common.Dtos.ProjectDtos.CategoryDtos.CategoryRequestDtos
{
    public class CategoryCreateRequestDto
    {
        public string? Icon { get; set; }
        public string LanguageCode { get; set; }
        public string Name { get; set; }
        public string? DescriptionShort { get; set; }
        public string? DescriptionLong { get; set; }
        public string? MetaTitle { get; set; }
        public string? MetaDescription { get; set; }
    }
}

