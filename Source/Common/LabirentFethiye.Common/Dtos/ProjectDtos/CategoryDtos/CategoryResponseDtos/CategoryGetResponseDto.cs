﻿using LabirentFethiye.Common.Enums;

namespace LabirentFethiye.Common.Dtos.ProjectDtos.CategoryDtos.CategoryResponseDtos
{
    public class CategoryGetResponseDto
    {
        public Guid Id { get; set; }
        public string Icon { get; set; }
        public GeneralStatusType GeneralStatusType { get; set; }
        public ICollection<CategoryGetResponseDtoCategoryDetail> CategoryDetails { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
    }
    public class CategoryGetResponseDtoCategoryDetail
    {
        public Guid Id { get; set; }
        public string LanguageCode { get; set; }
        public string Name { get; set; }
        public string DescriptionShort { get; set; }
        public string DescriptionLong { get; set; }
        public GeneralStatusType GeneralStatusType { get; set; }

    }

}