using LabirentFethiye.Common.Requests;

namespace LabirentFethiye.Common.Dtos.ProjectDtos.CategoryDtos.CategoryRequestDtos
{
    public class CategoryGetAllRequestDto
    {
        public CategoryGetAllRequestDto()
        {
            Pagination = new Pagination()
            {
                Size = 10,
                Page = 0
            };
        }
        public Pagination? Pagination { get; set; }
        public bool? OrderByName { get; set; }
        public string? SearchName { get; set; }
    }
}
