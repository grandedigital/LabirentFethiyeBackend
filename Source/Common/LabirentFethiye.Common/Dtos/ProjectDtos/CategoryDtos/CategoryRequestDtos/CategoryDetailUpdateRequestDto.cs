using LabirentFethiye.Common.Enums;
namespace LabirentFethiye.Common.Dtos.ProjectDtos.CategoryDtos.CategoryRequestDtos
{
    public class CategoryDetailUpdateRequestDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? DescriptionShort { get; set; }
        public string? DescriptionLong { get; set; }
        public GeneralStatusType GeneralStatusType { get; set; }
    }
}
