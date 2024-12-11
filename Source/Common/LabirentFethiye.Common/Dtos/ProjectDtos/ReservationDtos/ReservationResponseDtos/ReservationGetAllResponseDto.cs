using LabirentFethiye.Common.Enums;

namespace LabirentFethiye.Common.Dtos.ProjectDtos.ReservationDtos.ReservationResponseDtos
{
    public class ReservationGetAllResponseDto
    {
        public Guid Id { get; set; }
        public string ReservationNumber { get; set; }
        public ReservationChannalType ReservationChannalType { get; set; }
        public ReservationStatusType ReservationStatusType { get; set; }

        public string Note { get; set; }

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

        public virtual List<ReservationGetAllResponseDtoReservationInfo> ReservationInfos { get; set; }
        public virtual List<ReservationGetAllResponseDtoReservationItem> ReservationItems { get; set; }

        public Guid? VillaId { get; set; }
        public virtual ReservationGetAllResponseDtoVilla Villa { get; set; }

        public Guid? RoomId { get; set; }
        public virtual ReservationGetAllResponseDtoRoom Room { get; set; }
    }
    public class ReservationGetAllResponseDtoReservationInfo
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
    public class ReservationGetAllResponseDtoReservationItem
    {
        public DateTime Day { get; set; }
        public Decimal Price { get; set; }

    }
    public class ReservationGetAllResponseDtoVilla
    {
        public string Name { get; set; }

    }
    public class ReservationGetAllResponseDtoRoom
    {
        public string Name { get; set; }

    }
}
