namespace LabirentFethiye.Common.Dtos.ProjectDtos.VillaDtos.VillaRequestDtos
{
    public class VillaCategoryAsignRequestDto
    {
        public Guid VillaId { get; set; }
        public List<Guid> CategoryIds { get; set; }
    }
}
