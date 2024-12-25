namespace LabirentFethiye.Common.Dtos.ProjectDtos.ClientDtos.ClientRequestDtos
{
    public class CalculatePriceForDate
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }
    }
}
