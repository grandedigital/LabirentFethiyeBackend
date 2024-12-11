using LabirentFethiye.Common.Enums;

namespace LabirentFethiye.Common.Dtos.ProjectDtos.ReservationInfoDtos.ReservationInfoRequestDtos
{
    public class ReservationInfoCreateRequestDto
    {
        public string? IdNo { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }

        public bool Owner { get; set; }
        public PeopleType PeopleType { get; set; }

        public Guid ReservationId { get; set; }
    }
}
