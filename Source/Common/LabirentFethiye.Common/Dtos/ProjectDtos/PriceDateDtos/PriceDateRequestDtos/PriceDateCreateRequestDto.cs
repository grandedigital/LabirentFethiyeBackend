namespace LabirentFethiye.Common.Dtos.ProjectDtos.PriceDateDtos.PriceDateRequestDtos
{
    public class PriceDateCreateRequestDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }
        public Guid? VillaId { get; set; }
        public Guid? RoomId { get; set; }
    }
}
