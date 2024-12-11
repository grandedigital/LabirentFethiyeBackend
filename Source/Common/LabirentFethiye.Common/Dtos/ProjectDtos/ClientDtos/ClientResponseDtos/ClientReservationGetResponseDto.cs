using LabirentFethiye.Common.Enums;

namespace LabirentFethiye.Common.Dtos.ProjectDtos.ClientDtos.ClientResponseDtos
{
    public class ClientReservationGetResponseDto
    {
        public ReservationStatusType ReservationStatusType { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public decimal Amount { get; set; }
        public decimal ExtraPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
        public string ReservationNumber { get; set; }

        public virtual List<ClientReservationGetResponseDtoReservationInfo>? ReservationInfos { get; set; }
        public virtual List<ClientReservationGetResponseDtoReservationItem>? ReservationItems { get; set; }
        public virtual ClientReservationGetResponseDtoVilla? Villa { get; set; }
        public virtual ClientReservationGetResponseDtoRoom? Room { get; set; }
    }
    public class ClientReservationGetResponseDtoReservationInfo
    {
        public bool Owner { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Surname { get; set; }
    }

    public class ClientReservationGetResponseDtoReservationItem
    {
        public DateTime Day { get; set; }
        public Decimal Price { get; set; }
    }
    public class ClientReservationGetResponseDtoVilla
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Photo { get; set; }
    }

    public class ClientReservationGetResponseDtoRoom
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Photo { get; set; }
    }

}
