using LabirentFethiye.Common.Enums;

namespace LabirentFethiye.Common.Dtos.GlobalDtos.MailDtos.MailRequestDtos
{
    public class ReservationCreateMailRequestDto
    {
        public string ReservationNumber { get; set; }
        public string Note { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }

        public ReservationChannalType ReservationChannalType { get; set; }
        public ReservationStatusType ReservationStatusType { get; set; }

        public bool IsDepositPrice { get; set; }
        public bool IsCleaningPrice { get; set; }

        public PriceType PriceType { get; set; }
        public decimal Amount { get; set; }
        public decimal ExtraPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }

        public ReservationCreateMailRequestDtoVilla Villa { get; set; }
        public ReservationCreateMailRequestDtoRoom Room { get; set; }
        public ReservationCreateMailRequestDtoReservationInfos ReservationInfo { get; set; }
        //public ICollection<ReservationCreateMailRequestDtoReservationItems> ReservationItems { get; set; }
    }

    public class ReservationCreateMailRequestDtoVilla
    {
        public string Name { get; set; }
        public int Person { get; set; }
    }
    public class ReservationCreateMailRequestDtoRoom
    {
        public string HotelName { get; set; }
        public string Name { get; set; }
        public int Person { get; set; }
    }
    public class ReservationCreateMailRequestDtoReservationInfos {
        public string IdNo { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

    }
    //public class ReservationCreateMailRequestDtoReservationItems { }

}
