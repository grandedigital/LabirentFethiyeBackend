namespace LabirentFethiye.Common.Dtos.ProjectDtos.ClientDtos.ClientRequestDtos
{
    public class ClientReservationGetPriceRequestDto
    {
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public string Slug { get; set; }
    }
}
