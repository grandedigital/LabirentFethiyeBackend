using LabirentFethiye.Common.Enums;

namespace LabirentFethiye.Common.Dtos.ProjectDtos.ReservationDtos.ReservationRequestDtos
{
    public class ReservationCreateRequestDto
    {
        public ReservationChannalType ReservationChannalType { get; set; }
        public ReservationStatusType ReservationStatusType { get; set; }

        public string? Note { get; set; }
        public string? Description { get; set; }

        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }

        public bool IsDepositPrice { get; set; }
        public bool IsCleaningPrice { get; set; }
        public bool HomeOwner { get; set; }

        //public decimal Amount { get; set; }
        //public decimal ExtraPrice { get; set; }
        public decimal Discount { get; set; }
        //public decimal Total { get; set; }

        //public PriceType PriceType { get; set; }

        public Guid? VillaId { get; set; }
        public Guid? RoomId { get; set; }

        public string? IdNo { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
    }
}
