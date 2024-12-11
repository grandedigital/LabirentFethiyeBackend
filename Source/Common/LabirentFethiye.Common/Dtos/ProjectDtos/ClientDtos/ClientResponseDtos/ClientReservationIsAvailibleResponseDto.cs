using LabirentFethiye.Common.Enums;

namespace LabirentFethiye.Common.Dtos.ProjectDtos.ClientDtos.ClientResponseDtos
{
    public class ClientReservationIsAvailibleResponseDto
    {
        public bool IsAvailible { get; set; }
        public decimal TotalPrice { get; set; }
        public PriceType PriceType { get; set; }
        public ICollection<ClientReservationIsAvailibleResponseDtoPriceDetails> PriceDetails { get; set; }
    }

    public class ClientReservationIsAvailibleResponseDtoPriceDetails
    {
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
        public PriceType PriceType { get; set; }
    }
}
