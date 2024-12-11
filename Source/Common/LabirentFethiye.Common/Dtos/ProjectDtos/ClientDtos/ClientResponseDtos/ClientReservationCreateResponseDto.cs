using LabirentFethiye.Common.Enums;

namespace LabirentFethiye.Common.Dtos.ProjectDtos.ClientDtos.ClientResponseDtos
{
    public class ClientReservationCreateResponseDto
    {
        public string ReservationNumber { get; set; }
    }

    //public class ClientReservationCreateResponseDto
    //{
    //    public ReservationStatusType ReservationStatusType { get; set; }
    //    public DateTime CheckIn { get; set; }
    //    public DateTime CheckOut { get; set; }
    //    public decimal Amount { get; set; }
    //    public decimal ExtraPrice { get; set; }
    //    public decimal Discount { get; set; }
    //    public decimal Total { get; set; }
    //    public string ReservationNumber { get; set; }

    //    public virtual List<ClientReservationCreateResponseDtoReservationInfo>? ReservationInfos { get; set; }
    //    public virtual List<ClientReservationCreateResponseDtoReservationItem>? ReservationItems { get; set; }
    //    public virtual ClientReservationCreateResponseDtoVilla? Villa { get; set; }
    //    public virtual ClientReservationCreateResponseDtoRoom? Room { get; set; }
    //}
    //public class ClientReservationCreateResponseDtoReservationInfo
    //{
    //    public bool Owner { get; set; }
    //    public string Name { get; set; }
    //    public string Phone { get; set; }
    //    public string Email { get; set; }
    //    public string Surname { get; set; }
    //}

    //public class ClientReservationCreateResponseDtoReservationItem
    //{
    //    public DateTime Day { get; set; }
    //    public Decimal Price { get; set; }
    //}
    //public class ClientReservationCreateResponseDtoVilla
    //{
    //    public string Name { get; set; }
    //    public string Slug { get; set; }
    //    public string Photo { get; set; }
    //}

    //public class ClientReservationCreateResponseDtoRoom
    //{
    //    public string Name { get; set; }
    //    public string Slug { get; set; }
    //    public string Photo { get; set; }
    //}
}
