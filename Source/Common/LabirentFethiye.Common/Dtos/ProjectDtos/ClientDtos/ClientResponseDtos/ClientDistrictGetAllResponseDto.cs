namespace LabirentFethiye.Common.Dtos.ProjectDtos.ClientDtos.ClientResponseDtos
{
    public class ClientDistrictGetAllResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<ClientDistrictGetAllResponseDtoTown> Towns { get; set; }

    }
    public class ClientDistrictGetAllResponseDtoTown
    {
        public string Name { get; set; }

    }
}
