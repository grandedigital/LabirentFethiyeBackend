using LabirentFethiye.Common.Requests;

namespace LabirentFethiye.Common.Dtos.ProjectDtos.ReservationDtos.ReservationRequestDtos
{
    public class GetAllReservationRequestDto
    {
        public GetAllReservationRequestDto()
        {
            Pagination = new Pagination()
            {
                Size = 10,
                Page = 0
            };
        }

        public Guid? VillaId { get; set; }
        public Guid? RoomId { get; set; }

        public Pagination? Pagination { get; set; }
        public bool? OrderByCustomerName { get; set; }
        public bool? OrderByReservationStatus { get; set; }
        public bool? OrderByCheckIn { get; set; }
        public bool? OrderByCheckOut { get; set; }
        public bool? OrderByPrice { get; set; }

        public string? CustomerSearchName { get; set; }

        public bool HomeOwner { get; set; }
        public bool AgencyOwner { get; set; }
    }
}
