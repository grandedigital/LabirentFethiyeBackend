using LabirentFethiye.Common.Enums;
using LabirentFethiye.Domain.Entities.GlobalEntities.BaseEntities;

namespace LabirentFethiye.Domain.Entities.ProjectEntities
{
    public class ReservationInfo:BaseEntity
    {
        public string IdNo { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public bool Owner { get; set; }
        public PeopleType PeopleType { get; set; }

        public Guid ReservationId { get; set; }
        public Reservation Reservation { get; set; }
    }
}
