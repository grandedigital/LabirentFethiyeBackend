using LabirentFethiye.Common.Enums;

namespace LabirentFethiye.Common.Dtos.ProjectDtos.CategoryDtos.CategoryRequestDtos
{
    public class CategoryUpdateRequestDto
    {
        public Guid Id { get; set; }
        public string? Icon { get; set; }
        public string? Slug { get; set; }
        public GeneralStatusType GeneralStatusType { get; set; }
        public string? MetaTitle { get; set; }
        public string? MetaDescription { get; set; }
    }   
}
