namespace LabirentFethiye.Common.Dtos.ProjectDtos.PriceDateDtos.PriceDateResponseDtos
{
    public class PriceDateGetResponseDto
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }

        public Guid? VillaId { get; set; }
        //public PriceDateGetResponseDtoVilla Villa { get; set; }
        public Guid? RoomId { get; set; }
        //public PriceDateGetResponseDtoRoom Room { get; set; }
    }
}
