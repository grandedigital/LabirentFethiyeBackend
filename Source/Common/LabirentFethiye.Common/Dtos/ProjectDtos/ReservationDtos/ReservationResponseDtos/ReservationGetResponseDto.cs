using LabirentFethiye.Common.Enums;

namespace LabirentFethiye.Common.Dtos.ProjectDtos.ReservationDtos.ReservationResponseDtos
{
    public class ReservationGetResponseDto
    {
        public Guid Id { get; set; }
        public string ReservationNumber { get; set; }
        public ReservationChannalType ReservationChannalType { get; set; }
        public ReservationStatusType ReservationStatusType { get; set; }

        public string Note { get; set; }
        public string Description { get; set; }

        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }

        public bool IsDepositPrice { get; set; }
        public bool IsCleaningPrice { get; set; }
        public bool HomeOwner { get; set; }

        public decimal Amount { get; set; }
        public decimal ExtraPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }

        public PriceType PriceType { get; set; }

        public virtual List<ReservationGetResponseDtoReservationInfo> ReservationInfos { get; set; }
        public virtual List<ReservationGetResponseDtoReservationItem> ReservationItems { get; set; }
        public virtual List<ReservationGetResponseDtoPayments> Payments { get; set; }

        public Guid? VillaId { get; set; }
        public virtual ReservationGetResponseDtoVilla Villa { get; set; }

        public Guid? RoomId { get; set; }
        public virtual ReservationGetResponseDtoRoom Room { get; set; }
    }
    public class ReservationGetResponseDtoPayments
    {
        public decimal Amount { get; set; }
        public bool InOrOut { get; set; }
        public PriceType PriceType{ get; set; }
    }
    public class ReservationGetResponseDtoReservationInfo
    {
        public string IdNo { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool Owner { get; set; }
        public PeopleType PeopleType { get; set; }
    }
    public class ReservationGetResponseDtoReservationItem
    {
        public DateTime Day { get; set; }
        public Decimal Price { get; set; }
    }
    public class ReservationGetResponseDtoVilla
    {
        public string Name { get; set; }
    }
    public class ReservationGetResponseDtoRoom
    {
        public string Name { get; set; }
    }
}
