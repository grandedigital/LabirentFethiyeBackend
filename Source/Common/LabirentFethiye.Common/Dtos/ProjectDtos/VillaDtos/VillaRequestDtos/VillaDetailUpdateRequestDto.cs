﻿using LabirentFethiye.Common.Enums;

namespace LabirentFethiye.Common.Dtos.ProjectDtos.VillaDtos.VillaRequestDtos
{
    public class VillaDetailUpdateRequestDto
    {
        public Guid Id { get; set; }
        public GeneralStatusType GeneralStatusType { get; set; }
        public string Name { get; set; }
        public string? DescriptionShort { get; set; }
        public string? DescriptionLong { get; set; }
        public string? FeatureTextBlue { get; set; }
        public string? FeatureTextRed { get; set; }
        public string? FeatureTextWhite { get; set; }
    }
}