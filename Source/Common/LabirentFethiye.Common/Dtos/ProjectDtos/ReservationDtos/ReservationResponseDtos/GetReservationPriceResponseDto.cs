using LabirentFethiye.Common.Enums;

namespace LabirentFethiye.Common.Dtos.ProjectDtos.ReservationDtos.ReservationResponseDtos
{
    public class GetReservationPriceResponseDto
    {
        public decimal Amount { get; set; }
        public decimal ExtraPrice { get; set; }
        public decimal Total { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public PriceType PriceType { get; set; }
        public ICollection<GetReservationPriceResponseDtoReservationPriceDay> Days { get; set; }
    }

    public class GetReservationPriceResponseDtoReservationPriceDay
    {
        public string Day { get; set; }
        public decimal Price { get; set; }
    }

}
