using LabirentFethiye.Common.Requests;

namespace LabirentFethiye.Common.Dtos.ProjectDtos.VillaDtos.VillaRequestDtos
{
    public class VillaGetAllRequestDto
    {
        public VillaGetAllRequestDto()
        {
            Pagination = new Pagination()
            {
                Size = 10,
                Page = 0
            };
        }
        public Pagination? Pagination { get; set; }
        public bool? OrderByName { get; set; }
        public bool? OrderByPerson { get; set; }
        public bool? OrderByOnlineReservation { get; set; }
        public string? SearchName { get; set; }
    }
}
